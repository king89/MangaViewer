using MangaViewer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
                    mangaPageList.Add(new MangaPageItem("page-" + i, i.ToString(), imagePath, pageUrlList[i], chapter.Title, chapter.Menu));

                }
                return mangaPageList;
            });
        }
    }


}
