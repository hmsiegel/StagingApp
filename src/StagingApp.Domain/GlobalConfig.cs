namespace StagingApp.Domain;
public static class GlobalConfig
{
    public const string ScriptPath = "C:\\AlohaStaging";
    public const string Ibgolden = "IBGOLDEN";
    public const string Osk = "osk";
    public const string CommandString = "/c ";
    public const string Cmd = "CMD.exe";

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
}
