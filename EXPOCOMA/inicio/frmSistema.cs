using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EXPOCOMA.inicio
{
    public partial class frmSistema : Form
    {
        funciones _funciones = new funciones();
        private Thread currentThread;
        delegate void RefreshProgressDelegate(decimal percent, String mensaje);
        delegate void ControlDelegate(Boolean value);

        public frmSistema()
        {
            InitializeComponent();
        }

        private void frmSistema_Load(object sender, EventArgs e)
        {
            _funciones.icono(this);
            //SISTEMA
            checkBCerrar.Checked = Properties.Settings.Default.cerrarApp;
            //
            txtDblocal.Text = Properties.Settings.Default.dblocal;

            //SQL SERVER
            txtServidor.Text = Properties.Settings.Default.sqlserServidor;
            txtUsuario.Text = Properties.Settings.Default.sqlserUsuario;
            txtContrasena.Text = Properties.Settings.Default.sqlserPass;

            String[,] _cbdatos = {
                { "sqlserver",},//"oracle"
                { "SQL SERVER",}//"ORACLE"
            };
            //MessageBox.Show(Properties.Settings.Default.sqlorigen);
            _funciones.llenarCombobox(cbxOrigenDatos, _cbdatos);
            cbxOrigenDatos.SelectedValue = Properties.Settings.Default.sqlorigen;
            //lblMensaje.Text = "";
        }

        private void frmSistema_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtDblocal_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.dblocal= this.txtDblocal.Text;
            Properties.Settings.Default.Save();
        }

        private void txtServidor_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.sqlserServidor = this.txtServidor.Text;
            Properties.Settings.Default.Save();
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.sqlserUsuario= this.txtUsuario.Text;
            Properties.Settings.Default.Save();
        }

        private void txtContrasena_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.sqlserPass = this.txtContrasena.Text;
            Properties.Settings.Default.Save();
        }


        public void RefreshProgress(decimal value, String mensaje)
        {
            lblMensaje.Text = mensaje;
            if (this == null) return;
            pbconexion.Value = (int)value;
        }

        public void Control(Boolean value)
        {
            txtServidor.Enabled = value;
            txtUsuario.Enabled = value;
            txtContrasena.Enabled = value;
            btnTest.Enabled = true;
            if (value ==true)
            {
                btnTest.Text = "Test";
            }

        }
        public void Test()
        {
       
            //_funciones.DesabilitarControles(this,false, btnTest);
            this.Invoke(new ControlDelegate(Control), false);
            using (SqlConnection _con = new SqlConnection(_funciones.SqlCone()))
            {

               


                    Decimal total = 2;

                    _funciones.Cargando(this, pbconexion, 10, 1, total, lblMensaje, "Conectando");
                    ////lblMensaje.Text = "Conectando";
                    //Thread.Sleep(10);
                    //var percent = (1 / total) * 100;
                    //this.Invoke(new RefreshProgressDelegate(RefreshProgress), percent, "Conectando");
                    try
                    {
                        _con.Open();
                        _funciones.Cargando(this, pbconexion, 10, 2, total, lblMensaje, "Conexion Exitosa");
                        ////lblMensaje.Text = "Conexion Exitosa";
                        //Thread.Sleep(10);
                        //percent = (2 / total) * 100;
                        //this.Invoke(new RefreshProgressDelegate(RefreshProgress), percent, "Conexion Exitosa");

                    }
                    catch (Exception exc)
                    {

                        _funciones.Cargando(this, pbconexion, 10, 2, total, lblMensaje, "Error al conectar");
                        ////lblMensaje.Text = "Error al conectar";
                        //Thread.Sleep(10);
                        //percent = (2 / total) * 100;
                        //this.Invoke(new RefreshProgressDelegate(RefreshProgress), percent, "Error al conectar");
                    }

                    finally
                    {
                        _con.Close();

                    }
                }
            this.Invoke(new ControlDelegate(Control), true);
                currentThread = null;
                //_funciones.DesabilitarControles(this, true, btnTest);
            
}
        private void btnTest_Click(object sender, EventArgs e)
        {
            if (currentThread == null)
            {
                btnTest.Text = "Cancelar";
                btnTest.Enabled = false;
                currentThread = new Thread(Test);
                currentThread.IsBackground = true;
                currentThread.Start();
                btnTest.Enabled = true;
            }
            else
            {
                btnTest.Text = "Test";
                currentThread.Abort();
                currentThread = null;
                pbconexion.Value = 0;
                Control(true);
            }

        }

        private void checkBCerrar_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.cerrarApp = checkBCerrar.Checked;
            Properties.Settings.Default.Save();
        }

        private void frmSistema_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
        }

        private void cbxOrigenDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cbxOrigenDatos_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Properties.Settings.Default.sqlorigen = cbxOrigenDatos.SelectedValue.ToString();
            Properties.Settings.Default.Save();
            //MessageBox.Show(cbxOrigenDatos.SelectedValue.ToString());
        }
    }
}
