// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2012
// admin@franknagl.de
//
//Some source code fragments from AForge.NET framework
// http://code.google.com/p/aforge/
//
namespace SBIP.Helper
{
    using System.Drawing;

    /// <summary>
    /// Helper class for drawing in a <see cref="Graphics"/> object.
    /// </summary>
    public static class DrawingGraphics
    {
        /// <summary>
        /// Draws a cross at <paramref name="point"/> into the
        /// <paramref name="graphics"/> object.
        /// </summary>
        /// <param name="graphics">The graphics object to draw in.</param>
        /// <param name="point">The coordinate to draw the cross.</param>
        /// <param name="color">The color of the cross.</param>
        /// <param name="lineStrength">The line strength of the cross.</param>
        /// <param name="crossRadius">The radius of the cross.</param>
        public static void DrawCross(
            Graphics graphics,
            Point point,
            Color color,
            float lineStrength,
            byte crossRadius
            )
        {
            graphics.DrawLine(
                new Pen(color, lineStrength),
                point.X - crossRadius,
                point.Y,
                point.X + crossRadius,
                point.Y);
            graphics.DrawLine(
                new Pen(color, lineStrength),
                point.X,
                point.Y - crossRadius,
                point.X,
                point.Y + crossRadius);
        }
    }
}
