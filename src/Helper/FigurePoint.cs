// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2012
// admin@franknagl.de
//
namespace SBIP.Helper
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Advancement of <see cref="Point"/> by properties, specified for points 
    /// of <see cref="Figure"/>s.
    /// </summary>
    [Serializable]
    public struct FigurePoint : ICloneable
    {
        /// <summary>X-component.</summary>
        public int X { get; set; }
        /// <summary>Y-component.</summary>
        public int Y { get; set; }

        /// <summary>Id of corresponding <see cref="Figure"/>.</summary>
        public int FigureId { get; private set; }

        /// <summary>Id of the <see cref="FigurePoint"/>.</summary>
        public int Id { get; private set; }

        /// <summary>OPTIONAL: Id of corresponding 3d key point.</summary>
        public int KeyPointId { get; set; }

        /// <summary>
        /// Id of parent (orignal) corresponding <see cref="Figure"/>.
        /// </summary>
        public int ParentFigureId { get; private set; }

        /// <summary>
        /// Id of the parent (orignal) <see cref="FigurePoint"/>.
        /// </summary>
        public int ParentId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FigurePoint"/> struct.
        /// </summary>
        /// <param name="x">The x component.</param>
        /// <param name="y">The y component.</param>
        /// <param name="id">The unique id of the <see cref="FigurePoint"/>.</param>
        /// <param name="figureId">The Id of corresponding <see cref="Figure"/>.</param>
        /// <param name="parentId">The parent id.</param>
        /// <param name="parentFigureId">The parent figure id.</param>
        /// <param name="keyPointId">The key point id.</param>
        private FigurePoint(
            int x,
            int y,
            int id,
            int figureId,
            int parentId,
            int parentFigureId,
            int keyPointId)
             : this()
        {
            X = x;
            Y = y;
            Id = id;
            FigureId = figureId;
            ParentId = parentId;
            ParentFigureId = parentFigureId;
            KeyPointId = keyPointId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FigurePoint"/> struct.
        /// </summary>
        /// <param name="x">The x component.</param>
        /// <param name="y">The y component.</param>
        /// <param name="id">The unique id of the <see cref="FigurePoint"/>.</param>
        /// <param name="figureId">The Id of corresponding <see cref="Figure"/>.</param>
        /// <param name="keyPointId">The key point id.</param>
        public FigurePoint(
            int x, 
            int y, 
            int id, 
            int figureId, 
            int keyPointId = -1) : this(x, y, id, figureId, -1, -1, keyPointId)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FigurePoint"/> struct.
        /// </summary>
        /// <param name="parent">The parent <see cref="FigurePoint"/>.</param>
        /// <param name="id">
        /// The unique id of the <see cref="FigurePoint"/>.</param>
        /// <param name="figureId">
        /// The Id of corresponding <see cref="Figure"/>.</param>
        /// <param name="keyPointId">The key point id.</param>
        public FigurePoint(
            FigurePoint parent, int id, int figureId, int keyPointId = -1) : 
            this(parent.X, parent.Y, id, figureId, parent.Id, parent.FigureId, keyPointId)
        { }

        /// <summary>
        /// Returns the X- and Y-component as <see cref="Point"/>.
        /// </summary>
        /// <returns>The X- and Y-component as <see cref="Point"/>.</returns>
        public Point ToPoint()
        {
            return new Point(X, Y);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="p1">The first <see cref="FigurePoint"/>.</param>
        /// <param name="p2">The second <see cref="FigurePoint"/>.</param>
        /// <returns> The bool result of the operator. </returns>
        public static bool operator ==(FigurePoint p1, FigurePoint p2)
        {
            return p1.Equals(p2);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="p1">The first <see cref="FigurePoint"/>.</param>
        /// <param name="p2">The second <see cref="FigurePoint"/>.</param>
        /// <returns> The bool result of the operator. </returns>
        public static bool operator !=(FigurePoint p1, FigurePoint p2)
        {
            return !p1.Equals(p2);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(FigurePoint other)
        {
            return other.X == X && 
                   other.Y == Y && 
                   other.Id == Id && 
                   other.FigureId == FigureId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return obj.GetType() == typeof(FigurePoint) && Equals((FigurePoint)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = X;
                result = (result * 397) ^ Y;
                result = (result * 397) ^ Id;
                result = (result * 397) ^ FigureId;
                return result;
            }
        }

        #region ICloneable Member

        public object Clone()
        {
            return new FigurePoint(
                X, Y, Id, FigureId, ParentId, ParentFigureId, KeyPointId);
        }

        /// <summary>
        /// Clones the this instance and sets a new <see cref="KeyPointId"/>.
        /// </summary>
        /// <param name="newKeyPointId">The new key point id to set.</param>
        /// <returns>The cloned instance with new key pint id.</returns>
        public FigurePoint Clone(int newKeyPointId)
        {
            return new FigurePoint(
                X, Y, Id, FigureId, ParentId, ParentFigureId, newKeyPointId);
        }

        #endregion
    }
}
