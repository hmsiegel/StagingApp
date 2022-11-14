namespace StagingApp.Application.FileSystem.Commands.CreateMarkerFile;
internal sealed class CreateMarkerFileCommandHandler : ICommandHandler<CreateMarkerFileCommand>
{
    public async Task<Result> Handle(CreateMarkerFileCommand request, CancellationToken cancellationToken)
    {
        if (File.Exists(request.FileName))
        {
            return Result.Failure(Errors.FileSystem.MarkerFileAlreadyExists);
        }

        if (request.FileName is null)
        {
            return Result.Failure(Errors.FileSystem.CannotCreateMarkerFile);
        }

        await Task.Run(() => File.Create(request.FileName), cancellationToken);

        return Result.Success();
    }
}
