namespace StagingApp.Application.Terminal.Commands.SaveTerminalInfoAndSysPrep;

[SupportedOSPlatform("Windows7.0")]
internal sealed class SaveTerminalInfoAndSysPrepCommandHandler : ICommandHandler<SaveTerminalInfoAndSysPrepCommand>
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private readonly ICsvFileRepository _csvFileRepository;
    private readonly IApplicationService _applicationService;
    private readonly IConfiguration _config;

    public SaveTerminalInfoAndSysPrepCommandHandler(ICsvFileRepository csvFileRepository, IApplicationService applicationService, IConfiguration config)
    {
        _csvFileRepository = csvFileRepository;
        _applicationService = applicationService;
        _config = config;
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

        var sysprepargs = _config!.GetValue<string>("ApplicationSettings:SysPrepArguments") + ":" + Path.Join(GlobalConfig.SysPrepPath, GlobalConfig.Unattend);

        _applicationService.RunSysprep(sysprepargs);

        return Task.FromResult(Result.Success());
    }
}
