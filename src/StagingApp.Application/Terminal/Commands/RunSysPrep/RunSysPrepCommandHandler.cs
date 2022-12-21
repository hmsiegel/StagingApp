namespace StagingApp.Application.Terminal.Commands.RunSysPrep;
internal sealed class RunSysPrepCommandHandler : ICommandHandler<RunSysPrepCommand>
{
    private readonly IApplicationService _appService;

    public RunSysPrepCommandHandler(IApplicationService appService)
    {
        _appService = appService;
    }

    public Task<Result> Handle(RunSysPrepCommand request, CancellationToken cancellationToken)
    {
        _appService.RunSysprep(request.args);

        return Task.FromResult(Result.Success());
    }
}
