namespace StagingApp.Application.FileSystem.Queries.CheckMarkerFile;
public sealed record CheckMarkerFileQuery(string MarkerFilePath) : IQuery<bool>;
