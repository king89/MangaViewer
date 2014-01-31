using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaViewer.Model
{
    public enum SaveType
    {
        Temp = 0,
        Local = 1
    }

    public enum WebSiteEnum
    {

        IManhua = 0,
        HHComic = 1,
        Local,
    }

    public static class MenuType
    {
        public static readonly string TopManga = "TopManga";
        public static readonly string NewManga = "NewManga";
        public static readonly string MyFavourite = "MyFavourite";

    }


}
