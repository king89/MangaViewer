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
        public virtual string GetFirstPageHtml(string firstPageUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(firstPageUrl);
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
        public virtual List<string> GetChapterList() { return null; }

/*
//  
//  Menu
// 
*/
        public virtual List<string> GetTopMangaList() { return null; }
        public virtual List<string> GetNewMangaList() { return null; }
    }


}
