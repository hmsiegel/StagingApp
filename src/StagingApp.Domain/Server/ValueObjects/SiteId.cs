namespace StagingApp.Domain.Server.ValueObjects;

public class SiteId : ValueObject
{
    public string? Value { get; }

    private SiteId(string? value)
    {
        Value = value;
    }

    public static SiteId Create(string? value)
    {
        return new SiteId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value!;
    }
}