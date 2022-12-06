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

    private const string _ibgolden = "IBGOLDEN";
    private const string _osk = "osk";
    private const string _alohaSupportReady = "Aloha SupportReady";
    private const string _alohaAdm = "AlohaAdm";
    private const string _ralExe = "AlhAdmin";
    private const string _archive = "Archive";
    private const string _edcTa = "EdcTa";
    private const string _bootdrv = "Bootdrv";

    private const string _commandString = "/c ";
    private const string _shutdownCommand = "shutdown /r /t 30 /c \"The terminal will reboot in 30 seconds\"";
    private const string _cmd = "CMD.exe";
    private const string _ralShortcut = "Aloha Terminal Configuration.lnk";
    private const string _taDomain = "@ta.com";
    private const string _sysPrepExe = "sysprep.exe";
    private const string _unattend = "unattend.xml";

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

    public static string Ibgolden => _ibgolden;

    public static string Osk => _osk;

    public static string AlohaSupportReady => _alohaSupportReady;

    public static string AlohaAdm => _alohaAdm;

    public static string RalExe => _ralExe;

    public static string Archive => _archive;

    public static string EdcTa => _edcTa;

    public static string Bootdrv => _bootdrv;

    public static string CommandString => _commandString;

    public static string ShutdownCommand => _shutdownCommand;

    public static string Cmd => _cmd;

    public static string RalShortcut => _ralShortcut;

    public static string TaDomain => _taDomain;

    public static string SysPrepExe => _sysPrepExe;

    public static string Unattend => _unattend;
}
