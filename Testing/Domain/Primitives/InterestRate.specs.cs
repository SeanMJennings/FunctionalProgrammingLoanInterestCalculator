using NUnit.Framework;

namespace Testing.Domain.Primitives;

public static partial class InterestRateSpecsShould
{
    [Test]
    public static void provide_rate_to_2_dp_with_percentage_symbol()
    {
        Given(a_decimal_value).When(converting_to_interest_rate).Then(interest_rate_is_formatted_to_2_dp_with_percentage_symbol);
    } 
}