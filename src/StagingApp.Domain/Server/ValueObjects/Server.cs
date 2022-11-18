namespace StagingApp.Domain.Server.ValueObjects;
public sealed class Server : ValueObject
{
    public SiteId? SiteId { get; }
    public string? Concept { get; }
    public string? Timezone { get; }
    public string? Termstr { get; }
    public int Numterms { get; }
    public Guid CfcGuid { get; }
    public string? KeyNumber { get; }
    public string? ServerName { get; }
    public NetworkAdapter Adapter { get; }
    public string? AdminUser { get; }
    public string? AdminPass { get; }
    public string? Workgroup { get; }
    public string? KitchenIds { get; }
    public string? BootdrvRoot { get; }
    public string? CfcActivationCode { get; }
    public string? CfcHost { get; }
    public string? TerminalStartingId { get; }
    public string? TerminalStartingIp { get; }
    public string? KitchenIp { get; }
    public string? KitchenCount { get; }
    public string? DataDistribution { get; }
    public string? StagingTech { get; }

    private Server(
        SiteId? siteId,
        string? serverName,
        string? concept,
        string? timezone,
        string? termstr,
        int numterms,
        Guid cfcGuid,
        string? keyNumber,
        NetworkAdapter adapter,
        string? adminUser,
        string? adminPass,
        string? workgroup,
        string? kitchenIds,
        string? bootdrvRoot,
        string? cfcActivationCode,
        string? cfcHost,
        string? terminalStartingId,
        string? terminalStartingIp,
        string? kitchenIp,
        string? kitchenCount,
        string? dataDistribution,
        string? stagingTech)
    {
        SiteId = siteId;
        Concept = concept;
        Timezone = timezone;
        Termstr = termstr;
        Numterms = numterms;
        CfcGuid = cfcGuid;
        KeyNumber = keyNumber;
        ServerName = serverName;
        Concept = concept;
        Timezone = timezone;
        Termstr = termstr;
        Numterms = numterms;
        CfcGuid = cfcGuid;
        KeyNumber = keyNumber;
        Adapter = adapter;
        AdminUser = adminUser;
        AdminPass = adminPass;
        Workgroup = workgroup;
        KitchenIds = kitchenIds;
        BootdrvRoot = bootdrvRoot;
        CfcActivationCode = cfcActivationCode;
        CfcHost = cfcHost;
        TerminalStartingId = terminalStartingId;
        TerminalStartingIp = terminalStartingIp;
        KitchenIp = kitchenIp;
        KitchenCount = kitchenCount;
        DataDistribution = dataDistribution;
        StagingTech = stagingTech;
    }

    public static Server Create(
        SiteId? siteId,
        string? serverName,
        string? concept,
        string? timezone,
        string? termstr,
        int numterms,
        Guid cfcGuid,
        string? keyNumber,
        NetworkAdapter adapter,
        string? adminUser,
        string? adminPass,
        string? workgroup,
        string? kitchenIds,
        string? bootdrvRoot,
        string? cfcActivationCode,
        string? cfcHost,
        string? terminalStartingId,
        string? terminalStartingIp,
        string? kitchenIp,
        string? kitchenCount,
        string? dataDistribution,
        string? stagingTech)
    {
        return new Server(
            siteId,
            serverName,
            concept,
            timezone,
            termstr,
            numterms,
            cfcGuid,
            keyNumber,
            adapter,
            adminUser,
            adminPass,
            workgroup,
            kitchenIds,
            bootdrvRoot,
            cfcActivationCode,
            cfcHost,
            terminalStartingId,
            terminalStartingIp,
            kitchenIp,
            kitchenCount,
            dataDistribution,
            stagingTech);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return SiteId!;
        yield return ServerName!;
        yield return Concept!;
        yield return Timezone!;
        yield return Termstr!;
        yield return Numterms!;
        yield return CfcGuid!;
        yield return KeyNumber!;
        yield return Adapter!;
        yield return AdminUser!;
        yield return AdminPass!;
        yield return Workgroup!;
        yield return KitchenIds!;
        yield return BootdrvRoot!;
        yield return CfcActivationCode!;
        yield return CfcHost!;
        yield return TerminalStartingId!;
        yield return TerminalStartingIp!;
        yield return KitchenIp!;
        yield return KitchenCount!;
        yield return DataDistribution!;
        yield return StagingTech!;
    }
}
