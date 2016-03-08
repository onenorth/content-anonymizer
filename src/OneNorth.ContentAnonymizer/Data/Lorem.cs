using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using OneNorth.ContentAnonymizer.Data.Locales;

namespace OneNorth.ContentAnonymizer.Data
{
    public class Lorem : ILorem
    {
        private static readonly ILorem _instance = new Lorem();
        public static ILorem Instance { get { return _instance; } }

        private Lorem()
        {
            
        }

        public string Words(ILocale locale, int wordCount = 3)
        {
            return string.Join(" ", locale.LoremWords.Random(wordCount));
        }

        public string Sentence(ILocale locale, int minWordCount = 3, int maxWordCount = 14)
        {
            var wordCount = RandomProvider.GetThreadRandom().Next(minWordCount, maxWordCount);
            var sentence = string.Join(" ", locale.LoremWords.Random(wordCount)) + ".";
            return char.ToUpper(sentence[0]) + ((sentence.Length > 1) ? sentence.Substring(1).ToLower() : string.Empty);
        }

        public string Sentences(ILocale locale, int minSentenceCount = 2, int maxSentenceCount = 5)
        {
            var sentenceCount = RandomProvider.GetThreadRandom().Next(minSentenceCount, maxSentenceCount);
            var sentences = new List<string>();
            for (var i = 0; i < sentenceCount; i++)
            {
                sentences.Add(Sentence(locale));
            }
            return string.Join(" ", sentences);
        }

        public string Paragraph(ILocale locale, int minSentenceCount = 3, int maxSentenceCount = 10)
        {
            var sentenceCount = RandomProvider.GetThreadRandom().Next(minSentenceCount, maxSentenceCount);
            var sentences = new List<string>();
            for (var i = 0; i < sentenceCount; i++)
            {
                sentences.Add(Sentence(locale));
            }
            return string.Join(" ", sentences);
        }

        public string Paragraphs(ILocale locale, int minParagraphCount = 3, int maxParagraphCount = 10)
        {
            var paragraphCount = RandomProvider.GetThreadRandom().Next(minParagraphCount, maxParagraphCount);
            var paragraphs = new List<string>();
            for (var i = 0; i < paragraphCount; i++)
            {
                paragraphs.Add(Paragraph(locale));
            }
            return string.Join(Environment.NewLine, paragraphs);
        }

        public string Replace(ILocale locale, string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var nodes = doc.DocumentNode.SelectNodes("//text()");
            if (nodes == null)
                return html;

            foreach (var node in nodes)
            {
                var htmlTextNode = node as HtmlTextNode;
                if (htmlTextNode == null)
                    continue;

                var regex = new Regex(@"\w+", RegexOptions.IgnoreCase);
                htmlTextNode.Text = regex.Replace(htmlTextNode.Text, match =>
                    {
                        var value = match.Value;
                        if (string.Equals(value, "lt", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(value, "gt", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(value, "amp", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(value, "nbsp", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(value, "http", StringComparison.OrdinalIgnoreCase))
                            return value;
                        if (char.IsUpper(value[0]))
                            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(locale.LoremWords.Random());
                        return locale.LoremWords.Random();
                    });
            }

            return doc.DocumentNode.OuterHtml;
        }
    }
}