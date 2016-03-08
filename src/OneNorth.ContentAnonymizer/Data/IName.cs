using OneNorth.ContentAnonymizer.Data.Locales;

namespace OneNorth.ContentAnonymizer.Data
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