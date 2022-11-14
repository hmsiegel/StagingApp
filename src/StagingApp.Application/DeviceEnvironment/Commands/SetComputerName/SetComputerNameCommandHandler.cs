namespace StagingApp.Application.DeviceEnvironment.Commands.SetComputerName;

internal sealed partial class SetComputerNameCommandHandler : ICommandHandler<SetComputerNameCommand>
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private const string _activeComputerName = @"SYSTEM\CurrentControlSet\Control\ComputerName\ActiveComputerName";
    private const string _computerName = @"SYSTEM\CurrentControlSet\Control\ComputerName\ComputerName";
    private const string _hostName = @"SYSTEM\CurrentControlSet\services\Tcpip\Parameters";
    private const string _parameters = @"SYSTEM\CurrentControlSet\services\lanmanserver\Parameters";
    private const string _computerNameString = "ComputerName";
    private const string _hostnameString = "Hostname";
    private const string _nvHostname = "NV Hostname";
    private const string _comment = "srvcomment";

    private readonly IRegistryService _registryService;

    public SetComputerNameCommandHandler(IRegistryService registryService)
    {
        _registryService = registryService;
    }

    public async Task<Result> Handle(SetComputerNameCommand request, CancellationToken cancellationToken)
    {
        _logger.Info($"Attempting to set computer name to: {request.ComputerName}.");

        if (SetComputerName(request.ComputerName))
        {
            _logger.Info("Computer name was set successfully.");

            _registryService.SetRegistryKeyAndValue(_activeComputerName, _computerNameString, request.ComputerName);
            _registryService.SetRegistryKeyAndValue(_computerName, _computerNameString, request.ComputerName);
            _registryService.SetRegistryKeyAndValue(_hostName, _hostnameString, request.ComputerName);
            _registryService.SetRegistryKeyAndValue(_hostName, _nvHostname, request.ComputerName);
            _registryService.SetRegistryKeyAndValue(_parameters, _comment, request.ComputerName);
        }
        else
        {
            _logger.Error("Computer name change failed.");
            return Result.Failure(Errors.DeviceEnvironment.ChangeComputerName);
        }

        return Result.Success();
    }

    [DllImport("kernel32.dll")]
    [SuppressMessage("Globalization", "CA2101:Specify marshaling for P/Invoke string arguments", Justification = "Specifying marshalling causes method to fail.")]
    private static extern bool SetComputerName(string lpComputerName);
}
