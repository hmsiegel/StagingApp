namespace StagingApp.Application.Abstractions.Services;
public interface IApplicationService
{
    string GetSoftwareVersion(string filePath);
    void RunProcessInCurrentDirectory(string args, string currentDir, bool logToNormalLog, bool ignoreExitCode, bool runAsAdmin);
    void EndProcess(string processName);
    void RunSysprep(string args);
    void StartOsk();
    void RunProcess(string args, bool logToNormalLog, bool ignoreExitCode, bool runAsAdmin);
}
