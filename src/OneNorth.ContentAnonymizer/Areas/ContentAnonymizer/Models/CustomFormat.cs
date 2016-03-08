using System.Collections.Generic;

namespace OneNorth.ContentAnonymizer.Areas.ContentAnonymizer.Models
{
    public class CustomFormat
    {
        public string Format { get; set; }
        public List<Token> Tokens { get; set; }
    }
}