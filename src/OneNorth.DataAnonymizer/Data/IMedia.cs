using Sitecore.Data.Fields;

namespace OneNorth.DataAnonymizer.Data
{
    public interface IMedia
    {
        void File(FileField field, string parentPath);
        void Image(ImageField field, string parentPath);
    }
}