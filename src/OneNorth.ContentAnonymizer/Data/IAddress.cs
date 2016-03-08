using OneNorth.ContentAnonymizer.Data.Locales;

namespace OneNorth.ContentAnonymizer.Data
{
    public interface IAddress
    {
        string PostalCode(ILocale locale);
        string City(ILocale locale);
        string StreetAddress(ILocale locale);
        string StreetAddress2(ILocale locale);
        string County(ILocale locale);
        string Country(ILocale locale);
        string State(ILocale locale);
        string Latitude();
        string Longitude();
        string Coordinates();
    }
}