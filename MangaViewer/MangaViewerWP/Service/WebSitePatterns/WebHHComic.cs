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
    public class WebHHComic : MangaPattern
    {
        string param = "*v";
        List<string> ServerList = new List<string>();


        public WebHHComic()
        {
            WEBSITEURL = "http://hhcomic.com/";
            CHARSET = "gb2312";

            for (int i = 0; i < 16; i++ )
            {
                ServerList.Add("");
            }
            ServerList[0] = "http://33.3348.net:9393/dm01/";
            ServerList[1] = "http://33.3348.net:9393/dm02/";
            ServerList[2] = "http://58.215.241.206:98/dm03/";
            ServerList[3] = "http://33.3348.net:9393/dm04/";
            ServerList[4] = "http://33.3348.net:9393/dm05/";
            ServerList[5] = "http://33.3348.net:9393/dm06/";
            ServerList[6] = "http://33.3348.net:9393/dm07/";
            ServerList[7] = "http://58.215.241.206:98/dm08/";
            ServerList[8] = "http://33.3348.net:9393/dm09/";
            ServerList[9] = "http://33.3348.net:9393/dm10/";
            ServerList[10] = "http://33.3348.net:9393/dm11/";
            ServerList[11] = "http://58.215.241.206:98/dm12/";
            ServerList[12] = "http://33.3348.net:9393/dm13/";
            ServerList[13] = "http://8.8.8.8:99/dm14/";
            ServerList[14] = "http://33.3348.net:9393/dm15/";
            ServerList[15] = "http://33.3348.net:9393/dm16/";
        }
        public override List<string> GetPageList(string firstPageUrl)
        {
            if (firstPageHtml == null)
            {
                firstPageHtml = GetHtml(firstPageUrl);
            }
            List<string> pageList = new List<string>();
            Regex re = new Regex("(?<=PicListUrl = \").+?(?=\")");
            var codeRe = re.Match(firstPageHtml);
            string key = "";
            string code = "";
            if (codeRe.Value != "")
            {
                code = codeRe.Value;
                key = "tahficoewrm";

            }
            else
            {
                re = new Regex("(?<=PicLlstUrl = \").+?(?=\")");
                code = re.Match(firstPageHtml).Groups[0].Value; 
                key = "tavzscoewrm";
            }

            int server = 0;
            Regex reServer = new Regex("(?<=s=)[0-9]{1,2}");
            server = Int32.Parse(reServer.Match(firstPageUrl).Value);
            pageList = decode(code,key,server);
            totalNum = pageList.Count;
            return pageList;
        }
        public List<string> decode(string code, string key, int server)
        {

            string result = "";
            char spliter = key.ToCharArray()[key.Length - 1];
            key = key.Substring(0, key.Length - 1);
            int i = 0;
            foreach (var k in key.ToCharArray())
            {
                code = code.Replace(k.ToString(), i.ToString());
                i = i + 1;
            }
            string[] codeList = code.Split(spliter);

            foreach (var c in codeList)
            {
                result = result + (char)Int32.Parse(c);
            }
            string[] resultArr = result.Split('|');

            List<string> resultList = new List<string>();
            string baseUrl = this.ServerList[server - 1];
            foreach (var p in resultArr)
            {
                string tmp = baseUrl.Trim('/') + p;
                resultList.Add(tmp);
            }

            return resultList;
        }
        public async override void GetImageByImageUrl(MangaPageItem pageItem, SaveType saveType = SaveType.Temp)
        {
            string imgUrl = pageItem.WebImageUrl;
            //Get Image 
            //To Do
            string fileRealPath = await DownloadImgPage(imgUrl, pageItem, saveType,WEBSITEURL);
            pageItem.SetImage(fileRealPath);
            return;
        }

        public override string GetImageUrl(string pageUrl, int nowNum)
        {
            return pageUrl;
        }

        public override List<TitleAndUrl> GetChapterList(string chapterUrl)
        {
            string html = GetHtml(chapterUrl);
            //Rex1  = <ul class="mh_fj" .+<li>.+</li></ul>
            Regex rGetUl = new Regex("<ul class=\"bl\">[\\s\\S]+?</ul>");
            //Rex2 = <li>.*?</li>
            html = rGetUl.Match(html).Value;

            List<TitleAndUrl> chapterList = new List<TitleAndUrl>();
            Regex rUrlAndTitle = new Regex("<a href=(.+?) .+?>(.+?)<");
            MatchCollection liList = rUrlAndTitle.Matches(html);
            foreach (Match m in liList)
            {
                string liStr = m.Value;
                string url = WEBSITEURL + rUrlAndTitle.Match(liStr).Groups[1].Value;
                string title = rUrlAndTitle.Match(liStr).Groups[2].Value;
                chapterList.Add(new TitleAndUrl(title, url));

            }


            return chapterList;
        }

        public override List<TitleAndUrl> GetNewMangaList(string html)
        {
            List<TitleAndUrl> newMangaList = new List<TitleAndUrl>();
            Regex rGetUl = new Regex("<div class=\"w100\">[\\s\\S]+?<div>");
            html = rGetUl.Match(html).Value;
            List<TitleAndUrl> chapterList = new List<TitleAndUrl>();
            Regex rUrlAndTitle = new Regex("<a href=\"(.+?)\".+?>([^<.]+?)</a>");
            MatchCollection liList = rUrlAndTitle.Matches(html);
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
            Regex rGetUl = new Regex("<div id=\"inhh\">[\\s\\S]+?</div>");
            MatchCollection liList = rGetUl.Matches(html);
            List<TitleAndUrl> chapterList = new List<TitleAndUrl>();
            Regex rUrlAndTitle = new Regex("<a href=\"(.+?)\".+?\"[\\s\\S]+?>[\\s\\S]+?<img src=\"([\\s\\S]+?)\" alt=\"(.+?)\"");

            foreach (Match m in liList)
            {
                string liStr = m.Value;
                string url = WEBSITEURL.Trim('/') + rUrlAndTitle.Match(liStr).Groups[1].Value;
                string title = rUrlAndTitle.Match(liStr).Groups[3].Value;
                string imageUrl = rUrlAndTitle.Match(liStr).Groups[2].Value;
                topMangaList.Add(new TitleAndUrl(title, url, imageUrl));

            }
            return topMangaList;
        }
    }
}
