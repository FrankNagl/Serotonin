// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2010
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
    public static class Drawing8Bpp
    {
        /// <summary>
        /// Draws a line on the specified grayscale (8bpp) image.
        /// </summary>
        /// <param name="bitmap">Source bitmap to draw on.</param>
        /// <param name="gray">The gray value for the line's pixels.</param>
        /// <param name="point1">The first point to connect.</param>
        /// <param name="point2">The second point to connect.</param>
        /// <param name="lineStrength">The pixel strength of the line.</param>
        /// <exception cref="NotSupportedException">
        /// The source image has not 8 bpp format.</exception>
        public static void DrawLine(
            Bitmap bitmap,
            byte gray,
            Point point1,
            Point point2,
            int lineStrength)
        {
            Rectangle r = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData data = 
                bitmap.LockBits(r, ImageLockMode.WriteOnly, bitmap.PixelFormat);
            DrawLine(data, gray, point1, point2, lineStrength);
            bitmap.UnlockBits(data);
        }

        /// <summary>
        /// Draws a line on the specified grayscale (8bpp) image.
        /// </summary>
        /// <param name="data">Source bitmap data to draw on.</param>
        /// <param name="gray">The gray value for the line's pixels.</param>
        /// <param name="point1">The first point to connect.</param>
        /// <param name="point2">The second point to connect.</param>
        /// <param name="lineStrength">The pixel strength of the line.</param>
        /// <exception cref="NotSupportedException">
        /// The source image has not 8 bpp format.</exception>
        public static void DrawLine(
            BitmapData data,
            byte gray,
            Point point1,
            Point point2,
            int lineStrength)
        {
            BitmapHelper.CheckPixelFormat(
                PixelFormatFlags.Format8BppIndexed, data.PixelFormat);

            // image dimension
            int imageWidth = data.Width;
            int imageHeight = data.Height;

            // check if there is something to draw
            if (
                ((point1.X < 0) && (point2.X < 0)) ||
                ((point1.Y < 0) && (point2.Y < 0)) ||
                ((point1.X >= imageWidth) && (point2.X >= imageWidth)) ||
                ((point1.Y >= imageHeight) && (point2.Y >= imageHeight)))
            {
                // nothing to draw
                return;
            }

            BitmapHelper.CheckEndPoint(
                imageWidth, imageHeight, point1, ref point2);
            BitmapHelper.CheckEndPoint(
                imageWidth, imageHeight, point2, ref point1);

            // check again if there is something to draw
            if (
                ((point1.X < 0) && (point2.X < 0)) ||
                ((point1.Y < 0) && (point2.Y < 0)) ||
                ((point1.X >= imageWidth) && (point2.X >= imageWidth)) ||
                ((point1.Y >= imageHeight) && (point2.Y >= imageHeight)))
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

                // using for grayscale image
                for (int x = 0; x != dx; x += step)
                {
                    int px = startX + x;
                    int py = (int)(startY + (slope * x));

                    //byte* ptr = (byte*)data.Scan0.ToPointer() + py * stride + px;
                    //*ptr = gray;
                    DrawThickPoint(data, gray, new Point(px, py), lineStrength);
                }
            }
            else
            {
                // the line is more vertical, we'll plot along the y axis.
                float slope = (float)dx / dy;
                int step = (dy > 0) ? 1 : -1;

                // correct dy so last point is included as well
                dy += step;

                // using for grayscale image
                for (int y = 0; y != dy; y += step)
                {
                    int px = (int)(startX + (slope * y));
                    int py = startY + y;

                    //byte* ptr = (byte*)data.Scan0.ToPointer() + py * stride + px;
                    //*ptr = gray;
                    DrawThickPoint(data, gray, new Point(px, py), lineStrength);
                }
            }
        }

        /// <summary>
        /// Draws a reactangle on the specified grayscale (8bpp) image.
        /// </summary>
        /// <param name="data">Source bitmap data to draw on.</param>
        /// <param name="gray">The gray value for the pixels.</param>
        /// <param name="rect">The rectangle to draw.</param>
        /// <param name="lineStrength">The pixel strength of the line.</param>
        /// <exception cref="NotSupportedException">
        /// The source image has not 8 bpp format.</exception>
        public static void DrawRectangle(
            BitmapData data,
            byte gray,
            Rectangle rect,
            int lineStrength)
        {
            Point end = new Point(rect.Width + rect.X, rect.Height + rect.Y);

            // TOP line
            DrawLine(
                data,
                gray,
                rect.Location,
                new Point(end.X, rect.Y),
                lineStrength);

            // BOTTOM line
            DrawLine(
                data,
                gray,
                new Point(rect.X, end.Y),
                end,
                lineStrength);

            // LEFT line
            DrawLine(
                data,
                gray,
                rect.Location,
                new Point(rect.X, end.Y),
                lineStrength);

            // RIGHT line
            DrawLine(
                data,
                gray,
                new Point(end.X, rect.Y),
                end,
                lineStrength);

            //// DIAGONAL
            //DrawLine(
            //    data, 
            //    gray, 
            //    r.Location,
            //    end);
        }

        /// <summary>Draws a point with a specified diameter.</summary>
        /// <param name="data">Source bitmap data to draw on.</param>
        /// <param name="gray">The gray value for the line's pixels.</param>
        /// <param name="coord">The point coordinate to draw.</param>
        /// <param name="diameter">The diameter of the point to draw.</param>
        /// <exception cref="NotSupportedException">
        /// The source image has not 8 bpp format.</exception>
        public static unsafe void DrawThickPoint(
            BitmapData data,
            byte gray,
            Point coord,
            int diameter)
        {
            BitmapHelper.CheckPixelFormat(
                PixelFormatFlags.Format8BppIndexed, data.PixelFormat);

            int radius = diameter / 2;
            byte* ptr = 
                (byte*)data.Scan0.ToPointer() + coord.Y * data.Stride + coord.X;

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
                        ptr[ir * data.Stride + jr] = gray;
                    }
                }
            }
        }

        /// <summary>
        /// Replaces all old pixel values with new pixel values.
        /// </summary>
        /// <param name="bitmap">Source bitmap (8bpp) to draw on.</param>
        /// <param name="oldValue">The old pixel value to replace.</param>
        /// <param name="newValue">The new value for replacing the old pixels.
        /// </param>
        /// <exception cref="NotSupportedException">
        /// The source image has not 8 bpp format.</exception>
        public static void ReplacePixel(
            Bitmap bitmap,
            byte oldValue,
            byte newValue)
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData data = bitmap.LockBits(
                rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
            ReplacePixel(data, oldValue, newValue);
            bitmap.UnlockBits(data);
        }

        /// <summary>
        /// Replaces all old pixel values with new pixel values.
        /// </summary>
        /// <param name="data">Source bitmap (8bpp) data to draw on.</param>
        /// <param name="oldValue">The old pixel value to replace.</param>
        /// <param name="newValue">The new value for replacing the old pixels.
        /// </param>
        /// <exception cref="NotSupportedException">
        /// The source image has not 8 bpp format.</exception>
        public static unsafe void ReplacePixel(
            BitmapData data,
            byte oldValue,
            byte newValue)
        {
            BitmapHelper.CheckPixelFormat(
                PixelFormatFlags.Format8BppIndexed, data.PixelFormat);

            byte* dst2 = (byte*)data.Scan0.ToPointer();

            for (int i = 0; i < data.Stride * data.Height; i++)
            {
                if (dst2[i] == oldValue)
                {
                    dst2[i] = newValue;
                }
            }
        }

        /// <summary>
        /// Replaces all old pixel values with new pixel values inside a
        /// rectangle of the bitmap data.
        /// </summary>
        /// <param name="data">Source bitmap (8bpp) data to draw on.</param>
        /// <param name="rect">The rectangle of the bitmap data to process.
        /// </param>
        /// <param name="oldValue">The old pixel value to replace.</param>
        /// <param name="newValue">The new value for replacing the old pixels.
        /// </param>
        /// <exception cref="NotSupportedException">
        /// The source image has not 8 bpp format.</exception>
        public static unsafe void ReplacePixelInRect(
            BitmapData data,
            Rectangle rect,
            byte oldValue,
            byte newValue)
        {
            BitmapHelper.CheckPixelFormat(
                PixelFormatFlags.Format8BppIndexed, data.PixelFormat);
         
            byte* ptr = (byte*)data.Scan0.ToPointer();
            ptr += data.Stride * rect.Y + rect.X;

            // for each line
            for (int y = rect.Y; y < rect.Y + rect.Height; y++)
            {
                // for each channel of each pixel in line
                for (int x = rect.X; x < rect.X + rect.Width; x++, ptr++)
                {
                    if (*ptr == oldValue)
                    {
                        *ptr = newValue;
                    }
                }
                ptr += data.Stride - rect.X - rect.Width;
            }
        }

        /// <summary>
        /// Replaces all pixel values with the passed <paramref name="background"/>
        /// except pixels with value <paramref name="pixel"/>.
        /// </summary>
        /// <param name="bitmap">Source bitmap (8bpp) to draw on.</param>
        /// <param name="pixel">The pixel value to save.</param>
        /// <param name="background">The value for replacing pixels.</param>
        /// <exception cref="NotSupportedException">
        /// The source image has not 8 bpp format.</exception>
        public static void ReplacePixels(
            Bitmap bitmap,
            byte pixel,
            byte background)
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData data = bitmap.LockBits(
                rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
            ReplacePixels(data, pixel, background);
            bitmap.UnlockBits(data);
        }

        /// <summary>
        /// Replaces all pixel values with the passed <paramref name="background"/>
        /// except pixels with value <paramref name="pixel"/>.
        /// </summary>
        /// <param name="data">Source bitmap data to draw on.</param>
        /// <param name="pixel">The pixel value to save.</param>
        /// <param name="background">The value for replacing pixels.</param>
        /// <exception cref="NotSupportedException">
        /// The source image has not 8 bpp format.</exception>
        public static unsafe void ReplacePixels(
            BitmapData data, 
            byte pixel, 
            byte background)
        {
            BitmapHelper.CheckPixelFormat(
                PixelFormatFlags.Format8BppIndexed, data.PixelFormat);

            byte* dst2 = (byte*)data.Scan0.ToPointer();

            for (int i = 0; i < data.Stride * data.Height; i++)
            {
                if (dst2[i] != pixel)
                {
                    dst2[i] = background;
                }
            }
        }
    }
}
