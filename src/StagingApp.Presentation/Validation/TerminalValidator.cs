namespace StagingApp.Presentation.Validation;
public class TerminalValidator : AbstractValidator<TerminalConfigureModel>
{
    public TerminalValidator()
    {
        RuleFor(x => x.TerminalName)
            .NotNull()
            .Matches("((TABLE)|(QUICK)|(BAR))\\d{2,4}_\\d{1,2}:")
            .WithMessage("The terminal name is in the correct format. Please correct the issue and try again.");

        RuleFor(x => x.IpAddress)
            .NotNull()
            .Matches("((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)")
            .WithMessage("The IP Address is not in the correct format. Please try again.");
    }
}
