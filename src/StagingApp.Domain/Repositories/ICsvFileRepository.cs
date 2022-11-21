namespace StagingApp.Domain.Repositories;
public interface ICsvFileRepository
{
    void WriteToCsvFile<T>(List<T> model, string? outputfile, bool appendFile);
    T ReadFromCsvFile<T>(string? inputfile);
}
