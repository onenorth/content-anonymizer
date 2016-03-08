using OneNorth.ContentAnonymizer.Areas.ContentAnonymizer.Models;
using Sitecore.Data.Items;

namespace OneNorth.ContentAnonymizer.Data
{
    public interface IItemNameAnonymizer
    {
        void AnonymizeItemName(Item item, AnonymizeOptions options);
    }
}