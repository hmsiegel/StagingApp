namespace StagingApp.Presentation.ViewModels.ConfigureViewModels;
public class KitchenConfigureViewModel : BaseConfigureViewModel
{

    private string? _controllerName;
    private string? _controllerNumber;
    private string? _termStr;
    private string? _bohServerName;
    private string? _ipAddress;
    private string? _keyNumber;
    private string? _selectedConcept = string.Empty;
    private ObservableCollection<string>? _concepts;

    [Description("CONTROLLER NAME:")]
    public string? ControllerName
    {
        get => _controllerName;
        set
        {
            _controllerName = value;
            NotifyOfPropertyChange(() => ControllerName);
        }
    }

    [Description("CONTROLLER NUMBER:")]
    public string? ControllerNumber
    {
        get => _controllerNumber;
        set
        {
            _controllerNumber = value;
            NotifyOfPropertyChange(() => ControllerNumber);
        }
    }

    [Description("TERMSTR:")]
    public string? TermStr
    {
        get => _termStr;
        set
        {
            _termStr = value;
            NotifyOfPropertyChange(() => TermStr);
        }
    }

    [Description("BOH SERVER NAME:")]
    public string? BohServerName
    {
        get => _bohServerName;
        set
        {
            _bohServerName = value;
            NotifyOfPropertyChange(() => BohServerName);
        }
    }

    [Description("IP ADDRESS:")]
    public string? IpAddress
    {
        get => _ipAddress;
        set
        {
            _ipAddress = value;
            NotifyOfPropertyChange(() => IpAddress);
        }
    }

    [Description("KEY NUMBER:")]
    public string? KeyNumber
    {
        get => _keyNumber;
        set
        {
            _keyNumber = value;
            NotifyOfPropertyChange(() => KeyNumber);
        }
    }

    public string? SelectedConcept
    {
        get => _selectedConcept;
        set
        {
            _selectedConcept = value;
            NotifyOfPropertyChange(() => SelectedConcept);
        }
    }

    public ObservableCollection<string>? Concepts
    {
        get => _concepts;
        set
        {
            _concepts = value;
            NotifyOfPropertyChange(() => Concepts);
        }
    }


    public KitchenConfigureViewModel() => LoadList();

    private void LoadList()
    {
        var conceptsList = EnumServices.GetEnumDescriptions<KitchenConcepts>();
        Concepts = new ObservableCollection<string>(conceptsList);
    }

    public override void ValidateInput()
    {
        throw new NotImplementedException();
    }

    public override void Configure()
    {
        throw new NotImplementedException();
    }
}
