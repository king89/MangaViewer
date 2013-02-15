using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaViewer.Foundation.Interactive
{
    public sealed class EventTriggerCollection : AttachedCollection<EventTrigger>
    {
        internal EventTriggerCollection()
        {

        }

        internal override void ItemAdded(EventTrigger item)
        {
            if (item != null) item.Attach(this.AssociatedObject);
        }
        internal override void ItemRemoved(EventTrigger item)
        {
            if (item != null) item.Detach();
        }
    }
}
