using OneNorth.DataAnonymizer.Areas.DataAnonymizer.Models;
using Sitecore.Data.Items;

namespace OneNorth.DataAnonymizer.Data
{
    public interface IItemAnonymizer
    {
        void AnonymizeItem(Item item, AnonymizeOptions options);
    }
}