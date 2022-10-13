namespace StagingApp.Presentation.ViewModels.InfoViewModels;
public partial class KitchenInfoViewModel : BaseViewModel
{
    [ObservableProperty]
    [Description("Controller Name")]
    private string? _controllerName;

    [ObservableProperty]
    [Description("Controller Number")]
    private string? _controllerNumber;

    [ObservableProperty]
    [Description("BOH ServerName")]
    private string? _bohServerName;

    [ObservableProperty]
    [Description("TERMSTR")]
    private string? _termStr;

    [ObservableProperty]
    [Description("Key Number")]
    private string? _keyNumber;

    [ObservableProperty]
    [Description("IP Address")]
    private string? _ipAddress;

    [ObservableProperty]
    [Description("BOH IP Address")]
    private string? _bohIpAddress;

    public ObservableCollection<DeviceInfoRowViewModel> Rows { get; set; }
    public KitchenInfoViewModel()
    {
        Rows = new ObservableCollection<DeviceInfoRowViewModel>
        {
            new DeviceInfoRowViewModel
            {
                LabelText = "Controller Name:",

            },
            new DeviceInfoRowViewModel
            {
                LabelText = "Controller Number:"
            },
            new DeviceInfoRowViewModel
            {
                LabelText = "BOH Server Name:"
            },
            new DeviceInfoRowViewModel
            {
                LabelText = "TERMSTR:"
            },
            new DeviceInfoRowViewModel
            {
                LabelText = "Key Number:"
            },
            new DeviceInfoRowViewModel
            {
                LabelText = "IP Address:"
            },
            new DeviceInfoRowViewModel
            {
                LabelText = "BOH IP Address:"
            },
        };
    }

    [RelayCommand]
    private void Ok()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private void Cancel()
    {
        throw new NotImplementedException();
    }
}
