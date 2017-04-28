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
            this.rvReporte = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rvReporte
            // 
            this.rvReporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvReporte.IsDocumentMapWidthFixed = true;
            this.rvReporte.Location = new System.Drawing.Point(0, 0);
            this.rvReporte.Name = "rvReporte";
            this.rvReporte.ShowBackButton = false;
            this.rvReporte.ShowRefreshButton = false;
            this.rvReporte.ShowStopButton = false;
            this.rvReporte.Size = new System.Drawing.Size(783, 441);
            this.rvReporte.TabIndex = 0;
            // 
            // FrmVistaPrevia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 441);
            this.Controls.Add(this.rvReporte);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmVistaPrevia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vista Previa - FrmVistaPrevia";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmVistaPrevia_FormClosing);
            this.Load += new System.EventHandler(this.FrmVistaPrevia_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvReporte;
    }
}