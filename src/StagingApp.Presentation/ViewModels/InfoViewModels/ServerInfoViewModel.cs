namespace StagingApp.Presentation.ViewModels.InfoViewModels;
public sealed class ServerInfoViewModel : BaseInfoViewModel
{
    [Description("Site ID:")]
    public string? SiteId { get; set; }

    private bool _isSiteIdEditVisible;

    public bool IsSiteIdEditVisible
    {
        get => _isSiteIdEditVisible;
        set 
        {
            _isSiteIdEditVisible = value;
            NotifyOfPropertyChange(() => IsSiteIdEditVisible);
        }
    }

    private bool _isNumTermsEditVisible;

    public bool IsNumTermsEditVisible
    {
        get => _isNumTermsEditVisible;
        set 
        {
            _isNumTermsEditVisible = value;
            NotifyOfPropertyChange(() => IsNumTermsEditVisible);
        }
    }

    public ServerInfoViewModel()
    {

    }

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

    public override void OK()
    {
        throw new NotImplementedException();
    }


    //public void ToggleButtonVisibility(string parameter, bool isVisible)
    //{
    //    switch (parameter)
    //    {
    //        case nameof(SiteId):
    //            IsSiteIdEditVisible = isVisible;
    //            break;
    //        case nameof(BohServerName):
    //            IsBohServerNameEditVisible = isVisible;
    //            break;
    //        case nameof(KeyNumber):
    //            IsKeyNumberEditVisible = isVisible;
    //            break;
    //        case nameof(IpAddress):
    //            IsIpAddressEditVisible = isVisible;
    //            break;
    //        case nameof(TermStr):
    //            IsTermStrEditVisible = isVisible;
    //            break;
    //        case nameof(NumTerms):
    //            IsNumTermsEditVisible = isVisible;
    //            break;
    //        default:
    //            break;
    //    }
    //}
}
