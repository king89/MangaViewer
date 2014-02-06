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
using System.Collections.ObjectModel;
using MangaViewer.ViewModel;
using MangaViewer.Model;
using System.Threading;
using System.Windows.Media.Imaging;
using MangaViewer.Service;
using System.IO;
using Windows.UI.Xaml;

namespace MangaViewer.View
{
    public partial class MangaImgPage : PhoneApplicationPage
    {
        Thread thread = null;
        // Constructor
        public MangaImgPage()
        {
            InitializeComponent();
            GetPageList();
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        async void GetPageList()
        {
            //有网络
            ViewModelLocator.AppViewModel.Main.PageList = null;
            ObservableCollection<MangaPageItem> pageItem = await MangaViewerWP.App.MangaService.GetPageList(ViewModelLocator.AppViewModel.Main.SelectedChapter);
            //LoadingStack.Visibility = Visibility.Collapsed;
            //this.TopAppBar.IsOpen = false;
            //this.BottomAppBar.IsOpen = false;
            ViewModelLocator.AppViewModel.Main.PageList = pageItem;
            
            thread = new Thread(StartDownload);
            thread.Start();
            ////没网络
            //ViewModelLocator.AppViewModel.Main.PageList = new MangaViewer.Data.PageListData().PageList;
        }
        public static void StartDownload()
        {
            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                foreach (var item in ViewModelLocator.AppViewModel.Main.PageList)
                {
                    MangaViewerWP.App.MangaService.GetPageImage(item);
                }
            });
        }
        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ViewModelLocator.AppViewModel.Main.SelectedPage.Image = null;
            //ScrollViewer sv = VisualTreeExtensions.FindVisualChild<ScrollViewer>(this.ImgPivot);
            //Image img = (Image)sv.FindName("Img");
            //BitmapImage bitmapImage = img.Source as BitmapImage;
            //bitmapImage.UriSource = null;
            //img.Source = null;

        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            if (thread!= null&&thread.IsAlive)
            {
                thread.Abort();
            }
        }

        private void Image_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ScrollViewer sv = (ScrollViewer)sender;
            Image img = (Image)sv.FindName("Img");
            if (!double.IsNaN(img.Width))
            {
                img.Width = double.NaN;
                img.Height = double.NaN;
                sv.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            }
            else
            {
                img.Width = ViewModelLocator.AppViewModel.Main.SelectedPage.ImageWidth;
                img.Height = ViewModelLocator.AppViewModel.Main.SelectedPage.ImageHeight;
                sv.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
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