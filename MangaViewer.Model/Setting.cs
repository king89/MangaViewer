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
        [IgnoreDataMember]
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

        [DataMember]
        private List<TitleAndUrl> _favouriteMenuCopy;
        [DataMember]
        public List<TitleAndUrl> FavouriteMenuCopy
        {
            get
            {
                return _favouriteMenuCopy;
            }
            set
            {
                _favouriteMenuCopy = value;
            }
        }

        //private List<TitleAndUrl> GetMenuCopy()
        //{

        //}
        [OnSerializing]
        public void OnSerializing(StreamingContext context)
        {
            _favouriteMenuCopy = new List<TitleAndUrl>();
            foreach(FavouriteMangaItem fmi in _favouriteMenu)
            {
             _favouriteMenuCopy.Add(new TitleAndUrl(fmi.MenuItem.Title,fmi.MenuItem.Url,fmi.MenuItem.GetImagePath()));
            }
        }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            if (_favouriteMenuCopy != null)
            {

                _favouriteMenu = new List<FavouriteMangaItem>();
                foreach (TitleAndUrl fmi in _favouriteMenuCopy)
                {
                    MangaMenuItem menu = new MangaMenuItem(fmi);

                    _favouriteMenu.Add(new FavouriteMangaItem(menu, this.WebSite));
                }
            }
        }
    }
}
