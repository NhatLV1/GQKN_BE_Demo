
using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.Data.SqlClient;
using System.Globalization;

namespace PVI.GQKN.API.Infrastructure;

public class GQKNDbContextSeed
{
    public async Task SeedAsync(GQKNDbContext context,
        IWebHostEnvironment env,
        IOptions<AppSettings> settings,
        ILogger<GQKNDbContextSeed> logger)
    {

        var policy = CreatePolicy(logger, nameof(GQKNDbContextSeed));

        await policy.ExecuteAsync(async () =>
        {
            var contentRootPath = env.ContentRootPath;

            // Đơn vị
            if (!context.DonVi.Any())
            {
                string csvFile = Path.Combine(contentRootPath, "Setup", "DonVi.csv");
                if (File.Exists(csvFile))
                {
                    try
                    {
                        var entries = ReadCSV<CreateDonViRequest>(csvFile).ToList();
                        var entities = entries.Select(e => new DonVi()
                        {
                            MaDonVi = e.MaDonVi,
                            TenDonVi = e.TenDonVi,
                            MaTinh = e.MaTinh
                        });
                        context.AddRange(entities);
                        await context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {

                        logger.LogError(e.Message);
                    }
                   
                }
            }
            if (!context.ChucVu.Any())
            {
                string csvFile = Path.Combine(contentRootPath, "Setup", "ChucVu.csv");
                await ImportEntries<ChucVuClass, ChucDanh>(
                    csvFile,
                    context,
                    (i) => new ChucDanh() { 
                        TenChucVu = i.TenChucVu,
                        MaChucVu = i.MaChucVu
                    }, logger);
            }
            if (!context.PhongBan.Any())
            {
                var donVis = await context.DonVi.ToListAsync();
                string csvFile = Path.Combine(contentRootPath, "Setup", "PhongBan.csv");
                await ImportEntries<PhongBanClass, PhongBan>(
                    csvFile, 
                    context, 
                    (i) => {
                        var dv = donVis.FirstOrDefault(e => e.MaDonVi == i.DonViCode);

                        return new PhongBan()
                        {
                            DonViId = dv?.Id,
                            TenPhongBan = i.TenPhongBan,
                            MaPhongBan = i.MaPhongBan,
                            LoaiPhongBan = i.TenPhongBan.StartsWith("Phòng", StringComparison.InvariantCultureIgnoreCase) ? LoaiPhongBan.Phong : LoaiPhongBan.Ban,
                        };
                    }, logger);
            }
        });
    }

    private async Task ImportEntries<I, T>(string csvFile, 
        GQKNDbContext context, 
        Func<I, T> func,
        ILogger logger)
        where T : class
    {
        if (File.Exists(csvFile))
        {
            try
            {
                var entries = ReadCSV<I>(csvFile).ToList();
                var entities = entries.Select(e => func(e));
                context.Set<T>().AddRange(entities);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }
    }

    // https://code-maze.com/csharp-read-data-from-csv-file/
    private IEnumerable<T> ReadCSV<T>(string file)
    {
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            BadDataFound = null,
            HasHeaderRecord = true,
            TrimOptions = TrimOptions.Trim,
            IgnoreBlankLines = true,
            
        };
        configuration.MissingFieldFound = (args) => { };

        IEnumerable<T> items;
        using (var reader = new StreamReader(file))
        using (var csv = new CsvReader(reader, configuration))
        {
            var records = csv.GetRecords<T>();
            items = records.ToList();
        }
        return items;
    }

    private AsyncRetryPolicy CreatePolicy(ILogger<GQKNDbContextSeed> logger, string prefix, int retries = 3)
    {
        return Policy.Handle<SqlException>().
            WaitAndRetryAsync(
                retryCount: retries,
                sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                onRetry: (exception, timeSpan, retry, ctx) =>
                {
                    logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                }
            );
    }
}

internal sealed class PhongBanClass
{
    public string DonViCode { get; set; }
    public string MaPhongBan { get; set; }
    public string TenPhongBan { get; set; }
}

internal sealed class ChucVuClass
{
    public string TenChucVu { get; set; }
    public string MaChucVu { get; set; }
}