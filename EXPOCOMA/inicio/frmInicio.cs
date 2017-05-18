using EXPOCOMA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using EXPOCOMA.Stand;
using EXPOCOMA.Funciones;
using System.IO;
using EXPOCOMA.login;

namespace EXPOCOMA.inicio
{
    public partial class frmInicio : Form
    {

        funcionesdblocal _funciones = new funcionesdblocal();
        private Thread CargarInfo;
        private Thread currentThread;
        delegate void RefreshProgressDelegate(decimal percent);
        delegate void ControlesDelegate(Boolean valor);

        delegate void AbrirIndexDelegate();

        private Int32 totalArchi = 0;
        private Int32 TotalDll = 0;

        //public Boolean _respuestaLogin =false;

        public frmInicio()
        {
            //_funciones._TIPObasedatos = "dbcompaq";
            InitializeComponent();
        }

        public void llenar_list(ListBox lb)
        {
            var _dato = _funciones.llenar_form("cat_expo", 0, "nombre", txtExpo.Text);
            lb.DataSource = _dato;
            lb.DisplayMember = "id";
            lb.DisplayMember = "nombre";
            //cbExpo.DataSource = _dato;
            //cbExpo.DisplayMember = "nombre";
            //cbExpo.ValueMember = "id";
            //cbExpo.Datasource = _dato;
        }

        private void frmInicio_Load(object sender, EventArgs e)
        {
            //Boolean _respuestaLogin = false;
            FrmLogin _frmLogin = new FrmLogin();
            _frmLogin._ejecutando= true;
            _frmLogin.ShowDialog();
            var _respuestaLogin = _frmLogin._respuestaLogin;

            if (_respuestaLogin)
            {
                //this.Enabled = false;
                menInicio.Enabled = false;
                CargarInfo = new Thread(CargarInformacion);
                CargarInfo.IsBackground = true;
                CargarInfo.Start();
            }
            else
            {
                this.Close();
            }

            


            //_funciones.PicCargando(picbCargando);
            _funciones.icono(this);

            
           

        }


        void CargarInformacion()
        {

            _funciones.DesabilitarControles(this, false);
            Thread.Sleep(500);

            this.Invoke((MethodInvoker)delegate
            {
                //stripSLEstatus.Text = "";
                //llenar_list(lbExpo);
                _funciones.cargar_datos(dgvExpos, "cat_expo");
                //___DGVDATOS.DataSource = dt;   
            });

            //Thread.Sleep(1000);
            //stripSLEstatus.Text = "Listo";
            _funciones.Cargando(this, stripPBEstatus, 0, 0 , 1, stripSLEstatus, "");
            _funciones.DesabilitarControles(this, true);

            this.Invoke((MethodInvoker)delegate
            {
                dgvExpos.Columns["id"].Visible = false;
                dgvExpos.Columns["db"].Visible = false;

                dgvExpos.DefaultCellStyle.SelectionBackColor = Properties.Settings.Default.filaSeleccion;
                dgvExpos.AlternatingRowsDefaultCellStyle.BackColor = Properties.Settings.Default.filaAltern;

                menInicio.Enabled = true;
            });
                


            CargarInfo =null;
            GC.Collect();



            //_funcion.Cargando(this, pbCargando, 0, 1, 3, lblCargando, "Preparando dbf ");
        }


