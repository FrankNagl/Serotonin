// <copyright file="SRGB.cs" company="Erfurt University of Applied Sciences">
//   Image Processing 3D Copyright © 2007-2010 Erfurt University of Applied Sciences. All Right Reserved.
// </copyright>
// <web>http://www.fh-erfurt.de</web>
// <revision>$Revision: 146 $</revision>
// <url>$URL: http://gdv.fh-erfurt.de/svn/ip3d/Filter/src/ColorSpace/sRGB.cs $</url>
// <author>$Author: Nagl.Frank $</author>
// <date>$Date: 2011-10-06 16:47:09 +0200 (Do, 06 Okt 2011) $</date>

using System;

namespace Serotonin.ColorSpace
{
    /// <summary>
    /// A struct which defines the SRGB color space.
    /// </summary>
    /// <remarks>
    /// Further information for conversion: 
    /// http://www.brucelindbloom.com/index.html?Eqn_XYZ_to_RGB.html
    /// </remarks>
    public struct SRGB : IColorSpace
    {
        /// <summary>
        /// Gets an empty <see cref="SRGB"/> instance;
        /// </summary>
        public static readonly SRGB Empty;

        #region Accessors

        /// <summary>
        /// Gets or sets triple of color channels.
        /// </summary>
        /// <value>The color triple.</value>
        public ColorTriple Color
        {
            get
            {
                return new ColorTriple(R, G, B);
            }
            set
            {
                R = value.A;
                G = value.B;
                B = value.C;
            }
        }

        /// <summary>
        /// Gets or sets the red channel.
        /// </summary>
        /// <value>The red channel.</value>
        public float Red
        {
            get { return R; } set { R = value; } 
        }

        /// <summary>
        /// Gets or sets the green channel.
        /// </summary>
        /// <value>The green channel.</value>
        public float Green
        {
            get { return G; } set { G = value; }
        }

        /// <summary>
        /// Gets or sets the blue channel.
        /// </summary>
        /// <value>The blue channel.</value>
        public float Blue
        {
            get { return B; } set { B = value; }
        }

        /// <summary>
        /// Short accessor for the red channel.
        /// </summary>
        /// <value>The red channel.</value>
        public float R { get; set; }

        /// <summary>
        /// Short accessor for the green channel.
        /// </summary>
        /// <value>The green channel.</value>
        public float G { get; set; }

        /// <summary>
        /// Short accessor for the blue channel.
        /// </summary>
        /// <value>The blue channel.</value>
        public float B { get; set; }

        #endregion

