using NUnit.Framework;

namespace Testing.Domain.Primitives;

public static partial class CurrencySpecsShould
{
    [Test]
    public static void convert_a_two_letter_iso_code_to_a_currency()
    {
        Given(a_two_letter_iso_code).When(converting_to_currency).Then(currency_is_created);
    }

    [Test]
    public static void informs_unknown_currency_is_unknown()
    {
        Given(an_unknown_code).WhenValidating(converting_to_currency).ThenInforms("The code provided is unknown.");
    }
}