using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneNorth.ContentAnonymizer.Areas.ContentAnonymizer.Models
{
    public class FieldInfo
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public AnonymizeType Anonymize { get; set; }
        public string DisplayName { get; set; }
        public CustomFormat Format { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ItemInfo Source { get; set; }
        public string Type { get; set; }
        public bool OverwriteEmptyValues { get; set; }
    }
}