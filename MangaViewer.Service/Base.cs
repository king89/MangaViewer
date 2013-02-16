using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MangaViewer.Service
{
    public class MangaPattern
    {
        protected int startNum = 1;
        protected int totalNum = 1;
        protected string firstPageHtml = null;
        /// <summary>
        ///   Page
        /// </summary>
        public virtual List<string> GetPageList(string firstPageUrl) { return null; }
        public virtual string GetImageUrl(string pageUrl) { return null; }
        public virtual int InitSomeArgs(string firstPageUrl) { return 0; }
        public virtual void DownloadOnePage(string pageUrl,string folder,int nowPageNum) { return; }

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
        /// <summary>
        ///   Chapter
        /// </summary>
        public virtual List<string> GetChapterList() { return null; }

        /// <summary>
        ///   Menu
        /// </summary>
        public virtual List<string> GetTopMangaList() { return null; }
        public virtual List<string> GetNewMangaList() { return null; }
    }

   
}
