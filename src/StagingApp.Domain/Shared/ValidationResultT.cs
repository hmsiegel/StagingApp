namespace StagingApp.Domain.Shared;
public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    private ValidationResult(Errors[] errors)
        : base(false, IValidationResult.ValidationError, default) => 
        Errors = errors;

    public Errors[] Errors { get; }

    public static ValidationResult<TValue> WithErrors(Errors[] errors) => new(errors);
}
