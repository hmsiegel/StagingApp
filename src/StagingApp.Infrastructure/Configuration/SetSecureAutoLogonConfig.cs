namespace StagingApp.Infrastructure.Configuration;
internal static class SetSecureAutoLogonConfig
{
    private const string _autologon = "AutoAdminLogon";
    private const string _defaultUserName = "DefaultUserName";
    private const string _defaultDomainName = "DefaultDomainName";
    private const string _enableValue = "1";

    internal static string Autologon => _autologon;
    internal static string DefaultUserName => _defaultUserName;
    internal static string DefaultDomainName => _defaultDomainName;
    internal static string EnableValue => _enableValue;
}
