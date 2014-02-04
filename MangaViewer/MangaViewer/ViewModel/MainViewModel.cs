using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using System.Net.Http;
using System.Text;
using Windows.UI.Popups;
using System.Windows.Input;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using MangaViewer.Service;

using MangaViewer.Model;
using MangaViewer.Common;
using MangaViewer.View;


#if Win8
using MangaViewer.Data;
using MangaViewer.Foundation.Helper;
using MangaViewer.Foundation.Interactive;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls;
using Windows.Data.Xml.Dom;
using Windows.UI.Xaml;
using Windows.UI.Core;

#elif WP
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MangaViewerWP;
#endif




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
                //Is Loaded
                if (_menuGroups != null)
                {
                    IsMenuLoaded = true;

                }
                if (_menuGroups == null)
                {
                    IsMenuLoaded = false;
                }
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
                //Is Loaded
                if (_chapterList != null)
                {
                    IsChapterLoaded = true;

                }
                if (_chapterList == null)
                {
                    IsChapterLoaded = false;
                }
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
                //默认选中第一个
                if (_pageListData != null && _pageListData.Count > 0)
                {
                    _selectedPage = PageList[0];
                    
                }
                if (_pageListData == null)
                {
                    _selectedPage = null;
                    
                }
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

        #region IsMenuLoaded
        ///<summary>
        /// Is Menu Loaded
        ///</summary>
        private bool _mIsMenuLoaded = false;
        public bool IsMenuLoaded
        {
            get
            {
                return _mIsMenuLoaded;
            }
            set
            {
                _mIsMenuLoaded = value;
                RaisePropertyChanged(() => IsMenuLoaded);
            }
        } 
        #endregion

        #region IsChapterLoaded
        ///<summary>
        /// Is Chapter List Loaded
        ///</summary>
        private bool _mIsChapterLoaded = false;
        public bool IsChapterLoaded
        {
        	get 
        	{ 
        		return _mIsChapterLoaded;
        	}
        	set 
        	{ 
        		_mIsChapterLoaded = value;
        		RaisePropertyChanged(() => IsChapterLoaded);
        	}
        }
        #endregion

        

        #region SeachingDataMenu
        ObservableCollection<MangaMenuItem> _searchListData;
        public ObservableCollection<MangaMenuItem> SearchingList
        {
            get
            {
                //Demo
                //if (_pageListData == null)
                //    _pageListData = new PageListData().PageList;

                return _searchListData;


            }
            set
            {
                _searchListData = value;
                RaisePropertyChanged(() => SearchingList);
            }
        }
        #endregion
        public bool IsFavourited
        {
            get
            {
                return App.SettingService.CheckFavourtie(SelectedMenu);
            }
            set 
            {
                RaisePropertyChanged(() => IsFavourited);
            }
        }

        #region Command
        
#if Win8

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
                    string path = await App.MangaService.GetIamgeByImageUrl(_selectedPage);
                    _selectedPage.SetImage(path);
                }));
            }
        }

        private RelayCommand<ExCommandParameter> _searchMenuSelectedCommand;
        public RelayCommand<ExCommandParameter> SearchMenuSelectedCommand
        {
            get
            {
                return _searchMenuSelectedCommand ?? (_searchMenuSelectedCommand = new RelayCommand<ExCommandParameter>((ep) =>
                {
                    ItemClickEventArgs e = ep.EventArgs as ItemClickEventArgs;
                    _selectedMenu = e.ClickedItem as MangaMenuItem;
                    App.NavigationService.Navigate(typeof(ChapterPage), _selectedMenu);
                }));
            }
        }
#elif WP
        private RelayCommand<MangaMenuItem> _menuSelectedCommand;
        public RelayCommand<MangaMenuItem> MenuSelectedCommand
        {
            get
            {
                return _mSettingCommand ?? (_mSettingCommand = new RelayCommand<MangaMenuItem>((ep) =>
                {
                    MangaViewerWP.App.NavigationService.Navigate(new Uri("/View/ChapterPage.xaml", UriKind.Relative));
                }));
            }
        }

        private RelayCommand<string> _chapterSelectedCommand;
        public RelayCommand<string> ChapterSelectedCommand
        {
            get
            {
                return _chapterSelectedCommand ?? (_chapterSelectedCommand = new RelayCommand<string>((ep) =>
                {
                    MangaViewerWP.App.NavigationService.Navigate(new Uri("/View/MangaImgPage.xaml", UriKind.Relative));
                }));
            }
        }

        private RelayCommand<MangaMenuItem> _mSettingCommand;
        public RelayCommand<MangaMenuItem> SettingCommand
        {
            get
            {
                return _mSettingCommand ?? (_mSettingCommand = new RelayCommand<MangaMenuItem>((ep) =>
                {
                    MangaViewerWP.App.NavigationService.Navigate(new Uri("/View/SettingPage.xaml", UriKind.Relative));
                }));
            }
        }
#endif
        #endregion

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


        public void SearchManga(string queryText)
        {
#if Win8
            App.NavigationService.Navigate(typeof(SearchingPage), queryText);
#elif WP

#endif
            //throw new NotImplementedException();
        }
        public void AddMyFavouriteMangaMenu(MangaMenuItem menu)
        {
            foreach (HubMenuGroup group in MenuGroups)
            {
                if (group.UniqueId == MenuType.MyFavourite.ToString())
                {
                    group.Items.Add(menu);
                    return;
                }
            }
        }
    }
}