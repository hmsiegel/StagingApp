namespace StagingApp.Infrastructure.Services;

[SupportedOSPlatform("Windows7.0")]
public partial class ApplicationService : IApplicationService
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public string GetSoftwareVersion(string filePath)
    {
        return FileVersionInfo.GetVersionInfo(filePath).FileVersion!;
    }

    public void RunProcess(string args, bool logToNormalLog, bool ignoreExitCode, bool runAsAdmin)
    {
        ExecuteCmdProcess(string.Join(" ", GlobalConfig.CommandString, args), logToNormalLog, ignoreExitCode, runAsAdmin);
    }

    public void RunProcessInCurrentDirectory(string args, string currentDir, bool logToNormalLog, bool ignoreExitCode, bool runAsAdmin)
    {
        ExecuteCmdProcessInCurrentDirectory(GlobalConfig.CommandString + args, currentDir, logToNormalLog, ignoreExitCode, runAsAdmin);
    }

    public void EndProcess(string processName)
    {
        var processes = Process.GetProcessesByName(processName);
        foreach (var process in processes)
        {
            process.Kill();
            process.WaitForExit();
            process.Dispose();
        }
    }

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

    public void StartOsk()
    {
        RunProcessInCurrentDirectory(
            GlobalConfig.Osk + FileExtensions.exe.ConvertToFileExtension(),
            GlobalConfig.ScriptPath,
            true,
            true,
            true);
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

    [DllImport("Kernel32.dll", EntryPoint = "Wow64EnableWow64FsRedirection")]
    private static extern bool EnableWow64FsRedirection(bool enable);

}

