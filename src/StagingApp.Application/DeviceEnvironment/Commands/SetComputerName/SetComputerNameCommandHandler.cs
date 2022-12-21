namespace StagingApp.Application.DeviceEnvironment.Commands.SetComputerName;
public sealed class SetComputerNameCommandHandler : ICommandHandler<SetComputerNameCommand>
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private readonly ISystemEnvironmentService _systemEnvironmentService;

    public SetComputerNameCommandHandler(ISystemEnvironmentService systemEnvironmentService)
    {
        _systemEnvironmentService = systemEnvironmentService;
    }

    public async Task<Result> Handle(SetComputerNameCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        _logger.Info("Setting terminal name to {terminalName}", request.ComputerName);
        _systemEnvironmentService.SetCompName(request.ComputerName);

        return Result.Success();
    }
}
