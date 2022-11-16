namespace StagingApp.Application.DeviceEnvironment.Commands.SetComputerName;
internal static class SetComputerNameConfig
{
    private const string _activeComputerName = @"SYSTEM\CurrentControlSet\Control\ComputerName\ActiveComputerName";
    private const string _computerName = @"SYSTEM\CurrentControlSet\Control\ComputerName\ComputerName";
    private const string _hostName = @"SYSTEM\CurrentControlSet\services\Tcpip\Parameters";
    private const string _parameters = @"SYSTEM\CurrentControlSet\services\lanmanserver\Parameters";
    private const string _computerNameString = "ComputerName";
    private const string _hostnameString = "Hostname";
    private const string _nvHostname = "NV Hostname";
    private const string _comment = "srvcomment";

    internal static string ActiveComputerName => _activeComputerName;

    internal static string ComputerName => _computerName;

    internal static string HostName => _hostName;

    internal static string Parameters => _parameters;

    internal static string ComputerNameString => _computerNameString;

    internal static string HostnameString => _hostnameString;

    internal static string NvHostname => _nvHostname;

    internal static string Comment => _comment;
}
