using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaViewer.Foundation.Interactive
{
    public abstract class AttachedCollection<T> : ObservableCollection<T>, IAttachedObject where T : IAttachedObject
    {

        internal AttachedCollection()
        {

        }

        internal abstract void ItemAdded(T item);
        internal abstract void ItemRemoved(T item);
        private Collection<T> snapshot = new Collection<T>();

        protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (T item in e.NewItems)
                    {
                        try
                        {
                            this.VerifyAdd(item);
                            this.ItemAdded(item);
                            continue;
                        }
                        finally
                        {
                            this.snapshot.Insert(base.IndexOf(item), item);
                        }
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (T item in e.OldItems)
                    {
                        this.ItemRemoved(item);
                        this.snapshot.Remove(item);
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    foreach (T item in e.OldItems)
                    {
                        this.ItemRemoved(item);
                        this.snapshot.Remove(item);
                    }
                    foreach (T item in e.NewItems)
                    {
                        try
                        {
                            this.VerifyAdd(item);
                            this.ItemAdded(item);
                            continue;
                        }
                        finally
                        {
                            this.snapshot.Insert(base.IndexOf(item), item);
                        }
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    foreach (T item in this.snapshot)
                    {
                        this.ItemRemoved(item);
                    }
                    this.snapshot = new Collection<T>();
                    foreach (T item in this)
                    {
                        this.VerifyAdd(item);
                        this.ItemAdded(item);
                    }
                    break;
            }
        }

        private void VerifyAdd(T item)
        {
            if (this.snapshot.Contains(item)) throw new InvalidOperationException(string.Format("已经存在该元素"));
        }

        public void Attach(Windows.UI.Xaml.DependencyObject dependencyObject)
        {
            if (dependencyObject != this.AssociatedObject)
            {
                if (this.AssociatedObject != null) throw new InvalidOperationException("已经为该对象附加");
                associatedObject = dependencyObject;
            }
        }

        public void Detach()
        {
            foreach (var item in this)
            {
                item.Detach();
            }
            this.associatedObject = null;
        }

        private Windows.UI.Xaml.DependencyObject associatedObject;
        public Windows.UI.Xaml.DependencyObject AssociatedObject
        {
            get
            {
                return associatedObject;
            }
        }
    }
}
