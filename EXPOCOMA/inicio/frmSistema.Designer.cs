namespace EXPOCOMA.inicio
{
    partial class frmSistema
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSistema));
            this.tabSistema = new System.Windows.Forms.TabControl();
            this.tpSistema = new System.Windows.Forms.TabPage();
            this.checkBCerrar = new System.Windows.Forms.CheckBox();
            this.tpdb = new System.Windows.Forms.TabPage();
            this.txtDblocal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabsql = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxOrigenDatos = new System.Windows.Forms.ComboBox();
            this.pbconexion = new System.Windows.Forms.ProgressBar();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServidor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabSistema.SuspendLayout();
            this.tpSistema.SuspendLayout();
            this.tpdb.SuspendLayout();
            this.tabsql.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabSistema
            // 
            this.tabSistema.Controls.Add(this.tpSistema);
            this.tabSistema.Controls.Add(this.tpdb);
            this.tabSistema.Controls.Add(this.tabsql);
            this.tabSistema.Location = new System.Drawing.Point(11, 11);
            this.tabSistema.Name = "tabSistema";
            this.tabSistema.SelectedIndex = 0;
            this.tabSistema.Size = new System.Drawing.Size(357, 332);
            this.tabSistema.TabIndex = 0;
            // 
            // tpSistema
            // 
            this.tpSistema.Controls.Add(this.checkBCerrar);
            this.tpSistema.Location = new System.Drawing.Point(4, 26);
            this.tpSistema.Name = "tpSistema";
            this.tpSistema.Padding = new System.Windows.Forms.Padding(3);
            this.tpSistema.Size = new System.Drawing.Size(349, 302);
            this.tpSistema.TabIndex = 1;
            this.tpSistema.Text = "Sistema";
            this.tpSistema.UseVisualStyleBackColor = true;
            // 
            // checkBCerrar
            // 
            this.checkBCerrar.AutoSize = true;
            this.checkBCerrar.Location = new System.Drawing.Point(5, 29);
            this.checkBCerrar.Name = "checkBCerrar";
            this.checkBCerrar.Size = new System.Drawing.Size(276, 21);
            this.checkBCerrar.TabIndex = 0;
            this.checkBCerrar.Text = "Cerrar la aplicación al cerrar FrmIndex";
            this.checkBCerrar.UseVisualStyleBackColor = true;
            this.checkBCerrar.CheckedChanged += new System.EventHandler(this.checkBCerrar_CheckedChanged);
            // 
            // tpdb
            // 
            this.tpdb.Controls.Add(this.txtDblocal);
            this.tpdb.Controls.Add(this.label1);
            this.tpdb.Location = new System.Drawing.Point(4, 25);
            this.tpdb.Name = "tpdb";
            this.tpdb.Padding = new System.Windows.Forms.Padding(3);
            this.tpdb.Size = new System.Drawing.Size(349, 303);
            this.tpdb.TabIndex = 0;
            this.tpdb.Text = "DB local";
            this.tpdb.UseVisualStyleBackColor = true;
            // 
            // txtDblocal
            // 
            this.txtDblocal.Enabled = false;
            this.txtDblocal.Location = new System.Drawing.Point(79, 41);
            this.txtDblocal.Name = "txtDblocal";
            this.txtDblocal.Size = new System.Drawing.Size(266, 25);
            this.txtDblocal.TabIndex = 1;
            this.txtDblocal.TextChanged += new System.EventHandler(this.txtDblocal_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "DB Local:";
            // 
            // tabsql
            // 
            this.tabsql.Controls.Add(this.label5);
            this.tabsql.Controls.Add(this.cbxOrigenDatos);
            this.tabsql.Controls.Add(this.pbconexion);
            this.tabsql.Controls.Add(this.lblMensaje);
            this.tabsql.Controls.Add(this.btnTest);
            this.tabsql.Controls.Add(this.txtContrasena);
            this.tabsql.Controls.Add(this.label4);
            this.tabsql.Controls.Add(this.txtUsuario);
            this.tabsql.Controls.Add(this.label3);
            this.tabsql.Controls.Add(this.txtServidor);
            this.tabsql.Controls.Add(this.label2);
            this.tabsql.Location = new System.Drawing.Point(4, 25);
            this.tabsql.Name = "tabsql";
            this.tabsql.Padding = new System.Windows.Forms.Padding(3);
            this.tabsql.Size = new System.Drawing.Size(349, 303);
            this.tabsql.TabIndex = 2;
            this.tabsql.Text = "SQL";
            this.tabsql.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Origen de datos";
            // 
            // cbxOrigenDatos
            // 
            this.cbxOrigenDatos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOrigenDatos.FormattingEnabled = true;
            this.cbxOrigenDatos.Location = new System.Drawing.Point(5, 23);
            this.cbxOrigenDatos.Name = "cbxOrigenDatos";
            this.cbxOrigenDatos.Size = new System.Drawing.Size(337, 25);
            this.cbxOrigenDatos.TabIndex = 1;
            this.cbxOrigenDatos.SelectedIndexChanged += new System.EventHandler(this.cbxOrigenDatos_SelectedIndexChanged);
            this.cbxOrigenDatos.SelectionChangeCommitted += new System.EventHandler(this.cbxOrigenDatos_SelectionChangeCommitted);
            // 
            // pbconexion
            // 
            this.pbconexion.Location = new System.Drawing.Point(5, 267);
            this.pbconexion.Name = "pbconexion";
            this.pbconexion.Size = new System.Drawing.Size(337, 22);
            this.pbconexion.TabIndex = 10;
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Location = new System.Drawing.Point(3, 246);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(76, 17);
            this.lblMensaje.TabIndex = 9;
            this.lblMensaje.Text = "lblMensaje";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(5, 204);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(80, 28);
            this.btnTest.TabIndex = 8;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtContrasena
            // 
            this.txtContrasena.Location = new System.Drawing.Point(5, 164);
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.PasswordChar = '*';
            this.txtContrasena.Size = new System.Drawing.Size(337, 25);
            this.txtContrasena.TabIndex = 7;
            this.txtContrasena.TextChanged += new System.EventHandler(this.txtContrasena_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Contraseña:";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(5, 117);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(337, 25);
            this.txtUsuario.TabIndex = 5;
            this.txtUsuario.TextChanged += new System.EventHandler(this.txtUsuario_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Usuario:";
            // 
            // txtServidor
            // 
            this.txtServidor.Location = new System.Drawing.Point(5, 70);
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.Size = new System.Drawing.Size(337, 25);
            this.txtServidor.TabIndex = 3;
            this.txtServidor.TextChanged += new System.EventHandler(this.txtServidor_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Servidor:";
            // 
            // frmSistema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 356);
            this.Controls.Add(this.tabSistema);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSistema";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSistema";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSistema_FormClosing);
            this.Load += new System.EventHandler(this.frmSistema_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmSistema_KeyUp);
            this.tabSistema.ResumeLayout(false);
            this.tpSistema.ResumeLayout(false);
            this.tpSistema.PerformLayout();
            this.tpdb.ResumeLayout(false);
            this.tpdb.PerformLayout();
            this.tabsql.ResumeLayout(false);
            this.tabsql.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabSistema;
        private System.Windows.Forms.TabPage tpdb;
        private System.Windows.Forms.TabPage tpSistema;
        private System.Windows.Forms.TextBox txtDblocal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabsql;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtServidor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.ProgressBar pbconexion;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.CheckBox checkBCerrar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxOrigenDatos;
    }
}