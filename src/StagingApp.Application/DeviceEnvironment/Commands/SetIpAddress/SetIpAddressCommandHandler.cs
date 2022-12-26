namespace StagingApp.Application.DeviceEnvironment.Commands.SetIpAddress;

[SupportedOSPlatform("Windows7.0")]
internal sealed class SetIpAddressCommandHandler : ICommandHandler<SetIpAddressCommand>
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly IConfiguration _config;
    private readonly ISystemEnvironmentService _systemEnvironmentService;

    public SetIpAddressCommandHandler(
        IConfiguration config,
        ISystemEnvironmentService systemEnvironmentService)
    {
        _config = config;
        _systemEnvironmentService = systemEnvironmentService;
    }

    public async Task<Result> Handle(SetIpAddressCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var gateway = NetworkManagementService.GetGateway(request.IpAddress);

        var adapter = NetworkAdapter.Create(
            request.IpAddress,
            _config.GetValue<string>("NetworkSettings:Subnet")!,
            _config.GetValue<string>("NetworkSettings:DNS1")!,
            _config.GetValue<string>("NetworkSettings:DNS2")!,
            gateway);

        _logger.Info("Setting IP address to {ip}", request.IpAddress);
        _systemEnvironmentService.SetIPInformation(
            adapter.NicName!,
            adapter.IpAddress!,
            adapter.Subnet!,
            adapter.Gateway!,
            adapter.DNS1!,
            adapter.DNS2!,
            false);

        Thread.Sleep(30000);

        return Result.Success();
    }
}
