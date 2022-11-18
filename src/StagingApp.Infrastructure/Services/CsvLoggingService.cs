using MediatR;

using StagingApp.Application.EnvironmentVariables.Queries.GetEnvironmentVariable;
using StagingApp.Domain.Kitchen.ValueObjects;
using StagingApp.Domain.Server.ValueObjects;
using StagingApp.Domain.Shared;

namespace StagingApp.Infrastructure.Services;
public class CsvLoggingService : ICsvLoggingService
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly ISender _sender;

    public CsvLoggingService(ISender sender)
    {
        _sender = sender;
    }

    public void CreateCsvKitchenLog(KitchenController model)
    {
        _logger.Info("Kitchen Controller Information");
        _logger.Info($"Controller Name: {model.ControllerName}");
        _logger.Info($"Controller Number: {model.ControllerNumber}");
        _logger.Info($"Server Name: {model.ServerName}");
        _logger.Info($"IP Address: {model.IpAddress}");
        _logger.Info($"TERMSTR: {model.TermStr}");
        _logger.Info($"Key Number: {model.KeyNumber}");
        _logger.Info($"Concept: {model.Concept}");
    }

    public void CreateCsvServerLog(Server model, PackageModel package)
    {
        _logger.Info("Server Information");

        _logger.Info($"Aloha Version: ");
        _logger.Info($"Aloha Kitchen Version: ");
        _logger.Info($"Aloha Takeout Version: ");
        _logger.Info($"Order Point Version:");
        _logger.Info($"Paytronix Version:");
        _logger.Info($"Server Name:");
        _logger.Info($"NumTerms:");
        _logger.Info($"Time Zone:");
        _logger.Info($"Current Date/ Time:");
        _logger.Info("");
        _logger.Info("CFC Information");
        _logger.Info($"GUID: ");
        _logger.Info($"IP Address:");
        _logger.Info("");
        _logger.Info("Aloha.ini Contents");
        _logger.Info($"SEC1 = ");
        _logger.Info($"SEC2 = ");
        _logger.Info($"SEC3 = ");
        _logger.Info($"SEC4 = ");
        _logger.Info($"SEC5 = ");
        _logger.Info($"SEC6 = ");
        _logger.Info($"Date of Business = ");
    }

    public void CreateCsvTemrinalLog()
    {
        throw new NotImplementedException();
    }
}
