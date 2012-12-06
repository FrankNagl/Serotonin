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
    using System.IO;

    /// <summary>
    /// Helper class for different Image converts. 
    /// See function details for more information.
    /// </summary>
    public static class BitmapConverter
    {
        #region enums, statics and constants

        #pragma warning disable 649
        // ReSharper disable InconsistentNaming
        private static uint BI_RGB;
        private static uint DIB_RGB_COLORS;
        // ReSharper restore InconsistentNaming
        #pragma warning restore 649

        private const int SRCCOPY = 0x00CC0020;

        [System.Runtime.InteropServices.StructLayout(
            System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct Bitmapinfo
        {
            public uint biSize;
            public int biWidth, biHeight;
            public short biPlanes, biBitCount;
            public uint biCompression, biSizeImage;
            public int biXPelsPerMeter, biYPelsPerMeter;
            public uint biClrUsed, biClrImportant;
            [System.Runtime.InteropServices.MarshalAs(
                System.Runtime.InteropServices.UnmanagedType.ByValArray, 
                SizeConst = 256)]
            public uint[] cols;
        }

        #endregion enums, statics and constants

        /// <summary>
        /// Converts 32 bpp bitmap to 8 bpp bitmap by mean value of all 3 color 
        /// channels.
        /// </summary>
        /// <param name="source">The bitmap, which will be converted.</param>
        /// <returns>Result (8 bpp) bitmap.</returns>
        public static Bitmap ARGBTo8Bpp(Bitmap source)
        {
            // check image format
            if (source.PixelFormat != PixelFormat.Format32bppRgb ||
                source.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ArgumentException(
                    "Source image can be color (32 bpp) image only");

            int w = source.Width;
            int h = source.Height;
            Rectangle rect = new Rectangle(0, 0, w, h);

            Bitmap destination = new Bitmap(w, h, PixelFormat.Format8bppIndexed);
            BitmapData dstData = destination.LockBits
                (rect, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // lock source bitmap data
            BitmapData srcData = source.LockBits(
                rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            var dstOffset = dstData.Stride - w;

            // process image
            unsafe
            {
                byte* src = (byte*)srcData.Scan0.ToPointer();
                byte* dst = (byte*)dstData.Scan0.ToPointer();
                // for each line
                for (int y = 0; y < h; y++)
                {
                    // for each pixel in line
                    for (int x = 0; x < w; x++, src += 4, dst++)
                    {
                        int avg = src[RGBA.R] + src[RGBA.G] + src[RGBA.B];
                        avg /= 3;
                        *dst = (byte)avg;
                    }
                    dst += dstOffset;
                }
            }
            // unlock destination image
            source.UnlockBits(srcData);
            destination.UnlockBits(dstData);
            Serotonin.Helper.ColorPalette.SetColorPaletteToGray(destination);

            return destination;
        }

        /// <summary>
        /// Converts 32 bpp bitmap to 24 bpp bitmap.
        /// </summary>
        /// <param name="source">The bitmap, which will be converted.</param>
        /// <returns>Result (24 bpp) bitmap.</returns>
// ReSharper disable InconsistentNaming
        public static Bitmap ARGBToRGB(Bitmap source)
// ReSharper restore InconsistentNaming
        {
            BitmapHelper.CheckPixelFormat(
                PixelFormatFlags.Format32BppArgb | PixelFormatFlags.Format32BppRgb, 
                source.PixelFormat);

            int w = source.Width;
            int h = source.Height;
            Rectangle rect = new Rectangle(0, 0, w, h);

            Bitmap destination = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            BitmapData dstData = destination.LockBits
                (rect, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            // lock source bitmap data
            BitmapData srcData = source.LockBits(
                rect, ImageLockMode.ReadOnly, source.PixelFormat);

            var dstOffset = dstData.Stride - w * 3;

            // process image
            unsafe
            {
                byte* src = (byte*)srcData.Scan0.ToPointer();
                byte* dst = (byte*)dstData.Scan0.ToPointer();
                // for each line
                for (int y = 0; y < h; y++)
                {
                    // for each pixel in line
                    for (int x = 0; x < w; x++, src+=4, dst += 3)
                    {
                        dst[RGBA.R] = src[RGBA.R];
                        dst[RGBA.G] = src[RGBA.G];
                        dst[RGBA.B] = src[RGBA.B];
                    }
                    dst += dstOffset;
                }
            }
            // unlock destination image
            source.UnlockBits(srcData);
            destination.UnlockBits(dstData);

            return destination;
        }

        /// <summary>Copies bitmap data to a new bitmap object.</summary>
        /// <param name="srcData">The source bitmap data to copy.</param>
        /// <returns>The new bitmap of the copied bitmp data.</returns>
        public static unsafe Bitmap BitmapDataToBitmap(BitmapData srcData)
        {
            Rectangle r = new Rectangle(0, 0, srcData.Width, srcData.Height);
            Bitmap b = new Bitmap(srcData.Width, srcData.Height, srcData.PixelFormat);
            BitmapData dstData = b.LockBits(r, ImageLockMode.WriteOnly, b.PixelFormat);
            byte* src = (byte*)srcData.Scan0.ToPointer();
            byte* dst = (byte*)dstData.Scan0.ToPointer();

            for (int i = 0; i < srcData.Stride * srcData.Height; i++)
            {
                dst[i] = src[i];
            }

            b.UnlockBits(dstData);
            return b;
        }

        /// <summary>
        /// Copies a array of byte values into a bitmap.
        /// </summary>
        /// <param name="byteArray">The byte array.</param>
        /// <returns>The generated bitmap.</returns>
        public static Bitmap BitmapFromByteArray(byte[] byteArray)
        {
            Bitmap b;
            using (MemoryStream s = new MemoryStream(byteArray))
            {
                b = new Bitmap(s);
            }
            return b;
        }

        /// <summary>
        /// Converts 8 bpp bitmap to 24 bpp bitmap.
        /// </summary>
        /// <param name="source">The bitmap, which will be converted.</param>
        /// <returns>Result (24 bpp) bitmap.</returns>
        public static Bitmap GrayscaleToRGB(Bitmap source)
        {
            // check image format
            if (source.PixelFormat != PixelFormat.Format8bppIndexed)
                throw new ArgumentException(
                    "Source image can be grayscale (8 bpp) image only.");

            int w = source.Width;
            int h = source.Height;
            Rectangle rect = new Rectangle(0, 0, w, h);

            Bitmap destination = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            BitmapData dstData = destination.LockBits
                (rect, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            // lock source bitmap data
            BitmapData srcData = source.LockBits(
                rect, ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);

            var srcOffset = srcData.Stride - w;
            var dstOffset = dstData.Stride - w * 3;

            // process image
            unsafe
            {
                byte* src = (byte*)srcData.Scan0.ToPointer();
                byte* dst = (byte*)dstData.Scan0.ToPointer();
                // for each line
                for (int y = 0; y < h; y++)
                {
                    // for each pixel in line
                    for (int x = 0; x < w; x++, src++, dst += 3)
                    {
                        dst[RGBA.R] = *src;
                        dst[RGBA.G] = *src;
                        dst[RGBA.B] = *src;
                    }
                    src += srcOffset;
                    dst += dstOffset;
                }
            }
            // unlock destination image
            source.UnlockBits(srcData);
            destination.UnlockBits(dstData);

            return destination;
        }

        /// <summary>
        /// Converts 24 bpp bitmap to 8 bpp bitmap by mean value of all 3 channels.
        /// </summary>
        /// <param name="source">The bitmap, which will be converted.</param>
        /// <returns>Result (8 bpp) bitmap.</returns>
        public static Bitmap RGBTo8Bpp(Bitmap source)
        {
            // check image format
            if (source.PixelFormat != PixelFormat.Format24bppRgb)
                throw new ArgumentException(
                    "Source image can be color (24 bpp) image only");

            int w = source.Width;
            int h = source.Height;
            Rectangle rect = new Rectangle(0, 0, w, h);

            Bitmap destination = new Bitmap(w, h, PixelFormat.Format8bppIndexed);
            BitmapData dstData = destination.LockBits
                (rect, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // lock source bitmap data
            BitmapData srcData = source.LockBits(
                rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            var srcOffset = srcData.Stride - w * 3;
            var dstOffset = dstData.Stride - w;

            // process image
            unsafe
            {
                byte* src = (byte*)srcData.Scan0.ToPointer();
                byte* dst = (byte*)dstData.Scan0.ToPointer();
                // for each line
                for (int y = 0; y < h; y++)
                {
                    // for each pixel in line
                    for (int x = 0; x < w; x++, src += 3, dst++)
                    {
                        int avg = src[RGBA.R] + src[RGBA.G] + src[RGBA.B];
                        avg /= 3;
                        *dst = (byte)avg;
                    }
                    src += srcOffset;
                    dst += dstOffset;
                }
            }
            // unlock destination image
            source.UnlockBits(srcData);
            destination.UnlockBits(dstData);
            Serotonin.Helper.ColorPalette.SetColorPaletteToGray(destination);

            return destination;
        }

        /// <summary>
        /// Draws a bitmap onto the screen.
        /// Original source from: http://www.wischik.com/lu/programmer/1bpp.html
        /// </summary>
        /// <param name="b">The bitmap to draw on the screen.</param>
        /// <param name="x">X screen coordinate.</param>
        /// <param name="y">Y screen coordinate.</param>
        public static void SplashImage(Bitmap b, int x, int y)
        { // Drawing onto the screen is supported by GDI, but not by the Bitmap/Graphics class.
            // So we use interop:
            // (1) Copy the Bitmap into a GDI hbitmap
            IntPtr hbm = b.GetHbitmap();
            // (2) obtain the GDI equivalent of a "Graphics" for the screen
            IntPtr sdc = GetDC(IntPtr.Zero);
            // (3) obtain the GDI equivalent of a "Graphics" for the hbitmap
            IntPtr hdc = CreateCompatibleDC(sdc);
            SelectObject(hdc, hbm);
            // (4) Draw from the hbitmap's "Graphics" onto the screen's "Graphics"
            BitBlt(sdc, x, y, b.Width, b.Height, hdc, 0, 0, SRCCOPY);
            // and do boring GDI cleanup:
            DeleteDC(hdc);
            ReleaseDC(IntPtr.Zero, sdc);
            DeleteObject(hbm);
        }

        /// <summary>
        /// Converts a bitmap into a 1 bpp bitmap of the same dimensions, fast.
        /// Original source from: http://www.wischik.com/lu/programmer/1bpp.html
        /// </summary>
        /// <param name="source">The bitmap, which will be converted.</param>
        /// <returns>A 1 or 8 bpp copy of the source bitmap.</returns>
        public static Bitmap To1Bpp(Bitmap source)
        {
            // Plan: built into Windows GDI is the ability to convert
            // bitmaps from one format to another. Most of the time, this
            // job is actually done by the graphics hardware accelerator card
            // and so is extremely fast. The rest of the time, the job is done by
            // very fast native code.
            // We will call into this GDI functionality from C#. Our plan:
            // (1) Convert our Bitmap into a GDI hbitmap (ie. copy unmanaged->managed)
            // (2) Create a GDI monochrome hbitmap
            // (3) Use GDI "BitBlt" function to copy from hbitmap into monochrome (as above)
            // (4) Convert the monochrone hbitmap into a Bitmap (ie. copy unmanaged->managed)

            int w = source.Width, h = source.Height;
            IntPtr hbm = source.GetHbitmap(); // this is step (1)
            //
            // Step (2): create the monochrome bitmap.
            // "BITMAPINFO" is an interop-struct which we define below.
            // In GDI terms, it's a BITMAPHEADERINFO followed by an array of two RGBQUADs
            Bitmapinfo bmi = new Bitmapinfo();
            bmi.biSize = 40;  // the size of the BITMAPHEADERINFO struct
            bmi.biWidth = w;
            bmi.biHeight = h;
            bmi.biPlanes = 1; // "planes" are confusing. We always use just 1. Read MSDN for more info.
            bmi.biBitCount = 1;
            bmi.biCompression = BI_RGB; // ie. the pixels in our RGBQUAD table are stored as RGBs, not palette indexes
            bmi.biSizeImage = (uint)(((w + 7) & 0xFFFFFFF8) * h / 8);
            bmi.biXPelsPerMeter = 1000000; // not really important
            bmi.biYPelsPerMeter = 1000000; // not really important
            // Now for the colour table.
            const uint ncols = (uint)1 << 1;
            bmi.biClrUsed = ncols;
            bmi.biClrImportant = ncols;
            bmi.cols = new uint[256]; // The structure always has fixed size 256, even if we end up using fewer colours
            bmi.cols[0] = MAKERGB(0, 0, 0); 
            bmi.cols[1] = MAKERGB(255, 255, 255);

            // Now create the indexed bitmap "hbm0"
            IntPtr bits0; // not used for our purposes. It returns a pointer to the raw bits that make up the bitmap.
            IntPtr hbm0 = CreateDIBSection(IntPtr.Zero, ref bmi, DIB_RGB_COLORS, out bits0, IntPtr.Zero, 0);
            //
            // Step (3): use GDI's BitBlt function to copy from original hbitmap into monocrhome bitmap
            // GDI programming is kind of confusing... nb. The GDI equivalent of "Graphics" is called a "DC".
            IntPtr sdc = GetDC(IntPtr.Zero);       // First we obtain the DC for the screen
            // Next, create a DC for the original hbitmap
            IntPtr hdc = CreateCompatibleDC(sdc); SelectObject(hdc, hbm);
            // and create a DC for the monochrome hbitmap
            IntPtr hdc0 = CreateCompatibleDC(sdc); SelectObject(hdc0, hbm0);
            // Now we can do the BitBlt:
            BitBlt(hdc0, 0, 0, w, h, hdc, 0, 0, SRCCOPY);
            // Step (4): convert this monochrome hbitmap back into a Bitmap:
            Bitmap b0 = Image.FromHbitmap(hbm0);
            //
            // Finally some cleanup.
            DeleteDC(hdc);
            DeleteDC(hdc0);
            ReleaseDC(IntPtr.Zero, sdc);
            DeleteObject(hbm);
            DeleteObject(hbm0);
            //
            return b0;
        }

        #region private methods
        
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern int DeleteDC(IntPtr hdc);

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern int BitBlt(
            IntPtr hdcDst, 
            int xDst, 
            int yDst, 
            int w, 
            int h, 
            IntPtr hdcSrc, 
            int xSrc, 
            int ySrc, 
            int rop);        

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        static extern IntPtr CreateDIBSection(
            IntPtr hdc, 
            ref Bitmapinfo bmi, 
            uint usage, 
            out IntPtr bits, 
            IntPtr hSection, 
            uint dwOffset);

        private static uint MAKERGB(int r, int g, int b)
        {
            return ((uint)(b & 255)) | ((uint)((r & 255) << 8)) | ((uint)((g & 255) << 16));
        }

        #endregion private methods
    }
}
