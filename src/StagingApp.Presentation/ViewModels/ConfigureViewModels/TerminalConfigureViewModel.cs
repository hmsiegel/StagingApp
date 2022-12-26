namespace StagingApp.Presentation.ViewModels.ConfigureViewModels;
public sealed class TerminalConfigureViewModel : BaseConfigureViewModel
{
    private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();

    private readonly IValidator<TerminalConfigureModel>? _validator;
    private readonly ISender? _sender;
    private readonly IMapper? _mapper;
    private readonly IConfiguration? _config;
    private string? _terminalName;
    private string? _ipAddress;

    internal TerminalConfigureViewModel(
        IValidator<TerminalConfigureModel>? validator,
        ISender? sender,
        IMapper? mapper,
        IConfiguration? config) : this()
    {
        _validator = validator;
        _sender = sender;
        _mapper = mapper;
        _config = config;
    }

    public TerminalConfigureViewModel()
    {

    }

    protected override void OnViewLoaded(object view)
    {
        base.OnViewLoaded(view);
        CheckForMarkerFiles();
    }

    [Description("TERMINAL NAME:")]
    public string? TerminalName
    {
        get => _terminalName;
        set
        {
            _terminalName = value;
            NotifyOfPropertyChange(() => TerminalName);
        }
    }

    [Description("IP ADDRESS:")]
    public string? IpAddress
    {
        get => _ipAddress;
        set
        {
            _ipAddress = value;
            NotifyOfPropertyChange(() => IpAddress);
        }
    }

    public async override Task<bool> ValidateInput()
    {
        _logger.Info("Validating user inputs...");

        var model
            = TerminalConfigureModel.Create(
                TerminalName!.Replace("_", "-"),
                IpAddress!);

        var result = _validator!.Validate(model);

        var terminalModel = _mapper!.Map<TerminalModel>(model);

        if (result.IsValid)
        {
            _logger.Info("Inputs are valid. Configuring...");

            var command = new SaveTerminalInfoCommand(terminalModel);
            await _sender!.Send(command);

            File.Create(Path.Combine(
                GlobalConfig.ScriptPath,
                MarkerFiles.first.ToString(),
                FileExtensions.marker.ConvertToFileExtension()));

            File.Create(Path.Combine(
                GlobalConfig.ScriptPath,
                StagingRegistryKey.TerminalStaging.ToString(),
                FileExtensions.stage.ConvertToFileExtension()));

            var sysprepargs = _config!.GetValue<string>("ApplicationSettings:SysPrepArguments")
                + ":"
                + Path.Join(GlobalConfig.SysPrepPath, GlobalConfig.Unattend);

            var sysPrepCommand = new RunSysPrepCommand(sysprepargs);
            var response = await _sender!.Send(sysPrepCommand);

            return response.IsSuccess;
        }
        else
        {
            // TODO: Implement what happens when validation is not successful.
            return false;
        }
    }

    public override void Configure()
    {
        throw new NotImplementedException();
        // TODO: Implement the configure method to start the configuration
    }
    private async void CheckForMarkerFiles()
    {
        _logger.Info("Checking for marker files...");

        var markerFile =
            Directory.GetFiles(
                GlobalConfig.ScriptPath,
                "*.done",
                SearchOption.TopDirectoryOnly).FirstOrDefault();

        if (markerFile is null)
        {
            _logger.Info("No marker files found. Starting first run...");
            await StartOsk();
        }

        switch (markerFile)
        {
            case nameof(MarkerFiles.first):
                await StartStageTerminal();
                break;
            case nameof(MarkerFiles.second):
                await StartThirdPass();
                break;
            case nameof(MarkerFiles.third):
                await StartRadiantAutoLoader();
                break;
        }
    }

    private async Task<bool> StartStageTerminal(CancellationToken cancellationToken = default)
    {
        var query = new GetTerminalConfigQuery();
        var response = _sender!.Send(query, cancellationToken);

        var model = TerminalModel.Create(
            response.Result.Value!.TerminalName,
            response.Result.Value!.IpAddress);

        var setComputerNameCommand = new SetComputerNameCommand(model.TerminalName);
        await _sender!.Send(setComputerNameCommand, cancellationToken);

        var setIpAddressCommand = new SetIpAddressCommand(model.IpAddress);
        await _sender!.Send(setIpAddressCommand, cancellationToken);

        var setAutoLogonCommand = new SetAutoLogonCommand(model.TerminalName);
        await _sender!.Send(setAutoLogonCommand, cancellationToken);

        File.Delete(
            Path.Join(
                GlobalConfig.ScriptPath,
                string.Join(
                    "",
                    MarkerFiles.first.ToString(),
                    FileExtensions.done.ConvertToFileExtension())));

        File.Create(
            Path.Join(
                GlobalConfig.ScriptPath,
                string.Join(
                    "",
                    MarkerFiles.second.ToString(),
                    FileExtensions.done.ConvertToFileExtension())));

        var deleteRunRegistryKeyCommand = new DeleteRunRegistryKeyCommand(StagingRegistryKey.TerminalStaging);
        await _sender!.Send(deleteRunRegistryKeyCommand, cancellationToken);

        var createRunOnceRegistryKeyCommand = new CreateRunOnceRegistryKeyCommand(StagingRegistryKey.TerminalStaging);
        await _sender!.Send(createRunOnceRegistryKeyCommand, cancellationToken);

        _logger.Info("First configuration is complete.");
        var runShutdownCommand = new RunShutdownCommand();
        await _sender!.Send(runShutdownCommand, cancellationToken);

        return true;
    }

    private async Task<bool> StartThirdPass(CancellationToken cancellationToken = default)
    {
        var query = new GetTerminalConfigQuery();
        var response = _sender!.Send(query, cancellationToken);

        var command = new StartThirdPassCommand(response.Result.Value!);
        var commandResponse = await _sender!.Send(command, cancellationToken);

        return commandResponse.IsSuccess;
    }

    private async Task<bool> StartRadiantAutoLoader(CancellationToken cancellationToken = default)
    {
        var command = new StartRadiantAutoLoaderCommand();
        var response = await _sender!.Send(command, cancellationToken);
        return response.IsSuccess;
    }

    private async Task<bool> StartOsk(CancellationToken cancellationToken = default)
    {
        var command = new StartOskCommand();
        var response = await _sender!.Send(command, cancellationToken)
        return response.IsSuccess;
    }
}
