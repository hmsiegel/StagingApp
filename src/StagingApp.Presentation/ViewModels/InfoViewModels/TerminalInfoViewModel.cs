namespace StagingApp.Presentation.ViewModels.InfoViewModels;
public sealed class TerminalInfoViewModel : BaseInfoViewModel
{
    [Description("Terminal Name:")]
    public string? TerminalName { get; set; }

    [Description("Terminal Number:")]
    public string? TerminalNumber { get; set; }

    public override void Ok()
    {
        throw new NotImplementedException();
    }
}
