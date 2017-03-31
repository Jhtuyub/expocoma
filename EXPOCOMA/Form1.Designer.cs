namespace EXPOCOMA
{
    partial class frmIndex
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menIndex = new System.Windows.Forms.MenuStrip();
            this.MenIndexItemConfiguracion = new System.Windows.Forms.ToolStripMenuItem();
            this.menIndex.SuspendLayout();
            this.SuspendLayout();
            // 
            // menIndex
            // 
            this.menIndex.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menIndex.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenIndexItemConfiguracion});
            this.menIndex.Location = new System.Drawing.Point(0, 0);
            this.menIndex.Name = "menIndex";
            this.menIndex.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menIndex.Size = new System.Drawing.Size(807, 28);
            this.menIndex.TabIndex = 1;
            this.menIndex.Text = "menIndex";
            // 
            // MenIndexItemConfiguracion
            // 
            this.MenIndexItemConfiguracion.Name = "MenIndexItemConfiguracion";
            this.MenIndexItemConfiguracion.Size = new System.Drawing.Size(95, 23);
            this.MenIndexItemConfiguracion.Text = "Configuración";
            this.MenIndexItemConfiguracion.Click += new System.EventHandler(this.MenIndexItemConfiguracion_Click);
            // 
            // frmIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 483);
            this.Controls.Add(this.menIndex);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmIndex";
            this.Text = "EXPOCOMA - frmIndex";
            this.Load += new System.EventHandler(this.frmIndex_Load);
            this.menIndex.ResumeLayout(false);
            this.menIndex.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menIndex;
        private System.Windows.Forms.ToolStripMenuItem MenIndexItemConfiguracion;
    }
}

