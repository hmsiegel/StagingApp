namespace StagingApp.Application.Terminal.Queries.GetTerminalConfig;
internal sealed class GetTerminalConfigQueryHandler : IQueryHandler<GetTerminalConfigQuery, TerminalModel>
{
    private readonly ICsvFileRepository _csvFileRepository;

    public GetTerminalConfigQueryHandler(ICsvFileRepository csvFileRepository)
    {
        _csvFileRepository = csvFileRepository;
    }

    public Task<Result<TerminalModel>> Handle(GetTerminalConfigQuery request, CancellationToken cancellationToken)
    {
        var model = _csvFileRepository.ReadFromCsvFile<TerminalModel>(
            Path.Combine(
                GlobalConfig.ScriptPath,
                string.Join(
                    "",
                    CsvFiles.staging.ToString(),
                    FileExtensions.csv.ConvertToFileExtension())));

        return Task.FromResult(Result.Success(model));
    }
}
