namespace StagingApp.Presentation.ViewModels.Base;
public abstract class BaseInfoViewModel : Screen
{
    public string? Message { get; set; }

    [Description("IP Address:")]
    public string? IpAddress { get; set; }

    [Description("BOH Server Name:")]
    public string? BohServerName { get; set; }

    [Description("Key Number:")]
    public string? KeyNumber { get; set; }

    [Description("NUMTERMS:")]
    public string? NumTerms { get; set; }

    [Description("TERMSTR:")]
    public string? TermStr { get; set; }

    public abstract void Ok();
    public void Cancel()
    {
        TryCloseAsync();
    }
}
