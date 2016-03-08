using OneNorth.DataAnonymizer.Areas.DataAnonymizer.Models;
using OneNorth.DataAnonymizer.Data.Locales;
using Sitecore.Data.Items;

namespace OneNorth.DataAnonymizer.Data
{
    public interface IFieldAnonymizer
    {
        void AnonymizeField(FieldInfo fieldInfo, Item item, AnonymizeOptions options, ILocale locale);
        void AnonymizeCustomField(FieldInfo fieldInfo, Item item);
    }
}