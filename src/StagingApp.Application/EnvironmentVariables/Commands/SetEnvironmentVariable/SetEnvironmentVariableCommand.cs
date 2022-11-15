namespace StagingApp.Application.EnvironmentVariables.Commands.SetEnvironmentVariable;

public sealed record SetEnvironmentVariableCommand(string Key, string Value) : ICommand;
