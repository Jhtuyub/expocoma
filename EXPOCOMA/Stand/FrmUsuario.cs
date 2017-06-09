using EXPOCOMA.Funciones;
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
    public partial class FrmUsuario : Form
    {

        funcionesdblocal _funciones = new funcionesdblocal();

        public FrmUsuario()
        {
            InitializeComponent();
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            _funciones.icono(this);
        }
    }
}
