using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaViewer.Model
{
    public class HubMenuGroup : CommonItem
    {
        public HubMenuGroup(string uniqueId, string title, string subtitle, string imagePath, string description)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
        }

        private ObservableCollection<HubMenuItem> _items = new ObservableCollection<HubMenuItem>();
        public ObservableCollection<HubMenuItem> Items
        {
            get { return this._items; }
        }
    }
}
