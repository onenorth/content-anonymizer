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

        public void File(FileField field, string parentPath)
        {
            if (string.IsNullOrEmpty(field.Value))
                return;

            var mediaItem = field.MediaItem;
            if (mediaItem == null)
                return;

            mediaItem = GetRandomMediaItem(mediaItem, parentPath);
            if (mediaItem == null)
                return;

            field.MediaID = mediaItem.ID;
            var shellOptions = MediaUrlOptions.GetShellOptions();
            field.Src = MediaManager.GetMediaUrl(mediaItem, shellOptions);
        }

        public void Image(ImageField field, string parentPath)
        {
            if (string.IsNullOrEmpty(field.Value))
                return;

            var mediaItem = field.MediaItem;
            if (mediaItem == null)
                return;

            mediaItem = GetRandomMediaItem(mediaItem, parentPath);
            if (mediaItem == null)
                return;

            field.MediaID = mediaItem.ID;
            field.SetAttribute("showineditor", "1");
        }

        private static MediaItem GetRandomMediaItem(MediaItem currentMediaItem, string parentPath)
        {
            var index = ContentSearchManager.GetIndex("sitecore_master_index");
            if (index == null)
                throw new ApplicationException("sitecore_master_index not found");

            var item = (Item) currentMediaItem;
            var templateId = item.TemplateID;
            var language = item.Language.Name;

            List<SearchResultItem> searchResultItems;
            using (var context = index.CreateSearchContext())
            {
                searchResultItems = context.GetQueryable<SearchResultItem>()
                    .Where(x => x.TemplateId == templateId && x[BuiltinFields.LatestVersion].Equals("1") && x.Language == language)
                    .ToList();
            }

            // Filter based on path
            searchResultItems = searchResultItems.Where(x => x.Path.StartsWith(parentPath)).ToList();

            var searchResultItem = searchResultItems.Random();
            item = searchResultItem.GetItem();
            var mediaItem = new MediaItem(item);
            return mediaItem;
        }
    }
}