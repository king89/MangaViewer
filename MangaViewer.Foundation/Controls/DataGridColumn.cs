using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MangaViewer.Foundation.Controls
{
    public class DataGridTemplateColumn : DependencyObject
    {
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(object), typeof(DataGridTemplateColumn), null);
        /// <summary>
        /// 列标题
        /// </summary>
        public object Header
        {
            get
            {
                return this.GetValue(HeaderProperty);
            }
            set
            {
                this.SetValue(HeaderProperty, value);
            }
        }

        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(DataGridTemplateColumn), null);
        /// <summary>
        /// 列标题模版
        /// </summary>
        public DataTemplate HeaderTemplate
        {
            get
            {
                return this.GetValue(HeaderTemplateProperty) as DataTemplate;
            }
            set
            {
                this.SetValue(HeaderTemplateProperty, value);
            }
        }

        public static readonly DependencyProperty CellTemplateProperty = DependencyProperty.Register("CellTemplate", typeof(DataTemplate), typeof(DataGridTemplateColumn), null);
        /// <summary>
        /// 单元格模版
        /// </summary>
        public DataTemplate CellTemplate
        {
            get
            {
                return this.GetValue(CellTemplateProperty) as DataTemplate;
            }
            set
            {
                this.SetValue(CellTemplateProperty, value);
            }
        }

        public static readonly DependencyProperty CellEditingTemplateProperty = DependencyProperty.Register("CellEditingTemplate", typeof(DataTemplate), typeof(DataGridTemplateColumn), null);
        /// <summary>
        /// 单元格编辑模式模板
        /// </summary>
        public DataTemplate CellEditingTemplate
        {
            get
            {
                return this.GetValue(CellEditingTemplateProperty) as DataTemplate;
            }
            set
            {
                this.SetValue(CellEditingTemplateProperty, value);
            }
        }


        public static readonly DependencyProperty WidthProperty = DependencyProperty.Register("Width", typeof(GridLength), typeof(DataGridTemplateColumn), new PropertyMetadata(new GridLength(1, GridUnitType.Star)));
        /// <summary>
        /// 列宽
        /// </summary>
        public GridLength Width
        {
            get
            {
                return (GridLength)this.GetValue(WidthProperty);
            }
            set
            {
                this.SetValue(WidthProperty, value);
            }
        }
    }
}
