using MangaViewer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;

namespace MangaViewer.Service
{
    public class WebComic131 : MangaPattern
    {
      
        public override List<string> GetPageList(string firstPageUrl)
        {
            if (firstPageHtml == null)
            {
                firstPageHtml = GetHtml(firstPageUrl);
            }
            totalNum = GetTotalNum(firstPageHtml);
            List<string> pageList = new List<string>();
            string baseUrl = firstPageUrl.Substring(0,firstPageUrl.LastIndexOf("/")+1);
            for (int i = startNum; i <= totalNum; i++)
            {
                pageList.Add(baseUrl + i.ToString() + ".html");
            }
            return pageList;
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

            string html = GetHtml(pageUrl);
            Regex reImg = new Regex("<img id=\"comicBigPic\" src=.+\" alt");
            Match result = reImg.Match(html);
            reImg = new Regex("src=.*\"");
            result = reImg.Match(result.Value);
            string strResult = result.Value.Replace("src=\"","").Replace("\"","");

            return strResult;

        }

        public override List<TitleAndUrl> GetChapterList(string chapterUrl)
        {
            //http://comic.131.com/content/shaonian/2104.html
            string html = GetHtml(chapterUrl);
            //Rex1  = <ul class="mh_fj" .+<li>.+</li></ul>
            Regex rGetUl = new Regex("<ul class=\"mh_fj\" .+<li>.+</li></ul>");
            //Rex2 = <li>.*?</li>
            html = rGetUl.Match(html).Value;
            Regex rGetLi = new Regex("<li>.*?</li>");
            MatchCollection liList = rGetLi.Matches(html);
            List<TitleAndUrl> chapterList = new List<TitleAndUrl>();
            Regex rUrl = new Regex("(?<=href=\").+?(?=\")");
            Regex rTitle = new Regex("(?<=\">).+?(?=<)");
            foreach (Match m in liList)
            {
                string liStr = m.Value;
                chapterList.Add(new TitleAndUrl(rTitle.Match(liStr).Value, rUrl.Match(liStr).Value));

            }


            return chapterList;
        }

        public override List<TitleAndUrl> GetTopMangaList(string html)
        {
            List<TitleAndUrl> topMangaList = new List<TitleAndUrl>();
            Regex rUl = new Regex("<ul class=\"xqkd\">[\\s\\S]*</ul>");
            string result = rUl.Match(html).Value;
            Regex rLi = new Regex("<li>.*?</li>");
            MatchCollection mCollection = rLi.Matches(result);

            Regex rUrl = new Regex("(?<=href=\").*?(?=\")");
            Regex rTitle = new Regex("(?<=\"_blank\">).*?(?=</a>)");
            foreach (Match m in mCollection)
            {
                string url = rUrl.Match(m.Value).Value;
                string title = rTitle.Match(m.Value).Value;
                topMangaList.Add(new TitleAndUrl(title,url));
            }
            return topMangaList;
        }
    }
}
