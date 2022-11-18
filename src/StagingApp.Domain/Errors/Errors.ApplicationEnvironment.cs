namespace StagingApp.Domain.Errors;
public static partial class Errors
{
    public static class ApplicationEnvironment
    {
        public static Error InvalidApplicationPath => new(
            code: "ApplicationEnvironment.InvalidApplicationPath",
            message: "The application path is incorrect");

        public static Error NullApplicationPath => new(
            code: "ApplicationEnvironment.NullApplicationPath",
            message: "The application path is empty.");
    }
}
