using System.Globalization;
using FunctionalProgrammingKit;
using Utilities;

namespace Domain.Primitives;

public readonly record struct Currency
{
    public RegionInfo RegionInfo { get; } = null!;
    public string Code => RegionInfo.TwoLetterISORegionName;

    private Currency(string countryCode)
    {
        RegionInfo = new RegionInfo(countryCode);
    }

    public static ValueObject<Currency> New(string countryCode)
    {
        if (!Regions.IsSupportedCountryCode(countryCode)) return new Error("The code provided is unknown.");
        return new Currency(countryCode);
    }

    public override string ToString()
    {
        return RegionInfo.ISOCurrencySymbol;
    }

    public static implicit operator ValueObject<Currency>(Currency currency) => Valid(currency);
}