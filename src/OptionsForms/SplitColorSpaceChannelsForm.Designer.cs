namespace Serotonin.OptionsForms
{
    partial class SplitColorSpaceChannelsForm
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
            this.tabctrlOptions = new System.Windows.Forms.TabControl();
            this.tabpgContourTracer = new System.Windows.Forms.TabPage();
            this.gbColorSpace = new System.Windows.Forms.GroupBox();
            this.comBoxColorSpace = new System.Windows.Forms.ComboBox();
            this.gbColorChannel = new System.Windows.Forms.GroupBox();
            this.rdBtnC = new System.Windows.Forms.RadioButton();
            this.rdBtnB = new System.Windows.Forms.RadioButton();
            this.rdBtnA = new System.Windows.Forms.RadioButton();
            this.Options.SuspendLayout();
            this.gbProcess.SuspendLayout();
            this.tabctrlOptions.SuspendLayout();
            this.tabpgContourTracer.SuspendLayout();
            this.gbColorSpace.SuspendLayout();
            this.gbColorChannel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Options
            // 
            this.Options.Controls.Add(this.gbProcess);
            this.Options.Controls.Add(this.tabctrlOptions);
            this.Options.Location = new System.Drawing.Point(12, 12);
            this.Options.Name = "Options";
            this.Options.Size = new System.Drawing.Size(250, 500);
            this.Options.TabIndex = 3;
            this.Options.TabStop = false;
            this.Options.Text = "Options";
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
            // tabctrlOptions
            // 
            this.tabctrlOptions.Controls.Add(this.tabpgContourTracer);
            this.tabctrlOptions.Location = new System.Drawing.Point(6, 19);
            this.tabctrlOptions.Name = "tabctrlOptions";
            this.tabctrlOptions.SelectedIndex = 0;
            this.tabctrlOptions.Size = new System.Drawing.Size(238, 325);
            this.tabctrlOptions.TabIndex = 0;
            // 
            // tabpgContourTracer
            // 
            this.tabpgContourTracer.BackColor = System.Drawing.Color.AliceBlue;
            this.tabpgContourTracer.Controls.Add(this.gbColorSpace);
            this.tabpgContourTracer.Controls.Add(this.gbColorChannel);
            this.tabpgContourTracer.Location = new System.Drawing.Point(4, 22);
            this.tabpgContourTracer.Name = "tabpgContourTracer";
            this.tabpgContourTracer.Padding = new System.Windows.Forms.Padding(3);
            this.tabpgContourTracer.Size = new System.Drawing.Size(230, 299);
            this.tabpgContourTracer.TabIndex = 1;
            this.tabpgContourTracer.Text = "Contour Tracer";
            // 
            // gbColorSpace
            // 
            this.gbColorSpace.Controls.Add(this.comBoxColorSpace);
            this.gbColorSpace.Location = new System.Drawing.Point(6, 6);
            this.gbColorSpace.Name = "gbColorSpace";
            this.gbColorSpace.Size = new System.Drawing.Size(218, 58);
            this.gbColorSpace.TabIndex = 16;
            this.gbColorSpace.TabStop = false;
            this.gbColorSpace.Text = "Color Spaces";
            // 
            // comBoxColorSpace
            // 
            this.comBoxColorSpace.FormattingEnabled = true;
            this.comBoxColorSpace.Items.AddRange(new object[] {
            "HSB",
            "HSL",
            "LAB",
            "LCH",
            "LUV",
            "RGB",
            "sRGB",
            "XYZ"});
            this.comBoxColorSpace.Location = new System.Drawing.Point(6, 19);
            this.comBoxColorSpace.Name = "comBoxColorSpace";
            this.comBoxColorSpace.Size = new System.Drawing.Size(121, 21);
            this.comBoxColorSpace.TabIndex = 20;
            this.comBoxColorSpace.Text = "HSB";
            this.comBoxColorSpace.SelectedIndexChanged += new System.EventHandler(this.ComBoxColorSpaceSelectedIndexChanged);
            // 
            // gbColorChannel
            // 
            this.gbColorChannel.Controls.Add(this.rdBtnC);
            this.gbColorChannel.Controls.Add(this.rdBtnB);
            this.gbColorChannel.Controls.Add(this.rdBtnA);
            this.gbColorChannel.Location = new System.Drawing.Point(6, 70);
            this.gbColorChannel.Name = "gbColorChannel";
            this.gbColorChannel.Size = new System.Drawing.Size(218, 94);
            this.gbColorChannel.TabIndex = 15;
            this.gbColorChannel.TabStop = false;
            this.gbColorChannel.Text = "Color Channel";
            // 
            // rdBtnC
            // 
            this.rdBtnC.AutoSize = true;
            this.rdBtnC.Location = new System.Drawing.Point(7, 66);
            this.rdBtnC.Name = "rdBtnC";
            this.rdBtnC.Size = new System.Drawing.Size(32, 17);
            this.rdBtnC.TabIndex = 2;
            this.rdBtnC.Text = "C";
            this.rdBtnC.UseVisualStyleBackColor = true;
            this.rdBtnC.CheckedChanged += new System.EventHandler(this.RdBtnCCheckedChanged);
            // 
            // rdBtnB
            // 
            this.rdBtnB.AutoSize = true;
            this.rdBtnB.Location = new System.Drawing.Point(7, 43);
            this.rdBtnB.Name = "rdBtnB";
            this.rdBtnB.Size = new System.Drawing.Size(32, 17);
            this.rdBtnB.TabIndex = 1;
            this.rdBtnB.Text = "B";
            this.rdBtnB.UseVisualStyleBackColor = true;
            this.rdBtnB.CheckedChanged += new System.EventHandler(this.RdBtnBCheckedChanged);
            // 
            // rdBtnA
            // 
            this.rdBtnA.AutoSize = true;
            this.rdBtnA.Checked = true;
            this.rdBtnA.Location = new System.Drawing.Point(7, 20);
            this.rdBtnA.Name = "rdBtnA";
            this.rdBtnA.Size = new System.Drawing.Size(32, 17);
            this.rdBtnA.TabIndex = 0;
            this.rdBtnA.TabStop = true;
            this.rdBtnA.Text = "A";
            this.rdBtnA.UseVisualStyleBackColor = true;
            this.rdBtnA.CheckedChanged += new System.EventHandler(this.RdBtnACheckedChanged);
            // 
            // NSSplitColorSpaceChannelsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 522);
            this.Controls.Add(this.Options);
            this.Name = "NSSplitColorSpaceChannelsForm";
            this.Text = "ContourTracerForm";
            this.Options.ResumeLayout(false);
            this.gbProcess.ResumeLayout(false);
            this.tabctrlOptions.ResumeLayout(false);
            this.tabpgContourTracer.ResumeLayout(false);
            this.gbColorSpace.ResumeLayout(false);
            this.gbColorChannel.ResumeLayout(false);
            this.gbColorChannel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        /// <summary>Contains all filter specified property-GUI elements.</summary>
        public System.Windows.Forms.GroupBox Options;
        private System.Windows.Forms.TabControl tabctrlOptions;
        private System.Windows.Forms.TabPage tabpgContourTracer;
        private System.Windows.Forms.GroupBox gbProcess;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.GroupBox gbColorChannel;
        private System.Windows.Forms.GroupBox gbColorSpace;
        private System.Windows.Forms.ComboBox comBoxColorSpace;
        private System.Windows.Forms.RadioButton rdBtnC;
        private System.Windows.Forms.RadioButton rdBtnB;
        private System.Windows.Forms.RadioButton rdBtnA;
    }
}