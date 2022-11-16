namespace StagingApp.Infrastructure;

[SupportedOSPlatform("Windows7.0")]
public class ModuleLoader : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RegistryService>().As<IRegistryService>();
    }
}
