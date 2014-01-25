using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if Win8
using Windows.UI.Xaml.Controls;
#elif WP
using System.Windows.Controls;
#endif




namespace MangaViewer.Common
{
    public class NavigationService
    {
        readonly Frame frame;

        public NavigationService(Frame frame)
        {
            this.frame = frame;
        }

        public void GoBack()
        {
            frame.GoBack();
        }

        public void GoForward()
        {
            frame.GoForward();
        }
#if Win8
        public bool Navigate<T>(object parameter = null)
        {
            var type = typeof(T);

            return Navigate(type, parameter);
        }

        public bool Navigate(Type source, object parameter = null)
        {
            return frame.Navigate(source, parameter);
        }
#elif WP
        public bool Navigate(Uri uri)
        {
            
            return frame.Navigate(uri);
        }
#endif
    }
}
