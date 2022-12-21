namespace StagingApp.Application.Terminal.Commands.SaveTerminalInfoAndSysPrep;

[SupportedOSPlatform("Windows7.0")]
internal sealed class SaveTerminalInfoCommandHandler : ICommandHandler<SaveTerminalInfoCommand>
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private readonly ICsvFileRepository _csvFileRepository;
    private readonly IApplicationService _appService;

    public SaveTerminalInfoCommandHandler(
        ICsvFileRepository csvFileRepository,
        IApplicationService appService)
    {
        _csvFileRepository = csvFileRepository;
        _appService = appService;
    }

    public Task<Result> Handle(SaveTerminalInfoCommand request, CancellationToken cancellationToken)
    {
        _appService.EndProcess(GlobalConfig.Osk);

        string outfile = Path.Combine(
            GlobalConfig.ScriptPath,
            CsvFiles.staging.ToString(),
            FileExtensions.csv.ConvertToFileExtension());

        var terminalModel = new List<TerminalModel> { request.Model };
        _csvFileRepository.WriteToCsvFile(terminalModel, outfile, false);

        _logger.Info("Terminal info saved to csv file.");

        return Task.FromResult(Result.Success());
    }
}
