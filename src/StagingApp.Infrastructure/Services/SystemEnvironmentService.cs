using System.Diagnostics.CodeAnalysis;

using StagingApp.Domain.Models;
using StagingApp.Domain.Network.Services;
using StagingApp.Domain.Services;
using StagingApp.Infrastructure.Configuration;

namespace StagingApp.Infrastructure.Services;

[SupportedOSPlatform("Windows7.0")]
public sealed class SystemEnvironmentService : ISystemEnvironmentService
{
    private const string lasKey = "Default Password";
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private readonly IRegistryService _registryService;
    private readonly IConfiguration _config;

    public SystemEnvironmentService(IRegistryService registryService, IConfiguration config)
    {
        _registryService = registryService;
        _config = config;
    }

    public void SetCompName(string compName)
    {
        try
        {
            _logger.Info("Setting computer name to {computerName}", compName);

            if (SetComputerName(compName))
            {
                _logger.Info("Computer name was set properly");
            }
            else
            {
                _logger.Error("Computer name change failed");
                return;
            }

            _registryService.SetRegistryKeyAndValue(
                SetComputerNameConfig.ActiveComputerName,
                SetComputerNameConfig.ComputerNameString,
                compName);

            _registryService.SetRegistryKeyAndValue(
                SetComputerNameConfig.ComputerName,
                SetComputerNameConfig.ComputerNameString,
                compName);
            _registryService.SetRegistryKeyAndValue(
                SetComputerNameConfig.HostName,
                SetComputerNameConfig.HostnameString,
                compName);
            _registryService.SetRegistryKeyAndValue(
                SetComputerNameConfig.HostName,
                SetComputerNameConfig.NvHostname,
                compName);
            _registryService.SetRegistryKeyAndValue(
                SetComputerNameConfig.Parameters,
                SetComputerNameConfig.Comment,
                compName);

        }
        catch (Exception)
        {

            throw;
        }
    }

    public void SetIPInformation(string adapter, string ip, string subnet, string gateway, string dns1, string dns2, bool isServer)
    {
        NetworkManagementService.SetIP(ip, subnet, adapter);
        NetworkManagementService.SetDNS(adapter, dns1, dns2, isServer);
        // TODO: Implement Image Logger

        if (isServer)
        {
            NetworkManagementService.SetGateway(gateway, adapter);
        }

        _logger.Info("IP information set to IP: {ip} Subnet: {subnet} Gateway: {gateway} DNS1: {dns1} DNS2: {dns2}", ip, subnet, gateway, dns1, dns2);
    }

    public void SetSecureAutoLogon(string username, string password, string computerName)
    {
        try
        {
            string? decryptedPassword =
                SecureStringService.DecryptString(password, _config.GetValue<string>("PasswordSettings:PasswordSalt")!);

            _logger.Info("Storing the password securely.");
            LsaModel lsa = new(lasKey);
            lsa.SetSecret(decryptedPassword);

            _logger.Info("Setting registry entries.");
            _logger.Info("Setting AutoAdminLogon to {value}", SetSecureAutoLogonConfig.EnableValue);
            _registryService.EditRegistryFromValues(
                RegistryHive.LocalMachine,
                GlobalConfig.Winlogon,
                SetSecureAutoLogonConfig.Autologon,
                SetSecureAutoLogonConfig.EnableValue,
                RegistryValueKind.String);
                
            _logger.Info("Setting DefaultUserName to {username}", username);
            _registryService.EditRegistryFromValues(
                RegistryHive.LocalMachine,
                GlobalConfig.Winlogon,
                SetSecureAutoLogonConfig.DefaultUserName,
                username,
                RegistryValueKind.String);
                
            _logger.Info("Setting DefaultDomainName to {domain}", computerName);
            _registryService.EditRegistryFromValues(
                RegistryHive.LocalMachine,
                GlobalConfig.Winlogon,
                SetSecureAutoLogonConfig.DefaultDomainName,
                computerName,
                RegistryValueKind.String);
        }
        catch (Exception ex)
        {
            _logger.Error("Failed to set auto logon");
            _logger.Error(ex.ToString());
            throw;
        }
    }

    [DllImport("kernel32.dll")]
    [SuppressMessage("Globalization", "CA2101:Specify marshalling for P/Invoke string arguments", Justification = "Method fails when specifying marshalling.")]
    private static extern bool SetComputerName(string lpComputerName);
}
