namespace EXPOCOMA.Stand
{
    partial class FrmImporTablas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImporTablas));
            this.dgvSucursal = new System.Windows.Forms.DataGridView();
            this.checkBTodosSucursales = new System.Windows.Forms.CheckBox();
            this.dgvTablas = new System.Windows.Forms.DataGridView();
            this.checkbxDbfTodos = new System.Windows.Forms.CheckBox();
            this.checkbxSqlTodos = new System.Windows.Forms.CheckBox();
            this.btnImportar = new System.Windows.Forms.Button();
            this.btnsalir = new System.Windows.Forms.Button();
            this.barraProgreso = new System.Windows.Forms.ProgressBar();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.picbCargando = new System.Windows.Forms.PictureBox();
            this.tabConSucursales = new System.Windows.Forms.TabControl();
            this.tabpagSucursal = new System.Windows.Forms.TabPage();
            this.tabConTablas = new System.Windows.Forms.TabControl();
            this.tabPagTablas = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSucursal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTablas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbCargando)).BeginInit();
            this.tabConSucursales.SuspendLayout();
            this.tabpagSucursal.SuspendLayout();
            this.tabConTablas.SuspendLayout();
            this.tabPagTablas.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSucursal
            // 
            this.dgvSucursal.AllowUserToAddRows = false;
            this.dgvSucursal.AllowUserToDeleteRows = false;
            this.dgvSucursal.AllowUserToResizeRows = false;
            this.dgvSucursal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSucursal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSucursal.BackgroundColor = System.Drawing.Color.White;
            this.dgvSucursal.Location = new System.Drawing.Point(6, 33);
            this.dgvSucursal.Name = "dgvSucursal";
            this.dgvSucursal.RowHeadersWidth = 4;
            this.dgvSucursal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSucursal.Size = new System.Drawing.Size(866, 125);
            this.dgvSucursal.TabIndex = 9;
            this.dgvSucursal.TabStop = false;
            this.dgvSucursal.Tag = "";
            this.dgvSucursal.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSucursal_CellContentClick);
            // 
            // checkBTodosSucursales
            // 
            this.checkBTodosSucursales.AutoSize = true;
            this.checkBTodosSucursales.Location = new System.Drawing.Point(6, 6);
            this.checkBTodosSucursales.Name = "checkBTodosSucursales";
            this.checkBTodosSucursales.Size = new System.Drawing.Size(115, 21);
            this.checkBTodosSucursales.TabIndex = 10;
            this.checkBTodosSucursales.Text = "Marcar Todos";
            this.checkBTodosSucursales.UseVisualStyleBackColor = true;
            this.checkBTodosSucursales.Click += new System.EventHandler(this.checkBTodosSucursales_Click);
            // 
            // dgvTablas
            // 
            this.dgvTablas.AllowUserToAddRows = false;
            this.dgvTablas.AllowUserToDeleteRows = false;
            this.dgvTablas.AllowUserToResizeRows = false;
            this.dgvTablas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTablas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTablas.BackgroundColor = System.Drawing.Color.White;
            this.dgvTablas.Location = new System.Drawing.Point(5, 32);
            this.dgvTablas.Name = "dgvTablas";
            this.dgvTablas.RowHeadersWidth = 4;
            this.dgvTablas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTablas.Size = new System.Drawing.Size(868, 160);
            this.dgvTablas.TabIndex = 11;
            this.dgvTablas.TabStop = false;
            this.dgvTablas.Tag = "";
            this.dgvTablas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTablas_CellContentClick);
            // 
            // checkbxDbfTodos
            // 
            this.checkbxDbfTodos.AutoSize = true;
            this.checkbxDbfTodos.Location = new System.Drawing.Point(6, 6);
            this.checkbxDbfTodos.Name = "checkbxDbfTodos";
            this.checkbxDbfTodos.Size = new System.Drawing.Size(149, 21);
            this.checkbxDbfTodos.TabIndex = 12;
            this.checkbxDbfTodos.Text = "Marcar Todos DBF";
            this.checkbxDbfTodos.UseVisualStyleBackColor = true;
            this.checkbxDbfTodos.Click += new System.EventHandler(this.checkbxDbfTodos_Click);
            // 
            // checkbxSqlTodos
            // 
            this.checkbxSqlTodos.AutoSize = true;
            this.checkbxSqlTodos.Location = new System.Drawing.Point(161, 6);
            this.checkbxSqlTodos.Name = "checkbxSqlTodos";
            this.checkbxSqlTodos.Size = new System.Drawing.Size(149, 21);
            this.checkbxSqlTodos.TabIndex = 13;
            this.checkbxSqlTodos.Text = "Marcar Todos SQL";
            this.checkbxSqlTodos.UseVisualStyleBackColor = true;
            this.checkbxSqlTodos.Click += new System.EventHandler(this.checkbxSqlTodos_Click);
            // 
            // btnImportar
            // 
            this.btnImportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportar.Location = new System.Drawing.Point(732, 462);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(80, 29);
            this.btnImportar.TabIndex = 14;
            this.btnImportar.Text = "Importar";
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // btnsalir
            // 
            this.btnsalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnsalir.Location = new System.Drawing.Point(818, 462);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Size = new System.Drawing.Size(80, 29);
            this.btnsalir.TabIndex = 15;
            this.btnsalir.Text = "Salir";
            this.btnsalir.UseVisualStyleBackColor = true;
            this.btnsalir.Click += new System.EventHandler(this.btnsalir_Click);
            // 
            // barraProgreso
            // 
            this.barraProgreso.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barraProgreso.Location = new System.Drawing.Point(11, 462);
            this.barraProgreso.Name = "barraProgreso";
            this.barraProgreso.Size = new System.Drawing.Size(681, 28);
            this.barraProgreso.TabIndex = 16;
            // 
            // lblMensaje
            // 
            this.lblMensaje.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Location = new System.Drawing.Point(9, 443);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(20, 17);
            this.lblMensaje.TabIndex = 17;
            this.lblMensaje.Text = "...";
            // 
            // picbCargando
            // 
            this.picbCargando.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.picbCargando.Image = global::EXPOCOMA.Properties.Resources.loader;
            this.picbCargando.Location = new System.Drawing.Point(699, 462);
            this.picbCargando.Name = "picbCargando";
            this.picbCargando.Size = new System.Drawing.Size(27, 28);
            this.picbCargando.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbCargando.TabIndex = 18;
            this.picbCargando.TabStop = false;
            // 
            // tabConSucursales
            // 
            this.tabConSucursales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabConSucursales.Controls.Add(this.tabpagSucursal);
            this.tabConSucursales.Location = new System.Drawing.Point(12, 12);
            this.tabConSucursales.Name = "tabConSucursales";
            this.tabConSucursales.SelectedIndex = 0;
            this.tabConSucursales.Size = new System.Drawing.Size(886, 194);
            this.tabConSucursales.TabIndex = 19;
            // 
            // tabpagSucursal
            // 
            this.tabpagSucursal.Controls.Add(this.checkBTodosSucursales);
            this.tabpagSucursal.Controls.Add(this.dgvSucursal);
            this.tabpagSucursal.Location = new System.Drawing.Point(4, 26);
            this.tabpagSucursal.Name = "tabpagSucursal";
            this.tabpagSucursal.Padding = new System.Windows.Forms.Padding(3);
            this.tabpagSucursal.Size = new System.Drawing.Size(878, 164);
            this.tabpagSucursal.TabIndex = 0;
            this.tabpagSucursal.Text = "Sucursal";
            this.tabpagSucursal.UseVisualStyleBackColor = true;
            // 
            // tabConTablas
            // 
            this.tabConTablas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabConTablas.Controls.Add(this.tabPagTablas);
            this.tabConTablas.Location = new System.Drawing.Point(11, 212);
            this.tabConTablas.Name = "tabConTablas";
            this.tabConTablas.SelectedIndex = 0;
            this.tabConTablas.Size = new System.Drawing.Size(887, 228);
            this.tabConTablas.TabIndex = 11;
            // 
            // tabPagTablas
            // 
            this.tabPagTablas.Controls.Add(this.checkbxDbfTodos);
            this.tabPagTablas.Controls.Add(this.dgvTablas);
            this.tabPagTablas.Controls.Add(this.checkbxSqlTodos);
            this.tabPagTablas.Location = new System.Drawing.Point(4, 26);
            this.tabPagTablas.Name = "tabPagTablas";
            this.tabPagTablas.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagTablas.Size = new System.Drawing.Size(879, 198);
            this.tabPagTablas.TabIndex = 0;
            this.tabPagTablas.Text = "Tablas";
            this.tabPagTablas.UseVisualStyleBackColor = true;
            // 
            // FrmImporTablas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 503);
            this.Controls.Add(this.tabConTablas);
            this.Controls.Add(this.tabConSucursales);
            this.Controls.Add(this.picbCargando);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.barraProgreso);
            this.Controls.Add(this.btnsalir);
            this.Controls.Add(this.btnImportar);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "FrmImporTablas";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmImporTablas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmImporTablas_FormClosing);
            this.Load += new System.EventHandler(this.FrmImporTablas_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmImporTablas_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSucursal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTablas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbCargando)).EndInit();
            this.tabConSucursales.ResumeLayout(false);
            this.tabpagSucursal.ResumeLayout(false);
            this.tabpagSucursal.PerformLayout();
            this.tabConTablas.ResumeLayout(false);
            this.tabPagTablas.ResumeLayout(false);
            this.tabPagTablas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvSucursal;
        private System.Windows.Forms.CheckBox checkBTodosSucursales;
        private System.Windows.Forms.DataGridView dgvTablas;
        private System.Windows.Forms.CheckBox checkbxDbfTodos;
        private System.Windows.Forms.CheckBox checkbxSqlTodos;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Button btnsalir;
        private System.Windows.Forms.ProgressBar barraProgreso;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.PictureBox picbCargando;
        private System.Windows.Forms.TabControl tabConSucursales;
        private System.Windows.Forms.TabPage tabpagSucursal;
        private System.Windows.Forms.TabControl tabConTablas;
        private System.Windows.Forms.TabPage tabPagTablas;
    }
}