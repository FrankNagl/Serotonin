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

    /// <summary>
    /// Draws the white edges of the resulting edge map of 
    /// <see cref="NSCannyEdgeDetector"/> into the original image.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>The filter accepts 8 bpp grayscale, 24 bpp and 32 bpp
    /// color images for processing.</para>  
    /// 
    /// <para>Sample usage:</para>
    /// <code>
    /// Bitmap bitmap = new Bitmap("Cathedral.jpg");
    ///
    /// NSCannyEdgeMarker filter = new NSCannyEdgeMarker();
    /// // optional: configure filter
    /// // ...
    /// // draw canny edge map into image
    /// bitmap = filter.Apply(bitmap);
    /// </code>
    /// 
    /// <para><b>Initial image:</b></para>
    /// <img src="../../Cathedral.jpg" width="500" height="338" />
    /// <para><b>Result image:</b></para>
    /// <img src="../../NSCannyEdgeMarker.png" width="500" height="338" />
    /// </remarks>
    /// 
    /// <seealso cref="NSCannyEdgeDetector"/>
    /// <seealso cref="NSSobelEdgeMarker"/>
    /// 
    public class NSCannyEdgeMarker : NSAbstractEdgeMarker
    {
        #region properties

        /// <summary>The SBIP canny edge detector.</summary>
        public NSCannyEdgeDetector CannyEdgeDetector { get; set; }

        #endregion properties

        /// <summary>
        /// Initializes a new instance of the <see cref="NSCannyEdgeMarker"/> class.
        /// </summary>
        /// 
        public NSCannyEdgeMarker()
        {
            CannyEdgeDetector = new NSCannyEdgeDetector();
        }

        /// <summary>
        /// Processes the image and return the canny edge map as 8 bpp bitmap.
        /// </summary>
        /// <param name="data">The bitmap data to use for edge detection.</param>
        /// <returns>
        /// The 8 bpp edge bitmap.
        /// </returns>
        protected override Bitmap GetEdgeImage(BitmapData data)
        {
            Bitmap canny = Helper.BitmapConverter.BitmapDataToBitmap(data);
            canny = CannyEdgeDetector.Apply(canny);            
            return canny;
        }
    }
}
