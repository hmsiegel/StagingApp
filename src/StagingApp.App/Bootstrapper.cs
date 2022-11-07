namespace StagingApp.Main;
[SupportedOSPlatform("Windows7.0")]
public sealed class Bootstrapper : BootstrapperBase
{
    private readonly SimpleContainer _container = new();
    private const string _viewModel = "ViewModel";
    private const string _deviceType = "DeviceType";
    public const string SettingsFileName = "appsettings.json";
    private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
    public static readonly string SettingsFileFullName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, SettingsFileName);

    public Bootstrapper()
    {
        Initialize();
        LogManager.GetLog = type => new DebugLog(type);

        if (!Directory.Exists(GlobalConfig.ScriptPath))
        {
            Directory.CreateDirectory(GlobalConfig.ScriptPath);
        }

        UpdateLogConfig();

        _logger.Info("********************************************************************");
        _logger.Info("Starting application.");
        _logger.Info("Current logged in user: {user}", WindowsIdentity.GetCurrent().Name);
    }

    private static void UpdateLogConfig()
    {
        string? deviceType = StagingHelper.DetermineDeviceType();

        switch (deviceType)
        {
            case var type when type.Equals(DeviceType.Server.ToString()):
                GlobalDiagnosticsContext.Set(_deviceType, StagingRegistryKey.ServerStaging.ToString());
                break;
            case var type when type.Equals(DeviceType.Terminal.ToString()):
                GlobalDiagnosticsContext.Set(_deviceType, StagingRegistryKey.TerminalStaging.ToString());
                break;
            case var type when type.Equals(DeviceType.Kitchen.ToString()):
                GlobalDiagnosticsContext.Set(_deviceType, StagingRegistryKey.AKStaging.ToString());
                break;
            default:
                break;
        }
    }

    protected override void Configure()
    {
        var config = new TypeMappingConfiguration
        {
            DefaultSubNamespaceForViewModels = "Presentation.ViewModels",
            DefaultSubNamespaceForViews = "Main.Views"
        };
        ViewLocator.ConfigureTypeMappings(config);
        ViewModelLocator.ConfigureTypeMappings(config);
        ViewLocator.AddSubNamespaceMapping("Presentation.ViewModels.*", new[] { "Main.Views.ConfigureViews", "Main.Views.InfoViews" });

        _container.Instance(_container)
            .PerRequest<ShellViewModel>();

        _container
            .Singleton<IWindowManager, WindowManager>()
            .Singleton<IEventAggregator, EventAggregator>()
            .Singleton<TerminalConfigureViewModel>()
            .Singleton<KitchenConfigureViewModel>()
            .Singleton<ServerConfigureViewModel>();

        _container
            .RegisterInstance(typeof(IConfiguration), nameof(IConfiguration), AddConfiguration());

        GetType().Assembly.GetTypes()
            .Where(t => t.IsClass)
            .Where(t => t.Name.EndsWith(_viewModel))
            .ToList()
            .ForEach(vm => _container.RegisterPerRequest(
                vm, vm.ToString(), vm));

    }

    protected override async void OnStartup(object sender, StartupEventArgs e)
    {
        await DisplayRootViewForAsync(typeof(ShellViewModel));
    }

    protected override object GetInstance(Type service, string key)
    {
        return _container.GetInstance(service, key);
    }

    protected override IEnumerable<object> GetAllInstances(Type service)
    {
        return _container.GetAllInstances(service);
    }

    protected override void BuildUp(object instance)
    {
        _container.BuildUp(instance);
    }

    protected override IEnumerable<Assembly> SelectAssemblies()
    {
        var assemblies = base.SelectAssemblies().ToList();
        assemblies.Add(typeof(Presentation.AssemblyReference).Assembly);
        assemblies.Add(typeof(AssemblyReference).Assembly);

        return assemblies;
    }
    private static IConfiguration AddConfiguration()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(SettingsFileFullName, false, false);

        return builder.Build();

    }
}
