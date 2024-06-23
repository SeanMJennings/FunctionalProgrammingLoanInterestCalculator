namespace FunctionalProgrammingKit;

public static class Return
{
    public static R Match<T,R>(this ValueObject<T> value, Func<T, R> Some, Func<Error[], R> None) => value.IsValid ? Some(value._value!) : None(value._errors);
}

public record ReturnWrapper<T>(T Value, Error[] Errors);

public static class ReturnWrapperExtensions
{
    public static ReturnWrapper<T> ValidWrapper<T>(T v) => new(v, Array.Empty<Error>());
    public static ReturnWrapper<T> InvalidWrapper<T>(Error[] errors) => new(default!, errors);
}