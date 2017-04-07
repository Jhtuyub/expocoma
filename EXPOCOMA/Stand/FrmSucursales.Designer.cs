namespace EXPOCOMA.Stand
{
    partial class FrmSucursales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSucursales));
            this.dgvCatEmpresa = new System.Windows.Forms.DataGridView();
            this.dgvTblEmpresa = new System.Windows.Forms.DataGridView();
            this.menuSucursal = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.altaSucursalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPasar = new System.Windows.Forms.Button();
            this.btnRegre = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.tabCatSucursal = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabtblSucursal = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCatEmpresa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTblEmpresa)).BeginInit();
            this.menuSucursal.SuspendLayout();
            this.tabCatSucursal.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabtblSucursal.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvCatEmpresa
            // 
            this.dgvCatEmpresa.AllowUserToAddRows = false;
            this.dgvCatEmpresa.AllowUserToDeleteRows = false;
            this.dgvCatEmpresa.AllowUserToResizeRows = false;
            this.dgvCatEmpresa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCatEmpresa.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCatEmpresa.BackgroundColor = System.Drawing.Color.White;
            this.dgvCatEmpresa.Location = new System.Drawing.Point(6, 41);
            this.dgvCatEmpresa.Name = "dgvCatEmpresa";
            this.dgvCatEmpresa.ReadOnly = true;
            this.dgvCatEmpresa.RowHeadersWidth = 4;
            this.dgvCatEmpresa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCatEmpresa.Size = new System.Drawing.Size(865, 128);
            this.dgvCatEmpresa.TabIndex = 6;
            this.dgvCatEmpresa.TabStop = false;
            this.dgvCatEmpresa.Tag = "";
            this.dgvCatEmpresa.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCatEmpresa_CellClick);
            this.dgvCatEmpresa.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCatEmpresa_CellDoubleClick);
            this.dgvCatEmpresa.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgvCatEmpresa_MouseMove);
            // 
            // dgvTblEmpresa
            // 
            this.dgvTblEmpresa.AllowUserToAddRows = false;
            this.dgvTblEmpresa.AllowUserToDeleteRows = false;
            this.dgvTblEmpresa.AllowUserToResizeRows = false;
            this.dgvTblEmpresa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTblEmpresa.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTblEmpresa.BackgroundColor = System.Drawing.Color.White;
            this.dgvTblEmpresa.Location = new System.Drawing.Point(6, 41);
            this.dgvTblEmpresa.Name = "dgvTblEmpresa";
            this.dgvTblEmpresa.RowHeadersWidth = 4;
            this.dgvTblEmpresa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTblEmpresa.Size = new System.Drawing.Size(865, 141);
            this.dgvTblEmpresa.TabIndex = 8;
            this.dgvTblEmpresa.TabStop = false;
            this.dgvTblEmpresa.Tag = "";
            this.dgvTblEmpresa.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTblEmpresa_CellClick);
            this.dgvTblEmpresa.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTblEmpresa_CellDoubleClick);
            this.dgvTblEmpresa.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgvTblEmpresa_MouseMove);
            // 
            // menuSucursal
            // 
            this.menuSucursal.AllowMerge = false;
            this.menuSucursal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem});
            this.menuSucursal.Location = new System.Drawing.Point(0, 0);
            this.menuSucursal.Name = "menuSucursal";
            this.menuSucursal.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuSucursal.Size = new System.Drawing.Size(909, 24);
            this.menuSucursal.TabIndex = 9;
            this.menuSucursal.Text = "menuSucursal";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.altaSucursalToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // altaSucursalToolStripMenuItem
            // 
            this.altaSucursalToolStripMenuItem.Name = "altaSucursalToolStripMenuItem";
            this.altaSucursalToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.altaSucursalToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.altaSucursalToolStripMenuItem.Text = "Alta Sucursal";
            this.altaSucursalToolStripMenuItem.Click += new System.EventHandler(this.altaSucursalToolStripMenuItem_Click);
            // 
            // btnPasar
            // 
            this.btnPasar.Location = new System.Drawing.Point(6, 6);
            this.btnPasar.Name = "btnPasar";
            this.btnPasar.Size = new System.Drawing.Size(80, 29);
            this.btnPasar.TabIndex = 0;
            this.btnPasar.Text = ">";
            this.btnPasar.UseVisualStyleBackColor = true;
            this.btnPasar.Click += new System.EventHandler(this.btnPasar_Click);
            // 
            // btnRegre
            // 
            this.btnRegre.Location = new System.Drawing.Point(6, 6);
            this.btnRegre.Name = "btnRegre";
            this.btnRegre.Size = new System.Drawing.Size(80, 29);
            this.btnRegre.TabIndex = 1;
            this.btnRegre.Text = "<";
            this.btnRegre.UseVisualStyleBackColor = true;
            this.btnRegre.Click += new System.EventHandler(this.btnRegre_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.Location = new System.Drawing.Point(817, 462);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(80, 29);
            this.btnSalir.TabIndex = 3;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.Location = new System.Drawing.Point(732, 462);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(80, 29);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // tabCatSucursal
            // 
            this.tabCatSucursal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCatSucursal.Controls.Add(this.tabPage1);
            this.tabCatSucursal.Location = new System.Drawing.Point(12, 27);
            this.tabCatSucursal.Name = "tabCatSucursal";
            this.tabCatSucursal.SelectedIndex = 0;
            this.tabCatSucursal.Size = new System.Drawing.Size(885, 205);
            this.tabCatSucursal.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvCatEmpresa);
            this.tabPage1.Controls.Add(this.btnPasar);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(877, 175);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Catalago Sucursal";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabtblSucursal
            // 
            this.tabtblSucursal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabtblSucursal.Controls.Add(this.tabPage2);
            this.tabtblSucursal.Location = new System.Drawing.Point(12, 238);
            this.tabtblSucursal.Name = "tabtblSucursal";
            this.tabtblSucursal.SelectedIndex = 0;
            this.tabtblSucursal.Size = new System.Drawing.Size(885, 218);
            this.tabtblSucursal.TabIndex = 11;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnRegre);
            this.tabPage2.Controls.Add(this.dgvTblEmpresa);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(877, 188);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Expo Sucursal";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // FrmSucursales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 503);
            this.Controls.Add(this.tabtblSucursal);
            this.Controls.Add(this.tabCatSucursal);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.menuSucursal);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuSucursal;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "FrmSucursales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sucursales Participantes - FrmSucursales";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSucursales_FormClosing);
            this.Load += new System.EventHandler(this.FrmSucursales_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmSucursales_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCatEmpresa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTblEmpresa)).EndInit();
            this.menuSucursal.ResumeLayout(false);
            this.menuSucursal.PerformLayout();
            this.tabCatSucursal.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabtblSucursal.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.DataGridView dgvCatEmpresa;
        private System.Windows.Forms.DataGridView dgvTblEmpresa;
        private System.Windows.Forms.MenuStrip menuSucursal;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem altaSucursalToolStripMenuItem;
        private System.Windows.Forms.Button btnPasar;
        private System.Windows.Forms.Button btnRegre;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TabControl tabCatSucursal;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabtblSucursal;
        private System.Windows.Forms.TabPage tabPage2;
    }
}