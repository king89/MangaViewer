using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace MangaViewer.Model
{
    public class MangaPageItem : CommonItem
    {
        private static Uri _baseUri = new Uri("ms-appx:///");
        public MangaPageItem(string uniqueId, string pageNum, string imagePath, string pageUrl, string chapter, string menu)
            : base(uniqueId, string.Empty, string.Empty, imagePath, string.Empty)
        {
            _pageUrl = pageUrl;
            _imagePath = imagePath;
            _chapter = chapter;
            _menu = menu;
        }

        public MangaPageItem()
            : base()
        {
        }

        #region Property

        private string _pageUrl = string.Empty;
        public string PageUrl
        {
            get { return this._pageUrl; }
            set
            {
                if (_pageUrl != value)
                {
                    this._pageUrl = value;
                    RaisePropertyChanged(() => PageUrl);
                }
            }
        }
        private string _chapter = string.Empty;
        public string Chapter
        {
            get { return this._chapter; }
            set
            {
                if (_chapter != value)
                {
                    this._chapter = value;
                    RaisePropertyChanged(() => Chapter);
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

        private ImageSource _image = null;
        private string _imagePath = null;
        public new ImageSource Image
        {
            get
            {
                if (this._image == null && this._imagePath != null)
                {
                    this._image = new BitmapImage(new Uri(_baseUri, this._imagePath));
                }
                return this._image;
            }

            set
            {
                if (_image != value)
                {
                    this._imagePath = null;
                    RaisePropertyChanged(() => Image);
                }
            }
        } 
        #endregion
    }
}
