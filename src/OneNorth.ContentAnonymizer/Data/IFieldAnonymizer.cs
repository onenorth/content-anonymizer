using OneNorth.ContentAnonymizer.Areas.ContentAnonymizer.Models;
using OneNorth.ContentAnonymizer.Data.Locales;
using Sitecore.Data.Items;

namespace OneNorth.ContentAnonymizer.Data
{
    public interface IFieldAnonymizer
    {
        void AnonymizeField(FieldInfo fieldInfo, Item item, AnonymizeOptions options, ILocale locale);
        void AnonymizeCustomField(FieldInfo fieldInfo, Item item);
    }
}