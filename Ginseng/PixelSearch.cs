using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UFO202
{
    class ColorSearch
    {
        /*
        public Rectangle Screen = new Rectangle(0, 0, 370, 225);
        public Point result = new Point(-1, -1);
        public int X = 0;
        public int Y = 0;
        public ColorYellow=0xFFDD44;
        */
        public static Point PixelSearch(Rectangle rect, int PixelColor, int Shade_Variation)
        {
            Color Pixel_Color = Color.FromArgb(PixelColor);

            Point Pixel_Coords = new Point(-1, -1);
            Bitmap RegionIn_Bitmap = CaptureScreenRegion(rect);
            BitmapData RegionIn_BitmapData = RegionIn_Bitmap.LockBits(new Rectangle(0, 0, RegionIn_Bitmap.Width, RegionIn_Bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int[] Formatted_Color = new int[3] { Pixel_Color.B, Pixel_Color.G, Pixel_Color.R }; //bgr

            unsafe
            {
                for (int y = 0; y < RegionIn_BitmapData.Height; y++)
                {
                    byte* row = (byte*)RegionIn_BitmapData.Scan0 + (y * RegionIn_BitmapData.Stride);

                    for (int x = 0; x < RegionIn_BitmapData.Width; x++)
                    {
                        if (row[x * 3] >= (Formatted_Color[0] - Shade_Variation) & row[x * 3] <= (Formatted_Color[0] + Shade_Variation)) //blue
                        {
                            if (row[(x * 3) + 1] >= (Formatted_Color[1] - Shade_Variation) & row[(x * 3) + 1] <= (Formatted_Color[1] + Shade_Variation)) //green
                            {
                                if (row[(x * 3) + 2] >= (Formatted_Color[2] - Shade_Variation) & row[(x * 3) + 2] <= (Formatted_Color[2] + Shade_Variation)) //red
                                {
                                    Pixel_Coords = new Point(x + rect.X, y + rect.Y);
                                    goto end;
                                }
                            }
                        }
                    }
                }
            }

        end:
            return Pixel_Coords;
        }

        public static Point []PixelSearch_For_All_Graph(Rectangle rect, int PixelColor, int Shade_Variation)
        {
            Color Pixel_Color = Color.FromArgb(PixelColor);
            Point initial = new Point(-1, -1);
            Point[] PointResult = new Point[10] { initial, initial, initial, initial, initial, initial, initial, initial, initial, initial };
            Bitmap RegionIn_Bitmap = CaptureScreenRegion(rect);
            BitmapData RegionIn_BitmapData = RegionIn_Bitmap.LockBits(new Rectangle(0, 0, RegionIn_Bitmap.Width, RegionIn_Bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            byte i=0;
            int[] Formatted_Color = new int[3] { Pixel_Color.B, Pixel_Color.G, Pixel_Color.R }; //bgr

            unsafe
            {
                for (int y = 0; y < RegionIn_BitmapData.Height; y++)
                {
                    byte* row = (byte*)RegionIn_BitmapData.Scan0 + (y * RegionIn_BitmapData.Stride);

                    for (int x = 0; x < RegionIn_BitmapData.Width; x++)
                    {
                        if (row[x * 3] >= (Formatted_Color[0] - Shade_Variation) & row[x * 3] <= (Formatted_Color[0] + Shade_Variation)) //blue
                        {
                            if (row[(x * 3) + 1] >= (Formatted_Color[1] - Shade_Variation) & row[(x * 3) + 1] <= (Formatted_Color[1] + Shade_Variation)) //green
                            {
                                if (row[(x * 3) + 2] >= (Formatted_Color[2] - Shade_Variation) & row[(x * 3) + 2] <= (Formatted_Color[2] + Shade_Variation)) //red
                                {
                                    PointResult[i] = new Point(x + rect.X, y + rect.Y);
                                    if (i < 10)
                                    { i++; }
                                    else
                                    { goto end; }
                                }
                            }
                        }
                    }
                }
            }

        end:
            return PointResult;
        }
        private static Bitmap CaptureScreenRegion(Rectangle rect)
        {
            Bitmap BMP = new Bitmap(rect.Width, rect.Height, PixelFormat.Format24bppRgb);
            Graphics GFX = System.Drawing.Graphics.FromImage(BMP);
            GFX.CopyFromScreen(rect.X, rect.Y, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);
            return BMP;
        }
    }
}
