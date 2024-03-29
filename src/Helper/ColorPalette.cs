﻿// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
namespace Serotonin.Helper
{
    using System.Drawing;
    using SBIP.Filter.NonSBIP;

    /// <summary>
    /// Helper class for (re-)setting the bitmap's color palette.
    /// See function details for more information.
    /// </summary>
    public static class ColorPalette
    {
        /// <summary> All 140 system-defined colors. </summary>
        public static readonly Color[] Colors = new[]
        {
            Color.Red,
            Color.Green,
            Color.Blue,
                                   
            Color.Aquamarine,
            Color.Azure,
            Color.Beige,
            Color.Bisque,
            Color.Black,
            Color.BlanchedAlmond,
            Color.Aqua, // Color.Blue,
            Color.BlueViolet,
            Color.Brown,
            Color.BurlyWood,
            Color.CadetBlue,
            Color.Chartreuse,
            Color.Chocolate,
            Color.Coral,
            Color.CornflowerBlue,
            Color.Cornsilk,
            Color.Crimson,
            Color.Cyan,
            Color.DarkBlue,
            Color.DarkCyan,
            Color.DarkGoldenrod,
            Color.DarkGray,
            Color.DarkGreen,
            Color.DarkKhaki,
            Color.DarkMagenta,
            Color.DarkOliveGreen,
            Color.DarkOrange,
            Color.DarkOrchid,
            Color.DarkRed,
            Color.DarkSalmon,
            Color.DarkSeaGreen,
            Color.DarkSlateBlue,
            Color.DarkSlateGray,
            Color.DarkTurquoise,
            Color.DarkViolet,
            Color.DeepPink,
            Color.DeepSkyBlue,
            Color.DimGray,
            Color.DodgerBlue,
            Color.Firebrick,
            Color.FloralWhite,
            Color.ForestGreen,
            Color.Fuchsia,
            Color.Gainsboro,
            Color.GhostWhite,
            Color.Gold,
            Color.Goldenrod,
            Color.Gray,
            // Color.AntiqueWhite, // Color.Green,
            Color.GreenYellow,
            Color.Honeydew,
            Color.HotPink,
            Color.IndianRed,
            Color.Indigo,
            Color.Ivory,
            Color.Khaki,
            Color.Lavender,
            Color.LavenderBlush,
            Color.LawnGreen,
            Color.LemonChiffon,
            Color.LightBlue,
            Color.LightCoral,
            Color.LightCyan,
            Color.LightGoldenrodYellow,
            Color.LightGray,
            Color.LightGreen,
            Color.LightPink,
            Color.LightSalmon,
            Color.LightSeaGreen,
            Color.LightSkyBlue,
            Color.LightSlateGray,
            Color.LightSteelBlue,
            Color.LightYellow,
            Color.Lime,
            Color.LimeGreen,
            Color.Linen,
            Color.Magenta,
            Color.Maroon,
            Color.MediumAquamarine,
            Color.MediumBlue,
            Color.MediumOrchid,
            Color.MediumPurple,
            Color.MediumSeaGreen,
            Color.MediumSlateBlue,
            Color.MediumSpringGreen,
            Color.MediumTurquoise,
            Color.MediumVioletRed,
            Color.MidnightBlue,
            Color.MintCream,
            Color.MistyRose,
            Color.Moccasin,
            // Color.NavajoWhite,
            Color.Navy,
            Color.OldLace,
            Color.Olive,
            Color.OliveDrab,
            Color.Orange,
            Color.OrangeRed,
            Color.Orchid,
            Color.PaleGoldenrod,
            Color.PaleGreen,
            Color.PaleTurquoise,
            Color.PaleVioletRed,
            Color.PapayaWhip,
            Color.PeachPuff,
            Color.Peru,
            Color.Pink,
            Color.Plum,
            Color.PowderBlue,
            Color.Purple,
            Color.AliceBlue, // Color.Red,  
            Color.RosyBrown,
            Color.RoyalBlue,
            Color.SaddleBrown,
            Color.Salmon,
            Color.SandyBrown,
            Color.SeaGreen,
            Color.SeaShell,
            Color.Sienna,
            Color.Silver,
            Color.SkyBlue,
            Color.SlateBlue,
            Color.SlateGray,
            Color.Snow,
            Color.SpringGreen,
            Color.SteelBlue,
            Color.Tan,
            Color.Teal,
            Color.Thistle,
            Color.Tomato,
            Color.Turquoise,
            Color.Violet,
            Color.Wheat,
            // Color.White,
            Color.WhiteSmoke,
            Color.Yellow,
            Color.YellowGreen
        };

