using OneNorth.ContentAnonymizer.Data.Locales;

namespace OneNorth.ContentAnonymizer.Data
{
    public interface IInternet
    {
        string Email(ILocale locale, string firstName = null, string lastName = null, string provider = null);
        string Url(ILocale locale);
        string UserName(ILocale locale, string firstName = null, string lastName = null);
    }
}