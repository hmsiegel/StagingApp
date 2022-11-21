﻿namespace StagingApp.Domain;
public static class GlobalConfig
{
    public const string ScriptPath = "C:\\AlohaStaging";
    public const string StageExtension = "*.stage";
    public const string Ibgolden = "IBGOLDEN";
    public const string Osk = "osk";

    /// <summary>
    /// The name of the computer that the application is being run on.
    /// </summary>
    //public static string ComputerName { get; set; } = Environment.MachineName;
    public const string ComputerName = "AKXXXSTAGE";

    /// <summary>
    /// The name of the application.
    /// </summary>
    public static string? ApplicationName { get; set; } =
        Environment.ProcessPath?.Split('\\').Where(x => x.Contains(".exe")).FirstOrDefault();
}
