using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MangaViewer.Foundation.Interactive;
using MangaViewer.Model;

namespace MangaViewer.ViewModel
{
    public class SettingViewModel : ViewModelBase
    {
        public SettingViewModel()
        {
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
        #endregion

        #region Sample Command
        private RelayCommand<ExCommandParameter> _favouriteCommand;
        public RelayCommand<ExCommandParameter> FavouriteCommand
        {
            get
            {
                return _favouriteCommand ?? (_favouriteCommand = new RelayCommand<ExCommandParameter>((ep) =>
                    {
                        //Command logic here
                        MangaChapterItem chpter = (MangaChapterItem)ep.Parameter;
                        chpter.Menu.ToString();
                    }));
            }
        }
        #endregion

    }
}
