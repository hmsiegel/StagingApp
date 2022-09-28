namespace StagingApp.Presentation.ViewModels.ConfigureViewModels;
public partial class ServerConfigureViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof (ConfigureCommand))]
    private string? _stagingTechName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsHardDriveLetterVisible))]
    private string? _selectedHardDriveReplacement;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ConfigureCommand))]
    private string? _selectedSiteId;

    [ObservableProperty]
    private string? _selectedHardDriveLetter;

    [ObservableProperty]
    private List<string>? _siteIds;

    [ObservableProperty]
    private ObservableCollection<string>? _hardDriveLettersList;

    [ObservableProperty]
    private ObservableCollection<string>? _replacementSelectionsList;

    public bool IsHardDriveLetterVisible
    {
        get
        {
            bool output = false;
            if (SelectedHardDriveReplacement?.ToLower() == "yes")
            {
                output = true;
            }
            return output;
        }
    }

    private bool CanConfigure => SelectedSiteId is not null && StagingTechName is not null;

    public ServerConfigureViewModel()
    {
        LoadList();
    }

    private void LoadList()
    {
        var hdList = EnumServices.GetEnumDescriptions<HardDriveLetters>();
        var replacementSelection = EnumServices.GetEnumDescriptions<ReplacementSelections>();

        HardDriveLettersList = new ObservableCollection<string>(hdList);
        ReplacementSelectionsList = new ObservableCollection<string>(replacementSelection);
    }

    [RelayCommand(CanExecute = nameof(CanConfigure))]
    private void Configure()
    {
        throw new NotImplementedException();
    }
}
