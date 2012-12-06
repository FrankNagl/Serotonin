// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
// Article from canny edge detector by Bill Green was used as base for this filter
// http://www.pages.drexel.edu/~weg22/can_tut.html
//
//Some source code fragments from AForge.NET framework
// http://code.google.com/p/aforge/
//
namespace Serotonin.Filter
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using SBIP.Filter.NonSBIP;
    using SBIP.Filter.ThirdParty;
    using Serotonin.Helper;
    using SBIP.Helper;

    /// <summary>ConGrap - Contour Detection based on Gradient Map of Images.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>The filter searches for objects' contours by an algoritm 
    /// based on applying Canny edge detector.
    /// The implementation of Canny edge detector follows
    /// <a href="http://www.pages.drexel.edu/~weg22/can_tut.html">Bill Green's 
    /// Canny edge detection tutorial</a> respectively uses source code fragments 
    /// from <a href="http://code.google.com/p/aforge/">AForge.NET framework.</a>
    /// </para>
    /// <para>
    /// More information about ConGrap can be found in the paper 
    /// <a href="http://franknagl.de/?p=287">ConGrap@IPCV2011</a>.
    /// </para>
    /// 
    /// <para>Sample usage:</para>
    /// <code>
    /// Bitmap image = new Bitmap("Objects.png");
    /// // create filter
    /// ConGrap filter = new ConGrap();
    /// // optional: configure filter
    /// // ...
    /// image = filter.Apply(image);
    /// // access to the properties of each contour
    /// foreach (Contour c in filter.Contours)
    /// {
    ///     ...
    ///     // access to each pixel coordinate of the contour
    ///     foreach (Point pixel in c.BorderLine)
    ///     {
    ///         ...
    ///     }
    /// }
    /// </code>
    /// 
    /// <para><b>Initial image:</b></para>
    /// <img src="../../Objects.png"/>
    /// <para><b>Result image:</b></para>
    /// <img src="../../ConGrap.png"/>
    /// <para><b>Result image (contour's border rectangle):</b></para>
    /// <img src="../../ConGrap-with-borderrect.png"/>
    /// <para><b>Result image (only contour's border line):</b></para>
    /// <img src="../../ConGrap-only-border-lines.png"/>
    /// </remarks>
    /// 
    public class ConGrap : BaseNonSBIPFilter
    {
        private readonly TPGaussianBlur gaussianFilter = new TPGaussianBlur( );
        private int dstStride;
        private BitmapData data;
        private Rectangle rect;
        private byte[,] orients;     

        #region public properties

        /// <summary>All detected contours in the image.</summary>
        /// <seealso cref="Contour"/>
        public List<Contour> Contours { get; set; }

        /// <summary>Determines, if the border as rectangle will be drawn.
        /// </summary>
        public bool DrawBorderRectangle { get; set; }

        /// <summary>The pixel strength of the contour's rectangle border.
        ///  Default: 2.</summary>
        public int LineRectStrength { get; set; }

        /// <summary>
        /// Determines, if the border line will be drawn exclusively or all pixels 
        /// of all contours will be drawn.
        /// </summary>
        public bool OnlyBorderLine { get; set; }

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

        /// <summary>
        /// The radius of the trace contour filter (in pixels)..
        /// </summary>
        public byte TraceRadius { get; set; }

        /// <summary>
        /// Summand for non-matching orientations of neighbour pixels. 
        /// Default: 3.
        /// </summary>
        public byte Weight{ get; set; }
        #endregion public properties

        /// <summary>
        /// Initializes a new instance of the <see cref="ConGrap"/> class.
        /// </summary>
        public ConGrap()
        {
            SupportedSrcPixelFormats = PixelFormatFlags.All;           
            SupportedDstPixelFormat = PixelFormatFlags.Format8BppIndexed;
            LowThreshold = 20;
            HighThreshold = 40;            
            //GaussianSigma = 1.4; // default initializing not necessary here
            //GaussianSize = 5;

            LineRectStrength = 2;
            TraceRadius = 3;
            Weight = 3;
        }

        #region protected methods
        /// <summary>
        /// Processes the filter on the passed <paramref name="srcData"/>.
        /// </summary>
        /// <param name="srcData">The source bitmap data.</param>
        /// <param name="dstData">The destination bitmap data.</param>
        protected override unsafe void Process(BitmapData srcData, BitmapData dstData)
        {            
            Bitmap blur = BitmapConverter.BitmapDataToBitmap(srcData);
            // do grayscaling the image
            if (blur.PixelFormat != PixelFormat.Format8bppIndexed)
            {
                // STEP 0 - do grayscaling the image
                blur = TPGrayscale.CommonAlgorithms.BT709.Apply(blur);
            }

            // STEP 1 - blur image
            blur = gaussianFilter.Apply(blur);

            rect = new Rectangle(0, 0, blur.Width, blur.Height);
            BitmapData blurData =
                blur.LockBits(rect, ImageLockMode.ReadWrite, blur.PixelFormat);

            data = dstData;
            // processing start and stop X,Y positions
            int startX = rect.Left + 1;
            int startY = rect.Top + 1;
            int stopX = startX + rect.Width - 2;
            int stopY = startY + rect.Height - 2;

            const double toAngle = 180.0 / Math.PI;
            float leftPixel = 0, rightPixel = 0;

            dstStride = data.Stride;
            int srcStride = blurData.Stride;

            int dstOffset = dstStride - rect.Width + 2;
            int srcOffset = srcStride - rect.Width + 2;

            // orientation array
            orients = new byte[data.Width, data.Height];

            // gradients array
            //int[,] gxArray = new int[dstData.Width, dstData.Height];
            //int[,] gyArray = new int[dstData.Width, dstData.Height];
            float[,] gradients = new float[data.Width, data.Height];
            float maxGradient = float.NegativeInfinity;

            // do the job
            byte* src = (byte*)blurData.Scan0.ToPointer();
            // allign pointer
            src += srcStride * startY + startX;

            #region canny
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
            byte* dst = (byte*)data.Scan0.ToPointer();
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
                        byte b = (byte)(gradients[x, y] / maxGradient * 255);
                        *dst = b;
                    }
                }
                dst += dstOffset;
            }

            // STEP 4 - hysteresis
            dst = (byte*)data.Scan0.ToPointer();
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
                    *dst = value;
                }
                dst += dstOffset;
            }
            #endregion canny

            #region contour tracing
            // STEP 5 - contour tracing
            dstOffset = dstStride - rect.Width;
            dst = (byte*)data.Scan0.ToPointer();
            // allign pointer
            // dst += dstStride * (startY + 2) + startX + 2;

            byte index = 1, id = 1;
            Contours = new List<Contour>(rect.Width * rect.Height);

            Contour c;
            // for each line
            for (int y = 0; y < rect.Height; y++)
            {
                // for each pixel
                for (int x = 0; x < rect.Width; x++, dst++)
                {
                    // is an edge pixel?
                    if (*dst == 255) // && index != 255)
                    {
                        c = new Contour(id, index);
                        Point coord1 = new Point(x, y);
                        c.UpdateBorderRect(coord1);

                        while (GetAndDrawCountour(ref coord1, c)) { }
                        coord1 = new Point(x, y);
                        while (GetAndDrawCountour(ref coord1, c)) { }

                        GetBorderLine(c);
                        Contours.Add(c);
                        index++;
                        id++;
                    }

                    if (index == 255)
                    {
                        index = 1;
                    }
                }
                dst += dstOffset;
            }

            // sorts upwards by the contour's border rectangle size
            Contours.Sort();

            if (OnlyBorderLine)
            {
                Drawing8Bpp.ReplacePixels(data, 0, 0);
                foreach (Contour t in Contours)
                {
                    t.DrawBorderLine(data);
                }
            }

            if (DrawBorderRectangle)
            {
                foreach (var contour in Contours)
                {
                    contour.DrawBorderRect(data, LineRectStrength);
                }
            }
            #endregion contour tracing

            blur.UnlockBits(blurData);
            blur.Dispose();
        }
        #endregion protected methods

        #region private methods

        private unsafe void CheckNeighbours(
            byte* dst2, 
            byte index, 
            Point coord1, 
            out Point coord2)
        {
            coord2 = coord1;

            // kernel's diameter
            byte diameter = ((byte)(TraceRadius * 2 + 1));

            // most significant pixel's grade (1 = best grade, bigger 1 = worse)
            short spPriority = short.MaxValue;

            // for each kernel row
            for (int i = 0; i < diameter; i++)
            {
                int ir = i - TraceRadius;
                int ty = coord1.Y + ir;

                // skip row
                if (ty < 0)
                    continue;
                // break
                if (ty >= rect.Height)
                    break;

                // for each kernel column
                for (int j = 0; j < diameter; j++)
                {
                    int jr = j - TraceRadius;
                    int tx = coord1.X + jr;

                    // skip column
                    if (tx < 0)
                        continue;

                    if (tx >= rect.Width)
                    {
                        continue;
                    }

                    // do not process identical pixels
                    if (ir == 0 && jr == 0)
                    {
                        continue;
                    }

                    if (dst2[ir * dstStride + jr] != index)
                    {
                        continue;
                    }

                    short prio = (short)Math.Max(Math.Abs(ir), Math.Abs(jr));
                    if (orients[coord1.X, coord1.Y] != orients[tx, ty])
                    {
                        prio += Weight;
                    }

                    if (prio < spPriority)
                    {
                        spPriority = prio;
                        coord2.X = tx;
                        coord2.Y = ty;
                    }
                }
            }
        }

        // Draws the contour's line between two coordinates
        private static void DrawLine(
            BitmapData data,
            byte color,
            Point point1,
            Point point2)
        {
            // check if there is something to draw
            if (
                ((point1.X < 0) && (point2.X < 0)) ||
                ((point1.Y < 0) && (point2.Y < 0)) ||
                ((point1.X >= data.Width) && (point2.X >= data.Width)) ||
                ((point1.Y >= data.Height) && (point2.Y >= data.Height)))
            {
                // nothing to draw
                return;
            }

            BitmapHelper.CheckEndPoint(data.Width, data.Height, point1, ref point2);
            BitmapHelper.CheckEndPoint(data.Width, data.Height, point2, ref point1);

            // check again if there is something to draw
            if (
                ((point1.X < 0) && (point2.X < 0)) ||
                ((point1.Y < 0) && (point2.Y < 0)) ||
                ((point1.X >= data.Width) && (point2.X >= data.Width)) ||
                ((point1.Y >= data.Height) && (point2.Y >= data.Height)))
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

                // color image
                for (int x = 0; x != dx; x += step)
                {
                    int px = startX + x;
                    int py = (int)(startY + (slope * x));
                    DrawThickPoint(data, color, new Point(px, py));
                }
            }
            else
            {
                // the line is more vertical, we'll plot along the y axis.
                float slope = (float)dx / dy;
                int step = (dy > 0) ? 1 : -1;

                // correct dy so last point is included as well
                dy += step;

                // color image
                for (int y = 0; y != dy; y += step)
                {
                    int px = (int)(startX + (slope * y));
                    int py = startY + y;
                    DrawThickPoint(data, color, new Point(px, py));
                }
            }
        }

        // Draws the contour line's point and its 7x7 neigbours also, if they are white
        private static unsafe void DrawThickPoint(
            BitmapData data,
            byte color,
            Point coord)
        {
            const int diameter = 3;
            const int radius = diameter / 2;
            // image dimension
            int ps = Image.GetPixelFormatSize(data.PixelFormat) / 8;
            byte* ptr = (byte*)data.Scan0.ToPointer() + coord.Y * data.Stride + coord.X * ps;

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

                    if (tx >= data.Width)
                    {
                        continue;
                    }

                    // draw identical pixel
                    if (ir == 0 && jr == 0)
                    {
                        ptr[ir * data.Stride + jr * ps] = color;
                    }
                    // check, if direct neighbour pixel is white, if yes, draw it
                    else
                    {
                        if (ptr[ir * data.Stride + jr * ps] == 255)
                        {
                            ptr[ir * data.Stride + jr * ps] = color;
                        }
                    }
                }
            }
        }

        private unsafe bool GetAndDrawCountour(ref Point coord1, Contour c)
        {
            byte* dst2 = (byte*)data.Scan0.ToPointer();
            // allign pointer
            dst2 += dstStride * coord1.Y + coord1.X;

            Point coord2;
            // 1.) look for white (edge px) neighbours
            CheckNeighbours(dst2, 255, coord1, out coord2);
            if (coord1 == coord2)
            {
                // try to close the contour with step 2
                // 2.) look for neighbours of same contour and stop tracing
                CheckNeighbours(dst2, c.Index, coord1, out coord2);
                DrawLine(data, c.Index, coord1, coord2);
                if (coord1 != coord2)
                {
                    c.UpdateBorderRect(coord2);
                }
                return false;
            }

            DrawLine(data, c.Index, coord1, coord2);
            c.UpdateBorderRect(coord2);
            coord1 = coord2;
            return true;
        }

        private unsafe void GetBorderLine(Contour c)
        {
            int offset = dstStride + c.LeftX - c.RightX - 1;
            // dstOffset = dstStride - rect.Width + 6;
            Byte* dst = (byte*)data.Scan0.ToPointer();
            // allign pointer
            dst += dstStride * c.UpY + c.LeftX;

            // FIND BORDER LINE PIXEL IN X- AND Y-DIRECTION
            // for each row
            bool isFirst;
            Point p;
            for (int y = c.UpY; y <= c.BottomY; y++)
            {
                isFirst = true;
                p = Point.Empty;
                // for each pixel
                for (int x = c.LeftX; x <= c.RightX; x++, dst++)
                {
                    if (*dst != c.Index)
                    {
                        continue;
                    }
                    if (p.X < x)
                    {
                        p.X = x;
                        p.Y = y;
                    }

                    if (!isFirst)
                    {
                        continue;
                    }
                    c.BorderLine.Add(p); // old version
                    isFirst = false;
                }

                //check if no start point is detected
                if (p == Point.Empty)
                {
                    dst += offset;
                    continue;
                }

                c.BorderLine.Add(p); // old version
                dst += offset;
            }

            // for each column
            for (int x = c.LeftX; x <= c.RightX; x++)
            {
                isFirst = true;
                p = Point.Empty;
                dst = (byte*)data.Scan0.ToPointer();
                // allign pointer  
                dst += dstStride * c.UpY + x;
                for (int y = c.UpY; y <= c.BottomY; y++, dst += dstStride)
                {
                    if (*dst != c.Index)
                    {
                        continue;
                    }
                    if (p.Y < y)
                    {
                        p.X = x;
                        p.Y = y;
                    }

                    if (!isFirst)
                    {
                        continue;
                    }
                    c.BorderLine.Add(p); // old version
                    isFirst = false;
                }

                //check if no start point is detected
                if (p == Point.Empty)
                {
                    continue;
                }
                c.BorderLine.Add(p); // old version
            }
        }

        #endregion private methods      
    }
}
