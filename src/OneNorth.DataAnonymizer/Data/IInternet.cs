using OneNorth.DataAnonymizer.Data.Locales;

namespace OneNorth.DataAnonymizer.Data
{
    public interface IInternet
    {
        string Email(ILocale locale, string firstName = null, string lastName = null, string provider = null);
        string Url(ILocale locale);
        string UserName(ILocale locale, string firstName = null, string lastName = null);
    }
}