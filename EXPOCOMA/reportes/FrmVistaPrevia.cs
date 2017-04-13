using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EXPOCOMA.reportes
{
    public partial class FrmVistaPrevia : Form
    {

        public String _crvReporte;
        public DataTable _dtTabla;

        public FrmVistaPrevia()
        {
            InitializeComponent();
        }

        private void FrmVistaPrevia_Load(object sender, EventArgs e)
        {
            DSReporte DSReport = new DSReporte();
            ReportDocument cRep = new ReportDocument();
            cRep.Load(Application.StartupPath + "\\reportes\\"+ _crvReporte+".rpt"); //CREtiquetas.rpt"
            cRep.SetDataSource(_dtTabla);
            crvVistaPrevia.ReportSource = cRep;
        }
    }
}
