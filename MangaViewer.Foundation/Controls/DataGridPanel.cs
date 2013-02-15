using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MangaViewer.Foundation.Controls
{
    public class DataGridPanel : Grid
    {
        protected override Windows.Foundation.Size ArrangeOverride(Windows.Foundation.Size finalSize)
        {
            for (int i = 0; i < this.Children.Count; i++)
            {
                FrameworkElement ui = this.Children[i] as FrameworkElement;
                if (ui != null)
                {
                    if (this.ColumnDefinitions.Count < this.Children.Count)
                    {
                        var dgc = ui.DataContext as DataGridTemplateColumn;
                        if (dgc == null)
                            this.ColumnDefinitions.Add(new ColumnDefinition());
                        else
                        {
                            this.ColumnDefinitions.Add(new ColumnDefinition() { Width = dgc.Width });
                        }
                    }
                    Grid.SetColumn(ui, i);
                }
            }
            return base.ArrangeOverride(finalSize);
        }
    }
}
