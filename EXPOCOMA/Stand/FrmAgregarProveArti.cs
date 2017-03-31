using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EXPOCOMA.Stand
{
    public partial class FrmAgregarProveArti : Form
    {
        public String _CadenaConexion;
        public String _NumAlmacen;
        public String _CProve;
        public String _NomProve;

        public FrmAgregarProveArti()
        {
            InitializeComponent();
        }

        private void FrmAgregarProveArti_Load(object sender, EventArgs e)
        {

        }

        private void FrmAgregarProveArti_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmProveArti._frmProveArti.Enabled = true;
        }
    }
}
