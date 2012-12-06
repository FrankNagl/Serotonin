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
    /// <summary>Supported color spaces in SBIP.</summary>
    public enum ColorSpaceEnum
    {
        /// <summary>The HSB color space.</summary>
        HSB,
        /// <summary>The HSL color space.</summary>
        HSL,
        /// <summary>The CIE LAB color space.</summary>
        LAB,
        /// <summary>The CIE LCH (ab) and CIE LCH (uv) color space.</summary>
        LCH,
        /// <summary>The CIE LUV color space.</summary>
        LUV,
        /// <summary>The RGB color space.</summary>
        RGB,
        /// <summary>The sRGB color space.</summary>
        SRGB,
        /// <summary>The CIE XYZ color space.</summary>
        XYZ
    }
}