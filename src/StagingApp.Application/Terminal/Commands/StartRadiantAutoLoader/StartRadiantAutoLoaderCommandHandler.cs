namespace StagingApp.Application.Terminal.Commands.StartRadiantAutoLoader;
internal sealed class StartRadiantAutoLoaderCommandHandler : ICommandHandler<StartRadiantAutoLoaderCommand>
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    public async Task<Result> Handle(StartRadiantAutoLoaderCommand request, CancellationToken cancellationToken)
    {
        _logger.Info("Third marker file found. Starting third pass...");
        return default;
        
    }
}
