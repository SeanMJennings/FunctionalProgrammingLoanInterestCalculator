using ISO3166;

namespace Utilities;

public static class Regions
{
    public static string[] SupportedCountryCodes()
    {
        return Country.List.Select(c => c.TwoLetterCode).Order().ToArray();
    }

    public static (string name, string twoLetterCode)[] Countries()
    {
        return Country.List.Select(c => (c.Name, c.TwoLetterCode)).Order().ToArray();
    }
    
    public static bool IsSupportedCountryCode(string countryCode)
    {
        return SupportedCountryCodes().Contains(countryCode, StringComparer.InvariantCultureIgnoreCase);
    }
}