using System.ComponentModel;

namespace StagingApp.Presentation.ViewModels.Common;
public partial class DeviceInfoRowViewModel : ObservableObject, INotifyPropertyChanged
{
    private string? _labelText;

    public string? LabelText
    {
        get => _labelText;
        set 
        { 
            _labelText = value;
            OnPropertyChanged();
        }
    }

    private string? _infoTextBox;

    public string? InfoTextBox
    {
        get => _infoTextBox;
        set 
        { 
            _infoTextBox = value;
            OnPropertyChanged();
        }
    }


    [RelayCommand]
    private void Ok()
    {

    }

    [RelayCommand]
    private void Cancel()
    {
        
    }

    [RelayCommand]
    private void Edit()
    {
        
    }
}

