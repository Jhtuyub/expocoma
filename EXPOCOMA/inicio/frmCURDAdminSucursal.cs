using EXPOCOMA.Funciones;
using EXPOCOMA.Stand;
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
    public partial class frmCURDAdminSucursal : Form
    {
        funcionesdblocal _funcion = new funcionesdblocal();
        public string ___accion;
        public int ___idDato;
        public DataTable ___dtTbl;
        private string _validarAlmacen = ""; 
            private string _validarAlmacenSql = "";

        public String _Form;
        public frmCURDAdminSucursal()
        {
            //_funcion._TIPObasedatos = "dbcompaq";
            InitializeComponent();
        }

        private void frmCURDAdminSucursal_Load(object sender, EventArgs e)
        {
            _funcion.icono(this);

            lblMenAlmacen.Text = "";

            _funcion.reiniciar_campos(this);
            _funcion.nombre_form(this, ___accion, "NUEVO EMPRESA - frmCURDAdminSucursal", "MODIFICAR EMPRESA ID:" + " " + ___idDato + " - frmCURDAdminSucursal");

            if (___accion == "modificar")
            {

                var _datos = _funcion.llenar_form("cat_sucursal", ___idDato);
                txtAlmacen.Text = _datos.Rows[0]["almacen"].ToString();
                txtAlmacenSql.Text = _datos.Rows[0]["organization_id"].ToString();
                txtSucursal.Text = _datos.Rows[0]["sucursal"].ToString();
                txtServidor.Text = _datos.Rows[0]["servidor"].ToString();
                txtdbf.Text = _datos.Rows[0]["dbf"].ToString();
                txtUsudb.Text = _datos.Rows[0]["usuario"].ToString();
                txtPassdb.Text = _datos.Rows[0]["contrasena"].ToString();
                txtDb.Text = _datos.Rows[0]["db"].ToString();
                txtRutaBaja.Text = _datos.Rows[0]["ruta_baja"].ToString();
                txtAgenBaja.Text = _datos.Rows[0]["agen_baja"].ToString();

                _validarAlmacen = _datos.Rows[0]["almacen"].ToString();
                _validarAlmacenSql = _datos.Rows[0]["organization_id"].ToString();


                //txtNombre.Text = _datos.Rows[0]["nombre"].ToString();
                //txtDB.Text = _datos.Rows[0]["db"].ToString();
                //_validarDB = _datos.Rows[0]["db"].ToString();

                //txtDB.Enabled = false;

                //}
            }
        }

        private void frmCURDAdminSucursal_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string[,] _datos = {
                {"almacen","organization_id", "sucursal", "servidorsucu","servidor","dbf","usuario","contrasena", "db", "ruta_baja","agen_baja"},
                {txtAlmacen.Text, txtAlmacenSql.Text, txtSucursal.Text,txtServSucu.Text, txtServidor.Text,txtdbf.Text,txtUsudb.Text,txtPassdb.Text, txtDb.Text, txtRutaBaja.Text, txtAgenBaja.Text},
                {"varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar","varchar", "varchar","varchar"}
            };

            _funcion.validar_campo(this, _datos, "cat_sucursal", ___accion, ___idDato);

            //_funcion.validar_campo(this, _datos, "cat_empresa", timMensaje, ___accion, ___idDato);
        }

        private void txtAlmacen_KeyUp(object sender, KeyEventArgs e)
        {

            //if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            //    e.Handled = false;
            //else
            //    e.Handled = true;
         
            String[] datosTabla = { "cat_sucursal", "almacen", txtAlmacen.Text, "varchar" };
            _funcion.key_campo(txtAlmacen, lblMenAlmacen, ___accion, _validarAlmacen, datosTabla);
            //_funcion.key_campo(txtAlmacen,true,"Ya existe este numero de almacen",txtAlmacen,___accion,"almacen",{ "","",""});
        }

        private void txtSucursal_KeyUp(object sender, KeyEventArgs e)
        {
            _funcion.key_campo(txtSucursal);
        }

        private void txtServidor_KeyUp(object sender, KeyEventArgs e)
        {
            _funcion.key_campo(txtServidor);
        }

        private void txtfacturacion_KeyUp(object sender, KeyEventArgs e)
        {
            _funcion.key_campo(txtdbf);
        }

        private void txtCopyFiles_KeyUp(object sender, KeyEventArgs e)
        {
            _funcion.key_campo(txtUsudb);
        }

        private void txtPath_KeyUp(object sender, KeyEventArgs e)
        {
            _funcion.key_campo(txtPassdb);
        }

        private void txtAlmacen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                //MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            _funcion.limpiar_campos(txtAlmacen, this);
        }

        private void frmCURDAdminSucursal_FormClosing(object sender, FormClosingEventArgs e)
        {
            var _cerrando = _funcion.antes_cerrar(this);
            if (!_cerrando)
            {
                //Form parent = this.Owner;
                //Control[] ctrl = parent.Controls.Find("pbxImage", true);
                //ObtenerImagen(((PictureBox)ctrl[0]));
                //===============================================
                //frmAdminExpos adminExpo = new frmAdminExpos();
                //__funciones.cargar_datos((DataGridView)adminExpo.Controls["dgvExpos"], "cat_expo");
                //===============================================
                //Form parent = this.Owner;
                //Control[] ctrl = parent.Controls.Find("dgvExpos", true);
                //__funciones.cargar_datos((DataGridView)ctrl[0], "cat_expo");
                //===============================================
                if (_Form == "frmAdminEmpresa")
                {
                    DataGridView _dgvSucursal = ((frmAdminEmpresa)this.Owner).dgvEmpresa;
                    _funcion.cargar_datos(_dgvSucursal, "cat_sucursal");
                }
                else if (_Form == "FrmSucursales")
                {
                    //FrmSucursales.cargarDgv();
                    //var _dgvSucursal = ((DataGridView)this.Controls["dgvCatEmpresa"]);
                    //
                    //DataGridView _dgvSucursal = ((FrmSucursales)this.Owner).dgvCatEmpresa;
                    //_funcion.cargar_datos(_dgvSucursal, "cat_sucursal");
                    //

                    //DataTable _dtcat;

                    FrmSucursales._frmSucursal.Enabled = true;
                    //_funcion.cargar_datos(FrmSucursales._DgvSucursal, "cat_sucursal");
                    FrmSucursales._dtcat = _funcion.llenar_form("cat_sucursal");
                    int countTbl = 0;
                    //foreach (DataRow filaCat in FrmSucursales._dtcat.Rows)
                    //{
                    //    foreach (DataRow filaTbl in FrmSucursales._dtbl.Rows)
                    //    {
                    //        filaTbl[countTbl].ToString();
                    //        if (filaTbl["id"].ToString() == filaCat["id"].ToString())
                    //        {
                    //            FrmSucursales._dtcat.Rows.RemoveAt(countTbl);

                    //        }
                    //        countTbl++;
                    //    }
                    //}
                    //MessageBox.Show(""+___dtTbl.Rows.Count);

                    for (int i = 0; i < ___dtTbl.Rows.Count; i++)
                    {
                        for (int j = 0; j < FrmSucursales._dtcat.Rows.Count; j++)
                        {
                            //MessageBox.Show("i:"+i+"/"+ ___dtTbl.Rows.Count + " j:"+j +"/" + FrmSucursales._dtcat.Rows.Count+"  igual: " + FrmSucursales._dtcat.Rows[j]["id"].ToString() + " = " + ___dtTbl.Rows[i]["id"].ToString());
                            if (FrmSucursales._dtcat.Rows[j]["almacen"].ToString() == ___dtTbl.Rows[i]["almacen"].ToString())
                            {
                                //MessageBox.Show("igual: " + FrmSucursales._dtcat.Rows[i]["id"].ToString() + " = " + ___dtTbl.Rows[j]["id"].ToString());
                                FrmSucursales._dtcat.Rows.RemoveAt(j);

                            }
                            //MessageBox.Show(FrmSucursales._dtcat.Rows[i]["id"].ToString() + " = " + FrmSucursales._dtbl.Rows[j]["id"].ToString());
                            //var mi_variable = FrmSucursales._dtcat.Rows[j];
                        }
                    }
                    //for (int i = 0; i < FrmSucursales._dtcat.Rows.Count; i++)
                    //{

                    //    //_dtcat.Rows.RemoveAt(dgvCatEmpresa.CurrentRow.Index);
                    //}
                    FrmSucursales._DgvSucursal.DataSource = FrmSucursales._dtcat;
                    //FrmSucursales._frmSucursal();
                }
                
                GC.Collect();
            }
            e.Cancel = _cerrando;

            
        }

        private void txtAlmacenSql_KeyUp(object sender, KeyEventArgs e)
        {
            String[] datosTabla = { "cat_sucursal", "organization_id", txtAlmacenSql.Text, "varchar" };
            _funcion.key_campo(txtAlmacenSql, lblMenAlmacenSql, ___accion, _validarAlmacenSql, datosTabla);
        }

        private void txtAlmacenSql_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                //MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtDb_KeyUp(object sender, KeyEventArgs e)
        {
            _funcion.key_campo(txtDb);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtRutaBaja_KeyUp(object sender, KeyEventArgs e)
        {
            _funcion.key_campo(txtRutaBaja);
        }

        private void txtAgenBaja_KeyUp(object sender, KeyEventArgs e)
        {
            _funcion.key_campo(txtAgenBaja);
        }

        private void txtServSucu_KeyUp(object sender, KeyEventArgs e)
        {
            _funcion.key_campo(txtServSucu);
        }
    }
}
