namespace StagingApp.Domain.Errors;
public static partial class DomainErrors
{
    public static class Terminal
    {
        public static Error StageTerminal => new(
            code: "Terminal.StageTerminal",
            message: "There was an error staging the terminal.");
    }
}
