// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
namespace Serotonin.Controls
{
    using System.Threading;
    using System.Windows.Forms;

    /// <summary>A form with an animated endless-running progress bar.</summary>
    /// 
    /// <remarks> 
    /// <para><b>Example:</b></para>
    /// <img src="../../EndlessProgressBarForm.jpg"/>
    /// </remarks>
    /// <seealso cref="EndlessProgressBarFormInThread"/>
    public partial class EndlessProgressBarForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndlessProgressBarForm"/> class.
        /// </summary>
        public EndlessProgressBarForm()
        {
            InitializeComponent();
        }
    }

    /// <summary>
    /// Provides the <see cref="EndlessProgressBarForm"/> in a separate thread.
    /// </summary>
    /// 
    /// <remarks> 
    /// <para>Sample usage:</para>
    /// <code>
    /// EndlessProgressBarFormInThread form = 
    ///     new EndlessProgressBarFormInThread("Do Contour Tracing ... ", 
    ///         "Please wait, this takes a view seconds.");
    /// form.Start();
    /// // do something here
    /// // ...
    /// form.End();
    /// </code>
    /// 
    /// </remarks>
    /// <seealso cref="ProgressBarForm"/>
    public class EndlessProgressBarFormInThread
    {
        private volatile bool isStarted;
        private Thread thread;
        private readonly string title;
        private readonly string info;

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="EndlessProgressBarFormInThread"/> class.
        /// </summary>
        /// <param name="title">Text of <see cref="EndlessProgressBarForm"/>'s 
        /// title bar.</param>
        /// <param name="info">Text of <see cref="EndlessProgressBarForm.lbInfo"/>.
        /// </param>
        public EndlessProgressBarFormInThread(string title, string info)
        {
            this.title = title;
            this.info = info;
        }

        /// <summary>
        /// Ends the started thread and closes the opened 
        /// <see cref="EndlessProgressBarForm"/> of <see cref="Start()"/>.
        /// </summary>
        public void End()
        {
            isStarted = false;
            thread.Join();
        }

        /// <summary>
        /// Starts a thread for displaying <see cref="EndlessProgressBarForm"/>.
        /// </summary>
        public void Start()
        {
            isStarted = true;
            thread = new Thread(ShowForm);
            thread.Start();
        }

        private void ShowForm()
        {
            EndlessProgressBarForm form = new EndlessProgressBarForm
                {
                    StartPosition = FormStartPosition.CenterScreen,
                    Text = title,
                    lbInfo = {Text = info}
                };
            form.Show();
            form.TopMost = true;
            while (isStarted)
            {
                form.Refresh();
            }
            form.Dispose();
        }
    }
}
