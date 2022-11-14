namespace StagingApp.Application.DeviceEnvironment.Commands.SetComputerName;
internal sealed class SetComputerNameCommandHandler : ICommandHandler<SetComputerNameCommand>
{
    public Task<Result> Handle(SetComputerNameCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
