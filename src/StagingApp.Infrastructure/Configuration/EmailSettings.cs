namespace StagingApp.Infrastructure.Configuration;
public class EmailSettings
{
    public string? SmtpHost { get; set; } = string.Empty;
    public string? ToAddress { get; set; } = string.Empty;
    public string? ToDisplayName { get; set; } = string.Empty;
}
