namespace StagingApp.Application.EnvironmentVariables.Queries.GetEnvironmentVariable;
internal sealed class GetEnvironmentVariableQueryHandler : IQueryHandler<GetEnvironmentVariableQuery, string>
{
    public async Task<Result<string>> Handle(GetEnvironmentVariableQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var output = Environment.GetEnvironmentVariable(request.Key);

        if (output is null)
        {
            return null!;
        }

        return output;
    }
}
