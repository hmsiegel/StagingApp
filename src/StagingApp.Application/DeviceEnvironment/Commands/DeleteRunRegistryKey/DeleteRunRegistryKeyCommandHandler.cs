namespace StagingApp.Application.DeviceEnvironment.Commands.DeleteRunRegistryKey;
public sealed class DeleteRunRegistryKeyCommandHandler : ICommandHandler<DeleteRunRegistryKeyCommand>
{
    private readonly IRegistryService? _registryService;

    public DeleteRunRegistryKeyCommandHandler(IRegistryService? registryService)
    {
        _registryService = registryService;
    }

    public async Task<Result> Handle(DeleteRunRegistryKeyCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        _registryService!.DeleteRunRegistryKey(request.RegistryKey.ToString());

        return Result.Success();
    }
}
