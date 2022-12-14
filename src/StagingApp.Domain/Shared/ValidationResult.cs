namespace StagingApp.Domain.Shared;
public sealed class ValidationResult : Result, IValidationResult
{
    private ValidationResult(Error[] errors) : base(false, IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public Error[] Errors { get; }

    public static ValidationResult WithError(Error[] errors) => new(errors);
}
