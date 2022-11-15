namespace StagingApp.Application.EnvironmentVariables.Commands.SetEnvironmentVariable;
internal sealed class SetEnvironmentVariableCommandHandler : ICommandHandler<SetEnvironmentVariableCommand>
{
    private static readonly Logger _logger= LogManager.GetCurrentClassLogger();

    public async Task<Result> Handle(SetEnvironmentVariableCommand request, CancellationToken cancellationToken)
    {
        await Task.Run(() => Environment.SetEnvironmentVariable(request.Key, request.Value, EnvironmentVariableTarget.Machine), cancellationToken));
        return Result.Success();
    }
}
