using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using MangaViewer.Model;
using System.Net.Http;

namespace MangaViewer.Service
{
    public class MangaPattern
    {
        protected int startNum = 1;
        protected int totalNum = 1;
        protected string firstPageHtml = null;
        public string WEBSITEURL = "";
        public string WEBSEARCHURL = "";
        public string CHARSET = "utf8";
/*
//  
//  Page
// 
*/
        public virtual List<string> GetPageList(string firstPageUrl) { return null; }
        public virtual string GetImageUrl(string pageUrl) { return null; }
        public virtual string GetImageUrl(string pageUrl, int nowPage) { return null; }
        public virtual void GetImageByImageUrl(MangaPageItem page,SaveType saveType=SaveType.Temp) { return ; }
        public virtual int InitSomeArgs(string firstPageUrl) { return 0; }
      //public virtual void DownloadOnePage(string pageUrl,string folder,int nowPageNum) { return; }
        public virtual string GetHtml(string Url)
        {
            try
            {

                HttpClient client = new HttpClient();
                string UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/536.5 (KHTML, like Gecko) Chrome/19.0.1084.56 Safari/536.5";
                client.DefaultRequestHeaders.UserAgent.TryParseAdd(UserAgent);
                HttpResponseMessage response = client.GetAsync(Url).Result;
                response.EnsureSuccessStatusCode();
                
                var responseBody = response.Content.ReadAsStreamAsync().Result;
                Encoding encode = null;
                if (CHARSET == "gb2312")
                {
#if WP

                     encode = DBCSCodePage.DBCSEncoding.GetDBCSEncoding("gb2312");
#elif Win8
                    encode = Encoding.UTF8;
#endif
                } 
                else
                {
                    encode = Encoding.UTF8;
                }
                
                using (StreamReader reader = new StreamReader(responseBody, encode))
                {
                    string html = reader.ReadToEnd();
                    return html;
                }
            }
            catch (System.Exception ex)
            {
                return "";
            }
        }
        public virtual int GetTotalNum(string html)
        {
            Regex r = new Regex("value=\"[0-9]+\"");
            MatchCollection m = r.Matches(html);
            r = new Regex("[0-9]+");
            if (m.Count > 0)
            {
                m = r.Matches(m[m.Count - 1].Value);
                return Int32.Parse(m[0].Value);
            }
            else
            {
                return 0;
            }
            
        }

        public async virtual Task<string> DownloadImgPage(string imgUrl, MangaPageItem pageItem, SaveType saveType,string refer = "")
        {
            HttpClient client = new HttpClient();
            string UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/536.5 (KHTML, like Gecko) Chrome/19.0.1084.56 Safari/536.5";

            client.DefaultRequestHeaders.UserAgent.TryParseAdd(UserAgent);
            if (refer != "")
            {
                client.DefaultRequestHeaders.Referrer = new Uri(refer);
            }
            else
            {
                client.DefaultRequestHeaders.Referrer = new Uri(pageItem.PageUrl);
            }
            // Call asynchronous network methods in a try/catch block to handle exceptions 
            try
            {
                HttpResponseMessage response = await client.GetAsync(imgUrl);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStreamAsync();
                // Above three lines can be replaced with new helper method below 
                // string responseBody = await client.GetStringAsync(uri);
                string folderName = Constant.MANGAFOLDER + this.GetType().Name + "_" + pageItem.Chapter.Menu.Title + "_" + pageItem.Chapter.Title;
                string fileName = System.IO.Path.GetFileName(imgUrl);
                string fileRealPath = await FileService.SaveFileInTemp(folderName, fileName, responseBody);
                responseBody.Close();
                return fileRealPath;
            }
            catch (Exception e)
            {
                return "";
            }
            
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
        public virtual List<TitleAndUrl> GetTopMangaList(string html) { return null; }
        public virtual List<TitleAndUrl> GetNewMangaList(string html) { return null; }
        public virtual List<TitleAndUrl> GetSearchingList(string queryText,int pageNum) { return null; }
    }


}
