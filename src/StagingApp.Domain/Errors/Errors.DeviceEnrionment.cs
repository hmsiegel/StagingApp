namespace StagingApp.Domain.Errors;
public static partial class Errors
{
    public static class DeviceEnvironment
    {
        public static Error ChangeComputerName => new(
            code: "DeviceEnvironment.ChangeComputerName",
            message: "Error changing computer name.");
     }
}
