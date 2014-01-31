﻿using System;
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
        public virtual string GetImageByImageUrl(MangaPageItem page,SaveType saveType=SaveType.Temp) { return null; }
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
                     encode = DBCSCodePage.DBCSEncoding.GetDBCSEncoding("gb2312");
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
        public virtual List<TitleAndUrl> GetTopMangaList(string html) { return null; }
        public virtual List<TitleAndUrl> GetNewMangaList(string html) { return null; }
        public virtual List<TitleAndUrl> GetSearchingList(string queryText,int pageNum) { return null; }
    }


}
