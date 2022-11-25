namespace StagingApp.Domain;
public static class GlobalConfig
{
    private const string _scriptPath = "C:\\AlohaStaging";
    private const string _cfcSettingsPath = "Bootdrv\\CFC\\Settings";
    private const string _sysPrepPath = "C:\\Windows\\sysnative\\Sysprep";
    private const string _imageBuilderPath = "C:\\ImageBuilder";
    private const string _runKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
    private const string _runOnceKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce";
    private const string _winlogon = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon";
    private const string _sqlDataPath = "Program Files\\Microsoft SQL Server\\MSSQL11.SQLEXPRESS\\MSSQL\\DATA";
    private const string _alohaTakeoutData = "Aloha Takeout\\Database";

    public const string Ibgolden = "IBGOLDEN";
    public const string Osk = "osk";
    public const string AlohaSupportReady = "Aloha SupportReady";
    public const string AlohaAdm = "AlohaAdm";
    public const string RalExe = "AlhAdmin";
    public const string Archive = "Archive";
    public const string EdcTa = "EdcTa";
    public const string Bootdrv = "Bootdrv";

    public const string CommandString = "/c ";
    public const string ShutdownCommand = "shutdown /r /t 30 /c \"The terminal will reboot in 30 seconds\"";

    public const string Cmd = "CMD.exe";
    public const string RalShortcut = "Aloha Terminal Configuration.lnk";
    public const string TaDomain = "@ta.com";
    public const string SysPrepExe = "sysprep.exe";
    public const string Unattend = "unattend.xml";

    /// <summary>
    /// The name of the computer that the application is being run on.
    /// </summary>
    //public static string ComputerName { get; set; } = Environment.MachineName;
    public const string ComputerName = "TERMXXX";

    /// <summary>
    /// The name of the application.
    /// </summary>
    public static string? ApplicationName { get; set; } =
        Environment.ProcessPath?.Split('\\')
            .Where(x => x.Contains(FileExtensions.exe.ConvertToFileExtension())).FirstOrDefault();

    public static string? CmdExe { get; set; } =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), Cmd);

    public static string? CommonStartupFolder { get; set; } =
        Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup);

    public static string? RalPath { get; set; } =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), AlohaSupportReady, AlohaAdm);

    public static string? FromAddress { get; set; } =
        Environment.MachineName + TaDomain;

    public static string ScriptPath => _scriptPath;

    public static string SysPrepPath => _sysPrepPath;

    public static string CFCSettingsPath => _cfcSettingsPath;

    public static string ImageBuilderPath => _imageBuilderPath;

    public static string RunKey => _runKey;

    public static string RunOnceKey => _runOnceKey;

    public static string Winlogon => _winlogon;

    public static string SqlDataPath => _sqlDataPath;

    public static string AlohaTakeoutData => _alohaTakeoutData;
}