        ///// <summary>
        ///// Initializes a new instance of the <see cref="SRGB"/> struct.
        ///// </summary>
        //public SRGB()
        //{
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="SRGB"/> struct.
        /// </summary>
        /// <param name="r">The red channel.</param>
        /// <param name="g">The green channel.</param>
        /// <param name="b">The blue channel.</param>
        public SRGB(float r, float g, float b) : this()
        {
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SRGB"/> struct.
        /// </summary>
        /// <param name="srgb">The color in <see cref="SRGB"/> color space.</param>
        public SRGB(SRGB srgb) : this()
        {
            R = srgb.R;
            G = srgb.G;
            B = srgb.B;
        }

        #region convert

        /// <summary>
        /// Converts from <see cref="SRGB"/> to <see cref="RGB"/> color space.
        /// </summary>
        /// <param name="r">The red channel.</param>
        /// <param name="g">The green channel.</param>
        /// <param name="b">The blue channel.</param>
        /// <returns>The color in <see cref="RGB"/> color space.</returns>
        public static RGB ToRGB(double r, double g, double b)
        {
            // convert SRGB (D65) to liniar RGB
            var red = (r > 0.0031308) ? (1 + 0.055) * Math.Pow(r, (1.0 / 2.4)) - 0.055 : 12.92 * r;
            var green = (g > 0.0031308) ? (1 + 0.055) * Math.Pow(g, (1.0 / 2.4)) - 0.055 : 12.92 * g;
            var blue = (b > 0.0031308) ? (1 + 0.055) * Math.Pow(b, (1.0 / 2.4)) - 0.055 : 12.92 * b;

            // denormalize
            return new RGB
            {
                R = (byte)Math.Round(red * 255.0),
                G = (byte)Math.Round(green * 255.0),
                B = (byte)Math.Round(blue * 255.0)
            };
        }

        /// <summary>
        /// Converts <see cref="SRGB"/> to <see cref="RGB"/> structure.
        /// </summary>
        /// <param name="srgb">The color in <see cref="SRGB"/> color space.</param>
        /// <returns></returns>
        public static RGB ToRGB(SRGB srgb)
        {
            return ToRGB(srgb.R, srgb.G, srgb.B);
        }

        /// <summary>
        /// Converts from <see cref="RGB"/> to <see cref="SRGB"/> color space.
        /// </summary>
        /// <param name="r">The red channel.</param>
        /// <param name="g">The green channel.</param>
        /// <param name="b">The blue channel.</param>
        /// <returns>
        /// The color in <see cref="SRGB"/> color space.
        /// </returns>
        public static SRGB FromRGB(float r, float g, float b)
        {
            // normalize red, green, blue values
            float red = r / 255.0f;
            float green = g / 255.0f;
            float blue = b / 255.0f;

            // convert to SRGB
            return new SRGB
            {
                R = (red > 0.04045f) ? (float)(Math.Pow((red + 0.055f) / (1 + 0.055f), 2.4f)) : (red / 12.92f),
                G = (green > 0.04045f) ? (float)(Math.Pow((green + 0.055f) / (1 + 0.055f), 2.4f)) : (green / 12.92f),
                B = (blue > 0.04045f) ? (float)(Math.Pow((blue + 0.055f) / (1 + 0.055f), 2.4f)) : (blue / 12.92f)
            };
        }

        /// <summary>
        /// Converts from <see cref="RGB"/> to <see cref="SRGB"/> color space.
        /// </summary>
        /// <param name="rgb">The RGB.</param>
        /// <returns>
        /// The color in <see cref="SRGB"/> color space.
        /// </returns>
        public static SRGB FromRGB(RGB rgb)
        {
            return FromRGB(rgb.R, rgb.G, rgb.B);
        }

        /// <summary>
        /// Converts this <see cref="SRGB"/> to <see cref="RGB"/> color space.
        /// </summary>
        /// <returns>The color in <see cref="RGB"/> color space.</returns>
        public RGB ToRGB()
        {
            return ToRGB(R, G, B);
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
            if (typeof(T) == typeof(SRGB))
            {
                // just return a new SRGB
                return new T { Color = Color };
            }

            if (typeof(T) == typeof(RGB))
            {
                // conversion to RGB is supported
                return new T { Color = ToRGB().Color };
            }

            RGB rgb = ToRGB();

            // convert from RGB to the target color space
            return rgb.To<T>();
        }

        /// <summary>
        /// Converts a source <see cref="IColorSpace"/> into this <see cref="IColorSpace"/>.
        /// </summary>
        /// <typeparam name="T">The source <see cref="IColorSpace"/> from which this instance should be converted.</typeparam>
        /// <param name="color">The <see cref="IColorSpace"/> to be converted.</param>
        public void From<T>(T color) where T : IColorSpace, new()
        {
            if (typeof(T) == typeof(SRGB))
            {
                // just return a new SRGB
                Color = color.Color;
                return;
            }

            if (typeof(T) == typeof(RGB))
            {
                Color = FromRGB(color.Color.A, color.Color.B, color.Color.C).Color;
                return;
            }

            // try to convert to RGB
            RGB rgb = color.To<RGB>();

            // convert from XYZ to the target color space
            Color = FromRGB(rgb).Color;
        }

        #endregion
    } 
}
