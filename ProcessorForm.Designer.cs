// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
namespace SBIP
{
    partial class ProcessorForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessorForm));
            this.panel = new System.Windows.Forms.Panel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsItmOpenImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsItmOpenVideo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.SaveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.QuitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NoFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CannyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Convolution3x3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Convolution5x5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChessboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExtractChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gaussianBlurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GrayscaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InvertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LaplaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LightingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.posterizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RotateChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sepiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SobelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SquareChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ThresholdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ThresholdRGBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.voronoiDiagramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whiteContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meanShiftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NonSBIPFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coloredCannyEdgeDetectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorEliminatedRegionGrowingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KeyPointBasedFloodFillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.floodFillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleDilatationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitColorSpaceChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conTrapContourTracerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conGrapLightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SimpleLuvColorbasedSegmentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lUVColorbasedSegmentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.infos = new System.Windows.Forms.GroupBox();
            this.lbHeight = new System.Windows.Forms.Label();
            this.lbWidth = new System.Windows.Forms.Label();
            this.lbY = new System.Windows.Forms.Label();
            this.lbX = new System.Windows.Forms.Label();
            this.lbHeightText = new System.Windows.Forms.Label();
            this.lbWidthText = new System.Windows.Forms.Label();
            this.lbYText = new System.Windows.Forms.Label();
            this.lbXText = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.infos.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel.Location = new System.Drawing.Point(13, 37);
            this.panel.Margin = new System.Windows.Forms.Padding(4);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(487, 302);
            this.panel.TabIndex = 0;
            this.panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelMouseMove);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.AliceBlue;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.FilterToolStripMenuItem,
            this.NonSBIPFilterToolStripMenuItem,
            this.InfoToolStripMenuItem,
            this.UpdateToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1459, 28);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsItmOpenImage,
            this.tsItmOpenVideo,
            this.tsSeparator,
            this.SaveAsToolStripMenuItem,
            this.tsSeparator2,
            this.QuitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // tsItmOpenImage
            // 
            this.tsItmOpenImage.Name = "tsItmOpenImage";
            this.tsItmOpenImage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsItmOpenImage.Size = new System.Drawing.Size(315, 24);
            this.tsItmOpenImage.Text = "Open image";
            this.tsItmOpenImage.Click += new System.EventHandler(this.TsItmOpenImageClick);
            // 
            // tsItmOpenVideo
            // 
            this.tsItmOpenVideo.Name = "tsItmOpenVideo";
            this.tsItmOpenVideo.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.tsItmOpenVideo.Size = new System.Drawing.Size(315, 24);
            this.tsItmOpenVideo.Text = "Open video";
            this.tsItmOpenVideo.Click += new System.EventHandler(this.TsItmOpenVideoClick);
            // 
            // tsSeparator
            // 
            this.tsSeparator.Name = "tsSeparator";
            this.tsSeparator.Size = new System.Drawing.Size(312, 6);
            // 
            // SaveAsToolStripMenuItem
            // 
            this.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem";
            this.SaveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveAsToolStripMenuItem.Size = new System.Drawing.Size(315, 24);
            this.SaveAsToolStripMenuItem.Text = "Save image";
            this.SaveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItemClick);
            // 
            // tsSeparator2
            // 
            this.tsSeparator2.Name = "tsSeparator2";
            this.tsSeparator2.Size = new System.Drawing.Size(312, 6);
            // 
            // QuitToolStripMenuItem
            // 
            this.QuitToolStripMenuItem.Name = "QuitToolStripMenuItem";
            this.QuitToolStripMenuItem.Size = new System.Drawing.Size(315, 24);
            this.QuitToolStripMenuItem.Text = "Quit";
            this.QuitToolStripMenuItem.Click += new System.EventHandler(this.QuitToolStripMenuItemClick);
            // 
            // FilterToolStripMenuItem
            // 
            this.FilterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NoFilterToolStripMenuItem,
            this.toolStripSeparator1,
            this.CannyToolStripMenuItem,
            this.Convolution3x3ToolStripMenuItem,
            this.Convolution5x5ToolStripMenuItem,
            this.ChessboardToolStripMenuItem,
            this.ExtractChannelToolStripMenuItem,
            this.gaussianBlurToolStripMenuItem,
            this.GrayscaleToolStripMenuItem,
            this.InvertToolStripMenuItem,
            this.LaplaceToolStripMenuItem,
            this.LightingToolStripMenuItem,
            this.posterizationToolStripMenuItem,
            this.RotateChannelToolStripMenuItem,
            this.sepiaToolStripMenuItem,
            this.SobelToolStripMenuItem,
            this.SquareChannelToolStripMenuItem,
            this.ThresholdToolStripMenuItem,
            this.ThresholdRGBToolStripMenuItem,
            this.voronoiDiagramToolStripMenuItem,
            this.whiteContentToolStripMenuItem,
            this.meanShiftToolStripMenuItem});
            this.FilterToolStripMenuItem.Name = "FilterToolStripMenuItem";
            this.FilterToolStripMenuItem.Size = new System.Drawing.Size(106, 24);
            this.FilterToolStripMenuItem.Text = "Image Filters";
            // 
            // NoFilterToolStripMenuItem
            // 
            this.NoFilterToolStripMenuItem.Name = "NoFilterToolStripMenuItem";
            this.NoFilterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.N)));
            this.NoFilterToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.NoFilterToolStripMenuItem.Text = "No Filter";
            this.NoFilterToolStripMenuItem.Click += new System.EventHandler(this.NoFilterToolStripMenuItemClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(255, 6);
            // 
            // CannyToolStripMenuItem
            // 
            this.CannyToolStripMenuItem.Name = "CannyToolStripMenuItem";
            this.CannyToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.CannyToolStripMenuItem.Text = "Canny Edge Detector";
            this.CannyToolStripMenuItem.Click += new System.EventHandler(this.CannyToolStripMenuItemClick);
            // 
            // Convolution3x3ToolStripMenuItem
            // 
            this.Convolution3x3ToolStripMenuItem.Name = "Convolution3x3ToolStripMenuItem";
            this.Convolution3x3ToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.Convolution3x3ToolStripMenuItem.Text = "Convolution 3x3";
            this.Convolution3x3ToolStripMenuItem.Click += new System.EventHandler(this.Convolution3X3ToolStripMenuItemClick);
            // 
            // Convolution5x5ToolStripMenuItem
            // 
            this.Convolution5x5ToolStripMenuItem.Name = "Convolution5x5ToolStripMenuItem";
            this.Convolution5x5ToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.Convolution5x5ToolStripMenuItem.Text = "Convolution 5x5";
            this.Convolution5x5ToolStripMenuItem.Click += new System.EventHandler(this.Convolution5X5ToolStripMenuItemClick);
            // 
            // ChessboardToolStripMenuItem
            // 
            this.ChessboardToolStripMenuItem.Name = "ChessboardToolStripMenuItem";
            this.ChessboardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.ChessboardToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.ChessboardToolStripMenuItem.Text = "Chessboard :-)";
            this.ChessboardToolStripMenuItem.Click += new System.EventHandler(this.ChessboardToolStripMenuItemClick);
            // 
            // ExtractChannelToolStripMenuItem
            // 
            this.ExtractChannelToolStripMenuItem.Name = "ExtractChannelToolStripMenuItem";
            this.ExtractChannelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
            this.ExtractChannelToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.ExtractChannelToolStripMenuItem.Text = "Extract Channel";
            this.ExtractChannelToolStripMenuItem.Click += new System.EventHandler(this.ExtractChannelToolStripMenuItemClick);
            // 
            // gaussianBlurToolStripMenuItem
            // 
            this.gaussianBlurToolStripMenuItem.Name = "gaussianBlurToolStripMenuItem";
            this.gaussianBlurToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.B)));
            this.gaussianBlurToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.gaussianBlurToolStripMenuItem.Text = "Gaussian Blur";
            this.gaussianBlurToolStripMenuItem.Click += new System.EventHandler(this.GaussianBlurToolStripMenuItemClick);
            // 
            // GrayscaleToolStripMenuItem
            // 
            this.GrayscaleToolStripMenuItem.Name = "GrayscaleToolStripMenuItem";
            this.GrayscaleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.G)));
            this.GrayscaleToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.GrayscaleToolStripMenuItem.Text = "Grayscale";
            this.GrayscaleToolStripMenuItem.Click += new System.EventHandler(this.GrayscaleToolStripMenuItemClick);
            // 
            // InvertToolStripMenuItem
            // 
            this.InvertToolStripMenuItem.Name = "InvertToolStripMenuItem";
            this.InvertToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.I)));
            this.InvertToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.InvertToolStripMenuItem.Text = "Invert";
            this.InvertToolStripMenuItem.Click += new System.EventHandler(this.InvertToolStripMenuItemClick);
            // 
            // LaplaceToolStripMenuItem
            // 
            this.LaplaceToolStripMenuItem.Name = "LaplaceToolStripMenuItem";
            this.LaplaceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.L)));
            this.LaplaceToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.LaplaceToolStripMenuItem.Text = "Laplace";
            this.LaplaceToolStripMenuItem.Click += new System.EventHandler(this.LaplaceToolStripMenuItemClick);
            // 
            // LightingToolStripMenuItem
            // 
            this.LightingToolStripMenuItem.Name = "LightingToolStripMenuItem";
            this.LightingToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.LightingToolStripMenuItem.Text = "Lighting";
            this.LightingToolStripMenuItem.Click += new System.EventHandler(this.LightingToolStripMenuItemClick);
            // 
            // posterizationToolStripMenuItem
            // 
            this.posterizationToolStripMenuItem.Name = "posterizationToolStripMenuItem";
            this.posterizationToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.posterizationToolStripMenuItem.Text = "Posterization";
            this.posterizationToolStripMenuItem.Click += new System.EventHandler(this.PosterizationToolStripMenuItemClick);
            // 
            // RotateChannelToolStripMenuItem
            // 
            this.RotateChannelToolStripMenuItem.Name = "RotateChannelToolStripMenuItem";
            this.RotateChannelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
            this.RotateChannelToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.RotateChannelToolStripMenuItem.Text = "RotateChannel";
            this.RotateChannelToolStripMenuItem.Click += new System.EventHandler(this.RotateChannelToolStripMenuItemClick);
            // 
            // sepiaToolStripMenuItem
            // 
            this.sepiaToolStripMenuItem.Name = "sepiaToolStripMenuItem";
            this.sepiaToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.sepiaToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.sepiaToolStripMenuItem.Text = "Sepia";
            this.sepiaToolStripMenuItem.Click += new System.EventHandler(this.SepiaToolStripMenuItemClick);
            // 
            // SobelToolStripMenuItem
            // 
            this.SobelToolStripMenuItem.Name = "SobelToolStripMenuItem";
            this.SobelToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.SobelToolStripMenuItem.Text = "Sobel Edge Detector";
            this.SobelToolStripMenuItem.Click += new System.EventHandler(this.SobelToolStripMenuItemClick);
            // 
            // SquareChannelToolStripMenuItem
            // 
            this.SquareChannelToolStripMenuItem.Name = "SquareChannelToolStripMenuItem";
            this.SquareChannelToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.SquareChannelToolStripMenuItem.Text = "Square Channel";
            this.SquareChannelToolStripMenuItem.Click += new System.EventHandler(this.SquareChannelToolStripMenuItemClick);
            // 
            // ThresholdToolStripMenuItem
            // 
            this.ThresholdToolStripMenuItem.Name = "ThresholdToolStripMenuItem";
            this.ThresholdToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.T)));
            this.ThresholdToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.ThresholdToolStripMenuItem.Text = "Threshold";
            this.ThresholdToolStripMenuItem.Click += new System.EventHandler(this.ThresholdToolStripMenuItemClick);
            // 
            // ThresholdRGBToolStripMenuItem
            // 
            this.ThresholdRGBToolStripMenuItem.Name = "ThresholdRGBToolStripMenuItem";
            this.ThresholdRGBToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.T)));
            this.ThresholdRGBToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.ThresholdRGBToolStripMenuItem.Text = "Threshold RGB";
            this.ThresholdRGBToolStripMenuItem.Click += new System.EventHandler(this.ThresholdRgbToolStripMenuItemClick);
            // 
            // voronoiDiagramToolStripMenuItem
            // 
            this.voronoiDiagramToolStripMenuItem.Name = "voronoiDiagramToolStripMenuItem";
            this.voronoiDiagramToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.voronoiDiagramToolStripMenuItem.Text = "Voronoi Diagram";
            this.voronoiDiagramToolStripMenuItem.Click += new System.EventHandler(this.VoronoiDiagramToolStripMenuItemClick);
            // 
            // whiteContentToolStripMenuItem
            // 
            this.whiteContentToolStripMenuItem.Name = "whiteContentToolStripMenuItem";
            this.whiteContentToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.whiteContentToolStripMenuItem.Text = "White Content";
            this.whiteContentToolStripMenuItem.Click += new System.EventHandler(this.WhiteContentToolStripMenuItemClick);
            // 
            // meanShiftToolStripMenuItem
            // 
            this.meanShiftToolStripMenuItem.Name = "meanShiftToolStripMenuItem";
            this.meanShiftToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.meanShiftToolStripMenuItem.Text = "MeanShift";
            this.meanShiftToolStripMenuItem.Click += new System.EventHandler(this.MeanShiftToolStripMenuItemClick);
            // 
            // NonSBIPFilterToolStripMenuItem
            // 
            this.NonSBIPFilterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.coloredCannyEdgeDetectorToolStripMenuItem,
            this.colorEliminatedRegionGrowingToolStripMenuItem,
            this.KeyPointBasedFloodFillToolStripMenuItem,
            this.floodFillToolStripMenuItem,
            this.simpleDilatationToolStripMenuItem,
            this.splitColorSpaceChannelsToolStripMenuItem,
            this.conTrapContourTracerToolStripMenuItem,
            this.conGrapLightToolStripMenuItem,
            this.SimpleLuvColorbasedSegmentationToolStripMenuItem,
            this.lUVColorbasedSegmentationToolStripMenuItem});
            this.NonSBIPFilterToolStripMenuItem.Name = "NonSBIPFilterToolStripMenuItem";
            this.NonSBIPFilterToolStripMenuItem.Size = new System.Drawing.Size(127, 24);
            this.NonSBIPFilterToolStripMenuItem.Text = "Non-SBIP Filters";
            // 
            // coloredCannyEdgeDetectorToolStripMenuItem
            // 
            this.coloredCannyEdgeDetectorToolStripMenuItem.Name = "coloredCannyEdgeDetectorToolStripMenuItem";
            this.coloredCannyEdgeDetectorToolStripMenuItem.Size = new System.Drawing.Size(357, 24);
            this.coloredCannyEdgeDetectorToolStripMenuItem.Text = "Colored Canny Edge Detector";
            this.coloredCannyEdgeDetectorToolStripMenuItem.Click += new System.EventHandler(this.ColoredCannyEdgeDetectorToolStripMenuItemClick);
            // 
            // colorEliminatedRegionGrowingToolStripMenuItem
            // 
            this.colorEliminatedRegionGrowingToolStripMenuItem.Name = "colorEliminatedRegionGrowingToolStripMenuItem";
            this.colorEliminatedRegionGrowingToolStripMenuItem.Size = new System.Drawing.Size(357, 24);
            this.colorEliminatedRegionGrowingToolStripMenuItem.Text = "Color-Eliminated Region Growing";
            this.colorEliminatedRegionGrowingToolStripMenuItem.Click += new System.EventHandler(this.ColorEliminatedRegionGrowingToolStripMenuItemClick);
            // 
            // KeyPointBasedFloodFillToolStripMenuItem
            // 
            this.KeyPointBasedFloodFillToolStripMenuItem.Name = "KeyPointBasedFloodFillToolStripMenuItem";
            this.KeyPointBasedFloodFillToolStripMenuItem.Size = new System.Drawing.Size(357, 24);
            this.KeyPointBasedFloodFillToolStripMenuItem.Text = "Key Point-based Color Segmentation";
            this.KeyPointBasedFloodFillToolStripMenuItem.Click += new System.EventHandler(this.KeyPointBasedFloodFillToolStripMenuItemClick);
            // 
            // floodFillToolStripMenuItem
            // 
            this.floodFillToolStripMenuItem.Name = "floodFillToolStripMenuItem";
            this.floodFillToolStripMenuItem.Size = new System.Drawing.Size(357, 24);
            this.floodFillToolStripMenuItem.Text = "Pseudo Flood Fill";
            this.floodFillToolStripMenuItem.Click += new System.EventHandler(this.SimpleColorSegmentationToolStripMenuItemClick);
            // 
            // simpleDilatationToolStripMenuItem
            // 
            this.simpleDilatationToolStripMenuItem.Name = "simpleDilatationToolStripMenuItem";
            this.simpleDilatationToolStripMenuItem.Size = new System.Drawing.Size(357, 24);
            this.simpleDilatationToolStripMenuItem.Text = "Simple Dilatation";
            this.simpleDilatationToolStripMenuItem.Click += new System.EventHandler(this.SimpleDilatationToolStripMenuItemClick);
            // 
            // splitColorSpaceChannelsToolStripMenuItem
            // 
            this.splitColorSpaceChannelsToolStripMenuItem.Name = "splitColorSpaceChannelsToolStripMenuItem";
            this.splitColorSpaceChannelsToolStripMenuItem.Size = new System.Drawing.Size(357, 24);
            this.splitColorSpaceChannelsToolStripMenuItem.Text = "Split Color Space Channels";
            this.splitColorSpaceChannelsToolStripMenuItem.Click += new System.EventHandler(this.SplitColorSpaceChannelsToolStripMenuItemClick);
            // 
            // conTrapContourTracerToolStripMenuItem
            // 
            this.conTrapContourTracerToolStripMenuItem.Name = "conTrapContourTracerToolStripMenuItem";
            this.conTrapContourTracerToolStripMenuItem.Size = new System.Drawing.Size(357, 24);
            this.conTrapContourTracerToolStripMenuItem.Text = "ConGrap Contour Tracer";
            this.conTrapContourTracerToolStripMenuItem.Click += new System.EventHandler(this.ConTrapContourTracerToolStripMenuItemClick);
            // 
            // conGrapLightToolStripMenuItem
            // 
            this.conGrapLightToolStripMenuItem.Name = "conGrapLightToolStripMenuItem";
            this.conGrapLightToolStripMenuItem.Size = new System.Drawing.Size(357, 24);
            this.conGrapLightToolStripMenuItem.Text = "ConGrap Light";
            this.conGrapLightToolStripMenuItem.Click += new System.EventHandler(this.ConGrapLightToolStripMenuItemClick);
            // 
            // SimpleLuvColorbasedSegmentationToolStripMenuItem
            // 
            this.SimpleLuvColorbasedSegmentationToolStripMenuItem.Name = "SimpleLuvColorbasedSegmentationToolStripMenuItem";
            this.SimpleLuvColorbasedSegmentationToolStripMenuItem.Size = new System.Drawing.Size(357, 24);
            this.SimpleLuvColorbasedSegmentationToolStripMenuItem.Text = "Simple L*U*V*  color-based Segmentation";
            this.SimpleLuvColorbasedSegmentationToolStripMenuItem.Click += new System.EventHandler(this.SimpleLuvColorbasedSegmentationToolStripMenuItemClick);
            // 
            // lUVColorbasedSegmentationToolStripMenuItem
            // 
            this.lUVColorbasedSegmentationToolStripMenuItem.Name = "lUVColorbasedSegmentationToolStripMenuItem";
            this.lUVColorbasedSegmentationToolStripMenuItem.Size = new System.Drawing.Size(357, 24);
            this.lUVColorbasedSegmentationToolStripMenuItem.Text = "L*U*V*  color-based Segmentation";
            this.lUVColorbasedSegmentationToolStripMenuItem.Click += new System.EventHandler(this.LuvColorbasedSegmentationToolStripMenuItemClick);
            // 
            // InfoToolStripMenuItem
            // 
            this.InfoToolStripMenuItem.Name = "InfoToolStripMenuItem";
            this.InfoToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.InfoToolStripMenuItem.Text = "Info";
            this.InfoToolStripMenuItem.Click += new System.EventHandler(this.InfoToolStripMenuItemClick);
            // 
            // UpdateToolStripMenuItem
            // 
            this.UpdateToolStripMenuItem.BackColor = System.Drawing.Color.Orange;
            this.UpdateToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.UpdateToolStripMenuItem.Name = "UpdateToolStripMenuItem";
            this.UpdateToolStripMenuItem.Size = new System.Drawing.Size(79, 24);
            this.UpdateToolStripMenuItem.Text = "UPDATE";
            this.UpdateToolStripMenuItem.Visible = false;
            this.UpdateToolStripMenuItem.Click += new System.EventHandler(this.UpdateToolStripMenuItemClick);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // infos
            // 
            this.infos.Controls.Add(this.lbHeight);
            this.infos.Controls.Add(this.lbWidth);
            this.infos.Controls.Add(this.lbY);
            this.infos.Controls.Add(this.lbX);
            this.infos.Controls.Add(this.lbHeightText);
            this.infos.Controls.Add(this.lbWidthText);
            this.infos.Controls.Add(this.lbYText);
            this.infos.Controls.Add(this.lbXText);
            this.infos.Location = new System.Drawing.Point(189, 374);
            this.infos.Margin = new System.Windows.Forms.Padding(4);
            this.infos.Name = "infos";
            this.infos.Padding = new System.Windows.Forms.Padding(4);
            this.infos.Size = new System.Drawing.Size(333, 76);
            this.infos.TabIndex = 2;
            this.infos.TabStop = false;
            this.infos.Text = "Informations";
            // 
            // lbHeight
            // 
            this.lbHeight.AutoSize = true;
            this.lbHeight.Location = new System.Drawing.Point(241, 46);
            this.lbHeight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbHeight.Name = "lbHeight";
            this.lbHeight.Size = new System.Drawing.Size(40, 17);
            this.lbHeight.TabIndex = 7;
            this.lbHeight.Text = "1000";
            // 
            // lbWidth
            // 
            this.lbWidth.AutoSize = true;
            this.lbWidth.Location = new System.Drawing.Point(80, 46);
            this.lbWidth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbWidth.Name = "lbWidth";
            this.lbWidth.Size = new System.Drawing.Size(40, 17);
            this.lbWidth.TabIndex = 6;
            this.lbWidth.Text = "1000";
            // 
            // lbY
            // 
            this.lbY.AutoSize = true;
            this.lbY.Location = new System.Drawing.Point(241, 25);
            this.lbY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbY.Name = "lbY";
            this.lbY.Size = new System.Drawing.Size(32, 17);
            this.lbY.TabIndex = 5;
            this.lbY.Text = "100";
            // 
            // lbX
            // 
            this.lbX.AutoSize = true;
            this.lbX.Location = new System.Drawing.Point(80, 25);
            this.lbX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbX.Name = "lbX";
            this.lbX.Size = new System.Drawing.Size(32, 17);
            this.lbX.TabIndex = 4;
            this.lbX.Text = "100";
            // 
            // lbHeightText
            // 
            this.lbHeightText.AutoSize = true;
            this.lbHeightText.Location = new System.Drawing.Point(167, 46);
            this.lbHeightText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbHeightText.Name = "lbHeightText";
            this.lbHeightText.Size = new System.Drawing.Size(65, 17);
            this.lbHeightText.TabIndex = 3;
            this.lbHeightText.Text = "Height = ";
            // 
            // lbWidthText
            // 
            this.lbWidthText.AutoSize = true;
            this.lbWidthText.Location = new System.Drawing.Point(9, 46);
            this.lbWidthText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbWidthText.Name = "lbWidthText";
            this.lbWidthText.Size = new System.Drawing.Size(60, 17);
            this.lbWidthText.TabIndex = 2;
            this.lbWidthText.Text = "Width = ";
            // 
            // lbYText
            // 
            this.lbYText.AutoSize = true;
            this.lbYText.Location = new System.Drawing.Point(199, 25);
            this.lbYText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbYText.Name = "lbYText";
            this.lbYText.Size = new System.Drawing.Size(33, 17);
            this.lbYText.TabIndex = 1;
            this.lbYText.Text = "Y = ";
            // 
            // lbXText
            // 
            this.lbXText.AutoSize = true;
            this.lbXText.Location = new System.Drawing.Point(37, 25);
            this.lbXText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbXText.Name = "lbXText";
            this.lbXText.Size = new System.Drawing.Size(33, 17);
            this.lbXText.TabIndex = 0;
            this.lbXText.Text = "X = ";
            // 
            // ProcessorForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1459, 727);
            this.Controls.Add(this.infos);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ProcessorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Serotonin - Image Processing";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ProcessorFormDragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.ProcessorFormDragOver);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.infos.ResumeLayout(false);
            this.infos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem QuitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NoFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InvertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChessboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LaplaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GrayscaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ThresholdRGBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ThresholdToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExtractChannelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sepiaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RotateChannelToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem tsItmOpenVideo;
        private System.Windows.Forms.ToolStripMenuItem tsItmOpenImage;
        private System.Windows.Forms.ToolStripSeparator tsSeparator;
        private System.Windows.Forms.ToolStripSeparator tsSeparator2;
        private System.Windows.Forms.ToolStripMenuItem SobelToolStripMenuItem;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.ToolStripMenuItem UpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CannyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NonSBIPFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem posterizationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem floodFillToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem whiteContentToolStripMenuItem;
        private System.Windows.Forms.Label lbHeightText;
        private System.Windows.Forms.Label lbWidthText;
        private System.Windows.Forms.Label lbYText;
        private System.Windows.Forms.Label lbXText;
        private System.Windows.Forms.GroupBox infos;
        private System.Windows.Forms.Label lbHeight;
        private System.Windows.Forms.Label lbWidth;
        private System.Windows.Forms.Label lbY;
        private System.Windows.Forms.Label lbX;
        private System.Windows.Forms.ToolStripMenuItem voronoiDiagramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem KeyPointBasedFloodFillToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gaussianBlurToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem coloredCannyEdgeDetectorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conTrapContourTracerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simpleDilatationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorEliminatedRegionGrowingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SquareChannelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem splitColorSpaceChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem meanShiftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Convolution3x3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Convolution5x5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SimpleLuvColorbasedSegmentationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conGrapLightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lUVColorbasedSegmentationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LightingToolStripMenuItem;

    }
}

