namespace StagingApp.Application.Abstractions.Services;
public interface IFileSystemService
{
    void CreateMarkerFile(string? fileName);
    void DeleteMarkerFile(string? fileName);
    string CheckForMarkerFile(string? fileName);
    void RemoveFilesAndDirectories(string path, string[] extensions);
}
