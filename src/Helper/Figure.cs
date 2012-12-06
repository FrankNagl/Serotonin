// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
namespace SBIP.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    /// <summary> Represents a polygon figure in an image. </summary>
    [Serializable]
    public class Figure
    {
        /// <summary>Default colors for the several <see cref="Type"/>s.</summary>
        public static readonly Dictionary<FigureType, Color> DefaultColors =
            new Dictionary<FigureType, Color>
                {
                    {FigureType.Common, Color.CornflowerBlue},
                    {FigureType.Line, Color.Red},
                    {FigureType.Orthogonal, Color.Green},
                    {FigureType.Background, Color.Yellow}
                };

        /// <summary>
        /// This Pen-Object is used to check, if a point is located over the 
        /// line of an object. The color is irrelevant. As width the 
        /// value <code>4</code> makes a good job.
        /// </summary>
        private static readonly Pen HitTestPen = new Pen(Brushes.Black, 4);
        [NonSerialized] private GraphicsPath path;

        private List<FigurePointTriple> triples;

        /// <summary> Corner points of the polygon figure. </summary>
        public List<FigurePoint> Corners { get; set; }

        ///// <summary>Default colors for the several <see cref="Type"/>s.</summary>
        //public static Dictionary<FigureType, Color> DefaultColors
        //{
        //    get
        //    {
        //        return defaultColors;
        //    }
        //}

        /// <summary>
        /// Unique id of the figure to distinguish all figuresd in an image.
        /// </summary>
        public int Id { get; private set; }

        /// <summary> The line strength of drawing the figure. </summary>
        public int LineStrength { get; set; }

        /// <summary>
        /// Triples built from <see cref="Corners"/>, which are inside the 
        /// figure.
        /// </summary>
        public List<FigurePointTriple> Triples
        {
            get
            {
                if (triples != null || Corners == null)
                {
                    return triples;
                }

                triples = new List<FigurePointTriple>(Corners.Count / 3);

                foreach (FigurePoint p in Corners)
                {
                    foreach (FigurePoint p1 in Corners)
                    {
                        // FIRST LINE CHECK
                        // 50 % of line between p and p1
                        Point a = 
                            new Point((p.X + p1.X) / 2, (p.Y + p1.Y) / 2);
                        // 25 % of line between p and p1
                        Point b =
                            new Point((p.X + a.X) / 2, (p.Y + a.Y) / 2);
                        // 12,5 % of line between p and p1
                        Point c =
                            new Point((p.X + b.X) / 2, (p.Y + b.Y) / 2);
                        // 6,25 % of line between p and p1
                        Point d =
                            new Point((p.X + c.X) / 2, (p.Y + c.Y) / 2);


                        // 75 % of line between p and p1
                        Point e =
                            new Point((p1.X + a.X) / 2, (p1.Y + a.Y) / 2);                        
                        // 87,5 % of line between p and p1
                        Point f =
                            new Point((p1.X + e.X) / 2, (p1.Y + e.Y) / 2);
                        // 93,75 % of line between p and p1
                        Point g =
                            new Point((p1.X + f.X) / 2, (p1.Y + f.Y) / 2);

                        if (p == p1 ||
                            !Hit(a) && !Contains(a) ||
                            !Hit(b) && !Contains(b) ||
                            !Hit(c) && !Contains(c) ||
                            //!Hit(d) && !Contains(d) ||
                            !Hit(e) && !Contains(e) ||
                            !Hit(f) && !Contains(f) )// ||
                            //!Hit(g) && !Contains(g))
                        {
                            continue;
                        }

                        foreach (FigurePoint p2 in Corners)
                        {
                            // SECOND LINE CHECK
                            // 50 % of line between p and p1
                            Point a2 =
                                new Point((p.X + p2.X) / 2, (p.Y + p2.Y) / 2);
                            // 25 % of line between p and p1
                            Point b2 =
                                new Point((p.X + a2.X) / 2, (p.Y + a2.Y) / 2);
                            // 12,5 % of line between p and p1
                            Point c2 =
                                new Point((p.X + b2.X) / 2, (p.Y + b2.Y) / 2);
                            // 6,25 % of line between p and p1
                            Point d2 =
                                new Point((p.X + c2.X) / 2, (p.Y + c2.Y) / 2);
                            // 75 % of line between p and p1
                            Point e2 =
                                new Point((p2.X + a2.X) / 2, (p2.Y + a2.Y) / 2);
                            // 87,5 % of line between p and p1
                            Point f2 =
                                new Point((p2.X + e2.X) / 2, (p2.Y + e2.Y) / 2);
                            // 93,75 % of line between p and p1
                            Point g2 =
                                new Point((p2.X + f2.X) / 2, (p2.Y + f2.Y) / 2);

                            // THIRD LINE CHECK
                            // 50 % of line between p1 and p2 
                            Point a3 =
                                new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
                            // 25 % of line between p and p1
                            Point b3 =
                                new Point((p1.X + a3.X) / 2, (p1.Y + a3.Y) / 2);
                            // 12,5 % of line between p and p1
                            Point c3 =
                                new Point((p1.X + b3.X) / 2, (p1.Y + b3.Y) / 2);
                            // 6,25 % of line between p and p1
                            Point d3 =
                                new Point((p1.X + c3.X) / 2, (p1.Y + c3.Y) / 2);
                            // 75 % of line between p and p1
                            Point e3 =
                                new Point((p2.X + a3.X) / 2, (p2.Y + a3.Y) / 2);
                            // 87,5 % of line between p and p1
                            Point f3 =
                                new Point((p2.X + e3.X) / 2, (p2.Y + e3.Y) / 2);
                            // 93,75 % of line between p and p1
                            Point g3 =
                                new Point((p2.X + f3.X) / 2, (p2.Y + f3.Y) / 2);


                            if (p == p2 ||
                                p1 == p2 ||
                                !Hit(a2) && !Contains(a2) ||
                                !Hit(b2) && !Contains(b2) ||
                                !Hit(c2) && !Contains(c2) ||
                                //!Hit(d2) && !Contains(d2) ||
                                !Hit(e2) && !Contains(e2) ||
                                !Hit(f2) && !Contains(f2) ||
                                //!Hit(g2) && !Contains(g2) ||
                                !Hit(a3) && !Contains(a3) ||
                                !Hit(b3) && !Contains(b3) ||
                                !Hit(c3) && !Contains(c3) ||
                                //!Hit(d3) && !Contains(d3) ||
                                !Hit(e3) && !Contains(e3) ||
                                !Hit(f3) && !Contains(f3) )//||
                                //!Hit(g3) && !Contains(g3))
                            {
                                continue;
                            }

                            FigurePointTriple tri = new FigurePointTriple(p, p1, p2);
                            if (!triples.Contains(tri))
                            {
                                triples.Add(tri);
                            }
                        }
                    }
                }

                return triples;
            }
        }

        /// <summary> Characterizes the position of the figure. </summary>
        public FigureType Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Figure"/> class.
        /// </summary>
        /// <param name="id">The unique id of the figure.</param>
        /// <param name="type">The position type of the figure.</param>
        public Figure(int id, FigureType type)
        {
            Id = id;
            Corners = new List<FigurePoint>();
            path = new GraphicsPath();
            Type = type;
            LineStrength = 2;
        }

        /// <summary>
        /// Adds the specified point as corner of the figure.
        /// </summary>
        /// <param name="pt">The point to add.</param>
        public void AddCorner(FigurePoint pt)
        {            
            Corners.Add(pt);
            RefillPathWithCorners();
        }

        /// <summary>
        /// Deserializes objects, which are not serializable 
        /// (e.g. <see cref="path"/>). Note: Call this method <b>only after</b>
        /// deserialization.
        /// </summary>
        public void Deserialize()
        {
            path = new GraphicsPath();
            RefillPathWithCorners();
        }

        /// <summary>
        /// Checks, if the given point is inside the object.
        /// </summary>
        public virtual bool Contains(Point pt)
        {
            return path.IsVisible(pt);
        }

        /// <summary> Draws the figure into the specified graphics object. </summary>
        /// <param name="graphics">The graphics object to use for drawing.</param>
        public virtual void Draw(
            Graphics graphics)
        {
            graphics.DrawPath(new Pen(GetPenColor(), LineStrength), path);
        }

        /// <summary> Draws the figure into the specified image. </summary>
        /// <param name="image">The image object to use for drawing.</param>
        public virtual void Draw(Image image)
        {
            Graphics g = Graphics.FromImage(image);
            Draw(g);
        }

        /// <summary>
        /// Draws the corners of the figure into the specified graphics object.
        /// </summary>
        /// <param name="graphics">The graphics object to use for drawing.</param>
        /// <param name="crossColor">
        /// The color to use for drawing the crosses. </param>
        /// <param name="crossRadius">
        /// The radius of the cross. Default: 5px.</param>
        public virtual void DrawCorners(
            Graphics graphics,
            Color crossColor,
            int crossRadius = 5)
        {
            // map to byte            
            byte radius = (byte)
                       (crossRadius < 1 || crossRadius > 255 ? 5 : crossRadius);
            
            foreach (FigurePoint corner in Corners)
            {
                DrawingGraphics.DrawCross(
                    graphics, corner.ToPoint(), crossColor, LineStrength, radius);
            }
        }

        /// <summary>
        /// Draws the corners of the figure into the specified image.
        /// </summary>
        /// <param name="image">The image object to use for drawing.</param>
        /// <param name="crossColor">
        /// The color to use for drawing the crosses. </param>
        /// <param name="crossRadius">
        /// The radius of the cross. Default: 5px.</param>
        public virtual void DrawCorners(
            Image image, 
            Color crossColor,
            int crossRadius = 5)
        {
            Graphics g = Graphics.FromImage(image);
            DrawCorners(g, crossColor, crossRadius);
        }

        /// <summary> Draws the figure's <see cref="Triples"/>s into the 
        /// specified graphics object. </summary>
        /// <param name="graphics">The graphics object to use for drawing.</param>
        public virtual void DrawTriples(Graphics graphics)
        {
            if (Triples == null)
            {
                return;
            }

            Bitmap b = new Bitmap(
                1, 1, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            // get palette
            var cp = b.Palette;
            int index = 0;
            int black = Color.Black.ToArgb();

            GraphicsPath triplePath = new GraphicsPath();
            foreach (FigurePointTriple triple in Triples)
            {
                while (cp.Entries[index].ToArgb() == black)
                {
                    if (index == 255)
                    {
                        index = 0;
                    }
                    else
                    {
                        index++;
                    }
                }

                triplePath.Reset();
                triplePath.AddPolygon(triple.ToPointArray());
                //triplePath.CloseFigure();
                index++;
                if (index == 255)
                {
                    index = 1;
                }
                graphics.DrawPath(
                    new Pen(cp.Entries[index], LineStrength), triplePath);
                // new Controls.SimpleImageViewer(bb, index.ToString());
            }
        }

        /// <summary> Draws the figure's <see cref="Triples"/>s into the 
        /// specified image. </summary>
        /// <param name="image">The image object to use for drawing.</param>
        public virtual void DrawTriples(Image image)
        {
            Graphics g = Graphics.FromImage(image);
            DrawTriples(g);
        }

        /// <summary>Fills the figure into the specified graphics object.</summary>
        /// <param name="graphics">The graphics object to use for filling.</param>
        /// <param name="transpacency">The transpacency value for filling.</param>
        public virtual void Fill(
            Graphics graphics,
            int transpacency = 128)
        {
            graphics.FillPath(
                new SolidBrush(Color.FromArgb(transpacency, GetPenColor())),
                path);
        }

        /// <summary> Fills the figure into the specified image. </summary>
        /// <param name="image">The image object to use for filling.</param>
        /// <param name="transpacency">The transpacency value for filling.</param>
        public virtual void Fill(
            Image image, 
            int transpacency = 128)
        {
            Graphics g = Graphics.FromImage(image);
            Fill(g, transpacency);
        }
        
        /// <summary>
        /// Checks, if the given point is on the contour of the object.
        /// </summary>
        public virtual bool Hit(Point pt)
        {
            return path.IsOutlineVisible(pt, HitTestPen);
        }

        /// <summary> Resets the figure's path and corner points. </summary>
        public virtual void Reset()
        {
            path.Reset();
            Corners.Clear();
        }

        /// <summary>
        /// Scales the figure with specified scale factors for
        /// x-direction (horizontal) and y-direction (vertical).
        /// </summary>
        public virtual void Scale(float scaleX, float scaleY)
        {
            for (int i = 0; i < Corners.Count; i++)
            {
                FigurePoint pt = Corners[i];
                pt.X = (int)(pt.X * scaleX + 0.5f);
                pt.Y = (int)(pt.Y * scaleY + 0.5f);
                Corners[i] = pt;
            }

            Matrix mat = new Matrix();
            mat.Scale(scaleX, scaleY);
            path.Transform(mat);
        }

        /// <summary>
        /// Moves the figure <param name="deltaX" /> pixel in 
        /// x-direction (horizontal) and <param name="deltaY" /> pixel in 
        /// y-direction (vertical).
        /// </summary>
        public virtual void Translate(int deltaX, int deltaY)
        {
            Matrix mat = new Matrix();
            mat.Translate(deltaX, deltaY);
            path.Transform(mat);
        }

        //public void Print()
        //{
        //    Console.WriteLine("CORNER X: Corners-List" + "\t" + "Path-List\n");
        //    for (int i = 0; i < Corners.Count; i++)
        //    {
        //        Point pt = Corners[i];
        //        PointF pf = path.PathPoints[i];
        //        Console.WriteLine("Corner " + i + ": " + pt + "\t" + pf);
        //    }
        //}

        private Color GetPenColor()
        {
                Color col;
                DefaultColors.TryGetValue(Type, out col);
                return col;
        }

        private void RefillPathWithCorners()
        {
            path.Reset();
            switch (Corners.Count)
            {
                case 1:
                    return;
                case 2:
                    path.AddLine(Corners[0].ToPoint(), Corners[1].ToPoint());
                    break;
                default:
                    Point[] array = new Point[Corners.Count];
                    for (int i = 0; i < Corners.Count; i++)
                    {
                        array[i] = Corners[i].ToPoint();
                    }
                    path.AddPolygon(array);
                    break;
            }
        }
    }
}
