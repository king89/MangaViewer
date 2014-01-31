using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MangaViewer.Model;
using System.Collections.ObjectModel;
using MangaViewer.Service;
using System.Collections.Generic;

#if Win8
using MangaViewer.Foundation.Interactive;
using System.Collections.ObjectModel;
#elif WP
using MangaViewerWP;
#endif


namespace MangaViewer.ViewModel
{
    public class SettingViewModel : ViewModelBase
    {
        public SettingViewModel()
        {
            Init();
        }

        private void Init()
        {
            _mWebSiteList = new ObservableCollection<string>();
            foreach(var i in Enum.GetNames(typeof(WebSiteEnum)))
            {
                _mWebSiteList.Add(i);
            }
            _mSelectedWebSite = App.SettingService.GetWebSite().ToString();
        }


        #region  Property
        private string _SampleProperty = string.Empty;
        public string SampleProperty
        {
            get { return this._SampleProperty; }
            set
            {
                if (_SampleProperty != value)
                {
                    this._SampleProperty = value;
                    RaisePropertyChanged(() => SampleProperty);
                }
            }
        }

        ///<summary>
        /// Setting WebSite
        ///</summary>
        private ObservableCollection<string> _mWebSiteList = null;
        public ObservableCollection<string> WebSiteList
        {
        	get 
        	{ 
        		return _mWebSiteList;
        	}
        	set 
        	{ 
        		_mWebSiteList = value;
        		RaisePropertyChanged(() => WebSiteList);
        	}
        }

        ///<summary>
        /// Selected WebSite
        ///</summary>
        private string _mSelectedWebSite = "";
        public string SelectedWebSite
        {
        	get 
        	{ 
        		return _mSelectedWebSite;
        	}
        	set 
        	{ 
        		_mSelectedWebSite = value;
                App.SettingService.SetWebSite(_mSelectedWebSite);
        		RaisePropertyChanged(() => SelectedWebSite);
        	}
        }
        #endregion

        #region Sample Command
        //private RelayCommand<ExCommandParameter> _favouriteCommand;
        //public RelayCommand<ExCommandParameter> FavouriteCommand
        //{
        //    get
        //    {
        //        return _favouriteCommand ?? (_favouriteCommand = new RelayCommand<ExCommandParameter>((ep) =>
        //            {
        //                //Command logic here
        //                MangaChapterItem chpter = (MangaChapterItem)ep.Parameter;
        //                chpter.Menu.ToString();
        //            }));
        //    }
        //}
        #endregion

    }
}
