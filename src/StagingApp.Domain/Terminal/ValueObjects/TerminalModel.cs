namespace StagingApp.Domain.Terminal.ValueObjects;
public sealed class TerminalModel : ValueObject
{
    [Description("Terminal Name")]
    public string TerminalName { get; private set; }

    [Description("IP Address")]
    public string IpAddress { get; private set; }

    private TerminalModel(string terminalName, string ipAddress)
    {
        TerminalName = terminalName;
        IpAddress = ipAddress;
    }

    public static TerminalModel Create(string terminalName, string ipAddress) =>
        new(terminalName, ipAddress);

    public void SetTerminalName(string terminalName)
    {
        TerminalName = terminalName;
    }

    public void SetTerminalIp(string terminalIp)
    {
        IpAddress = terminalIp;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return TerminalName;
        yield return IpAddress;
    }
}
