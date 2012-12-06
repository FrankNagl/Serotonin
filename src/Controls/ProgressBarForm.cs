// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
namespace Serotonin.Controls
{
    using System.Windows.Forms;

    /// <summary>A form with a simple using progress bar.</summary>
    /// 
    /// <remarks>
    /// 
    /// <para>Sample usage:</para>
    /// <code>
    /// ProgressBarForm form = 
    ///     new ProgressBarForm
    ///     {
    ///         ProgressBar = { Maximum = 100 },
    ///         StartPosition = FormStartPosition.CenterScreen
    ///     };
    /// form.Show();
    /// form.TopMost = true;
    /// 
    /// for (int i = 0; i != 100; i++)
    /// {
    ///     // do something ...
    ///     form.Text = "Number " +  i;                    
    ///     form.Resultlabel.Text = "We are at number " +  i;
    ///     form.ProgressBar.PerformStep();
    ///     form.Refresh();
    /// }
    /// 
    /// form.Dispose();
    /// </code>
    /// 
    /// </remarks>
    public partial class ProgressBarForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBarForm"/> class.
        /// </summary>
        public ProgressBarForm()
        {
            InitializeComponent();
        }
    }
}
