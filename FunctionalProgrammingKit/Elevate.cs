namespace FunctionalProgrammingKit;

public static class Elevate
{
    public static ValueObject<U> Bind<T, U>(this ValueObject<T> value, Func<T, ValueObject<U>> func)
    {
        return value.Match(Some: func, None: Invalid<U>);
    }
    
    public static ValueObject<U> Map<T, U>(this ValueObject<T> value, Func<T, U> func)
    {
        return value.Match(Some: v => Valid(func(v)), None: Invalid<U>);
    }
}