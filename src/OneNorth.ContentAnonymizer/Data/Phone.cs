using OneNorth.ContentAnonymizer.Data.Locales;

namespace OneNorth.ContentAnonymizer.Data
{
    public class Phone : IPhone
    {
        private static readonly IPhone _instance = new Phone();
        public static IPhone Instance { get { return _instance; } }

        private readonly IFormatter _formatter;

        private Phone() : this(
            Formatter.Instance)
        {
            
        }

        internal Phone(
            IFormatter formatter)
        {
            _formatter = formatter;
        }

        public string PhoneNumber(ILocale locale)
        {
            return _formatter.ReplaceSymbols(locale.PhoneNumberFormats.Random());
        }

        public string FaxNumber(ILocale locale)
        {
            return _formatter.ReplaceSymbols(locale.CellPhoneNumberFormats.Random());
        }

        public string MobileNumber(ILocale locale)
        {
            return _formatter.ReplaceSymbols(locale.CellPhoneNumberFormats.Random());
        }
    }
}