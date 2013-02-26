using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using MangaViewer.Model;

namespace MangaViewer.Service
{
    public class MangaPattern
    {
        protected int startNum = 1;
        protected int totalNum = 1;
        protected string firstPageHtml = null;
/*
//  
//  Page
// 
*/
        public virtual List<string> GetPageList(string firstPageUrl) { return null; }
        public virtual string GetImageUrl(string pageUrl) { return null; }
        public virtual string GetImageByImageUrl(MangaPageItem page,SaveType saveType=SaveType.Temp) { return null; }
        public virtual int InitSomeArgs(string firstPageUrl) { return 0; }
      //public virtual void DownloadOnePage(string pageUrl,string folder,int nowPageNum) { return; }
        public virtual string GetHtml(string Url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            string UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/536.5 (KHTML, like Gecko) Chrome/19.0.1084.56 Safari/536.5";
            request.Headers["UserAgent"] = UserAgent;
            var myResponse = request.GetResponseAsync();
            using (StreamReader reader = new StreamReader(myResponse.Result.GetResponseStream()))
            {
                string html = reader.ReadToEnd();
                return html;
            }
        }
        public virtual int GetTotalNum(string html)
        {
            Regex r = new Regex("value=\"[0-9]+\"");
            MatchCollection m = r.Matches(html);
            r = new Regex("[0-9]+");
            m = r.Matches(m[m.Count-1].Value);
            return Int32.Parse(m[0].Value);
        }

/*
// 
//  Chapter
//  
*/
        public virtual List<TitleAndUrl> GetChapterList(string chapterUrl) { return null; }

/*
//  
//  Menu
// 
*/
        public virtual List<TitleAndUrl> GetTopMangaList() { return null; }
        public virtual List<TitleAndUrl> GetNewMangaList() { return null; }
    }


}
