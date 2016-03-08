using System;

namespace OneNorth.ContentAnonymizer.Areas.ContentAnonymizer.Models
{
    public class ItemInfo
    {
        public bool Anonymize { get; set; }
        public Guid Id { get; set; }
        public string Path { get; set; }
    }
}