namespace StagingApp.Presentation.ViewModels.ConfigureViewModels;
public sealed class TerminalConfigureViewModel : BaseConfigureViewModel
{
    private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();

    private readonly IValidator<TerminalConfigureModel> _validator;
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    private string? _terminalName;
    private string? _ipAddress;

    public TerminalConfigureViewModel(
        IValidator<TerminalConfigureModel> validator,
        ISender sender,
        IMapper mapper)
    {
        _validator = validator;
        _sender = sender;
        _mapper = mapper;
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

        var result = _validator.Validate(model);

        var terminalModel = _mapper.Map<TerminalModel>(model);

        if (result.IsValid)
        {
            _logger.Info("Inputs are valid. Configuring...");

            var command = new SaveTerminalInfoAndSysPrepCommand(terminalModel);
            var response = await _sender.Send(command);
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
            Directory.GetFiles(GlobalConfig.ScriptPath, "*.done", SearchOption.TopDirectoryOnly).FirstOrDefault();

        if (markerFile is null)
        {
            _logger.Info("No marker files found. Starting first run...");
            StartOsk();
        }

        CheckMarkerFileQuery query = new(markerFile!);

        Result<string> response = await _sender.Send(query, new CancellationToken());

        switch (response.Value)
        {
            case nameof(MarkerFiles.first):
                StartStageTerminal();
                break;
            case nameof(MarkerFiles.second):
                StartThirdPass();
                break;
            case nameof(MarkerFiles.third):
                StartRadiantAutoLoader();
                break;
        }
    }

    private void StartStageTerminal()
    {
        throw new NotImplementedException();
    }

    private void StartThirdPass()
    {
        throw new NotImplementedException();
    }

    private void StartRadiantAutoLoader()
    {
        throw new NotImplementedException();
    }

    private static void StartOsk()
    {
        ApplicationHelper.RunProcessInCurrentDirectory(GlobalConfig.Osk, GlobalConfig.ScriptPath);
    }
}
