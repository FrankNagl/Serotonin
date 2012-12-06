// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
namespace SBIP.Helper
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    /// <summary>
    /// Helper class for different bitmap operations, 
    /// e.g. cloning, resetting, etc.
    /// See function details for more information.
    /// </summary>
    public static class BitmapHelper
    {
        /// <summary>Helper function for drawing a line: 
        /// Check end point and make sure it is in the image.</summary>
        /// <param name="width">Image's width.</param>
        /// <param name="height">Image's height.</param>
        /// <param name="start">Start point of the line.</param>
        /// <param name="end">End point of the line.</param>
        public static void CheckEndPoint(
            int width, int height, Point start, ref Point end)
        {
            if (end.X >= width)
            {
                int newEndX = width - 1;

                double c = (double)(newEndX - start.X) / (end.X - start.X);

                end.Y = (int)(start.Y + c * (end.Y - start.Y));
                end.X = newEndX;
            }

            if (end.Y >= height)
            {
                int newEndY = height - 1;

                double c = (double)(newEndY - start.Y) / (end.Y - start.Y);

                end.X = (int)(start.X + c * (end.X - start.X));
                end.Y = newEndY;
            }

            if (end.X < 0)
            {
                double c = (double)(0 - start.X) / (end.X - start.X);

                end.Y = (int)(start.Y + c * (end.Y - start.Y));
                end.X = 0;
            }

            if (end.Y < 0)
            {
                double c = (double)(0 - start.Y) / (end.Y - start.Y);

                end.X = (int)(start.X + c * (end.X - start.X));
                end.Y = 0;
            }
        }

        /// <summary>
        /// Checks for correct pixel format. If wrong, an 
        /// <see cref="ArgumentException"/> is thrown.
        /// </summary>
        /// <param name="supportedPixelFormats">The supported pixel formats.
        /// </param>
        /// <param name="format">The pixel format to check.</param>
        /// <exception cref="NotSupportedException">
        /// No supported pixel format.</exception>
        public static void CheckPixelFormat(
            PixelFormatFlags supportedPixelFormats,
            PixelFormat format)
        {
            string errorMsg = "No supported source pixel format.";
            switch (supportedPixelFormats)
            {
                case PixelFormatFlags.None:
                    errorMsg = "No supported pixel format.";
                    break;
                case PixelFormatFlags.Format8BppIndexed:
                    errorMsg = "Source image can be grayscale (8 bpp) image only.";
                    break;
                case PixelFormatFlags.Format24BppRgb:
                    errorMsg = "Source image can be color (24 bpp) image only.";
                    break;
                case (PixelFormatFlags.Format32BppArgb | PixelFormatFlags.Format32BppRgb):
                    errorMsg = "Source image can be color (32 bpp) image only.";
                    break;
                case PixelFormatFlags.Format24And8Bpp:
                    errorMsg = "Source image can be color (24 bpp) image" +
                        " or grayscale (8 bpp) image only.";
                    break;
                case PixelFormatFlags.Format32And8Bpp:
                    errorMsg = "Source image can be color (32 bpp) image" +
                        " or grayscale (8 bpp) image only.";
                    break;
                case PixelFormatFlags.Color:
                    errorMsg = "Source image can be color (24 or 32 bpp) image only.";
                    break;
                case PixelFormatFlags.All:
                    errorMsg = "Source image can be color (24 or 32 bpp) image" +
                        " or grayscale (8 bpp) image only.";
                    break;
            }

            PixelFormatFlags flags;
            switch (format)
            {
                case PixelFormat.Format8bppIndexed:
                    flags = PixelFormatFlags.Format8BppIndexed;
                    break;
                case PixelFormat.Format24bppRgb:
                    flags = PixelFormatFlags.Format24BppRgb;
                    break;
                case PixelFormat.Format32bppArgb:
                    flags = PixelFormatFlags.Format32BppArgb;
                    break;
                case PixelFormat.Format32bppRgb:
                    flags = PixelFormatFlags.Format32BppRgb;
                    break;
                default:
                    flags = PixelFormatFlags.None;
                    break;
            }

            if ((flags & supportedPixelFormats) == 0)
            {
                throw new NotSupportedException(errorMsg);
            }
        }

        /// <summary>
        /// Clones the bitmap with the correct pixel format.
        /// </summary>
        /// <param name="source">Source image.</param>
        /// <param name="width">The new width of the result image.</param>
        /// <param name="height">The new height of the result image.</param>
        /// <param name="format">Pixel format of the result image.</param>
        /// <returns>
        /// Returns clone of the source image with specified pixel format.
        /// </returns>
        /// <remarks>
        /// The original 
        /// <see cref="Bitmap.Clone(System.Drawing.Rectangle, System.Drawing.Imaging.PixelFormat)">
        /// Bitmap.Clone()</see>
        /// does not produce the desired result - it does not create a clone 
        /// with specified pixel format.
        /// More of it, the original method does not create an actual clone - 
        /// it does not create a copy of the image.
        /// </remarks>
        public static Bitmap CloneBitmap(
            Bitmap source, 
            int width, 
            int height, 
            PixelFormat format)
        {
            if (!IsColorImage(source.PixelFormat))
            {
                throw new ArgumentException(
                    "Source image can be color (24 or 32 bpp) image only");                
            }
            // create new image with desired pixel format
            Bitmap bitmap = new Bitmap(width, height, format);

            // draw source image on the new one using Graphics
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawImage(source, 0, 0, width, height);
            g.Dispose();

            return bitmap;
        }

        /// <summary>
        /// Determines whether the specified format represents a color image.
        /// </summary>
        /// <param name="format">The specified image format.</param>
        /// <returns>
        ///   <c>true</c> if the specified format represents a color image; 
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool IsColorImage(PixelFormat format)
        {
            // check image format
            if (format != PixelFormat.Format24bppRgb &&
                format != PixelFormat.Format32bppArgb &&
                format != PixelFormat.Format32bppRgb &&
                format != PixelFormat.Format32bppPArgb)
            {
                return false;
            }
            return true;
        }

        /// <summary>All pixel values will be set to zero (black).</summary>
        /// <param name="bitmap">The bitmap to reset.</param>
        public static void ResetBitmap(Bitmap bitmap)
        {
            BitmapData data = bitmap.LockBits(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.WriteOnly,
                    bitmap.PixelFormat);

            ResetBitmapData(data);
            bitmap.UnlockBits(data);
        }

        /// <summary>All pixel values will be set to zero (black).</summary>
        /// <param name="data">The bitmap data to reset.</param>
        public static unsafe void ResetBitmapData(BitmapData data)
        {
            byte* ptr = (byte*)data.Scan0.ToPointer();
            // for each line
            for (int y = 0; y < data.Height; y++)
            {
                // for each channel of each pixel in line
                for (int x = 0; x < data.Stride; x++, ptr++)
                {
                    *ptr = 0;
                }
            }
        }
    }
}
