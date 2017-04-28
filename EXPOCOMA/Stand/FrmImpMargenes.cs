using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
        DataTable _dtProvexpoExcel;

        Int32 totalPoradic = 0;

        public FrmImpMargenes()
        {
            InitializeComponent();
        }

        private void FrmImpMargenes_Load(object sender, EventArgs e)
        {
            _funcion.icono(this);
            _funcion._SQLCadenaConexion = _CadenaConexion;
            _dtProvexpo = _funcion.llenar_dt("tbl_provexpo");
            //totalPoradic = _funcion.llenar_dt("tbl_provexpo","PORADIC","WHERE PORADIC IS NULL").Rows.Count;
            for (int i = 0; i < _dtProvexpo.Rows.Count; i++)
                {

                if (String.IsNullOrEmpty(_dtProvexpo.Rows[i]["PORADIC"].ToString()))
                {
                    //_dtProvexpo.Rows[i]["PORADIC"] = "1";
                    string[,] _datos = {
                                    {"PORADIC"},
                                    { "1"},
                                    {"varchar"}
                                };
                    //__funciones.validar_campo(this, _datos, "cat_expo", timMensaje, ___accion, ___idDato);
                    _funcion.guardar_datos(_datos, "tbl_provexpo", "modificar", Convert.ToInt32(_dtProvexpo.Rows[i]["id"].ToString()));
                }
               
            }
                
            
            _dtProvexpoExcel = _funcion.llenar_dt("tbl_provexpo","C_PROVE, DESCRI, PORADIC, MARGEN");

            dgvProvexpo.DataSource = _dtProvexpo;
            dgvProvexpo.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProvexpo.Columns["id"].Visible = false;
            dgvProvexpo.Columns["ID_SUCURSALALM"].Visible = false;
            dgvProvexpo.Columns["C_PROVE2"].Visible = false;
            dgvProvexpo.Columns["C_PROVE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProvexpo.Columns["C_PROVE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvProvexpo.Columns["PORADIC"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvProvexpo.Columns["MARGEN"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            

        }

        private void btnArchiMarPre_Click(object sender, EventArgs e)
        {
            if (_dtProvexpoExcel.Rows.Count > 0)
            {
                try
                {
                    SaveFileDialog selectCarpeta = new SaveFileDialog();
                    selectCarpeta.FileName = "Lista_provedor_";
                    selectCarpeta.Filter = "xls|*.xls";
                    selectCarpeta.Title = "Guardar Archivo Para Margen y Lista de Precios";
                    if (selectCarpeta.ShowDialog() == DialogResult.OK)
                    {
                        //textBox1.Text = selectCarpeta.FileName;
                        File.Delete(selectCarpeta.FileName);
                        string rutaArchivo = selectCarpeta.FileName;
                        String[] col = { "A", "B", "C", "D" };
                        int row;
                        using (ExcelPackage package = new ExcelPackage(new FileInfo(rutaArchivo)))
                        {
                            var worksheet = package.Workbook.Worksheets.Add("Contenido");
                            worksheet.Cells["A1"].Value = "C_PROVE";
                            worksheet.Cells["B1"].Value = "DESCRI";
                            worksheet.Cells["C1"].Value = "PORADIC";
                            worksheet.Cells["D1"].Value = "MARGEN";
                            //worksheet.Cells["E1"].Value = "COMPRADOR";



                            for (int ii = 0; ii < _dtProvexpoExcel.Rows.Count; ii++)
                            {
                                row = 0;
                                for (int jj = 0; jj < _dtProvexpoExcel.Columns.Count; jj++)
                                {
                                    row = ii + 2;
                                    //MessageBox.Show(dtDatos.Rows[i][j].ToString());
                                    worksheet.Cells[col[jj].ToString() + row].Value = _dtProvexpoExcel.Rows[ii][jj].ToString();
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
}
