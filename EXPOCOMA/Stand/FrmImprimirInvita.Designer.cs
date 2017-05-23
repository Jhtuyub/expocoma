namespace EXPOCOMA.Stand
{
    partial class FrmImprimirInvita
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
            this.txtClvClientes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cBoxSucursal = new System.Windows.Forms.ComboBox();
            this.txtNoImpre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.picbCargando = new System.Windows.Forms.PictureBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCliRutBaja = new System.Windows.Forms.TextBox();
            this.btnGenerarxls = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stripPBEstatus = new System.Windows.Forms.ToolStripProgressBar();
            this.stripSLEstatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.chRutabaja = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picbCargando)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtClvClientes
            // 
            this.txtClvClientes.Location = new System.Drawing.Point(13, 96);
            this.txtClvClientes.Multiline = true;
            this.txtClvClientes.Name = "txtClvClientes";
            this.txtClvClientes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtClvClientes.Size = new System.Drawing.Size(163, 259);
            this.txtClvClientes.TabIndex = 3;
            this.txtClvClientes.Click += new System.EventHandler(this.txtClvClientes_Click);
            this.txtClvClientes.Enter += new System.EventHandler(this.txtClvClientes_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sucursal:";
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImprimir.Location = new System.Drawing.Point(359, 390);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 29);
            this.btnImprimir.TabIndex = 12;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Clave Clientes:";
            // 
            // cBoxSucursal
            // 
            this.cBoxSucursal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBoxSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxSucursal.FormattingEnabled = true;
            this.cBoxSucursal.Location = new System.Drawing.Point(88, 9);
            this.cBoxSucursal.Name = "cBoxSucursal";
            this.cBoxSucursal.Size = new System.Drawing.Size(371, 25);
            this.cBoxSucursal.TabIndex = 1;
            // 
            // txtNoImpre
            // 
            this.txtNoImpre.Location = new System.Drawing.Point(183, 96);
            this.txtNoImpre.Multiline = true;
            this.txtNoImpre.Name = "txtNoImpre";
            this.txtNoImpre.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNoImpre.Size = new System.Drawing.Size(163, 259);
            this.txtNoImpre.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(180, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Clientes no encontratos:";
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.Location = new System.Drawing.Point(440, 390);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 29);
            this.btnSalir.TabIndex = 13;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // picbCargando
            // 
            this.picbCargando.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picbCargando.Location = new System.Drawing.Point(465, 12);
            this.picbCargando.Name = "picbCargando";
            this.picbCargando.Size = new System.Drawing.Size(50, 50);
            this.picbCargando.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbCargando.TabIndex = 19;
            this.picbCargando.TabStop = false;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLimpiar.Location = new System.Drawing.Point(12, 390);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 29);
            this.btnLimpiar.TabIndex = 9;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(352, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Clientes Ruta Baja:";
            // 
            // txtCliRutBaja
            // 
            this.txtCliRutBaja.Location = new System.Drawing.Point(352, 96);
            this.txtCliRutBaja.Multiline = true;
            this.txtCliRutBaja.Name = "txtCliRutBaja";
            this.txtCliRutBaja.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCliRutBaja.Size = new System.Drawing.Size(163, 259);
            this.txtCliRutBaja.TabIndex = 7;
            // 
            // btnGenerarxls
            // 
            this.btnGenerarxls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerarxls.Location = new System.Drawing.Point(352, 361);
            this.btnGenerarxls.Name = "btnGenerarxls";
            this.btnGenerarxls.Size = new System.Drawing.Size(163, 23);
            this.btnGenerarxls.TabIndex = 8;
            this.btnGenerarxls.Text = "Generar Excel";
            this.btnGenerarxls.UseVisualStyleBackColor = true;
            this.btnGenerarxls.Click += new System.EventHandler(this.btnGenerarxls_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnActualizar.Location = new System.Drawing.Point(93, 390);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(80, 29);
            this.btnActualizar.TabIndex = 10;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripPBEstatus,
            this.stripSLEstatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(527, 22);
            this.statusStrip1.TabIndex = 20;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stripPBEstatus
            // 
            this.stripPBEstatus.Name = "stripPBEstatus";
            this.stripPBEstatus.Size = new System.Drawing.Size(100, 16);
            // 
            // stripSLEstatus
            // 
            this.stripSLEstatus.Name = "stripSLEstatus";
            this.stripSLEstatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.stripSLEstatus.Size = new System.Drawing.Size(16, 17);
            this.stripSLEstatus.Text = "...";
            // 
            // chRutabaja
            // 
            this.chRutabaja.AutoSize = true;
            this.chRutabaja.Location = new System.Drawing.Point(179, 395);
            this.chRutabaja.Name = "chRutabaja";
            this.chRutabaja.Size = new System.Drawing.Size(151, 21);
            this.chRutabaja.TabIndex = 11;
            this.chRutabaja.Text = "Excluir ruta de baja";
            this.chRutabaja.UseVisualStyleBackColor = true;
            // 
            // FrmImprimirInvita
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 450);
            this.Controls.Add(this.picbCargando);
            this.Controls.Add(this.chRutabaja);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnGenerarxls);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCliRutBaja);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNoImpre);
            this.Controls.Add(this.cBoxSucursal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtClvClientes);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmImprimirInvita";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Imprimir Invitaciones - FrmImprimirInvita";
            this.Load += new System.EventHandler(this.FrmImprimirInvita_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picbCargando)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtClvClientes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cBoxSucursal;
        private System.Windows.Forms.TextBox txtNoImpre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.PictureBox picbCargando;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCliRutBaja;
        private System.Windows.Forms.Button btnGenerarxls;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar stripPBEstatus;
        private System.Windows.Forms.ToolStripStatusLabel stripSLEstatus;
        private System.Windows.Forms.CheckBox chRutabaja;
    }
}