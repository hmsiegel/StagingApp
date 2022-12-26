namespace StagingApp.Application.DeviceEnvironment.Commands.SetAutoLogon;
public sealed record SetAutoLogonCommand(string DeviceName) : ICommand;
