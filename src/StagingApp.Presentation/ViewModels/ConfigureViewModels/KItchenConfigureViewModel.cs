namespace StagingApp.Presentation.ViewModels.ConfigureViewModels;
public partial class KitchenConfigureViewModel : BaseConfigureViewModel
{
    [ObservableProperty]
    private string? _controllerName;
    
    [ObservableProperty]
    private string? _controllerNumber;
    
    [ObservableProperty]
    private string? _termStr;

    [ObservableProperty]
    private string? _bohServerName;
    
    [ObservableProperty]
    private string? _ipAddress;
    
    [ObservableProperty]
    private string? _keyNumber;

    [ObservableProperty]
    private ObservableCollection<KitchenConcepts>? _concept;

    public KitchenConfigureViewModel()
    {
    }

    public override void Configure()
    {
        throw new NotImplementedException();
    }
}
