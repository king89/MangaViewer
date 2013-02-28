using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaViewer.Model
{
    public class FavouriteMangaItem : ObservableObject
    {
        public MangaMenuItem menuItem;
        public DateTime favouriteDate;
        public WebSiteEnum webSite;
        public int clickTimes;
    }
}
