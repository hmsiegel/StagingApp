namespace StagingApp.Infrastructure.Services;

[SupportedOSPlatform("Windows7.0")]
public sealed partial class ApplicationService : IApplicationService
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public void RunSysprep(string args)
    {
        try
        {
            if (EnableWow64FsRedirection(true) == true)
            {
                EnableWow64FsRedirection(false);
            }

            _logger.Info("Starting system sysprep...");
            Process process = new();
            process.StartInfo.FileName = Path.Join(GlobalConfig.SysPrepPath, GlobalConfig.SysPrepExe);
            process.StartInfo.Arguments = args;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            process.Start();

            if (EnableWow64FsRedirection(false) == true)
            {
                EnableWow64FsRedirection(true);
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            throw;
        }
    }

    [LibraryImport("Kernel32.dll", EntryPoint = "Wow64EnableWow64FsRedirection")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool EnableWow64FsRedirection([MarshalAs(UnmanagedType.Bool)] bool enable);
}
