using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace MangaViewer.Model
{
    [DataContractAttribute]
    public class TitleAndUrl
    {
        [DataMemberAttribute]
        public string Title { get; set; }
        [DataMemberAttribute]
        public string Url { get; set; }
        [DataMemberAttribute]
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
        public  const string SETTINGFOLDER = "Setting";
        public  const string SETTINGFILE = "AppSetting.set";
        public  const string MANGAFOLDER = "Manga";
    }


}
