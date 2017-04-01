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
        public DataTable _dtProveedor;

        funciones _funcion = new funciones();

        

        String Sesion = "ALEX  CARDENAS";
        String SesionLetra = "H";

        public FrmAgregarProveArti()
        {
            InitializeComponent();
        }

        private void FrmAgregarProveArti_Load(object sender, EventArgs e)
        {
            //_funcion._SQLCadenaConexion = _CadenaConexion;
            //_dtProveedor = _funcion.llenar_dt("dbf_proveedo", "id, ID_SUCURSALALM, C_PROVE, DESCRI, RESP_COMA,  C_PROVE2", "WHERE ID_SUCURSALALM = " + _NumAlmacen + " AND RESP_COMA = '"+ SesionLetra+"'", "ORDER BY C_PROVE");

            dgvProveedor.DataSource = _dtProveedor;

            //_dtProveedor.Columns.Add("+", typeof(Boolean));
            //for (int i = 0; i < _dtProveedor.Rows.Count; i++)
            //{
            //    _dtProveedor.Rows[i]["+"] = false;
            //}

            dgvProveedor.Columns["id"].Visible = false;
            dgvProveedor.Columns["ID_SUCURSALALM"].Visible = false;
            dgvProveedor.Columns["C_PROVE2"].Visible = false;
            dgvProveedor.Columns["PARTICIPA"].DisplayIndex = 0;
            dgvProveedor.Columns["PARTICIPA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvProveedor.Columns["PARTICIPA"].HeaderText = "+";
            dgvProveedor.Columns["C_PROVE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvProveedor.Columns["RESP_COMA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvProveedor.Columns["C_PROVE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvProveedor.DefaultCellStyle.SelectionBackColor = Properties.Settings.Default.filaSeleccion;
            dgvProveedor.AlternatingRowsDefaultCellStyle.BackColor = Properties.Settings.Default.filaAltern;

            txtClProve.Text = _CProve;
            txtNomProve.Text = _NomProve;
        }

        private void FrmAgregarProveArti_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmProveArti._frmProveArti.Enabled = true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
