using NUnit.Framework;

namespace Testing.Domain.Primitives;

public static partial class MoneySpecsShould
{
    [Test]
    public static void provide_amount_to_2_dp()
    {
        Given(a_positive_decimal_value)
            .When(converting_to_money)
            .Then(money_is_formatted_to_2_dp);
    }
        
    [Test]
    public static void not_allow_negative_amount()
    {
        Given(a_negative_decimal_value)
            .WhenValidating(converting_to_money)
            .ThenInforms("Amount cannot be negative.");
    }
    
    [Test]
    public static void multiply_money()
    {
        Given(a_positive_decimal_value)
            .And(converting_to_money)
            .When(multiplying_by_two)
            .Then(the_amount_is_doubled);
    }
}