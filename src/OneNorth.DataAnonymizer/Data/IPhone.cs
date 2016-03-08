using OneNorth.DataAnonymizer.Data.Locales;

namespace OneNorth.DataAnonymizer.Data
{
    public interface IPhone
    {
        string PhoneNumber(ILocale locale);
        string FaxNumber(ILocale locale);
        string MobileNumber(ILocale locale);
    }
}