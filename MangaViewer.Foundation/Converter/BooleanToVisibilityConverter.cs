using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MangaViewer.Foundation.Converter
{
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        //parameter: is reverse ?
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter != null && bool.Parse(parameter as string))
                return (value is bool && (bool)value) ? Visibility.Collapsed : Visibility.Visible;
            else
                return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }
}
