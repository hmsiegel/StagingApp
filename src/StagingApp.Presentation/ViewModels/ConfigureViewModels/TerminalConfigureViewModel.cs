namespace StagingApp.Presentation.ViewModels.ConfigureViewModels;
public partial class TerminalConfigureViewModel : BaseConfigureViewModel
{
    [ObservableProperty]
    private string? _terminalName;

    [ObservableProperty]
    private string? _ipAddress;

    public override void Configure()
    {
        throw new NotImplementedException();
    }
}
