namespace StagingApp.Presentation.ViewModels.ConfigureViewModels;
public partial class TerminalConfigureViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ValidateInputsCommand))]
    private string? _terminalName;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ValidateInputsCommand))]
    private string? _ipAddress;

    private bool CanValidateInputs => TerminalName is not null && IpAddress is not null;

    [RelayCommand(CanExecute = nameof(CanValidateInputs))]
    private void ValidateInputs()
    {
        throw new NotImplementedException();
    }
}
