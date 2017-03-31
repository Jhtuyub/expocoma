using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EXPOCOMA
{
    public partial class frmIndex : Form
    {
        funciones funciones = new funciones();
        //para acceder a los metodos de esa clase haces lo sig
        SQL2014 conexSQL = new SQL2014();
        
       

        public frmIndex()
        {
            InitializeComponent();
            
        }


        private void frmIndex_Load(object sender, EventArgs e)
        {

          StringCollection mensaje = Properties.Settings.Default.Bdatos;
          
           // string mensaje= Properties.Settings.Default.Bdatos;

             //MessageBox.Show(mensaje);
            // funciones.hola();
            //DataSet ds = conexSQL.getVendedores("001"); // aca estan las funciones dela clase checalo para que dedes una idea
            //conexSQL.Open();
            //conexSQL.Close();


        }

        private void MenIndexItemConfiguracion_Click(object sender, EventArgs e)
        {
           // Properties.Settings.Default.Bdatos = "Bien y tu";
           // Properties.Settings.Default.Save();
        }
    }
}
