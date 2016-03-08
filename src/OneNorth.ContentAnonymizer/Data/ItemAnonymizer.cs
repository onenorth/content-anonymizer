using System.Linq;
using OneNorth.ContentAnonymizer.Areas.ContentAnonymizer.Models;
using OneNorth.ContentAnonymizer.Data.Locales;
using Sitecore.Data.Items;

namespace OneNorth.ContentAnonymizer.Data
{
    public class ItemAnonymizer : IItemAnonymizer
    {
        private static readonly IItemAnonymizer _instance = new ItemAnonymizer();
        public static IItemAnonymizer Instance { get { return _instance; } }

        private readonly IFieldAnonymizer _fieldAnonymizer;
        private readonly IItemNameAnonymizer _itemNameAnonymizer;


        private ItemAnonymizer() : this(
            FieldAnonymizer.Instance,
            ItemNameAnonymizer.Instance)
        {
            
        }

        internal ItemAnonymizer(
            IFieldAnonymizer fieldAnonymizer,
            IItemNameAnonymizer itemNameAnonymizer)
        {
            _fieldAnonymizer = fieldAnonymizer;
            _itemNameAnonymizer = itemNameAnonymizer;

        }


        public void AnonymizeItem(Item item, AnonymizeOptions options)
        {
            var locale = LocaleFactory.Instance.Get(item.Language.Name);
            if (locale == null)
                return;

            using (new EditContext(item))
            {
                // Process all fields that are not custom.
                foreach (var fieldInfo in options.Fields.Where(x => x.Anonymize != AnonymizeType.Custom))
                    _fieldAnonymizer.AnonymizeField(fieldInfo, item, options, locale);

                // Custom Values - Perform Custom format last as it is dependent on the non-custom fields
                foreach (var fieldInfo in options.Fields.Where(x => x.Anonymize == AnonymizeType.Custom))
                    _fieldAnonymizer.AnonymizeCustomField(fieldInfo, item);

                // Name - Perform rename last as it is dependent on the fields
                if (options.Rename)
                    _itemNameAnonymizer.AnonymizeItemName(item, options);
            }
        }

        
    }
}