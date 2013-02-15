using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace MangaViewer.Foundation.Interactive
{
    public interface IAttachedObject
    {
        void Attach(DependencyObject dependencyObject);
        void Detach();

        DependencyObject AssociatedObject { get; }
    }

}
