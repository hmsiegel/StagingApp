using StagingApp.Domain.Terminal.ValueObjects;

namespace StagingApp.Application.Terminal.Commands.SaveTerminalInfoAndSysPrep;

public sealed record SaveTerminalInfoAndSysPrepCommand(TerminalModel Model) : ICommand;
