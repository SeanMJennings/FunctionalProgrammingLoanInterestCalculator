namespace Domain.Primitives;

public static class InterestRate
{
    public static string ToInterestRate(this decimal amount) => $"{Math.Round(amount, 2)}%";
}