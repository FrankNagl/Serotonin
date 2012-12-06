// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
namespace Serotonin.OptionsForms
{
    using ColorSpace;
    using Controls;
    using Filter;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>Filter specified properties form.</summary>
    public partial class SplitColorSpaceChannelsForm : Form, IOptionsForm
    {
        public Processor Processor { get; private set; }
        private readonly SplitColorSpaceChannels filter;


        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="SplitColorSpaceChannelsForm"/> class.
        /// </summary>
        /// <param name="processor">The SBIP processor.</param>
        public SplitColorSpaceChannelsForm(Processor processor)
        {
            InitializeComponent();

            Processor = processor;
            filter = new SplitColorSpaceChannels {ColorSpace = ColorSpaceEnum.HSB};
        }

        #region Tab control events

        private void ComBoxColorSpaceSelectedIndexChanged(object sender, EventArgs e)
        {
            filter.ColorSpace = (ColorSpaceEnum)((ComboBox)sender).SelectedIndex;
        }

        private void RdBtnACheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                filter.ColorTriple = ColorTripleEnum.A;
        }

        private void RdBtnBCheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                filter.ColorTriple = ColorTripleEnum.B;
        }

        private void RdBtnCCheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                filter.ColorTriple = ColorTripleEnum.C;
        }

        #endregion Tab control events

        private void BtnProcessClick(object sender, EventArgs e)
        {

            EndlessProgressBarFormInThread form =
                new EndlessProgressBarFormInThread(
                    "Do filtering ... ",
                    "Please wait, this takes a few seconds.");
            form.Start();

            try
            {
                Bitmap b = filter.Apply(Processor.OriginalImage);
                Helper.ColorPalette.SetColorPaletteToGray(b);
                Processor.Change(b);
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
        }
    }
}
