using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;

namespace OneNorth.ContentAnonymizer.Data
{
    public class Media : IMedia
    {
        private static readonly IMedia _instance = new Media();
        public static IMedia Instance { get { return _instance; } }

        private Media()
        {
            
        }

        public void File(FileField field, Sitecore.Globalization.Language language, string parentPath, bool overwriteEmptyValues)
        {
            if (!overwriteEmptyValues && string.IsNullOrEmpty(field.Value))
                return;

            var mediaItem = field.MediaItem;
            if (!overwriteEmptyValues && mediaItem == null)
                return;

            mediaItem = GetRandomMediaItem(mediaItem, language, parentPath);
            if (mediaItem == null)
                return;

            field.MediaID = mediaItem.ID;
            var shellOptions = MediaUrlOptions.GetShellOptions();
            field.Src = MediaManager.GetMediaUrl(mediaItem, shellOptions);
        }

        public void Image(ImageField field, Sitecore.Globalization.Language language, string parentPath, bool overwriteEmptyValues)
        {
            if (!overwriteEmptyValues && string.IsNullOrEmpty(field.Value))
                return;

            var mediaItem = field.MediaItem;
            if (!overwriteEmptyValues && mediaItem == null)
                return;

            mediaItem = GetRandomMediaItem(mediaItem, language, parentPath);
            if (mediaItem == null)
                return;

            field.MediaID = mediaItem.ID;
            field.SetAttribute("showineditor", "1");
        }

        private static MediaItem GetRandomMediaItem(MediaItem currentMediaItem, Sitecore.Globalization.Language language, string parentPath)
        {
            var index = ContentSearchManager.GetIndex("sitecore_master_index");
            if (index == null)
                throw new ApplicationException("sitecore_master_index not found");

            var searchResultItems = new List<SearchResultItem>();

            var item = (Item) currentMediaItem;
            if (item != null)
            {
                var templateId = item.TemplateID;
                var languageName = item.Language.Name;

                using (var context = index.CreateSearchContext())
                {
                    searchResultItems = context.GetQueryable<SearchResultItem>()
                        .Where(x => x.TemplateId == templateId && x[BuiltinFields.LatestVersion].Equals("1") && x.Language == languageName)
                        .ToList();
                }

                // Filter based on path
                searchResultItems = searchResultItems.Where(x => x.Path.StartsWith(parentPath)).ToList();
            }

            if (searchResultItems.Count == 0)
            {
                // TODO: Make the image template configurable. This only finds images that use template: /sitecore/templates/System/Media/Unversioned/Image/.
                var unversionedImage = Sitecore.Data.ID.Parse("F1828A2C-7E5D-4BBD-98CA-320474871548");
                var languageName = language.Name;

                using (var context = index.CreateSearchContext())
                {
                    searchResultItems = context.GetQueryable<SearchResultItem>()
                        .Where(x => x.TemplateId == unversionedImage && x[BuiltinFields.LatestVersion].Equals("1") && x.Language == languageName)
                        .ToList();
                }

                // Filter based on path
                searchResultItems = searchResultItems.Where(x => x.Path.StartsWith(parentPath)).ToList();
            }

            var searchResultItem = searchResultItems.Random();
            item = searchResultItem.GetItem();
            var mediaItem = new MediaItem(item);
            return mediaItem;
        }
    }
}