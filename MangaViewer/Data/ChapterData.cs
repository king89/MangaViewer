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
            MangaMenuItem menu = MangaMenuItem.CreateADemo();
            for (int i = 100; i > 0; i--)
            {
                _chapterData.Add(new MangaChapterItem("chpt-" + i, i + "话", string.Empty, string.Empty, menu, "http://comic.131.com/content/2104/188362/1.html"));
            }
        }
    }
}
