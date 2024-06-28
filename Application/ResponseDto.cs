using FunctionalProgrammingKit;

namespace Application;

public class ResponseDto<T>
{
    public readonly IEnumerable<Error> Errors;
    public readonly T Data;

    public bool Success => !Errors.Any();

    internal ResponseDto(T t) => (Errors, Data) = (Enumerable.Empty<Error>(), t ?? throw new ArgumentNullException(nameof(t)));
    internal ResponseDto(IEnumerable<Error> errors) => (Errors, Data) = (errors, default!);
}