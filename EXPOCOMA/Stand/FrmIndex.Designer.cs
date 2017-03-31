namespace EXPOCOMA.Stand
{
    partial class FrmIndex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIndex));
            this.menIndex = new System.Windows.Forms.MenuStrip();
            this.expoConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.empresasParticipantesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importacionDeTablasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proveedoresYArticulosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripPBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripSLMensaje = new System.Windows.Forms.ToolStripStatusLabel();
            this.menIndex.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menIndex
            // 
            this.menIndex.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menIndex.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.expoConfigToolStripMenuItem});
            this.menIndex.Location = new System.Drawing.Point(0, 0);
            this.menIndex.Name = "menIndex";
            this.menIndex.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.menIndex.Size = new System.Drawing.Size(693, 27);
            this.menIndex.TabIndex = 1;
            this.menIndex.Text = "menuStrip1";
            // 
            // expoConfigToolStripMenuItem
            // 
            this.expoConfigToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.empresasParticipantesToolStripMenuItem,
            this.importacionDeTablasToolStripMenuItem,
            this.proveedoresYArticulosToolStripMenuItem});
            this.expoConfigToolStripMenuItem.Name = "expoConfigToolStripMenuItem";
            this.expoConfigToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
            this.expoConfigToolStripMenuItem.Size = new System.Drawing.Size(96, 21);
            this.expoConfigToolStripMenuItem.Text = "Expo config";
            // 
            // empresasParticipantesToolStripMenuItem
            // 
            this.empresasParticipantesToolStripMenuItem.Name = "empresasParticipantesToolStripMenuItem";
            this.empresasParticipantesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.empresasParticipantesToolStripMenuItem.Size = new System.Drawing.Size(292, 22);
            this.empresasParticipantesToolStripMenuItem.Text = "1.Sucursales participantes";
            this.empresasParticipantesToolStripMenuItem.Click += new System.EventHandler(this.empresasParticipantesToolStripMenuItem_Click);
            // 
            // importacionDeTablasToolStripMenuItem
            // 
            this.importacionDeTablasToolStripMenuItem.Name = "importacionDeTablasToolStripMenuItem";
            this.importacionDeTablasToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.I)));
            this.importacionDeTablasToolStripMenuItem.Size = new System.Drawing.Size(292, 22);
            this.importacionDeTablasToolStripMenuItem.Text = "2.Importación de tablas";
            this.importacionDeTablasToolStripMenuItem.Click += new System.EventHandler(this.importacionDeTablasToolStripMenuItem_Click);
            // 
            // proveedoresYArticulosToolStripMenuItem
            // 
            this.proveedoresYArticulosToolStripMenuItem.Name = "proveedoresYArticulosToolStripMenuItem";
            this.proveedoresYArticulosToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.proveedoresYArticulosToolStripMenuItem.Size = new System.Drawing.Size(292, 22);
            this.proveedoresYArticulosToolStripMenuItem.Text = "3.Proveedores y Articulos";
            this.proveedoresYArticulosToolStripMenuItem.Click += new System.EventHandler(this.proveedoresYArticulosToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripPBar,
            this.toolStripSLMensaje});
            this.statusStrip1.Location = new System.Drawing.Point(0, 348);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(11, 0, 1, 0);
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(693, 23);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripPBar
            // 
            this.toolStripPBar.Name = "toolStripPBar";
            this.toolStripPBar.Size = new System.Drawing.Size(178, 17);
            // 
            // toolStripSLMensaje
            // 
            this.toolStripSLMensaje.Name = "toolStripSLMensaje";
            this.toolStripSLMensaje.Size = new System.Drawing.Size(108, 18);
            this.toolStripSLMensaje.Text = "toolStripSLMensaje";
            // 
            // FrmIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 371);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menIndex);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menIndex;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "FrmIndex";
            this.Text = "EXPOCOMA - FrmIndex";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmIndex_FormClosing);
            this.Load += new System.EventHandler(this.FrmIndex_Load);
            this.menIndex.ResumeLayout(false);
            this.menIndex.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menIndex;
        private System.Windows.Forms.ToolStripMenuItem expoConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem empresasParticipantesToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripPBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripSLMensaje;
        private System.Windows.Forms.ToolStripMenuItem importacionDeTablasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem proveedoresYArticulosToolStripMenuItem;
    }
}