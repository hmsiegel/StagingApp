namespace StagingApp.Infrastructure;

[SupportedOSPlatform("Windows7.0")]
public class ModuleLoader : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ApplicationService>().As<IApplicationService>();
        builder.RegisterType<CsvLoggingService>().As<ICsvLoggingService>();
        builder.RegisterType<DeviceTypeService>().As<IDeviceTypeService>();
        builder.RegisterType<EmailService>().As<IEmailService>();
        builder.RegisterType<FileSystemService>().As<IFileSystemService>();
        builder.RegisterType<RegistryService>().As<IRegistryService>();
        builder.RegisterType<SystemEnvironmentService>().As<ISystemEnvironmentService>();
    }
}
