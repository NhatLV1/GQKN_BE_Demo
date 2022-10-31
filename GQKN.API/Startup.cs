//#define USE_HTTPS
//#define USE_RESPONSE_COMPRESSION
//#define USE_SPA
//#define USE_SIGNALR
//#define MOCK_PVI

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Hosting.Internal;
using PVI.GQKN.API.Infrastructure.VirtualFileProvider;
using PVI.GQKN.API.Services.Auth;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace PVI.GQKN.API;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        Env = env;
    }

    public IConfiguration Configuration { get; }

    public ILifetimeScope AutofacContainer { get; private set; }

    public IWebHostEnvironment Env { get; }

    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services"></param>
    public virtual void ConfigureServices(IServiceCollection services)
    {
        services
            .AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies()) // AutoMapping
            .AddMediatR(typeof(Startup)) // CQRS
            .AddBackgroundServices()
            .AddCustomDbContext(Configuration)
            .AddFileProviders(Configuration)
            .AddCustomMVC(Configuration)
            .AddCustomSwagger(Configuration)
#if USE_SIGNALR
            .AddCustomSignalR(Configuration)
#endif
#if !DEBUG
            //.AddCustomReactServices(Configuration, Env)
#endif
            //.AddEventBus(Configuration);
            .AddCustomAuth(Configuration)
            .AddOptions() // AutoFac
            .AddCustomConfiguration(Configuration)
            .AddCustomInteragration(Configuration);
    }

    // ConfigureContainer is where you can register things directly
    // with Autofac. This runs after ConfigureServices so the things
    // here will override registrations made in ConfigureServices.
    // Don't build the container; that gets done for you by the factory.
    public void ConfigureContainer(ContainerBuilder builder)
    {
        bool mockAuth = false;
#if MOCK_PVI
        mockAuth = true;
#endif
        // Register your own things directly with Autofac here. Don't
        // call builder.Populate(), that happens in AutofacServiceProviderFactory
        // for you.
        builder.RegisterModule(new ApplicationModule(
            Configuration["ConnectionString"], mockAuth));
        builder.RegisterModule(new MediatorModule());
    }

    // Middlewares setup
    /// <summary>
    /// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app,
        IWebHostEnvironment env)
    {
        // If, for some reason, you need a reference to the built container, you
        // can use the convenience extension method GetAutofacRoot.
        this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

#if USE_HTTPS
            app.UseHsts();
#endif
        }

#if USE_HTTPS
        app.UseHttpsRedirection();
#endif
        var pathBase = Configuration["PATH_BASE"];
        if (!string.IsNullOrEmpty(pathBase))
        {
            //loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
            app.UsePathBase(pathBase);
        }

        app.UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{(!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty)}/swagger/v1/swagger.json", "PVI.GQKN.API v1");
                c.OAuthClientId("gqknswaggerui");
                c.OAuthAppName("GQKN Swagger UI");
                c.DocExpansion(DocExpansion.List);
            });

        app.UseStaticFiles();

        app.UseRouting();

#if !DEBUG && USE_SPA
        app.UseSpaStaticFiles();
#endif

#if USE_RESPONSE_COMPRESSION
        app.UseResponseCompression();
#endif

        app.UseCors("CorsPolicy");

        // responsible for validating the request credentials and setting the user on the request context
        app.UseAuthentication();

        // The IdentityServer middleware that exposes the OpenID Connect endpoints
        app.UseIdentityServer();

        // enable authorization

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            endpoints.MapRazorPages();

            // SignalR
            //endpoints.MapHub<TestingHub>("/hubs/testing");
        });
#if USE_SPA
        app.UseSpa(spa =>
        {
            //spa.Options.SourcePath = "ClientApp";
#if DEBUG
            if (env.IsDevelopment())
            {
                //spa.UseReactDevelopmentServer(npmScript: "start");
                spa.UseProxyToSpaDevelopmentServer("https://localhost:3000");
            }
#endif
        });
