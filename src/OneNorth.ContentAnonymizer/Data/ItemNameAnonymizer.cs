using System;
using System.Linq;
using System.Web.Http.ModelBinding;
using OneNorth.ContentAnonymizer.Areas.ContentAnonymizer.Models;
using OneNorth.ContentAnonymizer.Data.Locales;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace OneNorth.ContentAnonymizer.Data
{
    public class ItemNameAnonymizer : IItemNameAnonymizer
    {
        private static readonly IItemNameAnonymizer _instance = new ItemNameAnonymizer();
        public static IItemNameAnonymizer Instance { get { return _instance; } }

        private readonly ILorem _lorem;

        private ItemNameAnonymizer() : this (
            Lorem.Instance)
        {

        }

        internal ItemNameAnonymizer(
            ILorem lorem)
        {
            _lorem = lorem;
        }

        public void AnonymizeItemName(Item item, AnonymizeOptions options, ILocale locale)
        {
            if (options.Rename == AnonymizeType.None)
                return;

            // Name
            var name = ResolveName(item, options, locale);
            var itemName = ProposeValidItemName(name, item.Parent, true);
            item.Name = itemName;

            // Display Name
            var displayNameField = item.Fields[FieldIDs.DisplayName];
            if (string.IsNullOrWhiteSpace(name))
                displayNameField.Reset();
            else
            {
                var displayName = (item.Name != name) ? name : string.Empty;
                if (string.IsNullOrEmpty(displayName))
                    displayNameField.Reset();
                else
                    displayNameField.Value = displayName;
            }
        }

        private string ResolveName(Item item, AnonymizeOptions options, ILocale locale)
        {
            switch (options.Rename)
            {
                case AnonymizeType.Custom:
                    return ResolveCustomNameFormat(item, options);
                case AnonymizeType.Replace:
                    return _lorem.Replace(locale, item.Name);
                default:
                    throw new NotImplementedException();
            }
        }

        private string ResolveCustomNameFormat(Item item, AnonymizeOptions options)
        {
            var format = options.NameFormat.Format;

            for (var i = 0; i < options.NameFormat.Tokens.Count; i++)
            {
                var value = string.Empty;

                var tokenFieldInfo = options.NameFormat.Tokens[i].Field;
                if (tokenFieldInfo != null)
                {
                    var tokenField = item.Fields[new ID(tokenFieldInfo.Id)];
                    if (tokenField != null)
                    {
                        value = tokenField.Value;
                        if (ID.IsID(value))
                        {
                            var tokenItem = item.Database.GetItem(new ID(value));
                            value = tokenItem.DisplayName;
                        }
                    }
                }

                var token = string.Format("${0}", i);
                format = format.Replace(token, value);
            }

            return format.Trim();
        }

        private string ProposeValidItemName(string name, Item parent, bool ensureUnique)
        {
            var proposedName = ItemUtil.ProposeValidItemName(name, "Unnamed item");
            var maxItemNameLength = Sitecore.Configuration.Settings.MaxItemNameLength;

            var truncate = proposedName.Length > maxItemNameLength;
            var itemName = truncate
                               ? proposedName.Substring(0, maxItemNameLength - 2) + "__"
                               : proposedName;

            if (parent != null && (truncate || ensureUnique))
            {
                var itemNames = parent.Children.Select(x => x.Name).ToList();

                var count = 2;
                while (itemNames.Contains(itemName))
                {
                    itemName = string.Format("{0}{1}", proposedName, count);
                    if (itemName.Length > maxItemNameLength)
                    {
                        var suffix = string.Format("{0}__", count);
                        itemName = proposedName.Substring(0, maxItemNameLength - suffix.Length) + suffix;
                    }

                    count++;
                }
            }
            return itemName;
        }
    }
}