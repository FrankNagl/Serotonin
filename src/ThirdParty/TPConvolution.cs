// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2010
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
	/// Convolution filter from AForge.NET Framwork.
	/// </summary>
	/// 
    /// <remarks><para>The filter implements convolution operator, which calculates each pixel
    /// of the result image as weighted sum of the correspond pixel and its neighbors in the source
    /// image. The weights are set by <see cref="Kernel">convolution kernel</see>. The weighted
    /// sum is divided by <see cref="Divisor"/> before putting it into result image and also
    /// may be thresholded using <see cref="Threshold"/> value.</para>
    /// 
    /// <para>Convolution is a simple mathematical operation which is fundamental to many common
    /// image processing filters. Depending on the type of provided kernel, the filter may produce
    /// different results, like blur image, sharpen it, find edges, etc.</para>
    /// 
    /// <para>The filter accepts 8 and 16 bpp grayscale images and 24, 32, 48 and 64 bpp
    /// color images for processing. Note: for 32 bpp and 64 bpp images, the alpha channel is
    /// not processed anyhow with the specified kernel; its values are just copied as is. </para>
    /// 
    /// <para>Sample usage:</para>
    /// <code>
    /// // define emboss kernel
    /// int[,] kernel = {
    ///             { -2, -1,  0 },
    ///             { -1,  1,  1 },
    ///             {  0,  1,  2 } };
    /// // create filter
    /// Convolution filter = new Convolution( kernel );
    /// // apply the filter
    /// filter.ApplyInPlace( image );
    /// </code>
    /// </remarks>
	/// 
    internal class TPConvolution : BaseNonSBIPFilter
	{
        // convolution kernel
        private int[,] kernel;
        // division factor
        private int divisor = 1;
        // threshold to add to weighted sum
        private int threshold;
        // kernel size
        private int size;
        // use dynamic divisor for edges
        private bool dynamicDivisorForEdges = true;

        /// <summary>
        /// Convolution kernel.
        /// </summary>
        /// 
        /// <remarks>
        /// <para><note>Convolution kernel must be square and its width/height
        /// should be odd and should be in the [3, 99] range.</note></para>
        /// 
        /// <para><note>Setting convolution kernel through this property does not
        /// affect <see cref="Divisor"/> - it is not recalculated automatically.</note></para>
        /// </remarks>
        /// 
        /// <exception cref="ArgumentException">Invalid kernel size is specified.</exception>
        /// 
        public int[,] Kernel
        {
            get { return kernel; }
            set
            {
                int s = value.GetLength( 0 );

                // check kernel size
                if ( ( s != value.GetLength( 1 ) ) || ( s < 3 ) || ( s > 99 ) || ( s % 2 == 0 ) )
                    throw new ArgumentException( "Invalid kernel size." );

                kernel = value;
                size = s;
            }
        }

        /// <summary>
        /// Division factor.
        /// </summary>
        /// 
        /// <remarks><para>The value is used to divide convolution - weighted sum
        /// of pixels is divided by this value.</para>
        /// 
        /// <para><note>The value may be calculated automatically in the case if constructor
        /// with one parameter is used (<see cref="TPConvolution( int[,] )"/>).</note></para>
        /// </remarks>
        /// 
        /// <exception cref="ArgumentException">Divisor can not be equal to zero.</exception>
        /// 
        public int Divisor
        {
            get { return divisor; }
            set
            {
                if ( value == 0 )
                    throw new ArgumentException( "Divisor can not be equal to zero." );
                divisor = value;
            }
        }

        /// <summary>
        /// Threshold to add to weighted sum.
        /// </summary>
        /// 
        /// <remarks><para>The property specifies threshold value, which is added to each weighted
        /// sum of pixels. The value is added right after division was done by <see cref="Divisor"/>
        /// value.</para>
        /// 
        /// <para>Default value is set to <b>0</b>.</para>
        /// </remarks>
        /// 
        public int Threshold
        {
            get { return threshold; }
            set { threshold = value; }
        }

        /// <summary>
        /// Use dynamic divisor for edges or not.
        /// </summary>
        /// 
        /// <remarks><para>The property specifies how to handle edges. If it is set to
        /// <see langword="false"/>, then the same divisor (which is specified by <see cref="Divisor"/>
        /// property or calculated automatically) will be applied both for non-edge regions
        /// and for edge regions. If the value is set to <see langword="true"/>, then dynamically
        /// calculated divisor will be used for edge regions, which is sum of those kernel
        /// elements, which are taken into account for particular processed pixel
        /// (elements, which are not outside image).</para>
        /// 
        /// <para>Default value is set to <see langword="true"/>.</para>
        /// </remarks>
        /// 
        public bool DynamicDivisorForEdges
        {
            get { return dynamicDivisorForEdges; }
            set { dynamicDivisorForEdges = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TPConvolution"/> class.
        /// </summary>
        protected TPConvolution( )
        {
            SupportedSrcPixelFormats = PixelFormatFlags.All;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TPConvolution"/> class.
        /// </summary>
        /// 
        /// <param name="kernel">Convolution kernel.</param>
        /// 
        /// <remarks><para>Using this constructor (specifying only convolution kernel),
        /// <see cref="Divisor">division factor</see> will be calculated automatically
        /// summing all kernel values. In the case if kernel's sum equals to zero,
        /// division factor will be assigned to 1.</para></remarks>
        /// 
        /// <exception cref="ArgumentException">Invalid kernel size is specified. Kernel must be
        /// square, its width/height should be odd and should be in the [3, 25] range.</exception>
        /// 
        public TPConvolution( int[,] kernel ) : this( )
        {
            Kernel = kernel;

            divisor = 0;

            // calculate divisor
            int n = kernel.GetLength( 0 );
            for ( int i = 0; i < n; i++ )
            {
                int k = kernel.GetLength( 1 );
                for ( int j = 0; j < k; j++ )
                {
                    divisor += kernel[i, j];
                }
            }
            if ( divisor == 0 )
                divisor = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TPConvolution"/> class.
        /// </summary>
        /// 
        /// <param name="kernel">Convolution kernel.</param>
        /// <param name="divisor">Divisor, used used to divide weighted sum.</param>
        /// 
        /// <exception cref="ArgumentException">Invalid kernel size is specified. Kernel must be
        /// square, its width/height should be odd and should be in the [3, 25] range.</exception>
        /// <exception cref="ArgumentException">Divisor can not be equal to zero.</exception>
        /// 
        public TPConvolution( int[,] kernel, int divisor ) : this( )
        {
            Kernel = kernel;
            Divisor = divisor;
        }

        /// <summary>
        /// Processes the filter on the passed <paramref name="srcData"/> bitmap data.
        /// </summary>
        /// <param name="srcData">The source bitmap data.</param>
        /// <param name="dstData">The destination bitmap data.</param>
        protected override unsafe void Process(BitmapData srcData, BitmapData dstData)
        {
            Rectangle rect = new Rectangle(0, 0, srcData.Width, srcData.Height);
            int pixelSize = Image.GetPixelFormatSize( srcData.PixelFormat ) / 8;

            // processing start and stop X,Y positions
            int startX  = rect.Left;
            int startY  = rect.Top;
            int stopX   = startX + rect.Width;
            int stopY   = startY + rect.Height;

            // loop and array indexes
            int i, j, t, k, ir, jr;
            // kernel's radius
            int radius = size >> 1;
            // color sums
            long r, g, b, div;

            // kernel size
            int kernelSize = size * size;
            // number of kernel elements taken into account
            int processedKernelSize;

            // check pixel size to find if we deal with 8 or 16 bpp channels
            if ( pixelSize <= 4 )
            {
                int srcStride = srcData.Stride;
                int dstStride = dstData.Stride;

                int srcOffset = srcStride - rect.Width * pixelSize;
                int dstOffset = dstStride - rect.Width * pixelSize;

                byte* src = (byte*) srcData.Scan0 .ToPointer( );
                byte* dst = (byte*) dstData.Scan0.ToPointer( );

                // allign pointers to the first pixel to process
                src += ( startY * srcStride + startX * pixelSize );
                dst += ( startY * dstStride + startX * pixelSize );

                // do the processing job
                if ( dstData.PixelFormat == PixelFormat.Format8bppIndexed )
                {
                    // grayscale image

                    // for each line
                    for ( int y = startY; y < stopY; y++ )
                    {
                        // for each pixel
                        for ( int x = startX; x < stopX; x++, src++, dst++ )
                        {
                            g = div = processedKernelSize = 0;

                            // for each kernel row
                            for ( i = 0; i < size; i++ )
                            {
                                ir = i - radius;
                                t = y + ir;

                                // skip row
                                if ( t < startY )
                                    continue;
                                // break
                                if ( t >= stopY )
                                    break;

                                // for each kernel column
                                for ( j = 0; j < size; j++ )
                                {
                                    jr = j - radius;
                                    t = x + jr;

                                    // skip column
                                    if ( t < startX )
                                        continue;

                                    if ( t < stopX )
                                    {
                                        k = kernel[i, j];

                                        div += k;
                                        g += k * src[ir * srcStride + jr];
                                        processedKernelSize++;
                                    }
                                }
                            }

                            // check if all kernel elements were processed
                            if ( processedKernelSize == kernelSize )
                            {
                                // all kernel elements are processed - we are not on the edge
                                div = divisor;
                            }
                            else
                            {
                                // we are on edge. do we need to use dynamic divisor or not?
                                if ( !dynamicDivisorForEdges )
                                {
                                    // do
                                    div = divisor;
                                }
                            }

                            // check divider
                            if ( div != 0 )
                            {
                                g /= div;
                            }
                            g += threshold;
                            *dst = (byte) ( ( g > 255 ) ? 255 : ( ( g < 0 ) ? 0 : g ) );
                        }
                        src += srcOffset;
                        dst += dstOffset;
                    }
                }
                else
                {
                    // RGB image

                    // for each line
                    for ( int y = startY; y < stopY; y++ )
                    {
                        // for each pixel
                        for ( int x = startX; x < stopX; x++, src += pixelSize, dst += pixelSize )
                        {
                            r = g = b = div = processedKernelSize = 0;

                            // for each kernel row
                            for ( i = 0; i < size; i++ )
                            {
                                ir = i - radius;
                                t = y + ir;

                                // skip row
                                if ( t < startY )
                                    continue;
                                // break
                                if ( t >= stopY )
                                    break;

                                // for each kernel column
                                for ( j = 0; j < size; j++ )
                                {
                                    jr = j - radius;
                                    t = x + jr;

                                    // skip column
                                    if ( t < startX )
                                        continue;

                                    if ( t < stopX )
                                    {
                                        k = kernel[i, j];
                                        byte* p = &src[ir * srcStride + jr * pixelSize];

                                        div += k;

                                        r += k * p[RGBA.R];
                                        g += k * p[RGBA.G];
                                        b += k * p[RGBA.B];

                                        processedKernelSize++;
                                    }
                                }
                            }

                            // check if all kernel elements were processed
                            if ( processedKernelSize == kernelSize )
                            {
                                // all kernel elements are processed - we are not on the edge
                                div = divisor;
                            }
                            else
                            {
                                // we are on edge. do we need to use dynamic divisor or not?
                                if ( !dynamicDivisorForEdges )
                                {
                                    // do
                                    div = divisor;
                                }
                            }

                            // check divider
                            if ( div != 0 )
                            {
                                r /= div;
                                g /= div;
                                b /= div;
                            }
                            r += threshold;
                            g += threshold;
                            b += threshold;

                            dst[RGBA.R] = (byte) ( ( r > 255 ) ? 255 : ( ( r < 0 ) ? 0 : r ) );
                            dst[RGBA.G] = (byte) ( ( g > 255 ) ? 255 : ( ( g < 0 ) ? 0 : g ) );
                            dst[RGBA.B] = (byte) ( ( b > 255 ) ? 255 : ( ( b < 0 ) ? 0 : b ) );

                            // take care of alpha channel
                            if ( pixelSize == 4 )
                                dst[RGBA.A] = src[RGBA.A];
                        }
                        src += srcOffset;
                        dst += dstOffset;
                    }
                }
            }
            else
            {
                pixelSize /= 2;

                int dstStride = dstData.Stride / 2;
                int srcStride = srcData.Stride / 2;

                // base pointers
                ushort* baseSrc = (ushort*) srcData.Scan0.ToPointer( );
                ushort* baseDst = (ushort*) dstData.Scan0.ToPointer( );

                // allign pointers by X
                baseSrc += ( startX * pixelSize );
                baseDst += ( startX * pixelSize );

                if ( srcData.PixelFormat == PixelFormat.Format16bppGrayScale )
                {
                    // 16 bpp grayscale image

                    // for each line
                    for ( int y = startY; y < stopY; y++ )
                    {
                        ushort* src = baseSrc + y * srcStride;
                        ushort* dst = baseDst + y * dstStride;

                        // for each pixel
                        for ( int x = startX; x < stopX; x++, src++, dst++ )
                        {
                            g = div = processedKernelSize = 0;

                            // for each kernel row
                            for ( i = 0; i < size; i++ )
                            {
                                ir = i - radius;
                                t = y + ir;

                                // skip row
                                if ( t < startY )
                                    continue;
                                // break
                                if ( t >= stopY )
                                    break;

                                // for each kernel column
                                for ( j = 0; j < size; j++ )
                                {
                                    jr = j - radius;
                                    t = x + jr;

                                    // skip column
                                    if ( t < startX )
                                        continue;

                                    if ( t < stopX )
                                    {
                                        k = kernel[i, j];

                                        div += k;
                                        g += k * src[ir * srcStride + jr];
                                        processedKernelSize++;
                                    }
                                }
                            }

                            // check if all kernel elements were processed
                            if ( processedKernelSize == kernelSize )
                            {
                                // all kernel elements are processed - we are not on the edge
                                div = divisor;
                            }
                            else
                            {
                                // we are on edge. do we need to use dynamic divisor or not?
                                if ( !dynamicDivisorForEdges )
                                {
                                    // do
                                    div = divisor;
                                }
                            }

                            // check divider
                            if ( div != 0 )
                            {
                                g /= div;
                            }
                            g += threshold;
                            *dst = (ushort) ( ( g > 65535 ) ? 65535 : ( ( g < 0 ) ? 0 : g ) );
                        }
                    }
                }
                else
                {
                    // for each line
                    for ( int y = startY; y < stopY; y++ )
                    {
                        ushort* src = baseSrc + y * srcStride;
                        ushort* dst = baseDst + y * dstStride;

                        // for each pixel
                        for ( int x = startX; x < stopX; x++, src += pixelSize, dst += pixelSize )
                        {
                            r = g = b = div = processedKernelSize = 0;

                            // for each kernel row
                            for ( i = 0; i < size; i++ )
                            {
                                ir = i - radius;
                                t = y + ir;

                                // skip row
                                if ( t < startY )
                                    continue;
                                // break
                                if ( t >= stopY )
                                    break;

                                // for each kernel column
                                for ( j = 0; j < size; j++ )
                                {
                                    jr = j - radius;
                                    t = x + jr;

                                    // skip column
                                    if ( t < startX )
                                        continue;

                                    if ( t < stopX )
                                    {
                                        k = kernel[i, j];
                                        ushort* p = &src[ir * srcStride + jr * pixelSize];

                                        div += k;

                                        r += k * p[RGBA.R];
                                        g += k * p[RGBA.G];
                                        b += k * p[RGBA.B];

                                        processedKernelSize++;
                                    }
                                }
                            }

                            // check if all kernel elements were processed
                            if ( processedKernelSize == kernelSize )
                            {
                                // all kernel elements are processed - we are not on the edge
                                div = divisor;
                            }
                            else
                            {
                                // we are on edge. do we need to use dynamic divisor or not?
                                if ( !dynamicDivisorForEdges )
                                {
                                    // do
                                    div = divisor;
                                }
                            }

                            // check divider
                            if ( div != 0 )
                            {
                                r /= div;
                                g /= div;
                                b /= div;
                            }
                            r += threshold;
                            g += threshold;
                            b += threshold;

                            dst[RGBA.R] = (ushort) ( ( r > 65535 ) ? 65535 : ( ( r < 0 ) ? 0 : r ) );
                            dst[RGBA.G] = (ushort) ( ( g > 65535 ) ? 65535 : ( ( g < 0 ) ? 0 : g ) );
                            dst[RGBA.B] = (ushort) ( ( b > 65535 ) ? 65535 : ( ( b < 0 ) ? 0 : b ) );

                            // take care of alpha channel
                            if ( pixelSize == 4 )
                                dst[RGBA.A] = src[RGBA.A];
                        }
                    }
                }
            }
        }
	}
}
