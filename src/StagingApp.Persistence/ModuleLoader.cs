namespace StagingApp.Persistence;
public class ModuleLoader : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CsvFileRepository>().As<ICsvFileRepository>();
        builder.RegisterType<DownloadRepository>().As<IDownloadRepository>();
    }
}
