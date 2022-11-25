namespace StagingApp.Application.Abstractions.Services;
public interface IRegistryService
{
    void EditRegistryFromValues(RegistryHive hiveType, string key, string value, string data, RegistryValueKind dataType);
    void SetRegistryKeyAndValue(string subKeyName, string name, string value);
    string GetRegistryKeyAndValue(RegistryHive hiveType, string key, string value);
    byte[] GetLocalRegistryKeyAndValue(string key, string value);
    void DeleteRunRegistryKey(string path, string runValue);
    void CreateRunOnceRegistryKey(string path, string runValue);
}
