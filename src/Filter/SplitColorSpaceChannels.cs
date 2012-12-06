// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
namespace Serotonin.Filter
{
    using ColorSpace;
    using SBIP.Filter.NonSBIP;
    using SBIP.Helper;
    using System.Drawing;
    using System.Drawing.Imaging;

    /// <summary>
    /// Splits the image into the three 
    /// <see cref="SplitColorSpaceChannels.ColorTriple">color channels</see> 
    /// of the specified 
    /// <see cref="SplitColorSpaceChannels.ColorSpace">color space</see>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This filter works with color (24 and 32 bpp) images.
    /// </para>
    /// <para>Sample usage:</para>
    /// <code>
    /// Bitmap image = new Bitmap("Cathedral.jpg");
    /// SplitColorSpaceChannels filter = new SplitColorSpaceChannels();
    /// optional: configure filter
    /// ...
    /// image = filter.Apply(image);
    /// </code>
    /// 
    /// </remarks>
    public class SplitColorSpaceChannels : BaseNonSBIPFilter
    {
        /// <summary>The specified color space to use.
        /// Default: <see cref="ColorSpaceEnum.HSL"/>.</summary>
        public ColorSpaceEnum ColorSpace { get; set; }

        /// <summary>The specified color channel to extract.
        /// Default: <see cref="ColorTripleEnum.A"/>.</summary>
        public ColorTripleEnum ColorTriple { get; set; }

        /// <summary>Initializes a new instance of the 
        /// <see cref="SplitColorSpaceChannels"/> class. </summary>
        public SplitColorSpaceChannels()
        {
            SupportedSrcPixelFormats = PixelFormatFlags.Color;
            SupportedDstPixelFormat = PixelFormatFlags.Format8BppIndexed;
            ColorSpace = ColorSpaceEnum.HSL;
        }

        /// <summary>
        /// Processes the filter on the passed <paramref name="srcData"/>
        /// resulting into <paramref name="dstData"/>.
        /// </summary>
        /// <param name="srcData">The source bitmap data.</param>
        /// <param name="dstData">The destination bitmap data.</param>
        protected override void Process(BitmapData srcData, BitmapData dstData)
        {
            int pixelSize = Image.GetPixelFormatSize(srcData.PixelFormat) / 8;
            int w = srcData.Width;
            int h = srcData.Height;
            int offsetSrc = srcData.Stride - w * pixelSize;
            int offsetDst = dstData.Stride - w;

            // process image
            unsafe
            {
                byte* src = (byte*)srcData.Scan0.ToPointer();
                byte* dst = (byte*)dstData.Scan0.ToPointer();
                // for each line
                for (int y = 0; y < h; y++)
                {
                    // for each pixel in line
                    for (int x = 0; x < w; x++, src += pixelSize, dst++)
                    {
                        RGB rgb = new RGB(src[RGBA.R], src[RGBA.G], src[RGBA.B]);

                        switch (ColorSpace)
                        {
                            case ColorSpaceEnum.HSB:
                                SetGrayscaleValue(dst, rgb.To<HSB>().Color, 359.764706, 1, 1);
                                break;
                            case ColorSpaceEnum.HSL:
                                SetGrayscaleValue(dst, rgb.To<HSL>().Color, 359.764706, 1, 1);
                                break;
                            case ColorSpaceEnum.LAB:
                                SetGrayscaleValue(dst, rgb.To<LAB>().Color, 100, 184.417028, 202.338137);
                                break;
                            case ColorSpaceEnum.LCH:
                                SetGrayscaleValue(dst, rgb.To<LCH>().Color, 100, 133.807615, 3.14159259);
                                break;
                            case ColorSpaceEnum.LUV:
                                SetGrayscaleValue(dst, rgb.To<LUV>().Color, 100, 258.092592, 241.501545);
                                break;
                            case ColorSpaceEnum.RGB:
                                SetGrayscaleValue(dst, rgb.Color, 255, 255, 255);
                                break;
                            case ColorSpaceEnum.SRGB:
                                SetGrayscaleValue(dst, rgb.To<SRGB>().Color, 1, 1, 1);
                                break;
                            case ColorSpaceEnum.XYZ:
                                SetGrayscaleValue(dst, rgb.To<XYZ>().Color, 0.95047, 1, 1.08883);
                                break;
                        }
                    }
                    src += offsetSrc;
                    dst += offsetDst;
                }
            }
        }

        private unsafe void SetGrayscaleValue(
            byte *dst, ColorTriple ct, double a, double b, double c)
        {
            switch (ColorTriple)
            {
                case ColorTripleEnum.A:
                    *dst = (byte)(ct.A * 255 / a + 0.5f);
                    break;
                case ColorTripleEnum.B:
                    *dst = (byte)(ct.B * 255 / b + 0.5f);
                    break;
                case ColorTripleEnum.C:
                    *dst = (byte)(ct.C * 255 / c + 0.5f);
                    break;
            }            
        }
    }
}
