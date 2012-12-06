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
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;

    /// <summary>
    /// Detects the ranges of SBIP supported color spaces 
    /// (see <see cref="ColorSpaceEnum"/>) of a color (24 and 32 bpp) image.
    /// </summary>
    public class ColorSpaceRangeDetector : BaseNonSBIPFilter
    {
        /// <summary>All maximum values.</summary>
        public List<double[]> MaxValues { get; private set; }
        /// <summary>All minimum values.</summary>
        public List<double[]> MinValues { get; private set; }
            
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorSpaceRangeDetector"/> class.
        /// </summary>
        public ColorSpaceRangeDetector()
        {
            SupportedSrcPixelFormats = PixelFormatFlags.Color;
            const int number = 8;
            MaxValues = new List<double[]>(number);
            for (int i = 0; i < number; i++ )
            {
                MaxValues.Add(new double[] { 0, 0, 0 });
            }

            MinValues = new List<double[]>(8);
            for (int i = 0; i < number; i++)
            {
                MinValues.Add(new double[] { 0, 0, 0 });
            }
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
            int offset = srcData.Stride - w * pixelSize;

            // process image
            unsafe
            {
                byte* src = (byte*)srcData.Scan0.ToPointer();
                // for each line
                for (int y = 0; y < h; y++)
                {
                    // Console.WriteLine(@"Process line {0}", y);
                    // for each pixel in line
                    for (int x = 0; x < w; x++, src += pixelSize)
                    {
                        RGB rgb = new RGB(src[RGBA.R], src[RGBA.G], src[RGBA.B]);

                        // HSB
                        IColorSpace cs = rgb.To<HSB>();
                        // max
                        double[] d = MaxValues[(int)ColorSpaceEnum.HSB];
                        d[0] = Math.Max(d[0], cs.Color.A);
                        d[1] = Math.Max(d[1], cs.Color.B);
                        d[2] = Math.Max(d[2], cs.Color.C);
                        MaxValues[(int)ColorSpaceEnum.HSB] = d;
                        // min
                        d = MinValues[(int)ColorSpaceEnum.HSB];
                        d[0] = Math.Min(d[0], cs.Color.A);
                        d[1] = Math.Min(d[1], cs.Color.B);
                        d[2] = Math.Min(d[2], cs.Color.C);
                        MinValues[(int)ColorSpaceEnum.HSB] = d;

                        // HSL
                        cs = rgb.To<HSL>();
                        // max
                        d = MaxValues[(int)ColorSpaceEnum.HSL];
                        d[0] = Math.Max(d[0], cs.Color.A);
                        d[1] = Math.Max(d[1], cs.Color.B);
                        d[2] = Math.Max(d[2], cs.Color.C);
                        MaxValues[(int)ColorSpaceEnum.HSL] = d;
                        // min
                        d = MinValues[(int)ColorSpaceEnum.HSL];
                        d[0] = Math.Min(d[0], cs.Color.A);
                        d[1] = Math.Min(d[1], cs.Color.B);
                        d[2] = Math.Min(d[2], cs.Color.C);
                        MinValues[(int)ColorSpaceEnum.HSL] = d;

                        // LAB
                        cs = rgb.To<LAB>();
                        // max
                        d = MaxValues[(int)ColorSpaceEnum.LAB];
                        d[0] = Math.Max(d[0], cs.Color.A);
                        d[1] = Math.Max(d[1], cs.Color.B);
                        d[2] = Math.Max(d[2], cs.Color.C);
                        MaxValues[(int)ColorSpaceEnum.LAB] = d;
                        // min
                        d = MinValues[(int)ColorSpaceEnum.LAB];
                        d[0] = Math.Min(d[0], cs.Color.A);
                        d[1] = Math.Min(d[1], cs.Color.B);
                        d[2] = Math.Min(d[2], cs.Color.C);
                        MinValues[(int)ColorSpaceEnum.LAB] = d;

                        // LCH
                        cs = rgb.To<LCH>();
                        // max
                        d = MaxValues[(int)ColorSpaceEnum.LCH];
                        d[0] = Math.Max(d[0], cs.Color.A);
                        d[1] = Math.Max(d[1], cs.Color.B);
                        if (!double.IsNaN(cs.Color.C))
                        {
                            d[2] = Math.Max(d[2], cs.Color.C);
                        }
                        MaxValues[(int)ColorSpaceEnum.LCH] = d;
                        // min
                        d = MinValues[(int)ColorSpaceEnum.LCH];
                        d[0] = Math.Min(d[0], cs.Color.A);
                        d[1] = Math.Min(d[1], cs.Color.B);
                        if (!double.IsNaN(cs.Color.C))
                        {
                            d[2] = Math.Min(d[2], cs.Color.C);
                        }
                        MinValues[(int)ColorSpaceEnum.LCH] = d;

                        // LUV
                        cs = rgb.To<LUV>();
                        // max
                        d = MaxValues[(int)ColorSpaceEnum.LUV];
                        d[0] = Math.Max(d[0], cs.Color.A);
                        d[1] = Math.Max(d[1], cs.Color.B);
                        d[2] = Math.Max(d[2], cs.Color.C);
                        MaxValues[(int)ColorSpaceEnum.LUV] = d;
                        // min
                        d = MinValues[(int)ColorSpaceEnum.LUV];
                        d[0] = Math.Min(d[0], cs.Color.A);
                        d[1] = Math.Min(d[1], cs.Color.B);
                        d[2] = Math.Min(d[2], cs.Color.C);
                        MinValues[(int)ColorSpaceEnum.LUV] = d;

                        // RGB
                        cs = rgb.To<RGB>();
                        // max
                        d = MaxValues[(int)ColorSpaceEnum.RGB];
                        d[0] = Math.Max(d[0], cs.Color.A);
                        d[1] = Math.Max(d[1], cs.Color.B);
                        d[2] = Math.Max(d[2], cs.Color.C);
                        MaxValues[(int)ColorSpaceEnum.RGB] = d;
                        // min
                        d = MinValues[(int)ColorSpaceEnum.RGB];
                        d[0] = Math.Min(d[0], cs.Color.A);
                        d[1] = Math.Min(d[1], cs.Color.B);
                        d[2] = Math.Min(d[2], cs.Color.C);
                        MinValues[(int)ColorSpaceEnum.RGB] = d;

                        // SRGB
                        cs = rgb.To<SRGB>();
                        // max
                        d = MaxValues[(int)ColorSpaceEnum.SRGB];
                        d[0] = Math.Max(d[0], cs.Color.A);
                        d[1] = Math.Max(d[1], cs.Color.B);
                        d[2] = Math.Max(d[2], cs.Color.C);
                        MaxValues[(int)ColorSpaceEnum.SRGB] = d;
                        // min
                        d = MinValues[(int)ColorSpaceEnum.SRGB];
                        d[0] = Math.Min(d[0], cs.Color.A);
                        d[1] = Math.Min(d[1], cs.Color.B);
                        d[2] = Math.Min(d[2], cs.Color.C);
                        MinValues[(int)ColorSpaceEnum.SRGB] = d;

                        // XYZ
                        cs = rgb.To<XYZ>();
                        // max
                        d = MaxValues[(int)ColorSpaceEnum.XYZ];
                        d[0] = Math.Max(d[0], cs.Color.A);
                        d[1] = Math.Max(d[1], cs.Color.B);
                        d[2] = Math.Max(d[2], cs.Color.C);
                        MaxValues[(int)ColorSpaceEnum.XYZ] = d;
                        // min
                        d = MinValues[(int)ColorSpaceEnum.XYZ];
                        d[0] = Math.Min(d[0], cs.Color.A);
                        d[1] = Math.Min(d[1], cs.Color.B);
                        d[2] = Math.Min(d[2], cs.Color.C);
                        MinValues[(int)ColorSpaceEnum.XYZ] = d;
                    }
                    src += offset;                    
                }
            }
        }

        /// <summary>Saves the lists in a file in csv format.</summary>
        /// <param name="filename">The filename.</param>
        public void SaveListsInFile(string filename)
        {
            string s = "COLOR SPACE;A;B;C";
            IOFile.WriteFile(filename, s);
            
            s = "HSB max;";
            double[] d = MaxValues[(int)ColorSpaceEnum.HSB];
            IOFile.WriteLine(
                filename, 
                1, 
                s + d[0] + ";" + d[1] + ";" + d[2] + ";", 
                true);
            s = "HSB min;";
            d = MinValues[(int)ColorSpaceEnum.HSB];
            IOFile.WriteLine(
                filename,
                2, 
                s + d[0] + ";" + d[1] + ";" + d[2] + ";",
                true);

            s = "HSL max;";
            d = MaxValues[(int)ColorSpaceEnum.HSL];
            IOFile.WriteLine(
                filename,
                3, 
                s + d[0] + ";" + d[1] + ";" + d[2] + ";",
                true);
            s = "HSL min;";
            d = MinValues[(int)ColorSpaceEnum.HSL];
            IOFile.WriteLine(
                filename,
                4, 
                s + d[0] + ";" + d[1] + ";" + d[2] + ";",
                true);

            s = "LAB max;";
            d = MaxValues[(int)ColorSpaceEnum.LAB];
            IOFile.WriteLine(
                filename,
                5, 
                s + d[0] + ";" + d[1] + ";" + d[2] + ";",
                true);
            s = "LAB min;";
            d = MinValues[(int)ColorSpaceEnum.LAB];
            IOFile.WriteLine(
                filename,
                6, 
                s + d[0] + ";" + d[1] + ";" + d[2] + ";",
                true);

            s = "LCH max;";
            d = MaxValues[(int)ColorSpaceEnum.LCH];
            IOFile.WriteLine(
                filename,
                7, 
                s + d[0] + ";" + d[1] + ";" + d[2] + ";",
                true);
            s = "LCH min;";
            d = MinValues[(int)ColorSpaceEnum.LCH];
            IOFile.WriteLine(
                filename,
                8, 
                s + d[0] + ";" + d[1] + ";" + d[2] + ";",
                true);

            s = "LUV max;";
            d = MaxValues[(int)ColorSpaceEnum.LUV];
            IOFile.WriteLine(
                filename,
                9, 
                s + d[0] + ";" + d[1] + ";" + d[2] + ";",
                true);
            s = "LUV min;";
            d = MinValues[(int)ColorSpaceEnum.LUV];
            IOFile.WriteLine(
                filename,
                10, 
                s + d[0] + ";" + d[1] + ";" + d[2] + ";",
                true);

            s = "RGB max;";
            d = MaxValues[(int)ColorSpaceEnum.RGB];
            IOFile.WriteLine(
                filename,
                11, 
                s + d[0] + ";" + d[1] + ";" + d[2] + ";",
                true);
            s = "RGB min;";
            d = MinValues[(int)ColorSpaceEnum.RGB];
            IOFile.WriteLine(
                filename,
                12, 
                s + d[0] + ";" + d[1] + ";" + d[2] + ";",
                true);

            s = "SRGB max;";
            d = MaxValues[(int)ColorSpaceEnum.SRGB];
            IOFile.WriteLine(
                filename,
                13, 
                s + d[0] + ";" + d[1] + ";" + d[2] + ";",
                true);
            s = "SRGB min;";
            d = MinValues[(int)ColorSpaceEnum.SRGB];
            IOFile.WriteLine(
                filename,
                14, 
                s + d[0] + ";" + d[1] + ";" + d[2] + ";",
                true);

            s = "XYZ max;";
            d = MaxValues[(int)ColorSpaceEnum.XYZ];
            IOFile.WriteLine(
                filename,
                15, 
                s + d[0] + ";" + d[1] + ";" + d[2] + ";",
                true);
            s = "XYZ min;";
            d = MinValues[(int)ColorSpaceEnum.XYZ];
            IOFile.WriteLine(
                filename,
                16, 
                s + d[0] + ";" + d[1] + ";" + d[2] + ";",
                true);
        }

        #region Results of SBIP's NSColorSpaceRangeDetector
        // Results of SBIP's NSColorSpaceRangeDetector for all16777216rgb.png
        // http://davidnaylor.org/temp/all16777216rgb.png
        //
        //HSB max;359,764705882353;1;1;
        //HSB min;0;0;0;
        //HSL max;359,764705882353;1,00000000000001;1;
        //HSL min;0;0;0;
        //LAB max;100,000003866667;98,234311888004;94,4779750536703;
        //LAB min;0;-86,1827164205346;-107,860161754148;
        //LCH max;100,000003866667;133,807614853762;1,57079628503524;
        //LCH min;0;0;-1,57079630596368;
        //LUV max;100,000003866667;175,015029946927;107,398541240044;
        //LUV min;0;-83,0775622441578;-134,103004196986;
        //RGB max;255;255;255;
        //RGB min;0;0;0;
        //SRGB max;1;1;1;
        //SRGB min;0;0;0;
        //XYZ max;0,95047;1,0000001;1,08883;
        //XYZ min;0;0;0;
        //
        #endregion Results of SBIP's NSColorSpaceRangeDetector
    }
}
