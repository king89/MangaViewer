using MangaViewer.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

#if Win8
using Windows.UI.Xaml.Media;
#elif WP
using System.Windows.Media;
#endif

namespace MangaViewer.Model
{
    public static class HubItemSizes
    {
        public static Size FocusItem = new Size(6, 3);
        public static Size PrimaryItem = new Size(6, 2);
        public static Size SecondarySmallItem = new Size(3, 1);
        public static Size SecondaryTallItem = new Size(3, 2);
        public static Size OtherSmallItem = new Size(2, 1);

    }

    public interface HubItem
    {
        Size ItemSize
        { get; set; }
    }

    public class HubMenuItem : CommonItem, 
                                HubItem
    {
        public HubMenuItem(string uniqueId, string title, string subtitle, string imagePath, string description, string content, HubMenuGroup group, string link, Size size, string titleBackground)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this._content = content;
            this._group = group;
            this._itemSize = size;
            this._link = link;
            this._titleBackground = titleBackground;
        }

        public HubMenuItem()
            : base()
        {
        }

        private string _content = string.Empty;
        public string Content
        {
            get { return this._content; }
            set
            {
                if (_content != value)
                {
                    this._content = value;
                    RaisePropertyChanged(() => Content);
                }
            }
        }

        private string _link = string.Empty;
        public string Link
        {
            get { return this._link; }
            set
            {
                if (_link != value)
                {
                    this._link = value;
                    RaisePropertyChanged(() => Link);
                }
            }
        }

        private Size _itemSize;
        public Size ItemSize
        {
            get { return _itemSize; }
            set
            {
                if (_itemSize != value)
                {
                    this._itemSize = value;
                    RaisePropertyChanged(() => ItemSize);
                }
            }
        }

        private string _titleBackground;
        public Brush TitleBackground
        {
            get
            {
                if (!string.IsNullOrEmpty(_titleBackground))
                    return new SolidColorBrush(ColorHelper.HexToColor(_titleBackground));
                else
                    return new SolidColorBrush(ColorHelper.HexToColor("#aa111111")); //default value
            }
        }

        private HubMenuGroup _group;
        public HubMenuGroup Group
        {
            get { return this._group; }
            set
            {
                if (_group != value)
                {
                    this._group = value;
                    RaisePropertyChanged(() => Group);
                }
            }
        }
    }
}
