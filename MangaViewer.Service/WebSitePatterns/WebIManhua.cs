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
    public class WebIManhua : MangaPattern
    {

        string param = "p";
        string imagePrefix = ""; //imanhua_ ,  JOJO_, no prefix  
        string imageFormat = "";      //jpg,png
        string imageUrl = "http://t5.mangafiles.com/Files/Images";
        public override List<string> GetPageList(string firstPageUrl)
        {
            if (firstPageHtml == null)
            {
                firstPageHtml = GetFirstPageHtml(firstPageUrl);
            }
            totalNum = GetTotalNum(firstPageHtml);
            List<string> pageList = new List<string>();
            for (int i = startNum; i <= totalNum; i++ )
            {
                pageList.Add(firstPageUrl + "?" + param + "=" + i.ToString());
            }
            return pageList;
        }

        public override Uri GetImageByImageUrl(MangaPageItem page, SaveType saveType = SaveType.Temp)
        {
            string imgUrl = GetImageUrl(page.PageUrl);
            //Get Image
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imgUrl);
            CookieContainer cc = new CookieContainer();
            request.CookieContainer = cc;
            string UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/536.5 (KHTML, like Gecko) Chrome/19.0.1084.56 Safari/536.5";
            string Referer = "http://www.imanhua.com/comic/612/list_78542.html";
            request.Headers["UserAgent"] = UserAgent;
            request.Headers["Referer"] = Referer;
            //Cookie myc = new Cookie("visited", "4", "/", "www.imanhua.com");
            //cc.Add(myc);
            request.CookieContainer = cc;
            var res = request.GetResponseAsync();
            HttpWebResponse myResponse = (HttpWebResponse)res.Result; 

            return new Uri(""); 
        }

        public override string GetImageUrl(string pageUrl)
        {
            if (firstPageHtml == null)
            {
                firstPageHtml = GetFirstPageHtml(pageUrl);
            }
            int nowNum = -1;
            Int32.TryParse(pageUrl.Substring(pageUrl.LastIndexOf("=") + 1), out nowNum);
            Regex r = new Regex("\"files\":\\[.+\"\\]");
            Match m = r.Match(firstPageHtml);
            string result = m.Value;
            result = result.Replace("\"files\":[","").Replace("]","");
            string[] resultList = result.Split(',');

            Regex reFirst = new Regex("/[0-9]+/");
            Regex reSec = new Regex("list_[0-9]+");

            string firstNum = reFirst.Match(pageUrl).Value;
            firstNum = firstNum.Trim("/".ToCharArray());
            string SecNum = reSec.Match(pageUrl).Value;
            SecNum = SecNum.Trim("list_".ToCharArray());

            return imageUrl.TrimEnd('/') + '/' + firstNum + '/' + SecNum + '/' + resultList[nowNum];
        }


    }
}
