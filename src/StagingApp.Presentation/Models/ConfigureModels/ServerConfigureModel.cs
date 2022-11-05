namespace StagingApp.Presentation.Models.ConfigureModels;
public sealed class ServerConfigureModel
{
    public string? SiteId { get; set; }
    public string? StagingTech { get; set; }
    public string? HardDriveLetter { get; set; }
    public bool HardDriveReplacement { get; set; }
}
