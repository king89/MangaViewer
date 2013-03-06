using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
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

    public static class Constant
    {
        public static string settingFolder = "Setting\\";
        public static string settingFile = "AppSetting.set";
    }


}
