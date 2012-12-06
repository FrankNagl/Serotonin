// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2012
// admin@franknagl.de
//
//Some source code fragments from AForge.NET framework
// http://code.google.com/p/aforge/
//
namespace SBIP.Helper
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    /// <summary>
    /// Helper class for drawhing and modifying lines in a bitmap.
    /// </summary>
    public static class Drawing24Or32Bpp
    {
        /// <summary>
        /// Draws a cross at <paramref name="point"/> into the
        /// <paramref name="bitmap"/>.
        /// </summary>
        /// <param name="bitmap">The image to draw in.</param>
        /// <param name="point">The coordinate to draw the cross.</param>
        /// <param name="color">The color of the cross.</param>
        /// <param name="lineStrength">The line strength of the cross.</param>
        /// <param name="crossRadius">The radius of the cross.</param>
        /// <exception cref="NotSupportedException">
        /// The source image has not 24 or 32 bpp format.</exception>
        public static void DrawCross(
            Bitmap bitmap,
            Point point,
            Color color,
            float lineStrength,
            byte crossRadius
            )
        {
            BitmapHelper.CheckPixelFormat(PixelFormatFlags.Color, bitmap.PixelFormat);
            //const float ColorLineStrenght = 3;
            //const byte ColorCrossRadius = 7;
            Graphics g = Graphics.FromImage(bitmap);
            DrawingGraphics.DrawCross(g, point, color, lineStrength, crossRadius);
            g.Dispose();
        }

        /// <summary>Draws a line on the specified image.</summary>
        /// <param name="bitmap">Source bitmap to draw on.</param>
        /// <param name="color">The color value for the line's pixels.</param>
        /// <param name="point1">The first point to connect.</param>
        /// <param name="point2">The second point to connect.</param>
        /// <param name="lineStrength">The pixel strength of the line.</param>
        /// <exception cref="NotSupportedException">
        /// The source image has not 24 or 32 bpp format.</exception>
        public static void DrawLine(
            Bitmap bitmap,
            Color color,
            Point point1,
            Point point2,
            int lineStrength)
        {
            Rectangle r = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData data = 
                bitmap.LockBits(r, ImageLockMode.WriteOnly, bitmap.PixelFormat);
            DrawLine(data, color, point1, point2, lineStrength);
            bitmap.UnlockBits(data);
        }

        /// <summary>
        /// Draws a line on the specified image.
        /// </summary>
        /// <param name="data">Source bitmap data to draw on.</param>
        /// <param name="color">The color value for the line's pixels.</param>
        /// <param name="point1">The first point to connect.</param>
        /// <param name="point2">The second point to connect.</param>
        /// <param name="lineStrength">The pixel strength of the line.</param>
        /// <exception cref="NotSupportedException">
        /// The source image has not 24 or 32 bpp format.</exception>
        public static void DrawLine(
            BitmapData data,
            Color color,
            Point point1,
            Point point2,
            int lineStrength)
        {
            BitmapHelper.CheckPixelFormat(PixelFormatFlags.Color, data.PixelFormat);            

            // check if there is something to draw
            if (
                ((point1.X < 0) && (point2.X < 0)) ||
                ((point1.Y < 0) && (point2.Y < 0)) ||
                ((point1.X >= data.Width) && (point2.X >= data.Width)) ||
                ((point1.Y >= data.Height) && (point2.Y >= data.Height)))
            {
                // nothing to draw
                return;
            }

            BitmapHelper.CheckEndPoint(data.Width, data.Height, point1, ref point2);
            BitmapHelper.CheckEndPoint(data.Width, data.Height, point2, ref point1);

            // check again if there is something to draw
            if (
                ((point1.X < 0) && (point2.X < 0)) ||
                ((point1.Y < 0) && (point2.Y < 0)) ||
                ((point1.X >= data.Width) && (point2.X >= data.Width)) ||
                ((point1.Y >= data.Height) && (point2.Y >= data.Height)))
            {
                // nothing to draw
                return;
            }

            int startX = point1.X;
            int startY = point1.Y;
            int stopX = point2.X;
            int stopY = point2.Y;

            // draw the line
            int dx = stopX - startX;
            int dy = stopY - startY;

            if (Math.Abs(dx) >= Math.Abs(dy))
            {
                // the line is more horizontal, we'll plot along the X axis
                float slope = (dx != 0) ? (float)dy / dx : 0;
                int step = (dx > 0) ? 1 : -1;

                // correct dx so last point is included as well
                dx += step;

                // color image
                for (int x = 0; x != dx; x += step)
                {
                    int px = startX + x;
                    int py = (int)(startY + (slope * x));

                    //byte* ptr = (byte*)data.Scan0.ToPointer() + py * stride + px * ps;

                    //ptr[RGBA.R] = color.R;
                    //ptr[RGBA.G] = color.G;
                    //ptr[RGBA.B] = color.B;

                    DrawThickPoint(data, color, new Point(px, py), lineStrength);
                }
            }
            else
            {
                // the line is more vertical, we'll plot along the y axis.
                float slope = (float)dx / dy;
                int step = (dy > 0) ? 1 : -1;

                // correct dy so last point is included as well
                dy += step;
                
                // color image
                for (int y = 0; y != dy; y += step)
                {
                    int px = (int)(startX + (slope * y));
                    int py = startY + y;

                    //byte* ptr = (byte*)data.Scan0.ToPointer() + py * stride + px * ps;

                    //ptr[RGBA.R] = color.R;
                    //ptr[RGBA.G] = color.G;
                    //ptr[RGBA.B] = color.B;

                    DrawThickPoint(data, color, new Point(px, py), lineStrength);
                }
            }
        }

        /// <summary>
        /// Draws a rectangle on the specified color image.
        /// </summary>
        /// <param name="data">Source bitmap data to draw on.</param>
        /// <param name="color">The color for the rectangle's pixels.</param>
        /// <param name="rect">The rectangle to draw.</param>
        /// <param name="lineStrength">The pixel strength of the line.</param>
        public static void DrawRectangle(
            BitmapData data,
            Color color,
            Rectangle rect,
            int lineStrength)
        {
            Point end = new Point(rect.Width + rect.X, rect.Height + rect.Y);

            // TOP line
            DrawLine(
                data,
                color,
                rect.Location,
                new Point(end.X, rect.Y),
                lineStrength);

            // BOTTOM line
            DrawLine(
                data,
                color,
                new Point(rect.X, end.Y),
                end,
                lineStrength);

            // LEFT line
            DrawLine(
                data,
                color,
                rect.Location,
                new Point(rect.X, end.Y),
                lineStrength);

            // RIGHT line
            DrawLine(
                data,
                color,
                new Point(end.X, rect.Y), 
                end,
                lineStrength);

            //// DIAGONAL
            //DrawLine(
            //    data, 
            //    gray, 
            //    r.Location,
            //    end,
            //    lineStrength);
        }

        /// <summary>Draws a point with a specified diameter.</summary>
        /// <param name="data">Source bitmap data to draw on.</param>
        /// <param name="color">The color value for the line's pixels.</param>
        /// <param name="coord">The point coordinate to draw.</param>
        /// <param name="diameter">The diameter of the point to draw.</param>
        /// <exception cref="NotSupportedException">
        /// The source image has not 24 or 32 bpp format.</exception>
        public static unsafe void DrawThickPoint(
            BitmapData data,
            Color color,
            Point coord,
            int diameter)
        {
            int radius = diameter / 2;
            // image dimension
            int ps = Image.GetPixelFormatSize(data.PixelFormat) / 8;
            byte* ptr = (byte*)data.Scan0.ToPointer() + coord.Y * data.Stride + coord.X * ps;

            // for each kernel row
            for (int i = 0; i < diameter; i++)
            {
                int ir = i - radius;
                int ty = coord.Y + ir;

                // skip row
                if (ty < 0)
                    continue;
                // break
                if (ty >= data.Height)
                    break;

                // for each kernel column
                for (int j = 0; j < diameter; j++)
                {
                    int jr = j - radius;
                    int tx = coord.X + jr;

                    // skip column
                    if (tx < 0)
                        continue;

                    if (tx < data.Width)
                    {
                        ptr[ir*data.Stride + jr*ps + RGBA.R] = color.R;
                        ptr[ir*data.Stride + jr*ps + RGBA.G] = color.G;
                        ptr[ir*data.Stride + jr*ps + RGBA.B] = color.B;
                        if (ps == 4)
                        {
                            ptr[ir*data.Stride + jr*ps + RGBA.A] = color.A;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Replaces all pixels of <paramref name="oldColor">specified color</paramref> 
        /// with a <paramref name="newColor">new color</paramref>.
        /// </summary>
        /// <param name="bitmap">Source bitmap (24 or 32 bpp) to replace color.</param>
        /// <param name="oldColor">The old color to replace.</param>
        /// <param name="newColor">The new color to use.</param>
        /// <exception cref="NotSupportedException">
        /// The source image has not 24 or 32 bpp format.</exception>
        public static void ReplaceColor(
            Bitmap bitmap,
            Color oldColor,
            Color newColor)
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData data = bitmap.LockBits(
                rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
            ReplaceColor(data, oldColor, newColor);
            bitmap.UnlockBits(data);
        }

        /// <summary>
        /// Replaces all pixels of <paramref name="oldColor">specified color</paramref> 
        /// with a <paramref name="newColor">new color</paramref>.
        /// </summary>
        /// <param name="data">Source bitmap (24 or 32 bpp) data to replace color.</param>
        /// <param name="oldColor">The old color to replace.</param>
        /// <param name="newColor">The new color to use.</param>
        /// <exception cref="NotSupportedException">
        /// The source image has not 24 or 32 bpp format.</exception>
        public static unsafe void ReplaceColor(
            BitmapData data,
            Color oldColor,
            Color newColor)
        {
            BitmapHelper.CheckPixelFormat(PixelFormatFlags.Color, data.PixelFormat);
            // pixel size
            int ps = Image.GetPixelFormatSize(data.PixelFormat) / 8;
            int offset = data.Stride - data.Width * ps;
            byte* ptr = (byte*)data.Scan0.ToPointer();

            if (ps == 4)
            {
                // for each line
                for (int y = 0; y < data.Height; y++)
                {
                    // for each channel of each pixel in line
                    for (int x = 0; x < data.Width; x++, ptr += ps)
                    {
                        if (ptr[RGBA.R] == oldColor.R &&
                            ptr[RGBA.G] == oldColor.G &&
                            ptr[RGBA.B] == oldColor.B &&
                            ptr[RGBA.A] == oldColor.A)
                        {
                            ptr[RGBA.R] = newColor.R;
                            ptr[RGBA.G] = newColor.G;
                            ptr[RGBA.B] = newColor.B;
                            ptr[RGBA.A] = newColor.A;
                        }
                    }
                }
            }
            else
            {
                // for each line
                for (int y = 0; y < data.Height; y++)
                {
                    // for each channel of each pixel in line
                    for (int x = 0; x < data.Width; x++, ptr += ps)
                    {
                        if (ptr[RGBA.R] == oldColor.R &&
                            ptr[RGBA.G] == oldColor.G &&
                            ptr[RGBA.B] == oldColor.B)
                        {
                            ptr[RGBA.R] = newColor.R;
                            ptr[RGBA.G] = newColor.G;
                            ptr[RGBA.B] = newColor.B;
                        }
                    }
                    ptr += offset;
                }                
            }
        }
    }
}
