namespace StagingApp.Application.Terminal.Commands.StartThirdPass;
internal sealed class StartThirdPassCommandHandler : ICommandHandler<StartThirdPassCommand>
{
    private readonly ISystemEnvironmentService _systemEnvironmentService;
    private readonly IDownloadRepository _downloadRepository;

    public StartThirdPassCommandHandler(
        ISystemEnvironmentService systemEnvironmentService,
        IDownloadRepository downloadRepository)
    {
        _systemEnvironmentService = systemEnvironmentService;
        _downloadRepository = downloadRepository;
    }

    public Task<Result> Handle(StartThirdPassCommand request, CancellationToken cancellationToken)
    {
        var ou = _systemEnvironmentService.GetDomainOu(GlobalConfig.ComputerName);

        bool fileDownloaded = false;

        while (fileDownloaded == false)
        {
            try
            {

            }
            catch 
            {
            }
        }
    }

}
