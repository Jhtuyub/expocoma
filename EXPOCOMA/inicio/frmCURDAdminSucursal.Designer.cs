namespace EXPOCOMA.inicio
{
    partial class frmCURDAdminSucursal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCURDAdminSucursal));
            this.label1 = new System.Windows.Forms.Label();
            this.txtAlmacen = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSucursal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServidor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtdbf = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUsudb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPassdb = new System.Windows.Forms.TextBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblMenAlmacen = new System.Windows.Forms.Label();
            this.txtDb = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAlmacenSql = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblMenAlmacenSql = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Numero Almacen:";
            // 
            // txtAlmacen
            // 
            this.txtAlmacen.Location = new System.Drawing.Point(11, 28);
            this.txtAlmacen.MaxLength = 3;
            this.txtAlmacen.Name = "txtAlmacen";
            this.txtAlmacen.Size = new System.Drawing.Size(357, 25);
            this.txtAlmacen.TabIndex = 2;
            this.txtAlmacen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAlmacen_KeyPress);
            this.txtAlmacen.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAlmacen_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Nombre Sucursal:";
            // 
            // txtSucursal
            // 
            this.txtSucursal.Location = new System.Drawing.Point(11, 124);
            this.txtSucursal.Name = "txtSucursal";
            this.txtSucursal.Size = new System.Drawing.Size(357, 25);
            this.txtSucursal.TabIndex = 6;
            this.txtSucursal.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSucursal_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Servidor sql:";
            // 
            // txtServidor
            // 
            this.txtServidor.Location = new System.Drawing.Point(11, 218);
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.Size = new System.Drawing.Size(357, 25);
            this.txtServidor.TabIndex = 10;
            this.txtServidor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtServidor_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Dirección dbf:";
            // 
            // txtdbf
            // 
            this.txtdbf.Location = new System.Drawing.Point(11, 171);
            this.txtdbf.Name = "txtdbf";
            this.txtdbf.Size = new System.Drawing.Size(357, 25);
            this.txtdbf.TabIndex = 8;
            this.txtdbf.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtfacturacion_KeyUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 245);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Usuario sql:";
            // 
            // txtUsudb
            // 
            this.txtUsudb.Location = new System.Drawing.Point(11, 265);
            this.txtUsudb.Name = "txtUsudb";
            this.txtUsudb.Size = new System.Drawing.Size(357, 25);
            this.txtUsudb.TabIndex = 12;
            this.txtUsudb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCopyFiles_KeyUp);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 293);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 17);
            this.label6.TabIndex = 13;
            this.label6.Text = "Contraseña sql:";
            // 
            // txtPassdb
            // 
            this.txtPassdb.Location = new System.Drawing.Point(11, 312);
            this.txtPassdb.Name = "txtPassdb";
            this.txtPassdb.PasswordChar = '*';
            this.txtPassdb.Size = new System.Drawing.Size(358, 25);
            this.txtPassdb.TabIndex = 14;
            this.txtPassdb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPath_KeyUp);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(289, 410);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(80, 29);
            this.btnSalir.TabIndex = 19;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(204, 410);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(80, 29);
            this.btnLimpiar.TabIndex = 18;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(118, 410);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(80, 29);
            this.btnGuardar.TabIndex = 17;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lblMenAlmacen
            // 
            this.lblMenAlmacen.AutoSize = true;
            this.lblMenAlmacen.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenAlmacen.Location = new System.Drawing.Point(127, 7);
            this.lblMenAlmacen.Name = "lblMenAlmacen";
            this.lblMenAlmacen.Size = new System.Drawing.Size(125, 19);
            this.lblMenAlmacen.TabIndex = 1;
            this.lblMenAlmacen.Tag = "mensaje";
            this.lblMenAlmacen.Text = "lblMenAlmacen";
            // 
            // txtDb
            // 
            this.txtDb.Location = new System.Drawing.Point(11, 360);
            this.txtDb.Name = "txtDb";
            this.txtDb.Size = new System.Drawing.Size(358, 25);
            this.txtDb.TabIndex = 16;
            this.txtDb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDb_KeyUp);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 340);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 17);
            this.label7.TabIndex = 15;
            this.label7.Text = "Base de datos:";
            // 
            // txtAlmacenSql
            // 
            this.txtAlmacenSql.Location = new System.Drawing.Point(11, 76);
            this.txtAlmacenSql.MaxLength = 3;
            this.txtAlmacenSql.Name = "txtAlmacenSql";
            this.txtAlmacenSql.Size = new System.Drawing.Size(357, 25);
            this.txtAlmacenSql.TabIndex = 4;
            this.txtAlmacenSql.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAlmacenSql_KeyPress);
            this.txtAlmacenSql.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAlmacenSql_KeyUp);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(158, 17);
            this.label8.TabIndex = 3;
            this.label8.Text = "Numero Almacen SQL:";
            // 
            // lblMenAlmacenSql
            // 
            this.lblMenAlmacenSql.AutoSize = true;
            this.lblMenAlmacenSql.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenAlmacenSql.Location = new System.Drawing.Point(161, 56);
            this.lblMenAlmacenSql.Name = "lblMenAlmacenSql";
            this.lblMenAlmacenSql.Size = new System.Drawing.Size(150, 19);
            this.lblMenAlmacenSql.TabIndex = 20;
            this.lblMenAlmacenSql.Tag = "mensaje";
            this.lblMenAlmacenSql.Text = "lblMenAlmacenSql";
            // 
            // frmCURDAdminSucursal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 451);
            this.Controls.Add(this.lblMenAlmacenSql);
            this.Controls.Add(this.txtAlmacenSql);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtDb);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblMenAlmacen);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtPassdb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtUsudb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtdbf);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtServidor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSucursal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAlmacen);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCURDAdminSucursal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCURDAdminSucursal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCURDAdminSucursal_FormClosing);
            this.Load += new System.EventHandler(this.frmCURDAdminSucursal_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmCURDAdminSucursal_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAlmacen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSucursal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtServidor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtdbf;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUsudb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPassdb;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblMenAlmacen;
        private System.Windows.Forms.TextBox txtDb;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAlmacenSql;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblMenAlmacenSql;
    }
}