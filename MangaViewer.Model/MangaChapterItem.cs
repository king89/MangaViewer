using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaViewer.Model
{
    public class MangaChapterItem : CommonItem
    {
        public MangaChapterItem(string uniqueId, string title, string imagePath, string description, string subtitle,string url)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            _url = url;
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

        public MangaChapterItem()
            : base()
        {
        }
    }
}
