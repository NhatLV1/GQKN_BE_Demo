
using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.Data.SqlClient;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.AspNetCore.Identity;

namespace PVI.GQKN.API.Infrastructure;

public class IdentityAuthorizationSeed
{
    public async Task SeedAsync(GQKNDbContext context,
        IWebHostEnvironment env,
        IOptions<AppSettings> settings,
        IServiceProvider services)
    {
        var userManager = services.GetService<UserManager<ApplicationUser>>();
        var roleManager = services.GetService<RoleManager<ApplicationRole>>();
        var logger = services.GetService<ILogger<IdentityAuthorizationSeed>>();
        var authService = services.GetService<IAuthService>();

        var policy = CreatePolicy(logger, nameof(IdentityAuthorizationSeed));

        await policy.ExecuteAsync(async () =>
        {
            var useCustomizationData = settings.Value
                .UseCustomizationData;

            var contentRootPath = env.ContentRootPath;

            // Role
            //if (!context.ApplicationUserRoles.Any())
            //{
            //    string csvRole = Path.Combine(contentRootPath, "Setup", "Roles.csv");
            //    if (File.Exists(csvRole))
            //    {
            //        int id = 1;
            //        var roles = File.ReadAllLines(csvRole)
            //                                    .Skip(1) // skip header row
            //                                    .SelectTry(x => CreateRole(x, ref id))
            //                                    .OnCaughtException(ex =>
            //                                    {
            //                                        logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message); return null;
            //                                    })
            //                                    .Where(x => x != null);

            //        foreach (var roleClaim in roles)
            //        {
            //            var role = roleClaim[0];
            //            var claims = roleClaim.Length > 0 ? roleClaim[1].Split(";") : null;

            //            // role
            //            var userRole = await roleManager.EnsureRole(role);

            //            // role claims
            //            if (claims != null)
            //            {
            //                foreach (var claim in claims)
            //                {
            //                    var kv = claim.Split(":", StringSplitOptions.RemoveEmptyEntries);
            //                    if (kv.Length > 0)
            //                    {
            //                        var k = kv[0];
            //                        if (k == "*")
            //                        {
            //                            authService.GetACL();
            //                        }
            //                        else if (kv.Length > 1)
            //                        {

            //                        }
            //                        else
            //                        { 
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            // ensure role admin
            
            var adminRole = await roleManager.FindByNameAsync("admin");
            if (adminRole == null)
            {
                adminRole = await roleManager.EnsureRole("admin");
                var adminClaims = authService.AggregateClaim(ADMIN_OPS.ACL_SCOPE).ToString();
                var kbttClaims = authService.AggregateClaim(KBTT_OPS.ACL_SCOPE).ToString();
                var bcttClaims = authService.AggregateClaim(BCTT_OPS.ACL_SCOPE).ToString();
              
                await roleManager.AddClaimAsync(adminRole, new Claim(ADMIN_OPS.ACL_SCOPE, adminClaims));
                await roleManager.AddClaimAsync(adminRole, new Claim(KBTT_OPS.ACL_SCOPE, kbttClaims));
                await roleManager.AddClaimAsync(adminRole, new Claim(BCTT_OPS.ACL_SCOPE, bcttClaims));
            }

            // User
            //if (!context.ApplicationUsers.Any())
            //{
            //    // role claims
            //    string csvFile = Path.Combine(contentRootPath, "Setup", "Users.csv");
            //    if (File.Exists(csvFile))
            //    {
            //        dynamic users = File.ReadAllLines(csvFile)
            //            .Skip(1)
            //            .SelectTry(x => CreateUser(x))
            //              .OnCaughtException(ex =>
            //              {
            //                  logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message); return null;
            //              }).Where(x => x != null);

            //        foreach (var user in users)
            //        {
            //            var uid = await userManager.EnsureUser(user.Username as string, user.Password as string);
            //            if (user.Roles != null)
            //            {
            //                foreach (var role in user.Roles)
            //                {
            //                    await userManager.EnsureUserRole(uid, role as string);
            //                }
            //            }
            //        }

            //    }
            //}

            // ensure admin user
            var adminRoleName = "admin";
            await EnsureSuperAdmin("admin", "P@ssw0rd", adminRoleName, userManager);
            await EnsureSuperAdmin("superadmin", "P@ssw0rd", adminRoleName, userManager);
        });
    }

    private async Task EnsureSuperAdmin(string username, string password, string adminRoleName,
        UserManager<ApplicationUser> userManager)
    {
      
        var adminUser = await userManager.FindByNameAsync(username);
        if (adminUser == null)
        {
            var uid = await userManager.EnsureUser(username, password);

        }
        await userManager.EnsureClaimAsync(adminUser, IAuthService.SUPER_ADMIN_CLAIM);
        await userManager.EnsureUserRole(adminUser.Id, adminRoleName);
    }

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

    private object CreateUser(string x)
    {
        if (string.IsNullOrEmpty(x))
            throw new Exception("User is null or empty");

        var segments = x.Split(",");
        if (segments.Length < 2)
            throw new Exception($"Missing username or password {x}");

        return new
        {
            Username = segments[0],
            Password = segments[1],
            Roles = segments.Length > 2 ? segments[2].Split(":") : null
        };
    }


    private string[] CreateRole(string value, ref int id)
    {
        if (String.IsNullOrEmpty(value))
        {
            throw new Exception("Role is null or empty");
        }

        var values = value.Split(",", StringSplitOptions.RemoveEmptyEntries);

        return values;
    }

    private AsyncRetryPolicy CreatePolicy(ILogger<IdentityAuthorizationSeed> logger, string prefix, int retries = 3)
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
