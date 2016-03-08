using OneNorth.ContentAnonymizer.Areas.ContentAnonymizer.Models;
using Sitecore.Data.Items;

namespace OneNorth.ContentAnonymizer.Data
{
    public interface IItemAnonymizer
    {
        void AnonymizeItem(Item item, AnonymizeOptions options);
    }
}