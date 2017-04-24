using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EXPOCOMA.Stand
{
    public partial class FrmImpMargenes : Form
    {
        public String _CadenaConexion;
        funciones _funcion = new funciones();

        DataTable _dtProvexpo;

        public FrmImpMargenes()
        {
            InitializeComponent();
        }

        private void FrmImpMargenes_Load(object sender, EventArgs e)
        {
            _funcion.icono(this);
            _funcion._SQLCadenaConexion = _CadenaConexion;
            _dtProvexpo = _funcion.llenar_dt("tbl_provexpo");

            dgvProvexpo.DataSource = _dtProvexpo;
        }

        private void btnArchiMarPre_Click(object sender, EventArgs e)
        {
            if (_dtProvexpo.Rows.Count > 0)
            {
                try
                {
                    SaveFileDialog selectCarpeta = new SaveFileDialog();
                    selectCarpeta.FileName = "Lista_provedor_";
                    selectCarpeta.Filter = "xls|*.xls";
                    selectCarpeta.Title = "Guardar clientes ruta de baja";
                    if (selectCarpeta.ShowDialog() == DialogResult.OK)
                    {
                        //textBox1.Text = selectCarpeta.FileName;
                        File.Delete(selectCarpeta.FileName);
                        string rutaArchivo = selectCarpeta.FileName;
                        String[] col = { "A", "B", "C", "D", "E" };
                        int row;
                        using (ExcelPackage package = new ExcelPackage(new FileInfo(rutaArchivo)))
                        {
                            var worksheet = package.Workbook.Worksheets.Add("Contenido");
                            worksheet.Cells["A1"].Value = "C_CLIENTE";
                            worksheet.Cells["B1"].Value = "NOMBRE";
                            worksheet.Cells["C1"].Value = "C_AGENTE";
                            worksheet.Cells["D1"].Value = "NOMBRE AGENTE";
                            worksheet.Cells["E1"].Value = "C_RUTA";



                            for (int ii = 0; ii < _dtCliBaja.Rows.Count; ii++)
                            {
                                row = 0;
                                for (int jj = 0; jj < _dtCliBaja.Columns.Count; jj++)
                                {
                                    row = ii + 2;
                                    //MessageBox.Show(dtDatos.Rows[i][j].ToString());
                                    worksheet.Cells[col[jj].ToString() + row].Value = _dtCliBaja.Rows[ii][jj].ToString();
                                    //texto += dtDatos.Rows[i][j].ToString() + "\t";
                                }
                                //worksheet.Cells["A"+i].Value = dtDatos.Rows[i][j].ToString()
                            }


                            package.Save();
                        }

                    }
                }
                catch (Exception ee)
                {

                    MessageBox.Show(ee.Message, "¡Espera!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
    }
}
