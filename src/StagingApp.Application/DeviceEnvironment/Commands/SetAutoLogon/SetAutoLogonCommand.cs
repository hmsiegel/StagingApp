namespace StagingApp.Application.DeviceEnvironment.Commands.SetAutoLogon;

public sealed record SetAutoLogonCommand(string Username, string Password, string Domain) : ICommand;
