using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MangaViewer.Foundation.Controls
{
    /// <summary>
    /// DataGrid行样式
    /// </summary>
    [TemplateVisualState(GroupName = "SelectionStates", Name = DetailVisualStateName)]
    public class DataGridRow : ListViewItem
    {
        internal const string DetailVisualStateName = "Detail";
        public static readonly DependencyProperty RowDetailsTemplateProperty = DependencyProperty.Register("RowDetailsTemplate", typeof(DataTemplate), typeof(DataGridRow), null);

        public DataTemplate RowDetailsTemplate
        {
            get
            {
                return this.GetValue(RowDetailsTemplateProperty) as DataTemplate;
            }
            internal set
            {
                this.SetValue(RowDetailsTemplateProperty, value);
            }
        }


        public static readonly DependencyProperty CellContentProperty = DependencyProperty.Register("CellContent", typeof(object), typeof(DataGridRow), null);

        public object CellContent
        {
            get
            {
                return this.GetValue(CellContentProperty);
            }
            internal set
            {
                this.SetValue(CellContentProperty, value);
            }
        }

        public static readonly DependencyProperty CellStyleProperty = DependencyProperty.Register("CellStyle", typeof(Style), typeof(DataGridRow), null);
        /// <summary>
        /// DataGrid单元格边框样式
        /// </summary>
        public Style CellStyle
        {
            get
            {
                return this.GetValue(CellStyleProperty) as Style;
            }
            set
            {
                this.SetValue(CellStyleProperty, value);
            }
        }

        internal ObservableCollection<DataGridTemplateColumn> Columns
        {
            get;
            set;
        }

        public DataGridRow()
        {
            this.DefaultStyleKey = typeof(DataGridRow);
        }
        /// <summary>
        /// 每行的单元格集合
        /// </summary>
        public List<DataGridCell> Cells { get; private set; }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            DataGridPanel panel = new DataGridPanel();
            Cells = new List<DataGridCell>();
            foreach (var item in this.Columns)
            {
                DataGridCell c = new DataGridCell();
                if (this.CellStyle != null)
                    c.SetValue(Control.StyleProperty, this.CellStyle);
                c.ContentTemplate = item.CellTemplate;
                c.Content = newContent;
                c.DataContext = item;
                c.EditingTemplate = item.CellEditingTemplate;
                Cells.Add(c);
                panel.Children.Add(c);

            }
            this.CellContent = panel;
        }
    }
}
