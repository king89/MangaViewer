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
using MangaViewer.Service;


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
        ObservableCollection<HubMenuGroup> _menuGroups;
        public ObservableCollection<HubMenuGroup> MenuGroups
        {
            get
            {
                return _menuGroups;
            }
            set 
            {
                _menuGroups = value;
                RaisePropertyChanged(() => MenuGroups);
            }
        }
        #endregion
        #region ChapterData
        ObservableCollection<MangaChapterItem> _chapterList;
        public ObservableCollection<MangaChapterItem> ChapterList
        {
            get
            {
                return _chapterList;
            }
            set
            {
                _chapterList = value;
                RaisePropertyChanged(() => ChapterList);
            }
        }
        #endregion
        #region PageList
        ObservableCollection<MangaPageItem> _pageListData;
        public  ObservableCollection<MangaPageItem> PageList
        {
            get
            {
                //Demo
                //if (_pageListData == null)
                //    _pageListData = new PageListData().PageList;

                return _pageListData;


            }
            set 
            {
                _pageListData = value;
                RaisePropertyChanged(() => PageList);
            }
        }
        MangaPageItem _selectedPage = null;
        public MangaPageItem SelectedPage
        {
            get 
            {
                return _selectedPage;
            }
            set
            { 
                _selectedPage = value;
                RaisePropertyChanged(() => SelectedPage);
            }
        }
        
        #endregion

        public bool IsFavourited
        {
            get
            {
                return SettingService.CheckFavourtie(SelectedMenu);
            }
            set 
            {
                RaisePropertyChanged(() => IsFavourited);
            }
        }
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

        private RelayCommand<ExCommandParameter> _menuSelectedCommand;
        public RelayCommand<ExCommandParameter> MenuSelectedCommand
        {
            get
            {
                return _menuSelectedCommand ?? (_menuSelectedCommand = new RelayCommand<ExCommandParameter>((ep) =>
                    {
                        ItemClickEventArgs e = ep.EventArgs as ItemClickEventArgs;
                        _selectedMenu = e.ClickedItem as MangaMenuItem;
                        App.NavigationService.Navigate(typeof(ChapterPage), _selectedMenu);

                      
                    }));
            }
        }

        private RelayCommand<ExCommandParameter> _chapterSelectedCommand;
        public RelayCommand<ExCommandParameter> ChapterSelectedCommand
        {
            get
            {
                return _chapterSelectedCommand ?? (_chapterSelectedCommand = new RelayCommand<ExCommandParameter>( (ep) =>
                {
                    ItemClickEventArgs e = ep.EventArgs as ItemClickEventArgs;
                    _selectedChapter = e.ClickedItem as MangaChapterItem;
                    App.NavigationService.Navigate(typeof(MangaImgPage), _selectedChapter);
                }));
            }
        }

        private RelayCommand<ExCommandParameter> _pageSelectedCommand;
        public RelayCommand<ExCommandParameter> PageSelectedCommand
        {
            get
            {
                return _pageSelectedCommand ?? (_pageSelectedCommand = new RelayCommand<ExCommandParameter>(async (ep) =>
                {
                    ItemClickEventArgs e = ep.EventArgs as ItemClickEventArgs;
                    _selectedPage = e.ClickedItem as MangaPageItem;
                     string path = await MangaService.GetIamgeByImageUrl(_selectedPage);
                    _selectedPage.SetImage(path);
                }));
            }
        }

        //some private var
        MangaMenuItem _selectedMenu = null;
        public MangaMenuItem SelectedMenu
        {
            get { return _selectedMenu; }
            set { _selectedMenu = value; }
        }
        MangaChapterItem _selectedChapter = null;
        public MangaChapterItem SelectedChapter
        {
            get { return _selectedChapter; }
            set { _selectedChapter = value; }
        }

    }
}