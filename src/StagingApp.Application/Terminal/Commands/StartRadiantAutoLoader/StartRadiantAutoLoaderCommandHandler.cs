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
    private readonly IRegistryService _registryService;
    private readonly IEmailService _emailService;

    public StartRadiantAutoLoaderCommandHandler(
        ICsvFileRepository csvFileRepository,
        IRegistryService registryService,
        IConfiguration config,
        IEmailService emailService)
    {
        _csvFileRepository = csvFileRepository;
        _registryService = registryService;
        _config = config;
        _emailService = emailService;
    }

    public Task<Result> Handle(StartRadiantAutoLoaderCommand request, CancellationToken cancellationToken)
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

        _logger.Info("Deleting application directory and files...");
        FileSystemHelper.RemoveFilesAndDirectories(
            GlobalConfig.ScriptPath,
            _config.GetValue<string[]>("FileSettings:ExtensionsToExclude")!);

        FileSystemHelper.DeleteMarkerFile(Path.Join(
            GlobalConfig.ScriptPath, (
                string.Join(
                    "",
                    MarkerFiles.third.ToString(),
                    FileExtensions.marker.ConvertToFileExtension()))));

        _logger.Info("Create the BigFix baseline marker file.");
        FileSystemHelper.CreateMarkerFile(Path.Join(
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

        string? emailMessage = _emailService.CreateEmailMessage(request.Model);

        _logger.Info("Sending email...");
        _emailService.SendEmailMessage(
            GlobalConfig.FromAddress!,
            "Terminal Staging Complete",
            emailMessage);

        ApplicationHelper.RunProcessInCurrentDirectory(
            GlobalConfig.RalExe,
            GlobalConfig.RalPath!,
            true,
            false,
            false);

        return Task.FromResult(Result.Success());
    }
}
