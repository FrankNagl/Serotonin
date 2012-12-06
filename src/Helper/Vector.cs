// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//

using Serotonin.ColorSpace;

namespace SBIP.Helper
{
    using System.Drawing;
    using System;

    /// <summary>Helper class for vectors several types.</summary>
    public static class Vector
    {
        #region public methods

        /// <summary>
        /// The length (the absolute value) of the vector spanned by
        /// <paramref name="color1"/> and <paramref name="color2"/>.
        /// </summary>
        /// <param name="color1">The first color.</param>
        /// <param name="color2">The second color.</param>
        /// <returns>
        /// The length (the absolute value) of the spanned vector.
        /// </returns>
        public static float Distance(Color color1, Color color2)
        {
            return (float)Math.Sqrt(
              Math.Pow(color1.R - color2.R, 2) +
              Math.Pow(color1.G - color2.G, 2) +
              Math.Pow(color1.B - color2.B, 2));
        }

        /// <summary>
        /// The length (the absolute value) of the vector spanned by
        /// the point (<paramref name="triple1"/>) and the
        /// point (<paramref name="triple2"/>).
        /// </summary>
        /// <param name="triple1">The triple1.</param>
        /// <param name="triple2">The triple2.</param>
        /// <returns>
        /// The length (the absolute value) of the vector.
        /// </returns>
        public static float Distance(ColorTriple triple1, ColorTriple triple2)
        {
            return (float)Math.Sqrt(
              Math.Pow(triple1.A - triple2.A, 2) + 
              Math.Pow(triple1.B - triple2.B, 2) +
              Math.Pow(triple1.C - triple2.C, 2));
        }

        /// <summary>
        /// The length (the absolute value) of the vector spanned by 
        /// <paramref name="p1"/> and <paramref name="p2"/>.
        /// </summary>
        /// <param name="p1">The first point of the vector.</param>
        /// <param name="p2">The end point of the vector.</param>
        /// <returns>The length (the absolute value) of the vector.</returns>
        public static float Distance(Point p1, Point p2)
        {
            return (float)Math.Sqrt(
              Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        /// <summary>
        /// The length (the absolute value) of the vector spanned by
        /// the point (<paramref name="x1"/>, <paramref name="y1"/>) and the 
        /// point (<paramref name="x2"/>, <paramref name="y2"/>).
        /// </summary>
        /// <param name="x1">The X-coordinate of first vector.</param>
        /// <param name="y1">The Y-coordinate of first vector.</param>
        /// <param name="x2">The X-coordinate of second vector.</param>
        /// <param name="y2">The Y-coordinate of second vector.</param>
        /// <returns>
        /// The length (the absolute value) of the vector.
        /// </returns>
        public static float Distance(int x1, int y1, int x2, int y2)
        {
            return (float)Math.Sqrt(
              Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        /// <summary>
        /// The length (the absolute value) of the vector spanned by
        /// the vector (<paramref name="x1"/>, <paramref name="y1"/>) and the 
        /// vector (<paramref name="x2"/>, <paramref name="y2"/>).
        /// </summary>
        /// <param name="x1">The X-coordinate of first vector.</param>
        /// <param name="y1">The Y-coordinate of first vector.</param>
        /// <param name="x2">The X-coordinate of second vector.</param>
        /// <param name="y2">The Y-coordinate of second vector.</param>
        /// <returns>
        /// The length (the absolute value) of the vector.
        /// </returns>
        public static float Distance(float x1, float y1, float x2, float y2)
        {
            return (float)Math.Sqrt(
              Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }        

        /// <summary>
        /// The difference vector, separated in x- and y-direction,
        /// spanned by the points <paramref name="p1"/> and <paramref name="p2"/> as 
        /// <see cref="Size"/> object.
        /// </summary>
        /// <param name="p1">The first point of the vector.</param>
        /// <param name="p2">The end point of the vector.</param>
        /// <returns>The difference vector as <see cref="Size"/> object.</returns>
        public static Size Subtract(Point p1, Point p2)
        {
            Size s = Subtract(p1.X, p1.Y, p2.X, p2.Y);
            return s;
        }

        /// <summary>
        /// The difference vector, separated in x- and y-direction,
		/// spanned by the specified coordinates
        /// as <see cref="Size"/> object.
        /// </summary>
        /// <param name="x1">The X-coordinate of first vector.</param>
        /// <param name="y1">The Y-coordinate of first vector.</param>
        /// <param name="x2">The X-coordinate of second vector.</param>
        /// <param name="y2">The Y-coordinate of second vector.</param>
        /// <returns>The difference vector as <see cref="Size"/> object.</returns>
        public static Size Subtract(int x1, int y1, int x2, int y2)
        {
            Size s = new Size(x1 - x2, y1 - y2);
            return s;
        }

        #endregion public methods
    }
}
