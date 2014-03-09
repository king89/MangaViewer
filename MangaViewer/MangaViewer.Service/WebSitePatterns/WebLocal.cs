using MangaViewer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MangaViewer.Service
{
    public class WebLocal : MangaPattern
    {
        public override List<string> GetPageList(string firstPageUrl)
        {
            List<string> pageList = new List<string>();
            for (int i = 0; i < 20; i++)
            {
                pageList.Add("http://localhost:8800/");
            }

            Sleep();
            return pageList;
        }

        void Sleep()
        {
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            int dur = 2;
            while ((end - start).Seconds < dur)
            {
                end = DateTime.Now;
            }

        }
        public async override void GetImageByImageUrl(MangaPageItem pageItem, SaveType saveType = SaveType.Temp)
        {
            string imgUrl = pageItem.WebImageUrl;
            //Get Image 
            //To Do
            string fileRealPath = "/Assets/ApplicationIcon.png";
            pageItem.SetImage(fileRealPath);
            return;
        }

        public override string GetImageUrl(string pageUrl, int nowNum)
        {
            string strResult = "http://localhost:8800/image/test.jpg";
            return strResult;
        }

        public override string GetImageUrl(string pageUrl)
        {

            string strResult = "http://localhost:8800/image/test.jpg";
            Sleep();
            return strResult;

        }

        public override List<TitleAndUrl> GetChapterList(string chapterUrl)
        {
            List<TitleAndUrl> chapterList = new List<TitleAndUrl>();
            for (int i = 100; i > 0; i--)
            {
                chapterList.Add(new TitleAndUrl("第" + i.ToString() + "话", "http://comic.131.com/content/2104/191221/1.html"));
            }
            Sleep();
            return chapterList;
        }

        public override List<TitleAndUrl> GetTopMangaList(string html)
        {
            List<TitleAndUrl> newMenuList = new List<TitleAndUrl>();
            for (int i = 100; i > 0; i--)
            {
                newMenuList.Add(new TitleAndUrl("第" + i.ToString() + "话", "http://comic.131.com/content/2104/191221/1.html"));
            }
            Sleep();
            return newMenuList;
            //return base.GetNewMangaList();
        }


        public override List<TitleAndUrl> GetSearchingList(string queryText, int pageNum)
        {
            return this.GetTopMangaList(queryText);
        }
    }
}
