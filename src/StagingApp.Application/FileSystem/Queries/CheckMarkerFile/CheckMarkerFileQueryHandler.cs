namespace StagingApp.Application.FileSystem.Queries.CheckMarkerFile;
internal sealed class CheckMarkerFileQueryHandler : IQueryHandler<CheckMarkerFileQuery, string>
{
    public async Task<Result<string>> Handle(CheckMarkerFileQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (request.MarkerFilePath is null)
        {
            return Result.Failure<string>(Errors.FileSystem.MarkerFileIsNull);
        }

        return Path.GetFileNameWithoutExtension(request.MarkerFilePath);
    }
}
