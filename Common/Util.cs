using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;

namespace ChessGUI.Common
{
    internal class Util
    {
        public static BitmapSource BitmapFilter(BitmapSource bitmap, byte R, byte G, byte B, byte Alpha = 0xff)
        {
            FormatConvertedBitmap fb = new();
            fb.BeginInit();
            fb.Source = bitmap;
            fb.EndInit();
            var stride = (bitmap.PixelWidth * bitmap.Format.BitsPerPixel + 7) / 8;
            byte[] buf = new byte[fb.PixelHeight * stride];
            fb.CopyPixels(Int32Rect.Empty, buf, stride, 0);
            for (long ic = 0; ic < buf.LongLength; ic += 4)
            {
                if (buf[ic] == R && buf[ic + 1] == G && buf[ic + 2] == B && buf[ic + 3] == Alpha)
                {
                    buf[ic] = 0x00;
                    buf[ic + 1] = 0x00;
                    buf[ic + 2] = 0x00;
                    buf[ic + 3] = 0x00;//透明处理
                }
            }
            return BitmapSource.Create(fb.PixelWidth, fb.PixelHeight, fb.DpiX, fb.DpiY, fb.Format, null, buf, stride);
        }
    }
}
