using EXPOCOMA.Funciones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EXPOCOMA.inicio
{
    public partial class frmAdminEmpresa : Form
    {
        funcionesdblocal _funciones = new funcionesdblocal();
        public frmAdminEmpresa()
        {
            //_funciones._TIPObasedatos = "dbcompaq";
            InitializeComponent();
        }

        private void frmAdminSucursal_Load(object sender, EventArgs e)
        {

            _funciones.icono(this);

            String[,] _cbdatos = {
                { "almacen", "sucursal","servidor","dbf","usuario","contrasena", "db" },
                { "Almacen", "Sucursal","Servidor","Dirección dbf ","Usuario","contraseña", "base de datos" }
            };
            _funciones.llenarCombobox(cbCampo, _cbdatos);
            //cbCampo.Items.Add("Lima");
            _funciones.cargar_datos(dgvEmpresa,"cat_sucursal");
            dgvEmpresa.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEmpresa.Columns["id"].Visible =false;
            dgvEmpresa.Columns["almacen"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvEmpresa.Columns["almacen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEmpresa.Columns["almacen"].HeaderText = "alm";
            dgvEmpresa.Columns["organization_id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvEmpresa.Columns["organization_id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEmpresa.Columns["organization_id"].HeaderText = "alm sql";
            dgvEmpresa.Columns["servidorsucu"].Visible = false;
            dgvEmpresa.Columns["ruta_baja"].Visible = false;
            dgvEmpresa.Columns["agen_baja"].Visible = false;
            //SUCURSAL
            dgvEmpresa.Columns["dbf"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvEmpresa.Columns["dbf"].HeaderText = "dir dbf";
            dgvEmpresa.Columns["contrasena"].Visible = false;
        }

        private void frmAdminSucursal_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmCURDAdminSucursal _CURDSucursal = new frmCURDAdminSucursal();
            //_CURDSucursal.Owner = this;
            _CURDSucursal.___accion = "nuevo";
            _CURDSucursal.___idDato = 0;
            _CURDSucursal._Form = "frmAdminEmpresa";
            _CURDSucursal.ShowDialog(this);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvEmpresa_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvEmpresa.Rows.Count > 0)
            {
                btnModificar.PerformClick();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            frmCURDAdminSucursal _CURDSucursal = new frmCURDAdminSucursal();
            _CURDSucursal.___accion = "modificar";
            _CURDSucursal._Form = "frmAdminEmpresa";
            _CURDSucursal.___idDato = Convert.ToInt32(dgvEmpresa.CurrentRow.Cells[0].Value.ToString());
            _CURDSucursal.ShowDialog(this);
        }

        private void dgvEmpresa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEmpresa.Rows.Count > 0)
            {
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
            }
            else
            {
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            _funciones.eliminar_datos("cat_sucursal", dgvEmpresa.CurrentRow.Cells["id"].Value.ToString(), dgvEmpresa);
        }

        private void txtNombre_KeyUp(object sender, KeyEventArgs e)
        {
            _funciones.cargar_datos(dgvEmpresa, "cat_sucursal", cbCampo.SelectedValue.ToString(), txtNombre.Text);
        }

        private void frmAdminEmpresa_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
        }

        private void dgvEmpresa_MouseMove(object sender, MouseEventArgs e)
        {
            dgvEmpresa.Focus();
        }
    }
}
