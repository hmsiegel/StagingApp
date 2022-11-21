using StagingApp.Domain.Network.ValueObjects;

namespace StagingApp.Domain.Network.Services;

[SupportedOSPlatform("Windows7.0")]
public abstract class LanaService
{
    private readonly static Logger _logger = LogManager.GetCurrentClassLogger();

    public static bool CheckForConnectedNIC()
    {
        return true;
    }

    public static NicInfoArray GetLanaCfgArray()
    {
        try
        {
            Process process = new()
            {
                StartInfo =
                    {
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Minimized,
                        RedirectStandardOutput = true,
                        FileName = "lanacfg.exe",
                        Arguments = "showlanapaths"
                    }
            };
            process.Start();
            process.WaitForExit();
            string end = process.StandardOutput.ReadToEnd();
            process.Close();
            _logger.Info(end);
            return GetLanaDetails(end);
        }
        catch (Win32Exception ex)
        {
            _logger.Error("Lanacfg.exe not detected on System");
            _logger.Error(ex);
            throw new Exception("Lanacfg.exe not detected on System");
        }
    }

    public static bool GetMaxLanaNumber(out int maxLana)
    {
        maxLana = 100;
        try
        {
            Process process = new()
            {
                StartInfo =
                    {
                        FileName = "lanacfg.exe",
                        WindowStyle = ProcessWindowStyle.Minimized,
                        UseShellExecute = false,
                        Arguments = "showlanadiag",
                        RedirectStandardError = true,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true
                    }
            };
            process.Start();
            process.WaitForExit();
            string end = process.StandardOutput.ReadToEnd();
            process.Close();
            int num = end.IndexOf("Maximum Lana:");
            if (num > 0)
            {
                string str = end[(num + "Maximum Lana:".Length)..];
                string s = str[..str.IndexOf("\n")];
                maxLana = int.Parse(s);
                ++maxLana;
                return true;
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex + "\nStack Trace: " + ex.StackTrace);
        }
        return false;
    }

    public static NicInfoArray GetNicAndLanaNumbers()
    {
        _logger.Info("In GetNicNumber");
        List<NicInfoModel> wmiNicDetails = GetWmiNicDetails();

        _logger.Info($"NIC Array count: {wmiNicDetails.Count}");
        NicInfoArray lanaCfgArray = GetLanaCfgArray();
        NicInfoArray nicsValidTemp = new();
        NicInfoArray nicInfoArray = new();

        lanaCfgArray.ValidNics = 0;

        if (wmiNicDetails.Count == 0)
        {
            return nicInfoArray;
        }

        if (lanaCfgArray?.Nics?.Length != wmiNicDetails.Count)
        {
            _logger.Info($"NIC array counts don't match WMI: {wmiNicDetails.Count} LanaCfg: {lanaCfgArray?.Nics?.Length}");
        }
        else
        {
            _logger.Info($"Lana count matches: {lanaCfgArray.Nics.Length}");
        }
        if (lanaCfgArray?.ValidNics is null)
        {
            throw new NullReferenceException();
        }
        lanaCfgArray.ValidNics = lanaCfgArray?.Nics?.Length;

        foreach (NicInfoModel nicInfo in wmiNicDetails)
        {
            _logger.Info("WMI NIC Description: {NicDescription}", nicInfo.NicDescription);
            _logger.Info("Caption: {caption}", nicInfo.Caption);
            _logger.Info("IP: {ipaddress}, DNS1: {dns1}, DNS2: {dns2}, Gateway: {gateway}", nicInfo.IpAddress, nicInfo.Dns1, nicInfo.Dns2, nicInfo.Gateway);
        }

        if (lanaCfgArray?.Nics is null)
        {
            throw new NullReferenceException();
        }
        foreach (NicInfoModel nic in lanaCfgArray.Nics)
        {
            if (nic != null!)
            {
                _logger.Info("Lana #{lananumber} NIC Description: {nicdescription}", nic.LanaNumber, nic.NicDescription);
            }
        }

        int nics = FindNics(lanaCfgArray.Nics, wmiNicDetails, nicsValidTemp);
        _logger.Info("NicsValidTotal = {nics}", nics);
        nicInfoArray.Nics = new NicInfoModel[nics];

        for (int i = 0; i < nics; ++i)
        {
            if (nicInfoArray.Nics[i] is null)
            {
                throw new NullReferenceException();
            }

            if (nicsValidTemp?.Nics?[i] is null)
            {
                throw new NullReferenceException();
            }

            nicInfoArray.Nics[i] = NicInfoModel.Create(
                nicsValidTemp.Nics[i].IpAddress,
                nicsValidTemp.Nics[i].NicDescription,
                nicsValidTemp.Nics[i].MacAddress,
                nicsValidTemp.Nics[i].LanaNumber,
                nicsValidTemp.Nics[i].NicProtocol,
                nicsValidTemp.Nics[i].Caption,
                nicsValidTemp.Nics[i].IsValid,
                nicsValidTemp.Nics[i].Dns1,
                nicsValidTemp.Nics[i].Dns2,
                nicsValidTemp.Nics[i].Gateway,
                nicsValidTemp.Nics[i].DhcpEnabled
            );
            _logger.Info("Nics {i} IP = {IpAddress}, DNS1 = {Dns1}, ", i, nicInfoArray.Nics[i].IpAddress, nicInfoArray.Nics[i].Dns1 +
                "DNS2 = {Dns2}, Gateway = {Gateway}, DHCP = {DhcpEnabled}, ", nicInfoArray.Nics[i].Dns2, nicInfoArray.Nics[i].Gateway, nicInfoArray.Nics[i].DhcpEnabled +
                "NicProtocol = {NicProtocol}, NicProtocol1 = {NicProtocol1} ", nicInfoArray.Nics[i].NicProtocol, nicInfoArray.Nics[i].NicProtocol);
        }
        if (lanaCfgArray.Nics.Length != nics)
        {
            _logger.Info("Valid lana count does not match... please research... Valid lana count is {nics}", nics);
        }
        return nicInfoArray;
    }

