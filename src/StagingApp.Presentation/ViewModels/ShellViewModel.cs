namespace StagingApp.Presentation.ViewModels;
public sealed class ShellViewModel : Conductor<object>, IHandle<CloseApplicationEvent>
{
    private readonly static Logger _logger = NLog.LogManager.GetCurrentClassLogger();

    private readonly ISender? _sender;

    internal ShellViewModel(ISender? sender) : base()
    {
        _sender = sender;
    }

    public ShellViewModel()
    {
    }

    public Task HandleAsync(CloseApplicationEvent message, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            Environment.Exit(0);
        }, cancellationToken);
    }

    protected override async void OnViewLoaded(object view)
    {
        base.OnViewLoaded(view);
        var query = new DetermineDeviceQuery();
        var response = _sender!.Send(query, new CancellationToken());
        string? viewModelName = response.Result.Value;

        if (viewModelName == null)
        {
            _logger.Error("Unable to determine device type.");
            return;
        }

        switch (viewModelName)
        {
            case var name when name.Equals(DeviceType.Server.ToString()):
                await ActivateItemAsync(IoC.Get<ServerConfigureViewModel>());
                break;
            case var name when name.Equals(DeviceType.Terminal.ToString()):
                await ActivateItemAsync(IoC.Get<TerminalConfigureViewModel>());
                break;
            case var name when name.Equals(DeviceType.Kitchen.ToString()):
                await ActivateItemAsync(IoC.Get<KitchenConfigureViewModel>());
                break;
            default:
                break;
        }
    }

}
