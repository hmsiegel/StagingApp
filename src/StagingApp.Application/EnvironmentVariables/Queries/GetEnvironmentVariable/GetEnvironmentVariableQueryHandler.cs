namespace StagingApp.Application.EnvironmentVariables.Queries.GetEnvironmentVariable;
internal sealed class GetEnvironmentVariableQueryHandler : IQueryHandler<GetEnvironmentVariableQuery, string>
{
    public async Task<Result<string>> Handle(GetEnvironmentVariableQuery request, CancellationToken cancellationToken)
    {
        return await Task.Run(() => Environment.GetEnvironmentVariable(request.Key));
    }
}
