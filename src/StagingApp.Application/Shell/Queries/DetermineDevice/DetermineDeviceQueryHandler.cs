namespace StagingApp.Application.Shell.Queries.DetermineDevice;
internal sealed class DetermineDeviceQueryHandler : ICommandHandler<DetermineDeviceQuery, string>
{
    private readonly IDeviceTypeService _deviceTypeService;

    public DetermineDeviceQueryHandler(IDeviceTypeService deviceTypeService)
    {
        _deviceTypeService = deviceTypeService;
    }

    public Task<Result<string>> Handle(DetermineDeviceQuery request, CancellationToken cancellationToken)
    {
        var output = _deviceTypeService.DetermineDeviceType();

        return Task.FromResult(Result.Success(output));
    }
}
