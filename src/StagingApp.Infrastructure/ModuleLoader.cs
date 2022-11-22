namespace StagingApp.Infrastructure;

[SupportedOSPlatform("Windows7.0")]
public class ModuleLoader : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RegistryService>().As<IRegistryService>();
        builder.RegisterType<ApplicationService>().As<IApplicationService>();
        builder.RegisterType<CsvLoggingService>().As<ICsvLoggingService>();
        builder.RegisterType<EmailService>().As<IEmailService>();
    }
}