        private void ToolStripMenuItemAdminExpo_Click(object sender, EventArgs e)
        {
            
            frmAdminExpos frmAdEx = new frmAdminExpos();
            frmAdEx.Owner = this;
            frmAdEx.ShowDialog();
            
        }

       

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void Controles(Boolean value)
        {
            txtExpo.Enabled = value;
            btnAceptar.Enabled = value;
            dgvExpos.Enabled = value;
        }
        public void RefreshProgress(decimal value)
        {
            if (this == null) return;
            stripPBEstatus.Value = (int)value;
        }
        public void Entrar() {

            Boolean _Conec = true;
            String _Mensaje = "";
            
                //this.Invoke(new ControlesDelegate(Controles), false);
                _funciones.DesabilitarControles(this, false);

                using (SqlConnection _con = new SqlConnection(_funciones.SqlConexion(dgvExpos.CurrentRow.Cells["db"].Value.ToString())))
                {

                    Decimal total = 4;
                Boolean _existe =  false;

                    var resultado = 0;
                    try
                    {
                        //_funciones.Cargando(this, stripPBEstatus, 1, total, stripSLEstatus, "Conectando a la base de datos");
                        //stripSLEstatus.Text = "Conectando a la base de datos";
                        //Thread.Sleep(10);
                        //var percent = (1 / total) * 100;
                        //this.Invoke(new RefreshProgressDelegate(RefreshProgress), percent);
                        _funciones.Cargando(this, stripPBEstatus,10, 1, total, stripSLEstatus, "Conectado a la base de datos, servidor: "+Properties.Settings.Default.sqlserServidor);
                        //stripSLEstatus.Text = "Conectado a la base de datos";


                        _con.Open();
                    _existe = true;
                        //_funciones.Cargando(this, stripPBEstatus, 10, 1, total, stripSLEstatus, "No encuentro la base de datos");
                        //stripSLEstatus.Text = "Conectado a la base de datos";
                        // Insert code to process data.

                }

                    catch (Exception ex)
                    {
                        try
                        {
                            _funciones.Cargando(this, stripPBEstatus, 10, 2, total, stripSLEstatus, "Creando base de datos");
                            //stripSLEstatus.Text = "Creando base de datos";
                            //Thread.Sleep(10);
                            //var percent = (2 / total) * 100;
                            //this.Invoke(new RefreshProgressDelegate(RefreshProgress), percent);

                            SqlConnection _conCrear = new SqlConnection(_funciones.SqlCone());
                            _conCrear.Open();
                            SqlCommand _crearDB = new SqlCommand("IF NOT EXISTS (SELECT * FROM sysdatabases WHERE NAME ='" + dgvExpos.CurrentRow.Cells["db"].Value.ToString() + "')"
                                + " CREATE DATABASE " + dgvExpos.CurrentRow.Cells["db"].Value.ToString()
                                //+ " BEGIN"
                                //+ " END "
                                , _conCrear);
                            resultado = _crearDB.ExecuteNonQuery();

                            _funciones.Cargando(this, stripPBEstatus, 10, 2, total, stripSLEstatus, "Conectado a base de datos");
                        //stripSLEstatus.Text = "Conectado a la base de datos";
                        //MessageBox.Show(ex.ToString(), "¿Algún Problema?");
                        _existe = false;
                        }
                        catch (Exception exc)
                        {

                            MessageBox.Show("No logro crear la base de datos: " + exc.ToString());
                        _Conec = false;
                        _existe = false;
                        }
                        
                    }
                    finally
                    {
                        _con.Close();
                    if (_Conec)
                    {
                        _Mensaje = "Conectado. Preparando sistema";
                        _funciones.Cargando(this, stripPBEstatus, 10, 3, total, stripSLEstatus, _Mensaje);
                        if (!_existe)
                        {
                            Thread.Sleep(2000);
                            _funciones.Cargando(this, stripPBEstatus, 10, 4, total, stripSLEstatus, "Servidor: " + Properties.Settings.Default.sqlserServidor);
                            Thread.Sleep(2000);
                            _funciones.Cargando(this, stripPBEstatus, 10, 4, total, stripSLEstatus, "Bienvenido");
                            Thread.Sleep(1000);
                        }
                        
                        this.Invoke(new AbrirIndexDelegate(AbrirIndex));
                    }
                    else
                    {
                        _Mensaje = "No se logro conectar";
                        _funciones.Cargando(this, stripPBEstatus, 10, 3, total, stripSLEstatus, _Mensaje);
                    }
                        //_funciones.Cargando(this, stripPBEstatus, 3, total, stripSLEstatus, _Mensaje);
                        //Thread.Sleep(10);
                        //var percent = (3 / total) * 100;
                        //this.Invoke(new RefreshProgressDelegate(RefreshProgress), percent);
                    }


                    //if (resultado ==1)
                    //{
                    //stripSLEstatus.Text = "Se ha creado";
                    ////MessageBox.Show("Se creo");
                    //}else
                    //{
                    //    MessageBox.Show("No se creo");
                    //}
                    //MessageBox.Show("Se conecto: " +_con);
                }

            _funciones.DesabilitarControles(this, true);
            currentThread = null;
                //this.Invoke(new ControlesDelegate(Controles), true);
                
           
}


