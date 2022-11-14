namespace StagingApp.Application.FileSystem.Queries.CheckMarkerFile;
internal sealed class CheckMarkerFileQueryHandler : IQueryHandler<CheckMarkerFileQuery, bool>
{
    public Task<Result<bool>> Handle(CheckMarkerFileQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
