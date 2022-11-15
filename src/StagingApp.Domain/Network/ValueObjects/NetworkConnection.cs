namespace StagingApp.Domain.Network.ValueObjects;
public class NetworkConnection : ValueObject, IDisposable
{
    private readonly string? _networkName;

    public NetworkConnection(string? networkName, NetworkCredential credentials)
    {
        _networkName = networkName;

        NetResource netResource = new()
        {
            Scope = Enums.ResourceScope.GlobalNetwork,
            ResourceType = ResourceType.Disk,
            DisplayType = ResourceDisplaytype.Share,
            RemoteName = networkName!.TrimEnd('\\')
        };

        int result = WNetAddConnection2(
            netResource, credentials.Password, credentials.UserName, 0);

        if (result != 0)
        {
            throw new Win32Exception(result);
        }
    }

    public event EventHandler<EventArgs>? Disposed;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing) 
        { 
            Disposed?.Invoke(this, EventArgs.Empty);
        }

        WNetCancelConnection2(_networkName!, 0, true);
    }

    [DllImport("mpr.dll")]
    [SuppressMessage("Globalization", "CA2101: Specify marshalling for P/Inkvoke string arguments", Justification = "Connection to network fails with character set specified.")]
    private static extern int WNetAddConnection2(NetResource netResource, string password, string username, int flags);


    [DllImport("mpr.dll")]
    [SuppressMessage("Globalization", "CA2101: Specify marshalling for P/Inkvoke string arguments", Justification = "Connection to network fails with character set specified.")]
    private static extern int WNetCancelConnection2(string name, int flags, bool force);

    ~NetworkConnection()
    {
        Dispose(false);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return _networkName!;
    }
}
