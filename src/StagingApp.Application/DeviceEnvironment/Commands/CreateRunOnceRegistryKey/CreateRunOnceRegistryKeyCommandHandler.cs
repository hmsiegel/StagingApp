namespace StagingApp.Application.DeviceEnvironment.Commands.CreateRunOnceRegistryKey;
public sealed class CreateRunOnceRegistryKeyCommandHandler : ICommandHandler<CreateRunOnceRegistryKeyCommand>
{
    private readonly IRegistryService _registryService;

    public CreateRunOnceRegistryKeyCommandHandler(IRegistryService registryService)
    {
        _registryService = registryService;
    }

    public async Task<Result> Handle(CreateRunOnceRegistryKeyCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _registryService.CreateRunOnceRegistryKey(request.RegistryKey.ToString());

        return Result.Success();
    }
}
