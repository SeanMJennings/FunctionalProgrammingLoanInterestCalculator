using NodaTime;

namespace Utilities;

public static class Date
{
    public static LocalDate Now { get; set; } = LocalDate.FromDateTime(DateTime.Now);
    
    public static LocalDate ParseDate(string date)
    {
        return LocalDate.FromDateTime(DateTime.Parse(date));
    }
}