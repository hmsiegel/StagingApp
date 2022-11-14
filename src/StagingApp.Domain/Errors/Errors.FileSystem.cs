namespace StagingApp.Domain.Errors;
public static class Errors
{
    public static class FileSystem
    {
        public static Error MarkerFileAlreadyExists => Error.Conflict(
            code: "FileSystem.MarkerFileAlreadyExists",
            description: "Marker file already exists");
    }

}
