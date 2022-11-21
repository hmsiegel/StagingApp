namespace StagingApp.Presentation.ViewModels.ConfigureViewModels;
public class ServerConfigureViewModel : BaseConfigureViewModel
{
    private string? _stagingTechName;
    private string? _selectedHardDriveReplacement;
    private string? _selectedSiteId;
    private string? _selectedHardDriveLetter;
    private BindableCollection<string>? _siteIds;
    private BindableCollection<string>? _hardDriveLettersList;
    private BindableCollection<string>? _replacementSelectionsList;

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

    [Description("STAGING TECH NAME:")]
    public string? StagingTechName
    {
        get => _stagingTechName;
        set
        {
            _stagingTechName = value;
            NotifyOfPropertyChange(() => StagingTechName);
        }
    }

    public string? SelectedHardDriveReplacement
    {
        get => _selectedHardDriveReplacement;
        set
        {
            _selectedHardDriveReplacement = value;
            NotifyOfPropertyChange(() => SelectedHardDriveReplacement);
            NotifyOfPropertyChange(() => IsHardDriveLetterVisible);
        }
    }

    public string? SelectedSiteId
    {
        get => _selectedSiteId;
        set
        {
            _selectedSiteId = value;
            NotifyOfPropertyChange(() => SelectedSiteId);
        }
    }

    public string? SelectedHardDriveLetter
    {
        get => _selectedHardDriveLetter;
        set
        {
            _selectedHardDriveLetter = value;
            NotifyOfPropertyChange(() => SelectedHardDriveLetter);
        }
    }
    public BindableCollection<string>? SiteIds
    {
        get => _siteIds;
        set
        {
            _siteIds = value;
            NotifyOfPropertyChange(() => SiteIds);
        }
    }
    public BindableCollection<string>? HardDriveLettersList
    {
        get => _hardDriveLettersList;
        set
        {
            _hardDriveLettersList = value;
            NotifyOfPropertyChange(() => HardDriveLettersList);
        }
    }
    public BindableCollection<string>? ReplacementSelectionsList
    {
        get => _replacementSelectionsList;
        set
        {
            _replacementSelectionsList = value;
            NotifyOfPropertyChange(() => SiteIds);
        }
    }

    public ServerConfigureViewModel()
    {
        LoadList();
    }

    private void LoadList()
    {
        var hdList = EnumServices.GetEnumDescriptions<HardDriveLetters>();
        var replacementSelection = EnumServices.GetEnumDescriptions<ReplacementSelections>();

        HardDriveLettersList = new BindableCollection<string>(hdList);
        ReplacementSelectionsList = new BindableCollection<string>(replacementSelection);
    }

    public override Task<bool> ValidateInput()
    {
        throw new NotImplementedException();
        // TODO: Implement data input validation
    }

    public override void Configure()
    {
        throw new NotImplementedException();
        // TODO: Implement the configure method to start the configuration
    }
}
