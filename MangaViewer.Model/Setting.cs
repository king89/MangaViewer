using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MangaViewer.Model
{
    public class Setting : ObservableObject
    {
        [IgnoreDataMember]
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
                if (fm.MenuItem.Url == menu.Url)
                {
                    return fm;
                }
            }
            return null;
        }

        public bool AddFavouriteMenu(MangaMenuItem menu)
        {
            try
            {
                FavouriteMangaItem fMenu = new FavouriteMangaItem(menu, WebSite);
                FavouriteMenu.Add(fMenu);
                RaisePropertyChanged(()=>FavouriteMenu);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }


        }
        public  void RemoveFavouriteMenu(MangaMenuItem menu)
        {
            FavouriteMangaItem fMenu = GetFavouriteItem(menu);
            FavouriteMenu.Remove(fMenu);
            RaisePropertyChanged(() => FavouriteMenu);
        }

        [CollectionDataContract]
        private List<TitleAndUrl> _favouriteMenuCopy;


    }
}
