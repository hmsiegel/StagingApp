namespace StagingApp.Application.Terminal.Commands.SaveTerminalInfoAndSysPrep;
internal sealed class SaveTerminalInfoAndSysPrepCommandHandler : ICommandHandler<SaveTerminalInfoAndSysPrepCommand>
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private readonly ICsvFileRepository _csvFileRepository;

    public SaveTerminalInfoAndSysPrepCommandHandler(ICsvFileRepository csvFileRepository)
    {
        _csvFileRepository = csvFileRepository;
    }

    public Task<Result> Handle(SaveTerminalInfoAndSysPrepCommand request, CancellationToken cancellationToken)
    {
        ApplicationHelper.EndProcess(GlobalConfig.Osk);

        string outfile = Path.Combine(GlobalConfig.ScriptPath, $"{CsvFiles.staging}.csv");
        var terminalModel = new List<TerminalModel> { request.Model };
        _csvFileRepository.WriteToCsvFile(terminalModel, outfile, false);

        _logger.Info("Terminal info saved to csv file.");

        FileSystemHelper.CreateMarkerFile(Path.Combine(
            GlobalConfig.ScriptPath,
            MarkerFiles.first.ToString(),
            FileExtensions.marker.ConvertToFileExtension()));
        File.Create(Path.Combine(
            GlobalConfig.ScriptPath,
            StagingRegistryKey.TerminalStaging.ToString(),
            FileExtensions.stage.ConvertToFileExtension()));

        ApplicationHelper.RunSysPrep();

        return Task.FromResult(Result.Success());
    }
}
