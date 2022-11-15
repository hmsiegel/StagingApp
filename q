namespace StagingApp.Infrastructure.Services;

[SupportedOSPlatform("Windows7.0")]
public class RegistryService : IRegistryService
{
    private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    public void EditRegistryFromValues(
        RegistryHive hiveType,
        string key,
        string value,
        string data,
        RegistryValueKind dataType)
    {
        try
        {
            RegistryKey registryKey = !Environment.Is64BitOperatingSystem 
                ? RegistryKey.OpenBaseKey(hiveType, RegistryView.Registry32) 
                : RegistryKey.OpenBaseKey(hiveType, RegistryView.Registry64);
            registryKey.CreateSubKey(key);
            registryKey.OpenSubKey(key, true)!.SetValue(value, data, dataType);
        }
        catch (Exception ex)
        {
            _logger.Error("Error editing registry.");
            _logger.Error(ex.ToString());
        }
    }

    public void SetRegistryKeyAndValue(string subKeyName, string name, string value)
    {
        RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(subKeyName);
        registryKey.SetValue(name, value);
        registryKey.Close();
    }
}
