using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if Win8
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
#elif WP
using System.Windows.Media;
using System.Windows.Media.Imaging;
#endif


namespace MangaViewer.Common.Helper
{
    public class ColorHelper
    {
        public static Color HexToColor(string hexValue)
        {
            try
            {
                hexValue = hexValue.Replace("#", string.Empty);
                byte position = 0;
                byte alpha = System.Convert.ToByte("ff", 16);

                if (hexValue.Length == 8)
                {
                    // get the alpha channel value
                    alpha = System.Convert.ToByte(hexValue.Substring(position, 2), 16);
                    position = 2;
                }

                // get the red value
                byte red = System.Convert.ToByte(hexValue.Substring(position, 2), 16);
                position += 2;

                // get the green value
                byte green = System.Convert.ToByte(hexValue.Substring(position, 2), 16);
                position += 2;

                // get the blue value
                byte blue = System.Convert.ToByte(hexValue.Substring(position, 2), 16);

                // create the Color object
                Color color = Color.FromArgb(alpha, red, green, blue);

                // create the SolidColorBrush object
                return color;
            }
            catch
            {
                return Color.FromArgb(255, 251, 237, 187);
            }
        }

    }
}
