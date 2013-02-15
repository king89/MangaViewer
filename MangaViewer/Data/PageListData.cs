using MangaViewer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaViewer.Data
{
    public class PageListData
    {
        private ObservableCollection<MangaPageItem> _pageListData = new ObservableCollection<MangaPageItem>();
        public ObservableCollection<MangaPageItem> PageList
        {
            get { return this._pageListData; }
        }

        public PageListData()
        {
            for (int i = 1; i <= 20; i++)
            {
                _pageListData.Add(new MangaPageItem("page-" + i, i.ToString(), "http://localhost:8800/image/Hub/Hub-Product.jpg", string.Empty, "海贼王", "http://www.imanhua.com/comic/55/list_78283.html"));
            }
        }
    }
}
