using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaViewer.Foundation.Interactive
{
    public sealed class BehaviorCollection : AttachedCollection<Behavior>
    {
        internal BehaviorCollection()
        {

        }

        internal override void ItemAdded(Behavior item)
        {
            if (base.AssociatedObject != null) item.Attach(base.AssociatedObject);
        }

        internal override void ItemRemoved(Behavior item)
        {
            if (((IAttachedObject)item).AssociatedObject != null) item.Detach();
        }

        //protected override void OnAttached()
        //{
        //    foreach (Behavior behavior in this)
        //    {
        //        behavior.Attach(base.AssociatedObject);
        //    }
        //}

        //protected override void OnDetaching()
        //{
        //    foreach (Behavior behavior in this)
        //    {
        //        behavior.Detach();
        //    }
        //}
    }


}
