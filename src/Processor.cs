namespace Serotonin
{
    using System.Drawing;
    using System.Windows.Forms;

    public class Processor
    {
        public Bitmap OriginalImage { get; private set; }

        public Bitmap WorkingImage { get; set; }

        private Panel panel;

        public Processor(Bitmap originalImage, Panel panel)
        {
            OriginalImage = originalImage;
            this.panel = panel;
            this.panel.BackgroundImage = OriginalImage;
        }

        public void Reset()
        {
            panel.BackgroundImage = OriginalImage;
        }

        public void Change (Bitmap newImage)
        {
            WorkingImage = newImage;
            panel.BackgroundImage = newImage;
        }

        public Bitmap MakeGrayScale()
        {
            // TODO 1: Making grayscale
            return WorkingImage;
        }
    }
}