        /// <summary>
        /// Number of all <see cref="Colors"/> (<value>140</value> colors).
        /// </summary>
        public static int ColorCount = Colors.Length;

        /// <summary>
        /// Sets the color palette of the <paramref name="bitmap"/> to the 
        /// canny edges chromatic circle.
        /// <para><b>Canny edge's chromatic circle:</b></para>
        /// <img src="../../Semicirc.jpg"/>
        /// </summary>
        /// <param name="bitmap">The bitmap to set its color palette to canny 
        /// edge's chromatic circle.</param>
        /// <seealso cref="NSCannyEdgeDetector"/>
        public static void SetCannyColorPalette(Bitmap bitmap)
        {
            // get palette
            var cp = bitmap.Palette;
            // init palette
            for (int i = 0; i < 256; i++)
            {
                cp.Entries[i] = Color.Black;
            }

            // cp.Entries[1] = Color.Turquoise;
            cp.Entries[45] = Color.FromArgb(0, 255, 0);
            cp.Entries[90] = Color.Blue;
            cp.Entries[135] = Color.Red;
            cp.Entries[255] = Color.Yellow;
            // set palette back
            bitmap.Palette = cp;
        }

        /// <summary>
        /// (Re-)Sets the color palette of the <paramref name="bitmap"/> to 256 
        /// different colors, instead of standard 216 colors.
        /// </summary>
        /// <param name="bitmap">The bitmap to reset its color palette.</param>
        public static void SetColorPalette(Bitmap bitmap)
        {
            // get palette
            var cp = bitmap.Palette;
            // init palette
            int m = 0, n = 0; // 7 interval, 49 interval
            byte r = 0, g = 0, b = 0;

            for (int i = 0; i < 255; i++, m++, n++, r += 42)
            {
                if (m == 7 && n != 49)
                {
                    g += 42;
                    r = 0;
                    m = 0;
                }

                if (n == 49)
                {
                    b += 42;
                    r = g = 0;
                    m = n = 0;
                }

                cp.Entries[i] = Color.FromArgb(r, g, b);
                if ((r == 0 && g == 0 && b == 42))
                    cp.Entries[i] = Color.AliceBlue;
                //if (r == 252 && g == 0 && b == 0)
                //    Console.WriteLine("Index red: " + i);
                //if (r == 0 && g == 252 && b == 0)
                //    Console.WriteLine("Index green: " + i);
                //if (r == 0 && g == 0 && b > 150)
                //    Console.WriteLine("Index blue: " + i);
            }

            cp.Entries[255] = Color.White;

            //TODO FF: Remove it
            cp.Entries[1] = Color.Red;
            cp.Entries[2] = Color.Green;
            cp.Entries[3] = Color.Blue;

            cp.Entries[4] = Color.Yellow;
            cp.Entries[5] = Color.YellowGreen;

            cp.Entries[6] = Color.LightPink;
            cp.Entries[7] = Color.DeepPink;

            cp.Entries[8] = Color.LightCyan;
            cp.Entries[9] = Color.DarkCyan;

            cp.Entries[10] = Color.ForestGreen;
            cp.Entries[11] = Color.LightGreen;
            //

            // set palette back
            bitmap.Palette = cp;
        }
        
        /// <summary>
        /// Sets the color palette of the <paramref name="bitmap"/> to gray.
        /// </summary>
        /// <param name="bitmap">The bitmap to set its color palette to gray.
        /// </param>
        public static void SetColorPaletteToGray(Bitmap bitmap)
        {
            // get palette
            var cp = bitmap.Palette;
            // init palette
            for (int i = 0; i < 256; i++)
            {
                cp.Entries[i] = Color.FromArgb(i, i, i);
            }
            // set palette back
            bitmap.Palette = cp;    
        }
    }
}
