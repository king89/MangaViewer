using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if Win8
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
#elif WP
using System.Windows.Media;
using System.Windows.Media.Imaging;
#endif

namespace MangaViewer.Model
{
    public class MangaPageItem : CommonItem
    {
        public MangaPageItem(string uniqueId, string imagePath, string pageUrl, MangaChapterItem chapter,int pageNum,int totalNum)
            : base(uniqueId, string.Empty, string.Empty, imagePath, string.Empty)
        {
            _pageUrl = pageUrl;
            _imagePath = imagePath;
            _chapter = chapter;
            _pageNum = pageNum;
            _totalNum = totalNum;
        }

        public MangaPageItem()
            : base()
        {
        }

        #region Property

        private int _pageNum = 0;
        public int PageNum
        {
            get { return this._pageNum; }
            set
            {
                if (_pageNum != value)
                {
                    this._pageNum = value;
                    RaisePropertyChanged(() => PageNum);
                }
            }
        }

        private int _totalNum = 0;
        public int TotalNum
        {
            get { return this._totalNum; }
            set
            {
                if (_totalNum != value)
                {
                    this._totalNum = value;
                    RaisePropertyChanged(() => TotalNum);
                }
            }
        }

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
        private MangaChapterItem _chapter = null;
        public MangaChapterItem Chapter
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

        private ImageSource _image = null;
        private string _imagePath = null;
        public override ImageSource Image
        {
            get
            {
                if (this._image == null && this._imagePath != null && this._imagePath != "")
                {
                    //using (System.IO.FileStream s = System.IO.File.OpenRead(_imagePath))
                    //{
                    //    BitmapImage bmp = new BitmapImage();
                    //    bmp.SetSource(s);
                    //    ImageHeight = bmp.PixelHeight;
                    //    ImageWidth = bmp.PixelWidth;
                    //}
                    this._image = new BitmapImage(new Uri(_baseUri,_imagePath));
                }
                if (_image != null)
                {
                    this.IsLoadedImage = true;
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

        #region ImageHeight
        ///<summary>
        /// ImageHeight
        ///</summary>
        private double _mImageHeight = 0;
        public double ImageHeight
        {
        	get 
        	{ 
        		return _mImageHeight;
        	}
        	set 
        	{ 
        		_mImageHeight = value;
        		RaisePropertyChanged(() => ImageHeight);
        	}
        }
        #endregion
        #region ImageWidth
        ///<summary>
        /// ImageWidth
        ///</summary>
        private double _mImageWidth = 0;
        public double ImageWidth
        {
        	get 
        	{ 
        		return _mImageWidth;
        	}
        	set 
        	{ 
        		_mImageWidth = value;
        		RaisePropertyChanged(() => ImageWidth);
        	}
        }
        #endregion
        private bool _isLoadedImage = false;
        public bool IsLoadedImage
        {
            get 
            {
                return  _isLoadedImage;
            }
            set
            {
                _isLoadedImage = value;
                this.RaisePropertyChanged(() => IsLoadedImage);
            }
        }

        ///<summary>
        /// WebImageUrl
        ///</summary>
        private string _mWebImageUrl = null;
        public string WebImageUrl
        {
        	get 
        	{ 
        		return _mWebImageUrl;
        	}
        	set 
        	{ 
        		_mWebImageUrl = value;
        		RaisePropertyChanged(() => WebImageUrl);
        	}
        }
        #endregion

        public override void SetImage(String path)
        {
            this._image = null;
            this._imagePath = path;
            this.RaisePropertyChanged(() => Image);

        }
        public string GetImagePath()
        {
            return _imagePath;
        }
        public void RefreshImage()
        {
            this._image = null;
            this.IsLoadedImage = false;
        }
    }
}
