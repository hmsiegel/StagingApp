namespace StagingApp.Domain.Network.Services;

[SupportedOSPlatform("Windows7.0")]
public static class NetworkManagementService
{
    private readonly static Logger _logger = LogManager.GetCurrentClassLogger();

    public static void SetIP(string? ipAddress, string? subnetMask, string? NIC)
    {
        ManagementObjectCollection instances = new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances();
        _logger.Info($"In SetIP. Setting to {ipAddress} Subnet: {subnetMask}");
        foreach (ManagementObject managementObject in instances)
        {
            if ((bool)managementObject["IPEnabled"]!)
            {
                if (managementObject["Caption"]!.ToString()!.Contains(NIC))
                {
                    try
                    {
                        object propertyValue = managementObject.GetPropertyValue("SettingID");
                        using (RegistryKey? registryKey = Registry.LocalMachine.OpenSubKey($"SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\\Interfaces\\{propertyValue}", true))
                        {
                            registryKey.SetValue("EnableDHCP", 0);
                            registryKey.SetValue("IPAddress", new string[1] { ipAddress }, RegistryValueKind.MultiString);
                            registryKey.SetValue("SubnetMask", new string[1] { subnetMask }, RegistryValueKind.MultiString);
                        }
                        ManagementBaseObject? methodParameters = managementObject.GetMethodParameters("EnableStatic");
                        methodParameters["IPAddress"] = new string[1] { ipAddress };
                        methodParameters["SubnetMask"] = new string[1] { subnetMask };
                        managementObject.InvokeMethod("EnableStatic", methodParameters, null);
                        _logger.Info("Invoked EnableStatic");
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"Exception: {ex}");
                        throw;
                    }
                }
            }
        }
    }

    public static void SetIP(string? ipAddress, string? subnetMask, string? NIC, string? gateway)
    {
        _logger.Info("In SetIP. Setting to {ipAddress} Subnet: {subnetMask} Gateway: {gateway}", ipAddress, subnetMask, gateway);

        NetworkInterface networkInterface = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(x => x.Name == NIC);
        IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();
        UnicastIPAddressInformation ipInfo = ipProperties.UnicastAddresses.FirstOrDefault(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork);
        string currentIP = ipInfo.Address.ToString();
        string currentSubnet = ipInfo.IPv4Mask.ToString();
        bool isDHCPEnabled = ipProperties.GetIPv4Properties().IsDhcpEnabled;

        if (!isDHCPEnabled && currentIP == ipAddress && currentSubnet == subnetMask)
        {
            return;
        }

        AppHelper.RunProcess($"netsh interface ip set address \"{NIC}\" static {ipAddress} {subnetMask} {(string.IsNullOrWhiteSpace(gateway) ? "" : $"{gateway} 1")}",
                             false,
                             true,
                             true,
                             "SetIP");

    }

    public static void SetGateway(string gateway, string NIC)
    {
        ManagementObjectCollection instances = new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances();
        _logger.Info("In SetGateway. Setting to {gateway}", gateway);
        foreach (ManagementObject managementObject in instances)
        {
            if ((bool)managementObject["IPEnabled"])
            {
                if (managementObject["Caption"]!.ToString()!.Contains(NIC))
                {
                    try
                    {
                        ManagementBaseObject methodParameters = managementObject.GetMethodParameters("SetGateways");
                        methodParameters["DefaultIPGateway"] = new string[1] { gateway };
                        methodParameters["GatewayCostMetric"] = new int[1] { 1 };
                        managementObject.InvokeMethod("SetGateways", methodParameters, null);
                        _logger.Info("Invoked SetGateways");
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"Exception: {ex}");
                        throw;
                    }
                }
            }
        }
    }

    public static void SetDNS(string NIC, string DNS1, string DNS2, bool isServer)
    {
        _logger.Info("In SetDNS. Setting DNS1 to: {DNS1} DNS2: {DNS2}", DNS1, DNS2);

        if (isServer)
        {
            ManagementObjectCollection instances = new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances();
            foreach (ManagementObject managementObject in instances.Cast<ManagementObject>())
            {
                if ((bool)managementObject["IPEnabled"])
                {
                    if (managementObject["Caption"]!.ToString()!.Contains(NIC))
                    {
                        try
                        {
                            ManagementBaseObject methodParameters = managementObject.GetMethodParameters("SetDNSServerSearchOrder");
                            methodParameters["DNSServerSearchOrder"] = new string[2] { DNS1, DNS2 };
                            managementObject.InvokeMethod("SetDNSServerSearchOrder", methodParameters, null);
                            _logger.Info("Invoked SetDNSServerSearchOrder");
                        }
                        catch (Exception ex)
                        {
                            _logger.Error($"Exception: {ex}");
                            throw;
                        }
                    }
                }
            }
        }
        else
        {
            AppHelper.RunProcess($"netsh interface ipv4 set dns name=\"{NIC}\" static {DNS1}",
                        false,
                        true,
                        true,
                        "SetDNS1");

            AppHelper.RunProcess($"netsh interface ip add dns name=\"{NIC}\" {DNS2} index=2",
                         false,
                         true,
                         true,
                         "SetDNS2");
        }



    }

    public static void SetWINS(string NIC, string priWINS, string secWINS)
    {
        ManagementObjectCollection instances = new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances();
        _logger.Info("In SetWINS. Setting to {priWINS} {secWINS}", priWINS, secWINS);
        foreach (ManagementObject managementObject in instances)
        {
            if ((bool)managementObject["IPEnabled"])
            {
                if (managementObject["Caption"].ToString().Contains(NIC))
                {
                    try
                    {
                        ManagementBaseObject methodParameters = managementObject.GetMethodParameters("SetWINSServer");
                        methodParameters.SetPropertyValue("WINSPrimaryServer", priWINS);
                        methodParameters.SetPropertyValue("WINSSecondaryServer", secWINS);
                        managementObject.InvokeMethod("SetWINSServer", methodParameters, null);
                        _logger.Info("Invoked SetWINSServer");
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"Exception: {ex}");
                        throw;
                    }
                }
            }
        }
    }

    public static void SetNetBIOS(string NIC)
    {
        ManagementObjectCollection instances = new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances();
        _logger.Info($"In SetGateway. In SetNetBIOS");
        foreach (ManagementObject managementObject in instances)
        {
            if ((bool)managementObject["IPEnabled"])
            {
                if (managementObject["Caption"].ToString().Contains(NIC))
                {
                    try
                    {
                        ManagementBaseObject methodParameters = managementObject.GetMethodParameters("SetTcpipNetbios");
                        methodParameters.SetPropertyValue("TcpipNetbiosOptions", 0);
                        managementObject.InvokeMethod("SetTcpipnetbios", methodParameters, null);
                        _logger.Info("Invoked SetTcpipNetbios");
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"Exception: {ex}");
                        throw;
                    }
                }
            }
        }
    }

    public static string GetNetConnectionID(string nic)
    {
        foreach (ManagementObject instance in new ManagementClass("Win32_NetworkAdapter").GetInstances())
        {
            if (instance["Caption"].ToString().Contains(nic))
            {
                try
                {
                    return instance["NetConnectionID"].ToString();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        return "";
    }

    public static void DisableDHCP(string NIC)
    {
        ManagementObjectCollection instances = new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances();
        _logger.Info("In DisableDHCP.");
        foreach (ManagementObject managementObject in instances)
        {
            if ((bool)managementObject["IPEnabled"])
            {
                if (managementObject["Caption"].ToString().Contains(NIC))
                {
                    try
                    {
                        ManagementBaseObject methodParameters = managementObject.GetMethodParameters("SetDNSServerSearchOrder");
                        methodParameters["DNSServerSearchOrder"] = null;
                        managementObject.InvokeMethod("DisableDHCP", null, null);
                        managementObject.InvokeMethod("SetDNSServerSearchOrder", methodParameters, null);
                        _logger.Info("Invoked SetDNSServerSearchOrder");
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"Exception: {ex}");
                        throw;
                    }
                }
            }
        }
    }

    public static string GetMacAddress()
    {
        string? macAddress = NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .Select(nic => nic.GetPhysicalAddress().ToString())
            .FirstOrDefault();
        if (macAddress == null)
        {
            return "";
        }

        return FormatMACAddress(macAddress);
    }

    public static string GetIPAddress()
    {
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new NetworkInformationException();
    }

    private static string FormatMACAddress(string? input)
    {
        StringBuilder builder = new(input);
        for (int i = builder.Length - 2; i > 0; i -= 2)
        {
            builder.Insert(i, ':');
        }
        return builder.ToString();
    }

    public static string[] GetAllLocalIPv4(NetworkInterfaceType networkType)
    {
        List<string> ipAddressList = new();
        foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (item.NetworkInterfaceType == networkType && item.OperationalStatus == OperationalStatus.Up)
            {
                foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddressList.Add(ip.Address.ToString());
                    }
                }
            }
        }
        return ipAddressList.ToArray();
    }

    public static bool PingHost(string nameOrIP, int count)
    {
        bool pingable = false;
        Ping pinger = null;

        for (int i = 0; i <= count; i++)
        {
            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrIP);
                pingable = reply.Status == IPStatus.Success;
                _logger.Info($"Reply from {reply.Address}: icmp_seq={i} status={reply.Status} time={reply.RoundtripTime}");
                Thread.Sleep(1000);
            }
            catch (PingException)
            {

            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }
        }
        return pingable;
    }

    public static string GetNetworkInterfaceDescription()
    {
        NetworkInterface nic = NetworkInterface
            .GetAllNetworkInterfaces()
            .FirstOrDefault(i => i.NetworkInterfaceType != NetworkInterfaceType.Loopback && i.NetworkInterfaceType != NetworkInterfaceType.Tunnel);

        return nic.Description;
    }

    public static string GetNICName()
    {
        NetworkInterface nic = NetworkInterface
           .GetAllNetworkInterfaces()
           .FirstOrDefault(x => x.NetworkInterfaceType != NetworkInterfaceType.Loopback && x.NetworkInterfaceType != NetworkInterfaceType.Tunnel);
        return nic.Name;
    }

    public static string GetGateway(string ipAddress)
    {
        return string.Concat(ipAddress.AsSpan(0, ipAddress.LastIndexOf(".") + 1), "1");
    }

    public static bool GetDhcp()
    {
        string adapter = GetNetworkInterfaceDescription();
        NetworkInterface networkInterface = NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(x => x.Description == adapter).First();
        return networkInterface.GetIPProperties().GetIPv4Properties().IsDhcpEnabled;
    }
}
