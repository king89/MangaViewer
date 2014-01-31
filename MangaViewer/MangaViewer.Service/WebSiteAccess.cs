using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using MangaViewer.Model;

namespace MangaViewer.Service
{
    public class WebSiteAccess
    {
        static Dictionary<WebSiteEnum, string> WebSiteList = new Dictionary<WebSiteEnum, string>(){
        { WebSiteEnum.IManhua, "WebIManhua" },
        {WebSiteEnum.HHComic,"WebHHComic"},
        {WebSiteEnum.Local,"WebLocal"}
        };
        static WebSiteEnum WebSiteType;
        WebSiteAccess()
        {
            //可以写到配置
            //WebSiteList.Add(WebSiteEnum.IManhua,"WebIManhua");
            //WebSiteList.Add(WebSiteEnum.Comic131,"WebComic131");
        }
        public static MangaPattern GetMangaPatternInstance(WebSiteEnum type)
        {
            WebSiteType = type;
            string factoryType = "MangaViewer.Service." + WebSiteList[WebSiteType];
            Type t = Type.GetType(factoryType);
            return Activator.CreateInstance(t) as MangaPattern;

        }
    }



}
