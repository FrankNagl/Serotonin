// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2010
// admin@franknagl.de
//
namespace Serotonin.OptionsForms
{
    using System.Windows.Forms;

    /// <summary>Filter specified properties form.</summary>
    public partial class OriginalForm : Form, IOptionsForm
    {
        public Processor Processor { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OriginalForm"/> class.
        /// </summary>
        /// <param name="processor">The processor.</param>
        public OriginalForm(Processor processor)
        {
            InitializeComponent();
            Processor = processor;
        }
    }
}
