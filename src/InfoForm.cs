// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2010
// admin@franknagl.de
//
namespace SBIP
{
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// Informations about SBIP in a separate form.
    /// </summary>
    public partial class InfoForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InfoForm"/> class.
        /// </summary>
        public InfoForm()
        {
            InitializeComponent();

            StreamReader stream =
                new StreamReader(Program.OptionsFile, System.Text.Encoding.Default);
            lbVersion.Text = stream.ReadLine();
            // ReSharper disable PossibleNullReferenceException
            lbVersion.Text = lbVersion.Text.Substring(lbVersion.Text.IndexOf(';') + 1);
            // ReSharper restore PossibleNullReferenceException
            lbRelDate.Text = stream.ReadLine();
            // ReSharper disable PossibleNullReferenceException
            lbRelDate.Text = lbRelDate.Text.Substring(lbRelDate.Text.IndexOf(';') + 1);
            // ReSharper restore PossibleNullReferenceException
            stream.Close();
        }

        private void BtnCloseClick(object sender, System.EventArgs e)
        {
            Close();
        }

        private void LinkLabel2LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://code.google.com/p/sbip/");
        }

        private void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.franknagl.de/04_en_veroeffentlichungen.php?id=1291304080");
        }

        private void LinkLabel3LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.franknagl.de");
        }
    }
}
