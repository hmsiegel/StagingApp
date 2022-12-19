namespace StagingApp.Persistence.Helpers;
public static class FileHelper
{
    private const int _fileStreamDefaultBufferSize = 4096;

    public static async Task MoveAsync(string? sourceFilePath, string? destinationFilePath)
    {
        sourceFilePath!.AssertHasText(nameof(sourceFilePath));
        destinationFilePath!.AssertHasText(nameof(destinationFilePath));

        if (IsUncPath(sourceFilePath!)
            || HasNetworkDrive(sourceFilePath!)
            || HasNetworkDrive(destinationFilePath!)
            || IsUncPath(destinationFilePath!))
        {
            await InternalCopyToAsync(
                sourceFilePath!,
                destinationFilePath!,
                FileOptions.DeleteOnClose).ConfigureAwait(false);
            return;
        }

        FileInfo sourceFileInfo = new(sourceFilePath!);
        string sourceDrive = Path.GetPathRoot(sourceFileInfo.FullName)!;

        FileInfo destinationFileInfo = new(destinationFilePath!);
        string destinationDrive = Path.GetPathRoot(destinationFileInfo.FullName)!;

        if (sourceDrive == destinationDrive)
        {
            File.Move(sourceFilePath!, destinationFilePath!);
            return;
        }

        await Task.Run(() => File.Move(sourceFilePath!, destinationFilePath!)).ConfigureAwait(false);
    }

    private static async Task InternalCopyToAsync(
        string sourceFilePath,
        string destinationFilePath,
        FileOptions? sourceFileOptions = null,
        bool overwrite = false)
    {
        sourceFilePath.AssertHasText(nameof(sourceFilePath));
        destinationFilePath.AssertHasText(nameof(destinationFilePath));

        var sourceStreamFileOptions = (sourceFileOptions
            ?? FileOptions.SequentialScan) | FileOptions.Asynchronous;

        using FileStream sourceStream = new(
            sourceFilePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            _fileStreamDefaultBufferSize,
            sourceStreamFileOptions);
        using FileStream destinationStream = new(
            destinationFilePath,
            overwrite ? FileMode.Create : FileMode.CreateNew,
            FileAccess.Write,
            FileShare.None,
            _fileStreamDefaultBufferSize,
            true);
        await sourceStream.CopyToAsync(
            destinationStream,
            _fileStreamDefaultBufferSize).ConfigureAwait(false);
    }

    private static bool HasNetworkDrive(string path)
    {
        try
        {
            return new DriveInfo(path).DriveType == DriveType.Network;
        }
        catch 
        {
            return false;
        }
    }

    private static bool IsUncPath(string sourceFilePath)
    {
        try
        {
            return new Uri(sourceFilePath).IsUnc;
        }
        catch 
        {
            return false;
        }
    }
}
