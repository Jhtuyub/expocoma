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
    public partial class FrmImprimirInvita : Form
    {
        public String _CadenaConexion;
        funciones _funcion = new funciones();

        DataTable _dtSucursales;

        public FrmImprimirInvita()
        {
            InitializeComponent();
        }

        private void FrmImprimirInvita_Load(object sender, EventArgs e)
        {
            _funcion.icono(this);
            _funcion._SQLCadenaConexion = _CadenaConexion;
            _dtSucursales = _funcion.llenar_dt("tbl_sucursal", "ALMACEN, SUCURSAL" ,"", "ORDER BY anfitrion DESC");

            cBoxSucursal.DataSource = _dtSucursales;
            cBoxSucursal.ValueMember = "ALMACEN";//"valor";
            cBoxSucursal.DisplayMember = "SUCURSAL"; //"opcion";

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            String[] clvCliente;
            String clvNoCliente ="";
            String claves = txtClvClientes.Text.Trim();

            claves = claves.Replace("\r", "");
            clvCliente = claves.Split('\n');

            for (int i = 0; i < clvCliente.Count(); i++)
            {
                clvNoCliente += clvCliente[i].ToString()+ "\r\n";
            }

            txtNoImpre.Text = clvNoCliente;
        }
    }
}
