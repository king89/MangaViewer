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

        public bool IsLoadedImage
        {
            get 
            {
                if (_imagePath != string.Empty)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        public new void SetImage(String path)
        {
            this._image = null;
            this._imagePath = path;
            this.RaisePropertyChanged(() => Image);
            this.RaisePropertyChanged(() => IsLoadedImage);
        }
    }
}
