using OneNorth.DataAnonymizer.Data.Locales;

namespace OneNorth.DataAnonymizer.Data
{
    public interface IName
    {
        string FirstName(ILocale locale, string original = null);
        string LastName(ILocale locale, string original = null);
        string Prefix(ILocale locale);
        string Suffix(ILocale locale);
        string FullName(ILocale locale);
    }
}