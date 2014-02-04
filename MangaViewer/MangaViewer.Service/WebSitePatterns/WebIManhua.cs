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
            if (m.Value != "")
            {
                string result = m.Value;
                deserializedProduct = JsonConvert.DeserializeObject<ImanhuaInfo>(result);
            }
            else
            {
                r = new Regex("(?<=}\\().+?(?=\\)\\))");
                m = r.Match(firstPageHtml);
                if (m.Value != "")
                {
                    //从 js 函数取出 cinfo 代码 部分
                    r = new Regex("(?<!\"'[^,]+),(?![^,]+\")");
                    string result = m.Value;
                    string[] vars = r.Split(result);
                    string cinfo = vars[0];
                    string isdecimal = vars[1];
                    string[] names = vars[3].Replace(".split('|')","").Trim('\'').Split('|');
                    //解密cinfo
                    r = new Regex("[0-9a-zA-Z]{1,3}");
                    MatchCInfo.names = names;
                    MatchCInfo.isdecimal = Int32.Parse(isdecimal);
                    cinfo = r.Replace(cinfo, new MatchEvaluator(MatchCInfo.Change));

                    r = new Regex("(?<=var cInfo=){.+?}");
                    m = r.Match(cinfo);
                    result = m.Value;
                    deserializedProduct = JsonConvert.DeserializeObject<ImanhuaInfo>(result);
                }
            }


            return pageList;
        }

        class MatchCInfo
        {
            public static string[] names = null;
            public static int isdecimal = 10;
            public static string Change(Match m)
            {
                // Get the matched string.
                string x = m.ToString();
                return names[GetInt(x, isdecimal)] == "" ? "0" : names[GetInt(x, isdecimal)];
            }
            private static int GetInt(string s, int isDecimal)
            {
                char[] aList = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                Dictionary<string, int> aDict = new Dictionary<string, int>();
                for (int i = 0; i < aList.Length; i++)
                {
                    aDict[aList[i].ToString()] = i;
                }
                if (isDecimal == 10)
                {
                    return Int32.Parse(s);
                }
                else
                {
                    char[] sArrary = s.ToCharArray();
                    double total = 0;
                    int index = 0;
                    for (int i = s.Length - 1; i >= 0; i--)
                    {
                        total = total + (Math.Pow(isDecimal, index) * aDict[sArrary[i].ToString()]);
                        index += 1;
                    }
                    return (int)total;
                }
            }
        }
        

        public async override void GetImageByImageUrl(MangaPageItem pageItem, SaveType saveType = SaveType.Temp)
        {
            string imgUrl = pageItem.WebImageUrl;
            //Get Image 
            //To Do
            string fileRealPath = await DownloadImgPage(imgUrl, pageItem, saveType);
            pageItem.SetImage(fileRealPath);
            return ; 
        }

        public override string GetImageUrl(string pageUrl,int nowNum)
        {

            return imageUrl.TrimEnd('/') + '/' + deserializedProduct.bid + '/' + deserializedProduct.cid + '/' + deserializedProduct.files[nowNum-1];
        }

        
        public override List<TitleAndUrl> GetChapterList(string chapterUrl)
        {
            //http://comic.131.com/content/shaonian/2104.html
            string html = GetHtml(chapterUrl);
            //Rex1  = <ul class="mh_fj" .+<li>.+</li></ul>
            Regex rGetUl = new Regex("id=[\"']subBookList[\"']>.+?</ul>");
            //Rex2 = <li>.*?</li>
            html = rGetUl.Match(html).Value;
            Regex rGetLi = new Regex("<li>.+?</li>");
            MatchCollection liList = rGetLi.Matches(html);
            List<TitleAndUrl> chapterList = new List<TitleAndUrl>();
            Regex rUrlAndTitle = new Regex("<a href=\"(.+?)\".+?>(.+?)<");

            foreach (Match m in liList)
            {
                string liStr = m.Value;
                string url = WEBSITEURL + rUrlAndTitle.Match(liStr).Groups[1].Value;
                string title = rUrlAndTitle.Match(liStr).Groups[2].Value;
                chapterList.Add(new TitleAndUrl(title,url));

            }


            return chapterList;
        }

        public override List<TitleAndUrl> GetNewMangaList(string html)
        {
            List<TitleAndUrl> newMangaList = new List<TitleAndUrl>();
            Regex rGetUl = new Regex("<ul class=\"newUpdate\".+</ul>");
            html = rGetUl.Match(html).Value;
            Regex rGetLi = new Regex("<li>.+?</li>");
            MatchCollection liList = rGetLi.Matches(html);
            List<TitleAndUrl> chapterList = new List<TitleAndUrl>();
            Regex rUrlAndTitle = new Regex("<a href=\"(.+?)\".+?>(.+?)</a>");

            foreach (Match m in liList)
            {
                string liStr = m.Value;
                string url = WEBSITEURL.Trim('/') + rUrlAndTitle.Match(liStr).Groups[1].Value;
                string title = rUrlAndTitle.Match(liStr).Groups[2].Value;
                newMangaList.Add(new TitleAndUrl(title, url));

            }
            return newMangaList;
        }

        public override List<TitleAndUrl> GetTopMangaList(string html)
        {
            List<TitleAndUrl> topMangaList = new List<TitleAndUrl>();
            Regex rGetUl = new Regex("id=[\"']comicList[\"']>.+?</ul>");
            html = rGetUl.Match(html).Value;
            Regex rGetLi = new Regex("<li>.+?</li>");
            MatchCollection liList = rGetLi.Matches(html);
            List<TitleAndUrl> chapterList = new List<TitleAndUrl>();
            Regex rUrlAndTitle = new Regex("<a href=\"(.+?)\".+?title=\"(.+?)\"><img src=\"(.+?)\"");

            foreach (Match m in liList)
            {
                string liStr = m.Value;
                string url = WEBSITEURL.Trim('/') + rUrlAndTitle.Match(liStr).Groups[1].Value;
                string title = rUrlAndTitle.Match(liStr).Groups[2].Value;
                string imageUrl = rUrlAndTitle.Match(liStr).Groups[3].Value;
                topMangaList.Add(new TitleAndUrl(title, url,imageUrl));

            }
            return topMangaList;
        }
    }
}
