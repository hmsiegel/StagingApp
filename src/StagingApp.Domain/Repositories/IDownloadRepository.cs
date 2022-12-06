namespace StagingApp.Domain.Repositories;
public interface IDownloadRepository
{
    public void GetSingleFile(string fileName, string filePath, string destinationPath, NetworkCredential networkCredential);
    void GetFilesFromDirectory(string filePath, string destinationPath, NetworkCredential networkCredential);
    void GetFolders(string sourcePath, string destinationPath, NetworkCredential networkCredential);
}
