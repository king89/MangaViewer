using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaViewer.ViewModel
{
    public class AppViewModel
    {
        #region MainViewModel
        private MainViewModel mainViewModel;
        public MainViewModel Main
        {
            get { return mainViewModel ?? (mainViewModel = new MainViewModel()); }
        }

        private SettingViewModel settingViewModel;
        public SettingViewModel Setting
        {
            get { return settingViewModel ?? (settingViewModel = new SettingViewModel()); }
        }
        #endregion

    }
}
