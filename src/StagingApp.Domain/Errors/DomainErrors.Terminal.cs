namespace StagingApp.Domain.Errors;
public static partial class DomainErrors
{
    public static class Terminal
    {
        public static Error StageTerminal => new(
            code: "Terminal.StageTerminal",
            message: "There was an error staging the terminal.");
        public static Error Empty => new(
            code: "Terminal.Empty",
            message: "The terminal name or IP address cannot be empty.");
    }
}
