using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaViewer.Model
{
    public class MangaChapterItem : CommonItem
    {
        public MangaChapterItem(string uniqueId, string title, string imagePath, string description, string menu,string url)
            : base(uniqueId, title, string.Empty, imagePath, description)
        {
            _url = url;
            _menu = menu;
        }

        private string _url = string.Empty;
        public string Url
        {
            get { return this._url; }
            set
            {
                if (_url != value)
                {
                    this._url = value;
                    RaisePropertyChanged(() => Url);
                }
            }
        }

        private string _menu = string.Empty;
        public string Menu
        {
            get { return this._menu; }
            set
            {
                if (_menu != value)
                {
                    this._menu = value;
                    RaisePropertyChanged(() => Menu);
                }
            }
        }

        public MangaChapterItem()
            : base()
        {
        }
    }
}
