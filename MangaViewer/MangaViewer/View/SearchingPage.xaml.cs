using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MangaViewer.Foundation.Common;
using System.Threading.Tasks;
using MangaViewer.Service;
using Windows.ApplicationModel.Search;

namespace MangaViewer.View
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class SearchingPage : LayoutAwarePage
    {
        public SearchingPage()
        {
            this.InitializeComponent();
            this.LoadingStack.Visibility = Visibility.Collapsed;

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
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            SearchPane searchPane = SearchPane.GetForCurrentView();
            searchPane.QuerySubmitted += new TypedEventHandler<SearchPane, SearchPaneQuerySubmittedEventArgs>(searchPane_QuerySubmitted);

        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            SearchPane searchPane = SearchPane.GetForCurrentView();
            searchPane.QuerySubmitted -= new TypedEventHandler<SearchPane, SearchPaneQuerySubmittedEventArgs>(searchPane_QuerySubmitted);

        }
        protected  override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                SearchMethod((string)e.Parameter);
            }
        }
        async Task GetSearchingList(string queryText)
        {
            var menu = await App.MangaService.GetSearchingList(queryText);
            MangaViewer.ViewModel.ViewModelLocator.AppViewModel.Main.SearchingList = menu;
        }
        void searchPane_QuerySubmitted(SearchPane sender, SearchPaneQuerySubmittedEventArgs agrs)
        {
            SearchMethod(agrs.QueryText);

        }

        private async void SearchMethod(string queryText)
        {
            MangaViewer.ViewModel.ViewModelLocator.AppViewModel.Main.SearchingList = null;
            LoadingStack.Visibility = Visibility.Visible;
            this.pageTitle.Text = "搜索'" + queryText + "'的结果";
            await GetSearchingList(queryText);
            LoadingStack.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            App.NavigationService.Navigate(typeof(MainPage));
        }

        private void ScrollViewer_ViewChanged_1(object sender, ScrollViewerViewChangedEventArgs e)
        {
            ScrollViewer sv = (ScrollViewer)sender;
            if (sv.HorizontalOffset >= sv.ScrollableWidth)
            {
            }
            
        }
    }
}
