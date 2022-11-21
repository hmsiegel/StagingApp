namespace StagingApp.Application.Helpers;
public static partial class ApplicationHelper
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
        ExecuteCmdProcess(string.Join(" ", _commandString, args), logToNormalLog, ignoreExitCode);
    }

    public static void RunProcessInCurrentDirectory(object osk, string scriptPath)
    {
        throw new NotImplementedException();
    }

    public static void EndProcess(string processName)
    {
        var processes = Process.GetProcessesByName(processName);
        foreach (var process in processes)
        {
            process.Kill();
            process.WaitForExit();
            process.Dispose();
        }
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

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            int exitCode = process.ExitCode;
            process.Close();
            if (exitCode != 0 && ignoreExitCode)
            {
                throw new ProcessException(exitCode, returnMessage + returnError);
            }

            if (logToNormalLog)
            {
                _logger.Info("Process with arguments: {args} completed with exit code {exitCode}. Output: {returnMessage} {returnError}", args, exitCode, returnMessage, returnError);
            }

        }
        catch (ProcessException ex)
        {
            if (ex.ErrorCode != 3010)
            {
                if (logToNormalLog)
                {
                    _logger.Error("Process failed with exception: {ErrorCode} Message: {Message} Args: {args}", ex.ErrorCode, ex.Message, args);
                }
                else
                {
                    _logger.Error("Process failed with exception: {ErrorCode} No further logging as it is turned off for this function.", ex.ErrorCode);
                }
                throw new ProcessException(ex.ErrorCode, ex.Message);
            }
        }
        catch (Exception ex)
        {
            if (logToNormalLog)
            {
                _logger.Error("Process with arguments: {args} could not be completed. Output: {Message} Full String: {ex}", args, ex.Message, ex);
            }
            else
            {
                _logger.Error("Process could not be completed. Output: {Message} Full String: {ex}", ex.Message, ex);
            }
        }
    }

    public static void RunSysPrep()
    {
        try
        {
            if (EnableWow64FsRedirection(true) == true)
            {
                EnableWow64FsRedirection(false);
            }

            _logger.Info("Starting system sysprep...");
            Process process = new();
            process.StartInfo.FileName = @"C:\Windows\System32\sysprep\sysprep.exe";
            process.StartInfo.Arguments = @"/generalize /oobe /reboot /unattend:C:\Windows\System32\Sysprep\unattend.xml";
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

    [LibraryImport("kernel32.dll", EntryPoint = "Wow64EnableWow64FsRedirection")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool EnableWow64FsRedirection([MarshalAs(UnmanagedType.Bool)] bool enable);
}

