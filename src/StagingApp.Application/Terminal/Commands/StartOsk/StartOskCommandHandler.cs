namespace StagingApp.Application.Terminal.Commands.StartOsk;
internal sealed class StartOskCommandHandler : ICommandHandler<StartOskCommand>
{
    private readonly IApplicationService _applicationService;

    public StartOskCommandHandler(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public Task<Result> Handle(StartOskCommand request, CancellationToken cancellationToken)
    {
        _applicationService.StartOsk();
        
        return Task.FromResult(Result.Success());

    }
}
