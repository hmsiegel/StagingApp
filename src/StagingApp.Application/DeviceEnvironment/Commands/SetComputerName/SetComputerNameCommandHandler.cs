namespace StagingApp.Application.DeviceEnvironment.Commands.SetComputerName;

internal sealed partial class SetComputerNameCommandHandler : ICommandHandler<SetComputerNameCommand>
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly IRegistryService _registryService;

    public SetComputerNameCommandHandler(IRegistryService registryService)
    {
        _registryService = registryService;
    }

    public async Task<Result> Handle(SetComputerNameCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        _logger.Info($"Attempting to set computer name to: {request.ComputerName}.");

        if (SetComputerName(request.ComputerName))
        {
            _logger.Info("Computer name was set successfully.");

            _registryService.SetRegistryKeyAndValue(
                SetComputerNameConfig.ActiveComputerName,
                SetComputerNameConfig.ComputerNameString,
                request.ComputerName);

            _registryService.SetRegistryKeyAndValue(
                SetComputerNameConfig.ComputerName,
                SetComputerNameConfig.ComputerNameString,
                request.ComputerName);
            
            _registryService.SetRegistryKeyAndValue(
                SetComputerNameConfig.HostName,
                SetComputerNameConfig.HostnameString,
                request.ComputerName);
            
            _registryService.SetRegistryKeyAndValue(
                SetComputerNameConfig.HostName,
                SetComputerNameConfig.NvHostname,
                request.ComputerName);

            _registryService.SetRegistryKeyAndValue(
                SetComputerNameConfig.Parameters,
                SetComputerNameConfig.Comment,
                request.ComputerName);
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
