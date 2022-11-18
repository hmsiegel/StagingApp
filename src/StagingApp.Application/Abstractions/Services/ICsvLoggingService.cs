using StagingApp.Domain.Kitchen.ValueObjects;

namespace StagingApp.Application.Abstractions.Services;
public interface ICsvLoggingService
{
    void CreateCsvServerLog(Server model, PackageModel package);
    void CreateCsvTemrinalLog();
    void CreateCsvKitchenLog(KitchenController model);
}
