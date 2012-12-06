// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
namespace Serotonin.OptionsForms
{
    partial class NSColoredCannyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NSColoredCannyForm));
            this.Options = new System.Windows.Forms.GroupBox();
            this.gbProcess = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.tabctrlOptions = new System.Windows.Forms.TabControl();
            this.tabpgCanny = new System.Windows.Forms.TabPage();
            this.lbGaussSigmaText = new System.Windows.Forms.Label();
            this.trbarGaussSigma = new System.Windows.Forms.TrackBar();
            this.lbGaussSigma = new System.Windows.Forms.Label();
            this.lbGaussSizeText = new System.Windows.Forms.Label();
            this.trbarGaussSize = new System.Windows.Forms.TrackBar();
            this.lbGaussSize = new System.Windows.Forms.Label();
            this.lbLowThresholdText = new System.Windows.Forms.Label();
            this.trbarLowThreshold = new System.Windows.Forms.TrackBar();
            this.lbLowThreshold = new System.Windows.Forms.Label();
            this.lbHighThresholdText = new System.Windows.Forms.Label();
            this.trbarHighThreshold = new System.Windows.Forms.TrackBar();
            this.lbHighThreshold = new System.Windows.Forms.Label();
            this.tabpgDrawMode = new System.Windows.Forms.TabPage();
            this.gbLineThickness = new System.Windows.Forms.GroupBox();
            this.lbDiameterText = new System.Windows.Forms.Label();
            this.trBarDiameter = new System.Windows.Forms.TrackBar();
            this.lbDiameter = new System.Windows.Forms.Label();
            this.gbDrawInImage = new System.Windows.Forms.GroupBox();
            this.rdBtnYesDrawInImage = new System.Windows.Forms.RadioButton();
            this.rdBtnNoDrawInImage = new System.Windows.Forms.RadioButton();
            this.pboxSemiCircle = new System.Windows.Forms.PictureBox();
            this.gbDrawModes = new System.Windows.Forms.GroupBox();
            this.rbOrientColored = new System.Windows.Forms.RadioButton();
            this.rbNormal = new System.Windows.Forms.RadioButton();
            this.Options.SuspendLayout();
            this.gbProcess.SuspendLayout();
            this.tabctrlOptions.SuspendLayout();
            this.tabpgCanny.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbarGaussSigma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbarGaussSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbarLowThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbarHighThreshold)).BeginInit();
            this.tabpgDrawMode.SuspendLayout();
            this.gbLineThickness.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trBarDiameter)).BeginInit();
            this.gbDrawInImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxSemiCircle)).BeginInit();
            this.gbDrawModes.SuspendLayout();
            this.SuspendLayout();
            // 
            // Options
            // 
            this.Options.Controls.Add(this.gbProcess);
            this.Options.Controls.Add(this.tabctrlOptions);
            this.Options.Location = new System.Drawing.Point(16, 15);
            this.Options.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Options.Name = "Options";
            this.Options.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Options.Size = new System.Drawing.Size(333, 615);
            this.Options.TabIndex = 3;
            this.Options.TabStop = false;
            this.Options.Text = "Options";
            // 
            // gbProcess
            // 
            this.gbProcess.Controls.Add(this.btnReset);
            this.gbProcess.Controls.Add(this.btnProcess);
            this.gbProcess.Location = new System.Drawing.Point(9, 540);
            this.gbProcess.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbProcess.Name = "gbProcess";
            this.gbProcess.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbProcess.Size = new System.Drawing.Size(316, 68);
            this.gbProcess.TabIndex = 2;
            this.gbProcess.TabStop = false;
            this.gbProcess.Text = "Process";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(183, 25);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(120, 28);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.BtnResetClick);
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(16, 25);
            this.btnProcess.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(120, 28);
            this.btnProcess.TabIndex = 0;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.BtnProcessClick);
            // 
            // tabctrlOptions
            // 
            this.tabctrlOptions.Controls.Add(this.tabpgCanny);
            this.tabctrlOptions.Controls.Add(this.tabpgDrawMode);
            this.tabctrlOptions.Location = new System.Drawing.Point(8, 23);
            this.tabctrlOptions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabctrlOptions.Name = "tabctrlOptions";
            this.tabctrlOptions.SelectedIndex = 0;
            this.tabctrlOptions.Size = new System.Drawing.Size(317, 510);
            this.tabctrlOptions.TabIndex = 0;
            // 
            // tabpgCanny
            // 
            this.tabpgCanny.BackColor = System.Drawing.Color.AliceBlue;
            this.tabpgCanny.Controls.Add(this.lbGaussSigmaText);
            this.tabpgCanny.Controls.Add(this.trbarGaussSigma);
            this.tabpgCanny.Controls.Add(this.lbGaussSigma);
            this.tabpgCanny.Controls.Add(this.lbGaussSizeText);
            this.tabpgCanny.Controls.Add(this.trbarGaussSize);
            this.tabpgCanny.Controls.Add(this.lbGaussSize);
            this.tabpgCanny.Controls.Add(this.lbLowThresholdText);
            this.tabpgCanny.Controls.Add(this.trbarLowThreshold);
            this.tabpgCanny.Controls.Add(this.lbLowThreshold);
            this.tabpgCanny.Controls.Add(this.lbHighThresholdText);
            this.tabpgCanny.Controls.Add(this.trbarHighThreshold);
            this.tabpgCanny.Controls.Add(this.lbHighThreshold);
            this.tabpgCanny.Location = new System.Drawing.Point(4, 25);
            this.tabpgCanny.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabpgCanny.Name = "tabpgCanny";
            this.tabpgCanny.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabpgCanny.Size = new System.Drawing.Size(309, 481);
            this.tabpgCanny.TabIndex = 0;
            this.tabpgCanny.Text = "Canny Edge Detector";
            // 
            // lbGaussSigmaText
            // 
            this.lbGaussSigmaText.AutoSize = true;
            this.lbGaussSigmaText.Location = new System.Drawing.Point(8, 250);
            this.lbGaussSigmaText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbGaussSigmaText.Name = "lbGaussSigmaText";
            this.lbGaussSigmaText.Size = new System.Drawing.Size(115, 17);
            this.lbGaussSigmaText.TabIndex = 25;
            this.lbGaussSigmaText.Text = "Gaussian Sigma:";
            // 
            // trbarGaussSigma
            // 
            this.trbarGaussSigma.LargeChange = 10;
            this.trbarGaussSigma.Location = new System.Drawing.Point(8, 270);
            this.trbarGaussSigma.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trbarGaussSigma.Maximum = 5000;
            this.trbarGaussSigma.Minimum = 500;
            this.trbarGaussSigma.Name = "trbarGaussSigma";
            this.trbarGaussSigma.Size = new System.Drawing.Size(291, 56);
            this.trbarGaussSigma.TabIndex = 24;
            this.trbarGaussSigma.Value = 1400;
            this.trbarGaussSigma.ValueChanged += new System.EventHandler(this.TrbarGaussSigmaValueChanged);
            // 
            // lbGaussSigma
            // 
            this.lbGaussSigma.AutoSize = true;
            this.lbGaussSigma.Location = new System.Drawing.Point(131, 250);
            this.lbGaussSigma.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbGaussSigma.Name = "lbGaussSigma";
            this.lbGaussSigma.Size = new System.Drawing.Size(28, 17);
            this.lbGaussSigma.TabIndex = 26;
            this.lbGaussSigma.Text = "1.4";
            // 
            // lbGaussSizeText
            // 
            this.lbGaussSizeText.AutoSize = true;
            this.lbGaussSizeText.Location = new System.Drawing.Point(8, 170);
            this.lbGaussSizeText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbGaussSizeText.Name = "lbGaussSizeText";
            this.lbGaussSizeText.Size = new System.Drawing.Size(103, 17);
            this.lbGaussSizeText.TabIndex = 22;
            this.lbGaussSizeText.Text = "Gaussian Size:";
            // 
            // trbarGaussSize
            // 
            this.trbarGaussSize.LargeChange = 4;
            this.trbarGaussSize.Location = new System.Drawing.Point(8, 191);
            this.trbarGaussSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trbarGaussSize.Maximum = 21;
            this.trbarGaussSize.Minimum = 3;
            this.trbarGaussSize.Name = "trbarGaussSize";
            this.trbarGaussSize.Size = new System.Drawing.Size(291, 56);
            this.trbarGaussSize.SmallChange = 2;
            this.trbarGaussSize.TabIndex = 21;
            this.trbarGaussSize.TickFrequency = 2;
            this.trbarGaussSize.Value = 5;
            this.trbarGaussSize.ValueChanged += new System.EventHandler(this.TrbarGaussSizeValueChanged);
            // 
            // lbGaussSize
            // 
            this.lbGaussSize.AutoSize = true;
            this.lbGaussSize.Location = new System.Drawing.Point(131, 170);
            this.lbGaussSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbGaussSize.Name = "lbGaussSize";
            this.lbGaussSize.Size = new System.Drawing.Size(16, 17);
            this.lbGaussSize.TabIndex = 23;
            this.lbGaussSize.Text = "5";
            // 
            // lbLowThresholdText
            // 
            this.lbLowThresholdText.AutoSize = true;
            this.lbLowThresholdText.Location = new System.Drawing.Point(8, 90);
            this.lbLowThresholdText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbLowThresholdText.Name = "lbLowThresholdText";
            this.lbLowThresholdText.Size = new System.Drawing.Size(37, 17);
            this.lbLowThresholdText.TabIndex = 19;
            this.lbLowThresholdText.Text = "Low:";
            // 
            // trbarLowThreshold
            // 
            this.trbarLowThreshold.Location = new System.Drawing.Point(8, 111);
            this.trbarLowThreshold.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trbarLowThreshold.Maximum = 255;
            this.trbarLowThreshold.Name = "trbarLowThreshold";
            this.trbarLowThreshold.Size = new System.Drawing.Size(291, 56);
            this.trbarLowThreshold.TabIndex = 18;
            this.trbarLowThreshold.Value = 20;
            this.trbarLowThreshold.ValueChanged += new System.EventHandler(this.TrbarLowThresholdValueChanged);
            // 
            // lbLowThreshold
            // 
            this.lbLowThreshold.AutoSize = true;
            this.lbLowThreshold.Location = new System.Drawing.Point(131, 90);
            this.lbLowThreshold.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbLowThreshold.Name = "lbLowThreshold";
            this.lbLowThreshold.Size = new System.Drawing.Size(24, 17);
            this.lbLowThreshold.TabIndex = 20;
            this.lbLowThreshold.Text = "20";
            // 
            // lbHighThresholdText
            // 
            this.lbHighThresholdText.AutoSize = true;
            this.lbHighThresholdText.Location = new System.Drawing.Point(8, 10);
            this.lbHighThresholdText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbHighThresholdText.Name = "lbHighThresholdText";
            this.lbHighThresholdText.Size = new System.Drawing.Size(41, 17);
            this.lbHighThresholdText.TabIndex = 16;
            this.lbHighThresholdText.Text = "High:";
            // 
            // trbarHighThreshold
            // 
            this.trbarHighThreshold.Location = new System.Drawing.Point(8, 31);
            this.trbarHighThreshold.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trbarHighThreshold.Maximum = 255;
            this.trbarHighThreshold.Name = "trbarHighThreshold";
            this.trbarHighThreshold.Size = new System.Drawing.Size(291, 56);
            this.trbarHighThreshold.TabIndex = 15;
            this.trbarHighThreshold.Value = 40;
            this.trbarHighThreshold.ValueChanged += new System.EventHandler(this.TrbarHighThresholdValueChanged);
            // 
            // lbHighThreshold
            // 
            this.lbHighThreshold.AutoSize = true;
            this.lbHighThreshold.Location = new System.Drawing.Point(131, 10);
            this.lbHighThreshold.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbHighThreshold.Name = "lbHighThreshold";
            this.lbHighThreshold.Size = new System.Drawing.Size(24, 17);
            this.lbHighThreshold.TabIndex = 17;
            this.lbHighThreshold.Text = "40";
            // 
            // tabpgDrawMode
            // 
            this.tabpgDrawMode.BackColor = System.Drawing.Color.AliceBlue;
            this.tabpgDrawMode.Controls.Add(this.gbLineThickness);
            this.tabpgDrawMode.Controls.Add(this.gbDrawInImage);
            this.tabpgDrawMode.Controls.Add(this.pboxSemiCircle);
            this.tabpgDrawMode.Controls.Add(this.gbDrawModes);
            this.tabpgDrawMode.Location = new System.Drawing.Point(4, 25);
            this.tabpgDrawMode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabpgDrawMode.Name = "tabpgDrawMode";
            this.tabpgDrawMode.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabpgDrawMode.Size = new System.Drawing.Size(309, 481);
            this.tabpgDrawMode.TabIndex = 1;
            this.tabpgDrawMode.Text = "Edge Drawing";
            // 
            // gbLineThickness
            // 
            this.gbLineThickness.Controls.Add(this.lbDiameterText);
            this.gbLineThickness.Controls.Add(this.trBarDiameter);
            this.gbLineThickness.Controls.Add(this.lbDiameter);
            this.gbLineThickness.Location = new System.Drawing.Point(8, 346);
            this.gbLineThickness.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbLineThickness.Name = "gbLineThickness";
            this.gbLineThickness.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbLineThickness.Size = new System.Drawing.Size(291, 112);
            this.gbLineThickness.TabIndex = 30;
            this.gbLineThickness.TabStop = false;
            this.gbLineThickness.Text = "Line Thickness";
            // 
            // lbDiameterText
            // 
            this.lbDiameterText.AutoSize = true;
            this.lbDiameterText.Location = new System.Drawing.Point(8, 30);
            this.lbDiameterText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDiameterText.Name = "lbDiameterText";
            this.lbDiameterText.Size = new System.Drawing.Size(65, 17);
            this.lbDiameterText.TabIndex = 28;
            this.lbDiameterText.Text = "Diameter";
            // 
            // trBarDiameter
            // 
            this.trBarDiameter.LargeChange = 10;
            this.trBarDiameter.Location = new System.Drawing.Point(8, 49);
            this.trBarDiameter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trBarDiameter.Maximum = 9;
            this.trBarDiameter.Minimum = 1;
            this.trBarDiameter.Name = "trBarDiameter";
            this.trBarDiameter.Size = new System.Drawing.Size(275, 56);
            this.trBarDiameter.TabIndex = 27;
            this.trBarDiameter.Value = 1;
            this.trBarDiameter.ValueChanged += new System.EventHandler(this.TrBarDiameterValueChanged);
            // 
            // lbDiameter
            // 
            this.lbDiameter.AutoSize = true;
            this.lbDiameter.Location = new System.Drawing.Point(107, 30);
            this.lbDiameter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDiameter.Name = "lbDiameter";
            this.lbDiameter.Size = new System.Drawing.Size(16, 17);
            this.lbDiameter.TabIndex = 29;
            this.lbDiameter.Text = "1";
            // 
            // gbDrawInImage
            // 
            this.gbDrawInImage.Controls.Add(this.rdBtnYesDrawInImage);
            this.gbDrawInImage.Controls.Add(this.rdBtnNoDrawInImage);
            this.gbDrawInImage.Location = new System.Drawing.Point(8, 249);
            this.gbDrawInImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbDrawInImage.Name = "gbDrawInImage";
            this.gbDrawInImage.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbDrawInImage.Size = new System.Drawing.Size(291, 90);
            this.gbDrawInImage.TabIndex = 29;
            this.gbDrawInImage.TabStop = false;
            this.gbDrawInImage.Text = "Draw In Image";
            // 
            // rdBtnYesDrawInImage
            // 
            this.rdBtnYesDrawInImage.AutoSize = true;
            this.rdBtnYesDrawInImage.Location = new System.Drawing.Point(9, 54);
            this.rdBtnYesDrawInImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdBtnYesDrawInImage.Name = "rdBtnYesDrawInImage";
            this.rdBtnYesDrawInImage.Size = new System.Drawing.Size(53, 21);
            this.rdBtnYesDrawInImage.TabIndex = 1;
            this.rdBtnYesDrawInImage.Text = "Yes";
            this.rdBtnYesDrawInImage.UseVisualStyleBackColor = true;
            this.rdBtnYesDrawInImage.CheckedChanged += new System.EventHandler(this.RdBtnYesDrawInImageCheckedChanged);
            // 
            // rdBtnNoDrawInImage
            // 
            this.rdBtnNoDrawInImage.AutoSize = true;
            this.rdBtnNoDrawInImage.Checked = true;
            this.rdBtnNoDrawInImage.Location = new System.Drawing.Point(9, 25);
            this.rdBtnNoDrawInImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdBtnNoDrawInImage.Name = "rdBtnNoDrawInImage";
            this.rdBtnNoDrawInImage.Size = new System.Drawing.Size(47, 21);
            this.rdBtnNoDrawInImage.TabIndex = 0;
            this.rdBtnNoDrawInImage.TabStop = true;
            this.rdBtnNoDrawInImage.Text = "No";
            this.rdBtnNoDrawInImage.UseVisualStyleBackColor = true;
            // 
            // pboxSemiCircle
            // 
            this.pboxSemiCircle.Enabled = false;
            this.pboxSemiCircle.Image = ((System.Drawing.Image)(resources.GetObject("pboxSemiCircle.Image")));
            this.pboxSemiCircle.Location = new System.Drawing.Point(8, 105);
            this.pboxSemiCircle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pboxSemiCircle.Name = "pboxSemiCircle";
            this.pboxSemiCircle.Size = new System.Drawing.Size(291, 137);
            this.pboxSemiCircle.TabIndex = 29;
            this.pboxSemiCircle.TabStop = false;
            this.pboxSemiCircle.Visible = false;
            // 
            // gbDrawModes
            // 
            this.gbDrawModes.Controls.Add(this.rbOrientColored);
            this.gbDrawModes.Controls.Add(this.rbNormal);
            this.gbDrawModes.Location = new System.Drawing.Point(8, 7);
            this.gbDrawModes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbDrawModes.Name = "gbDrawModes";
            this.gbDrawModes.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbDrawModes.Size = new System.Drawing.Size(291, 90);
            this.gbDrawModes.TabIndex = 28;
            this.gbDrawModes.TabStop = false;
            this.gbDrawModes.Text = "Draw Mode";
            // 
            // rbOrientColored
            // 
            this.rbOrientColored.AutoSize = true;
            this.rbOrientColored.Location = new System.Drawing.Point(9, 54);
            this.rbOrientColored.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbOrientColored.Name = "rbOrientColored";
            this.rbOrientColored.Size = new System.Drawing.Size(185, 21);
            this.rbOrientColored.TabIndex = 1;
            this.rbOrientColored.Text = "Colored edge orientation";
            this.rbOrientColored.UseVisualStyleBackColor = true;
            this.rbOrientColored.CheckedChanged += new System.EventHandler(this.RbOrientColoredCheckedChanged);
            // 
            // rbNormal
            // 
            this.rbNormal.AutoSize = true;
            this.rbNormal.Checked = true;
            this.rbNormal.Location = new System.Drawing.Point(9, 25);
            this.rbNormal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbNormal.Name = "rbNormal";
            this.rbNormal.Size = new System.Drawing.Size(74, 21);
            this.rbNormal.TabIndex = 0;
            this.rbNormal.TabStop = true;
            this.rbNormal.Text = "Normal";
            this.rbNormal.UseVisualStyleBackColor = true;
            // 
            // NSColoredCannyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 642);
            this.Controls.Add(this.Options);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "NSColoredCannyForm";
            this.Text = "ColoredCannyForm";
            this.Options.ResumeLayout(false);
            this.gbProcess.ResumeLayout(false);
            this.tabctrlOptions.ResumeLayout(false);
            this.tabpgCanny.ResumeLayout(false);
            this.tabpgCanny.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbarGaussSigma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbarGaussSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbarLowThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbarHighThreshold)).EndInit();
            this.tabpgDrawMode.ResumeLayout(false);
            this.gbLineThickness.ResumeLayout(false);
            this.gbLineThickness.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trBarDiameter)).EndInit();
            this.gbDrawInImage.ResumeLayout(false);
            this.gbDrawInImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxSemiCircle)).EndInit();
            this.gbDrawModes.ResumeLayout(false);
            this.gbDrawModes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        /// <summary>Contains all filter specified property-GUI elements.</summary>
        public System.Windows.Forms.GroupBox Options;
        private System.Windows.Forms.TabControl tabctrlOptions;
        private System.Windows.Forms.TabPage tabpgCanny;
        private System.Windows.Forms.Label lbGaussSigmaText;
        private System.Windows.Forms.TrackBar trbarGaussSigma;
        private System.Windows.Forms.Label lbGaussSigma;
        private System.Windows.Forms.Label lbGaussSizeText;
        private System.Windows.Forms.TrackBar trbarGaussSize;
        private System.Windows.Forms.Label lbGaussSize;
        private System.Windows.Forms.Label lbLowThresholdText;
        private System.Windows.Forms.TrackBar trbarLowThreshold;
        private System.Windows.Forms.Label lbLowThreshold;
        private System.Windows.Forms.Label lbHighThresholdText;
        private System.Windows.Forms.TrackBar trbarHighThreshold;
        private System.Windows.Forms.Label lbHighThreshold;
        private System.Windows.Forms.GroupBox gbProcess;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.TabPage tabpgDrawMode;
        private System.Windows.Forms.GroupBox gbDrawModes;
        private System.Windows.Forms.RadioButton rbOrientColored;
        private System.Windows.Forms.RadioButton rbNormal;
        private System.Windows.Forms.PictureBox pboxSemiCircle;
        private System.Windows.Forms.GroupBox gbDrawInImage;
        private System.Windows.Forms.RadioButton rdBtnYesDrawInImage;
        private System.Windows.Forms.RadioButton rdBtnNoDrawInImage;
        private System.Windows.Forms.GroupBox gbLineThickness;
        private System.Windows.Forms.Label lbDiameterText;
        private System.Windows.Forms.TrackBar trBarDiameter;
        private System.Windows.Forms.Label lbDiameter;
    }
}