using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
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
using MangaViewer.Service;
using System.Threading.Tasks;
using MangaViewer.View;

namespace MangaViewer
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public static Frame RootFrame;
        private static ResourceLoader resourceLoader;
        public static ResourceLoader Res
        {
            get
            {
                return resourceLoader ?? (resourceLoader = new ResourceLoader());
            }
        }
        public static NavigationService NavigationService;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;


        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs args)
        {
            await SettingService.LoadSetting();
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
                
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(View.MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();

            App.NavigationService = new NavigationService(rootFrame);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            await SettingService.SaveSetting();
            deferral.Complete();

        }

        protected async override void OnSearchActivated(SearchActivatedEventArgs args)
        {
            base.OnSearchActivated(args);
            string data = args.QueryText;
            if (args.PreviousExecutionState == ApplicationExecutionState.NotRunning)
            {
                await PageStart(args, data);　　　　//App.xml.cs中定义的私有方法，下面有介绍
                return;
            }
            {
                await EnsureSearchPageActive(args);
            }
           
           // ViewModel.ViewModelLocator.AppViewModel.Main.SearchManga(args.QueryText);

        }

        /// <summary>
        /// 程序启动时初始化页面
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private async Task PageStart(IActivatedEventArgs args, string data)
        {
            await SettingService.LoadSetting();
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                rootFrame = new Frame();
                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 从之前挂起的应用程序加载状态
                }
            }

            if (Window.Current.Content == null)
            {
                rootFrame = new Frame();
                rootFrame.Navigate(typeof(SearchingPage),data);
                Window.Current.Content = rootFrame;
            }
            Window.Current.Activate();
            App.NavigationService = new NavigationService(rootFrame);
        }
        private async Task EnsureSearchPageActive(IActivatedEventArgs args)
        {
            if (args.PreviousExecutionState != ApplicationExecutionState.Terminated)
            {
                App.NavigationService.Navigate(typeof(SearchingPage), ((SearchActivatedEventArgs)args).QueryText);
            }
        }
    }
}
