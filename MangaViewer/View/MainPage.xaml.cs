using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MangaViewer.Model;
using MangaViewer.ViewModel;
using System.Collections.ObjectModel;
using MangaViewer.Service;

namespace MangaViewer.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                LoadingStack.Visibility = Visibility.Collapsed;
            }
            else
            {
                GetMenu();
            }
            base.OnNavigatedTo(e);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //MangaMenuItem mi = (MangaMenuItem)(ViewModelLocator.AppViewModel.Main.MenuGroups[0].Items[0]);
            //mi.SetImage("http://localhost:8800/image/Hub/hub-BizPromotion.png");

            App.NavigationService.Navigate(typeof(SettingPage));
        }
        async void GetMenu()
        {
            //有网络
            ViewModelLocator.AppViewModel.Main.MenuGroups = null;
            ObservableCollection<HubMenuGroup> menu = await MangaService.GetMainMenu();
            LoadingStack.Visibility = Visibility.Collapsed;
            ViewModelLocator.AppViewModel.Main.MenuGroups = menu;

            ////没网络
            //ViewModelLocator.AppViewModel.Main.PageList = new MangaViewer.Data.PageListData().PageList;
        }
       

    }
}
