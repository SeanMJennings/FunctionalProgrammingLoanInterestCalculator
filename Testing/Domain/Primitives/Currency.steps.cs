using Domain.Primitives;
using FluentAssertions;
using FunctionalProgrammingKit;
using NUnit.Framework;

namespace Testing.Domain.Primitives;

[TestFixture]
public static partial class CurrencySpecsShould
{
    private const string US_code = "US";
    private const string unknown_code = "wibble";
    private const string US_iso_currency_symbol = "USD";
    
    private static object a_two_letter_iso_code()
    {
        return US_code;
    }    
    
    private static object an_unknown_code()
    {
        return unknown_code;
    }

    private static object converting_to_currency(object code)
    {
        return Currency.New((string)code);
    }

    private static string WhenValidating(this object previousResult, Func<object, object> func)
    {
        return previousResult.WhenValidating<Currency>(func);
    }

    private static void currency_is_created(object currency)
    {
        ((ValueObject<Currency>)currency).Match().Value.ToString().Should().Be(US_iso_currency_symbol);
    }
}