// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright � Frank Nagl, 2009-2011
// admin@franknagl.de
//
// Article from canny edge detector by Bill Green was used as base for this filter
// http://www.pages.drexel.edu/~weg22/can_tut.html
//
//Some source code fragments from AForge.NET framework
// http://code.google.com/p/aforge/
//
namespace SBIP.Filter.NonSBIP
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using Helper;
    using ThirdParty;

    /// <summary>
    /// Modified version of AForge's Canny edge detector.
    /// </summary>
    /// 
    /// <remarks><para>The filter searches for objects' edges by 
    /// applying Canny edge detector.
    /// The implementation of Canny edge detector follows
    /// <a href="http://www.pages.drexel.edu/~weg22/can_tut.html">Bill Green's 
    /// Canny edge detection tutorial</a>.</para>
    /// <para>
    /// Some source code fragments from 
    /// <a href="http://code.google.com/p/aforge/">AForge.NET framework</a>.
    /// </para>
    /// <para>
    /// Note: If <c><see cref="AdditionalApply"/></c> is used,
    /// the edges in result image will be drawn in dependency of the edge's 
    /// orientation with colors of canny edge's chromatic circle (see image below).
    /// </para>
    /// 
    /// <para>Sample usage:</para>
    /// <code>
    /// Bitmap image = new Bitmap("Cathedral.jpg");
    /// // create filter
    /// NSCannyEdgeDetector canny = new NSCannyEdgeDetector();
    /// // optional: configure filter
    /// ...
    /// image = canny.Apply(image);
    /// </code>
    /// 
    /// <para><b>Initial image:</b></para>
    /// <img src="../../Cathedral.jpg" width="500" height="338" />
    /// <para><b>Result image:</b></para>
    /// <img src="../../NSCannyEdgeDetectorWhite.png" width="500" height="338" />
    /// <para><b>Result image (<c><see cref="AdditionalApply"/></c> was used):</b></para>
    /// <img src="../../NSCannyEdgeDetectorColored.png" width="500" height="338" />
    /// <para><b>Canny edge's chromatic circle:</b></para>
    /// <img src="../../Semicirc.jpg"/>
    /// </remarks>
    /// 
    /// <seealso cref="CannyEdgeDetector"/>
    /// 
    public class NSCannyEdgeDetector : BaseNonSBIPFilter, IAdditionalApply
    {
        private readonly TPGaussianBlur gaussianFilter = new TPGaussianBlur( );        
        private bool orientColored;

        #region public properties

        /// <summary>
        /// Low threshold.
        /// </summary>
        /// 
        /// <remarks><para>Low threshold value used for hysteresis
        /// (see  <a href="http://www.pages.drexel.edu/~weg22/can_tut.html">tutorial</a>
        /// for more information).</para>
        /// 
        /// <para>Default value is set to <b>20</b>.</para>
        /// </remarks>
        /// 
        public byte LowThreshold { get; set; }

        /// <summary>
        /// High threshold.
        /// </summary>
        /// 
        /// <remarks><para>High threshold value used for hysteresis
        /// (see  <a href="http://www.pages.drexel.edu/~weg22/can_tut.html">tutorial</a>
        /// for more information).</para>
        /// 
        /// <para>Default value is set to <b>40</b>.</para>
        /// </remarks>
        /// 
        public byte HighThreshold { get; set; }

        /// <summary>
        /// Gaussian sigma.
        /// </summary>
        /// 
        /// <remarks>Sigma value for <see cref="TPGaussianBlur.Sigma">
        /// Gaussian bluring</see>.</remarks>
        /// 
        public double GaussianSigma
        {
            get { return gaussianFilter.Sigma; }
            set { gaussianFilter.Sigma = value; }
        }

        /// <summary>
        /// Gaussian size.
        /// </summary>
        /// 
        /// <remarks>Size of <see cref="TPGaussianBlur.Size">
        /// Gaussian kernel</see>.</remarks>
        /// 
        public int GaussianSize
        {
            get { return gaussianFilter.Size; }
            set { gaussianFilter.Size = value; }
        }

        #endregion public properties

        /// <summary>
        /// Initializes a new instance of the <see cref="NSCannyEdgeDetector"/> class.
        /// </summary>
        public NSCannyEdgeDetector()
        {
            SupportedSrcPixelFormats = PixelFormatFlags.All;
            SupportedDstPixelFormat = PixelFormatFlags.Format8BppIndexed;            
            LowThreshold = 20;
            HighThreshold = 40;
        }

        #region public methods
        /// <summary>
        /// Applies the filter on the passed <paramref name="source"/> bitmap.
        /// In contrast to <see cref="BaseNonSBIPFilter.Apply(System.Drawing.Bitmap)"/>,
        /// the resulting edges are colored with their orientations color.
        /// <para><b>Canny edge's chromatic circle:</b></para>
        /// <img src="../../Semicirc.jpg"/>
        /// </summary>
        /// <param name="source">The source image to process.</param>
        /// <returns>
        /// The filter result as a new bitmap.
        /// </returns>
        public Bitmap AdditionalApply(Bitmap source)
        {
            // pre process
            orientColored = true;
            // process
            Bitmap result = Apply(source);
            // post process
            Serotonin.Helper.ColorPalette.SetCannyColorPalette(result);
            orientColored = false;
            return result;
        }
        #endregion public methods

        #region protected methods

        /// <summary>
        /// Processes the filter on the passed <paramref name="srcData"/>
        /// resulting into <paramref name="dstData"/>.
        /// </summary>
        /// <param name="srcData">The source bitmap data.</param>
        /// <param name="dstData">The destination bitmap data.</param>
        protected override unsafe void Process(BitmapData srcData, BitmapData dstData)
        {
            // processing start and stop X,Y positions
            const int startX = 1;
            const int startY = 1;
            int stopX = startX + srcData.Width - 2;
            int stopY = startY + srcData.Height - 2;

            const double toAngle = 180.0 / Math.PI;
            float leftPixel = 0, rightPixel = 0;

            #region canny
            Bitmap blur = BitmapConverter.BitmapDataToBitmap(srcData);
            // do grayscaling the image
            if (blur.PixelFormat != PixelFormat.Format8bppIndexed)
            {
                // STEP 0 - do grayscaling the image
                blur = TPGrayscale.CommonAlgorithms.BT709.Apply(blur);
            }

            // STEP 1 - blur image
            blur = gaussianFilter.Apply(blur);

            Rectangle rect = new Rectangle(0, 0, blur.Width, blur.Height);
            BitmapData blurData =
                blur.LockBits(rect, ImageLockMode.ReadWrite, blur.PixelFormat);

            int dstStride = dstData.Stride;
            int srcStride = blurData.Stride;

            int dstOffset = dstStride - rect.Width + 2;
            int srcOffset = srcStride - rect.Width + 2;

            // orientation array
            byte[,] orients = new byte[dstData.Width, dstData.Height];
            // gradients array
            //int[,] gxArray = new int[dstData.Width, dstData.Height];
            //int[,] gyArray = new int[dstData.Width, dstData.Height];
            float[,] gradients = new float[dstData.Width, dstData.Height];
            float maxGradient = float.NegativeInfinity;

            // do the job
            byte* src = (byte*)blurData.Scan0.ToPointer();
            // allign pointer
            src += srcStride * startY + startX;
            
            // STEP 2 - calculate magnitude and edge orientation
            // for each line
            for (int y = startY; y < stopY; y++)
            {
                // for each pixel
                for (int x = startX; x < stopX; x++, src++)
                {
                    // pixel's value and gradients
                    int gx = src[-srcStride + 1] + src[srcStride + 1]
                             - src[-srcStride - 1] - src[srcStride - 1]
                             + 2 * (src[1] - src[-1]);

                    int gy = src[-srcStride - 1] + src[-srcStride + 1]
                             - src[srcStride - 1] - src[srcStride + 1]
                             + 2 * (src[-srcStride] - src[srcStride]);

                    //gxArray[x, y] = Math.Abs(gx);
                    //gyArray[x, y] = Math.Abs(gy);

                    // get gradient value
                    gradients[x, y] = (float)Math.Sqrt(gx * gx + gy * gy);
                    if (gradients[x, y] > maxGradient)
                        maxGradient = gradients[x, y];

                    // --- get orientation
                    double orientation;
                    if (gx == 0)
                    {
                        // can not divide by zero
                        orientation = (gy == 0) ? 0 : 90;
                    }
                    else
                    {
                        double div = (double)gy / gx;

                        // handle angles of the 2nd and 4th quads
                        if (div < 0)
                        {
                            orientation = 180 - Math.Atan(-div) * toAngle;
                        }
                        // handle angles of the 1st and 3rd quads
                        else
                        {
                            orientation = Math.Atan(div) * toAngle;
                        }

                        // get closest angle from 0, 45, 90, 135 set
                        if (orientation < 22.5)
                            orientation = 0;
                        else if (orientation < 67.5)
                            orientation = 45;
                        else if (orientation < 112.5)
                            orientation = 90;
                        else if (orientation < 157.5)
                            orientation = 135;
                        else orientation = 0;
                    }

                    // save orientation
                    orients[x, y] = (byte)orientation;
                }
                src += srcOffset;
            }

            // STEP 3 - suppress non maximums
            byte* dst = (byte*)dstData.Scan0.ToPointer();
            // allign pointer
            dst += dstStride * startY + startX;

            // for each line
            for (int y = startY; y < stopY; y++)
            {
                // for each pixel
                for (int x = startX; x < stopX; x++, dst++)
                {
                    // get two adjacent pixels
                    switch (orients[x, y])
                    {
                        case 0:
                            leftPixel = gradients[x - 1, y];
                            rightPixel = gradients[x + 1, y];
                            break;
                        case 45:
                            leftPixel = gradients[x - 1, y + 1];
                            rightPixel = gradients[x + 1, y - 1];
                            break;
                        case 90:
                            leftPixel = gradients[x, y + 1];
                            rightPixel = gradients[x, y - 1];
                            break;
                        case 135:
                            leftPixel = gradients[x + 1, y + 1];
                            rightPixel = gradients[x - 1, y - 1];
                            break;
                    }
                    // compare current pixels value with adjacent pixels
                    if ((gradients[x, y] < leftPixel) || (gradients[x, y] < rightPixel))
                    {
                        *dst = 0;
                    }
                    else
                    {
                        *dst = (byte)(gradients[x, y] / maxGradient * 255);
                    }
                }
                dst += dstOffset;
            }

            // STEP 4 - hysteresis
            dst = (byte*)dstData.Scan0.ToPointer();
            // allign pointer
            dst += dstStride * startY + startX;

            // for each line
            for (int y = startY; y < stopY; y++)
            {
                // for each pixel
                for (int x = startX; x < stopX; x++, dst++)
                {
                    byte value = 255;
                    if (*dst < HighThreshold)
                    {
                        if (*dst < LowThreshold)
                        {
                            // non edge
                            value = 0;
                        }
                        else
                        {
                            // check 8 neighboring pixels
                            if ((dst[-1] < HighThreshold) &&
                                (dst[1] < HighThreshold) &&
                                (dst[-dstStride - 1] < HighThreshold) &&
                                (dst[-dstStride] < HighThreshold) &&
                                (dst[-dstStride + 1] < HighThreshold) &&
                                (dst[dstStride - 1] < HighThreshold) &&
                                (dst[dstStride] < HighThreshold) &&
                                (dst[dstStride + 1] < HighThreshold))
                            {
                                value = 0;
                            }
                        }
                    }

                    #region color orientations
                    if (value == 255 && orientColored)
                    {
                        byte tmp = orients[x, y];
                        switch (tmp)
                        {
                            case 0:
                                value = 255;
                                break;
                            case 45:
                                value = 45;
                                break;
                            case 90:
                                value = 90;
                                break;
                            case 135:
                                value = 135;
                                break;
                        }
                    }
                    #endregion color orientations
                    *dst = value;
                }
                dst += dstOffset;
            }
            #endregion canny
            
            //#region Adapt line thickness
            //if (Diameter > 1)
            //{
            //    dst = (byte*)dstData.Scan0.ToPointer();
            //    // allign pointer
            //    dst += dstStride * startY + startX;

            //    // for each line
            //    for (int y = startY; y < stopY; y++)
            //    {
            //        // for each pixel
            //        for (int x = startX; x < stopX; x++, dst++)
            //        {
            //            if (*dst != 0)
            //            {
            //                Drawing8Bpp.DrawThickPoint(
            //                    dstData, *dst, new Point( x, y), Diameter);
            //            }
            //        }
            //        dst += dstOffset;
            //    }
            //}
            //#endregion Adapt line thickness

            blur.UnlockBits(blurData);
            blur.Dispose();
        }

        #endregion protected methods             
    }
}
