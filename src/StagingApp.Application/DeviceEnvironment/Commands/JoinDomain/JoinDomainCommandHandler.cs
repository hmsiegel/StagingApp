namespace StagingApp.Application.DeviceEnvironment.Commands.JoinDomain;

[SupportedOSPlatform("Windows7.0")]
internal sealed partial class JoinDomainCommandHandler : ICommandHandler<JoinDomainCommand>
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly JoinOptions _joinOptions = JoinOptions.NETSETUP_JOIN_DOMAIN | JoinOptions.NETSETUP_DOMAIN_JOIN_IF_JOINED | JoinOptions.NETSETUP_ACCT_CREATE;

    public async Task<Result> Handle(JoinDomainCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _logger.Info($"Attempting to join computer to domain, {request.Domain}.");

        int output = NetJoinDomain(
    request.Server,
    request.Domain,
    request.OU,
    request.Account,
    request.Password,
    _joinOptions);

        _logger.Info("JoinDomain completed with exit code: {output}.", output);

        if (output == 0)
        {
            return Result.Success();
        }
        else
        {
            return Result.Failure(Errors.DeviceEnvironment.JoinDomain);
        }
    }

    [LibraryImport("netapi32.dll", StringMarshalling = StringMarshalling.Utf16)]
    private static partial int NetJoinDomain(
        string lpServer,
        string lpDomain,
        string lpAccountOU,
        string lpAccount,
        string lpPassword,
        JoinOptions NameType);
}
