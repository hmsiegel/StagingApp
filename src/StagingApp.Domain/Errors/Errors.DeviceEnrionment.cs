namespace StagingApp.Domain.Errors;
public static partial class Errors
{
    public static class DeviceEnvironment
    {
        public static Error ChangeComputerName => new(
            code: "DeviceEnvironment.ChangeComputerName",
            message: "Error changing computer name.");

        public static Error JoinDomain => new(
            code: "DeviceEnvironment.JoinDomain",
            message: "Error joining to the domain.");

        public static Error SetAutoLogon => new(
            code: "DeviceEnvironment.SetAutoLogon",
            message: "Error setting autologon.");

        public static Error GetBootDrvLetter => new(
            code: "DeviceEnvironment.GetBootDrvLetter",
            message: "Error getting BootDrv letter.");
     }
}
