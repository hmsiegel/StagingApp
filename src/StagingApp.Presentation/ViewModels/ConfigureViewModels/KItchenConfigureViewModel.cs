namespace StagingApp.Presentation.ViewModels.ConfigureViewModels;
public partial class KitchenConfigureViewModel : BaseViewModel
{
    [ObservableProperty]
    [Description("CONTROLLER NAME:")]
    [NotifyCanExecuteChangedFor(nameof(ValidateInputCommand))]
    private string? _controllerName;

    [ObservableProperty]
    [Description("CONTROLLER NUMBER:")]
    [NotifyCanExecuteChangedFor(nameof(ValidateInputCommand))]
    private string? _controllerNumber;

    [ObservableProperty]
    [Description("TERMSTR:")]
    [NotifyCanExecuteChangedFor(nameof(ValidateInputCommand))]
    private string? _termStr;

    [ObservableProperty]
    [Description("BOH SERVER NAME:")]
    [NotifyCanExecuteChangedFor(nameof(ValidateInputCommand))]
    private string? _bohServerName;

    [ObservableProperty]
    [Description("IP ADDRESS:")]
    [NotifyCanExecuteChangedFor(nameof(ValidateInputCommand))]
    private string? _ipAddress;

    [ObservableProperty]
    [Description("KEY NUMBER:")]
    [NotifyCanExecuteChangedFor(nameof(ValidateInputCommand))]
    private string? _keyNumber;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ValidateInputCommand))]
    private string? _selectedConcept;

    [ObservableProperty]
    [Description("CONCEPT:")]
    private ObservableCollection<string>? _kitchenConceptsList;

    public ObservableCollection<DeviceConfigureTextRowViewModel> Rows { get; set; }

    private bool CanValidateInput
    {
        get
        {
            bool output = false;
            if (ControllerName?.Length > 0 && ControllerNumber?.Length > 0 &&
                BohServerName?.Length > 0 && IpAddress?.Length > 0 &&
                KeyNumber?.Length > 0 && TermStr?.Length > 0 && SelectedConcept?.Length > 0)
            {
                output = true;
            }

            return output;
        }
    }

    public KitchenConfigureViewModel()
    {
        LoadList();

        Rows = new ObservableCollection<DeviceConfigureTextRowViewModel>
        {
            new DeviceConfigureTextRowViewModel
            {
                ConfigureLabelText = "CONTROLLER NAME:"
            },
            new DeviceConfigureTextRowViewModel
            {
                ConfigureLabelText = "CONTROLLER NUMBER:"
            },
            new DeviceConfigureTextRowViewModel
            {
                ConfigureLabelText = "TERMSTR:"
            },
            new DeviceConfigureTextRowViewModel
            {
                ConfigureLabelText = "BOH SERVER NAME:"
            },
            new DeviceConfigureTextRowViewModel
            {
                ConfigureLabelText = "IP ADDRESS:"
            },
            new DeviceConfigureTextRowViewModel
            {
                ConfigureLabelText = "KEY NUMBER:"
            }
        };
    }

    private void LoadList()
    {
        var conceptsList = EnumServices.GetEnumDescriptions<KitchenConcepts>();
        KitchenConceptsList = new ObservableCollection<string>(conceptsList);
    }

    [RelayCommand(CanExecute = nameof(CanValidateInput))]
    private void ValidateInput()
    {
        throw new NotImplementedException();
    }
}
