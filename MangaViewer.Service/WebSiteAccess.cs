using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MangaViewer.Service
{
    public static class WebSiteAccess
    {
        static Dictionary<WebSiteEnum, string> WebSiteList = null;
        static WebSiteEnum WebSiteType;
        WebSiteAccess()
        {
            WebSiteList.Add(WebSiteEnum.IManhua,"WebIManhua");
            WebSiteList.Add(WebSiteEnum.Comic131,"WebComic131");
        }
        public static MangaPattern GetMangaPatternInstance(WebSiteEnum type)
        {
            WebSiteType = type;
            string factoryType = "MangaViewer.Service." + WebSiteList[WebSiteType];
            Type t = Type.GetType(factoryType);
            return Activator.CreateInstance(t) as MangaPattern;

        }
    }

    public enum WebSiteEnum
    {

        IManhua=0,

        Comic131=1
    }
}
