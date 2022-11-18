namespace StagingApp.Presentation.Models.ConfigureModels;
public sealed class TerminalConfigureModel : ValueObject
{
    public string TerminalName { get; private set; }
    public string IpAddress { get; private set; }

    private TerminalConfigureModel(string terminalName, string ipAddress)
    {
        TerminalName = terminalName;
        IpAddress = ipAddress;
    }

    public static TerminalConfigureModel Create(string terminalName, string ipAddress) =>
        new(terminalName, ipAddress);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return TerminalName;
        yield return IpAddress;
    }
}
