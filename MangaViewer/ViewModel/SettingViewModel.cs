using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace MangaViewer.ViewModel
{
    public class SettingViewModel : ViewModelBase
    {
        public SettingViewModel()
        {
        }


        #region Sample Property
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
        private RelayCommand _DoCommand;
        public RelayCommand DoCommand
        {
            get
            {
                return _DoCommand ?? (_DoCommand = new RelayCommand(() =>
                    {
                        //Command logic here
                    }));
            }
        }
        #endregion

    }
}
