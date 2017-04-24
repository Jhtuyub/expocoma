namespace EXPOCOMA.Stand
{
    partial class FrmImpMargenes
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
            this.btnArchiMarPre = new System.Windows.Forms.Button();
            this.btnSubirArchi = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.dgvProvexpo = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProvexpo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnArchiMarPre
            // 
            this.btnArchiMarPre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnArchiMarPre.Location = new System.Drawing.Point(12, 226);
            this.btnArchiMarPre.Name = "btnArchiMarPre";
            this.btnArchiMarPre.Size = new System.Drawing.Size(138, 68);
            this.btnArchiMarPre.TabIndex = 1;
            this.btnArchiMarPre.Text = "Archivo para Margen y Lista de Precios";
            this.btnArchiMarPre.UseVisualStyleBackColor = true;
            this.btnArchiMarPre.Click += new System.EventHandler(this.btnArchiMarPre_Click);
            // 
            // btnSubirArchi
            // 
            this.btnSubirArchi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSubirArchi.Location = new System.Drawing.Point(156, 226);
            this.btnSubirArchi.Name = "btnSubirArchi";
            this.btnSubirArchi.Size = new System.Drawing.Size(138, 68);
            this.btnSubirArchi.TabIndex = 2;
            this.btnSubirArchi.Text = "Subir Archivo con Margenes";
            this.btnSubirArchi.UseVisualStyleBackColor = true;
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.Location = new System.Drawing.Point(666, 334);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 29);
            this.btnSalir.TabIndex = 12;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            // 
            // dgvProvexpo
            // 
            this.dgvProvexpo.AllowUserToAddRows = false;
            this.dgvProvexpo.AllowUserToDeleteRows = false;
            this.dgvProvexpo.AllowUserToResizeRows = false;
            this.dgvProvexpo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProvexpo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProvexpo.BackgroundColor = System.Drawing.Color.White;
            this.dgvProvexpo.Location = new System.Drawing.Point(12, 12);
            this.dgvProvexpo.Name = "dgvProvexpo";
            this.dgvProvexpo.RowHeadersWidth = 4;
            this.dgvProvexpo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProvexpo.Size = new System.Drawing.Size(729, 208);
            this.dgvProvexpo.TabIndex = 23;
            this.dgvProvexpo.TabStop = false;
            this.dgvProvexpo.Tag = "";
            // 
            // FrmImpMargenes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 375);
            this.Controls.Add(this.dgvProvexpo);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnSubirArchi);
            this.Controls.Add(this.btnArchiMarPre);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmImpMargenes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmImpMargenes";
            this.Load += new System.EventHandler(this.FrmImpMargenes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProvexpo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnArchiMarPre;
        private System.Windows.Forms.Button btnSubirArchi;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.DataGridView dgvProvexpo;
    }
}