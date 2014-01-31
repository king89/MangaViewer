using MangaViewer.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace MangaViewer.Service
{
    public class WebIManhua : MangaPattern
    {
        public class ImanhuaInfo
        {
            public string bname { get; set; }
            public int finished { get; set; }
            public string burl { get; set; }
            public string cname { get; set; }
            public int cid { get; set; }
            public int bid { get; set; }
            public int len { get; set; }
            public List<string> files { get; set; }

        }

        public WebIManhua()
        {
            WEBSITEURL = "http://www.imanhua.com/";
            CHARSET = "gb2312";
        }
        string param = "p";
        string imagePrefix = ""; //imanhua_ ,  JOJO_, no prefix  
        string imageFormat = "";      //jpg,png
        string imageUrl = "http://t5.mangafiles.com/Files/Images";
        ImanhuaInfo deserializedProduct = null;

        public override List<string> GetPageList(string firstPageUrl)
        {
            if (firstPageHtml == null)
            {
                firstPageHtml = GetHtml(firstPageUrl);
            }
            totalNum = GetTotalNum(firstPageHtml);
            List<string> pageList = new List<string>();
            for (int i = startNum; i <= totalNum; i++ )
            {
                pageList.Add(firstPageUrl + "?" + param + "=" + i.ToString());
            }

            Regex r = new Regex("(?<=var cInfo=){.+?}");
            Match m = r.Match(firstPageHtml);
            string result = m.Value;

            deserializedProduct = JsonConvert.DeserializeObject<ImanhuaInfo>(result);

            return pageList;
        }

        public override string GetImageByImageUrl(MangaPageItem page, SaveType saveType = SaveType.Temp)
        {
            string imgUrl = GetImageUrl(page.PageUrl);
            //Get Image 
            //To Do
            return ""; 
        }

        public override string GetImageUrl(string pageUrl)
        {
            if (firstPageHtml == null)
            {
                firstPageHtml = GetHtml(pageUrl);
            }
            int nowNum = -1;


            return imageUrl.TrimEnd('/') + '/' + deserializedProduct.bid + '/' + deserializedProduct.cid + '/' + deserializedProduct.files[nowNum];
        }

        public override List<TitleAndUrl> GetChapterList(string chapterUrl)
        {
            //http://comic.131.com/content/shaonian/2104.html
            string html = GetHtml(chapterUrl);
            //Rex1  = <ul class="mh_fj" .+<li>.+</li></ul>
            Regex rGetUl = new Regex("<ul class=\"newUpdate\".+</ul>");
            //Rex2 = <li>.*?</li>
            html = rGetUl.Match(html).Value;
            Regex rGetLi = new Regex("<li>.+?</li>");
            MatchCollection liList = rGetLi.Matches(html);
            List<TitleAndUrl> chapterList = new List<TitleAndUrl>();
            Regex rUrlAndTitle = new Regex("<a href=\"(.+?)\".+?>(.+?)</a>");

            foreach (Match m in liList)
            {
                string liStr = m.Value;
                string url = rUrlAndTitle.Match(liStr).Groups[1].Value;
                string title = rUrlAndTitle.Match(liStr).Groups[2].Value;
                chapterList.Add(new TitleAndUrl(title,url));

            }


            return chapterList;
        }

        public override List<TitleAndUrl> GetNewMangaList(string html)
        {
            List<TitleAndUrl> topMangaList = new List<TitleAndUrl>();
            Regex rGetUl = new Regex("<ul class=\"newUpdate\".+</ul>");
            html = rGetUl.Match(html).Value;
            Regex rGetLi = new Regex("<li>.+?</li>");
            MatchCollection liList = rGetLi.Matches(html);
            List<TitleAndUrl> chapterList = new List<TitleAndUrl>();
            Regex rUrlAndTitle = new Regex("<a href=\"(.+?)\".+?>(.+?)</a>");

            foreach (Match m in liList)
            {
                string liStr = m.Value;
                string url = rUrlAndTitle.Match(liStr).Groups[1].Value;
                string title = rUrlAndTitle.Match(liStr).Groups[2].Value;
                topMangaList.Add(new TitleAndUrl(title, url));

            }
            return topMangaList;
        }


    }
}
