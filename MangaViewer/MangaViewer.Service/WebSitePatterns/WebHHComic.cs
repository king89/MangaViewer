using MangaViewer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MangaViewer.Service.WebSitePatterns
{
    public class WebHHComic : MangaPattern
    {
        public WebHHComic()
        {
            WEBSITEURL = "http://hhcomic.com/";
            WEBSEARCHURL = "http://hhcomic.com/Search.aspx?all=";
        }
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
            Regex reImg = new Regex("(?<=<script id=\"imgjs\".*?img=)[\\s\\S]*?(?=\")");
            string result = reImg.Match(html).Value;
            if (result == string.Empty)
            {
                reImg = new Regex("(?<=<img id=\"comicBigPic\" src=\")[\\s\\S]+?(?=\")");
                result = reImg.Match(html).Value;
            }
            if (result == string.Empty)
            {
            }
            return result;

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
            Regex rUl = new Regex("<ul class=\"xqkd\">[\\s\\S]*?</ul>");
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

        public override List<TitleAndUrl> GetNewMangaList(string html)
        {
            Regex rUl = new Regex("(?<=<em class=\"tc\">)[\\s\\S]*?(?=</div>)");
            string result = rUl.Match(html).Value;
            rUl = new Regex("<ul>[\\s\\S]*?</ul>");
            result = rUl.Match(result).Value;
            Regex rLi = new Regex("<li>[\\s\\S]*?</li>");
            MatchCollection mCollection = rLi.Matches(result);
            List<TitleAndUrl> newMangeList = new List<TitleAndUrl>();

            Regex rUrl = new Regex("(?<=href=\")[\\s\\S]*?(?=\" target=[\\s\\S]*?<img)");
            Regex rUrlPage = new Regex("(?<=:<a href=\")[\\s\\S]*?(?=\" target)");
            Regex rTitle = new Regex("(?<=<strong>)[\\s\\S]*?(?=</strong>)");
            Regex rTitlePage = new Regex("(?<=<img[\\s\\S]*?title=\")[\\s\\S]*?(?=\"><strong>)");
            Regex rImg = new Regex("(?<=<img src=\")[\\s\\S]*?(?=\")");
            foreach (Match m in mCollection)
            {
                string title = rTitlePage.Match(m.Value).Value;
                string url = WEBSITEURL.TrimEnd('/') + rUrl.Match(m.Value).Value;
                string img = rImg.Match(m.Value).Value;
                newMangeList.Add(new TitleAndUrl(title,url,img));
            }
            return newMangeList;
        }

        public override List<TitleAndUrl> GetSearchingList(string queryText,int pageNum = 1)
        {
            string pageUrl = this.WEBSEARCHURL + queryText+"&page="+pageNum.ToString();
            string html = GetHtml(pageUrl);
            Regex rUl = new Regex("(?<=</h4>\\s*?<ul>)[\\s\\S]*?(?=</ul>)");
            string result = rUl.Match(html).Value;
            Regex rLi = new Regex("<li>[\\s\\S]*?</li>");
            MatchCollection mCollection = rLi.Matches(result);

            List<TitleAndUrl> newMangeList = new List<TitleAndUrl>();

            Regex rUrl = new Regex("(?<=<a href=\")[\\s\\S]*?(?=\"[\\s\\S]*?<img)");
            Regex rTitle = new Regex("(?<=<p><a[\\s\\S]*?>)[\\s\\S]*?(?=</a>)");
            Regex rImg = new Regex("(?<=src=\")[\\s\\S]*?(?=\")");
            foreach (Match m in mCollection)
            {
                string title = rTitle.Match(m.Value).Value;
                string url = rUrl.Match(m.Value).Value;
                string img = rImg.Match(m.Value).Value;
                newMangeList.Add(new TitleAndUrl(title, url, img));
            }
            return newMangeList;   
        }
    }
}
