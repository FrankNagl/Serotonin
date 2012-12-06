// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
namespace Serotonin.OptionsForms
{
    using Controls;
    using Filter;
    using Helper;
    using SBIP.Filter.NonSBIP;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    /// <summary>Filter specified properties form.</summary>
    public partial class ConGrapForm : Form, IOptionsForm
    {
        public Processor Processor { get; private set; }

        private readonly ConGrap conGrap;        
        private bool doDilatation;
        /// <summary>
        /// Initializes a new instance of the <see cref="ConGrapForm"/> class.
        /// </summary>
        /// <param name="processor">The processor.</param>
        public ConGrapForm(Processor processor)
        {
            InitializeComponent();

            Processor = processor;
            conGrap = new ConGrap();

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
            conGrap.HighThreshold = (byte)trbarHighThreshold.Value;
            lbHighThreshold.Text = trbarHighThreshold.Value.ToString(
                CultureInfo.InvariantCulture);
        }

        private void TrbarLowThresholdValueChanged(object sender, EventArgs e)
        {
            conGrap.LowThreshold = (byte)trbarLowThreshold.Value;
            lbLowThreshold.Text = trbarLowThreshold.Value.ToString(
                CultureInfo.InvariantCulture);
        }

        private void TrbarGaussSizeValueChanged(object sender, EventArgs e)
        {
            conGrap.GaussianSize = trbarGaussSize.Value;
            lbGaussSize.Text = trbarGaussSize.Value.ToString(
                CultureInfo.InvariantCulture);
        }

        private void TrbarGaussSigmaValueChanged(object sender, EventArgs e)
        {
            conGrap.GaussianSigma = trbarGaussSigma.Value / 1000.0f;
            lbGaussSigma.Text = (trbarGaussSigma.Value / 1000.0f).ToString(
                CultureInfo.InvariantCulture);
        }
        #endregion Canny tab control events

        #region Contour tab control events

        private void ChboxDrawBorderRectCheckedChanged(object sender, EventArgs e)
        {
            conGrap.DrawBorderRectangle = ((CheckBox)sender).Checked;
        }

        private void ChboxDrawBorderLineCheckedChanged(object sender, EventArgs e)
        {
            conGrap.OnlyBorderLine = ((CheckBox)sender).Checked;
        }

        private void ChboxDoDilatationsCheckedChanged(object sender, EventArgs e)
        {
            doDilatation = ((CheckBox)sender).Checked;
        }

        private void TrbarRectStrengthValueChanged(object sender, EventArgs e)
        {
            conGrap.LineRectStrength = (byte)trbarStrength.Value;
            lbLineStrength.Text = trbarStrength.Value.ToString(
                CultureInfo.InvariantCulture);
        }

        private void TrbarTraceRadiusValueChanged(object sender, EventArgs e)
        {
            conGrap.TraceRadius = (byte)trbarTraceRadius.Value;
            lbTraceRadius.Text = trbarTraceRadius.Value.ToString(
                CultureInfo.InvariantCulture);
        }

        private void TrbarWeightValueChanged(object sender, EventArgs e)
        {
            conGrap.Weight = (byte)trbarWeight.Value;
            lbWeight.Text = trbarWeight.Value.ToString(
                CultureInfo.InvariantCulture);
        }

        #endregion Contour tab control events

        private void BtnProcessClick(object sender, EventArgs e)
        {

            EndlessProgressBarFormInThread form =
                new EndlessProgressBarFormInThread(
                    "Do Contour Tracing ... ",
                    "Please wait, this takes a few seconds.");
            form.Start();

            try
            {
                Bitmap b;

                // Todo FF: Hack for rectangle border and dilatation
                if (doDilatation)
                {
                    int tmp = conGrap.LineRectStrength;
                    conGrap.LineRectStrength = 1;
                    b = conGrap.Apply(Processor.OriginalImage);
                    conGrap.LineRectStrength = tmp;

                    NSSimpleDilatation dilatation = new NSSimpleDilatation();
                    for (int i = 0; i < conGrap.LineRectStrength; i++)
                    {
                        b = dilatation.Apply(b);
                    }
                }
                else
                {
                    b = conGrap.Apply(Processor.OriginalImage);
                }

                ColorPalette.SetColorPalette(b);
                Processor.Change(b);
                lbContourNumbers.Text = conGrap.Contours.Count.ToString(
                    CultureInfo.InvariantCulture);
            }
            catch (ArgumentException aex)
            {
                MessageBox.Show(aex.Message);
                BtnResetClick(sender, e);
            }

            form.End();
        }

        private void BtnResetClick(object sender, EventArgs e)
        {
            Processor.Reset();
            lbContourNumbers.Text = string.Format(@"0");
        }
    }
}
