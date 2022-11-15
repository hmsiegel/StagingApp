namespace StagingApp.Application.EnvironmentVariables.Queries.GetEnvironmentVariable;

public sealed record GetEnvironmentVariableQuery(string Key) : IQuery<string>;
