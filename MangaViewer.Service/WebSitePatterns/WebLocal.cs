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
        public override string GetImageByImageUrl(MangaPageItem page, SaveType saveType = SaveType.Temp)
        {
            string imgUrl = GetImageUrl(page.PageUrl);
            return imgUrl;

            ////Get Image
            //string extention = imgUrl.Substring(imgUrl.LastIndexOf("."));
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imgUrl);
            //CookieContainer cc = new CookieContainer();
            //var res = request.GetResponseAsync();
            //HttpWebResponse myResponse = (HttpWebResponse)res.Result;
            //string folder = page.Chapter.Menu.Title + "\\" + page.Chapter.Title + "\\" ;
            //var res2 = FileService.SaveFile(myResponse.GetResponseStream(), (int)myResponse.ContentLength, folder, page.PageNum + extention,saveType);
            //string path = res2;
            //return path;
        }

        public override string GetImageUrl(string pageUrl)
        {

            string strResult = "http://localhost:8800/image/hub/Hub-Product.jpg";
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

        public override List<TitleAndUrl> GetTopMangaList()
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

    }
}
