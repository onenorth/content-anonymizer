using OneNorth.DataAnonymizer.Areas.DataAnonymizer.Models;
using Sitecore.Data.Items;

namespace OneNorth.DataAnonymizer.Data
{
    public interface IItemNameAnonymizer
    {
        void AnonymizeItemName(Item item, AnonymizeOptions options);
    }
}