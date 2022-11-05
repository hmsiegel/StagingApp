namespace StagingApp.Application;
public static class GlobalConfig
{
    /// <summary>
    /// The name of the computer that the application is being run on.
    /// </summary>
    public static string? ComputerName { get; set; } = Environment.MachineName;

    /// <summary>
    /// The name of the application.
    /// </summary>
    public static string? ApplicationName { get; set; } = 
        Environment.ProcessPath?.Split('\\').Where(x => x.Contains(".exe")).FirstOrDefault();



}
