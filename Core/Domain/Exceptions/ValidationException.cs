public class ValidationException(IEnumerable<string> errors) : global::System.Exception("Validation Errors")
{
    public IEnumerable<string> Errors { get; } = errors;
}