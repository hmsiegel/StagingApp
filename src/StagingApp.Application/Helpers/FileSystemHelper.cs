namespace StagingApp.Application.Helpers;
public static class FileSystemHelper
{
    public static void CreateMarkerFile(string? fileName)
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

    public static void DeleteMarkerFile(string? fileName)
    {
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }
    }

    public static string CheckForMarkerFile(string? fileName)
    {
        
        if (fileName is null)
        {
            return null!;
        }

        return Path.GetFileNameWithoutExtension(fileName);
    }
}
