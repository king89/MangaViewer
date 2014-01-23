using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.UI.Xaml;

namespace MangaViewer.Foundation.Interactive
{
    public class EventToCommandCollection : ObservableCollection<EventToCommand>
    {
        internal FrameworkElement Element { get; set; }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            // set parent element in each added eventtocommand
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (EventToCommand newItem in e.NewItems)
                {
                    newItem.Element = Element;
                }
            }

            // remove parent element in each removed eventtocommand
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (EventToCommand newItem in e.NewItems)
                {
                    newItem.Element = null;
                }
            }
            base.OnCollectionChanged(e);
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.RegisterAttached(
                "ItemsPropertyInternal", // Shadow the name so the parser does not skip GetEventToCommandCollection
                typeof(EventToCommandCollection),
                typeof(EventToCommandCollection), null);

        public static EventToCommandCollection GetItems(FrameworkElement item)
        {
            var collection = (EventToCommandCollection)item.GetValue(ItemsProperty);
            if (collection == null)
            {
                collection = new EventToCommandCollection();
                collection.Element = item;
                item.SetValue(ItemsProperty, collection);
            }
            return collection;
        }

        public static void SetItems(FrameworkElement item, EventToCommandCollection value)
        {
            item.SetValue(ItemsProperty, value);
        }
    }
}
