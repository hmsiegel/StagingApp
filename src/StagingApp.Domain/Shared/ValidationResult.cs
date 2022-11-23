namespace StagingApp.Domain.Shared;
public sealed class ValidationResult : Result, IValidationResult
{
    private ValidationResult(Errors[] errors) : base(false, IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public Errors[] Errors { get; }

    public static ValidationResult WithErrors(Errors[] errors) => new(errors);
}
