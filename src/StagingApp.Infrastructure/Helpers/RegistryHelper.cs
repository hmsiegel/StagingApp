namespace StagingApp.Infrastructure.Helpers;

[SupportedOSPlatform("Windows7.0")]
public static class RegistryHelper
{
    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    public static void EditRegistryFromValues(
        RegistryHive hiveType,
        string key,
        string value,
        string data,
        RegistryValueKind dataType)
    {
        try
        {
            RegistryKey registryKey = DetermineRegistryHiveType(hiveType);
            registryKey.CreateSubKey(key);
            registryKey.OpenSubKey(key, true)!.SetValue(value, data, dataType);
        }
        catch (Exception ex)
        {
            _logger.Error("Error editing registry.");
            _logger.Error(ex.ToString());
        }
    }
    public static void SetRegistryKeyAndValue(string subKeyName, string name, string value)
    {
        RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(subKeyName);
        registryKey.SetValue(name, value);
        registryKey.Close();
    }

    public static string GetRegistryKeyAndValue(RegistryHive hiveType, string key, string value)
    {
        RegistryKey registryKey = DetermineRegistryHiveType(hiveType);
        registryKey = registryKey.OpenSubKey(key)!;
        string? output = string.Empty;
        try
        {
            output = registryKey.GetValue(value)!.ToString();
        }
        catch (Exception)
        {
            _logger.Error("Unable to get key: {key} for value: {value}.", key, value);
        }
        _logger.Info("Getting registry data for: {key} with value: {value}., key, value");
        return output!;
    }

    public static byte[] GetLocalRegistryKeyAndValue(string key, string value)
    {
        RegistryHive hive = RegistryHive.LocalMachine;
        RegistryKey registryKey = DetermineRegistryHiveType(hive);
        registryKey = registryKey.OpenSubKey(key)!;

        try
        {
            if (registryKey.GetValue(value) is not byte[] keyValue)
            {
                throw new NullReferenceException();
            }

            _logger.Info("Getting registry data for: {key} with value: {value}., key, value");
            return keyValue;
        }
        catch (Exception)
        {
            _logger.Error("Unable to get key: {key} for value: {value}.", key, value);
            throw;
        }
    }

    private static RegistryKey DetermineRegistryHiveType(RegistryHive hiveType) =>
        !Environment.Is64BitOperatingSystem
            ? RegistryKey.OpenBaseKey(hiveType, RegistryView.Registry32)
            : RegistryKey.OpenBaseKey(hiveType, RegistryView.Registry64);

}
