using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MangaViewer.Model
{
    [DataContract]
    public class FavouriteMangaItem : ModelBase
    {
        public FavouriteMangaItem(MangaMenuItem menu, WebSiteEnum webSite)
        {
            MenuItem = menu;
            favouriteDate = DateTime.Now;
            this.webSite = webSite;
            clickTimes = 0;
        }
        [IgnoreDataMember]
        private MangaMenuItem _menuItem;
        [DataMember]
        public MangaMenuItem MenuItem
        {
            get
            {
                return _menuItem;
            }
            set
            {
                _menuItem = value;
                RaisePropertyChanged(() => MenuItem);
            }
        }
        [DataMember]
        public DateTime favouriteDate;
        [DataMember]
        public WebSiteEnum webSite;
        [DataMember]
        public int clickTimes;

        //[DataMember]
        //private TitleAndUrl _favouriteMenuCopy;
        //[DataMember]
        //public TitleAndUrl FavouriteMenuCopy
        //{
        //    get
        //    {
        //        return _favouriteMenuCopy;
        //    }
        //    set
        //    {
        //        _favouriteMenuCopy = value;
        //    }
        //}
        //[OnSerializing]
        //public void OnSerializing(StreamingContext context)
        //{
        //    _favouriteMenuCopy = new TitleAndUrl(_menuItem.Title, _menuItem.Url, _menuItem.GetImagePath());
        //}

        //[OnDeserialized]
        //public void OnDeserialized(StreamingContext context)
        //{
        //    _menuItem= new MangaMenuItem(_favouriteMenuCopy);
        //}
    }
    
}
