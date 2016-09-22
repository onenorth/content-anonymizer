using Sitecore.Data.Fields;

namespace OneNorth.ContentAnonymizer.Data
{
    public interface IMedia
    {
        void File(FileField field, Sitecore.Globalization.Language language, string parentPath, bool overwriteEmptyValues);
        void Image(ImageField field, Sitecore.Globalization.Language language, string parentPath, bool overwriteEmptyValues);
    }
}