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
        public override List<string> GetPageList(string firstPageUrl)
        {
            if (firstPageHtml == null)
            {
                firstPageHtml = GetFirstPageHtml(firstPageUrl);
            }
            totalNum = GetTotalNum(firstPageHtml);
            List<string> pageList = new List<string>();
            for (int i = startNum; i <= totalNum; i++)
            {
                //pageList.Add(firstPageUrl + "?" + param + "=" + i.ToString());
            }
            return pageList;
        }

        public override Uri GetImageByImageUrl(MangaPageItem page, SaveType saveType = SaveType.Temp)
        {
            string imgUrl = GetImageUrl(page.PageUrl);
            //Get Image
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imgUrl);
            CookieContainer cc = new CookieContainer();
            var res = request.GetResponseAsync();
            HttpWebResponse myResponse = (HttpWebResponse)res.Result;
            string folder = page.Chapter.Menu.Title + "\\" + page.Chapter.Title + "\\" + page.Title;
            var res2 = FileService.SaveFile(myResponse.GetResponseStream(), (int)myResponse.ContentLength, folder, page.PageNum.ToString(),saveType);
            string path = res2.Result;
            return new Uri(path);
        }

        public override string GetImageUrl(string pageUrl)
        {
            if (firstPageHtml == null)
            {
                firstPageHtml = GetFirstPageHtml(pageUrl);
            }

            Regex reImg = new Regex("<img id=\"comicBigPic\" src=.+\" alt");
            Match result = reImg.Match(firstPageHtml);
            reImg = new Regex("src=.*\"");
            result = reImg.Match(result.Value);
            string strResult = result.Value.Replace("src=\"","").Replace("\"","");

            return strResult;

        }

    }
}
