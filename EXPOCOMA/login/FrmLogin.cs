using EXPOCOMA.Funciones;
using EXPOCOMA.inicio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EXPOCOMA.login
{
    public partial class FrmLogin : Form
    {
        funcionesdblocal _funciones = new funcionesdblocal();
        public Boolean _ejecutando; //true = EJECUTANDO EL SISTEMA. false = SISTEMA EN EJECUCIÓN.
      
        private Thread CargarInfo;
        private Thread ThreadIniciarSesion;

        private Int32 totalArchi = 0;
        private Int32 TotalDll = 0;

        public Boolean _respuestaLogin = false;
        
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            _funciones.icono(this);
            FondoPantalla();
            if (_ejecutando)
            {
                CargarInfo = new Thread(CargarInformacion);
                CargarInfo.IsBackground = true;
                CargarInfo.Start();
            }
            
            
            //frmInicio _frmInicio = new frmInicio();
            //_frmInicio._respuestaLogin = true;



        }

        public void FondoPantalla()
        {
            //Ruta donde se encuentra nuestra imagen
            string ruta = Application.StartupPath + @"\recursos\EXPO_ COMA.JPG";
            MdiClient ctlMDI;
            // Loop through all of the form's controls looking
            // for the control of type MdiClient.
            foreach (Control ctl in pnlLogo.Controls)
            {
                try
                {
                    // Attempt to cast the control to type MdiClient.
                    ctlMDI = (MdiClient)ctl;

                    // Set the BackColor of the MdiClient control.
                    ctlMDI.BackColor = pnlLogo.BackColor;
                }
                catch (InvalidCastException exc)
                {
                    // Catch and ignore the error if casting failed.
                }
            }

            //Comprobamos que la ruta exista
            if (File.Exists(ruta))
            {
                //Creamos un Bitmap con la imagen
                Bitmap bmp = new Bitmap(ruta);

                //Se la colocamos de fondo al formulario
                pnlLogo.BackgroundImage = bmp;
                //pnlLogo.BackgroundImageLayout = ImageLayout.Center;
                //this.BackgroundImageLayout = ImageLayout.Zoom;
                pnlLogo.BackgroundImageLayout = ImageLayout.Stretch;
                //pnlLogo.BackgroundImageLayout = ImageLayout.Tile;
                //pnlLogo.BackgroundImageLayout = ImageLayout.None;
            }
        }

        void  CargarInformacion()
        {
            _funciones.DesabilitarControles(this, false);
            stripSLEstatus.Text = "Analizando";
            Thread.Sleep(500);

            String[] Archi = {
                "sqlceca40.dll", "sqlcecompact40.dll","sqlceer40EN.dll","sqlceer40ES.dll", "sqlceme40.dll", "sqlceoledb40.dll","sqlceqp40.dll","sqlcese40.dll", "System.Data.SqlServerCe.dll", "dbexpo.sdf",
                "Microsoft.SqlServer.Types.dll", "Microsoft.ReportViewer.Common.dll","Microsoft.ReportViewer.ProcessingObjectModel.DLL","Microsoft.ReportViewer.WinForms.DLL",
                "EPPlus.dll","BarcodeFree.dll","zh-Hant\\Microsoft.SqlServer.Types.resources.dll","zh-Hans\\Microsoft.SqlServer.Types.resources.dll","ru\\Microsoft.SqlServer.Types.resources.dll",
                "pt\\Microsoft.SqlServer.Types.resources.dll","ko\\Microsoft.SqlServer.Types.resources.dll","ja\\Microsoft.SqlServer.Types.resources.dll","it\\Microsoft.SqlServer.Types.resources.dll","fr\\Microsoft.SqlServer.Types.resources.dll",
                "ES\\Microsoft.SqlServer.Types.resources.dll","ES\\System.Data.SqlServerCe.resources.dll","de\\Microsoft.SqlServer.Types.resources.dll","recursos\\comita.ico","recursos\\EXPO_ COMA.JPG",
                "recursos\\loader.gif"
            };
            TotalDll = Archi.Count();

            for (int i = 0; i < TotalDll; i++)
            {
                _funciones.Cargando(this, stripPBEstatus, 5, i, TotalDll, stripSLEstatus, "" + Archi[i].ToString());
                if (!(File.Exists(Archi[i].ToString())))
                {

                    this.Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show("No encontre el archivo " + Archi[i].ToString(), "¡Upssss!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        _respuestaLogin = false;
                        //Application.Exit();
                        this.Close();
                    });

                    break;

                }
                else
                {

                    totalArchi++;

                }
                _funciones.Cargando(this, stripPBEstatus, 5, i + 1, TotalDll, stripSLEstatus, "" + Archi[i].ToString());
            }

            //Thread.Sleep(500);
            try
            {
                
                stripSLEstatus.Text = "Listo";
                _funciones.DesabilitarControles(this, true);
                
            }
            catch (Exception)
            {

                //throw;
            }

            this.Invoke((MethodInvoker)delegate
                        {
                            txtUsuario.Focus();
                        });
            
            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {

            ThreadIniciarSesion = new Thread(IniciarSesion);
            ThreadIniciarSesion.IsBackground = true;
            ThreadIniciarSesion.Start();

            
        }

        public void IniciarSesion()
        {
            _funciones.DesabilitarControles(this, false);
            _funciones.Cargando(this, stripPBEstatus, 5, 1, 2, stripSLEstatus, "Validando...");
            DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://servdc2", txtUsuario.Text, txtPass.Text);
            DirectorySearcher searcher = new DirectorySearcher(directoryEntry)
            {
                PageSize = int.MaxValue,
                //Filter = cadena.ToString(),
                //Filter = ("samAccountName=programacion"),

            };

            SearchResult result = null;
            try
            {
                result = searcher.FindOne();
                _respuestaLogin = true;
            this.Invoke((MethodInvoker)delegate
            {
                _funciones.Cargando(this, stripPBEstatus, 5, 2, 2, stripSLEstatus, "Bienvenido");
                //_funciones.DesabilitarControles(this, true);
                this.Close();
            });

        }
            catch (Exception ee)
            {
                //stripSLEstatus.Text = ee.Message;
                //stripSLEstatus.Text = "Usuario o contraseña incorrecta";
                //_funciones.Cargando(this, stripPBEstatus, 5, 0, 2, stripSLEstatus, "Usuario o contraseña incorrecta");
                _funciones.DesabilitarControles(this, true);
                MessageBox.Show(ee.Message);
                _respuestaLogin = false;
                //this.Close();
            }

    

        }
    }
}
