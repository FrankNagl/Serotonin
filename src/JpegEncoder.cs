// Nail.JpegEncoder
// Nail framework, revision 60
// http://code.google.com/p/nail/
//
// Copyright © Frank Nagl, 2008
// admin@franknagl.de
// www.franknagl.de
//
//
// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2010
// admin@franknagl.de
//
namespace SBIP
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    /// <summary>
    /// Static functions for saving a jpeg image with specified quality.
    /// </summary>
    public static class JpegEncoder
    {
        /// <summary>
        /// Saves an image as a jpeg image, with the given quality
        /// </summary>
        /// <param name="path">Path to which the image would be saved.</param>
        /// <param name="img">The image</param>
        /// <param name="quality">An integer from 0 to 100, with 100 being the
        /// highest quality</param>
        public static void SaveJpeg (string path, Image img, int quality)
        {
            if (quality<0  ||  quality>100)
                throw new ArgumentOutOfRangeException("quality", "quality must be between 0 and 100.");
            
            // Encoder parameter for image quality
            EncoderParameter qualityParam =
                new EncoderParameter (Encoder.Quality, quality);
            // Jpeg image codec
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save (path, jpegCodec, encoderParams);
        }

        /// <summary>
        /// Returns the image codec with the given mime type
        /// </summary>
        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            foreach (ImageCodecInfo t in codecs)
                if(t.MimeType == mimeType)
                    return t;
            return null;
        } 
    }
}
