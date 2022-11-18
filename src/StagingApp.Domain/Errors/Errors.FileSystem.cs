namespace StagingApp.Domain.Errors;
public static partial class Errors
{
    public static class FileSystem
    {
        public static Error MarkerFileAlreadyExists => new(
            code: "FileSystem.MarkerFileAlreadyExists",
            message: "Marker file already exists");

        public static Error CannotCreateMarkerFile => new(
            code: "FileSystem.CannotCreateMarkerFile",
            message: "Error creating marker file.");

        public static Error CannotDeleteMarkerFile => new(
            code: "FileSystem.CannotDeleteMarkerFile",
            message: "Error deleting marker file.");

        public static Error MarkerFileIsNull => new(
            code: "FileSystem.MarkerFileIsNull",
            message: "Marker file is null.");
    }

}
