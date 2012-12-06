// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
//Some source code fragments from AForge.NET framework
// http://code.google.com/p/aforge/
//
namespace SBIP.Filter.ThirdParty
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using Helper;
    using NonSBIP;

    /// <summary>
    /// Base class for image grayscaling from AForge.NET Framwork.
    /// </summary>
    /// 
    /// <remarks><para>This class is the base class for image grayscaling. Other
    /// classes should inherit from this class and specify <b>RGB</b>
    /// coefficients used for color image conversion to grayscale.</para>
    /// 
    /// <para>The filter accepts 24, 32, 48 and 64 bpp color images and produces
    /// 8 (if source is 24 or 32 bpp image) or 16 (if source is 48 or 64 bpp image)
    /// bpp grayscale image.</para>
    /// 
    /// <para>Sample usage:</para>
    /// <code>
    /// // create grayscale filter (BT709)
    /// Grayscale filter = new Grayscale( 0.2125, 0.7154, 0.0721 );
    /// // apply the filter
    /// Bitmap grayImage = filter.Apply( image );
    /// </code>
    /// </remarks>
    ///
    internal class TPGrayscale : BaseNonSBIPFilter
    {
        /// <summary>
        /// Set of predefined common grayscaling algorithms, which have aldready initialized
        /// grayscaling coefficients.
        /// </summary>
        public static class CommonAlgorithms
        {
            /// <summary>
            /// Grayscale image using BT709 algorithm.
            /// </summary>
            /// 
            /// <remarks><para>The instance uses <b>BT709</b> algorithm to convert color image
            /// to grayscale. The conversion coefficients are:
            /// <list type="bullet">
            /// <item>Red: 0.2125;</item>
            /// <item>Green: 0.7154;</item>
            /// <item>Blue: 0.0721.</item>
            /// </list></para>
            /// 
            /// <para>Sample usage:</para>
            /// <code>
            /// // apply the filter
            /// Bitmap grayImage = Grayscale.CommonAlgorithms.BT709.Apply( image );
            /// </code>
            /// </remarks>
            /// 
            public static readonly TPGrayscale BT709 = new TPGrayscale(0.2125, 0.7154, 0.0721);

            /// <summary>
            /// Grayscale image using R-Y algorithm.
            /// </summary>
            /// 
            /// <remarks><para>The instance uses <b>R-Y</b> algorithm to convert color image
            /// to grayscale. The conversion coefficients are:
            /// <list type="bullet">
            /// <item>Red: 0.5;</item>
            /// <item>Green: 0.419;</item>
            /// <item>Blue: 0.081.</item>
            /// </list></para>
            /// 
            /// <para>Sample usage:</para>
            /// <code>
            /// // apply the filter
            /// Bitmap grayImage = Grayscale.CommonAlgorithms.RMY.Apply( image );
            /// </code>
            /// </remarks>
            /// 
            public static readonly TPGrayscale RMY = new TPGrayscale(0.5000, 0.4190, 0.0810);

            /// <summary>
            /// Grayscale image using Y algorithm.
            /// </summary>
            /// 
            /// <remarks><para>The instance uses <b>Y</b> algorithm to convert color image
            /// to grayscale. The conversion coefficients are:
            /// <list type="bullet">
            /// <item>Red: 0.299;</item>
            /// <item>Green: 0.587;</item>
            /// <item>Blue: 0.114.</item>
            /// </list></para>
            /// 
            /// <para>Sample usage:</para>
            /// <code>
            /// // apply the filter
            /// Bitmap grayImage = Grayscale.CommonAlgorithms.Y.Apply( image );
            /// </code>
            /// </remarks>
            /// 
            public static readonly TPGrayscale Y = new TPGrayscale(0.2990, 0.5870, 0.1140);
        }

        // RGB coefficients for grayscale transformation

        /// <summary>
        /// Portion of red channel's value to use during conversion from RGB to grayscale.
        /// </summary>
        public readonly double RedCoefficient;
        /// <summary>
        /// Portion of green channel's value to use during conversion from RGB to grayscale.
        /// </summary>
        public readonly double GreenCoefficient;
        /// <summary>
        /// Portion of blue channel's value to use during conversion from RGB to grayscale.
        /// </summary>
        public readonly double BlueCoefficient;

        /// <summary>
        /// Initializes a new instance of the <see cref="TPGrayscale"/> class.
        /// </summary>
        /// 
        /// <param name="cr">Red coefficient.</param>
        /// <param name="cg">Green coefficient.</param>
        /// <param name="cb">Blue coefficient.</param>
        /// 
        public TPGrayscale(double cr, double cg, double cb)
        {
            SupportedSrcPixelFormats = PixelFormatFlags.Color;
            SupportedDstPixelFormat = PixelFormatFlags.Format8BppIndexed;
            RedCoefficient = cr;
            GreenCoefficient = cg;
            BlueCoefficient = cb;
        }

        ///// <summary>
        ///// Applies the filter on the passed <paramref name="source"/> bitmap.
        ///// </summary>
        ///// <param name="source">The source image to process.</param>
        ///// <returns>The filter result as a new bitmap.</returns>
        //protected override Bitmap Process(Bitmap source)
        //{
        //    Bitmap destination = new Bitmap(source.Width, source.Height, PixelFormat.Format8bppIndexed);
        //    Rectangle rect = new Rectangle(0, 0, source.Width, source.Height);
        //    BitmapData srcData = source.LockBits(rect, ImageLockMode.ReadWrite, source.PixelFormat);
        //    BitmapData dstData = destination.LockBits(rect, ImageLockMode.ReadWrite, destination.PixelFormat);
        //    Filter(srcData, dstData);
        //    destination.UnlockBits(dstData);
        //    source.UnlockBits(srcData);

        //    return destination;
        //}

        /// <summary>
        /// Processes the filter on the passed <paramref name="srcData"/> bitmap data.
        /// </summary>
        /// <param name="srcData">The source bitmap data.</param>
        /// <param name="dstData">The destination bitmap data.</param>
        protected override unsafe void Process(BitmapData srcData, BitmapData dstData)
        {
            // get width and height
            int width = srcData.Width;
            int height = srcData.Height;
            PixelFormat srcPixelFormat = srcData.PixelFormat;

            if (srcPixelFormat == PixelFormat.Format8bppIndexed)
            {
                throw new ArgumentException
                    ("Source image can not be grayscale (8 bpp) image.");
            }

            if (
                (srcPixelFormat == PixelFormat.Format24bppRgb) ||
                (srcPixelFormat == PixelFormat.Format32bppRgb) ||
                (srcPixelFormat == PixelFormat.Format32bppArgb))
            {
                int pixelSize = (srcPixelFormat == PixelFormat.Format24bppRgb) ? 3 : 4;
                int srcOffset = srcData.Stride - width * pixelSize;
                int dstOffset = dstData.Stride - width;

                // do the job
                byte* src = (byte*)srcData.Scan0.ToPointer();
                byte* dst = (byte*)dstData.Scan0.ToPointer();

                // for each line
                for (int y = 0; y < height; y++)
                {
                    // for each pixel
                    for (int x = 0; x < width; x++, src += pixelSize, dst++)
                    {
                        *dst = (byte)(RedCoefficient * src[RGBA.R] + GreenCoefficient * src[RGBA.G] + BlueCoefficient * src[RGBA.B]);
                    }
                    src += srcOffset;
                    dst += dstOffset;
                }
            }
            else
            {
                int pixelSize = (srcPixelFormat == PixelFormat.Format48bppRgb) ? 3 : 4;
                int srcBase = (int)srcData.Scan0.ToPointer();
                int dstBase = (int)dstData.Scan0.ToPointer();
                int srcStride = srcData.Stride;
                int dstStride = dstData.Stride;

                // for each line
                for (int y = 0; y < height; y++)
                {
                    ushort* src = (ushort*)(srcBase + y * srcStride);
                    ushort* dst = (ushort*)(dstBase + y * dstStride);

                    // for each pixel
                    for (int x = 0; x < width; x++, src += pixelSize, dst++)
                    {
                        *dst = (ushort)(RedCoefficient * src[RGBA.R] + GreenCoefficient * src[RGBA.G] + BlueCoefficient * src[RGBA.B]);
                    }
                }
            }
        }
    }
}
