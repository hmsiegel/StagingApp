namespace StagingApp.Application.FileSystem.Commands.DeleteMarkerFile;
internal sealed class DeleteMarkerFileCommandHandler : ICommandHandler<DeleteMarkerFileCommand>
{
    private static readonly Logger _logger= LogManager.GetCurrentClassLogger();

    public async Task<Result> Handle(DeleteMarkerFileCommand request, CancellationToken cancellationToken)
    {
        _logger.Info($"Attempting to delete {request.MarkerFile}.");

        if (request.MarkerFile is null)
        {
            return Result.Failure(Errors.FileSystem.CannotDeleteMarkerFile);
        }

        await Task.Run(() => File.Delete(request.MarkerFile), cancellationToken);

        return Result.Success();
    }
}
