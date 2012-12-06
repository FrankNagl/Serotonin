// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
namespace SBIP
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Forms;
    using Serotonin;
    using Serotonin.OptionsForms;

    /// <summary>
    /// Main form of the application ShaderBasedImageProcessor.
    /// </summary>
    public partial class ProcessorForm : Form
    {
        // determines, if old version is updateable
        private bool isUpdateable;
        private string optionalStartFileName;
        private GroupBox options;
        /// <summary>
        /// Factor for multiplying cursor's X-coordinate at <see cref="panel"/> 
        /// to get correct image pixel coordinate.
        /// </summary>
        private float scaleCursorX;
        /// <summary>
        /// Factor for multiplying cursor's Y-coordinate at <see cref="panel"/> 
        /// to get correct image pixel coordinate.
        /// </summary>
        private float scaleCursorY;
        private Processor processor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessorForm"/> class.
        /// </summary>
        /// <param name="useUpdater">If set to <c>true</c> the autoupdater is 
        /// used.</param>
        protected ProcessorForm(bool useUpdater)
        {
            InitializeComponent();
            options = new GroupBox();
            if (!useUpdater)
            {
                return;
            }

            AutoUpdater updater = new AutoUpdater(
                Program.OptionsFile,
                "http://franknagl.de/updates/SBIP/update.csv",
                Program.ProgramPath,
                Program.ProgramExe);
            updater.OnCheckUpdateEvent += UpdateAvailable;
            updater.CheckUpdateAsThread();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessorForm"/> class.
        /// </summary>
        public ProcessorForm()
            : this(true)
        {
            Initialize(null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessorForm"/> class.
        /// </summary>
        /// <param name="filename">The passed image or video filename.</param>
        public ProcessorForm(string filename)
            : this(true)
        {
            Initialize(filename);
        }

        /// <summary>Routine for updating each frame.</summary>
        /// <param name="firstCall">If first call it has to be set to 
        /// <c>true</c>.</param>
        public void UpdateEachFrame(bool firstCall)
        {
            //if (Processor.IsVideo)
            //{
            //    videoForm.UpdateEachFrame(firstCall);
            //}
        }

        #region private methods

        private void InfoToolStripMenuItemClick(object sender, EventArgs e)
        {
            new InfoForm().Show(this);
        }

        private void Initialize(string filename)
        {
            if (filename == null)
            {
                System.Resources.ResourceManager resource =
                    new System.Resources.ResourceManager("SBIP.Resource",
                    System.Reflection.Assembly.GetExecutingAssembly());
                //processor = new Processor(
                //    new Bitmap((Bitmap)resource.GetObject("dom_erfurt")), panel);
                processor = new Processor(
                    new Bitmap(1024, 600, PixelFormat.Format24bppRgb), panel);

                options.Height = 100; // Hack, for SetPanelSize(..) method
                SetPanelSize(processor.OriginalImage.Width,
                    processor.OriginalImage.Height);

                InitOptions(new OriginalForm(processor).Options);                
            }
            else
            {
                Text = filename;
                optionalStartFileName = filename;
                FileInfo info = new FileInfo(filename);
                string ext = info.Extension.ToLower();

                switch (ext)
                {
                    case ".wmf":
                    case ".tiff":
                    case ".tif":
                    case ".gif":
                    case ".emf":
                    case ".png":
                    case ".bmp":
                    case ".jpeg":
                    case ".jpg":
                        LoadImage(filename);
                        break;
                    default:
                        if (MessageBox.Show(
                            "This file type is not supported by SBIP.\n" +
                            @"Open SBIP anyway?",
                            @"Wrong file type",
                            MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Initialize(null);
                        }
                        else
                        {
                            Dispose();
                            return;
                        }
                        break;
                }
            }

        }

        private void InitOptions(GroupBox groupBox)
        {
            Point loc = options.Location;
            Controls.Remove(options);
            options = groupBox;
            options.Location = loc;
            Controls.Add(options);

            // setting postion of informations group box
            infos.Location = new Point(
                options.Location.X, options.Location.Y + options.Height + 10);
        }

        private void LoadImage(string imageFilename)
        {
            optionalStartFileName = imageFilename;
            processor = new Processor(
                    new Bitmap(imageFilename), panel);
            SetPanelSize(processor.OriginalImage.Width,
                processor.OriginalImage.Height);
            InitOptions(new OriginalForm(processor).Options); 
        }

        private void SetPanelSize(int w, int h)
        {
            Location = new Point(0, 0);
            Size maxSize = new Size(
                    SystemInformation.PrimaryMonitorSize.Width,
                    SystemInformation.PrimaryMonitorSize.Height - 45);
            int moniW = SystemInformation.PrimaryMonitorSize.Width - 300;
            int moniH = SystemInformation.PrimaryMonitorSize.Height - 240;
            int addWidthForSlider = 0; // slider needs width of 450

            if (moniW < w || moniH < h)
            {
                bool wLonger =
                    (w / (float)h) > (moniW / (float)moniH) ? true : false;
                if (wLonger)
                {
                    moniH = h * moniW / w;
                    maxSize.Height = moniH + 170 + panel.Location.Y;
                }
                else
                {
                    moniW = w * moniH / h;
                    maxSize.Width = moniW + 300 + panel.Location.X;
                }
                panel.Size = new Size(moniW, moniH);
                Size = maxSize;
            }
            else
            {
                panel.Size = new Size(w, h);
                Size = new Size(
                    w + 300 + panel.Location.X,
                    h + 170 + panel.Location.Y);
            }

            // check, if windows is high enough, so complete options are visible
            if (panel.Size.Height < options.Height + 50 + panel.Location.Y)
                Height = options.Height + 200 + panel.Location.Y;

            // check, if windows is width enough, so complete video options are visible
            if (panel.Size.Width < 450)
            {
                addWidthForSlider = 450 - panel.Size.Width;
                Width = Width + addWidthForSlider;//options.Height + 300 + panel.Location.X;
            }

            options.Location = new Point(//832, 27);
                panel.Size.Width + addWidthForSlider + 30,
                30);

            //videoOptions.Location = new Point(
            //    panel.Location.X, panel.Location.Y + panel.Size.Height + 20);

            //videoOptions.Width = panel.Width + addWidthForSlider;

            // setting image informations
            scaleCursorX = w / (float)panel.Size.Width;
            scaleCursorY = h / (float)panel.Size.Height;
            lbWidth.Text = w.ToString();
            lbHeight.Text = h.ToString();
        }

        private void UpdateAvailable(bool updateable)
        {
            UpdateToolStripMenuItem.Visible = true;
            isUpdateable = updateable;
        }

        #region Open-Save-Close-Update GUI events

        private void SaveAsToolStripMenuItemClick(object sender, EventArgs e)
        {
            //saveFileDialog.InitialDirectory = "c:\\";
            saveFileDialog.Filter =
                @"PNG (*.png)|*.png|JPEG Files(*.jpg;*.jpeg)|*.jpg;*.jpeg|BMP (*.bmp)|*.bmp|" +
                @"TIFF Files(*.tif;*.tiff)|*.tif;*.tiff|GIF (*.gif)|*.gif|EMF (*.emf)|*.emf|" +
                @"WMF (*.wmf)|*.wmf|" +
                @"8 Bit Grayscale PNG Files(*.png)|*.png|" +
                @"8 Bit Grayscale BMP Files(*.bmp)|*.bmp";
            //@"8 Bit Indexed Color PNG Files(*.png)|*.png|" +
            //@"8 Bit Indexed Color BMP Files(*.bmp)|*.bmp|" +
            //@"8 Bit Colored Canny Edges PNG Files(*.png)|*.png|" +
            //@"8 Bit Colored Canny Edges BMP Files(*.bmp)|*.bmp";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            // check, if user wants to save as 8 bit image or not
            Bitmap image = saveFileDialog.FilterIndex > 7 ?
                           processor.MakeGrayScale() :
                           processor.WorkingImage;

            switch (saveFileDialog.FilterIndex)
            {
                case 1: image.Save(saveFileDialog.FileName, ImageFormat.Png); break;
                case 2: JpegEncoder.SaveJpeg(saveFileDialog.FileName, image, 100);//100% quality
                    break;
                case 3: image.Save(saveFileDialog.FileName, ImageFormat.Bmp); break;
                case 4: image.Save(saveFileDialog.FileName, ImageFormat.Tiff); break;
                case 5: image.Save(saveFileDialog.FileName, ImageFormat.Gif); break;
                case 6: image.Save(saveFileDialog.FileName, ImageFormat.Emf); break;
                case 7: image.Save(saveFileDialog.FileName, ImageFormat.Wmf); break;

                // 8 bit grayscale images
                case 8: image.Save(saveFileDialog.FileName, ImageFormat.Png); break;
                case 9: image.Save(saveFileDialog.FileName, ImageFormat.Bmp); break;

                //// 8 bit indexed color images
                //case 10:
                //    Helper.ImageConverter.SetColorPalette(image);  
                //    image.Save(saveFileDialog.FileName, ImageFormat.Png); break;
                //case 11:
                //    Helper.ImageConverter.SetColorPalette(image);
                //    image.Save(saveFileDialog.FileName, ImageFormat.Bmp); break;

                //// 8 bit colored canny edges images
                //case 12:
                //    Helper.ImageConverter.SetCannyColorPalette(image);  
                //    image.Save(saveFileDialog.FileName, ImageFormat.Png); break;
                //case 13:
                //    Helper.ImageConverter.SetCannyColorPalette(image);  
                //    image.Save(saveFileDialog.FileName, ImageFormat.Bmp); break;
            }
        }

        private void TsItmOpenImageClick(object sender, EventArgs e)
        {
            openFileDialog.Filter =
                @"Image Files(*.jpg;*.jpeg;*.bmp;*.png;*.emf;*.gif;*.tif;*.tiff;" +
                @"*.wmf)|*.jpg;*.jpeg;*.bmp;*.png;*.emf;*.gif;*.tif;*.tiff;*.wmf";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadImage(openFileDialog.FileName);
            }
        }

        private void TsItmOpenVideoClick(object sender, EventArgs e)
        {
            openFileDialog.Filter =
                @"Video Files(*.avi;*.wmv)|*.avi;*.wmv";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // LoadVideo(openFileDialog.FileName);
            }
        }

        private void UpdateToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (isUpdateable)
            {
                if (MessageBox.Show(
                    @"New version available. Do you want to update (recommended)?",
                    @"New version available :-)",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    VistaSecurity.RestartElevatedForUpdate();
                }
            }
            else
            {
                if (MessageBox.Show(
                    @"New version available. Do you want to download it from SBIP website (recommended)?",
                    @"New version available :-)",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("http://code.google.com/p/sbip/");
                }
            }
        }

        private void QuitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

        #endregion Open-Save-Close-Update GUI events

        #region Filter ToolStrip MenuItem Events
        private void NoFilterToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new OriginalForm(processor).Options);
        }

        private void CannyToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new CannyForm(processor).Options);
        }

        private void Convolution3X3ToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new Convolution3x3Form(processor).Options);
        }

        private void Convolution5X5ToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new Convolution5x5Form(processor).Options);
        }

        private void ChessboardToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new ChessboardForm(processor).Options);
        }

        private void ExtractChannelToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new ExtractChannelForm(processor).Options);
        }

        private void InvertToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new InvertForm(processor).Options);
        }

        private void GaussianBlurToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new GaussianBlurForm(processor).Options);
        }

        private void GrayscaleToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new GrayscaleForm(processor).Options);
        }

        private void LaplaceToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new LaplaceForm(processor).Options);
        }

        private void LightingToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new LightingForm(processor).Options);
        }

        private void PosterizationToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new PosterizationForm(processor).Options);
        }

        private void RotateChannelToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new RotateChannelsForm(processor).Options);
        }

        private void SepiaToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new SepiaForm(processor).Options);
        }

        private void SobelToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new SobelForm(processor).Options);
        }

        private void SquareChannelToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new SquareChannelForm(processor).Options);
        }

        private void ThresholdToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new ThresholdForm(processor).Options);
        }

        private void ThresholdRgbToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new ThresholdRGBForm(processor).Options);
        }

        private void VoronoiDiagramToolStripMenuItemClick(object sender, EventArgs e)
        {
            //InitOptions(new VoronoiDiagramForm(
            //    processor, 
            //    panel, 
            //    scaleCursorX, 
            //    scaleCursorY).Options);
        }

        private void WhiteContentToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new WhiteContentForm(processor).Options);
        }


        private void MeanShiftToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new MeanShiftForm(processor).Options);
        }
        #endregion Filter ToolStrip MenuItem Events


        #region NonSBIP Filters ToolStrip MenuItem Events

        private void ColoredCannyEdgeDetectorToolStripMenuItemClick(object sender, EventArgs e)
        {
            InitOptions(new NSColoredCannyForm(processor).Options);
        }

        private void ColorEliminatedRegionGrowingToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new NSColorEliminatedRegionGrowingForm(processor).Options);
        }

        private void ConGrapLightToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new NSConGrapLightForm(processor).Options);
        }

        private void ConTrapContourTracerToolStripMenuItemClick(object sender, EventArgs e)
        {
            InitOptions(new ConGrapForm(processor).Options);
        }

        private void KeyPointBasedFloodFillToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new NSKeyPointAndColorBasedSegmentationForm(processor).Options);
        }

        private void LuvColorbasedSegmentationToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new NSLuvColorSegmentationForm(processor).Options);
        }

        private void SimpleColorSegmentationToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new NSSimpleColorSegmentationForm(processor).Options);
        }

        private void SimpleDilatationToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new NSSimpleDilatationForm(processor).Options);
        }

        private void SimpleLuvColorbasedSegmentationToolStripMenuItemClick(object sender, EventArgs e)
        {
            // InitOptions(new NSSimpleLuvColorSegmentationForm(processor).Options);
        }

        private void SplitColorSpaceChannelsToolStripMenuItemClick(object sender, EventArgs e)
        {
            InitOptions(new SplitColorSpaceChannelsForm(processor).Options);
        }

        #endregion NonSBIP Filters ToolStrip MenuItem Events

        #region Drag and drop and mouse events
        private void PanelMouseMove(object sender, MouseEventArgs e)
        {
            int mouseX = panel.PointToClient(Cursor.Position).X;
            int mouseY = panel.PointToClient(Cursor.Position).Y;
            lbX.Text = (Math.Round(mouseX * scaleCursorX + 0.5f)).ToString();
            lbY.Text = (Math.Round(mouseY * scaleCursorY + 0.5f)).ToString();
        }

        private void ProcessorFormDragDrop(object sender, DragEventArgs dea)
        {
            if (dea.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])dea.Data.GetData(DataFormats.FileDrop);
                Initialize(files[0]); // only first element of drag objects
            }
        }

        private void ProcessorFormDragOver(object sender, DragEventArgs dea)
        {
            if (dea.Data.GetDataPresent(DataFormats.FileDrop))
                dea.Effect = DragDropEffects.Move;
        }
        #endregion Drag and drop and mouse events

        #endregion private methods
    }
}
