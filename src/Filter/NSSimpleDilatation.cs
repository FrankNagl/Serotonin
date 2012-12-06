// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
namespace SBIP.Filter.NonSBIP
{    
    using System.Drawing;
    using System.Drawing.Imaging;
    using Helper;

    /// <summary>
    /// Simple dilatation of all non-black pixels by using 3x3 filter 
    /// as a structuring element.
    /// </summary>
    /// 
    /// <remarks><para> The dilatation operation usually uses a structuring 
    /// element for probing and expanding the shapes contained in the input 
    /// image.</para>
    /// <para>Sample usage:</para>
    /// <code>
    /// Bitmap image = new Bitmap("Objects.png");
    /// ConGrap congrap = new ConGrap();
    /// congrap.OnlyBorderLine = true;
    /// // create contour image by using ConGrap
    /// image = congrap.Apply(image);
    /// // create simple dilatation filter
    /// NSSimpleDilatation filter = new NSSimpleDilatation();
    /// image = filter.Apply(image);
    /// </code>
    /// 
    /// <para><b>Initial image:</b></para>
    /// <img src="../../ConGrap-only-border-lines.png" width="500" height="338" />
    /// <para><b>Result image:</b></para>
    /// <img src="../../NSDilatation.png" width="500" height="338" />
    /// </remarks>
    public class NSSimpleDilatation : BaseNonSBIPFilter
    {
        /// <summary>
        /// The diameter of thickness for drawing edge pixels. 
        /// (Does not belong to canny algorithm.)
        /// </summary>
        /// <remarks>
        /// /// <para>Default value is set to <b>1</b>.</para>
        /// </remarks>
        public byte Diameter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NSSimpleDilatation"/> class.
        /// </summary>
        public NSSimpleDilatation()
        {
            SupportedSrcPixelFormats = PixelFormatFlags.All;
            Diameter = 2;
        }

        /// <summary>
        /// Extracts the color segments of the original bitmap as pixel-connected regions.
        /// </summary>
        protected override unsafe void Process(BitmapData srcData, BitmapData dstData)
        {
            int pixelSize = Image.GetPixelFormatSize(srcData.PixelFormat) / 8;
            int w = srcData.Width;
            int h = srcData.Height;
            int s = srcData.Stride;
            int offset = s - w * pixelSize;

            //if (Diameter != 0)
            {
                byte* src = (byte*)srcData.Scan0.ToPointer();
                // for each line
                for (int y = 0; y < h; y++)
                {
                    // for each pixel
                    for (int x = 0; x < w; x++, src += pixelSize)
                    {
                        if (pixelSize == 1  && (*src != 0))  
                        {
                            Drawing8Bpp.DrawThickPoint(
                                dstData, *src, new Point(x, y), Diameter);
                        }
                        else if (src[RGBA.R] != 0 || src[RGBA.G] != 0 || src[RGBA.B] != 0) // color
                        {
                            Color col = Color.FromArgb(
                                src[RGBA.R], src[RGBA.G], src[RGBA.B]);
                            Drawing24Or32Bpp.DrawThickPoint(
                                dstData, col, new Point(x, y), Diameter);                                
                        }
                    }
                    src += offset;
                }
            }
        }
    }
}
