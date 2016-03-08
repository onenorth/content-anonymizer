using OneNorth.DataAnonymizer.Data.Locales;

namespace OneNorth.DataAnonymizer.Data
{
    public class Address : IAddress
    {
        private static readonly IAddress _instance = new Address();
        public static IAddress Instance { get { return _instance; } }

        private readonly IFormatter _formatter;
        private readonly IName _name;

        private Address() : this(
            Formatter.Instance,
            Name.Instance)
        {
            
        }

        internal Address(
            IFormatter formatter,
            IName name)
        {
            _formatter = formatter;
            _name = name;
        }

        public string PostalCode(ILocale locale)
        {
            return _formatter.ReplaceSymbols(locale.AddressPostalCode.Random());
        }

        public string City(ILocale locale)
        {
            switch (RandomProvider.GetThreadRandom().Next(3))
            {
                case 0:
                    return CityPrefix(locale) + " " + _name.FirstName(locale) + CitySuffix(locale);
                case 1:
                    return CityPrefix(locale) + " " + _name.FirstName(locale);
                case 2:
                    return _name.FirstName(locale) + CitySuffix(locale);
                case 3:
                    return _name.LastName(locale) + CitySuffix(locale);
            }
            return string.Empty;
        }

        private string CityPrefix(ILocale locale)
        {
            return locale.AddressCityPrefix.Random();
        }

        private string CitySuffix(ILocale locale)
        {
            return locale.AddressCitySuffix.Random();
        }

        public string StreetAddress(ILocale locale)
        {
            switch (RandomProvider.GetThreadRandom().Next(2))
            {
                case 0:
                    return _formatter.ReplaceSymbols("#####") + " " + StreetName(locale);
                case 1:
                    return _formatter.ReplaceSymbols("####") + " " + StreetName(locale);
                case 2:
                    return _formatter.ReplaceSymbols("###") + " " + StreetName(locale);
            }
            return string.Empty;
        }

        private string StreetName(ILocale locale)
        {
            switch (RandomProvider.GetThreadRandom().Next(1))
            {
                case 0:
                    return _name.LastName(locale) + " " + StreetSuffix(locale);
                case 1:
                    return _name.FirstName(locale) + " " + StreetSuffix(locale);
            }
            return string.Empty;
        }

        private string StreetSuffix(ILocale locale)
        {
            return locale.AddressStreetSuffix.Random();
        }

        public string StreetAddress2(ILocale locale)
        {
            return _formatter.ReplaceSymbols(locale.AddressSecondaryAddress.Random());
        }

        public string County(ILocale locale)
        {
            return locale.AddressCounty.Random();
        }

        public string Country(ILocale locale)
        {
            return locale.AddressCountry.Random();
        }

        public string State(ILocale locale)
        {
            return locale.AddressState.Random();
        }

        public string Latitude()
        {
            return (RandomProvider.GetThreadRandom().Next(180*1000000)/1000000.0 - 90.0).ToString("G");
        }

        public string Longitude()
        {
            return (RandomProvider.GetThreadRandom().Next(360 * 1000000) / 1000000.0 - 180.0).ToString("G");
        }

        public string Coordinates()
        {
            return Latitude() + "," + Longitude();
        }
    }
}