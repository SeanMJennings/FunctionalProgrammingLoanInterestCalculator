using FunctionalProgrammingKit;
using NodaTime;

namespace Domain.Primitives;

public static class LocalDateExtensions
{
    public static ValueObject<LocalDate> Create(this LocalDate localDate)
    {
        return Valid(localDate);
    }  
}