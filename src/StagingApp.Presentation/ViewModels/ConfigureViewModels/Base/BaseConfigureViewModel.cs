namespace StagingApp.Presentation.ViewModels.ConfigureViewModels.Base;
public abstract partial class BaseConfigureViewModel : ObservableObject
{
    [RelayCommand]
    public abstract void Configure();
}
