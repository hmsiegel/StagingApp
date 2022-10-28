namespace StagingApp.Presentation.ViewModels.InfoViewModels;
public sealed class TerminalInfoViewModel : BaseInfoViewModel
{
    [Description("Terminal Name:")]
    public string? TerminalName { get; set; }

    [Description("Terminal Number:")]
    public string? TerminalNumber { get; set; }

    [Description("Gateay:")]
    public string? Gateway { get; set; }


    private bool _isTerminalNameEditVisible;

    public bool IsTemrinalNameEditVisible
    {
        get { return _isTerminalNameEditVisible; }
        set
        {
            _isTerminalNameEditVisible = value;
            NotifyOfPropertyChange(() => IsTemrinalNameEditVisible);
        }
    }

    private bool _isTerminalNumberEditVisible;

    public bool IsTerminalNumberEditVisible
    {
        get { return _isTerminalNumberEditVisible; }
        set
        {
            _isTerminalNumberEditVisible = value;
            NotifyOfPropertyChange(() => IsTerminalNumberEditVisible);
        }
    }

    private bool _isGatewayEditVisible;

    public bool IsGatewayEditVisible
    {
        get { return _isGatewayEditVisible; }
        set { _isGatewayEditVisible = value; }
    }

    public TerminalInfoViewModel()
    {

    }

    public override void OK()
    {
        throw new NotImplementedException();
    }

    public override void ToggleButtonVisibility(string parameter, bool isVisible)
    {
        switch (parameter)
        {
            case nameof(TerminalName):
                IsTemrinalNameEditVisible = isVisible;
                break;
            case nameof(TerminalNumber):
                IsTerminalNumberEditVisible = isVisible;
                break;
            case nameof(IpAddress):
                IsIpAddressEditVisible = isVisible;
                break;
            case nameof(Gateway):
                IsGatewayEditVisible = isVisible;
                break;
            default:
                break;
        }
    }
}
