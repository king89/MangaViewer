using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MangaViewer.Common.Helper
{

    public class StringHelper
    {
        public static string CleanHtml(string html)
        {
            Regex regex = new Regex("<(.|\n)+?>");

            return regex.Replace(html.Replace("<br />", Environment.NewLine).Replace("\n", Environment.NewLine), string.Empty);
        }

        public static string ClearLeftSpace(string text)
        {
            return text.Replace(Environment.NewLine + "　　", Environment.NewLine + Environment.NewLine)
                .Replace(Environment.NewLine + "  ", Environment.NewLine + Environment.NewLine);
        }

        public static string TruncString(string source, int length)
        {
            if (source.Length > length)
                return source.Substring(0, length);
            else
                return source;
        }

        public static string TruncStringWithEllipsis(string source, int length)
        {
            if (source.Length > length)
                return source.Substring(0, length) + "...";
            else
                return source;
        }
    }

}
