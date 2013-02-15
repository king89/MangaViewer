using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Data.Xml.Dom;
using Windows.UI.Xaml;
using Windows.UI.Core;
using System.Net.Http;
using System.Text;
using Windows.UI.Popups;
using System.Windows.Input;
using System.Linq;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls;
using HtmlAgilityPack;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using MangaViewer.Data;
using MangaViewer.Model;
using MangaViewer.Common;
using MangaViewer.Foundation.Helper;
using MangaViewer.Foundation.Interactive;
using MangaViewer.View;


namespace MangaViewer.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
        }

        #region Property IsDownloading
        private bool isDownloading = false;
        public bool IsDownloading
        {
            get
            {
                return isDownloading;
            }
            set
            {
                if (isDownloading != value)
                {
                    isDownloading = value;
                    RaisePropertyChanged(() => IsDownloading);
                }
            }
        }
        #endregion

        #region MainMenuDataSource
        HubMenuDataSource menuData;
        public ObservableCollection<HubMenuGroup> MenuGroups
        {
            get
            {
                if (menuData == null)
                    menuData = new HubMenuDataSource();
                return menuData.MenuGroups;
            }
        }
        #endregion
        #region ChapterData
        ChapterData chapterData;
        public ObservableCollection<MangaChapterItem> Chapters
        {
            get
            {
                if (chapterData == null)
                    chapterData = new ChapterData();

                return chapterData.Chapters;
            }
        }
        #endregion
        #region PageList
        PageListData pageListData;
        public ObservableCollection<MangaPageItem> PageList
        {
            get
            {
                if (pageListData == null)
                    pageListData = new PageListData();

                return pageListData.PageList;
            }
        }
        #endregion
        #region CoverImage
        public ImageSource CoverImage
        {
            get
            {
                Random r = new Random(DateTime.Now.Millisecond);
                int imgIndex = r.Next(0, 6);

                return new BitmapImage(new Uri(new Uri(Constants.BASEURI), string.Format("/Assets/Cover/{0}.jpg", imgIndex)));
            }
        }
        #endregion

        private RelayCommand<ExCommandParameter> _hubCommand;
        public RelayCommand<ExCommandParameter> HubCommand
        {
            get
            {
                return _hubCommand ?? (_hubCommand = new RelayCommand<ExCommandParameter>((ep) =>
                    {
                        ItemClickEventArgs e = ep.EventArgs as ItemClickEventArgs;
                        HubMenuItem selectedMenu = e.ClickedItem as HubMenuItem;
                        App.NavigationService.Navigate(typeof(ChapterPage), selectedMenu);

                      
                    }));
            }
        }

        private RelayCommand<ExCommandParameter> _chapterCommand;
        public RelayCommand<ExCommandParameter> ChapterCommand
        {
            get
            {
                return _chapterCommand ?? (_chapterCommand = new RelayCommand<ExCommandParameter>((ep) =>
                {
                    ItemClickEventArgs e = ep.EventArgs as ItemClickEventArgs;
                    CommonItem selectedMenu = e.ClickedItem as CommonItem;
                    App.NavigationService.Navigate(typeof(MangaImgPage), selectedMenu);
                }));
            }
        }
    }
}