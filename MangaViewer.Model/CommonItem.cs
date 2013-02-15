using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight;
using Windows.UI.Xaml.Media.Imaging;

namespace MangaViewer.Model
{
    public class CommonItem : ObservableObject
    {
        private static Uri _baseUri = new Uri("ms-appx:///");

        public CommonItem(string uniqueId, string title, string subtitle, string imagePath, string description)
        {
            this._uniqueId = uniqueId;
            this._title = title;
            this._subtitle = subtitle;
            this._description = description;
            this._imagePath = imagePath;
        }

        public CommonItem()
        {
        }

        private string _uniqueId = string.Empty;
        public string UniqueId
        {
            get { return this._uniqueId; }
            set
            {
                if (_uniqueId != value)
                {
                    this._uniqueId = value;
                    RaisePropertyChanged(() => UniqueId);
                }
            }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return this._title; }
            set
            {
                if (_title != value)
                {
                    this._title = value;
                    RaisePropertyChanged(() => Title);
                }
            }
        }

        private string _subtitle = string.Empty;
        public string Subtitle
        {
            get { return this._subtitle; }
            set
            {
                if (_subtitle != value)
                {
                    this._subtitle = value;
                    RaisePropertyChanged(() => Subtitle);
                }
            }
        }

        private string _description = string.Empty;
        public string Description
        {
            get { return this._description; }
            set
            {
                if (_description != value)
                {
                    this._description = value;
                    RaisePropertyChanged(() => Description);
                }
            }
        }

        private ImageSource _image = null;
        private string _imagePath = null;
        public ImageSource Image
        {
            get
            {
                if (this._image == null && this._imagePath != null)
                {
                    this._image = new BitmapImage(new Uri(CommonItem._baseUri, this._imagePath));
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

        public void SetImage(String path)
        {
            this._image = null;
            this._imagePath = path;
            this.RaisePropertyChanged(() => Image);
        }
    }
}
