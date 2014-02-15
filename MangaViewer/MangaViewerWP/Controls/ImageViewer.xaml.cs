using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections;
using Windows.UI.Xaml;

namespace MangaViewer.Controls
{
    public partial class ImageViewer : UserControl
    {
        public ImageViewer()
        {
            InitializeComponent();
            MyPivot.SelectedIndex = 1;
        }
        #region SelectedItem

        public static readonly DependencyProperty SelectedItemProperty =
                    DependencyProperty.Register(
                        "SelectedItem",
                        typeof(object),
                        typeof(ImageViewer),
                        new PropertyMetadata(null, OnSelectedItemChanged)
                    );

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var selector = (ImageViewer)d;
            selector.SetSelectedItem(e);
        }

        private void SetSelectedItem(DependencyPropertyChangedEventArgs e)
        {
            SelectedItem = e.NewValue;
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        #endregion

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(object),
                typeof(ImageViewer),
                new PropertyMetadata(null, OnItemsSourceChanged)
            );

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ItemsSource = (ImageViewer)d;
            ItemsSource.SetItemsSource(e);
        }

        private void SetItemsSource(DependencyPropertyChangedEventArgs e)
        {
            ItemsSource = e.NewValue;
            IList list = ItemsSource as IList;
            if (list != null)
            {
                SelectedItem = list[_nowPage];
            }
        }

        public object ItemsSource
        {
            get { return GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private int _nowPage = 0;
        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pivot pivot = sender as Pivot;
            IList list = ItemsSource as IList;
            ScrollViewer sv = VisualTreeExtensions.FindVisualChild(pivot, "sv") as ScrollViewer;
            sv.VerticalOffset = 0;
            sv.HorizontalOffset = 0;
            if (pivot.SelectedIndex == 2)
            {
                if (list[_nowPage] != null)
                {
                    Type Ts = list[_nowPage].GetType();
                    Ts.GetMethod("RefreshImage").Invoke(list[_nowPage], null);
                }
                if (_nowPage + 1 < list.Count)
                {
                    _nowPage += 1;
                }
                SelectedItem = list[_nowPage];
                Dispatcher.BeginInvoke(() =>
                {
                    pivot.SelectedIndex = 1;

                });
            }
            if (pivot.SelectedIndex == 0)
            {
                if (list[_nowPage] != null)
                {
                    Type Ts = list[_nowPage].GetType();
                    Ts.GetMethod("RefreshImage").Invoke(list[_nowPage], null);
                }
                if (_nowPage - 1 >= 0)
                {
                    _nowPage -= 1;
                }
                SelectedItem = list[_nowPage];
                Dispatcher.BeginInvoke(() =>
                {
                    pivot.SelectedIndex = 1;
                });
            }
        }
    }
}
