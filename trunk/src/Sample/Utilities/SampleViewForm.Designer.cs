#region License
/*
Copyright (c) 2010 ShanGuanDa etc.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
namespace Sample
{
    partial class SampleViewForm
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
            this.buttonSingleTextSample = new System.Windows.Forms.Button();
            this.groupBoxChain4Action = new System.Windows.Forms.GroupBox();
            this.buttonMultiActor = new System.Windows.Forms.Button();
            this.groupBoxChain4Action.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSingleTextSample
            // 
            this.buttonSingleTextSample.Location = new System.Drawing.Point(6, 20);
            this.buttonSingleTextSample.Name = "buttonSingleTextSample";
            this.buttonSingleTextSample.Size = new System.Drawing.Size(100, 23);
            this.buttonSingleTextSample.TabIndex = 0;
            this.buttonSingleTextSample.Text = "SingleText";
            this.buttonSingleTextSample.UseVisualStyleBackColor = true;
            this.buttonSingleTextSample.Click += new System.EventHandler(this.OnRunSingleText);
            // 
            // groupBoxChain4Action
            // 
            this.groupBoxChain4Action.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxChain4Action.Controls.Add(this.buttonMultiActor);
            this.groupBoxChain4Action.Controls.Add(this.buttonSingleTextSample);
            this.groupBoxChain4Action.Location = new System.Drawing.Point(12, 12);
            this.groupBoxChain4Action.Name = "groupBoxChain4Action";
            this.groupBoxChain4Action.Size = new System.Drawing.Size(224, 242);
            this.groupBoxChain4Action.TabIndex = 1;
            this.groupBoxChain4Action.TabStop = false;
            this.groupBoxChain4Action.Text = "Chain4Action";
            // 
            // buttonMultiActor
            // 
            this.buttonMultiActor.Location = new System.Drawing.Point(112, 20);
            this.buttonMultiActor.Name = "buttonMultiActor";
            this.buttonMultiActor.Size = new System.Drawing.Size(100, 23);
            this.buttonMultiActor.TabIndex = 1;
            this.buttonMultiActor.Text = "MultiActor";
            this.buttonMultiActor.UseVisualStyleBackColor = true;
            this.buttonMultiActor.Click += new System.EventHandler(this.OnRunMultiActor);
            // 
            // SampleViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 266);
            this.Controls.Add(this.groupBoxChain4Action);
            this.Name = "SampleViewForm";
            this.Text = "SampleViewForm";
            this.groupBoxChain4Action.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSingleTextSample;
        private System.Windows.Forms.GroupBox groupBoxChain4Action;
        private System.Windows.Forms.Button buttonMultiActor;
    }
}