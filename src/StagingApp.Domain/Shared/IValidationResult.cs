namespace StagingApp.Domain.Shared;
public interface IValidationResult
{
    public static readonly Errors ValidationError = new(
        "Validation Error",
        "A validation problem occurred");

    Errors[] Errors { get; }
}
