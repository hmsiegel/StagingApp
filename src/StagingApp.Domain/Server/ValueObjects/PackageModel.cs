namespace StagingApp.Domain.Server.ValueObjects;
public sealed class PackageModel : ValueObject
{
    public SiteId Id { get; }
    public int AlohaKitchen { get; }
    public int AlohaTakeout { get; }
    public int OrderPoint { get; }
    public int Paytronix { get; }
    public int Sicom { get; }
    public int Payroll { get; }
    public int TaxReport { get; }

    private PackageModel(
        SiteId id,
        int alohaKitchen,
        int alohaTakeout,
        int orderPoint,
        int paytronix,
        int sicom,
        int payroll,
        int taxReport)
    {
        Id = id;
        AlohaKitchen = alohaKitchen;
        AlohaTakeout = alohaTakeout;
        OrderPoint = orderPoint;
        Paytronix = paytronix;
        Sicom = sicom;
        Payroll = payroll;
        TaxReport = taxReport;
    }

    public static PackageModel Create(
        SiteId id,
        int alohaKitchen,
        int alohaTakeout,
        int orderPoint,
        int paytronix,
        int sicom,
        int payroll,
        int taxReport)
    {
        return new PackageModel(
            id,
            alohaKitchen,
            alohaTakeout,
            orderPoint,
            paytronix,
            sicom,
            payroll,
            taxReport);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id!;
        yield return AlohaKitchen!;
        yield return AlohaTakeout!;
        yield return OrderPoint!;
        yield return Paytronix!;
        yield return Sicom!;
        yield return Payroll!;
        yield return TaxReport!;
    }
}
