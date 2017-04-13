namespace EXPOCOMA.reportes
{
    partial class FrmVistaPrevia
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
            this.crvVistaPrevia = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crvVistaPrevia
            // 
            this.crvVistaPrevia.ActiveViewIndex = -1;
            this.crvVistaPrevia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.crvVistaPrevia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvVistaPrevia.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvVistaPrevia.Location = new System.Drawing.Point(0, 0);
            this.crvVistaPrevia.Name = "crvVistaPrevia";
            this.crvVistaPrevia.Size = new System.Drawing.Size(725, 441);
            this.crvVistaPrevia.TabIndex = 0;
            // 
            // FrmVistaPrevia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 441);
            this.Controls.Add(this.crvVistaPrevia);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmVistaPrevia";
            this.Text = "Vista Previa - FrmVistaPrevia";
            this.Load += new System.EventHandler(this.FrmVistaPrevia_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvVistaPrevia;
    }
}