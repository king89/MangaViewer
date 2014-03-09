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
using System.Windows.Media;
using System.Windows.Input;
using MangaViewer.Service;
using MangaViewerWP;
using MangaViewer.ViewModel;

namespace MangaViewer.View
{

    public partial class SearchPage : PhoneApplicationPage
    {
        private string SearchText { get; set; }
        // Constructor
        public SearchPage()
        {
            InitializeComponent();
            SearchText = "search...";
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode != NavigationMode.Back)
            {
                ViewModelLocator.AppViewModel.Main.SearchingList = null;
            }
            if (this.SearchTextBox.Text == "")
            {
                this.SearchTextBox.Text = SearchText;
                this.SearchTextBox.Foreground = new SolidColorBrush(Colors.Gray);

            }

        }

        private async void SearchTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.SearchingList.Focus();
                ViewModelLocator.AppViewModel.Main.SearchingList = await App.MangaService.GetSearchingList(this.SearchTextBox.Text);
            }
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.SearchTextBox.Text == SearchText)
            {
                this.SearchTextBox.Text = "";
            }
            this.SearchTextBox.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.SearchTextBox.Text == "")
            {
                this.SearchTextBox.Text = SearchText;
                this.SearchTextBox.Foreground = new SolidColorBrush(Colors.Gray);

            }
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
    }
}