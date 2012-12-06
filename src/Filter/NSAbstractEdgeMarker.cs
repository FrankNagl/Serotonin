// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
//Some source code fragments from AForge.NET framework
// http://code.google.com/p/aforge/
//
namespace SBIP.Filter.NonSBIP
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using Helper;

    /// <summary>
    /// Draws white edges of the resulting edge map into the original image. 
    /// </summary>
    /// 
    /// <remarks>
    /// <para>Note: Inherited filter have to implement <see cref="GetEdgeImage"/>. 
    /// This method applies the specified edge detector.</para>
    /// <para>The filter accepts 8 bpp grayscale, 24 bpp and 32 bpp
    /// color images for processing.</para>  
    /// 
    /// </remarks>
    /// 
    /// <seealso cref="NSCannyEdgeMarker"/>
    /// <seealso cref="NSSobelEdgeMarker"/>
    /// 
    public abstract class NSAbstractEdgeMarker : BaseNonSBIPFilter
    {
        /// <summary>The SBIP <see cref="NSSimpleDilatation"/> filter.</summary>
        public NSSimpleDilatation SimpleDilatation { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NSAbstractEdgeMarker"/> class.
        /// </summary>
        protected NSAbstractEdgeMarker()
        {
            SupportedSrcPixelFormats = PixelFormatFlags.All;
            SimpleDilatation = new NSSimpleDilatation();
        }

        /// <summary>
        /// Processes the image and return the edge map as 8 bpp bitmap.
        /// </summary>
        /// <param name="data">The bitmap data to use for edge detection.</param>
        /// <returns>The 8 bpp edge bitmap.</returns>
        protected abstract Bitmap GetEdgeImage(BitmapData data);

        /// <summary>
        /// Processes the filter on the passed <paramref name="srcData"/>
        /// resulting into <paramref name="dstData"/>.
        /// </summary>
        /// <param name="srcData">The source bitmap data.</param>
        /// <param name="dstData">The destination bitmap data.</param>
        protected override void Process(BitmapData srcData, BitmapData dstData)
        {
            Rectangle rect = new Rectangle(0, 0, srcData.Width, srcData.Height);
            //Bitmap destination = source.Clone(rect, source.PixelFormat);
            Bitmap edgeImage = GetEdgeImage(srcData);

            if (SimpleDilatation.Diameter > 1)
            {
                edgeImage = SimpleDilatation.Apply(edgeImage);
            }

            BitmapData edgeData = edgeImage.LockBits(
                rect, ImageLockMode.ReadOnly, edgeImage.PixelFormat);
            //BitmapData dstData =
            //    destination.LockBits(rect, ImageLockMode.ReadWrite, destination.PixelFormat);

            int w = edgeData.Width;
            int h = edgeData.Height;
            int pixelSize = Image.GetPixelFormatSize(dstData.PixelFormat) / 8;
            int offset = dstData.Stride - w * pixelSize;
            int edgeOffset = edgeData.Stride - w;

            // draw canny edges in dstData            
            unsafe
            {
                byte* src = (byte*)srcData.Scan0.ToPointer();
                byte* srcEdge = (byte*)edgeData.Scan0.ToPointer();
                byte* dst = (byte*)dstData.Scan0.ToPointer();

                // grayscale image
                if (pixelSize == 1)
                {
                    // for each line
                    for (int y = 0; y < h; y++)
                    {
                        // for each pixel in line
                        for (int x = 0; x < w; x++, src++, srcEdge++, dst++)
                        {
                            *dst = *srcEdge != 0 ? *srcEdge : *src;
                        }
                        dst += offset;
                        src += offset;
                        srcEdge += edgeOffset;
                    }
                }
                // colored image
                else
                {
                    // for each line
                    for (int y = 0; y < h; y++)
                    {
                        // for each pixel in line
                        for (int x = 0; x < w; x++, src += pixelSize, srcEdge++, dst += pixelSize)
                        {
                            if (*srcEdge != 0)
                            {
                                dst[RGBA.R] = *srcEdge;
                                dst[RGBA.G] = *srcEdge;
                                dst[RGBA.B] = *srcEdge;
                            }
                            else
                            {
                                dst[RGBA.R] = src[RGBA.R];
                                dst[RGBA.G] = src[RGBA.G];
                                dst[RGBA.B] = src[RGBA.B];                           
                            }

                            if (pixelSize == 4)
                            {
                                dst[RGBA.A] = src[RGBA.A];  
                            }
                        }
                        dst += offset;
                        src += offset;
                        srcEdge += edgeOffset;
                    }
                }
            }

            edgeImage.UnlockBits(edgeData);
            edgeImage.Dispose();
        }        
    }
}
