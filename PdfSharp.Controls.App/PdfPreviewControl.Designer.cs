namespace PdfSharpControls
{
    partial class PdfPreviewControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PdfPreviewControl));
            this.acroPdfPreview = new AxAcroPDFLib.AxAcroPDF();
            ((System.ComponentModel.ISupportInitialize)(this.acroPdfPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // acroPdfPreview
            // 
            this.acroPdfPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.acroPdfPreview.Enabled = true;
            this.acroPdfPreview.Location = new System.Drawing.Point(0, 0);
            this.acroPdfPreview.Margin = new System.Windows.Forms.Padding(0);
            this.acroPdfPreview.Name = "acroPdfPreview";
            this.acroPdfPreview.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("acroPdfPreview.OcxState")));
            this.acroPdfPreview.Size = new System.Drawing.Size(293, 298);
            this.acroPdfPreview.TabIndex = 1;
            this.acroPdfPreview.OnError += new System.EventHandler(this.axAcroPDF1_OnError);
            // 
            // PdfPreviewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.acroPdfPreview);
            this.Name = "PdfPreviewControl";
            this.Size = new System.Drawing.Size(293, 298);
            ((System.ComponentModel.ISupportInitialize)(this.acroPdfPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxAcroPDFLib.AxAcroPDF acroPdfPreview;
    }
}
