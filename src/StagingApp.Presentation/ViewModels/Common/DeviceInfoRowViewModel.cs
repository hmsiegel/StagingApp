using StagingApp.Presentation.ViewModels.Base;

namespace StagingApp.Presentation.ViewModels.Common;
public partial class DeviceInfoRowViewModel : BaseViewModel
{
    private string? _labelText;

    public string? LabelText
    {
        get => _labelText;
        set 
        { 
            _labelText = value;
            OnPropertyChanged(nameof(LabelText));
        }
    }

    private string? _infoTextBox;

    public string? InfoTextBox
    {
        get => _infoTextBox;
        set 
        { 
            _infoTextBox = value;
            OnPropertyChanged(nameof(InfoTextBox));
        }
    }

    private bool _isEditButtonVisible;

    public bool IsEditButtonVisible
    {
        get => _isEditButtonVisible;
        set 
        {
            _isEditButtonVisible = value;
            OnPropertyChanged(nameof(IsEditButtonVisible));
        }
    }



    [RelayCommand]
    public virtual void Ok()
    {
        IsEditButtonVisible = false;
    }

    [RelayCommand]
    public virtual void Cancel()
    {
        IsEditButtonVisible = false;
    }

    [RelayCommand]
    public virtual void Edit()
    {
        IsEditButtonVisible = true;
    }
}

