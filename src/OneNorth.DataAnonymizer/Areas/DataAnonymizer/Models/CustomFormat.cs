using System.Collections.Generic;

namespace OneNorth.DataAnonymizer.Areas.DataAnonymizer.Models
{
    public class CustomFormat
    {
        public string Format { get; set; }
        public List<Token> Tokens { get; set; }
    }
}