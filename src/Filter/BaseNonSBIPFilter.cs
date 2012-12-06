// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
namespace SBIP.Filter.NonSBIP
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.Serialization;
    using Helper;

    /// <summary>
    /// Base class for non-shader-based image processing filter.
    /// </summary>
    /// <remarks>
    /// Provides methods to process an image by a specified
    /// non-shader-based image processing filter.
    /// </remarks>
    [DataContract]
    public abstract class BaseNonSBIPFilter
    {
        [DataMember] private string errorMsg = "No supported source pixel format.";
        [DataMember] private PixelFormatFlags supportedSrcPixelFormats;
        [DataMember] private PixelFormatFlags supportedDstPixelFormat;
        [DataMember] private PixelFormat dstPixelFormat;
        
        /// <summary>Defines supported pixel format for result image of the 
        /// NonSBIP filter.</summary>
        public PixelFormatFlags SupportedDstPixelFormat
        {
            get { return supportedDstPixelFormat; }
            protected set
            {
                supportedDstPixelFormat = value;
                switch (value)
                {
                    case PixelFormatFlags.Format8BppIndexed:
                        dstPixelFormat = PixelFormat.Format8bppIndexed;
                        break;
                    case PixelFormatFlags.Format24BppRgb:
                        dstPixelFormat = PixelFormat.Format24bppRgb;
                        break;
                    case (PixelFormatFlags.Format32BppArgb):
                        dstPixelFormat = PixelFormat.Format32bppArgb;
                        break;
                    case PixelFormatFlags.Format32BppRgb:
                        dstPixelFormat = PixelFormat.Format32bppRgb;
                        break;
                }
            }
        }

        /// <summary>Defines supported pixel formats of the source image for the 
        /// NonSBIP filter.</summary>
        public PixelFormatFlags SupportedSrcPixelFormats
        {
            get { return supportedSrcPixelFormats; }
            protected set
            {
                supportedSrcPixelFormats = value;
                switch (value)
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
            } 
        }

        /// <summary>
        /// Applies the filter on the passed <paramref name="source"/> bitmap.
        /// </summary>
        /// <param name="source">The source image to process.</param>
        /// <returns>The filter result as a new bitmap.</returns>
        public Bitmap Apply(Bitmap source)
        {
            CheckPixelFormat(source.PixelFormat);
            Bitmap destination = new Bitmap(source.Width, source.Height, dstPixelFormat);
            Rectangle rect = new Rectangle(0, 0, source.Width, source.Height);
            BitmapData srcData = source.LockBits(rect, ImageLockMode.ReadWrite, source.PixelFormat);
            BitmapData dstData = destination.LockBits(rect, ImageLockMode.ReadWrite, destination.PixelFormat);
            Process(srcData, dstData);
            destination.UnlockBits(dstData);
            source.UnlockBits(srcData);

            return destination;
        }

        /// <summary>
        /// Applies the filter on the passed <paramref name="srcData"/> bitmap
        /// resulting into <paramref name="dstData"/>.
        /// </summary>
        /// <param name="srcData">The source bitmap data.</param>
        /// <param name="dstData">The destination bitmap data.</param>
        public void Apply(BitmapData srcData, BitmapData dstData)
        {
            CheckPixelFormat(srcData.PixelFormat);
            if (dstPixelFormat != dstData.PixelFormat)
            {
                throw new ArgumentException(@"Incorrect pixel format.", "dstData");
            }            
            Process(srcData, dstData);            
        }

        /// <summary>
        /// Processes the filter on the passed <paramref name="srcData"/> 
        /// resulting into <paramref name="dstData"/>.
        /// </summary>
        /// <param name="srcData">The source bitmap data.</param>
        /// <param name="dstData">The destination bitmap data.</param>
        protected abstract void Process(BitmapData srcData, BitmapData dstData);

        /// <summary>
        /// Checks, if the <paramref name="format"/> is supported by the filter. 
        /// <seealso cref="SupportedSrcPixelFormats"/>
        /// </summary>
        /// <param name="format">The format.</param>
        /// <exception cref="ArgumentException">Pixelformat of source image 
        /// cannot be processed.</exception>
        protected void CheckPixelFormat(PixelFormat format)
        {
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

            if ((flags & supportedSrcPixelFormats) == 0)
            {
                throw new ArgumentException(errorMsg);
            }          

            // if no dst format is defined, set it to source format
            if (dstPixelFormat == 0)
            {
                dstPixelFormat = format;
            }
        }
    }
}
