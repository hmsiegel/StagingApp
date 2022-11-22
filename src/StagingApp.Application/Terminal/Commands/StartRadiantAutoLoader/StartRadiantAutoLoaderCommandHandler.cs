namespace StagingApp.Application.Terminal.Commands.StartRadiantAutoLoader;
internal sealed class StartRadiantAutoLoaderCommandHandler : ICommandHandler<StartRadiantAutoLoaderCommand>
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private readonly ICsvFileRepository _csvFileRepository;

    public StartRadiantAutoLoaderCommandHandler(ICsvFileRepository csvFileRepository)
    {
        _csvFileRepository = csvFileRepository;
    }

    public async Task<Result> Handle(StartRadiantAutoLoaderCommand request, CancellationToken cancellationToken)
    {
        _logger.Info("Third marker file found. Starting third pass...");

        var stagingCsv = Path.Combine(
            GlobalConfig.ScriptPath,
            CsvFiles.staging.ToString(),
            FileExtensions.csv.ConvertToFileExtension());

        var model = _csvFileRepository.ReadFromCsvFile<TerminalModel>(stagingCsv);

        request.Model.SetTerminalName(model.TerminalName);
        request.Model.SetTerminalIp(model.IpAddress);

        _logger.Info("Copying the RAL shortcut from the application path to the startup folder.");

        File.Copy(
            Path.Combine(GlobalConfig.ScriptPath, GlobalConfig.RalShortcut), 
            Path.Combine(GlobalConfig.CommonStartupFolder!, GlobalConfig.RalShortcut), 
            true);


        return default;

    }
}
