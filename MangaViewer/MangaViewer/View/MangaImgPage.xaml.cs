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
using Windows.System;

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
            CanNotLoad.IsOpen = false;
            MangaPageItem selectPage = (MangaPageItem)ImageFlipView.SelectedItem;
            ViewModelLocator.AppViewModel.Main.SelectedPage = selectPage;
            if (selectPage != null)
            {
                if (!selectPage.IsLoadedImage)
                {
                    var path = await MangaService.GetIamgeByImageUrl(selectPage);
                    if (path == string.Empty)
                    {
                        CanNotLoad.IsOpen = true;
                        return;
                    }
                    selectPage.SetImage(path);
                    //下两页预读
                    int nowPage = selectPage.PageNum;
                    int totalPage = selectPage.TotalNum;
                    if (nowPage+1 < totalPage)
                    {
                        MangaPageItem nextOnePage = ViewModelLocator.AppViewModel.Main.PageList[nowPage + 1];
                        var pathNext = await MangaService.GetIamgeByImageUrl(nextOnePage);
                        nextOnePage.SetImage(pathNext);
                    }
                    if (nowPage+2 < totalPage)
                    {
                        MangaPageItem nextTwoPage = ViewModelLocator.AppViewModel.Main.PageList[nowPage + 2];
                        var pathNext = await MangaService.GetIamgeByImageUrl(nextTwoPage);
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

        private void ImageFlipView_KeyDown_1(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.W || e.Key == VirtualKey.S)
            {
                FlipView fv = (FlipView)sender;
                var container = fv.ItemContainerGenerator.ContainerFromItem(fv.SelectedItem);
                var children = CommonService.GetAllChildren(container);
                var name = "scrollViewer";
                var control = (ScrollViewer)children.First(c => c.Name == name);
                //最大值 control.ScrollableHeight
                double offset = 200;
                double nowVOffset = control.VerticalOffset;
                if (e.Key == VirtualKey.S)
                {

                    if (nowVOffset + offset >= control.ScrollableHeight)
                    {
                        nowVOffset = control.ScrollableHeight;
                    }
                    else
                    {
                        nowVOffset += offset;
                    }
                }
                if (e.Key == VirtualKey.W)
                {

                    if (nowVOffset - offset <= 0)
                    {
                        nowVOffset = 0;
                    }
                    else
                    {
                        nowVOffset -= offset;
                    }
                }
                //control.ZoomToFactor(3.0f);
                control.ScrollToVerticalOffset(nowVOffset);
            }
            if (e.Key == VirtualKey.A || e.Key == VirtualKey.D)
            {
                if (e.Key == VirtualKey.A)
                {
                    if (this.ImageFlipView.SelectedIndex - 1 >= 0)
                    {
                        this.ImageFlipView.SelectedIndex -= 1;
                    }

                } 
                else
                {
                    if (this.ImageFlipView.SelectedIndex + 1 < this.ImageFlipView.Items.Count)
                    {
                        this.ImageFlipView.SelectedIndex += 1;
                    }

                }
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModelLocator.AppViewModel.Main.SelectedPage.RefreshImage();
        }
    }
}
