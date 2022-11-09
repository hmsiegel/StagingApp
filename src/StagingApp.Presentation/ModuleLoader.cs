namespace StagingApp.Presentation;
public class ModuleLoader : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<KitchenConfigureViewModel>();
        builder.RegisterType<ServerConfigureViewModel>();
        builder.RegisterType<TerminalConfigureViewModel>();
    }
}
