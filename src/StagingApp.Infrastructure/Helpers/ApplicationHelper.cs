namespace StagingApp.Infrastructure.Helpers;
public static class ApplicationHelper
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private const string _commandString = "/c ";
    private const string _cmd = "CMD.exe";
    private static readonly string _cmdExe = Path.Combine(Environment.GetEnvironmentVariable("SYSTEM32")!, _cmd);

    public static string GetSoftwareVersion(string filePath)
    {
        return FileVersionInfo.GetVersionInfo(filePath).FileVersion!;
    }

    public static void RunProcess(string[] args, bool logToNormalLog, bool ignoreExitCode)
    {
        ExecuteCmdProcess(String.Join(" ", _commandString, args), logToNormalLog, ignoreExitCode);
    }

    private static void ExecuteCmdProcess(string args, bool logToNormalLog, bool ignoreExitCode)
    {
        try
        {
            if (logToNormalLog)
            {
                _logger.Info("Executing CMD process: {args}", args);
            }

            Process process = new()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = _cmdExe,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                }
            };
            process.Start();
            string returnMessage = string.Empty;
            process.OutputDataReceived += (sender, line) =>
            {
                if (line.Data is null) { return; }

                returnMessage += line.Data;
            };

            string returnError = string.Empty;
            process.ErrorDataReceived += (sender, line) =>
            {
                if (line.Data is null) { return; }

                returnError += line.Data;
            };
        }
        catch ()
        {

            throw;
        }
    }
}
