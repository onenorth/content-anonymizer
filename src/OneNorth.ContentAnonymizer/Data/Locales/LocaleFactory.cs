
namespace OneNorth.ContentAnonymizer.Data.Locales
{
    public class LocaleFactory : ILocaleFactory
    {
        private static readonly ILocaleFactory _instance = new LocaleFactory();
        public static ILocaleFactory Instance { get { return _instance; } }

        private readonly ILocale _en = new en();

        public ILocale Get(string locale)
        {
            switch (locale)
            {
                // TODO: add additional language support
                case "en":
                    return _en;
                default:
                    return _en;
            }
        }
    }
}