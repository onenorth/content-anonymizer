using System;
using Sitecore.Data;

namespace OneNorth.DataAnonymizer.Areas.DataAnonymizer.Models
{
    public class ItemInfo
    {
        public bool Anonymize { get; set; }
        public Guid Id { get; set; }
        public string Path { get; set; }
    }
}