namespace StagingApp.Presentation.ViewModels.Base;
public abstract class BaseConfigureViewModel : Screen
{
    public abstract void Configure();
    public abstract Task<bool> ValidateInput();
}
