using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MangaViewer.Foundation.Controls
{
    [TemplateVisualState(GroupName = "CellStates", Name = "Read")]
    [TemplateVisualState(GroupName = "CellStates", Name = "Edite")]
    public class DataGridCell : ContentControl
    {
        public static readonly DependencyProperty EditingTemplateProperty = DependencyProperty.Register("EditingTemplate", typeof(DataTemplate), typeof(DataGridCell), null);
        /// <summary>
        /// 单元格编辑模式模板
        /// </summary>
        public DataTemplate EditingTemplate
        {
            get
            {
                return this.GetValue(EditingTemplateProperty) as DataTemplate;
            }
            set
            {
                this.SetValue(EditingTemplateProperty, value);
            }
        }

        public static readonly DependencyProperty IsEditeProperty = DependencyProperty.Register("IsEdite", typeof(bool), typeof(DataGridCell), new PropertyMetadata(false, OnIsEditeChanged));

        private static void OnIsEditeChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            DataGridCell c = o as DataGridCell;
            if (c != null)
            {
                c.ChangedCellState();
            }
        }

        public bool IsEdite
        {
            get
            {
                return (bool)this.GetValue(IsEditeProperty);
            }
            set
            {
                this.SetValue(IsEditeProperty, value);
            }
        }


        internal DataGridCell()
        {
            this.DefaultStyleKey = typeof(DataGridCell);
        }

        protected override void OnDoubleTapped(Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            base.OnDoubleTapped(e);
            if (this.EditingTemplate != null)
                this.IsEdite = !this.IsEdite;
        }
        private void ChangedCellState()
        {
            if (this.EditingTemplate != null)
            {
                if (!IsEdite)
                    VisualStateManager.GoToState(this, "Read", true);
                else
                    VisualStateManager.GoToState(this, "Edite", true);
            }
            else
                throw new InvalidOperationException("没有定义EditingTemplate，无法进入编辑模式");
        }
    }
}
