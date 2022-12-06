namespace StagingApp.Application.Abstractions.Services;
public interface ISystemEnvironmentService
{
    void SetCompName(string compName);
    void SetIPInformation(string adapter, string ip, string subnet, string gateway, string dns1, string dns2, bool isServer);
    void SetSecureAutoLogon(string username, string password, string computerName);
    string GetDomainOu(string computerName);

}