    private static int FindNics(NicInfoModel[] nics, ICollection<NicInfoModel> wmiNics, NicInfoArray nicsValidTemp)
    {
        nicsValidTemp.Nics = new NicInfoModel[100];
        int index = 0;
        foreach (NicInfoModel wmiNic in wmiNics)
        {
            foreach (NicInfoModel nic in nics)
            {
                if (nic != null! && !nic.NicDescription!.ToUpper().Contains("VMWARE") && !nic.NicDescription.ToUpper().Contains("VIRTUAL"))
                {
                    string firstDescription = nic.NicDescription.Trim().ToUpper();
                    _logger.Info("Checking LANA: {firstDescription}", firstDescription);

                    int length = firstDescription.Length;

                    nic.SetIsValid(false);

                    string secondDescription = wmiNic.Caption!.Trim().ToUpper();
                    if (length > secondDescription.Length)
                    {
                        length = secondDescription.Length;
                    }
                    if (secondDescription[..length] != firstDescription[..length])
                    {
                        string thirdDescription = wmiNic.NicDescription!.Trim().ToUpper();

                        if (length > thirdDescription.Length)
                        {
                            length = thirdDescription.Length;
                        }
                        if (thirdDescription[..length] != firstDescription[..length])
                        {
                            continue;
                        }
                    }

                    nic.SetIsValid(true);

                    nicsValidTemp.Nics[index] = NicInfoModel.Create(
                        wmiNic.IpAddress,
                        wmiNic.NicDescription,
                        wmiNic.MacAddress,
                        wmiNic.LanaNumber,
                        wmiNic.NicProtocol,
                        wmiNic.Caption,
                        wmiNic.IsValid,
                        wmiNic.Dns1,
                        wmiNic.Dns2,
                        wmiNic.Gateway,
                        wmiNic.DhcpEnabled
                    );
                    _logger.Info("Adding valid NIC Description: {NicDescription}", nicsValidTemp.Nics[index].NicDescription);
                    _logger.Info("Caption: {caption}", nicsValidTemp.Nics[index].Caption);
                    _logger.Info("IP: {IpAddress}, DNS1: {Dns1}, DNS2: {Dns2}, Gateway: {Gateway}, DHCPEnabled: {DhcpEnabled}", wmiNic.IpAddress, wmiNic.Dns1, wmiNic.Dns2, wmiNic.Gateway, wmiNic.DhcpEnabled);
                    ++index;
                }
            }
        }
        return index;
    }

