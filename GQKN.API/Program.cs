using Serilog;

namespace PVI.GQKN.API;

public class Program
{
    public static void Main(string[] args)
    {
        var configuration = GetConfiguration();
        Log.Logger = CreateSerilogLogger(configuration);

        var builder = CreateHostBuilder(configuration, args);
        var host = builder.Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            // Test commit
            // migrate database on startup
            Log.Information("migrate database on startup");
            var context = services.GetRequiredService<GQKNDbContext>();
            context.Database.Migrate();

            var env = services.GetService<IWebHostEnvironment>();
            var settings = services.GetService<IOptions<AppSettings>>();
            var logger = services.GetService<ILogger<GQKNDbContextSeed>>();

            try
            {
                using (context)
                {
                    new GQKNDbContextSeed()
                        .SeedAsync(context, env, settings, logger)
                         .Wait();

                    new IdentityAuthorizationSeed()
                        .SeedAsync(context, env, settings, services)
                        .Wait();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occurred seeding the DB.");
            }

            var integrationContext = services.GetRequiredService<IntegrationEventLogContext>();
            integrationContext.Database.Migrate();
        }

        host.Run();
    }

    static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
    {
        var seqServerUrl = configuration["Serilog:SeqServerUrl"];
        var logstashUrl = configuration["Serilog:LogstashgUrl"];
        return new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.WithProperty("ApplicationContext", Program.AppName)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
            .WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://logstash:8080" : logstashUrl, null, null)
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }

    static IConfiguration GetConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        return builder.Build();
    }

    public static IHostBuilder CreateHostBuilder(IConfiguration configuration, string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }).ConfigureServices((hostContext, services) =>
            {
                #region Queue Tasks Services
                //services.AddSingleton<MonitorLoop>();
                //services.AddHostedService<QueuedHostedService>();
                //services.AddSingleton<IBackgroundTaskQueue>(ctx =>
                //{
                //    if (!int.TryParse(hostContext.Configuration["QueueCapacity"], out var queueCapacity))
                //        queueCapacity = 100;
                //    return new BackgroundTaskQueue(queueCapacity);
                //});
                #endregion

                #region Timer Serivces
                //services.AddHostedService<TimedHostedService>();
                #endregion

                #region Scope Services
                //services.AddHostedService<ConsumeScopedServiceHostedService>();
                //services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
                #endregion
            });

    public static string Namespace = typeof(Startup).Namespace;

    public static string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
}
