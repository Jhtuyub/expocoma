using Microsoft.Reporting.WinForms;
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

        funciones _funcion = new funciones();

        public String _crvReporte;
        public DataTable _dtTabla;
        public Form _frmParent;

        public FrmVistaPrevia()
        {
            InitializeComponent();
        }

        private void FrmVistaPrevia_Load(object sender, EventArgs e)
        {
            _funcion.icono(this);
            DSReportes DSReport = new DSReportes();
            //ReportDocument cRep = new ReportDocument();
            //cRep.Load(Application.StartupPath + "\\reportes\\"+ _crvReporte+".rpt"); //CREtiquetas.rpt"
            //cRep.SetDataSource(_dtTabla);
            //crvVistaPrevia.ReportSource = cRep;
            ////this.Enabled = false;
            //this.rvReporte.RefreshReport();

            rvReporte.SetDisplayMode(DisplayMode.PrintLayout);
            rvReporte.ZoomMode = ZoomMode.PageWidth;
            //Seleccionamos el zoom que deseamos utilizar. En este caso un 100%
            //rvReporte.ZoomPercent = 100;

            rvReporte.ProcessingMode = ProcessingMode.Local;

            rvReporte.LocalReport.ReportPath = Application.StartupPath + "\\reportes\\" + _crvReporte + ".rdlc";
            //rvReporte.LocalReport.ReportEmbeddedResource = @"EXPOCOMA.reportes." + _crvReporte + ".rdlc";
            ReportDataSource rds = new ReportDataSource(_dtTabla.TableName, _dtTabla);
            rvReporte.LocalReport.DataSources.Clear();
            rvReporte.LocalReport.DataSources.Add(rds);
            rvReporte.RefreshReport();

          
        }

        private void FrmVistaPrevia_FormClosing(object sender, FormClosingEventArgs e)
        {
            _frmParent.Enabled = true;
            GC.Collect();
        }

        private void crvVistaPrevia_Load(object sender, EventArgs e)
        {

        }
    }
}
