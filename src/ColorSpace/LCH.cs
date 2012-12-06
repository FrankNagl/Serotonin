// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
// ColorSpace namespace copyright © Tobias Bindel, 2011
//

using System;

namespace Serotonin.ColorSpace
{
    /// <summary>
    /// A struct which defines the CIE LCH (ab) and CIE LCH (uv) color space.
    /// </summary>
    public struct LCH : IColorSpace
    {
        /// <summary>
        /// Gets an empty <see cref="LCH"/> instance.
        /// </summary>
        public static readonly LCH Empty;

        /// <summary>
        /// Defines the possible base color spaces
        /// </summary>
        public enum BaseColorSpace
        {
            /// <summary>
            /// The LAB color space.
            /// </summary>
            LAB = 0,

            /// <summary>
            /// The LUV color space.
            /// </summary>
            LUV = 1
        }

        /// <summary>
        /// Gets or sets the based color space.
        /// </summary>
        /// <value>The based color space.</value>
        public BaseColorSpace ColorSpace { get; set; }

        #region Accessors

        /// <summary>
        /// Gets or sets triple of color channels.
        /// </summary>
        /// <value>The color triple.</value>
        public ColorTriple Color
        {
            get
            {
                return new ColorTriple(L, C, H);
            }
            set
            {
                L = value.A;
                C = value.B;
                H = value.C;
            }
        }

        /// <summary>
        /// Gets or sets the lightness channel.
        /// </summary>
        /// <value>The lightness channel.</value>
        public float Lightness
        {
            get { return L; } set { L = value; }
        }

        /// <summary>
        /// Gets or sets the chroma channel.
        /// </summary>
        /// <value>The chroma channel.</value>
        public float Chroma
        {
            get { return C; } set { C = value; }
        }

        /// <summary>
        /// Gets or sets the hue channel.
        /// </summary>
        /// <value>The hue channel.</value>
        public float Hue
        {
            get { return H; } set { H = value; }
        }

        /// <summary>
        /// Short accessor for the lightness channel.
        /// </summary>
        /// <value>The Lightness channel.</value>
        public float L { get; set; }

        /// <summary>
        /// Short accessor for the chroma channel.
        /// </summary>
        /// <value>The Chroma channel.</value>
        public float C { get; set; }

        /// <summary>
        /// Short accessor for the hue channel.
        /// </summary>
        /// <value>The Hue channel.</value>
        public float H { get; set; }

        #endregion

