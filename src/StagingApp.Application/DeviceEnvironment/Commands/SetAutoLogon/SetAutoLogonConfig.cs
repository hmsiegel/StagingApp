namespace StagingApp.Application.DeviceEnvironment.Commands.SetAutoLogon;
internal static class SetAutoLogonConfig
{
    private const string _path = $@"Software\Microsoft\Windows NT\CurrentVersion\Winlogon";
    private const string _lsaKey = "DefaultPassword";
    private const string _autoAdminLogon = "AutoAdminLogon";
    private const string _defaultUsername = "DefaultUserName";
    private const string _defaultDomainName = "DefaultDomainName";
    private const string _enableValue = "1";

    public static string EnableValue => _enableValue;

    internal static string Path => _path;

    internal static string LsaKey => _lsaKey;

    internal static string AutoAdminLogon => _autoAdminLogon;

    internal static string DefaultUsername => _defaultUsername;

    internal static string DefaultDomainName => _defaultDomainName;
}
