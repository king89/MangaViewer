using MangaViewer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaViewer.Data
{
    public class ChapterData
    {
        private ObservableCollection<MangaChapterItem> _chapterData = new ObservableCollection<MangaChapterItem>();
        public ObservableCollection<MangaChapterItem> Chapters
        {
            get { return this._chapterData; }
        }

        public ChapterData()
        {
            for (int i = 100; i > 0; i--)
            {
                //_chapterData.Add(new MangaChapterItem("chpt-" + i, i + "话", string.Empty, string.Empty, "海贼王", "http://www.imanhua.com/comic/55/list_78283.html"));
            }
        }
    }
}
