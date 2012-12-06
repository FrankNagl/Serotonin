// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//

using Serotonin.Helper;

namespace Serotonin.OptionsForms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using SBIP.Filter.NonSBIP;

    /// <summary>Filter specified properties form.</summary>
    public partial class NSColoredCannyForm : Form, IOptionsForm
    {
        public Processor Processor { get; private set; }

        private readonly NSCannyEdgeDetector canny;
        private bool drawInImage;        
        private readonly NSSimpleDilatation dilatation;

        /// <summary>
        /// Initializes a new instance of the <see cref="NSColoredCannyForm"/> class.
        /// </summary>
        /// <param name="processor">The SBIP processor.</param>
        public NSColoredCannyForm(Processor processor)
        {
            InitializeComponent();

            Processor = processor;
            canny = new NSCannyEdgeDetector();
            dilatation = new NSSimpleDilatation();

            // TODO FF: Delete this region
            #region Pre-Configure
            //trbarWeight.Value = 20;
            //trbarMinContourSize.Value = 300;
            //trbarTraceRadius.Value = 10;
            //chboxDrawBorderLine.Checked = true;

            //trbarHighThreshold.Value = 20;
            //trbarLowThreshold.Value = 10;
            //trbarGaussSize.Value = 5;
            //trbarGaussSigma.Value = (int)(1.631 * 1000 + 0.1);
            #endregion Pre-Configure
        }

        #region Canny tab control events

        private void TrbarHighThresholdValueChanged(object sender, EventArgs e)
        {
            canny.HighThreshold = (byte)trbarHighThreshold.Value;
            lbHighThreshold.Text = trbarHighThreshold.Value.ToString();
        }

        private void TrbarLowThresholdValueChanged(object sender, EventArgs e)
        {
            canny.LowThreshold = (byte)trbarLowThreshold.Value;
            lbLowThreshold.Text = trbarLowThreshold.Value.ToString();
        }

        private void TrbarGaussSizeValueChanged(object sender, EventArgs e)
        {
            canny.GaussianSize = trbarGaussSize.Value;
            lbGaussSize.Text = trbarGaussSize.Value.ToString();
        }

        private void TrbarGaussSigmaValueChanged(object sender, EventArgs e)
        {
            canny.GaussianSigma = trbarGaussSigma.Value / 1000.0f;
            lbGaussSigma.Text = (trbarGaussSigma.Value / 1000.0f).ToString();
        }
        #endregion Canny tab control events

        #region Edge Drawing tab control events
        private void RbOrientColoredCheckedChanged(object sender, EventArgs e)
        {
            pboxSemiCircle.Visible = ((RadioButton)sender).Checked;
            if (rbOrientColored.Checked)
            {
                rdBtnNoDrawInImage.Checked = true;
            }
        }

        private void RdBtnYesDrawInImageCheckedChanged(object sender, EventArgs e)
        {
            drawInImage = rdBtnYesDrawInImage.Checked;
            if (rdBtnYesDrawInImage.Checked)
            {
                rbNormal.Checked = true;
            }
        }

        private void TrBarDiameterValueChanged(object sender, EventArgs e)
        {
            dilatation.Diameter = (byte)(trBarDiameter.Value);// * 2 + 1);
            lbDiameter.Text = dilatation.Diameter.ToString();
        }

        #endregion Edge Drawing tab control events

        private void BtnProcessClick(object sender, EventArgs e)
        {
            try
            {
                Bitmap b;
                if (drawInImage)
                {
                    NSCannyEdgeMarker marker = new NSCannyEdgeMarker();
                    marker.CannyEdgeDetector = canny;
                    marker.SimpleDilatation = dilatation;
                    b = marker.Apply(Processor.OriginalImage);
                }
                else
                {
                    if (rbOrientColored.Checked)
                    {
                        b = canny.AdditionalApply(Processor.OriginalImage);
                        if (dilatation.Diameter > 1)
                        {
                            b = dilatation.Apply(b);
                            ColorPalette.SetCannyColorPalette(b);
                        }
                    }
                    else
                    {
                        b = canny.Apply(Processor.OriginalImage);
                        if (dilatation.Diameter > 1)
                        {
                            b = dilatation.Apply(b);
                        }
                    }

                }
                Processor.Change(b);
            }
            catch (ArgumentException aex)
            {
                MessageBox.Show(aex.Message);
                BtnResetClick(sender, e);
            }
        }

        private void BtnResetClick(object sender, EventArgs e)
        {
            Processor.Reset();
        }
    }
}
