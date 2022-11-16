namespace StagingApp.Application.DeviceEnvironment.Commands.SetAutoLogon;

[SupportedOSPlatform("Windows7.0")]
internal sealed class SetAutoLogonCommandHandler : ICommandHandler<SetAutoLogonCommand>
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private readonly IRegistryService _registryService;

    public SetAutoLogonCommandHandler(IRegistryService registryService)
    {
        _registryService = registryService;
    }

    public async Task<Result> Handle(SetAutoLogonCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        _logger.Info("Attempting to set auto logon.");

        try
        {
            _logger.Info("Storing the password securely.");
            LsaModel lsa = new(SetAutoLogonConfig.LsaKey);
            lsa.SetSecret(request.Password);

            _logger.Info("Setting AutoAdminLogon to 1");
            _registryService.EditRegistryFromValues(
                RegistryHive.LocalMachine,
                SetAutoLogonConfig.Path,
                SetAutoLogonConfig.AutoAdminLogon,
                SetAutoLogonConfig.EnableValue,
                RegistryValueKind.String);

            _logger.Info("Setting DefaultUserName to 1");
            _registryService.EditRegistryFromValues(
                RegistryHive.LocalMachine,
                SetAutoLogonConfig.Path,
                SetAutoLogonConfig.DefaultUsername,
                SetAutoLogonConfig.EnableValue,
                RegistryValueKind.String);

            _logger.Info($"Setting Default DomainName to {request.Domain}.");
            _registryService.EditRegistryFromValues(
                RegistryHive.LocalMachine,
                SetAutoLogonConfig.Path,
                SetAutoLogonConfig.DefaultDomainName,
                request.Domain,
                RegistryValueKind.String);
        }
        catch (Exception ex)
        {
            _logger.Error("Failed to set auto logon.");
            _logger.Error(ex.ToString());
            return Result.Failure(Errors.DeviceEnvironment.SetAutoLogon);
        }

        return Result.Success();
    }
}
