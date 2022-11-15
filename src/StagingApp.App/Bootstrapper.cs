namespace StagingApp.Main;

[SupportedOSPlatform("Windows7.0")]
public sealed partial class Bootstrapper : BootstrapperBase
{
    private IContainer? _container;

    private const string _applicationPrefix = "StagingApp";
    private const string _deviceType = "DeviceType";

    private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();

    public const string SettingsFileName = "appsettings.json";

    public static readonly string SettingsFileFullName =
        Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                     ?? string.Empty, SettingsFileName);

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

        ConfigurationModule module = AddConfiguration();

        var builder = new ContainerBuilder();
        builder.RegisterModule(module);
        RegisterClass<ShellViewModel>(builder);
        RegisterTypes(builder);
        RegisterModules(builder);
        _container = builder.Build();
    }


    protected override async void OnStartup(object sender, StartupEventArgs e)
    {
        _logger.Info("Checking if the application is running as an administrator...");
        _logger.Info("Application is running as administrator.");
        await DisplayRootViewForAsync<ShellViewModel>();

        // TODO: Change code to check if running as administartor prior to deployment.

        //bool isAdmin = IsAdmin();

        //if (isAdmin == false)
        //{
        //    _logger.Info("Application is not running as administrator. Restarting application as administrator...");
        //    string? exeName = Environment.ProcessPath;
        //    RestartApplicationAsAdministrator(exeName);
        //    return;
        //}
        //else
        //{
        //    _logger.Info("Application is running as administrator.");
        //    await DisplayRootViewForAsync<ShellViewModel>();
        //}

    }

    protected override object GetInstance(Type service, string key) =>
        key == null ? _container!.Resolve(service) : _container!.ResolveKeyed(key, service);

    protected override IEnumerable<object> GetAllInstances(Type service)
    {
        var enumerableOfServiceType = typeof(IEnumerable<>).MakeGenericType(service);
        return (IEnumerable<object>)_container!.Resolve(enumerableOfServiceType);
    }

    protected override void BuildUp(object instance)
    {
        _container!.InjectProperties(instance);
    }

    protected override IEnumerable<Assembly> SelectAssemblies()
    {
        return GetAllDllEntries().Select(Assembly.LoadFrom);
    }

    private static ConfigurationModule AddConfiguration()
    {
        var configuration = new ConfigurationBuilder();
        configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(SettingsFileFullName, false, false);

        var module = new ConfigurationModule(configuration.Build());
        return module;
    }

    private static void RegisterModules(ContainerBuilder builder)
    {
        builder.RegisterModule<Presentation.ModuleLoader>();
        builder.RegisterModule<Application.ModuleLoader>();
        builder.RegisterModule<Infrastructure.ModuleLoader>();
        builder.RegisterModule<Persistence.ModuleLoader>();
    }

    private static void RegisterTypes(ContainerBuilder builder)
    {
        builder.RegisterType<WindowManager>().As<IWindowManager>();
        builder.RegisterType<EventAggregator>().As<IEventAggregator>();
    }

    private static void RegisterClass<T>(ContainerBuilder builder)
    {
        builder!.RegisterType<T>()!.SingleInstance();
    }

    private static string[] GetAllDllEntries()
    {
        var runtimeDir = AppDomain.CurrentDomain.BaseDirectory;
        var files = Directory.GetFiles(runtimeDir)
            .Where(x => DllRegex().IsMatch(x))
            .Where(y =>
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(y);
                return fileNameWithoutExtension.StartsWith(_applicationPrefix, StringComparison.Ordinal);
            })
            .ToArray();

        return files;
    }

    private static void UpdateLogConfig()
    {
        string deviceType = DeviceTypeHelper.DetermineDeviceType();

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
                // Throw Exception
                break;
        }
    }

    private static bool IsAdmin()
    {
        WindowsIdentity identity = WindowsIdentity.GetCurrent();
        WindowsPrincipal principal = new(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    private void RestartApplicationAsAdministrator(string exeName)
    {
        ProcessStartInfo startInfo = new(exeName)
        {
            UseShellExecute = true,
            WorkingDirectory = Environment.CurrentDirectory,
            FileName = exeName,
            Verb = "runas"
        };
        Process.Start(startInfo);
        Application.Shutdown();
    }

    [GeneratedRegex("^.+\\.(dll)$")]
    private static partial Regex DllRegex();
}
