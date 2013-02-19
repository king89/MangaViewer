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
            pageTitle.Text = chapterItem.Subtitle + " " + chapterItem.Title;
           
           
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
            //ObservableCollection<MangaPageItem> pageItem = await App.MyMangaService.GetPageList(ViewModelLocator.AppViewModel.Main.SelectedChapter);
            //ViewModelLocator.AppViewModel.Main.PageList = pageItem;

            //没网络
            ViewModelLocator.AppViewModel.Main.PageList = new MangaViewer.Data.PageListData().PageList;
        }
        object sync = new object();
        private  void FlipView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            lock (sync)
            {
                MangaPageItem selectPage = (MangaPageItem)ImageFlipView.SelectedItem;
                if (selectPage != null)
                {
                    string path = App.MyMangaService.GetIamgeByImageUrl(selectPage).Result;
                    ((MangaPageItem)selectPage).SetImage(path);
                }
            }
        }
    }
}
