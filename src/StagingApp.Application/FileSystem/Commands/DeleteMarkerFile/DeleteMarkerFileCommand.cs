namespace StagingApp.Application.FileSystem.Commands.DeleteMarkerFile;
public sealed record DeleteMarkerFileCommand(string MarkerFile) : ICommand;
