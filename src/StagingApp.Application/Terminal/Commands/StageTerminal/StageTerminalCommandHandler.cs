namespace StagingApp.Application.Terminal.Commands.StartStageTerminal;

[SupportedOSPlatform("Windows7.0")]
internal sealed class StageTerminalCommandHandler : ICommandHandler<StageTerminalCommand>
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private readonly ISystemEnvironmentService _systemEnvironmentService;
    private readonly IConfiguration _config;

    public StageTerminalCommandHandler(
        ISystemEnvironmentService systemEnvironmentService,
        IConfiguration config)
    {
        _systemEnvironmentService = systemEnvironmentService;
        _config = config;
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
