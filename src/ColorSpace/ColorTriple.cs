// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
// ColorSpace namespace copyright © Tobias Bindel, 2011
//
namespace Serotonin.ColorSpace
{
    using SBIP.Helper;

    /// <summary>
    /// A struct which defines a combination of three color channels.
    /// </summary>
    public struct ColorTriple
    {
        /// <summary>
        /// Gets or sets the A channel.
        /// </summary>
        public float A { get; set; }

        /// <summary>
        /// Gets or sets the B channel.
        /// </summary>
        public float B { get; set; }

        /// <summary>
        /// Gets or sets the C channel.
        /// </summary>
        public float C { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorTriple"/> struct.
        /// </summary>
        /// <param name="a">The A channel.</param>
        /// <param name="b">The B channel.</param>
        /// <param name="c">The C channel.</param>
        public ColorTriple(float a, float b, float c)
            : this()
        {
            A = a;
            B = b;
            C = c;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorTriple"/> struct.
        /// </summary>
        /// <param name="color">The <see cref="ColorTriple"/>.</param>
        public ColorTriple(ColorTriple color) : this()
        {
            A = color.A;
            B = color.B;
            C = color.C;
        }

        /// <summary>
        /// Computes the euclidian distance between this color and the specified 
        /// <paramref name="foreignColor"/>.
        /// </summary>
        /// <param name="foreignColor">The foreign color to compare.</param>
        /// <returns>The euclidian distance between this color and the specified 
        /// <paramref name="foreignColor"/>.</returns>
        public float ColorDistance(ColorTriple foreignColor)
        {
            return Vector.Distance(this, foreignColor);
        }
    }
}