// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
namespace Serotonin.Controls
{
    public partial class ProgressBarForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressBarForm));
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.Resultlabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ProgressBar
            // 
            this.ProgressBar.BackColor = System.Drawing.Color.LavenderBlush;
            this.ProgressBar.Location = new System.Drawing.Point(29, 44);
            this.ProgressBar.Maximum = 10;
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(226, 23);
            this.ProgressBar.Step = 1;
            this.ProgressBar.TabIndex = 0;
            // 
            // Resultlabel
            // 
            this.Resultlabel.AutoSize = true;
            this.Resultlabel.Location = new System.Drawing.Point(13, 96);
            this.Resultlabel.Name = "Resultlabel";
            this.Resultlabel.Size = new System.Drawing.Size(59, 13);
            this.Resultlabel.TabIndex = 1;
            this.Resultlabel.Text = "Resultlabel";
            // 
            // ProgressBarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(284, 121);
            this.Controls.Add(this.Resultlabel);
            this.Controls.Add(this.ProgressBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProgressBarForm";
            this.Text = "ProgressBarForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        /// <summary>Illustrates the program progress.</summary>
        public System.Windows.Forms.ProgressBar ProgressBar;
        /// <summary>Gives feedback about the actual program progress.</summary>
        public System.Windows.Forms.Label Resultlabel;
    }
}