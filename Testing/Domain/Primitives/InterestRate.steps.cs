using Domain.Primitives;
using FluentAssertions;
using NUnit.Framework;

namespace Testing.Domain.Primitives;

[TestFixture]
public static partial class InterestRateSpecsShould
{
    private const decimal positive_value = 1.4567m;
    
    private static object a_decimal_value()
    {
        return positive_value;
    }

    private static object converting_to_interest_rate(object interest_rate)
    {
        return ((decimal)interest_rate).ToInterestRate();
    }    
    
    private static void interest_rate_is_formatted_to_2_dp_with_percentage_symbol(object interest_rate)
    {
        interest_rate.Should().Be($"{Math.Round(positive_value, 2)}%");
    }
}