namespace StagingApp.Application.ApplicationEnvironment.Queries.GetSoftwareVersion;

public record GetSoftwareVersionQuery(string Path) : IQuery<string>;
