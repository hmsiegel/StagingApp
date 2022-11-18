namespace StagingApp.Domain.Kitchen.ValueObjects;
public class KitchenController : ValueObject
{
    public string? ControllerName { get; }
    public string? ControllerNumber { get; }
    public string? TermStr { get; }
    public string? IpAddress { get; }
    public string? ServerName { get; }
    public string? KeyNumber { get; }
    public string? Concept { get; }
    public bool FastFood { get; }

    private KitchenController(
        string? controllerName,
        string? controllerNumber,
        string? termStr,
        string? ipAddress,
        string? serverName,
        string? keyNumber,
        string? concept,
        bool fastFood)
    {
        ControllerName = controllerName;
        ControllerNumber = controllerNumber;
        TermStr = termStr;
        IpAddress = ipAddress;
        ServerName = serverName;
        KeyNumber = keyNumber;
        Concept = concept;
        FastFood = fastFood;
    }

    public static KitchenController Create(
        string? controllerName,
        string? controllerNumber,
        string? termStr,
        string? ipAddress,
        string? serverName,
        string? keyNumber,
        string? concept,
        bool fastFood)
    {
        return new KitchenController(
            controllerName,
            controllerNumber,
            termStr,
            ipAddress,
            serverName,
            keyNumber,
            concept,
            fastFood);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return ControllerName!;
        yield return ControllerNumber!;
        yield return TermStr!;
        yield return IpAddress!;
        yield return ServerName!;
        yield return KeyNumber!;
        yield return Concept!;
        yield return FastFood!;
    }
}
