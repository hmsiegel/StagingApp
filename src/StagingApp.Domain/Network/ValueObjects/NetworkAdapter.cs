namespace StagingApp.Domain.Network.ValueObjects;

[SupportedOSPlatform("Windows7.0")]
public sealed class NetworkAdapter : ValueObject
{
    public string? NicName { get; }
    public string? IpAddress { get; }
    public bool IsDhcpEnabled { get; }
    public string? Subnet { get; }
    public string? DNS1 { get; }
    public string? DNS2 { get; }
    public string? Gateway { get; }

    private NetworkAdapter(
        string? ipAddress,
        string? subnet,
        string? dns1,
        string? dns2,
        string? gateway)
    {
        NicName = NetworkManagementService.GetNICName();
        IpAddress = ipAddress;
        IsDhcpEnabled = NetworkManagementService.GetDhcp();
        Subnet = subnet;
        DNS1 = dns1;
        DNS2 = dns2;
        Gateway = gateway;
    }

    public static NetworkAdapter Create(
        string ipAddress,
        string subnet,
        string dns1,
        string dns2,
        string gateway)
    {
        return new NetworkAdapter(
            ipAddress,
            subnet,
            dns1,
            dns2,
            gateway);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return NicName!;
        yield return IpAddress!;
        yield return IsDhcpEnabled!;
        yield return Subnet!;
        yield return DNS1!;
        yield return DNS2!;
        yield return Gateway!;
    }
}
