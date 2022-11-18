using System.Diagnostics;

namespace StagingApp.Application.ApplicationEnvironment.Queries.GetSoftwareVersion;
internal sealed class GetSoftwareVersionQueryHandler : IQueryHandler<GetSoftwareVersionQuery, string>
{
    public async  Task<Result<string>> Handle(GetSoftwareVersionQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (request.Path is null)
        {
            return Result.Failure<string>(Errors.ApplicationEnvironment.NullApplicationPath);
        }

        return FileVersionInfo.GetVersionInfo(request.Path).FileVersion;
    }
}
