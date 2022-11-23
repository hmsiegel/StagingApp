namespace StagingApp.Infrastructure.Services;
public class FileSystemService : IFileSystemService
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    public void CreateMarkerFile(string? fileName)
    {
        {
            if (File.Exists(fileName))
            {
                return;
            }

            if (fileName is null)
            {
                return;
            }

            File.Create(fileName);
        }
    }

    public void DeleteMarkerFile(string? fileName)
    {
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }
    }

    public string CheckForMarkerFile(string? fileName)
    {

        if (fileName is null)
        {
            return null!;
        }

        return Path.GetFileNameWithoutExtension(fileName);
    }

    public void RemoveFilesAndDirectories(string path, string[] extensions)
    {
        _logger.Info("Removing all files from directory: {path}", path);

        List<string> files = new(FilterFiles(path, extensions));

        files.ForEach(x =>
        {
            try
            {
                _logger.Info("Deleting {file}", x);
                File.Delete(x);
            }
            catch
            {
            }
        });

        _logger.Info("Removing directories from {path}", path);
        List<string> directories = new(Directory.GetDirectories(path));

        directories.ForEach(x =>
        {
            try
            {
                _logger.Info("Deleting {file}", x);
                Directory.Delete(x);
            }
            catch
            {
            }
        });

    }

    private static IEnumerable<string> FilterFiles(string path, params string[] extensions) =>
        Directory
            .GetFiles(path, "*", SearchOption.AllDirectories)
            .Where(file => !extensions.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase)));
}
