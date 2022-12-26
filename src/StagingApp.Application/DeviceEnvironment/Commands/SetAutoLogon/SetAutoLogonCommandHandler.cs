namespace StagingApp.Application.DeviceEnvironment.Commands.SetAutoLogon;
internal sealed class SetAutoLogonCommandHandler : ICommandHandler<SetAutoLogonCommand>
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly ISystemEnvironmentService _systemEnvironmentService;
    private readonly IConfiguration _config;

    public SetAutoLogonCommandHandler(
        ISystemEnvironmentService systemEnvironmentService,
        IConfiguration config)
    {
        _systemEnvironmentService = systemEnvironmentService;
        _config = config;
    }

    public async Task<Result> Handle(SetAutoLogonCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _logger.Info("Setting secure auto logon");
        _systemEnvironmentService.SetSecureAutoLogon(
            _config.GetValue<string>("PasswordSettings:AlohaAdminUser")!,
            _config.GetValue<string>("PasswordSettings:AlohaAdminPasword")!,
            request.DeviceName);

        Thread.Sleep(30000);

        return Result.Success();
    }
}
