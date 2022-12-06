namespace StagingApp.Persistence.Repositories;
public sealed class DownloadRepository : IDownloadRepository
{
    private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();


    public void GetSingleFile(string fileName, string filePath, string destinationPath, NetworkCredential networkCredential)
    {
        try
        {
            var connection = SetNetworkConnection(filePath, networkCredential);
            using (connection)
            {
                File.Copy(Path.Combine(filePath, fileName), Path.Combine(destinationPath, fileName), true);
            }
        }
        catch (FileNotFoundException ex)
        {
            _logger.Error("Error getting file: {0}", filePath);
            _logger.Error(ex.ToString());
            throw;
        }
        catch (DirectoryNotFoundException ex)
        {
            _logger.Error("Error getting file: {0}", filePath);
            _logger.Error(ex.ToString());
            throw;
        }
        catch (WebException ex)
        {
            _logger.Error("Error getting file: {0}", filePath);
            _logger.Error(ex.ToString());
            throw;
        }
        catch (Exception ex)
        {
            _logger.Error("Error getting file: {0}", filePath);
            _logger.Error(ex.ToString());
            throw;
        }
    }

    public void GetFilesFromDirectory(string filePath, string destinationPath, NetworkCredential networkCredential)
    {
        try
        {
            var connection = SetNetworkConnection(filePath, networkCredential);


            using (connection)
            {
                string[] files = Directory.GetFiles(filePath);

                foreach (var file in files)
                {
                    string destFile = Path.Combine(destinationPath, Path.GetFileName(file));
                    _logger.Info("Copying {0} to {1}...", file, destFile);
                    File.Copy(file, destFile, true);
                }

            }
        }
        catch (FileNotFoundException ex)
        {
            _logger.Error("Error getting file: {0}", filePath);
            _logger.Error(ex.ToString());
            throw;
        }
        catch (DirectoryNotFoundException ex)
        {
            _logger.Error("Error getting file: {0}", filePath);
            _logger.Error(ex.ToString());
            throw;
        }
        catch (WebException ex)
        {
            _logger.Error("Error getting file: {0}", filePath);
            _logger.Error(ex.ToString());
            throw;
        }
        catch (Exception ex)
        {
            _logger.Error("Error getting file: {0}", filePath);
            _logger.Error(ex.ToString());
            throw;
        }
    }

    public void GetFolders(string sourcePath, string destinationPath, NetworkCredential networkCredential)
    {
        throw new NotImplementedException();
    }

    private static NetworkConnection SetNetworkConnection(string path, NetworkCredential cred) =>
        new(path, cred);

}