        ///// <summary>
        ///// Initializes a new instance of the <see cref="LCH"/> struct.
        ///// </summary>
        //public LCH()
        //{
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="LCH"/> color.
        /// </summary>
        /// <param name="l">The lightness channel.</param>
        /// <param name="c">The chroma channel.</param>
        /// <param name="h">The hue channel.</param>
        /// <param name="space">The base color space.</param>
        public LCH(float l, float c, float h, BaseColorSpace space) : this()
        {
            L = l;
            C = c;
            H = h;
            ColorSpace = space;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LCH"/> struct.
        /// </summary>
        /// <param name="lch">The color in <see cref="LCH"/> color space.</param>
        public LCH(LCH lch) : this()
        {
            L = lch.L;
            C = lch.C;
            H = lch.H;
            ColorSpace = lch.ColorSpace;
        }

        #region convert

        /// <summary>
        /// Converts from <see cref="LCH"/> (ab) to <see cref="LAB"/> color space.
        /// </summary>
        /// <param name="l">The lightness channel.</param>
        /// <param name="c">The chroma channel.</param>
        /// <param name="h">The hue channel.</param>
        /// <returns>
        /// The color in <see cref="LAB"/> color space.
        /// </returns>
        public static LAB ToCIELab(float l, float c, float h)
        {
            return new LAB
            {
                L = l,
                A = c * (float)Math.Cos(h),
                B = c * (float)Math.Sin(h)
            };
        }

        /// <summary>
        /// Converts from <see cref="LCH"/> (ab) to <see cref="LAB"/>  color space.
        /// </summary>
        /// <param name="lch">The source color <see cref="LCH"/> (ab) in color space.</param>
        /// <returns>
        /// The color in <see cref="LAB"/> color space.
        /// </returns>
        public static LAB ToCIELab(LCH lch)
        {
            return ToCIELab(lch.L, lch.C, lch.H);
        }

        /// <summary>
        /// Converts from <see cref="LAB"/> to <see cref="LCH"/> (ab) color space.
        /// </summary>
        /// <param name="l">The L channel.</param>
        /// <param name="a">The A channel.</param>
        /// <param name="b">The B channel.</param>
        /// <returns>
        /// The color in <see cref="LCH"/> (ab) color space.
        /// </returns>
        public static LCH FromCIELab(float l, float a, float b)
        {
            return new LCH
            {
                L = l,
                C = (float)Math.Sqrt(a * a + b * b),
                H = (float)Math.Atan(b / a),
                ColorSpace = BaseColorSpace.LAB
            };
        }

        /// <summary>
        /// Converts from <see cref="LAB"/> to <see cref="LCH"/> (ab) color space.
        /// </summary>
        /// <param name="lab">The source color <see cref="LAB"/> in color space.</param>
        /// <returns>
        /// The color in <see cref="LCH"/> (ab) color space.
        /// </returns>
        public static LCH FromCIELab(LAB lab)
        {
            return FromCIELab(lab.L, lab.A, lab.B);
        }

        /// <summary>
        /// Converts from <see cref="LCH"/> (uv) to <see cref="LUV"/> color space.
        /// </summary>
        /// <param name="l">The lightness channel.</param>
        /// <param name="c">The chroma channel.</param>
        /// <param name="h">The hue channel.</param>
        /// <returns>
        /// The color in <see cref="LUV"/> in color space.
        /// </returns>
        public static LUV ToCIELuv(float l, float c, float h)
        {
            return new LUV
            {
                L = l,
                U = c * (float)(Math.Cos(h)),
                V = c * (float)(Math.Sin(h))
            };
        }

        /// <summary>
        /// Converts from <see cref="LCH"/> (uv) to <see cref="LUV"/> color space.
        /// </summary>
        /// <param name="lch">The source color in<see cref="LCH"/> (uv) color space.</param>
        /// <returns>
        /// The color in <see cref="LUV"/> color space.
        /// </returns>
        public static LUV ToCIELuv(LCH lch)
        {
            return ToCIELuv(lch.L, lch.C, lch.H);
        }

        /// <summary>
        /// Converts from <see cref="LUV"/> to <see cref="LCH"/> (uv) color space.
        /// </summary>
        /// <param name="l">The L channel.</param>
        /// <param name="u">The U channel.</param>
        /// <param name="v">The V channel.</param>
        /// <returns>
        /// The color in <see cref="LCH"/> (uv) in color space.
        /// </returns>
        public static LCH FromCIELuv(float l, float u, float v)
        {
            return new LCH
            {
                L = l,
                C = (float)(Math.Sqrt(u * u + v * v)),
                H = (float)(Math.Atan(v / u)),
                ColorSpace = BaseColorSpace.LUV
            };
        }

        /// <summary>
        /// Converts from <see cref="LUV"/> to <see cref="LCH"/> (uv) color space.
        /// </summary>
        /// <param name="luv">The source color in<see cref="LUV"/> color space.</param>
        /// <returns>
        /// The color in <see cref="LCH"/> (uv) color space.
        /// </returns>
        public static LCH FromCIELuv(LUV luv)
        {
            return FromCIELuv(luv.L, luv.U, luv.V);
        }

        /// <summary>
        /// Converts this <see cref="LCH"/> to <see cref="LAB"/> color space.
        /// </summary>
        /// <returns>
        /// The color in <see cref="LAB"/> color space.
        /// </returns>
        public LAB ToCIELab()
        {
            return ToCIELab(L, C, H);
        }

        /// <summary>
        /// Converts this <see cref="LCH"/> to <see cref="LUV"/> color space.
        /// </summary>
        /// <returns>
        /// The color in <see cref="LUV"/> color space.
        /// </returns>
        public LUV ToCIELuv()
        {
            return ToCIELuv(L, C, H);
        }

        /// <summary>
        /// Converts the instance into the target <see cref="IColorSpace"/>.
        /// </summary>
        /// <typeparam name="T">The target <see cref="IColorSpace"/> in which the instance should be converted.</typeparam>
        /// <returns>
        /// The converted <see cref="IColorSpace"/> instance.
        /// </returns>
        public T To<T>() where T : IColorSpace, new()
        {
            if (typeof(T) == typeof(LCH))
            {
                // just return a new LCH
                return new T { Color = Color };
            }

            if (typeof(T) == typeof(LAB))
            {
                // conversion to LAB is supported
                return new T { Color = ToCIELab().Color };
            }

            if (typeof(T) == typeof(LUV))
            {
                // conversion to LUV is supported
                return new T { Color = ToCIELuv().Color };
            }

            if (ColorSpace == BaseColorSpace.LAB)
            {
                LAB lab = ToCIELab();

                // convert from LAB to the target color space
                return lab.To<T>();
            }

            if (ColorSpace == BaseColorSpace.LUV)
            {
                LUV luv = ToCIELuv();

                // convert from LAB to the target color space
                return luv.To<T>();
            }

            // should be never reached because the ColorSpace is either LAB or LUV
            return new T();
        }

        /// <summary>
        /// Converts a source <see cref="IColorSpace"/> into this <see cref="IColorSpace"/>.
        /// </summary>
        /// <typeparam name="T">The source <see cref="IColorSpace"/> from which this instance should be converted.</typeparam>
        /// <param name="color">The <see cref="IColorSpace"/> to be converted.</param>
        public void From<T>(T color) where T : IColorSpace, new()
        {
            if (typeof(T) == typeof(LCH))
            {
                // just return a new LCH
                Color = color.Color;
                return;
            }

            if (typeof(T) == typeof(LAB))
            {
                // conversion to LAB is supported
                Color = FromCIELab(color.Color.A, color.Color.B, color.Color.C).Color;
                return;
            }

            if (typeof(T) == typeof(LUV))
            {
                // conversion to LUV is supported
                Color = FromCIELuv(color.Color.A, color.Color.B, color.Color.C).Color;
                return;
            }

            if (ColorSpace == BaseColorSpace.LAB)
            {
                // try to convert to LAB
                LAB lab = color.To<LAB>();

                // convert from LAB to the target color space
                Color = FromCIELab(lab).Color;
                return;
            }

            if (ColorSpace == BaseColorSpace.LUV)
            {
                // try to convert to LUV
                LUV luv = color.To<LUV>();

                // convert from LUV to the target color space
                Color = FromCIELuv(luv).Color;
                return;
            }
        }

        #endregion
    } 
}
