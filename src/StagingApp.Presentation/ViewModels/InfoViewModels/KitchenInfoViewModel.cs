using System.ComponentModel;

namespace StagingApp.Presentation.ViewModels.InfoViewModels;
public partial class KitchenInfoViewModel : ObservableObject
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
    [Description("IP Address")]
    private string? _ipAddress;

    [ObservableProperty]
    private string? _bohIpAddress;

    private bool _isControllerNameEditVisible;
    private bool _isControllerNumberEditVisible;
    private bool _isBohServerNameEditVisible;
    private bool _isTermStrEditVisible;
    private bool _isKeyNumberEditVisible;
    private bool _isIpAddressEditVisible;
    private bool _isBohIpAddressEditVisible;

    public bool IsControllerNameEditVisible
    {
        get => _isControllerNameEditVisible;
        set 
        { 
            _isControllerNameEditVisible = value;
            OnPropertyChanged(nameof(IsControllerNameEditVisible));
        }
    }

    public bool IsControllerNumberEditVisible
    {
        get => _isControllerNumberEditVisible;
        set 
        { 
            _isControllerNumberEditVisible = value;
            OnPropertyChanged(nameof(IsControllerNumberEditVisible));
        }
    }

    public bool IsBohServerNameEditVisible
    {
        get => _isBohServerNameEditVisible;
        set 
        { 
            _isBohServerNameEditVisible = value;
            OnPropertyChanged(nameof(IsBohServerNameEditVisible));
        }
    }

    public bool IsTermStrEditVisible
    {
        get => _isTermStrEditVisible;
        set 
        { 
            _isTermStrEditVisible = value;
            OnPropertyChanged(nameof(IsTermStrEditVisible));
        }
    }

    public bool IsKeyNumberEditVisible
    {
        get => _isKeyNumberEditVisible;
        set 
        { 
            _isKeyNumberEditVisible = value;
            OnPropertyChanged(nameof(IsKeyNumberEditVisible));
        }
    }

    public bool IsIpAddressEditVisible
    {
        get => _isIpAddressEditVisible;
        set 
        { 
            _isIpAddressEditVisible = value;
            OnPropertyChanged(nameof(IsIpAddressEditVisible));
        }
    }

    public bool IsBohIpAddressEditVisible
    {
        get => _isBohIpAddressEditVisible;
        set 
        { 
            _isBohIpAddressEditVisible = value;
            OnPropertyChanged(nameof(IsBohIpAddressEditVisible));
        }
    }

    public KitchenInfoViewModel()
    {

    }

    [RelayCommand]
    private void Edit(string commandParameter)
    {
        bool isVisible = true;

        ToggleButtonVisibility(commandParameter,isVisible);

    }

    [RelayCommand]
    private void OkEdit(string commandParameter)
    {
        bool isVisible = false;

        ToggleButtonVisibility(commandParameter, isVisible);
    }

    [RelayCommand]
    private void CancelEdit(string commandParameter)
    {
        bool isVisible = false;

        ToggleButtonVisibility(commandParameter, isVisible);
    }

    private void ToggleButtonVisibility(string commandParameter, bool isVisible)
    {
        switch (commandParameter)
        {
            case "ControllerName":
                IsControllerNameEditVisible = isVisible;
                break;
            case "ControllerNumber":
                IsControllerNumberEditVisible = isVisible;
                break;
            case "BohServerName":
                IsBohServerNameEditVisible = isVisible;
                break;
            case "TermStr":
                IsTermStrEditVisible = isVisible;
                break;
            case "KeyNumber":
                IsKeyNumberEditVisible = isVisible;
                break;
            case "IpAddress":
                IsIpAddressEditVisible = isVisible;
                break;
            case "BohIpAddress":
                IsBohIpAddressEditVisible = isVisible;
                break;
            default:
                break;
        }
    }
}
