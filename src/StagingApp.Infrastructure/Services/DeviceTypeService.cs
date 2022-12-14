namespace StagingApp.Infrastructure.Services;
public class DeviceTypeService : IDeviceTypeService
{
    public string DetermineDeviceType()
    {
        string? computerName = GlobalConfig.ComputerName;
        string? stagingMarkerFile = Directory.GetFiles(
            GlobalConfig.ScriptPath,
            FileExtensions.stage.ConvertToFileExtension(),
            SearchOption.TopDirectoryOnly)
            .FirstOrDefault();
        string? deviceType;
        if (stagingMarkerFile is not null)
        {
            deviceType = CheckStagingMarkerFile(stagingMarkerFile);
        }
        else
        {
            deviceType = CheckComputerName(computerName);
        }
        return deviceType!;
    }

    private static string? CheckComputerName(string computerName)
    {
        List<string> termPrefixes = Enum.GetValues(typeof(TermPrefix))
            .Cast<TermPrefix>()
            .Select(x => x.ToString())
            .ToList();

        string? stagingViewModel = string.Empty;

        stagingViewModel = IsThisATerminal(computerName)
            ? DeviceType.Terminal.ToString()
            : computerName.ToLower() switch
            {
                var name when name.StartsWith(InitialDevicePrefix.ALOHABOH.ToString().ToLower()) => DeviceType.Server.ToString(),
                var name when name.StartsWith(GlobalConfig.Ibgolden.ToLower()) => DeviceType.Server.ToString(),
                var name when name.StartsWith(InitialDevicePrefix.TERM.ToString().ToLower()) => DeviceType.Terminal.ToString(),
                var name when name.StartsWith(InitialDevicePrefix.AK.ToString().ToLower()) => DeviceType.Kitchen.ToString(),
                _ => null,
            };
        if (stagingViewModel is null)
        {
            // TODO: Throw an error here
        }

        return stagingViewModel;
    }

    private static string? CheckStagingMarkerFile(string stagingMarkerFile)
    {
        string? stagingViewModel = string.Empty;
        switch (stagingMarkerFile.ToLower())
        {
            case var file when file.Contains(StagingRegistryKey.ServerStaging.ToString().ToLower()):
                stagingViewModel = DeviceType.Server.ToString();
                break;
            case var file when file.Contains(StagingRegistryKey.TerminalStaging.ToString().ToLower()):
                stagingViewModel = DeviceType.Terminal.ToString();
                break;
            case var file when file.Contains(StagingRegistryKey.AKStaging.ToString().ToLower()):
                stagingViewModel = DeviceType.Kitchen.ToString();
                break;
            default:
                // Inform the user that the staging marker file is not correct
                // Close the application
                break;
        }
        return stagingViewModel;
    }
    private static bool IsThisATerminal(string computerName)
    {
        List<string> termPrefixes = Enum.GetNames(typeof(TermPrefix))
            .Cast<string>()
            .Select(x => x.ToString())
            .ToList();

        IEnumerable<string?> escapedWords = termPrefixes
            .Select(x => Regex.Escape(x));
        Regex pattern = new($@"({string.Join("|", escapedWords)})\d{{2,4}}(_|-)\d{{1,3}}");

        return pattern.IsMatch(computerName);
    }
}
