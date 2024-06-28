namespace FunctionalProgrammingKit;

public class ResponseDto<T>
{
    internal readonly IEnumerable<Error> _errors;
    private readonly T _value;

    public bool IsValid => !_errors.Any();

    private ResponseDto(T t) => (_errors, _value) = (Enumerable.Empty<Error>(), t ?? throw new ArgumentNullException(nameof(t)));
    private ResponseDto(IEnumerable<Error> errors) => (_errors, _value) = (errors, default!);

    public static ResponseDto<T> Valid(T t) => new(t);
    public static ResponseDto<T> Invalid(IEnumerable<Error> errors) => new(errors);

    public static implicit operator ResponseDto<T>(T t) => Valid(t);
    public static implicit operator ResponseDto<T>(Error error) => Invalid(new[] { error });
    public static implicit operator ResponseDto<T>(Error[] errors) => Invalid(errors.ToArray());

    public R Match<R>(Func<Error[], R> Invalid, Func<T, R> Valid)
    {
        return IsValid ? Valid(_value!) : Invalid(_errors.ToArray()!);
    }
    public ReturnWrapper<T> Match()
    {
        return IsValid ? ValidWrapper(_value!) : InvalidWrapper<T>(_errors.ToArray());
    } 
}