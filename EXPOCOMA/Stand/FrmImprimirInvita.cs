using BarcodeFree;

using EXPOCOMA.reportes;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Diagnostics;
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
        public String _nomUsuario;
        public String nomExpo;
        public String _CadenaConexion;
        //String _CadenaSql;
        String tmpCarpetaLocal;
        String carpetaServ;
        String tmpCarpetaServ;
        String idActSucu;
        String servidorSucu;
        String actuRutaBaja;
        String actuAgenBaja;


        funciones _funcion = new funciones();
        //internal static Form _frmParent;

        DataTable _dtSucursales;
        DataTable _dtClientes;
        DataTable _dtCliInvitaciones;
        DataTable _dtActualizaClie;
        DataTable _dtTmpCliente;

        DataTable _dtCliBaja = new DataTable();

        DSReportes DSReport;
        IEnumerable<DataRow> sql_dtClien;
        IEnumerable<DataRow> sql_dtClieInv;
        IEnumerable<DataRow> sql_dtsucusal;
        IEnumerable<DataRow> sql_dtsucu;
        IEnumerable<DataRow> sql_dtTmpClien;

        private Process programa;
        private Thread ThreadImprimir;
        private Thread ThreadActualizar;

        String[,] _tablas =
            {
                {
                    //_tablasNombre - TABLAS 
                    "cliente",
                    "agentes",
                    "rutagen",
                    "rutas",
                },
                {
                    //_esTablaBdf - SI ES BDF
                    "true",
                    "false",
                    "false",
                    "false",
                },
                {

                //_tablasBDFCampos - CAMPOS DE LA TABLA DBF
                "ID_SUCURSALALM,C_CLIENTE,NOM_CLIEN,NOM_TIENDA,POBLACION,TELEFONO,C_AGENTE,C_RUTA,DIRECCI,RFC", //
                    "",
                    "",
                    "",
                },
                {
                    //_esTablaSql - SI ES SQL
                    "false",
                    "false",
                    "false",
                    "false",
                },
                {
                //_tablasSQLCampos - CAMPOS DE LA TABLA SQL
                    "",
                    "",
                    "",
                    "",
                },
                {
                //_tablasNomDestino - TABLAS DESTINO
                    "dbf_cliente",
                    "",
                    "",
                    "",
                },
                {
                //_tablasNomDestinoCampos - CAMPOS DE TABLAS DESTINO
                    "ID_SUCURSALALM,C_CLIENTE,NOM_CLIEN,NOM_TIENDA,POBLACION,TELEFONO,C_AGENTE,C_RUTA,DIRECCI,RFC",//
                    "",
                    "",
                    "",
                },
                {
                //_tablasNomDestiCamposComparar - CAMPOS DE TABLAS DESTINO PARA COMPARA SI EXISTEN O NO
                    "ID_SUCURSALALM-C_CLIENTE",
                    "",
                    "",
                    "",
                },

    };
        String _tablasNombre;
        String _esTablaBdf;
        String _tablasBDFCampos;
        String _esTablaSql;        
        String _tablasSQLCampos;
        String _tablasNomDestino;
        String _tablasNomDestinoCampos;
        String _tablasNomDestiCamposComparar;

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
            _dtSucursales = _funcion.llenar_dt("tbl_sucursal", "ALMACEN, SUCURSAL,SERVIDORSUCU, RUTA_BAJA, AGEN_BAJA, DBF", "", "ORDER BY anfitrion DESC");
            

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
            if (!(String.IsNullOrWhiteSpace(txtClvClientes.Text)))
            {
                ThreadImprimir = new Thread(Imprimir);
                ThreadImprimir.IsBackground = true;
                ThreadImprimir.Start();
            }
            



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
                frmVistaPre._crvReporte = "rvInvitacion";
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
            _dtCliInvitaciones.Clear();
            _dtClientes.Clear();
            _dtCliBaja.Clear();
        }

        private void btnGenerarxls_Click(object sender, EventArgs e)
        {
            if (_dtCliBaja.Rows.Count > 0)
            {
                try
                {

                    String carpetaLocal = Application.StartupPath + @"\tmp_expo\" + nomExpo+ @"\" + _nomUsuario;
                    String Archivo = "TMPbajas_clientes_"+nomExpo+".xls";
                    String ArcExcel = carpetaLocal + Archivo;
                    if (!Directory.Exists(carpetaLocal))
                    {
                        Directory.CreateDirectory(carpetaLocal);
                    }

                    if (!Directory.Exists(ArcExcel))
                    {
                        File.Delete(ArcExcel);
                    }

                    //textBox1.Text = selectCarpeta.FileName;
                    //File.Delete(selectCarpeta.FileName);
                    //string rutaArchivo = selectCarpeta.FileName;
                    //String[] col = { "A", "B", "C", "D", "E" };
                    //int row;
                    using (ExcelPackage package = new ExcelPackage(new FileInfo(ArcExcel)))
                    {
                        //GUARDA EXCEL CON LOS DATOS DE UN DATATABLE
                        var worksheet = package.Workbook.Worksheets.Add("Hoja 1");
                        worksheet.Cells["A1"].LoadFromDataTable(_dtCliBaja, true);
                        package.Save();
                        System.Diagnostics.Process.Start(ArcExcel);

                        //GUARDAR EXCEL SELECCIONANDO UNA RUTA
                        //SaveFileDialog selectCarpeta = new SaveFileDialog();
                        //selectCarpeta.FileName = "Clientes_ruta_baja";
                        //selectCarpeta.Filter = "xls|*.xls";
                        //selectCarpeta.Title = "Guardar clientes ruta de baja";
                        //if (selectCarpeta.ShowDialog() == DialogResult.OK)
                        //{
                        //    //textBox1.Text = selectCarpeta.FileName;
                        //    File.Delete(selectCarpeta.FileName);
                        //    string rutaArchivo = selectCarpeta.FileName;
                        //    String[] col = { "A", "B", "C", "D", "E" };
                        //    int row;
                        //    using (ExcelPackage package = new ExcelPackage(new FileInfo(rutaArchivo)))
                        //    {
                        //        var worksheet = package.Workbook.Worksheets.Add("Contenido");
                        //        worksheet.Cells["A1"].Value = "C_CLIENTE";
                        //        worksheet.Cells["B1"].Value = "NOMBRE";
                        //        worksheet.Cells["C1"].Value = "C_AGENTE";
                        //        worksheet.Cells["D1"].Value = "NOMBRE AGENTE";
                        //        worksheet.Cells["E1"].Value = "C_RUTA";



                        //        for (int ii = 0; ii < _dtCliBaja.Rows.Count; ii++)
                        //        {
                        //            row = 0;
                        //            for (int jj = 0; jj < _dtCliBaja.Columns.Count; jj++)
                        //            {
                        //                row = ii + 2;
                        //                //MessageBox.Show(dtDatos.Rows[i][j].ToString());
                        //                worksheet.Cells[col[jj].ToString() + row].Value = _dtCliBaja.Rows[ii][jj].ToString();
                        //                //texto += dtDatos.Rows[i][j].ToString() + "\t";
                        //            }
                        //            //worksheet.Cells["A"+i].Value = dtDatos.Rows[i][j].ToString()
                        //        }


                        //        package.Save();
                        //    }

                        //}
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

        private void txtClvClientes_Click(object sender, EventArgs e)
        {
            txtClvClientes.SelectAll();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (ThreadActualizar == null)
            {
                ThreadActualizar = new Thread(Actualizar);
                ThreadActualizar.IsBackground = true;
                ThreadActualizar.Start();
            }
            else
            {
                ThreadActualizar.Suspend();
                var detener = MessageBox.Show("¿Desea detener la actualización?", "¡Aviso!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (detener == DialogResult.Yes)
                {
                    //btnImportar.Text = "Importar";
                    ThreadActualizar.Resume();
                    ThreadActualizar.Abort();
                    ThreadActualizar = null;
                    //lblMensaje.Text = "...";
                    //barraProgreso.Value = 0;
                    //this.Invoke(new CamposEnableDelegate(_funcion.CamposEnabled), this, true, btnImportar, "Importar");
                    _funcion.DesabilitarControles(this, true, btnActualizar);
                    _funcion.Cargando(this, stripPBEstatus, 0, 0, 1, stripSLEstatus, "...");
                }
                else
                {
                    ThreadActualizar.Resume();
                }
            }
            
        }

        public void Actualizar()
        {
            _funcion.DesabilitarControles(this, false, btnActualizar);
            //Thread.Sleep(5000);
            Invoke((MethodInvoker)delegate {
                idActSucu = cBoxSucursal.SelectedValue.ToString();
            });


            sql_dtsucu =
                   from dtSucu in _dtSucursales.AsEnumerable()
                   where dtSucu.Field<String>("ALMACEN") == idActSucu
                   select dtSucu;

            foreach (DataRow filSucu in sql_dtsucu)
            {
                tmpCarpetaLocal = Application.StartupPath + @"\tmp_expo\" + nomExpo + @"\" + _nomUsuario + @"\" + idActSucu;
                carpetaServ = filSucu["dbf"].ToString();
                tmpCarpetaServ = filSucu["dbf"].ToString() + @"\tmp_expo\" + nomExpo + @"\" + _nomUsuario + @"\" + idActSucu;
                servidorSucu = filSucu["SERVIDORSUCU"].ToString();
                actuRutaBaja = filSucu["RUTA_BAJA"].ToString();
                actuAgenBaja = filSucu["AGEN_BAJA"].ToString();

                break;
            }

            

            if (!Directory.Exists(tmpCarpetaLocal))
            {
                Directory.CreateDirectory(tmpCarpetaLocal);
            }

            if (!Directory.Exists(tmpCarpetaServ))
            {
                Directory.CreateDirectory(tmpCarpetaServ); //COLOCAR UN TRY PARA CUANDO NO PUEDA ENTRAR AL SERVIDOR PARA CREAR LAS CARPETAS DE COPIADO
            }

            for (int i = 0; i < _tablas.GetLength(1); i++)
            {
                _tablasNombre = _tablas[0, i];
                _esTablaBdf = _tablas[1, i];
                _tablasBDFCampos = _tablas[2, i];
                _esTablaSql = _tablas[3, i];
                _tablasSQLCampos = _tablas[4, i];
                _tablasNomDestino = _tablas[5, i];
                _tablasNomDestinoCampos = _tablas[6, i];
                _tablasNomDestiCamposComparar = _tablas[7, i];

                if (_funcion.PingServ(servidorSucu))
                {

                    if (Convert.ToBoolean(_esTablaBdf))//CUANDO EL TRASPASO ES EN BDF
                    {

                    
                        int actualProcesoDBF = 1;
                        int totalProcesoDBF = 3;
                        _funcion.Cargando(this, stripPBEstatus, 0, actualProcesoDBF, totalProcesoDBF, stripSLEstatus, "Preparando dbf " + _tablasNombre);

                        //COPIAR LOS DBF EN EL MISMO SERVIDOR CON OTRO NOMBRE
                        var servFptOrigen = Path.Combine(carpetaServ, _tablasNombre + ".fpt");
                        var servFptDestino = Path.Combine(tmpCarpetaServ, "expo_" + _tablasNombre + ".fpt");
                        //COPIAR LOS ARCHIVOS A LAS CARPETAS QUE EL SISTEMA CREA
                        var localFptDestino = Path.Combine(tmpCarpetaLocal, _tablasNombre + ".fpt");

                        //AQUI TE QUEDASTE
                        var servDbfOrigen = Path.Combine(carpetaServ.ToString(), _tablasNombre + ".dbf");
                        var servDbfDestino = Path.Combine(tmpCarpetaServ, "expo_" + _tablasNombre + ".dbf");
                        var localDbfDestino = Path.Combine(tmpCarpetaLocal, _tablasNombre + ".dbf");

                        if (File.Exists(servFptOrigen))
                        {

                            File.Copy(servFptOrigen, servFptDestino, true);

                            _funcion.Cargando(this, stripPBEstatus, 0, 2, 3, stripSLEstatus, "Preparando copiado fpt " + _tablasNombre);
                            //Thread.Sleep(1000);

                            CopyFile(servFptDestino, localFptDestino);
                            File.Delete(servFptDestino);

                        }

                        if (File.Exists(servDbfOrigen))
                        {
                            File.Copy(servDbfOrigen, servDbfDestino, true);

                            _funcion.Cargando(this, stripPBEstatus, 0, 2, 3, stripSLEstatus, "Preparando copiado dbf " + _tablasNombre);
                            //Thread.Sleep(1000);

                            CopyFile(servDbfDestino, localDbfDestino);
                            File.Delete(servDbfDestino);
                        }



                        _funcion.Cargando(this, stripPBEstatus, 0, actualProcesoDBF, totalProcesoDBF, stripSLEstatus, "Preparando para importación: " + _tablasNombre);
                        //Thread.Sleep(5000);


                        string cadena = @"Driver={Microsoft Visual Foxpro Driver};UID=;SourceType=DBF;SourceDB=" + tmpCarpetaLocal + " ;Exclusive=No;SHARED=YES;collate=Machine;NULL=NO;DELETED=NO;BACKGROUNDFETCH=YES;";
                        OdbcConnection con = new OdbcConnection();  //se crea la variable de conexion para el dbf
                        con.ConnectionString = cadena;              //se crea la conexion
                        con.Open();
                        string consulta = "SELECT * FROM " + _tablasNombre;
                        OdbcDataAdapter adapter = new OdbcDataAdapter(consulta, con);
                        DataTable dtDBF = new DataTable();

                        try
                        {
                            adapter.Fill(dtDBF);
                            con.Close();

                        }
                        catch (Exception ex)
                        {
                            con.Close();
                            dtDBF.Clear();

                            ProcessStartInfo info = null;
                            //String _RutaTabla = @"tmp_expo\" + nomExpo + @"\" + almdtSucu.ToString() + @"\" + _tablasNombre;
                            //String _RutaTabla = tmpCarpetaLocal + @"\" + _tablasNombre;
                            String _RutaTabla = @"tmp_expo\" + nomExpo + @"\" + _nomUsuario + @"\" + idActSucu + @"\" + _tablasNombre;
                            //String _Alm = _dtTablas.Rows[j]["almacen"].ToString();
                            info = new ProcessStartInfo(@"copiartabla.exe", '"' + _RutaTabla + '"');
                            //info = new ProcessStartInfo(@"C:\CRM\CRM_VentasDos.exe", "" + cvsuc + " " + Fecha + "");
                            info.WindowStyle = ProcessWindowStyle.Hidden;
                            programa = Process.Start(info);
                            programa.WaitForExit(1000 * 60 * 900);
                            programa.StartInfo.UseShellExecute = false;
                            if (!programa.HasExited)
                            {
                                programa.Kill();
                            }
                            
                            /////////////////////////////////////////////////
                            if (File.Exists(_RutaTabla + ".txt"))
                            {

                                String line;
                                Char delimiter = '\t';

                                DataRow rowdbf;

                                // Read the file and display it line by line.
                                System.IO.StreamReader file =
                                    new System.IO.StreamReader(_RutaTabla + ".txt");
                                while ((line = file.ReadLine()) != null)
                                {
                                    //try
                                    //{
                                    line = line.Replace("\"", "");
                                    string[] x = line.Split(delimiter);

                                    DataColumn col;
                                    rowdbf = dtDBF.NewRow();
                                    //int iColtxt = 0;
                                    for (int iColDbf = 0; iColDbf < dtDBF.Columns.Count; iColDbf++)
                                    {
                                        col = dtDBF.Columns[iColDbf];

                                        if (col.DataType.ToString() == "System.DateTime")
                                        {
                                            //try
                                            //{

                                            if (!(x[iColDbf] == " "))
                                            {
                                                String[] fechPedacitos = x[iColDbf].Split('/');
                                                String fecha = fechPedacitos[2] + "-" + fechPedacitos[0] + "-" + fechPedacitos[1];
                                                if (!(fechPedacitos[2] == "") && !(fechPedacitos[0] == "") && !(fechPedacitos[1] == ""))
                                                {
                                                    DateTime fechaco = Convert.ToDateTime(fecha);
                                                    rowdbf[iColDbf] = fechaco;
                                                }

                                            }
                                            else
                                            {
                                                DateTime? fechaco = null;
                                                rowdbf[iColDbf] = fechaco.GetValueOrDefault();
                                            }

                                        }
                                        else if (col.DataType.ToString() == "System.Decimal")
                                        {
                                            if (x[iColDbf].ToString() == "" || x[iColDbf].ToString() == "*****")
                                            {
                                                Decimal Valordeci = 0;
                                                rowdbf[iColDbf] = Valordeci;
                                            }

                                        }
                                        else if (col.DataType.ToString() == "System.Boolean")
                                        {
                                            if (x[iColDbf].ToString() == "T")
                                            {
                                                rowdbf[iColDbf] = true;
                                            }
                                            else if (x[iColDbf].ToString() == "F")
                                            {
                                                rowdbf[iColDbf] = false;
                                            }

                                        }
                                        else
                                        {
                                            var columnatipo = col.DataType.ToString();
                                            var valorCelda = x[iColDbf].ToString();
                                            var nombreColumna = col.ColumnName;
                                            rowdbf[iColDbf] = x[iColDbf].ToString();
                                        }


                                    }
                                    dtDBF.Rows.Add(rowdbf);
                                }

                                file.Close();
                                //throw;
                            }


                        }


                        String[] archivos = Directory.GetFiles(tmpCarpetaLocal, _tablasNombre + ".*");

                        foreach (String file in archivos)
                        {
                            if (File.Exists(file))
                            {
                                //MessageBox.Show(file);
                                File.Delete(file);
                            }
                        }

                        dtDBF.Columns.Add("id_sucursalalm", typeof(String));

                        for (int iAlm = 0; iAlm < dtDBF.Rows.Count; iAlm++)
                        {
                            _funcion.Cargando(this, stripPBEstatus, 0, iAlm, dtDBF.Rows.Count, stripSLEstatus, "Asignando el almacen: " + _tablasNombre);
                            dtDBF.Rows[iAlm]["ID_SUCURSALALM"] = idActSucu;
                        }



                        if (chRutabaja.Checked)
                        {
                            DataRow[] _dataRow;
                            DataTable _tmpDtDBF;
                            int posicion = 0;
                            _dataRow = dtDBF.Select("C_RUTA <> " + actuRutaBaja);

                            _tmpDtDBF = dtDBF.Clone();
                            foreach (DataRow fila in _dataRow)
                            {
                                _tmpDtDBF.ImportRow(fila);
                            }
                            dtDBF.Clear();
                            _dataRow = null;
                            _dataRow = _tmpDtDBF.Select();
                            foreach (DataRow fila in _dataRow)
                            {
                                _funcion.Cargando(this, stripPBEstatus, 0, posicion++, _dataRow.Count(), stripSLEstatus, "Filtrando clientes: " + _tablasNombre);
                                dtDBF.ImportRow(fila);
                            }
                        }

                        //MessageBox.Show(""+ dtDBF.Rows.Count);
                        using (SqlConnection _con = new SqlConnection(_CadenaConexion))
                        {

                            if (_con.State == ConnectionState.Closed)
                            {
                                _con.Open();
                            }

                            int totaldtDBF = dtDBF.Rows.Count;
                            int totalCiente;
                            String[] campo = _tablasNomDestiCamposComparar.ToString().Split('-');
                            String[] camposguardar = _tablasNomDestinoCampos.ToString().Split(',');
                            String _sql = "";
                            DataTable _datos = new DataTable();
                            DataRow dr;
                            _datos.Columns.Add("campo", typeof(String));
                            _datos.Columns.Add("valor", typeof(String));
                            _datos.Columns.Add("tipo", typeof(String));
                            for (int ii = 0; ii < totaldtDBF; ii++)
                            {
                                _datos.Clear();
                                //_funcion.Cargando(this, stripPBEstatus, 0, ii, dtDBF.Rows.Count, stripSLEstatus, "Checando existencia");
                                //_dtTmpCliente.Clear();
                                _dtTmpCliente = _funcion.llenar_dt(_tablasNomDestino, _tablasNomDestinoCampos, "WHERE " + campo[0].ToString() + " = " + dtDBF.Rows[ii][campo[0].ToString()] + " AND " + campo[1].ToString() + " = " + dtDBF.Rows[ii][campo[1].ToString()]);

                                totalCiente = _dtTmpCliente.Rows.Count;

                                for (int iii = 0; iii < camposguardar.Count(); iii++)
                                {
                                    dr = _datos.NewRow();
                                    dr["campo"] = camposguardar[iii];
                                    dr["valor"] = dtDBF.Rows[ii][camposguardar[iii]];
                                    dr["tipo"] = "varchar";
                                    _datos.Rows.Add(dr);
                                }

                                if (!(totalCiente > 0))
                                {
                                    _funcion.Cargando(this, stripPBEstatus, 0, ii, totaldtDBF, stripSLEstatus, "Nuevo registro: " + ii + "/" + totaldtDBF);
                                    _sql = _funcion._sql(_datos, _tablasNomDestino, "nuevo", "");
                                }
                                else
                                {

                                    _funcion.Cargando(this, stripPBEstatus, 0, ii, totaldtDBF, stripSLEstatus, "Modificando registro: " + ii + " / " + totaldtDBF);
                                    _sql = _funcion._sql(_datos, _tablasNomDestino, "modificar", "WHERE " + campo[0].ToString() + " = " + dtDBF.Rows[ii][campo[0].ToString()] + " AND " + campo[1].ToString() + " = " + dtDBF.Rows[ii][campo[1].ToString()]);
                                }
                                try
                                {
                                    //_con.Open();
                                    SqlCommand comando = new SqlCommand(_sql, _con);
                                    //resultado = comando.ExecuteNonQuery();
                                    comando.CommandTimeout = 10000;
                                    comando.ExecuteNonQuery();
                                    //            __dblocal.Close();
                                }
                                catch (Exception e)
                                {

                                    MessageBox.Show(e.Message, "¡Espera!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                            }

                        }
                    }
                else if (Convert.ToBoolean(_esTablaSql))//CUANDO EL TRASPASO ES EN SQL
                {

                }
                else
                {
                    //this.Invoke((MethodInvoker)delegate
                    //{
                    //    //MessageBox.Show("La tabla " + _tablasNombre + " no esta habilitada para actualizar", "¡Espera!",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    //});
                }

            } else
                    {
                this.Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show("No se pudo conectar al servidor", "¡Espera!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });
            }
        }
            

            _funcion.Cargando(this, stripPBEstatus, 0, 1, 1, stripSLEstatus, "Proceso terminado..." );
            _funcion.DesabilitarControles(this, true, btnActualizar);
            Thread.Sleep(5000);
            _funcion.Cargando(this, stripPBEstatus, 0, 0, 1, stripSLEstatus, "...");
            ThreadActualizar = null;

        }

       

        public void CopyFile(string Filein, string fileOut)
        {
            Stream st;
            try
            {
                st = File.OpenRead(Filein);

                byte[] arrbt = ReadFully(st);

                CreateFile(fileOut, arrbt);
                //copiarTablas.Abort();

                //copiarTablas = null;
                st.Close();
                //return true;
            }
            catch (Exception ee)
            {
                //copiarTablas = null;
                MessageBox.Show(ee.Message);
                //return false;
            }
            finally
            {

            }

        }

        public byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public void CreateFile(string fileOut, byte[] bta)
        {
            FileStream fs = File.Create(fileOut);
            //barraProgreso.Value = 0;
            //barraProgreso.Maximum = bta.Length;

            for (int i = 0; i < bta.Length; i++)
            {
                fs.WriteByte(bta[i]);
                if (i % 5000 == 0)
                {
                    _funcion.Cargando(this, stripPBEstatus, 0, i, bta.Length, stripSLEstatus, "copiando... " + i / 1024 + " kb de " + bta.Length / 1024 + " kb");
                    //barraProgreso.Value = i;
                    //Application.DoEvents();
                }
            }
            //_funcion.Cargando(this, barraProgreso, 10, bta.Length, bta.Length, lblMensaje, "listo Tabla: "+ nombreTabla);
            fs.Close();
        }

    }
}
