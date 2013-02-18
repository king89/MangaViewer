using MangaViewer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MangaViewer.Service
{
    public class MangaService
    {
        WebSiteEnum WebType;
        public MangaService()
        {
            WebType = WebSiteEnum.IManhua;
        }

        public void SetWebSiteType(WebSiteEnum type)
        {
            WebType = type;
        }

        /// <summary>
        ///   get web image and save into temp folder, return local path uri
        /// </summary>
        public Task<Uri> GetIamgeByImageUrl(MangaPageItem page)
        {
            return Task.Run<Uri>(() => 
            { 
                //Get Image
                MangaPattern mPattern = WebSiteAccess.GetMangaPatternInstance(WebType);
                return mPattern.GetImageByImageUrl(page); 
            });
        }

        public Task<ObservableCollection<MangaPageItem>> GetPageList(MangaChapterItem chapter)
        {

            return Task.Run<ObservableCollection<MangaPageItem>>(() => 
            {

                MangaPattern mPattern = WebSiteAccess.GetMangaPatternInstance(WebType);
                List<string> pageUrlList = mPattern.GetPageList(chapter.Url);
                ObservableCollection<MangaPageItem> mangaPageList = new ObservableCollection<MangaPageItem>();
                for (int i = 1; i <= pageUrlList.Count; i++)
                {
                    string imagePath = mPattern.GetImageUrl(pageUrlList[i]);
                    mangaPageList.Add(new MangaPageItem("page-" + i,  imagePath, pageUrlList[i], chapter, i,pageUrlList.Count));

                }
                return mangaPageList;
            });
        }
    }


}
