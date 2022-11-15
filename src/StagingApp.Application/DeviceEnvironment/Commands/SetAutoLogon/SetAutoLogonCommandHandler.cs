namespace StagingApp.Application.DeviceEnvironment.Commands.SetAutoLogon;

[SupportedOSPlatform("Windows7.0")]
internal sealed class SetAutoLogonCommandHandler : ICommandHandler<SetAutoLogonCommand>
{
    private const string _path = $@"Software\Microsoft\Windows NT\CurrentVersion\Winlogon";
	private const string _lsaKey = "DefaultPassword";
	private const string _autoAdminLogon = "AutoAdminLogon";
	private const string _defaultUsername = "DefaultUserName";
	private const string _defaultDomainName = "DefaultDomainName";
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

	private readonly IRegistryService _registryService;

    public SetAutoLogonCommandHandler(IRegistryService registryService)
    {
        _registryService = registryService;
    }

    public async Task<Result> Handle(SetAutoLogonCommand request, CancellationToken cancellationToken)
    {
        _logger.Info("Attempting to set auto logon.");

		try
		{
			_logger.Info("Storing the password securely.");
			LsaModel lsa = new(_lsaKey);
			lsa.SetSecret(request.Password);

			_logger.Info("Setting AutoAdminLogon to 1");
			_registryService.EditRegistryFromValues(RegistryHive.LocalMachine, _path, _autoAdminLogon, "1", RegistryValueKind.String);

			_logger.Info("Setting DefaultUserName to 1");
			_registryService.EditRegistryFromValues(RegistryHive.LocalMachine, _path, _defaultUsername, "1", RegistryValueKind.String);

			_logger.Info($"Setting Default DomainName to { request.Domain}.");
			_registryService.EditRegistryFromValues(RegistryHive.LocalMachine, _path, _defaultDomainName,request.Domain, RegistryValueKind.String);
		}
		catch (Exception ex)
		{
			_logger.Error("Failed to set auto logon.");
			_logger.Error(ex.ToString());
			return Result.Failure(Errors.DeviceEnvironment.AutoLogon);
		}

		return Result.Success();
    }
}
