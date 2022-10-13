namespace StagingApp.Presentation.ViewModels.Common;
public partial class DescriptionInfoRowViewModel : BaseViewModel
{
    private string? _infoText;

    public string? InfoText
    {
        get => _infoText;
        set 
        { 
            _infoText = value;
            OnPropertyChanged(nameof(InfoText));
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