#endif
    }
}

static class StartupExtentions
{
    public static IServiceCollection AddFileProviders(this IServiceCollection services, IConfiguration configuration)
    {
        IFileProvider physicalProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
        services.AddSingleton(physicalProvider);
        services.AddSingleton<DatabaseFileProvider>();
        return services;
    }

    public static IServiceCollection AddCustomAuth(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Identity with the default UI
        services.AddDefaultIdentity<ApplicationUser>
                (options => options.SignIn.RequireConfirmedAccount = false) //TODO: verify
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<GQKNDbContext>();

        // sets up some default ASP.NET Core conventions on top of IdentityServer
        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, GQKNDbContext>();

        // Authentication with an additional AddIdentityServerJwt helper method that configures
        // the app to validate JWT tokens produced by IdentityServer
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
        services.AddSingleton(tokenValidationParameters);
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddIdentityServerJwt()
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = tokenValidationParameters;
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];

                    // If the request is for our hub...
                    var path = context.HttpContext.Request.Path;
                    if (
                            (path.StartsWithSegments("/hubs")))
                    {
                        if (!string.IsNullOrEmpty(accessToken))
                            // Read the token out of the query string
                            context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
        });

        //        services.AddAuthorization(options =>
        //        {
        //            // Bỏ qua authenticate và authorization khi debug
        //#if DEBUG && false
        //            options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
        //               .RequireAssertion(e => true).Build();
        //#endif
        //        });
        // Replace the default authorization policy provider with our own
        // custom provider which can return authorization policies for given
        // policy names (instead of using the default policy provider)
        services.AddSingleton<IAuthorizationPolicyProvider, ClaimPolicyProvider>();

        // As always, handlers must be provided for the requirements of the authorization policies
        services.AddSingleton<IAuthorizationHandler, ClaimAuthorizationHandler>();

        return services;
    }

    public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
    {
        #region Timer Serivces
        //services.AddHostedService<CloakTokenService>();
        #endregion
        return services;
    }

    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        // https://learn.microsoft.com/en-us/ef/core/performance/advanced-performance-topics?tabs=with-di%2Cwith-constant#dbcontext-pooling

        services.AddDbContext<GQKNDbContext>(options =>
        //services.AddDbContextPool<GQKNDbContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionString"],
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            }
            // Without ContextPool
            , ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
         );

        //services.AddDbContextPool<IntegrationEventLogContext>(options =>
        services.AddDbContext<IntegrationEventLogContext>(options =>
        {
            options.UseSqlServer(configuration["ConnectionString"],
                                    sqlServerOptionsAction: sqlOptions =>
                                    {
                                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                        //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                    });
        });

#if DEBUG
        //services.AddDatabaseDeveloperPageExceptionFilter();
#endif
        return services;
    }

    public static IServiceCollection AddCustomMVC(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews(options =>
        {
            options.Filters.Add(typeof(HttpGlobalExceptionFilter));
        })
            //.AddJsonOptions(options => options.JsonSerializerOptions.)
        //.AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
        .Services.AddRazorPages()
        .AddRazorRuntimeCompilation(builder => { });

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                .SetIsOriginAllowed((host) => {
                    return true;
                })
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });
        var sp = services.BuildServiceProvider();
        services.Configure<MvcRazorRuntimeCompilationOptions>((opts) =>
        {
            var provider = sp.GetRequiredService<DatabaseFileProvider>();
            opts.FileProviders.Add(provider);
        });

        return services;
    }

    public static IServiceCollection AddCustomSignalR(this IServiceCollection services, IConfiguration configuration)
    {
        // SignalR
        services.AddSignalR()
            //.AddNewtonsoftJsonProtocol()
            ;

        return services;
    }

    public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "PVI GQKN - HTTP API",
                Version = "v1",
                Description = "GQKN HTTP API"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"Xác thực JWT sử dụng Bearer scheme.  
                      Nhập 'Bearer [Token]' vào ô dưới.
                      Ví dụ: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                  new OpenApiSecurityScheme
                  {
                    Reference = new OpenApiReference
                      {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                      },
                      Scheme = "oauth2",
                      Name = "Bearer",
                      In = ParameterLocation.Header,
                    },
                    new List<string>()
                  }
             });

            //options.OperationFilter<AuthorizeCheckOperationFilter>();
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }

    public static IServiceCollection AddCustomReactServices(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment env)
    {
        // In production, the React files will be served from this directory
        services.AddSpaStaticFiles(config =>
        {
            var appConfigFile = configuration["AppConfig"];
            var rootPath = "ClientApp";

            config.RootPath = rootPath;
        });

        return services;
    }

    public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddScoped<IUrlHelper>(factory =>
        {
            var actionContext = factory.GetService<IActionContextAccessor>().ActionContext;
            return new UrlHelper(actionContext);
        });

        //add http client services
        services.AddHttpClient<IAuthPVI, AuthPVIService>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Sample. Default lifetime is 2 minutes
                                                              //.AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                                                              //.AddDevspacesSupport()
                ;

        return services;
    }

    public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();
        services.Configure<AppSettings>(configuration);
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Please refer to the errors property for additional details."
                };

                return new BadRequestObjectResult(problemDetails)
                {
                    ContentTypes = { "application/problem+json", "application/problem+xml" }
                };
            };
        });

        return services;
    }

    public static IServiceCollection AddCustomInteragration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IIdentityService, IdentityService>();
        return services;
    }

    //public static IServiceCollection AddIntegrationServices(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
    //        sp => (DbConnection c) => new IntegrationEventLogService(c));

    //    services.AddTransient<IOrderIntegrationEventService, OrderIntegrationEventService>();

    //    if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
    //    {
    //        services.AddSingleton<IServiceBusPersisterConnection>(sp =>
    //        {
    //            var settings = sp.GetRequiredService<IOptions<AppSettings>>().Value;
    //            var serviceBusConnection = settings.EventBusConnection;

    //            return new DefaultServiceBusPersisterConnection(serviceBusConnection);
    //        });
    //    }
    //    else
    //    {
    //        services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
    //        {
    //            var settings = sp.GetRequiredService<IOptions<AppSettings>>().Value;
    //            var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

    //            var factory = new ConnectionFactory()
    //            {
    //                HostName = configuration["EventBusConnection"],
    //                DispatchConsumersAsync = true
    //            };

    //            if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
    //            {
    //                factory.UserName = configuration["EventBusUserName"];
    //            }

    //            if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
    //            {
    //                factory.Password = configuration["EventBusPassword"];
    //            }

    //            var retryCount = 5;
    //            if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
    //            {
    //                retryCount = int.Parse(configuration["EventBusRetryCount"]);
    //            }

    //            return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
    //        });
    //    }

    //    return services;
    //}


    //public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    //{
    //    if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
    //    {
    //        services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
    //        {
    //            var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
    //            var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
    //            var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
    //            var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
    //            string subscriptionName = configuration["SubscriptionClientName"];

    //            return new EventBusServiceBus(serviceBusPersisterConnection, logger,
    //                eventBusSubcriptionsManager, iLifetimeScope, subscriptionName);
    //        });
    //    }
    //    else
    //    {
    //        //services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
    //        //{
    //        //    var subscriptionClientName = configuration["SubscriptionClientName"];
    //        //    var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
    //        //    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
    //        //    var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
    //        //    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

    //        //    var retryCount = 5;
    //        //    if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
    //        //    {
    //        //        retryCount = int.Parse(configuration["EventBusRetryCount"]);
    //        //    }

    //        //    return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
    //        //});
    //    }

    //    services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
    //    //services.AddTransient<OrderScheduledIntegrationEventHandler>();
    //    //services.AddTransient<OrderStatusChangedToPaidIntegrationEventHandler>();

    //    return services;
    //}

}
