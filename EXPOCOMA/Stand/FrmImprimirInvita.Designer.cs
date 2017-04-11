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
            this.SuspendLayout();
            // 
            // txtClvClientes
            // 
            this.txtClvClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClvClientes.Location = new System.Drawing.Point(13, 60);
            this.txtClvClientes.Multiline = true;
            this.txtClvClientes.Name = "txtClvClientes";
            this.txtClvClientes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtClvClientes.Size = new System.Drawing.Size(181, 259);
            this.txtClvClientes.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sucursal:";
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImprimir.Location = new System.Drawing.Point(174, 325);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 29);
            this.btnImprimir.TabIndex = 2;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Clave Clientes:";
            // 
            // cBoxSucursal
            // 
            this.cBoxSucursal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBoxSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxSucursal.FormattingEnabled = true;
            this.cBoxSucursal.Location = new System.Drawing.Point(86, 12);
            this.cBoxSucursal.Name = "cBoxSucursal";
            this.cBoxSucursal.Size = new System.Drawing.Size(323, 25);
            this.cBoxSucursal.TabIndex = 4;
            // 
            // txtNoImpre
            // 
            this.txtNoImpre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNoImpre.Location = new System.Drawing.Point(228, 60);
            this.txtNoImpre.Multiline = true;
            this.txtNoImpre.Name = "txtNoImpre";
            this.txtNoImpre.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNoImpre.Size = new System.Drawing.Size(181, 259);
            this.txtNoImpre.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(225, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Clientes no impresos:";
            // 
            // FrmImprimirInvita
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 360);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNoImpre);
            this.Controls.Add(this.cBoxSucursal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtClvClientes);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "FrmImprimirInvita";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Imprimir Invitaciones - FrmImprimirInvita";
            this.Load += new System.EventHandler(this.FrmImprimirInvita_Load);
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
    }
}