// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
namespace Serotonin.OptionsForms
{
    partial class ConGrapForm
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
            this.Options = new System.Windows.Forms.GroupBox();
            this.gbProcess = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.gbResults = new System.Windows.Forms.GroupBox();
            this.lbContourNumbers = new System.Windows.Forms.Label();
            this.lbContourNumbersText = new System.Windows.Forms.Label();
            this.tabctrlOptions = new System.Windows.Forms.TabControl();
            this.tabpgContourTracer = new System.Windows.Forms.TabPage();
            this.lbWeightText = new System.Windows.Forms.Label();
            this.trbarWeight = new System.Windows.Forms.TrackBar();
            this.lbWeight = new System.Windows.Forms.Label();
            this.gbDrawModes = new System.Windows.Forms.GroupBox();
            this.trbarStrength = new System.Windows.Forms.TrackBar();
            this.lbLineStrength = new System.Windows.Forms.Label();
            this.chboxDrawBorderLine = new System.Windows.Forms.CheckBox();
            this.chboxDrawBorderRect = new System.Windows.Forms.CheckBox();
            this.lbTraceRadiusText = new System.Windows.Forms.Label();
            this.trbarTraceRadius = new System.Windows.Forms.TrackBar();
            this.lbTraceRadius = new System.Windows.Forms.Label();
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
            this.chboxDoDilatations = new System.Windows.Forms.CheckBox();
            this.lbLineStrengthName = new System.Windows.Forms.Label();
            this.Options.SuspendLayout();
            this.gbProcess.SuspendLayout();
            this.gbResults.SuspendLayout();
            this.tabctrlOptions.SuspendLayout();
            this.tabpgContourTracer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbarWeight)).BeginInit();
            this.gbDrawModes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbarStrength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbarTraceRadius)).BeginInit();
            this.tabpgCanny.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbarGaussSigma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbarGaussSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbarLowThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbarHighThreshold)).BeginInit();
            this.SuspendLayout();
            // 
            // Options
            // 
            this.Options.Controls.Add(this.gbProcess);
            this.Options.Controls.Add(this.gbResults);
            this.Options.Controls.Add(this.tabctrlOptions);
            this.Options.Location = new System.Drawing.Point(12, 12);
            this.Options.Name = "Options";
            this.Options.Size = new System.Drawing.Size(250, 500);
            this.Options.TabIndex = 3;
            this.Options.TabStop = false;
            this.Options.Text = "ConGrap Options";
            // 
            // gbProcess
            // 
            this.gbProcess.Controls.Add(this.btnReset);
            this.gbProcess.Controls.Add(this.btnProcess);
            this.gbProcess.Location = new System.Drawing.Point(7, 439);
            this.gbProcess.Name = "gbProcess";
            this.gbProcess.Size = new System.Drawing.Size(237, 55);
            this.gbProcess.TabIndex = 2;
            this.gbProcess.TabStop = false;
            this.gbProcess.Text = "Process";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(137, 20);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(90, 23);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.BtnResetClick);
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(12, 20);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(90, 23);
            this.btnProcess.TabIndex = 0;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.BtnProcessClick);
            // 
            // gbResults
            // 
            this.gbResults.Controls.Add(this.lbContourNumbers);
            this.gbResults.Controls.Add(this.lbContourNumbersText);
            this.gbResults.Location = new System.Drawing.Point(7, 365);
            this.gbResults.Name = "gbResults";
            this.gbResults.Size = new System.Drawing.Size(237, 68);
            this.gbResults.TabIndex = 1;
            this.gbResults.TabStop = false;
            this.gbResults.Text = "Results";
            // 
            // lbContourNumbers
            // 
            this.lbContourNumbers.AutoSize = true;
            this.lbContourNumbers.Location = new System.Drawing.Point(118, 20);
            this.lbContourNumbers.Name = "lbContourNumbers";
            this.lbContourNumbers.Size = new System.Drawing.Size(13, 13);
            this.lbContourNumbers.TabIndex = 1;
            this.lbContourNumbers.Text = "0";
            // 
            // lbContourNumbersText
            // 
            this.lbContourNumbersText.AutoSize = true;
            this.lbContourNumbersText.Location = new System.Drawing.Point(7, 20);
            this.lbContourNumbersText.Name = "lbContourNumbersText";
            this.lbContourNumbersText.Size = new System.Drawing.Size(104, 13);
            this.lbContourNumbersText.TabIndex = 0;
            this.lbContourNumbersText.Text = "Number of Contours:";
            // 
            // tabctrlOptions
            // 
            this.tabctrlOptions.Controls.Add(this.tabpgContourTracer);
            this.tabctrlOptions.Controls.Add(this.tabpgCanny);
            this.tabctrlOptions.Location = new System.Drawing.Point(6, 19);
            this.tabctrlOptions.Name = "tabctrlOptions";
            this.tabctrlOptions.SelectedIndex = 0;
            this.tabctrlOptions.Size = new System.Drawing.Size(238, 325);
            this.tabctrlOptions.TabIndex = 0;
            // 
            // tabpgContourTracer
            // 
            this.tabpgContourTracer.BackColor = System.Drawing.Color.AliceBlue;
            this.tabpgContourTracer.Controls.Add(this.lbWeightText);
            this.tabpgContourTracer.Controls.Add(this.trbarWeight);
            this.tabpgContourTracer.Controls.Add(this.lbWeight);
            this.tabpgContourTracer.Controls.Add(this.gbDrawModes);
            this.tabpgContourTracer.Controls.Add(this.lbTraceRadiusText);
            this.tabpgContourTracer.Controls.Add(this.trbarTraceRadius);
            this.tabpgContourTracer.Controls.Add(this.lbTraceRadius);
            this.tabpgContourTracer.Location = new System.Drawing.Point(4, 22);
            this.tabpgContourTracer.Name = "tabpgContourTracer";
            this.tabpgContourTracer.Padding = new System.Windows.Forms.Padding(3);
            this.tabpgContourTracer.Size = new System.Drawing.Size(230, 299);
            this.tabpgContourTracer.TabIndex = 1;
            this.tabpgContourTracer.Text = "Contour Tracer";
            // 
            // lbWeightText
            // 
            this.lbWeightText.AutoSize = true;
            this.lbWeightText.Location = new System.Drawing.Point(6, 7);
            this.lbWeightText.Name = "lbWeightText";
            this.lbWeightText.Size = new System.Drawing.Size(44, 13);
            this.lbWeightText.TabIndex = 17;
            this.lbWeightText.Text = "Weight:";
            // 
            // trbarWeight
            // 
            this.trbarWeight.Location = new System.Drawing.Point(6, 23);
            this.trbarWeight.Maximum = 25;
            this.trbarWeight.Name = "trbarWeight";
            this.trbarWeight.Size = new System.Drawing.Size(218, 45);
            this.trbarWeight.TabIndex = 16;
            this.trbarWeight.Value = 3;
            this.trbarWeight.ValueChanged += new System.EventHandler(this.TrbarWeightValueChanged);
            // 
            // lbWeight
            // 
            this.lbWeight.AutoSize = true;
            this.lbWeight.Location = new System.Drawing.Point(120, 7);
            this.lbWeight.Name = "lbWeight";
            this.lbWeight.Size = new System.Drawing.Size(13, 13);
            this.lbWeight.TabIndex = 18;
            this.lbWeight.Text = "3";
            // 
            // gbDrawModes
            // 
            this.gbDrawModes.Controls.Add(this.lbLineStrengthName);
            this.gbDrawModes.Controls.Add(this.chboxDoDilatations);
            this.gbDrawModes.Controls.Add(this.trbarStrength);
            this.gbDrawModes.Controls.Add(this.lbLineStrength);
            this.gbDrawModes.Controls.Add(this.chboxDrawBorderLine);
            this.gbDrawModes.Controls.Add(this.chboxDrawBorderRect);
            this.gbDrawModes.Location = new System.Drawing.Point(9, 140);
            this.gbDrawModes.Name = "gbDrawModes";
            this.gbDrawModes.Size = new System.Drawing.Size(218, 153);
            this.gbDrawModes.TabIndex = 15;
            this.gbDrawModes.TabStop = false;
            this.gbDrawModes.Text = "Draw Modes";
            // 
            // trbarStrength
            // 
            this.trbarStrength.Location = new System.Drawing.Point(6, 88);
            this.trbarStrength.Minimum = 1;
            this.trbarStrength.Name = "trbarStrength";
            this.trbarStrength.Size = new System.Drawing.Size(109, 45);
            this.trbarStrength.TabIndex = 19;
            this.trbarStrength.Value = 2;
            this.trbarStrength.ValueChanged += new System.EventHandler(this.TrbarRectStrengthValueChanged);
            // 
            // lbLineStrength
            // 
            this.lbLineStrength.AutoSize = true;
            this.lbLineStrength.Location = new System.Drawing.Point(177, 88);
            this.lbLineStrength.Name = "lbLineStrength";
            this.lbLineStrength.Size = new System.Drawing.Size(13, 13);
            this.lbLineStrength.TabIndex = 20;
            this.lbLineStrength.Text = "2";
            // 
            // chboxDrawBorderLine
            // 
            this.chboxDrawBorderLine.AutoSize = true;
            this.chboxDrawBorderLine.Location = new System.Drawing.Point(6, 65);
            this.chboxDrawBorderLine.Name = "chboxDrawBorderLine";
            this.chboxDrawBorderLine.Size = new System.Drawing.Size(109, 17);
            this.chboxDrawBorderLine.TabIndex = 1;
            this.chboxDrawBorderLine.Text = "Only Border Lines";
            this.chboxDrawBorderLine.UseVisualStyleBackColor = true;
            this.chboxDrawBorderLine.CheckedChanged += new System.EventHandler(this.ChboxDrawBorderLineCheckedChanged);
            // 
            // chboxDrawBorderRect
            // 
            this.chboxDrawBorderRect.AutoSize = true;
            this.chboxDrawBorderRect.Location = new System.Drawing.Point(6, 42);
            this.chboxDrawBorderRect.Name = "chboxDrawBorderRect";
            this.chboxDrawBorderRect.Size = new System.Drawing.Size(114, 17);
            this.chboxDrawBorderRect.TabIndex = 0;
            this.chboxDrawBorderRect.Text = "Border Rectangles";
            this.chboxDrawBorderRect.UseVisualStyleBackColor = true;
            this.chboxDrawBorderRect.CheckedChanged += new System.EventHandler(this.ChboxDrawBorderRectCheckedChanged);
            // 
            // lbTraceRadiusText
            // 
            this.lbTraceRadiusText.AutoSize = true;
            this.lbTraceRadiusText.Location = new System.Drawing.Point(6, 73);
            this.lbTraceRadiusText.Name = "lbTraceRadiusText";
            this.lbTraceRadiusText.Size = new System.Drawing.Size(74, 13);
            this.lbTraceRadiusText.TabIndex = 13;
            this.lbTraceRadiusText.Text = "Trace Radius:";
            // 
            // trbarTraceRadius
            // 
            this.trbarTraceRadius.Location = new System.Drawing.Point(6, 89);
            this.trbarTraceRadius.Maximum = 100;
            this.trbarTraceRadius.Minimum = 1;
            this.trbarTraceRadius.Name = "trbarTraceRadius";
            this.trbarTraceRadius.Size = new System.Drawing.Size(218, 45);
            this.trbarTraceRadius.TabIndex = 12;
            this.trbarTraceRadius.Value = 3;
            this.trbarTraceRadius.ValueChanged += new System.EventHandler(this.TrbarTraceRadiusValueChanged);
            // 
            // lbTraceRadius
            // 
            this.lbTraceRadius.AutoSize = true;
            this.lbTraceRadius.Location = new System.Drawing.Point(120, 73);
            this.lbTraceRadius.Name = "lbTraceRadius";
            this.lbTraceRadius.Size = new System.Drawing.Size(13, 13);
            this.lbTraceRadius.TabIndex = 14;
            this.lbTraceRadius.Text = "3";
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
            this.tabpgCanny.Location = new System.Drawing.Point(4, 22);
            this.tabpgCanny.Name = "tabpgCanny";
            this.tabpgCanny.Padding = new System.Windows.Forms.Padding(3);
            this.tabpgCanny.Size = new System.Drawing.Size(230, 299);
            this.tabpgCanny.TabIndex = 0;
            this.tabpgCanny.Text = "Canny Detector";
            // 
            // lbGaussSigmaText
            // 
            this.lbGaussSigmaText.AutoSize = true;
            this.lbGaussSigmaText.Location = new System.Drawing.Point(6, 203);
            this.lbGaussSigmaText.Name = "lbGaussSigmaText";
            this.lbGaussSigmaText.Size = new System.Drawing.Size(86, 13);
            this.lbGaussSigmaText.TabIndex = 25;
            this.lbGaussSigmaText.Text = "Gaussian Sigma:";
            // 
            // trbarGaussSigma
            // 
            this.trbarGaussSigma.LargeChange = 10;
            this.trbarGaussSigma.Location = new System.Drawing.Point(6, 219);
            this.trbarGaussSigma.Maximum = 5000;
            this.trbarGaussSigma.Minimum = 500;
            this.trbarGaussSigma.Name = "trbarGaussSigma";
            this.trbarGaussSigma.Size = new System.Drawing.Size(218, 45);
            this.trbarGaussSigma.TabIndex = 24;
            this.trbarGaussSigma.Value = 1400;
            this.trbarGaussSigma.ValueChanged += new System.EventHandler(this.TrbarGaussSigmaValueChanged);
            // 
            // lbGaussSigma
            // 
            this.lbGaussSigma.AutoSize = true;
            this.lbGaussSigma.Location = new System.Drawing.Point(98, 203);
            this.lbGaussSigma.Name = "lbGaussSigma";
            this.lbGaussSigma.Size = new System.Drawing.Size(22, 13);
            this.lbGaussSigma.TabIndex = 26;
            this.lbGaussSigma.Text = "1.4";
            // 
            // lbGaussSizeText
            // 
            this.lbGaussSizeText.AutoSize = true;
            this.lbGaussSizeText.Location = new System.Drawing.Point(6, 138);
            this.lbGaussSizeText.Name = "lbGaussSizeText";
            this.lbGaussSizeText.Size = new System.Drawing.Size(77, 13);
            this.lbGaussSizeText.TabIndex = 22;
            this.lbGaussSizeText.Text = "Gaussian Size:";
            // 
            // trbarGaussSize
            // 
            this.trbarGaussSize.LargeChange = 4;
            this.trbarGaussSize.Location = new System.Drawing.Point(6, 155);
            this.trbarGaussSize.Maximum = 21;
            this.trbarGaussSize.Minimum = 3;
            this.trbarGaussSize.Name = "trbarGaussSize";
            this.trbarGaussSize.Size = new System.Drawing.Size(218, 45);
            this.trbarGaussSize.SmallChange = 2;
            this.trbarGaussSize.TabIndex = 21;
            this.trbarGaussSize.TickFrequency = 2;
            this.trbarGaussSize.Value = 5;
            this.trbarGaussSize.ValueChanged += new System.EventHandler(this.TrbarGaussSizeValueChanged);
            // 
            // lbGaussSize
            // 
            this.lbGaussSize.AutoSize = true;
            this.lbGaussSize.Location = new System.Drawing.Point(98, 138);
            this.lbGaussSize.Name = "lbGaussSize";
            this.lbGaussSize.Size = new System.Drawing.Size(13, 13);
            this.lbGaussSize.TabIndex = 23;
            this.lbGaussSize.Text = "5";
            // 
            // lbLowThresholdText
            // 
            this.lbLowThresholdText.AutoSize = true;
            this.lbLowThresholdText.Location = new System.Drawing.Point(6, 73);
            this.lbLowThresholdText.Name = "lbLowThresholdText";
            this.lbLowThresholdText.Size = new System.Drawing.Size(30, 13);
            this.lbLowThresholdText.TabIndex = 19;
            this.lbLowThresholdText.Text = "Low:";
            // 
            // trbarLowThreshold
            // 
            this.trbarLowThreshold.Location = new System.Drawing.Point(6, 90);
            this.trbarLowThreshold.Maximum = 255;
            this.trbarLowThreshold.Name = "trbarLowThreshold";
            this.trbarLowThreshold.Size = new System.Drawing.Size(218, 45);
            this.trbarLowThreshold.TabIndex = 18;
            this.trbarLowThreshold.Value = 20;
            this.trbarLowThreshold.ValueChanged += new System.EventHandler(this.TrbarLowThresholdValueChanged);
            // 
            // lbLowThreshold
            // 
            this.lbLowThreshold.AutoSize = true;
            this.lbLowThreshold.Location = new System.Drawing.Point(98, 73);
            this.lbLowThreshold.Name = "lbLowThreshold";
            this.lbLowThreshold.Size = new System.Drawing.Size(19, 13);
            this.lbLowThreshold.TabIndex = 20;
            this.lbLowThreshold.Text = "20";
            // 
            // lbHighThresholdText
            // 
            this.lbHighThresholdText.AutoSize = true;
            this.lbHighThresholdText.Location = new System.Drawing.Point(6, 8);
            this.lbHighThresholdText.Name = "lbHighThresholdText";
            this.lbHighThresholdText.Size = new System.Drawing.Size(32, 13);
            this.lbHighThresholdText.TabIndex = 16;
            this.lbHighThresholdText.Text = "High:";
            // 
            // trbarHighThreshold
            // 
            this.trbarHighThreshold.Location = new System.Drawing.Point(6, 25);
            this.trbarHighThreshold.Maximum = 255;
            this.trbarHighThreshold.Name = "trbarHighThreshold";
            this.trbarHighThreshold.Size = new System.Drawing.Size(218, 45);
            this.trbarHighThreshold.TabIndex = 15;
            this.trbarHighThreshold.Value = 40;
            this.trbarHighThreshold.ValueChanged += new System.EventHandler(this.TrbarHighThresholdValueChanged);
            // 
            // lbHighThreshold
            // 
            this.lbHighThreshold.AutoSize = true;
            this.lbHighThreshold.Location = new System.Drawing.Point(98, 8);
            this.lbHighThreshold.Name = "lbHighThreshold";
            this.lbHighThreshold.Size = new System.Drawing.Size(19, 13);
            this.lbHighThreshold.TabIndex = 17;
            this.lbHighThreshold.Text = "40";
            // 
            // chboxDoDilatations
            // 
            this.chboxDoDilatations.AutoSize = true;
            this.chboxDoDilatations.Location = new System.Drawing.Point(6, 19);
            this.chboxDoDilatations.Name = "chboxDoDilatations";
            this.chboxDoDilatations.Size = new System.Drawing.Size(92, 17);
            this.chboxDoDilatations.TabIndex = 21;
            this.chboxDoDilatations.Text = "Do Dilatations";
            this.chboxDoDilatations.UseVisualStyleBackColor = true;
            this.chboxDoDilatations.CheckedChanged += new System.EventHandler(this.ChboxDoDilatationsCheckedChanged);
            // 
            // lbLineStrengthName
            // 
            this.lbLineStrengthName.AutoSize = true;
            this.lbLineStrengthName.Location = new System.Drawing.Point(121, 88);
            this.lbLineStrengthName.Name = "lbLineStrengthName";
            this.lbLineStrengthName.Size = new System.Drawing.Size(50, 13);
            this.lbLineStrengthName.TabIndex = 22;
            this.lbLineStrengthName.Text = "Strength:";
            // 
            // NSConGrapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 522);
            this.Controls.Add(this.Options);
            this.Name = "NSConGrapForm";
            this.Text = "ConTrapForm";
            this.Options.ResumeLayout(false);
            this.gbProcess.ResumeLayout(false);
            this.gbResults.ResumeLayout(false);
            this.gbResults.PerformLayout();
            this.tabctrlOptions.ResumeLayout(false);
            this.tabpgContourTracer.ResumeLayout(false);
            this.tabpgContourTracer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbarWeight)).EndInit();
            this.gbDrawModes.ResumeLayout(false);
            this.gbDrawModes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbarStrength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbarTraceRadius)).EndInit();
            this.tabpgCanny.ResumeLayout(false);
            this.tabpgCanny.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbarGaussSigma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbarGaussSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbarLowThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbarHighThreshold)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        /// <summary>Contains all filter specified property-GUI elements.</summary>
        public System.Windows.Forms.GroupBox Options;
        private System.Windows.Forms.GroupBox gbProcess;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.GroupBox gbResults;
        private System.Windows.Forms.Label lbContourNumbers;
        private System.Windows.Forms.Label lbContourNumbersText;
        private System.Windows.Forms.TabControl tabctrlOptions;
        private System.Windows.Forms.TabPage tabpgContourTracer;
        private System.Windows.Forms.Label lbWeightText;
        private System.Windows.Forms.TrackBar trbarWeight;
        private System.Windows.Forms.Label lbWeight;
        private System.Windows.Forms.GroupBox gbDrawModes;
        private System.Windows.Forms.TrackBar trbarStrength;
        private System.Windows.Forms.Label lbLineStrength;
        private System.Windows.Forms.CheckBox chboxDrawBorderLine;
        private System.Windows.Forms.CheckBox chboxDrawBorderRect;
        private System.Windows.Forms.Label lbTraceRadiusText;
        private System.Windows.Forms.TrackBar trbarTraceRadius;
        private System.Windows.Forms.Label lbTraceRadius;
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
        private System.Windows.Forms.CheckBox chboxDoDilatations;
        private System.Windows.Forms.Label lbLineStrengthName;
    }
}