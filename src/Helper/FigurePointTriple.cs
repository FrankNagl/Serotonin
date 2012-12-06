// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
namespace SBIP.Helper
{
    using System;
    using System.Collections;

    /// <summary>
    /// Triple of points to compare independently by permutations of its points.
    /// </summary>
    [Serializable]
    public class FigurePointTriple : IEquatable<FigurePointTriple>, IEnumerator, IEnumerable
    {
        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        private int pos = -1;

        /// <summary>The first point of the triple.</summary>
        public FigurePoint P1 { get; set; }

        /// <summary>The second point of the triple.</summary>
        public FigurePoint P2 { get; set; }

        /// <summary>The third point of the triple.</summary>
        public FigurePoint P3 { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FigurePointTriple"/> class.
        /// </summary>
        /// <param name="p1">The first point of the triple.</param>
        /// <param name="p2">The second point of the triple.</param>
        /// <param name="p3">The third point of the triple.</param>
        public FigurePointTriple(FigurePoint p1, FigurePoint p2, FigurePoint p3)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
        }

        /// <summary>Copies the points of the triple into a new array.</summary>
        /// <returns>The array with the points of the triple.</returns>
        public FigurePoint[] ToArray()
        {
            return new [] {P1, P2, P3};
        }

        /// <summary>Copies the points of the triple into a new array of 
        /// data type <see cref="System.Drawing.Point"/>.</summary>
        /// <returns>The array with the <see cref="System.Drawing.Point"/>s of 
        /// the triple.</returns>
        public System.Drawing.Point[] ToPointArray()
        {
            return new[] { P1.ToPoint(), P2.ToPoint(), P3.ToPoint() };
        }

        /// <summary>
        ///Indicates whether the current <see cref="FigurePointTriple"/> is equal to 
        /// another <see cref="FigurePointTriple"/> of the same type.
        /// NOTE: Permutations are allowed, means permutated points are also equal.
        /// </summary>
        /// <param name="other">An <see cref="FigurePointTriple"/> to compare with 
        /// this <see cref="FigurePointTriple"/>.</param>
        /// <returns>
        /// true if the current object is equal to the other parameter; 
        /// otherwise, false.
        /// </returns>
        public bool Equals(FigurePointTriple other)
        {
            // PERMUTATIONS
            // P1 P2 P3
            // P1 P3 P2
            // P2 P1 P3
            // P2 P3 P1
            // P3 P1 P2
            // P3 P2 P1

            if (// P1 P2 P3
                P1 == other.P1 && P2 == other.P2 && P3 == other.P3 ||
                // P1 P3 P2
                P1 == other.P1 && P3 == other.P2 && P2 == other.P3 ||
                // P2 P1 P3
                P2 == other.P1 && P1 == other.P2 && P3 == other.P3 ||
                // P2 P3 P1
                P2 == other.P1 && P3 == other.P2 && P1 == other.P3 ||
                // P3 P1 P2
                P3 == other.P1 && P1 == other.P2 && P2 == other.P3 ||
                // P3 P2 P1
                P3 == other.P1 && P2 == other.P2 && P1 == other.P3)
            {
                return true;
            }
            return false;
        }

        #region IEnumerator Member

        public object Current
        {
            get
            {
                try
                {
                    switch(pos)
                    {
                        case 0:
                            return P1;
                        case 1:
                            return P2;
                        case 2:
                            return P3;
                        default:
                            return P1;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        {
            pos++;
            return (pos < 3);
        }

        public void Reset()
        {
            pos = -1;
        }

        #endregion

        #region IEnumerable Member

        public IEnumerator GetEnumerator()
        {
            return this; //new FigurePointTriple(P1, P2, P3);
        }

        #endregion
    }
}