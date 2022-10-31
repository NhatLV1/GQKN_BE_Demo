using Autofac.Core;
using PVI.GQKN.Infrastructure.Contracts;

namespace PVI.GQKN.API.Infrastructure.AutofacModules;

public class ApplicationModule
    : Autofac.Module
{

    public string QueriesConnectionString { get; }
    public bool MockPVI { get; }

    public ApplicationModule(string qconstr, bool mockPVIAuth)
    {
        QueriesConnectionString = qconstr;
        MockPVI = mockPVIAuth;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<PhongBanRepository>()
            .As<IPhongBanRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<ChucDanhRepository>()
             .As<IChucDanhRepository>()
             .InstancePerLifetimeScope();

        builder.RegisterType<KhaiBaoTonThatRepository>()
           .As<IKhaiBaoTonThatRepository>()
           .InstancePerLifetimeScope();

        builder.RegisterType<RequestManager>()
            .As<IRequestManager>()
            .InstancePerLifetimeScope();

        builder.RegisterType<UserQueries>()
            .As<IUserQueries>()
            .InstancePerLifetimeScope();

        builder.RegisterType<AuthService>()
            .As<IAuthService>()
            .InstancePerLifetimeScope();

        if (MockPVI)
        {
            builder.RegisterType<MockAuthPVIService>()
               .As<IAuthPVI>()
               .InstancePerLifetimeScope();
        }
        else
        {
            builder.RegisterType<AuthPVIService>()
                  .As<IAuthPVI>()
                  .InstancePerLifetimeScope();
        }

        builder.RegisterType<RazorViewRenderService>()
            .As<IViewRenderService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<DonViRepository>()
             .As<IDonViRepository>()
             .InstancePerLifetimeScope();

        builder.RegisterType<HoSoTonThatRepository>()
             .As<IHoSoTonThatRepository>()
             .InstancePerLifetimeScope();

        builder.RegisterType<UserRepository>()
         .As<IUserRepository>()
         .InstancePerLifetimeScope();

        builder.RegisterType<ApplicationRoleRepository>()
          .As<IApplicationRoleRepository>()
          .InstancePerLifetimeScope();


        //builder.RegisterAssemblyTypes(typeof(CreateOrderCommandHandler).GetTypeInfo().Assembly)
        //    .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

    }
}