    private static List<NicInfoModel> GetWmiNicDetails()
    {
        List<NicInfoModel> nicInfoList = new();
        try
        {
            ArrayList arrayList = new();
            if (CheckForConnectedNIC())
            {
                arrayList = WMINicConnectedCards();
            }

            foreach (ManagementBaseObject managementBaseObject in new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'").Get())
            {
                if (managementBaseObject["Description"] is not string empty9)
                {
                    empty9 = string.Empty;
                }

                string str1 = empty9;
                if (managementBaseObject["Caption"] is not string empty10)
                {
                    empty10 = string.Empty;
                }

                string str2 = empty10;
                if (!CheckForConnectedNIC() || arrayList.IndexOf(str2) != -1)
                {
                    int num = str2.IndexOf("]");
                    if (num > 0 && num + 2 < str2.Length)
                    {
                        str2 = str2[(str2.IndexOf("]") + 2)..];
                    }

                    _logger.Info("HostName = {hostname}", managementBaseObject["DNSHostName"]);
                    _logger.Info("Description = {description}", str1);
                    _logger.Info("Caption {caption}", str2);
                    foreach (string str3 in (string[])managementBaseObject["IPAddress"])
                    {
                        if (str3.StartsWith("169"))
                        {
                            _logger.Info("Ignoring NIC with IPAddress = {ipaddress}", str3);
                        }
                        else
                        {
                            NicInfoModel nicInfo = NicInfoModel.Create(
                                str3,
                                str1,
                                (string)managementBaseObject["MacAddress"],
                                str2,
                                (bool)managementBaseObject["DHCPEnabled"]
                            );
                            if (managementBaseObject["DNSServerSearchOrder"] != null)
                            {
                                string[] strArray = (string[])managementBaseObject["DNSServerSearchOrder"];
                                if (strArray.Length > 0)
                                {
                                    nicInfo.SetDns1(strArray[0]);
                                }

                                if (strArray.Length > 1)
                                {
                                    nicInfo.SetDns2(strArray[1]);
                                }
                            }
                            if (managementBaseObject["DefaultIPGateway"] != null)
                            {
                                string[] strArray = (string[])managementBaseObject["DefaultIPGateway"];
                                if (strArray.Length > 0)
                                {
                                    nicInfo.SetGateway(strArray[0]);
                                }
                            }
                            _logger.Info("Adding   NIC with:\nIPAddress = {IpAddress}\nMacAddress = {MacAddress}\nCaption = {Caption}\n", nicInfo.IpAddress, nicInfo.MacAddress, nicInfo.Caption +
                                "DHCP Enabled = {DhcpEnabled}\n, DNS1 = {Dns1}\nDNS2 = {Dns2}\nGateway = {Gateway}", nicInfo.DhcpEnabled, nicInfo.Dns1, nicInfo.Dns2, nicInfo.Gateway);
                            nicInfoList.Add(nicInfo);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.Info("Error in GetWmiNicDetails " + ex.Message);
        }
        return nicInfoList;
    }

    private static ArrayList WMINicConnectedCards()
    {
        ArrayList arrayList = new();
        foreach (ManagementObject instance in new ManagementClass("Win32_NetworkAdapter")!.GetInstances().Cast<ManagementObject>())
        {
            try
            {
                if ((ushort)instance["NetConnectionStatus"] == 2)
                {
                    arrayList.Add(instance["Caption"]);
                }
            }
            catch
            {
            }
        }
        return arrayList;
    }

    public static NicInfoArray GetLanaDetails(string lanaoutput)
    {
        ArrayList array = SplitToArray(lanaoutput, "Lana:", false);
        _logger.Info("GetLanaDetails lana conut : " + array.Count);
        NicInfoArray nics = new() { Nics = new NicInfoModel[array.Count] };

        for (int i = 0; i < array.Count; i++)
        {
            ArrayList output = SplitToArray(array[i]!.ToString()!, "-->", true);
            if (output.Count >= 4)
            {
                nics.Nics[i] = NicInfoModel.Create(
                     output[3]!.ToString(),
                     Convert.ToInt32(output[0]!.ToString()),
                     output[1]!.ToString(),
                     output[2]!.ToString()
                );
            }
            else if (output.Count == 3)
            {
                nics.Nics[i] =  NicInfoModel.Create(
                     output[2]!.ToString(),
                     Convert.ToInt32(output[0]!.ToString()),
                     output[1]!.ToString(),
                     ""
                );
            }
        }
        _logger.Info("GetLanaDetails nics count: {count}", array.Count);
        return nics;
    }

    public static ArrayList SplitToArray(string output, string toSplit, bool replaceNewLine)
    {
        ArrayList arrayList = new();
        int stringLength = output.Length;
        int startIndex = 0;
        int length = 0;
        bool flag = true;

        for (; startIndex <= stringLength && length > -1; startIndex = length + toSplit.Length)
        {
            length = output.IndexOf(toSplit, startIndex);
            if (length != -1)
            {
                int num = output.IndexOf(toSplit, length + toSplit.Length);
                string secondString;
                if (num == -1)
                {
                    secondString = output[(length + toSplit.Length)..];
                }
                else
                {
                    if (flag)
                    {
                        if (length != 0)
                        {
                            string thirdString = output[..length];
                            if (replaceNewLine)
                            {
                                thirdString = thirdString.Replace("\n", "");
                            }
                            arrayList.Add(thirdString);
                        }
                        flag = false;
                    }
                    int thirdLength = num - length - toSplit.Length;
                    secondString = output.Substring(length + toSplit.Length, thirdLength);
                }
                if (replaceNewLine)
                {
                    secondString = secondString.Replace("\n", "");
                }
                arrayList.Add(secondString);
            }
            else
            {
                break;
            }
        }
        return arrayList;
    }

    public static bool SetLanaWithNumber(int currentLana, int numberToSet)
    {
        try
        {
            Process process = new()
            {
                StartInfo =
                    {
                        FileName = "lanacfg.exe",
                        WindowStyle = ProcessWindowStyle.Minimized,
                        UseShellExecute = false,
                        Arguments = $"setlananumber {currentLana} {numberToSet}",
                        RedirectStandardOutput = true
                    }
            };
            process.Start();
            process.WaitForExit();
            process.Close();
            process.StartInfo.Arguments = "rewritelanainfo";
            process.Start();
            process.WaitForExit();
            process.Close();
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
            return false;
        }
    }
}
