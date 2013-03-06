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
using MangaViewer.Foundation.Common;
using MangaViewer.Model;
using System.Collections.ObjectModel;
using MangaViewer.ViewModel;
using MangaViewer.Service;

namespace MangaViewer.View
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MangaImgPage : LayoutAwarePage
    {
        public MangaImgPage()
        {
            this.InitializeComponent();
            GetPageList();
        }
        
        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected  override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            MangaChapterItem chapterItem = navigationParameter as MangaChapterItem;
            pageTitle.Text = chapterItem.Menu.Title + " " + chapterItem.Title ;
            ViewModelLocator.AppViewModel.Main.PageList = null;
           
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        async void GetPageList()
        {
            //有网络
            ObservableCollection<MangaPageItem> pageItem = await MangaService.GetPageList(ViewModelLocator.AppViewModel.Main.SelectedChapter);
            LoadingStack.Visibility = Visibility.Collapsed;
            this.TopAppBar.IsOpen = false;
            this.BottomAppBar.IsOpen = false;
            ViewModelLocator.AppViewModel.Main.PageList = pageItem;

            ////没网络
            //ViewModelLocator.AppViewModel.Main.PageList = new MangaViewer.Data.PageListData().PageList;
        }
        private async void FlipView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            MangaPageItem selectPage = (MangaPageItem)ImageFlipView.SelectedItem;
            ViewModelLocator.AppViewModel.Main.SelectedPage = selectPage;
            if (selectPage != null)
            {
                if (!selectPage.IsLoadedImage)
                {
                    var path = await MangaService.GetIamgeByImageUrl(selectPage);
                    selectPage.SetImage(path);
                    //下两页预读
                    int nowPage = selectPage.PageNum;
                    int totalPage = selectPage.TotalNum;
                    if (nowPage+1 < totalPage)
                    {
                        MangaPageItem nextOnePage = ViewModelLocator.AppViewModel.Main.PageList[nowPage + 1];
                        var pathNext = await App.MyMangaService.GetIamgeByImageUrl(nextOnePage);
                        nextOnePage.SetImage(pathNext);
                    }
                    if (nowPage+2 < totalPage)
                    {
                        MangaPageItem nextTwoPage = ViewModelLocator.AppViewModel.Main.PageList[nowPage + 2];
                        var pathNext = await App.MyMangaService.GetIamgeByImageUrl(nextTwoPage);
                        nextTwoPage.SetImage(pathNext);
                    }
                   
                    
                }
                else
                {
                    ShowPageNumStoryboard.Begin();
                }

            }
        }
        private void scrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            ScrollViewer sv = (ScrollViewer)sender;
            Image img = (Image)sv.FindName("image");
            double screenWidth = sv.ViewportWidth;
            double screenHeight = sv.ViewportHeight;
            double ration = img.ActualHeight / img.ActualWidth;
            float factor = sv.ZoomFactor;

        }

        private void Grid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

            Grid g = (Grid)sender;
            ScrollViewer sv = (ScrollViewer)g.FindName("scrollViewer");
            Image img = (Image)sv.FindName("image");
            double screenWidth = sv.ViewportWidth;
            double screenHeight = sv.ViewportHeight;
            img.Width = screenWidth;
            //img.Height = screenWidth * (img.ActualHeight / img.ActualWidth);
            sv.ZoomToFactor(1);
        }

        private void image_ImageOpened_1(object sender, RoutedEventArgs e)
        {
            ViewModelLocator.AppViewModel.Main.SelectedPage.IsLoadedImage = true;
            ShowPageGrid.Visibility = Visibility.Visible;
            ShowPageNumStoryboard.Begin();
        }
    }
}
