using StagingApp.Presentation.ViewModels.Common;

namespace StagingApp.Presentation.ViewModels.InfoViewModels;
public partial class KitchenInfoViewModel : BaseViewModel
{
    [ObservableProperty]
    private string? _controllerName;

    [ObservableProperty]
    private string? _controllerNumber;

    [ObservableProperty]
    private string? _bohServerName;

    [ObservableProperty]
    private string? _termStr;

    [ObservableProperty]
    private string? _keyNumber;

    [ObservableProperty]
    private string? _ipAddress;

    [ObservableProperty]
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
