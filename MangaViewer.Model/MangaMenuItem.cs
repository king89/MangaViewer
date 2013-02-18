using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace MangaViewer.Model
{
    public class MangaMenuItem : HubMenuItem
    {
        private static Uri _baseUri = new Uri("ms-appx:///");
        private int col = 75;
        private int row = 150;
        public MangaMenuItem(string uniqueId, string title, string subtitle, string imagePath, string description, string content, HubMenuGroup group, string link, Size size, string titleBackground)
            : base(uniqueId, title, subtitle, imagePath, description,content,group,link,size,titleBackground)
        {
            _height = size.Height;
            _width = size.Width;
            _imagePath = imagePath;
        }

        private double _height = 0;
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
    }
}
