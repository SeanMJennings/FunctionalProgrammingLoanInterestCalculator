using FunctionalProgrammingKit;

namespace Domain;

public readonly record struct Money
{
    internal decimal Amount { get; }
    private Money(decimal amount)
    {
        Amount = amount;
    }

    public static ValueObject<Money> New(decimal amount)
    {
        if (amount < 0) return new Error("Amount cannot be negative.");
        return new Money(amount);
    }
    
    public override string ToString()
    {
        return Amount.ToString("F");
    }

    public static implicit operator ValueObject<Money>(Money money) => Valid(money);
    public static implicit operator decimal(Money money) => money.Amount;
}

public static class MoneyFunctionality
{
    public static ValueObject<Money> Add(this Money money, decimal amount)
    {
        return Money.New(money.Amount + amount);
    }    
    
    public static ValueObject<Money> Multiply(this Money money, int multiplier)
    {
        return Money.New(money.Amount * multiplier);
    }
}

