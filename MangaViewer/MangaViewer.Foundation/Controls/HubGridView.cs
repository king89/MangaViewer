using MangaViewer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MangaViewer.Foundation.Controls
{


    public class HubGridView : GridView
    {
        private int rowVal;
        private int colVal;

        public HubGridView()
        {
        }

        protected override void PrepareContainerForItemOverride(Windows.UI.Xaml.DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            HubItem hubItem = item as HubItem;

            if (hubItem != null)
            {
                colVal = (int)hubItem.ItemSize.Width;
                rowVal = (int)hubItem.ItemSize.Height;
            }
            else
            {
                colVal = (int)HubItemSizes.OtherSmallItem.Width;
                rowVal = (int)HubItemSizes.OtherSmallItem.Height;
            }

            VariableSizedWrapGrid.SetRowSpan(element as UIElement, rowVal);
            VariableSizedWrapGrid.SetColumnSpan(element as UIElement, colVal);
        }
    }


}
