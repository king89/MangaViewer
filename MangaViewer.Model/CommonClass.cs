using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaViewer.Model
{
    public class TitleAndUrl
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string ImagePath { get; set; }

        public TitleAndUrl(string title, string url,string imagePath ="")
        {
            Title = title;
            Url = url;
            ImagePath = imagePath;
        }
    }
}
