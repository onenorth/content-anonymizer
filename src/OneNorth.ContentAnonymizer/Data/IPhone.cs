using OneNorth.ContentAnonymizer.Data.Locales;

namespace OneNorth.ContentAnonymizer.Data
{
    public interface IPhone
    {
        string PhoneNumber(ILocale locale);
        string FaxNumber(ILocale locale);
        string MobileNumber(ILocale locale);
    }
}