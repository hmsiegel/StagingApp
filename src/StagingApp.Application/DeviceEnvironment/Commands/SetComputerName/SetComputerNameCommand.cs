namespace StagingApp.Application.DeviceEnvironment.Commands.SetComputerName;
public sealed record SetComputerNameCommand (string ComputerName) : ICommand;
