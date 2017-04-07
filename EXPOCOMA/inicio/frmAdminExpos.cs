using EXPOCOMA.Funciones;
using EXPOCOMA.modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EXPOCOMA.inicio
{
    public partial class frmAdminExpos : Form
    {

        funcionesdblocal __funciones = new funcionesdblocal();

        public frmAdminExpos()
        {
            //__funciones._TIPObasedatos = "dbcompaq";
            InitializeComponent();

            
        }

        private void frmAdminExpos_Load(object sender, EventArgs e)
        {

           

            __funciones.cargar_datos(dgvExpos, "cat_expo");
            //___DGVDATOS.DataSource = dt;
            dgvExpos.Columns["id"].Visible = false;
            dgvExpos.DefaultCellStyle.SelectionBackColor = Properties.Settings.Default.filaSeleccion;
            dgvExpos.AlternatingRowsDefaultCellStyle.BackColor = Properties.Settings.Default.filaAltern;
        }

        private void frmAdminExpos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //__funciones.cargar_datos(dgvExpos, "cat_expo");
            frmCURDAdminExpos __frmCURDAdminExpos = new frmCURDAdminExpos();
            __frmCURDAdminExpos.___accion = "nuevo";
            __frmCURDAdminExpos.___idDato = 0;
            __frmCURDAdminExpos.Owner = this;
            __frmCURDAdminExpos.ShowDialog();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //__funciones.cargar_datos(dgvExpos, "cat_expo");
            frmCURDAdminExpos __frmCURDAdminExpos = new frmCURDAdminExpos();
            __frmCURDAdminExpos.___accion = "modificar";
            __frmCURDAdminExpos.___idDato = Convert.ToInt32(dgvExpos.CurrentRow.Cells[0].Value.ToString()); 
            __frmCURDAdminExpos.Owner = this;
            __frmCURDAdminExpos.ShowDialog();
        }

        private void dgvExpos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvExpos.Rows.Count > 0)
            {
                frmCURDAdminExpos __frmCURDAdminExpos = new frmCURDAdminExpos();
                __frmCURDAdminExpos.___accion = "modificar";
                __frmCURDAdminExpos.___idDato = Convert.ToInt32(dgvExpos.CurrentRow.Cells[0].Value.ToString());
                __frmCURDAdminExpos.Owner = this;
                __frmCURDAdminExpos.ShowDialog();
            }
            
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            __funciones.eliminar_datos("cat_expo", dgvExpos.CurrentRow.Cells[0].Value.ToString(), dgvExpos);

           
        }

        public void frmAdminExpos_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            Form parent = this.Owner;
            Control[] ctrl = parent.Controls.Find("dgvExpos", true);
            __funciones.cargar_datos((DataGridView)ctrl[0], "cat_expo");

            GC.Collect();

            

        }

        private void dgvExpos_Click(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(dgvExpos.CurrentRow.Cells[0].Value.ToString()) > 0)
            //{
            //    btnModificar.Enabled = true;
            //    btnEliminar.Enabled = true;
            //}
            //else
            //{
            //    btnModificar.Enabled = false;
            //    btnEliminar.Enabled = false;
            //}
            
        }

        private void dgvExpos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvExpos.Rows.Count>0)
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

        private void txtNombre_KeyUp(object sender, KeyEventArgs e)
        {
            __funciones.cargar_datos(dgvExpos, "cat_expo", "nombre", txtNombre.Text);
        }

        private void dgvExpos_MouseMove(object sender, MouseEventArgs e)
        {
            dgvExpos.Focus();
        }






        //private void dgvExpos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    btnModificar.Enabled = true;
        //    btnEliminar.Enabled = true;
        //}
    }
}
