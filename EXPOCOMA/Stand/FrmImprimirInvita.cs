using BarcodeFree;
using CrystalDecisions.CrystalReports.Engine;
using EXPOCOMA.reportes;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EXPOCOMA.Stand
{
    public partial class FrmImprimirInvita : Form
    {
        public String _CadenaConexion;
        funciones _funcion = new funciones();
        //internal static Form _frmParent;

        DataTable _dtSucursales;
        DataTable _dtClientes;
        DataTable _dtCliInvitaciones;

        DataTable _dtCliBaja = new DataTable();

        DSReportes DSReport;
        IEnumerable<DataRow> sql_dtClien;
        IEnumerable<DataRow> sql_dtClieInv;
        IEnumerable<DataRow> sql_dtsucusal;

        private Thread ThreadImprimir;


        public FrmImprimirInvita()
        {
            InitializeComponent();
        }

        private void FrmImprimirInvita_Load(object sender, EventArgs e)
        {

           

            _funcion.icono(this);
            picbCargando.Visible = false;
            _funcion.PicCargando(picbCargando);
            _funcion._SQLCadenaConexion = _CadenaConexion;
            _dtSucursales = _funcion.llenar_dt("tbl_sucursal", "ALMACEN, SUCURSAL, RUTA_BAJA, AGEN_BAJA" ,"", "ORDER BY anfitrion DESC");
            

            cBoxSucursal.DataSource = _dtSucursales;
            cBoxSucursal.ValueMember = "ALMACEN";//"valor";
            cBoxSucursal.DisplayMember = "SUCURSAL"; //"opcion";

            _dtCliBaja.Columns.Add("C_CLIENTE", typeof(String));
            _dtCliBaja.Columns.Add("NOM_CLIEN", typeof(String));
            _dtCliBaja.Columns.Add("CLIAGEN", typeof(String));
            _dtCliBaja.Columns.Add("NOM_AGENTE", typeof(String));
            _dtCliBaja.Columns.Add("C_RUTA", typeof(String));

            //_frmParent = this;

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

            ThreadImprimir = new Thread(Imprimir);
            ThreadImprimir.IsBackground = true;
            ThreadImprimir.Start();



        }

        public void Imprimir()
        {

            _funcion.DesabilitarControles(this, false);

            Code128 text = new Code128();
            String[] clvCliente;
            String clvNoCliente = "";
            String ClieRutaBaja = "";
            //String ClieAgenBaja = "";
            String claves = txtClvClientes.Text.Trim();
            String rutaBaja="";
            String agenBaja="";
            DSReport = new DSReportes();
            String idSucur = "";
            _dtCliBaja.Clear();
            
            this.Invoke((MethodInvoker)delegate
            {
                idSucur = cBoxSucursal.SelectedValue.ToString();
            });


            //            SELECT clie.ID_SUCURSALALM, clie.C_CLIENTE, clie.NOM_CLIEN, clie.C_AGENTE, agen.C_AGENTE, agen.NOM_AGENTE
            //FROM         dbf_cliente clie, dbf_agentes agen
            //WHERE(clie.C_AGENTE = agen.C_AGENTE)AND(clie.ID_SUCURSALALM = '001' AND agen.ID_SUCURSALALM = '001')
            _dtClientes = _funcion.llenar_dt("dbf_cliente clie, dbf_agentes agen", "clie.ID_SUCURSALALM, clie.C_CLIENTE, clie.NOM_CLIEN, clie.C_AGENTE AS CLIAGEN, agen.C_AGENTE AS AGENAGEN, agen.NOM_AGENTE, clie.C_RUTA, clie.NOM_TIENDA, clie.POBLACION, clie.TELEFONO", "WHERE (clie.C_AGENTE = agen.C_AGENTE)AND(clie.ID_SUCURSALALM = '" + idSucur + "' AND agen.ID_SUCURSALALM = '" + idSucur + "')");// "WHERE ID_SUCURSALALM = "+cBoxSucursal.SelectedValue.ToString());
            

            sql_dtsucusal =
                   from dtSucu in _dtSucursales.AsEnumerable()
                   where dtSucu.Field<String>("ALMACEN") == idSucur
                   select dtSucu;
            foreach (DataRow filSucu in sql_dtsucusal)
            {
                rutaBaja = filSucu["RUTA_BAJA"].ToString();
                agenBaja = filSucu["AGEN_BAJA"].ToString();
                break;
            }


            _dtClientes.Columns.Add("C_CLIEXPO", typeof(String));
            _dtClientes.Columns.Add("BARCODE", typeof(String));
            for (int i = 0; i < _dtClientes.Rows.Count; i++)
            {
                //_funcion.Cargando(this, barraProgreso, 0, iAlm, dtDBF.Rows.Count, lblMensaje, "Asignando el almacen: " + tbldtTablas.ToString());
                _dtClientes.Rows[i]["C_CLIEXPO"] = _dtClientes.Rows[i]["ID_SUCURSALALM"].ToString().Substring(2) + _dtClientes.Rows[i]["C_CLIENTE"].ToString();
                _dtClientes.Rows[i]["BARCODE"] = text.Encode(_dtClientes.Rows[i]["ID_SUCURSALALM"].ToString().Substring(2) + _dtClientes.Rows[i]["C_CLIENTE"].ToString());
            }
            //dtDBF.Columns.Add("id_sucursalalm", typeof(String));

            //for (int iAlm = 0; iAlm < dtDBF.Rows.Count; iAlm++)
            //{
            //    _funcion.Cargando(this, barraProgreso, 0, iAlm, dtDBF.Rows.Count, lblMensaje, "Asignando el almacen: " + tbldtTablas.ToString());
            //    dtDBF.Rows[iAlm]["ID_SUCURSALALM"] = almdtSucu.ToString();
            //}

            claves = claves.Replace("\r", "");
            clvCliente = claves.Split('\n');

            for (int i = 0; i < clvCliente.Count(); i++)
            {

                sql_dtClien =
                   from dtClien in _dtClientes.AsEnumerable()
                   where dtClien.Field<String>("C_CLIENTE") == clvCliente[i].ToString()
                   select dtClien;

                if (sql_dtClien.Count() > 0)
                {


                    foreach (DataRow filClien in sql_dtClien)
                    {

                        //MessageBox.Show(rutaBaja+"="+filClien["C_RUTA"].ToString() +"-"+ agenBaja+"="+filClien["CLIAGEN"].ToString());

                        if (filClien["C_RUTA"].ToString() ==rutaBaja)
                        {
                            ClieRutaBaja += clvCliente[i].ToString() + "\r\n";
                            _dtCliBaja.Rows.Add(
                                new Object[]
                                {
                                    filClien["C_CLIENTE"].ToString(),
                                    filClien["NOM_CLIEN"].ToString(),
                                    filClien["CLIAGEN"].ToString(),
                                    filClien["NOM_AGENTE"].ToString(),
                                    filClien["C_RUTA"].ToString(),
                                }
                                );
                        }
                        //else if (filClien["CLIAGEN"].ToString() == agenBaja)
                        //{
                        //    ClieAgenBaja += clvCliente[i].ToString() + "\r\n";
                        //}
                        else
                        {




                            _dtCliInvitaciones = _funcion.llenar_dt("tbl_ctesinvitados", "*", "WHERE ID_SUCURSALALM = '" + idSucur + "'");
                            //fil_dtClien = _dtClientes.Rows.Count;
                            //fil_dtClien = filClien.Rows.Count;
                            //for (int ii = 0; ii <= fil_dtClien-2; ii++)
                            //{
                            DSReport.Tables["dtInvitaciones"].Rows.Add(
                                new Object[]
                                {
                                    filClien["C_CLIEXPO"].ToString(),
                                    filClien["NOM_CLIEN"].ToString(),
                                    filClien["NOM_TIENDA"].ToString(),
                                    filClien["POBLACION"].ToString(),
                                    filClien["TELEFONO"].ToString(),
                                    filClien["AGENAGEN"].ToString(),
                                    filClien["NOM_AGENTE"].ToString(),
                                    filClien["BARCODE"].ToString(),
                                }
                                );

                            sql_dtClieInv =
                        from dtClieInv in _dtCliInvitaciones.AsEnumerable()
                        where dtClieInv.Field<String>("C_CLIEXPO") == filClien["C_CLIEXPO"].ToString()
                        select dtClieInv;
                            //foreach (DataRow filClieInv in sql_dtClieInv)
                            //{
                            if (!(sql_dtClieInv.Count() > 0))
                            {
                                string[,] _datos = {
                                    {"ID_SUCURSALALM", "C_CLIENTE","C_CLIEXPO"},
                                    { filClien["ID_SUCURSALALM"].ToString(),  filClien["C_CLIENTE"].ToString(), filClien["C_CLIEXPO"].ToString()},
                                    {"varchar", "varchar", "varchar"}
                                };

                                //__funciones.validar_campo(this, _datos, "cat_expo", timMensaje, ___accion, ___idDato);
                                _funcion.guardar_datos(_datos, "tbl_ctesinvitados", "nuevo", 0);
                            }
                   

                        }

                    }

                }
                else
                {
                    clvNoCliente += clvCliente[i].ToString() + "\r\n";
                }






            }

            //ReportDocument cRep = new ReportDocument();
            //cRep.Load(Application.StartupPath + "\\reportes\\CREtiquetas.rpt");
            //cRep.SetDataSource(DSReport.Tables["dtEtiquetas"]);
            //crvClientes.ReportSource = cRep;

            

            this.Invoke((MethodInvoker)delegate
            {
                txtNoImpre.Text = clvNoCliente;
                txtCliRutBaja.Text = ClieRutaBaja;
                //txtClieAgenBaja.Text = ClieAgenBaja;

                FrmVistaPrevia frmVistaPre = new FrmVistaPrevia();
                frmVistaPre.MdiParent = this.MdiParent;
                frmVistaPre._frmParent = this;
                frmVistaPre._crvReporte = "CREtiquetas";
                frmVistaPre._dtTabla = DSReport.Tables["dtInvitaciones"];
                frmVistaPre.Show();
                this.Enabled = false;
            });
           

            _funcion.DesabilitarControles(this, true);

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtClvClientes.Text = "";
            txtNoImpre.Text = "";
            txtCliRutBaja.Text = "";
        }

        private void btnGenerarxls_Click(object sender, EventArgs e)
        {
            if (_dtCliBaja.Rows.Count > 0)
            {
                try
                {
                    SaveFileDialog selectCarpeta = new SaveFileDialog();
                    selectCarpeta.FileName = "Clientes_ruta_baja";
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


        private void txtClvClientes_Enter(object sender, EventArgs e)
        {
            txtClvClientes.SelectAll();
        }
    }
}
