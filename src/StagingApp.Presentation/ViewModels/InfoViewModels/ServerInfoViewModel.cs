namespace StagingApp.Presentation.ViewModels.InfoViewModels;
public sealed class ServerInfoViewModel : BaseInfoViewModel
{
    [Description("Site ID:")]
    public string? SiteId { get; set; }

    public ServerInfoViewModel(
        string? message,
        string? siteId,
        string? bohServerName,
        string? ipAddress,
        string? keyNumber,
        string? numTerms,
        string? termStr)
    {
        Message = message;
        SiteId = siteId;
        BohServerName = bohServerName;
        IpAddress = ipAddress;
        KeyNumber = keyNumber;
        NumTerms = numTerms;
        TermStr = termStr;
    }

    public override void Ok()
    {
        throw new NotImplementedException();
    }
}
