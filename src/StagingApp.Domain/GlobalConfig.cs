namespace StagingApp.Domain;
public static class GlobalConfig
{
    public const string ScriptPath = "C:\\AlohaStaging";
    public const string CFCSettingsPath = "Bootdrv\\CFC\\Settings";
    public const string SysPrepPath = "C:\\Windows\\sysnative\\Sysprep";
    public const string ImageBuilderPath = "C:\\ImageBuilder";
    public const string RunKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
    public const string RunOnceKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce";
    public const string Winlogon = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon";
    public const string SqlDataPath = "Program Files\\Microsoft SQL Server\\MSSQL11.SQLEXPRESS\\MSSQL\\DATA";
    public const string AlohaTakeoutData = "Aloha Takeout\\Database";

    public const string Ibgolden = "IBGOLDEN";
    public const string Osk = "osk";
    public const string AlohaSupportReady = "Aloha SupportReady";
    public const string AlohaAdm = "AlohaAdm";
    public const string RalExe = "AlhAdmin";
    public const string Archive = "Archive";
    public const string EdcTa = "EdcTa";
    public const string Bootdrv = "Bootdrv";

    public const string CommandString = "/c ";

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

}
