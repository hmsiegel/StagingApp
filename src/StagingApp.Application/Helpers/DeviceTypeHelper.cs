namespace StagingApp.Application.Helpers;
public static class DeviceTypeHelper
{
    public static bool IsThisATerminal(string? computerName)
    {
        List<string?> termPrefixes = Enum.GetNames(typeof(TermPrefix))
            .Cast<string>()
            .Select(x => x.ToString()
            .ToList();

        IEnumerable<string?> escapedWords = termPrefixes
            .Select(x => Regex.Escape(x));
        Regex pattern = new($@"({string.Join("|", escapedWords)})\d{{2,4}}(_|-)\d{{1,3}}");

        return pattern.IsMatch(computerName);
    }
    public static string? DetermineDeviceType()
    {
        string? computerName = GlobalConfig.ComputerName;
        string? stagingMarkerFile = Directory.GetFiles(
            GlobalConfig.ScriptPath,
            GlobalConfig.StageExtension,
            SearchOption.TopDirectoryOnly)
            .FirstOrDefault();
        string? stagingViewModelName;
        if (stagingMarkerFile is not null)
        {
            stagingViewModelName = CheckStagingMarkerFile(stagingMarkerFile);
        }
        else
        {
            stagingViewModelName = CheckComputerName(computerName);
        }
        return stagingViewModelName;
    }

    private static string? CheckComputerName(string? computerName)
    {
        List<string> termPrefixes = Enum.GetValues(typeof(TermPrefix))
            .Cast<TermPrefix>()
            .Select(x => x.ToString())
            .ToList();

        string? stagingViewModel = string.Empty;

        if (DeviceTypeHelper.IsThisATerminal(computerName))
        {
            stagingViewModel = DeviceType.Terminal.ToString();
        }
        else
        {
            stagingViewModel = computerName.ToLower() switch
            {
                var name when name.StartsWith(InitialDevicePrefix.ALOHABOH.ToString().ToLower()) => DeviceType.Server.ToString(),
                var name when name.StartsWith(GlobalConfig.Ibgolden.ToLower()) => DeviceType.Server.ToString(),
                var name when name.StartsWith(InitialDevicePrefix.TERM.ToString().ToLower()) => DeviceType.Terminal.ToString(),
                var name when name.StartsWith(InitialDevicePrefix.AK.ToString().ToLower()) => DeviceType.Kitchen.ToString(),
                _ => null,
            };
        }
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
}
