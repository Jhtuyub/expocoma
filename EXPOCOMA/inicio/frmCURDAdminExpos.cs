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
    public partial class frmCURDAdminExpos : Form
    {
        funcionesdblocal __funciones = new funcionesdblocal();
        
        //

        public string ___accion;
        private int _contador = 0;
        private string _validarDB = "";
        public int ___idDato;

        private Boolean flag = false;

        public frmCURDAdminExpos()
        {
            //__funciones._TIPObasedatos = "dbcompaq";
            InitializeComponent();
        }

        private void frmCURDAdminExpos_Load(object sender, EventArgs e)
        {
            __funciones.reiniciar_campos(this);
            __funciones.nombre_form(this, ___accion, "NUEVO EXPO - frmCURDAdminExpos", "MODIFICAR EXPO ID:" + " " + ___idDato + " - frmCURDAdminExpos");

            if (___accion=="modificar")
            {

                var _datos = __funciones.llenar_form("cat_expo", ___idDato);
                //var _sql = "SELECT * FROM cat_expo WHERE id = "+ ___idDato;
                //using (SqlCeConnection _con = dblocal.conexion())
                //{

                //    SqlCeDataAdapter myAdaptador = new SqlCeDataAdapter(_sql, _con);
                //    DataTable dt = new DataTable();
                //    myAdaptador.Fill(dt);
                txtNombre.Text = _datos.Rows[0]["nombre"].ToString();
                txtDB.Text = _datos.Rows[0]["db"].ToString();
                _validarDB = _datos.Rows[0]["db"].ToString();

                txtDB.Enabled = false;

                //}
            }
        }

        private void frmCURDAdminExpos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            //txtDB.Text = "dbe_" + txtNombre.Text.Replace(" ", "");
            //__funciones.key_campo(txtNombre, true, lblDBMensaje, txtDB, ___accion, _validarDB);
        }

        private void txtDB_TextChanged(object sender, EventArgs e)
        {
            //__funciones.key_campo(txtDB, true, lblDBMensaje, txtDB, ___accion, _validarDB);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string[,] _datos = {
                {"nombre", "db"},
                {txtNombre.Text, txtDB.Text},
                {"varchar", "varchar"}
            };

            //__funciones.validar_campo(this, _datos, "cat_expo", timMensaje, ___accion, ___idDato);
            __funciones.validar_campo(this, _datos, "cat_expo", ___accion, ___idDato);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            __funciones.limpiar_campos(txtNombre, this);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void frmCURDAdminExpos_FormClosing(object sender, FormClosingEventArgs e)
        {
            var _cerrando = __funciones.antes_cerrar(this);
            if (!_cerrando)
            {
                //Form parent = this.Owner;
                //Control[] ctrl = parent.Controls.Find("pbxImage", true);
                //ObtenerImagen(((PictureBox)ctrl[0]));
                //===============================================
                //frmAdminExpos adminExpo = new frmAdminExpos();
                //__funciones.cargar_datos((DataGridView)adminExpo.Controls["dgvExpos"], "cat_expo");
                //===============================================
                Form parent = this.Owner;
                Control[] ctrl = parent.Controls.Find("dgvExpos", true);
                __funciones.cargar_datos((DataGridView)ctrl[0], "cat_expo");
                GC.Collect();
            }
            e.Cancel = _cerrando;
        }

        private void txtNombre_KeyUp(object sender, KeyEventArgs e)
        {
            if (___accion=="nuevo")
            {
                txtDB.Text = "dbe_" + txtNombre.Text.Replace(" ", "");

                String[] _sql = { "cat_expo", "db", txtDB.Text, "varchar" };
                __funciones.key_campo(txtNombre, lblDBMensaje, ___accion, _validarDB, _sql);
                //__funciones.key_campo(txtNombre, true, lblDBMensaje, ___accion, _validarDB, _sql);
            }
 
        }

        private void txtDB_KeyUp(object sender, KeyEventArgs e)
        {
            String[] _sql = { "cat_expo", "db", txtDB.Text, "varchar" };
            __funciones.key_campo(txtDB, lblDBMensaje, ___accion, _validarDB, _sql);
            //__funciones.key_campo(txtDB, true, lblDBMensaje, ___accion, _validarDB, _sql);
        }
    }
}
