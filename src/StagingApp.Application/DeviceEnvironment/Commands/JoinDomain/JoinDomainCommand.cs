namespace StagingApp.Application.DeviceEnvironment.Commands.JoinDomain;

public sealed record JoinDomainCommand (
    string Server,
    string Domain,
    string OU,
    string Account,
    string Password) : ICommand;
