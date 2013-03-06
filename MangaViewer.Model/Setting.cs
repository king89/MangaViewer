using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaViewer.Model
{
    public class Setting : ObservableObject
    {
        private List<FavouriteMangaItem> _favouriteMenu = null;
        public List<FavouriteMangaItem> FavouriteMenu
        {
            get 
            {
                if (_favouriteMenu == null)
                {
                    _favouriteMenu = new List<FavouriteMangaItem>(); 
                }
                return _favouriteMenu; 
            }
            set
            {
                _favouriteMenu = value;
                RaisePropertyChanged(()=>FavouriteMenu);
            }
        }
        private WebSiteEnum _webSite = WebSiteEnum.Local;
        public WebSiteEnum WebSite
        {
            get { return _webSite; }
            set 
            {
                _webSite = value;
                RaisePropertyChanged(() => WebSite);
            }
        }

        public FavouriteMangaItem GetFavouriteItem(MangaMenuItem menu)
        {
            foreach (FavouriteMangaItem fm in FavouriteMenu)
            {
                if (fm.menuItem.Url == menu.Url)
                {
                    return fm;
                }
            }
            return null;
        }
    }
}
