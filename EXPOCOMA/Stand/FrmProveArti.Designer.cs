﻿namespace EXPOCOMA.Stand
{
    partial class FrmProveArti
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
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.tabProveedor = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chBoxProvTodos = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboxOrdProve = new System.Windows.Forms.ComboBox();
            this.cboxBusProve = new System.Windows.Forms.ComboBox();
            this.txtBusProve = new System.Windows.Forms.TextBox();
            this.dgvProveedor = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cBoxSucursal = new System.Windows.Forms.ComboBox();
            this.tabArticulo = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnAgregarArti = new System.Windows.Forms.Button();
            this.chBoxArtiTodos = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cBoxBusArticulo = new System.Windows.Forms.ComboBox();
            this.txtBusArticulo = new System.Windows.Forms.TextBox();
            this.dgvArticulo = new System.Windows.Forms.DataGridView();
            this.picbCargando = new System.Windows.Forms.PictureBox();
            this.cBoxMostarProv = new System.Windows.Forms.CheckBox();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.stripPBEstatus = new System.Windows.Forms.ToolStripProgressBar();
            this.stripSLEstatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnReporte = new System.Windows.Forms.Button();
            this.tabProveedor.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedor)).BeginInit();
            this.tabArticulo.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticulo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbCargando)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.Location = new System.Drawing.Point(817, 468);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(80, 29);
            this.btnSalir.TabIndex = 15;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.Location = new System.Drawing.Point(731, 468);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(80, 29);
            this.btnGuardar.TabIndex = 14;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // tabProveedor
            // 
            this.tabProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabProveedor.Controls.Add(this.tabPage1);
            this.tabProveedor.Location = new System.Drawing.Point(12, 37);
            this.tabProveedor.Name = "tabProveedor";
            this.tabProveedor.SelectedIndex = 0;
            this.tabProveedor.Size = new System.Drawing.Size(885, 205);
            this.tabProveedor.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chBoxProvTodos);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.cboxOrdProve);
            this.tabPage1.Controls.Add(this.cboxBusProve);
            this.tabPage1.Controls.Add(this.txtBusProve);
            this.tabPage1.Controls.Add(this.dgvProveedor);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(877, 175);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Proveedor";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chBoxProvTodos
            // 
            this.chBoxProvTodos.AutoSize = true;
            this.chBoxProvTodos.Location = new System.Drawing.Point(656, 10);
            this.chBoxProvTodos.Name = "chBoxProvTodos";
            this.chBoxProvTodos.Size = new System.Drawing.Size(115, 21);
            this.chBoxProvTodos.TabIndex = 5;
            this.chBoxProvTodos.Text = "Marcar Todos";
            this.chBoxProvTodos.UseVisualStyleBackColor = true;
            this.chBoxProvTodos.Click += new System.EventHandler(this.chBoxProvTodos_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 28;
            this.label2.Text = "Buscar:";
            // 
            // cboxOrdProve
            // 
            this.cboxOrdProve.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxOrdProve.FormattingEnabled = true;
            this.cboxOrdProve.Location = new System.Drawing.Point(484, 6);
            this.cboxOrdProve.Name = "cboxOrdProve";
            this.cboxOrdProve.Size = new System.Drawing.Size(166, 25);
            this.cboxOrdProve.TabIndex = 4;
            this.cboxOrdProve.SelectedIndexChanged += new System.EventHandler(this.cboxOrdProve_SelectedIndexChanged);
            // 
            // cboxBusProve
            // 
            this.cboxBusProve.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxBusProve.FormattingEnabled = true;
            this.cboxBusProve.Location = new System.Drawing.Point(312, 6);
            this.cboxBusProve.Name = "cboxBusProve";
            this.cboxBusProve.Size = new System.Drawing.Size(166, 25);
            this.cboxBusProve.TabIndex = 3;
            this.cboxBusProve.SelectedIndexChanged += new System.EventHandler(this.cboxBusProve_SelectedIndexChanged);
            // 
            // txtBusProve
            // 
            this.txtBusProve.Location = new System.Drawing.Point(72, 6);
            this.txtBusProve.Name = "txtBusProve";
            this.txtBusProve.Size = new System.Drawing.Size(233, 25);
            this.txtBusProve.TabIndex = 2;
            this.txtBusProve.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBusProve_KeyUp);
            // 
            // dgvProveedor
            // 
            this.dgvProveedor.AllowUserToAddRows = false;
            this.dgvProveedor.AllowUserToDeleteRows = false;
            this.dgvProveedor.AllowUserToResizeRows = false;
            this.dgvProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProveedor.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProveedor.BackgroundColor = System.Drawing.Color.White;
            this.dgvProveedor.Location = new System.Drawing.Point(10, 37);
            this.dgvProveedor.Name = "dgvProveedor";
            this.dgvProveedor.RowHeadersWidth = 4;
            this.dgvProveedor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProveedor.Size = new System.Drawing.Size(861, 132);
            this.dgvProveedor.TabIndex = 22;
            this.dgvProveedor.TabStop = false;
            this.dgvProveedor.Tag = "";
            this.dgvProveedor.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProveedor_CellClick);
            this.dgvProveedor.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProveedor_CellContentClick);
            this.dgvProveedor.SelectionChanged += new System.EventHandler(this.dgvProveedor_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 17);
            this.label1.TabIndex = 29;
            this.label1.Text = "Sucursal:";
            // 
            // cBoxSucursal
            // 
            this.cBoxSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxSucursal.FormattingEnabled = true;
            this.cBoxSucursal.Location = new System.Drawing.Point(88, 6);
            this.cBoxSucursal.Name = "cBoxSucursal";
            this.cBoxSucursal.Size = new System.Drawing.Size(406, 25);
            this.cBoxSucursal.TabIndex = 0;
            this.cBoxSucursal.SelectedIndexChanged += new System.EventHandler(this.cBoxSucursal_SelectedIndexChanged);
            // 
            // tabArticulo
            // 
            this.tabArticulo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabArticulo.Controls.Add(this.tabPage2);
            this.tabArticulo.Location = new System.Drawing.Point(12, 248);
            this.tabArticulo.Name = "tabArticulo";
            this.tabArticulo.SelectedIndex = 0;
            this.tabArticulo.Size = new System.Drawing.Size(885, 214);
            this.tabArticulo.TabIndex = 6;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnAgregarArti);
            this.tabPage2.Controls.Add(this.chBoxArtiTodos);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.cBoxBusArticulo);
            this.tabPage2.Controls.Add(this.txtBusArticulo);
            this.tabPage2.Controls.Add(this.dgvArticulo);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(877, 184);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Articulo";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnAgregarArti
            // 
            this.btnAgregarArti.Enabled = false;
            this.btnAgregarArti.Location = new System.Drawing.Point(484, 4);
            this.btnAgregarArti.Name = "btnAgregarArti";
            this.btnAgregarArti.Size = new System.Drawing.Size(128, 29);
            this.btnAgregarArti.TabIndex = 9;
            this.btnAgregarArti.Text = "Agregar Articulos";
            this.btnAgregarArti.UseVisualStyleBackColor = true;
            this.btnAgregarArti.Click += new System.EventHandler(this.btnAgregarArti_Click);
            // 
            // chBoxArtiTodos
            // 
            this.chBoxArtiTodos.AutoSize = true;
            this.chBoxArtiTodos.Location = new System.Drawing.Point(618, 10);
            this.chBoxArtiTodos.Name = "chBoxArtiTodos";
            this.chBoxArtiTodos.Size = new System.Drawing.Size(115, 21);
            this.chBoxArtiTodos.TabIndex = 10;
            this.chBoxArtiTodos.Text = "Marcar Todos";
            this.chBoxArtiTodos.UseVisualStyleBackColor = true;
            this.chBoxArtiTodos.Click += new System.EventHandler(this.chBoxArtiTodos_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 28;
            this.label3.Text = "Buscar:";
            // 
            // cBoxBusArticulo
            // 
            this.cBoxBusArticulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxBusArticulo.FormattingEnabled = true;
            this.cBoxBusArticulo.Location = new System.Drawing.Point(312, 6);
            this.cBoxBusArticulo.Name = "cBoxBusArticulo";
            this.cBoxBusArticulo.Size = new System.Drawing.Size(166, 25);
            this.cBoxBusArticulo.TabIndex = 8;
            this.cBoxBusArticulo.SelectedIndexChanged += new System.EventHandler(this.cBoxBusArticulo_SelectedIndexChanged);
            // 
            // txtBusArticulo
            // 
            this.txtBusArticulo.Location = new System.Drawing.Point(72, 6);
            this.txtBusArticulo.Name = "txtBusArticulo";
            this.txtBusArticulo.Size = new System.Drawing.Size(233, 25);
            this.txtBusArticulo.TabIndex = 7;
            this.txtBusArticulo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBusArticulo_KeyUp);
            // 
            // dgvArticulo
            // 
            this.dgvArticulo.AllowUserToAddRows = false;
            this.dgvArticulo.AllowUserToDeleteRows = false;
            this.dgvArticulo.AllowUserToResizeRows = false;
            this.dgvArticulo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvArticulo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvArticulo.BackgroundColor = System.Drawing.Color.White;
            this.dgvArticulo.Location = new System.Drawing.Point(10, 37);
            this.dgvArticulo.Name = "dgvArticulo";
            this.dgvArticulo.RowHeadersWidth = 4;
            this.dgvArticulo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvArticulo.Size = new System.Drawing.Size(861, 141);
            this.dgvArticulo.TabIndex = 22;
            this.dgvArticulo.TabStop = false;
            this.dgvArticulo.Tag = "";
            this.dgvArticulo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvArticulo_CellContentClick);
            // 
            // picbCargando
            // 
            this.picbCargando.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picbCargando.Location = new System.Drawing.Point(843, 6);
            this.picbCargando.Name = "picbCargando";
            this.picbCargando.Size = new System.Drawing.Size(50, 50);
            this.picbCargando.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbCargando.TabIndex = 31;
            this.picbCargando.TabStop = false;
            // 
            // cBoxMostarProv
            // 
            this.cBoxMostarProv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cBoxMostarProv.AutoSize = true;
            this.cBoxMostarProv.Location = new System.Drawing.Point(530, 473);
            this.cBoxMostarProv.Name = "cBoxMostarProv";
            this.cBoxMostarProv.Size = new System.Drawing.Size(197, 21);
            this.cBoxMostarProv.TabIndex = 13;
            this.cBoxMostarProv.Text = "Mostrar Prov. Sin Articulos";
            this.cBoxMostarProv.UseVisualStyleBackColor = true;
            this.cBoxMostarProv.Visible = false;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnActualizar.Location = new System.Drawing.Point(12, 468);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(80, 29);
            this.btnActualizar.TabIndex = 11;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripPBEstatus,
            this.stripSLEstatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 500);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip.Size = new System.Drawing.Size(909, 22);
            this.statusStrip.TabIndex = 32;
            this.statusStrip.Text = "statusStrip1";
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
            // btnReporte
            // 
            this.btnReporte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReporte.Location = new System.Drawing.Point(98, 468);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Size = new System.Drawing.Size(80, 29);
            this.btnReporte.TabIndex = 33;
            this.btnReporte.Text = "Reporte";
            this.btnReporte.UseVisualStyleBackColor = true;
            this.btnReporte.Click += new System.EventHandler(this.btnReporte_Click);
            // 
            // FrmProveArti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 522);
            this.Controls.Add(this.btnReporte);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.cBoxMostarProv);
            this.Controls.Add(this.picbCargando);
            this.Controls.Add(this.tabArticulo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cBoxSucursal);
            this.Controls.Add(this.tabProveedor);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnSalir);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "FrmProveArti";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proveedores y Articulos - FrmProveArti";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmProveArti_FormClosing);
            this.Load += new System.EventHandler(this.FrmProveArti_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmProveArti_KeyUp);
            this.tabProveedor.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedor)).EndInit();
            this.tabArticulo.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticulo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbCargando)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TabControl tabProveedor;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboxOrdProve;
        private System.Windows.Forms.ComboBox cboxBusProve;
        private System.Windows.Forms.TextBox txtBusProve;
        private System.Windows.Forms.DataGridView dgvProveedor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cBoxSucursal;
        private System.Windows.Forms.TabControl tabArticulo;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cBoxBusArticulo;
        private System.Windows.Forms.TextBox txtBusArticulo;
        private System.Windows.Forms.DataGridView dgvArticulo;
        private System.Windows.Forms.CheckBox chBoxProvTodos;
        private System.Windows.Forms.CheckBox chBoxArtiTodos;
        private System.Windows.Forms.PictureBox picbCargando;
        private System.Windows.Forms.Button btnAgregarArti;
        private System.Windows.Forms.CheckBox cBoxMostarProv;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar stripPBEstatus;
        private System.Windows.Forms.ToolStripStatusLabel stripSLEstatus;
        private System.Windows.Forms.Button btnReporte;
    }
}