namespace StagingApp.Presentation;
public class ModuleLoader : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<KitchenConfigureViewModel>().SingleInstance();
        builder.RegisterType<ServerConfigureViewModel>().SingleInstance();
        builder.RegisterType<TerminalConfigureViewModel>().SingleInstance();
    }
}
