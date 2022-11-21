namespace StagingApp.Domain.Terminal.ValueObjects;
public sealed class TerminalModel : ValueObject
{
    public string TerminalName { get; private set; }
    public string IpAddress { get; private set; }

    private TerminalModel(string terminalName, string ipAddress)
    {
        TerminalName = terminalName;
        IpAddress = ipAddress;
    }

    public static TerminalModel Create(string terminalName, string ipAddress) =>
        new(terminalName, ipAddress);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return TerminalName;
        yield return IpAddress;
    }
}
