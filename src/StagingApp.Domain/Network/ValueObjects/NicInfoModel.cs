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
        bool dhcpEnabled,
        string? nicProtocol1 = null)
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
        NicProtocol1 = nicProtocol1;
    }

    private NicInfoModel(
        string? ipAddress,
        string? nicDescription,
        string? macAddress,
        string? caption,
        bool dhcpEnabled)
    {
        IpAddress = ipAddress;
        NicDescription = nicDescription;
        MacAddress = macAddress;
        Caption = caption;
        DhcpEnabled = dhcpEnabled;
    }

    private NicInfoModel(
        string? nicDescription,
        int lanaNumber,
        string? nicProtocol,
        string? nicProtocol1 = null)
    {
        NicDescription = nicDescription;
        LanaNumber = lanaNumber;
        NicProtocol = nicProtocol;
        NicProtocol1 = nicProtocol1;
    }

    public string? IpAddress { get; private set; }
    public string? NicDescription { get; private set; }
    public string? MacAddress { get; private set; }
    public int LanaNumber { get; private set; }
    public string? NicProtocol { get; private set; }
    public string? NicProtocol1 { get; private set; } = string.Empty;
    public string? Caption { get; private set; }
    public bool IsValid { get; private set; }
    public string? Dns1 { get; private set; }
    public string? Dns2 { get; private set;  }
    public string? Gateway { get; private set; }
    public bool DhcpEnabled  { get; private set; }

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
        bool dhcpEnabled,
        string? nicProtocol1 = null)
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
            dhcpEnabled,
            nicProtocol1);
    }

    public static NicInfoModel Create(
        string? ipAddress,
        string? nicDescription,
        string? macAddress,
        string? caption,
        bool dhcpEnabled)
    {
        return new NicInfoModel(
            ipAddress,
            nicDescription,
            macAddress,
            caption,
            dhcpEnabled);
    }

    public static NicInfoModel Create(
        string? nicDescription,
        int lanaNumber,
        string? nicProtocol,
        string? nicProtocol1 = null)
    {
        return new NicInfoModel(
            nicDescription,
            lanaNumber,
            nicProtocol,
            nicProtocol1);
    }

    public void SetIsValid(bool isValid)
    {
        IsValid = isValid;
    }

    public void SetDns1(string? dns1)
    {
        Dns1 = dns1;
    }

    public void SetDns2(string? dns2)
    {
        Dns2 = dns2;
    }

    public void SetGateway(string? gateway)
    {
        Gateway = gateway;
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
