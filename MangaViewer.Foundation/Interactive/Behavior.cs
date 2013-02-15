using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace MangaViewer.Foundation.Interactive
{
    public abstract class Behavior : IAttachedObject
    {
        public abstract void Attach(Windows.UI.Xaml.DependencyObject dependencyObject);

        public abstract void Detach();

        internal DependencyObject associatedObject;

        public DependencyObject AssociatedObject
        {
            get { return associatedObject; }
        }
    }

    public abstract class Behavior<T> : Behavior
    {
        protected abstract void OnAttached();
        protected abstract void OnDetached();
        public override void Attach(DependencyObject dependencyObject)
        {
            this.associatedObject = dependencyObject;
            OnAttached();
        }
        public override void Detach()
        {
            OnDetached();
        }
    }
}
