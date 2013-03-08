using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace MangaViewer.Model
{
    [DataContract(Name = "MangaMenuItem", Namespace = "MangaViewer.Model")]
    public class MangaMenuItem : HubMenuItem
    {
        private static Uri _baseUri = new Uri("ms-appx:///");
        private int col = 75;
        private int row = 150;
        public static MangaMenuItem CreateADemo()
        {
            Size si = new Size();
            return new MangaMenuItem("menu-1", "Titel",  string.Empty, null, "http://comic.131.com/content/shaonian/2104.html", si, string.Empty);
        }

        public MangaMenuItem(string uniqueId, string title,  string imagePath,  HubMenuGroup group, string url, Size size, string titleBackground)
            : base(uniqueId, title, String.Empty, imagePath, string.Empty,string.Empty,group,url,size,titleBackground)
        {
            _height = size.Height;
            _width = size.Width;
            _imagePath = imagePath;
            _url = url;
        }
        
        private string _url = "";
        [DataMember()]
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

        private double _height = 0;
        [DataMember()]
        public double Height
        {
            get { return this._height*row; }
            set
            {
                if (_height != value)
                {
                    this._height = value;
                    RaisePropertyChanged(() => Height);
                }
            }
        }

        private double _width = 0;
        [DataMember()]
        public double Width
        {
            get { return this._width*col; }
            set
            {
                if (_width != value)
                {
                    this._width = value;
                    RaisePropertyChanged(() => Width);
                }
            }
        }
        private ImageSource _image = null;
        private string _imagePath = null;
        [DataMember()]
        public new ImageSource Image
        {
            get
            {
                if (this._image == null && this._imagePath != null)
                {

                    this._image = new BitmapImage(new Uri(_baseUri, this._imagePath));
                }
                if (this._image == null)
                {

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

        public new void SetImage(String path)
        {
            this._image = null;
            this._imagePath = path;
            this.RaisePropertyChanged(() => Image);
        }

        public MangaMenuItem Clone()
        {
            Size size = new Size(ItemSize.Width, ItemSize.Height);
            return new MangaMenuItem(this.UniqueId, this.Title, this._imagePath, this.Group, this.Url, size, "");
        }

        public void SetSize(Size size)
        {
            this.Height = size.Height;
            this.Width = size.Width;
            this.ItemSize = size;
        }
    }
}
