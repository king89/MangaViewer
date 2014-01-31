using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MangaViewer.Control
{
    public partial class ItemContainer : UserControl
    {
        public ItemContainer()
        {
            this.MouseLeftButtonDown += OnPointerDown;
            this.MouseLeftButtonUp += OnPointerUp;
            this.MouseLeave += OnPointerUp;
        }

        private void OnPointerDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if ((sender as UIElement) != null)
                (sender as UIElement).Projection = new PlaneProjection() { LocalOffsetZ = -30 };
        }

        private void OnPointerUp(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if ((sender as UIElement) != null)
                (sender as UIElement).Projection = null;
        }
    }  
}
