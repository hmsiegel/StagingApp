namespace StagingApp.Persistence.Repositories;
public sealed class CsvFileRepository : ICsvFileRepository
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    
    public T ReadFromCsvFile<T>(string? inputfile)
    {
        throw new NotImplementedException();
    }

    public void WriteToCsvFile<T>(List<T> model, string? outputfile, bool appendFile)
    {
        CsvConfiguration config = new(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false
        };

        try
        {
            if (appendFile)
            {
                using FileStream stream = File.Open(outputfile!, FileMode.Append);
                using StreamWriter writer = new(stream);
                using CsvWriter csvWriter = new(writer, config);
                csvWriter.WriteRecords(model);
                _logger.Info("Successfully added to {outputfile}...", outputfile);
            }
            else
            {
                using StreamWriter writer = new(outputfile!);
                using CsvWriter csvWriter = new(writer, config);
                csvWriter.WriteRecords(model);
                _logger.Info("Successfully wrote to {outputfile}...", outputfile);
            }
        }
        catch (CsvHelperException ex)
        {
            _logger.Error(ex);
            throw new Exception();
        }
    }
}
