using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MangaViewer.Foundation.Converter
{
    public class PageNumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int pageNum = (int)value;
            int totolNum = (int)parameter;

            return pageNum.ToString() + "/" + totolNum;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
