namespace StagingApp.Application.Abstractions.Services;
public interface IRegistryService
{
    void SetRegistryKeyAndValue(string subKeyName, string name, string value);
    string GetRegistryKeyAndValue(RegistryHive hiveType, string key, string value);
    byte[] GetLocalRegistryKeyAndValue(string key, string value);
    void EditRegistryFromValues(RegistryHive hiveType, string key, string value, string data, RegistryValueKind dataType);
    void DeleteRunRegistryKey(string runValue);
    void CreateRunOnceRegistryKey(string runValue);
}
