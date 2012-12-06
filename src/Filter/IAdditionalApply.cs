// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
namespace SBIP.Filter.NonSBIP
{
    using System.Drawing;

    /// <summary>
    /// Delivers additional method to 
    /// <see cref="BaseNonSBIPFilter.Apply(System.Drawing.Bitmap)"/>.
    /// </summary>
    public interface IAdditionalApply
    {
        /// <summary>
        /// Applies the filter on the passed <paramref name="source"/> bitmap.
        /// In contrast to <see cref="BaseNonSBIPFilter.Apply(System.Drawing.Bitmap)"/>,
        /// individual pre and post procssing routines can be added.
        /// </summary>
        /// <param name="source">The source image to process.</param>
        /// <returns>The filter result as a new bitmap.</returns>
        Bitmap AdditionalApply(Bitmap source);
    }
}
