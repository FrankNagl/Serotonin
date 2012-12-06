// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
namespace Serotonin.Helper
{
    using SBIP.Helper;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;

    /// <summary>All informations of a <see cref=" SBIP.Filter.NonSBIP.ConGrap"/>
    /// contour.</summary>
    public class Contour : IComparable
    {
        #region public properties
        /// <summary>Unique id of this contour.</summary>
        public int Id { get; private set; }
        /// <summary>The index for the indexed color.</summary>
        public byte Index { get; private set; }
        /// <summary>Number of corresponding pixels.</summary>
        public int Count { get; private set; }
        /// <summary>Left-X coordinate of the <see cref="BorderRectangle"/>.</summary>
        public int LeftX { get; private set; }
        /// <summary>Right-X coordinate of the <see cref="BorderRectangle"/>.</summary>
        public int RightX { get; private set; }
        /// <summary>Upper-Y coordinate of the <see cref="BorderRectangle"/>.</summary>
        public int UpY { get; private set; }
        /// <summary>Lower-Y coordinate of the <see cref="BorderRectangle"/>.</summary>
        public int BottomY { get; private set; }

        /// <summary>Contour's border as rectangle.</summary>
        public Rectangle BorderRectangle
        {
            get { return new Rectangle(LeftX, UpY, RightX - LeftX, BottomY - UpY); }
        }

        /// <summary>All pixel coordinates of the border line.</summary>
        public List<Point> BorderLine { get; private set; }

        /// <summary>The average pixel coordinate of <see cref="BorderLine"/>.
        /// </summary>
        public Point Centroid
        {
            get 
            { 
                Point centroid = Point.Empty;
                foreach (Point p in BorderLine)
                {
                    centroid.X += p.X;
                    centroid.Y += p.Y;
                }
                centroid.X /= BorderLine.Count;
                centroid.Y /= BorderLine.Count;

                return centroid;
            }
        }

        /// <summary>The center coordinate of <see cref="BorderRectangle"/>.
        /// </summary>
        public Point CentroidRect
        { 
            get 
            {
                return new Point((LeftX + RightX) / 2, (UpY + BottomY) / 2);
            }
        }

        #endregion public properties

        /// <summary>
        /// Initializes a new instance of the <see cref="Contour"/> class.
        /// </summary>
        /// <param name="id">The unique id for this contour.</param>
        /// <param name="index">The index for the indexed color.</param>
        public Contour(int id, byte index)
        {
            Id = id;
            Index = index;
            LeftX = int.MaxValue;
            RightX = 0;
            UpY = int.MaxValue;
            BottomY = 0;

            BorderLine = new List<Point>();
        }

        #region public methods

