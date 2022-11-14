namespace StagingApp.Infrastructure.Services;

[SupportedOSPlatform("Windows7.0")]
public class RegistryService : IRegistryService
{
    public void SetRegistryKeyAndValue(string subKeyName, string name, string value)
    {
        RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(subKeyName);
        registryKey.SetValue(name, value);
        registryKey.Close();
    }
}
