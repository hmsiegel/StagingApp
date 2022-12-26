namespace StagingApp.Application.DeviceEnvironment.Commands.RunShutdown;
public sealed class RunShutdownCommandHandler : ICommandHandler<RunShutdownCommand>
{
    private readonly IApplicationService? _applicationService;

    public RunShutdownCommandHandler(IApplicationService? applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task<Result> Handle(RunShutdownCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _applicationService!.RunProcess(GlobalConfig.ShutdownCommand, true, true, true);
        
        return Result.Success();

    }
}
