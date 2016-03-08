using System.Collections.Generic;

namespace OneNorth.DataAnonymizer.Areas.DataAnonymizer.Models
{
    public class AnonymizeOptions
    {
        public List<FieldInfo> Fields { get; set; }
        public List<ItemInfo> Items { get; set; } 
        public CustomFormat NameFormat { get; set; }
        public bool Rename { get; set; }
        public List<Replacement> Replacements { get; set; } 
        public TemplateInfo Template { get; set; }
    }
}