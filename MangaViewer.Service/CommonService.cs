using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace MangaViewer.Service
{
    public static class CommonService
    {
        public static T Clone<T>(object obj)
        {
            string xml = MySerialize.JsonSerialize(obj);

            object newObj = MySerialize.JsonDeserialize<T>(xml);

            return (T)newObj;
        }

        public static List<Control> GetAllChildren(DependencyObject parent)
        {
            var list = new List<Control>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent);i++ )
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is Control)
                {
                    list.Add(child as Control);
                }
                list.AddRange(GetAllChildren(child));
            }

            return list;
        }
    }
}