        /// <summary>
        /// Sorts upwards the contour's <see cref="BorderRectangle"/> size.
        /// Compares the current instance with another object of the same type and returns an 
        /// integer that indicates whether the current instance precedes, follows, or occurs 
        /// in the same position in the sort order as the other object.
        /// <remarks>Note: It sorts upward, NOT downward.</remarks>
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared.
        /// </returns>
        public int CompareTo(object obj)
        {
            if (obj is Contour)
            {
                Rectangle objRect = ((Contour)obj).BorderRectangle;
                if ((BorderRectangle.Width * BorderRectangle.Height) -
                    (objRect.Width * objRect.Height) < 0)
                {
                    return 1;
                }
                if ((BorderRectangle.Width * BorderRectangle.Height) -
                    (objRect.Width * objRect.Height) > 0)
                {
                    return -1;
                }
            }
            return 0;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Draws the <see cref="BorderLine"/> into the passed 
        /// <paramref name="data"/>.
        /// </summary>
        /// <param name="data">Source bitmap data to draw on.</param>
        public unsafe void DrawBorderLine(BitmapData data)
        {
            // DRAW BORDERLINE PIXELS IN IMAGE
            Byte* dst = (byte*)data.Scan0.ToPointer();
            foreach (Point px in BorderLine)
            {
                dst[px.Y * data.Stride + px.X] = Index;
            }
            return;
        }

        /// <summary>
        /// Draws the <see cref="BorderRectangle"/> into the passed
        /// <paramref name="data"/>.
        /// </summary>
        /// <param name="data">Source bitmap data to draw on.</param>
        /// <param name="lineRectStrength">The pixel strength of the contour's 
        /// rectangle border.</param>
        public void DrawBorderRect(BitmapData data, int lineRectStrength)
        {
            Drawing8Bpp.DrawLine(
              data, Index, new Point(LeftX, UpY), new Point(LeftX, BottomY), lineRectStrength);
            Drawing8Bpp.DrawLine(
              data, Index, new Point(LeftX, BottomY), new Point(RightX, BottomY), lineRectStrength);
            Drawing8Bpp.DrawLine(
              data, Index, new Point(RightX, BottomY), new Point(RightX, UpY), lineRectStrength);
            Drawing8Bpp.DrawLine(
              data, Index, new Point(RightX, UpY), new Point(LeftX, UpY), lineRectStrength);
        }

        /// <summary>
        /// Determines whether an pixel coordinate is enclosed in contour's
        /// <see cref="BorderLine"/>. Note: This method only works with closed 
        /// contours in the original contour image.
        /// </summary>
        /// <param name="point">The pixel coordinate to check.</param>
        /// <param name="data">The bitmap data of the contour image.</param>
        /// <returns><c>true</c>, if pixel coordinate is enclosed; 
        /// otherwise, <c>false</c></returns>
        public unsafe bool Intersect(Point point, BitmapData data)
        {
            // pre-check
            if (!IntersectBorderRect(point))
            {
                return false;
            }

            // 1.) check in X-direction
            Byte *dst = (byte*)data.Scan0.ToPointer();
            // allign pointer
            dst += data.Stride * point.Y + LeftX;

            bool isFirst = true;
            int start = 0;
            int end = 0;
            // for each pixel in row p.Y
            for (int x = LeftX; x <= RightX; x++, dst++)
            {
                if (*dst != Index)
                {
                    continue;
                }

                if (!isFirst)
                {
                    end = x;                    
                    continue;
                }

                start = x;
                isFirst = false;
            }

            if (start > point.X || end < point.X)
            {
                return false;
            }

            // 2.) check in Y-direction
            dst = (byte*)data.Scan0.ToPointer();
            // allign pointer  
            dst += data.Stride * UpY + point.X;
            isFirst = true;

            for (int y = UpY; y <= BottomY; y++, dst += data.Stride)
            {
                if (*dst != Index)
                {
                    continue;
                }

                if (!isFirst)
                {
                    end = y; 
                    continue;
                }

                start = y;
                isFirst = false;
            }

            if (start > point.Y || end < point.Y)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether a pixel coordinate is enclosed in 
        /// <see cref="BorderRectangle"/>.
        /// </summary>
        /// <param name="point">The pixel coordinate to check.</param>
        /// <returns><c>True</c> if the pixel coordinate is enclosed, 
        /// otherwise <c>false</c>.</returns>
        public bool IntersectBorderRect(Point point)
        {
            if (point.X < LeftX || point.X > RightX || point.Y < UpY || point.Y > BottomY)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Updates the <see cref="BorderRectangle"/> in dependency of the 
        /// passed <paramref name="point"/>.</summary>
        /// <param name="point">The coordinate of the added pixel.</param>
        public void UpdateBorderRect(Point point)
        {
            Count++;
            if (point.X < LeftX)
            {
                LeftX = point.X;
            }
            if (point.X > RightX)
            {
                RightX = point.X;
            }
            if (point.Y < UpY)
            {
                UpY = point.Y;
            }
            if (point.Y > BottomY)
            {
                BottomY = point.Y;
            }
        }
        #endregion public methods
    }
}
