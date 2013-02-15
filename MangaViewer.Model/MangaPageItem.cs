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
        public MangaPageItem(string uniqueId, string title, string imagePath, string url, string description, string subtitle)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            _url = url;
            _imagePath = imagePath;
        }

        public MangaPageItem()
            : base()
        {
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
    }
}
