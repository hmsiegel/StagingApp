namespace StagingApp.Application.Abstractions.Services;
public interface IRegistryService
{
    void SetRegistryKeyAndValue(string subKeyName, string name, string value);
}
