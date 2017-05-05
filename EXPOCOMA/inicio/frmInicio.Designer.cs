namespace EXPOCOMA.inicio
{
    partial class frmInicio
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.menInicio = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemArchivo = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemAdminExpo = new System.Windows.Forms.ToolStripMenuItem();
            this.administrarCatalogoEmpresasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemConfiguracion = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.txtExpo = new System.Windows.Forms.TextBox();
            this.dgvExpos = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stripPBEstatus = new System.Windows.Forms.ToolStripProgressBar();
            this.stripSLEstatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menInicio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpos)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(287, 261);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(80, 30);
            this.btnSalir.TabIndex = 3;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Enabled = false;
            this.btnAceptar.Location = new System.Drawing.Point(167, 261);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(80, 30);
            this.btnAceptar.TabIndex = 2;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "EXPO";
            // 
            // menInicio
            // 
            this.menInicio.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menInicio.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemArchivo,
            this.ToolStripMenuItemSalir});
            this.menInicio.Location = new System.Drawing.Point(0, 0);
            this.menInicio.Name = "menInicio";
            this.menInicio.Size = new System.Drawing.Size(526, 25);
            this.menInicio.TabIndex = 6;
            this.menInicio.Text = "menInicio";
            // 
            // ToolStripMenuItemArchivo
            // 
            this.ToolStripMenuItemArchivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemAdminExpo,
            this.administrarCatalogoEmpresasToolStripMenuItem,
            this.ToolStripMenuItemConfiguracion});
            this.ToolStripMenuItemArchivo.Name = "ToolStripMenuItemArchivo";
            this.ToolStripMenuItemArchivo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.ToolStripMenuItemArchivo.Size = new System.Drawing.Size(94, 21);
            this.ToolStripMenuItemArchivo.Text = "Administrar";
            // 
            // ToolStripMenuItemAdminExpo
            // 
            this.ToolStripMenuItemAdminExpo.Name = "ToolStripMenuItemAdminExpo";
            this.ToolStripMenuItemAdminExpo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
            this.ToolStripMenuItemAdminExpo.Size = new System.Drawing.Size(317, 22);
            this.ToolStripMenuItemAdminExpo.Text = "Administrar Expos";
            this.ToolStripMenuItemAdminExpo.Click += new System.EventHandler(this.ToolStripMenuItemAdminExpo_Click);
            // 
            // administrarCatalogoEmpresasToolStripMenuItem
            // 
            this.administrarCatalogoEmpresasToolStripMenuItem.Name = "administrarCatalogoEmpresasToolStripMenuItem";
            this.administrarCatalogoEmpresasToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.administrarCatalogoEmpresasToolStripMenuItem.Size = new System.Drawing.Size(317, 22);
            this.administrarCatalogoEmpresasToolStripMenuItem.Text = "Administrar Catalogo Sucursal";
            this.administrarCatalogoEmpresasToolStripMenuItem.Click += new System.EventHandler(this.administrarCatalogoEmpresasToolStripMenuItem_Click);
            // 
            // ToolStripMenuItemConfiguracion
            // 
            this.ToolStripMenuItemConfiguracion.Name = "ToolStripMenuItemConfiguracion";
            this.ToolStripMenuItemConfiguracion.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.ToolStripMenuItemConfiguracion.Size = new System.Drawing.Size(317, 22);
            this.ToolStripMenuItemConfiguracion.Text = "Configuración";
            this.ToolStripMenuItemConfiguracion.Click += new System.EventHandler(this.ToolStripMenuItemConfiguracion_Click);
            // 
            // ToolStripMenuItemSalir
            // 
            this.ToolStripMenuItemSalir.Name = "ToolStripMenuItemSalir";
            this.ToolStripMenuItemSalir.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.ToolStripMenuItemSalir.Size = new System.Drawing.Size(49, 21);
            this.ToolStripMenuItemSalir.Text = "Salir";
            this.ToolStripMenuItemSalir.Click += new System.EventHandler(this.ToolStripMenuItemSalir_Click);
            // 
            // txtExpo
            // 
            this.txtExpo.Location = new System.Drawing.Point(63, 30);
            this.txtExpo.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtExpo.Name = "txtExpo";
            this.txtExpo.Size = new System.Drawing.Size(452, 25);
            this.txtExpo.TabIndex = 0;
            this.txtExpo.TextChanged += new System.EventHandler(this.txtExpo_TextChanged);
            this.txtExpo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtExpo_KeyUp);
            // 
            // dgvExpos
            // 
            this.dgvExpos.AllowUserToAddRows = false;
            this.dgvExpos.AllowUserToDeleteRows = false;
            this.dgvExpos.AllowUserToResizeRows = false;
            this.dgvExpos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExpos.BackgroundColor = System.Drawing.Color.White;
            this.dgvExpos.Location = new System.Drawing.Point(10, 60);
            this.dgvExpos.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dgvExpos.Name = "dgvExpos";
            this.dgvExpos.ReadOnly = true;
            this.dgvExpos.RowHeadersWidth = 4;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.dgvExpos.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvExpos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExpos.Size = new System.Drawing.Size(505, 195);
            this.dgvExpos.TabIndex = 7;
            this.dgvExpos.TabStop = false;
            this.dgvExpos.Tag = "";
            this.dgvExpos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExpos_CellClick);
            this.dgvExpos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvExpos_KeyUp);
            this.dgvExpos.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvExpos_MouseDoubleClick);
            this.dgvExpos.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgvExpos_MouseMove);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripPBEstatus,
            this.stripSLEstatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 294);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(13, 0, 1, 0);
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(526, 23);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "stripEstatus";
            // 
            // stripPBEstatus
            // 
            this.stripPBEstatus.Name = "stripPBEstatus";
            this.stripPBEstatus.Size = new System.Drawing.Size(89, 17);
            // 
            // stripSLEstatus
            // 
            this.stripSLEstatus.Name = "stripSLEstatus";
            this.stripSLEstatus.Size = new System.Drawing.Size(88, 18);
            this.stripSLEstatus.Text = "stripSLEstatus";
            // 
            // frmInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 317);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dgvExpos);
            this.Controls.Add(this.txtExpo);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menInicio);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmInicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmInicio";
            this.Load += new System.EventHandler(this.frmInicio_Load);
            this.menInicio.ResumeLayout(false);
            this.menInicio.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpos)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menInicio;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemArchivo;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAdminExpo;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemConfiguracion;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSalir;
        private System.Windows.Forms.TextBox txtExpo;
        private System.Windows.Forms.DataGridView dgvExpos;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar stripPBEstatus;
        private System.Windows.Forms.ToolStripStatusLabel stripSLEstatus;
        private System.Windows.Forms.ToolStripMenuItem administrarCatalogoEmpresasToolStripMenuItem;
    }
}