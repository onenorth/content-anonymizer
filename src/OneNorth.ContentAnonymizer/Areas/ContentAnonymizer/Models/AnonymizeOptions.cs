using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneNorth.ContentAnonymizer.Areas.ContentAnonymizer.Models
{
    public class AnonymizeOptions
    {
        public List<FieldInfo> Fields { get; set; }
        public List<ItemInfo> Items { get; set; }
        public CustomFormat NameFormat { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public AnonymizeType Rename { get; set; }
        public List<Replacement> Replacements { get; set; } 
        public TemplateInfo Template { get; set; }
    }
}