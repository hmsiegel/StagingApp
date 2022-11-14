namespace StagingApp.Application.FileSystem.Queries.CheckMarkerFile;
internal sealed class CheckMarkerFileQueryHandler : IQueryHandler<CheckMarkerFileQuery, bool>
{
    public async Task<Result<bool>> Handle(CheckMarkerFileQuery request, CancellationToken cancellationToken)
    {
        return await Task.Run(() => File.Exists(request.MarkerFilePath), cancellationToken);
    }
}
