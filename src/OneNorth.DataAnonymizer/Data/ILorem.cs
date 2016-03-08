using OneNorth.DataAnonymizer.Data.Locales;

namespace OneNorth.DataAnonymizer.Data
{
    public interface ILorem
    {
        string Words(ILocale locale, int wordCount = 3);
        string Sentence(ILocale locale, int minWordCount = 3, int maxWordCount = 10);
        string Sentences(ILocale locale, int minSentenceCount = 2, int maxSentenceCount = 5);
        string Paragraph(ILocale locale, int minSentenceCount = 3, int maxSentenceCount = 10);
        string Paragraphs(ILocale locale, int minParagraphCount = 3, int maxParagraphCount = 10);
        string Replace(ILocale locale, string html);
    }
}