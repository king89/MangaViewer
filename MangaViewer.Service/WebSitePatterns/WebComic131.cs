using MangaViewer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;

namespace MangaViewer.Service
{
    public class WebComic131 : MangaPattern
    {
        private static object sync = new object();  
        public override List<string> GetPageList(string firstPageUrl)
        {
            lock (sync)
            {
                if (firstPageHtml == null)
                {
                    firstPageHtml = GetPageHtml(firstPageUrl);
                }
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

            string html = GetPageHtml(pageUrl);
            Regex reImg = new Regex("<img id=\"comicBigPic\" src=.+\" alt");
            Match result = reImg.Match(html);
            reImg = new Regex("src=.*\"");
            result = reImg.Match(result.Value);
            string strResult = result.Value.Replace("src=\"","").Replace("\"","");

            return strResult;

        }

    }
}
