namespace OneNorth.ContentAnonymizer.Data.Locales
{
    public interface ILocaleFactory
    {
        ILocale Get(string locale);
    }
}
