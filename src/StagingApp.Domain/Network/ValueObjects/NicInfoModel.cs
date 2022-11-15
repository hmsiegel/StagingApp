namespace StagingApp.Domain.Network.ValueObjects;

[Serializable]
public sealed class NicInfoModel : ValueObject
{
    private NicInfoModel(
        string? ipAddress,
        string? nicDescription,
        string? macAddress,
        int lanaNumber,
        string? nicProtocol,
        string? caption,
        bool isValid,
        string? dns1,
        string? dns2,
        string? gateway,
        bool dhcpEnabled)
    {
        IpAddress = ipAddress;
        NicDescription = nicDescription;
        MacAddress = macAddress;
        LanaNumber = lanaNumber;
        NicProtocol = nicProtocol;
        Caption = caption;
        IsValid = isValid;
        Dns1 = dns1;
        Dns2 = dns2;
        Gateway = gateway;
        DhcpEnabled = dhcpEnabled;
    }

    public string? IpAddress { get; }
    public string? NicDescription { get; }
    public string? MacAddress { get; }
    public int LanaNumber { get; }
    public string? NicProtocol { get; }
    public string? Caption { get; }
    public bool IsValid { get; }
    public string? Dns1 { get; }
    public string? Dns2 { get; }
    public string? Gateway { get; }
    public bool DhcpEnabled  { get; }

    public static NicInfoModel Create(
        string? ipAddress,
        string? nicDescription,
        string? macAddress,
        int lanaNumber,
        string? nicProtocol,
        string? caption,
        bool isValid,
        string? dns1,
        string? dns2,
        string? gateway,
        bool dhcpEnabled)
    {
        return new NicInfoModel(
            ipAddress,
            nicDescription,
            macAddress,
            lanaNumber,
            nicProtocol,
            caption,
            isValid,
            dns1,
            dns2,
            gateway,
            dhcpEnabled);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return IpAddress!;
        yield return NicDescription!;
        yield return MacAddress!;
        yield return LanaNumber;
        yield return NicProtocol!;
        yield return Caption!;
        yield return IsValid!;
        yield return Dns1!;
        yield return Dns2!;
        yield return Gateway!;
        yield return DhcpEnabled;
    }
}
