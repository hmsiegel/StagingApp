namespace StagingApp.Presentation.ViewModels.InfoViewModels;
public sealed class ServerInfoViewModel : BaseInfoViewModel
{
    private string? _siteId;

    [Description("SITE ID:")]
    [Sort(SortOrder = 1)]
    public string? SiteId
    {
        get => _siteId;
        set
        {
            _siteId = value;
            NotifyOfPropertyChange(() => SiteId);
        }
    }

    public ServerInfoViewModel()
    {

    }

    public override void OK()
    {
        throw new NotImplementedException();
    }
}
