namespace StagingApp.Application.Terminal.Commands.StartRadiantAutoLoader;

[SupportedOSPlatform("Windows7.0")]
internal sealed class StartRadiantAutoLoaderCommandHandler : ICommandHandler<StartRadiantAutoLoaderCommand>
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private const string _registryKey = "SYSTEM\\FSS";
    private const string _registryKeyValue = "Version";
    private const string _registryData = "1.0";

    private readonly ICsvFileRepository _csvFileRepository;
    private readonly IConfiguration _config;
    private readonly IEmailService _emailService;
    private readonly IFileSystemService _fileSystemService;
    private readonly IRegistryService _registryService;
    private readonly IApplicationService _applicationService;

    public StartRadiantAutoLoaderCommandHandler(
        ICsvFileRepository csvFileRepository,
        IConfiguration config,
        IEmailService emailService,
        IFileSystemService fileSystemService,
        IRegistryService registryService,
        IApplicationService applicationService)
    {
        _csvFileRepository = csvFileRepository;
        _config = config;
        _emailService = emailService;
        _fileSystemService = fileSystemService;
        _registryService = registryService;
        _applicationService = applicationService;
    }

    public Task<Result> Handle(StartRadiantAutoLoaderCommand request, CancellationToken cancellationToken)
    {

        _logger.Info("Third marker file found. Starting third pass...");

        var stagingCsv = Path.Combine(
            GlobalConfig.ScriptPath,
            CsvFiles.staging.ToString(),
            FileExtensions.csv.ConvertToFileExtension());

        var csvModel = _csvFileRepository.ReadFromCsvFile<TerminalModel>(stagingCsv);

        var terminalModel = TerminalModel.Create(
            csvModel.TerminalName,
            csvModel.IpAddress);


        _logger.Info("Copying the RAL shortcut from the application path to the startup folder.");

        File.Copy(
            Path.Combine(GlobalConfig.ScriptPath, GlobalConfig.RalShortcut),
            Path.Combine(GlobalConfig.CommonStartupFolder!, GlobalConfig.RalShortcut),
            true);

        _logger.Info("Deleting application directory and files...");
        _fileSystemService.RemoveFilesAndDirectories(
            GlobalConfig.ScriptPath,
            _config.GetValue<string[]>("FileSettings:ExtensionsToExclude")!);

        _fileSystemService.DeleteMarkerFile(Path.Join(
            GlobalConfig.ScriptPath, (
                string.Join(
                    "",
                    MarkerFiles.third.ToString(),
                    FileExtensions.marker.ConvertToFileExtension()))));

        _logger.Info("Create the BigFix baseline marker file.");
        _fileSystemService.CreateMarkerFile(Path.Join(
            GlobalConfig.ScriptPath, (
                string.Join(
                    "",
                    MarkerFiles.bigfix.ToString(),
                    FileExtensions.marker.ConvertToFileExtension()))));

        _logger.Info("Create the version registry key...");
        _registryService.EditRegistryFromValues(
            RegistryHive.LocalMachine,
            _registryKey,
            _registryKeyValue,
            _registryData,
            RegistryValueKind.String);

        string? emailMessage = _emailService.CreateEmailMessage(terminalModel);

        _logger.Info("Sending email...");
        _emailService.SendEmailMessage(
            GlobalConfig.FromAddress!,
            "Terminal Staging Complete",
            emailMessage);

        _applicationService.RunProcessInCurrentDirectory(
            GlobalConfig.RalExe,
            GlobalConfig.RalPath!,
            true,
            false,
            false);

        return Task.FromResult(Result.Success());
    }
}
