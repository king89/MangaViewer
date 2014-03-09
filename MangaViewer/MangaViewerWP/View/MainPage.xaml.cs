using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MangaViewer.Resources;
using MangaViewer.ViewModel;
using System.Collections.ObjectModel;
using MangaViewer.Model;
using MangaViewer.Service;
using MangaViewerWP;
using System.Windows.Input;

namespace MangaViewer.View
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
            GetMenu();
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

        async void GetMenu()
        {
            //有网络
            ViewModelLocator.AppViewModel.Main.MenuGroups = null;
            try
            {
                ObservableCollection<HubMenuGroup> menu = await App.MangaService.GetMainMenu();
                //LoadingStack.Visibility = Visibility.Collapsed;
                ViewModelLocator.AppViewModel.Main.MenuGroups = menu;
            }
            catch (System.Exception ex)
            {

            }

            ////没网络
            //ViewModelLocator.AppViewModel.Main.PageList = new MangaViewer.Data.PageListData().PageList;
        }

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    if (e.NavigationMode == NavigationMode.Back)
        //    {
        //        this.SearchGrid.Visibility = Visibility.Collapsed;
        //    }
        //    base.OnNavigatedTo(e);
        //}

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            MangaViewerWP.App.NavigationService.Navigate(new Uri("/View/SettingPage.xaml", UriKind.Relative));
        }

        private void SearchBarIconButton_Click(object sender, EventArgs e)
        {
            MangaViewerWP.App.NavigationService.Navigate(new Uri("/View/SearchPage.xaml", UriKind.Relative));

        }

    }
}