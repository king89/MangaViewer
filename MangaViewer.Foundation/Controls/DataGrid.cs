using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace MangaViewer.Foundation.Controls
{

    public class DataGrid : ListView
    {

        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register("Columns", typeof(ObservableCollection<DataGridTemplateColumn>), typeof(DataGrid), null);
        /// <summary>
        /// DataGrid的列集合
        /// </summary>
        public ObservableCollection<DataGridTemplateColumn> Columns
        {
            get
            {
                return this.GetValue(ColumnsProperty) as ObservableCollection<DataGridTemplateColumn>;
            }
            set
            {
                this.SetValue(ColumnsProperty, value);
            }
        }

        public DataGrid()
        {
            this.DefaultStyleKey = typeof(DataGrid);
            this.SelectionChanged += DataGrid_SelectionChanged;
            this.Columns = new ObservableCollection<DataGridTemplateColumn>();
        }


        public static readonly DependencyProperty RowDetailsTemplateProperty = DependencyProperty.Register("RowDetailsTemplate", typeof(DataTemplate), typeof(DataGrid), null);

        public DataTemplate RowDetailsTemplate
        {
            get
            {
                return this.GetValue(RowDetailsTemplateProperty) as DataTemplate;
            }
            set
            {
                this.SetValue(RowDetailsTemplateProperty, value);
            }
        }

        void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.SelectedIndex != -1)
            {
                var item = this.ItemContainerGenerator.ContainerFromIndex(this.SelectedIndex);
                if (item != null)
                {
                    //Detail状态其实就是Selected状态
                    if (RowDetailsTemplate == null)
                        VisualStateManager.GoToState(item as Control, "Selected", false);
                    else
                        VisualStateManager.GoToState(item as Control, DataGridRow.DetailVisualStateName, false);
                }
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DataGridRow() { Columns = this.Columns, RowDetailsTemplate = this.RowDetailsTemplate };
        }
    }
}