        public void AbrirIndex()
        {
            
            this.Hide();
            FrmIndex _Index = new FrmIndex();
            _Index._CadenaConexion = _funciones.SqlConexion(dgvExpos.CurrentRow.Cells["db"].Value.ToString());
            _Index.nomExpo = dgvExpos.CurrentRow.Cells["nombre"].Value.ToString();
            _Index.Owner=this;
            if (Properties.Settings.Default.cerrarApp)
            {
                _Index.ShowDialog();
                this.Close();
            }
            else
            {
                _Index.Show();
                stripPBEstatus.Value = 0;
                stripSLEstatus.Text = "";
            }
            GC.Collect();



            //ThreadIndex = new Thread(EntrarIndex);
            //ThreadIndex.IsBackground = true;
            //ThreadIndex.Start();

        }
        //public void EntrarIndex()
        //{
        //    FrmIndex _Index = new FrmIndex();
        //    _Index.Show();
        //    Close();
        //}



        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (currentThread == null)
            {
                //btnAceptar.Text = "Detener";
                currentThread = new Thread(Entrar);
                currentThread.IsBackground = true;
                currentThread.Start();
            }
            //else
            //{
            //    btnAceptar.Text = "Aceptar";
            //    currentThread.Abort();
            //    currentThread = null;
            //    stripPBEstatus.Value = 0;
            //}
            //else
            //{
            //    btnAceptar.Enabled = false;
            //}
            //Properties.Settings.Default.sqlserDb = this.txtDblocal.Text;
            //Properties.Settings.Default.Save();
            //MessageBox.Show(dgvExpos.CurrentRow.Cells["db"].Value.ToString());
            //try
            //{

            //using (SqlConnection _con = dbconexion.SqlConexion(dgvExpos.CurrentRow.Cells["db"].Value.ToString()))

            //}
            //catch (ArgumentException arEx)
            //{
            //    MessageBox.Show("Algun problema :S: "+ arEx.ToString());
            //    //throw;
            //}

            //llenar_list(lbExpo);
            //var _dato = _funciones.llenar_form("cat_expo", 0);
            //cbExpo.DataSource = _dato;
            //cbExpo.DisplayMember = "nombre";
            //cbExpo.ValueMember = "id";
        }

        private void ToolStripMenuItemSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtExpo_KeyUp(object sender, KeyEventArgs e)
        {
            _funciones.cargar_datos(dgvExpos, "cat_expo", "nombre", txtExpo.Text);
            //// Set the search string:
            ////string myString = "Isabella";
            //// Search starting from index -1:
            //int index = lbExpo.FindString(txtExpo.Text, -1);
            //if (index != -1)
            //{
            //    // Select the found item:
            //    lbExpo.SetSelected(index, true);
            //    // Send a success message:
            //    //MessageBox.Show("Found the item \"" + myString +
            //    //    "\" at index: " + index);
            //}

        }

        private void txtExpo_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvExpos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvExpos.Rows.Count > 0)
            {
                btnAceptar.Enabled = true;
                
            }
            else
            {
                btnAceptar.Enabled = false;
                
            }
            
        }

        private void dgvExpos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvExpos.Rows.Count > 0)
            {
                btnAceptar.PerformClick();
            }
        }

        private void ToolStripMenuItemConfiguracion_Click(object sender, EventArgs e)
        {
            frmSistema _frmSistema = new frmSistema();
            _frmSistema.ShowDialog();
        }

        private void dgvExpos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgvExpos.Rows.Count > 0)
                {
                    btnAceptar.PerformClick();
                }
            }
        }

        private void administrarCatalogoEmpresasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdminEmpresa _frmEmpresa = new frmAdminEmpresa();
            _frmEmpresa.ShowDialog();
        }

        private void dgvExpos_MouseMove(object sender, MouseEventArgs e)
        {
            dgvExpos.Focus();
        }
    }
}
