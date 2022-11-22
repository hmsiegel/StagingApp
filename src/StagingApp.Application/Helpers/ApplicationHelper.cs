namespace StagingApp.Application.Helpers;

[SupportedOSPlatform("Windows7.0")]
public partial class ApplicationHelper
{
    private readonly static Logger _logger = LogManager.GetCurrentClassLogger();

    public static string GetSoftwareVersion(string filePath)
    {
        return FileVersionInfo.GetVersionInfo(filePath).FileVersion!;
    }

    public static void RunProcess(string[] args, bool logToNormalLog, bool ignoreExitCode, bool runAsAdmin)
    {
        ExecuteCmdProcess(string.Join(" ", GlobalConfig.CommandString, args), logToNormalLog, ignoreExitCode, runAsAdmin);
    }

    public static void RunProcessInCurrentDirectory(string args, string currentDir, bool logToNormalLog, bool ignoreExitCode, bool runAsAdmin)
    {
        ExecuteCmdProcessInCurrentDirectory(GlobalConfig.CommandString + args, currentDir, logToNormalLog, ignoreExitCode, runAsAdmin);
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

    private static void ExecuteCmdProcess(string args, bool logToNormalLog, bool ignoreExitCode, bool runAsAdmin)
    {
        string? verb = null;
        try
        {
            LogToNormalLog(args, logToNormalLog);

            if (runAsAdmin)
            {
                verb = "runAs";
            }

            Process process = new()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = GlobalConfig.CmdExe,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    Verb = verb
                }
            };
            process.Start();
            ReturnOutputAndMessages(args, logToNormalLog, ignoreExitCode, process);

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

    private static void ReturnOutputAndMessages(string args, bool logToNormalLog, bool ignoreExitCode, Process process)
    {
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

    private static void ExecuteCmdProcessInCurrentDirectory(string args, string currentDir, bool logToNormalLog, bool ignoreExitCode, bool runAsAdmin)
    {
        string? verb = null;
        try
        {
            LogToNormalLog(
                args,
                logToNormalLog);

            if (runAsAdmin)
            {
                verb = "runAs";
            }

            Process process = new()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = GlobalConfig.CmdExe, 
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    WorkingDirectory = currentDir,
                    CreateNoWindow = true,
                    Verb = verb
                }
            };

            process.Start();
            ReturnOutputAndMessages(args, logToNormalLog, ignoreExitCode, process);

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

    private static void LogToNormalLog(string args, bool logToNormalLog)
    {
        if (logToNormalLog)
        {
            _logger.Info($"Execute CMD Process {args}...");
        }
        else
        {
            _logger.Info("Executing CMD process but logging is turned off...");
        }
    }
}

