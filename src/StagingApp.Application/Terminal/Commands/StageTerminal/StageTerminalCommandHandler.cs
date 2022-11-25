namespace StagingApp.Application.Terminal.Commands.StartStageTerminal;

[SupportedOSPlatform("Windows7.0")]
internal sealed class StageTerminalCommandHandler : ICommandHandler<StageTerminalCommand>
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private readonly ISystemEnvironmentService _systemEnvironmentService;
    private readonly IFileSystemService _fileSystemService;
    private readonly IRegistryService _registryService;
    private readonly IApplicationService _appService;
    private readonly IConfiguration _config;

    public StageTerminalCommandHandler(
        ISystemEnvironmentService systemEnvironmentService,
        IConfiguration config,
        IFileSystemService fileSystemService,
        IRegistryService registryService,
        IApplicationService appService)
    {
        _systemEnvironmentService = systemEnvironmentService;
        _config = config;
        _fileSystemService = fileSystemService;
        _registryService = registryService;
        _appService = appService;
    }

    public async Task<Result> Handle(StageTerminalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await Task.CompletedTask;

            var model = TerminalModel.Create(
                request.Model.TerminalName,
                request.Model.IpAddress);

            _logger.Info("Setting terminal name to {terminalName}", model.TerminalName);
            _systemEnvironmentService.SetCompName(model.TerminalName);

            var gateway = NetworkManagementService.GetGateway(model.IpAddress);

            var adapter = NetworkAdapter.Create(
                model.IpAddress,
                _config.GetValue<string>("NetworkSettings:Subnet")!,
                _config.GetValue<string>("NetworkSettings:DNS1")!,
                _config.GetValue<string>("NetworkSettings:DNS2")!,
                gateway);

            _logger.Info("Setting IP address to {ip}", model.IpAddress);
            _systemEnvironmentService.SetIPInformation(
                adapter.NicName!,
                adapter.IpAddress!,
                adapter.Subnet!,
                adapter.Gateway!,
                adapter.DNS1!,
                adapter.DNS2!,
                false);

            Thread.Sleep(30000);

            _logger.Info("Setting secure auto logon");
            _systemEnvironmentService.SetSecureAutoLogon(
                _config.GetValue<string>("PasswordSettings:AlohaAdminUser")!,
                _config.GetValue<string>("PasswordSettings:AlohaAdminPasword")!,
                model.TerminalName);

            Thread.Sleep(30000);

            _fileSystemService.DeleteMarkerFile(
                Path.Join(
                    GlobalConfig.ScriptPath,
                    string.Join(
                        "",
                        MarkerFiles.first.ToString(),
                        FileExtensions.done.ConvertToFileExtension())));

            _fileSystemService.CheckForMarkerFile(
                Path.Join(
                    GlobalConfig.ScriptPath,
                    string.Join(
                        "",
                        MarkerFiles.second.ToString(),
                        FileExtensions.done.ConvertToFileExtension())));

            _registryService.DeleteRunRegistryKey(GlobalConfig.RunKey, StagingRegistryKey.TerminalStaging.ToString());
            _registryService.CreateRunOnceRegistryKey(GlobalConfig.RunOnceKey, StagingRegistryKey.TerminalStaging.ToString());

            _logger.Info("First configuration is complete.");
            _appService.RunProcess(GlobalConfig.ShutdownCommand, true, true, false);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return Result.Failure(DomainErrors.Terminal.StageTerminal);
            throw;
        }
    }
}
