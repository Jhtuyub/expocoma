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
    public partial class FrmImporTablas : Form
    {
        public String _CadenaConexion;
        public String nomExpo;

        funciones _funcion = new funciones();
        private Thread copiarTablas;
        private Process programa;

        DataTable _dtSucursal;
        DataTable _dtTablas;

        DataTable _dtDbfTabla;
        DataTable _dtTblTablaSql;

        public FrmImporTablas()
        {
            InitializeComponent();
            
        }

        private void FrmImporTablas_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(_CadenaConexion);
            _funcion._SQLCadenaConexion = _CadenaConexion;
            //_funcion._TIPObasedatos = "sqlserver";
            //_funcion.cargar_datos(dgvSucursal, "tbl_sucursal");
            _dtSucursal = _funcion.llenar_form("tbl_sucursal");
            _dtSucursal.Columns.Add("importar", typeof(Boolean));

            for (int i = 0; i < _dtSucursal.Rows.Count; i++)
            {
                _dtSucursal.Rows[i]["importar"] = false;
            }

            picbCargando.Visible = false;

            //_dtSucursal.Columns["importar"].DefaultValue = false;
            dgvSucursal.DataSource = _dtSucursal;
            dgvSucursal.Columns["importar"].DisplayIndex = 0;
            //dgvSucursal.Columns["importar"].
            dgvSucursal.Columns["id"].Visible = false;
            dgvSucursal.Columns["id_catsucursal"].Visible = false;
            dgvSucursal.Columns["anfitrion"].ReadOnly = true;
            dgvSucursal.Columns["anfitrion"].Width = 55;
            dgvSucursal.Columns["almacen"].ReadOnly = true;
            dgvSucursal.Columns["sucursal"].ReadOnly = true;
            dgvSucursal.Columns["servidor"].ReadOnly = true;
            dgvSucursal.Columns["dbf"].ReadOnly = true;
            dgvSucursal.Columns["usuario"].ReadOnly = true;
            dgvSucursal.Columns["db"].ReadOnly = true;
            dgvSucursal.Columns["contrasena"].Visible = false;
            dgvSucursal.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvSucursal.DefaultCellStyle.SelectionBackColor = Properties.Settings.Default.filaSeleccion;
            dgvSucursal.AlternatingRowsDefaultCellStyle.BackColor = Properties.Settings.Default.filaAltern;

            String[,] _cbdatos = {
                {
                    "articulo",
                    "fam_arti",
                    "porpieza",
                    "cliente",
                    "prefijos",
                    "proveedo",
                    "statarti",
                    "estatus"
                },
                { "false", "false", "false","false", "false", "false", "false", "false"},
                {
                    "SELECT * FROM VW_MTL_SYSTEM_ITEMS_B_CMA_2 WHERE ORGANIZATION_ID = @ORGANIZATION_ID",//articulo
                    "SELECT * FROM PO_FAMILIAS_COMA WHERE ORGANIZATION_ID = @ORGANIZATION_ID",//fam_arti
                    "",//porpieza
                    "",//cliente
                    "",//prefijos
                    "SELECT * FROM VW_PO_VENDORS_INT WHERE organizacion = @ORGANIZATION_ID",//proveedo
                    "SELECT statarti.ITEM_NUMBER, statarti.STATUS_NUMBER, estatus.CONCEPTO, articulos.DESCRIPTION, articulos.ATTRIBUTE3, articulos.ATTRIBUTE2, articulos.NO_PROV_AFECTA_PRECIO FROM po_supp_item_status_int statarti INNER JOIN PO_CATALOG_STATUS estatus ON statarti.STATUS_NUMBER = estatus.STATUS_NUMBER INNER JOIN VW_MTL_SYSTEM_ITEMS_B_CMA_2 articulos ON statarti.ITEM_NUMBER = articulos.SEGMENT1 WHERE (statarti.ORGANIZATION_ID = @ORGANIZATION_ID) AND (statarti.STATUS_NUMBER <= 5 OR statarti.STATUS_NUMBER IN (12, 19, 20, 34)) AND (articulos.ORGANIZATION_ID = @ORGANIZATION_ID) ORDER BY statarti.ITEM_NUMBER, CONVERT(SMALLDATETIME, statarti.FECHA_CREACION, 105), CAST(statarti.STATUS_NUMBER AS numeric)",//statarti
                    "SELECT * FROM po_catalog_status"//estatus
                },
                { "false", "false", "false","false", "false", "false", "false", "false"},
                {
                    "ID_SUCURSALALM-ID_SUCURSALALM,SEGMENT1-C_ARTI,NO_PROV_AFECTA_PRECIO-C_PROVE,NO_PROV_AFECTA_PRECIO-C_PROVE2,SEGMENT2-FAMI_ARTI,DESCRIPTION-DES_ARTI,DESCRIPTION-DES_ART2,ATTRIBUTE2-CAP_ARTI,ATTRIBUTE3-EMPAQUE2,INVENTORY_ITEM_STATUS_CODE-STATUS,ATTRIBUTE13-CAJA,ATTRIBUTE14-UNIDAD,ATTRIBUTE15-EXHIBIDOR",
                    "ID_SUCURSALALM-ID_SUCURSALALM,SEGMENT2-FAMI_ARTI,DESCRIPCION_SEG2-NOMBRE,IVA-IVA,IVA-IVA2", //
                    "",
                    "",
                    "",
                    "ID_SUCURSALALM-ID_SUCURSALALM,SEGMENT1-C_PROVE,SEGMENT1-C_PROVE2,VENDOR_NAME-DESCRI,ATTRIBUTE9-RESP_COMA", //
                    "ID_SUCURSALALM-ID_SUCURSALALM,ITEM_NUMBER-C_ARTI,STATUS_NUMBER-STATUS,CONCEPTO-CONCEPTO,DESCRIPTION-DES_ARTI,ATTRIBUTE3-EMPAQUE,ATTRIBUTE2-CAP_ARTI,NO_PROV_AFECTA_PRECIO-C_PROVE", //,status_number-STATUS
                    "ID_SUCURSALALM-ID_SUCURSALALM,STATUS_NUMBER-STATUS,CONCEPTO-CONCEPTO,FECHA_ALTA-FECHA_ALT"
                },
               
                //"ID_SUCURSALALM-ID_SUCURSALALM,SEGMENT1-C_ARTI,SEGMENT2-FAMI_ARTI,DESCRIPTION-DES_ARTI,DESCRIPTION-DES_ART2,ATTRIBUTE2-CAP_ARTI",
                //{ "Almacen", ""}
            };

            _dtTablas = new DataTable("tablas");
            _dtTablas.Columns.Add("tablas");
            _dtTablas.Columns.Add("dbf", typeof(Boolean));
            _dtTablas.Columns.Add("tablassql");
            _dtTablas.Columns.Add("sql", typeof(Boolean));
            _dtTablas.Columns.Add("campos");

            DataRow dr;
            //
            var totalArray = _cbdatos.GetLength(1);
            for (int i = 0; i < totalArray; i++)
            {
                dr = _dtTablas.NewRow();
                dr["tablas"] = _cbdatos[0, i];
                dr["dbf"] = _cbdatos[1, i];
                dr["tablassql"] = _cbdatos[2, i];
                dr["sql"] = _cbdatos[3, i];
                dr["campos"] = _cbdatos[4, i];
                //dr["opcion"] = _cbdatos[1, i];
                _dtTablas.Rows.Add(dr);
            }
            
            dgvTablas.DataSource = _dtTablas;
            //dgvTablas.Columns["tablassql"].Visible = false;
            //dgvTablas.Columns["campos"].Visible = false;
            dgvTablas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvTablas.DefaultCellStyle.SelectionBackColor = Properties.Settings.Default.filaSeleccion;
            dgvTablas.AlternatingRowsDefaultCellStyle.BackColor = Properties.Settings.Default.filaAltern;
            
        }

        private void FrmImporTablas_FormClosing(object sender, FormClosingEventArgs ee)
        {
            if (!(copiarTablas==null))
            {
                copiarTablas.Suspend();
                var detener = MessageBox.Show("¿Desea detener la importación?", "¡Aviso!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (detener == DialogResult.Yes)
                {
                    copiarTablas.Resume();
                    btnImportar.Text = "Importar";
                    copiarTablas.Abort();
                    copiarTablas = null;
                    lblMensaje.Text = "...";
                    barraProgreso.Value = 0;
                    this.Invoke(new CamposEnableDelegate(_funcion.CamposEnabled), this, true, btnImportar, "Importar");
                    //this.Invoke(new CamposEnableDelegate(_funcion.CamposEnabled), this, true, btnImportar);

                    FrmIndex.opcPartSuc.Enabled = true;
                    FrmIndex.opcImporTabla.Enabled = true;

                    ee.Cancel = false;
                    GC.Collect();
                }
                else
                {
                    copiarTablas.Resume();
                    ee.Cancel = true;
                }
               
            }
            else
            {
                FrmIndex.opcPartSuc.Enabled = true;
                FrmIndex.opcImporTabla.Enabled = true;
                GC.Collect();
            }
            
        }

        private void FrmImporTablas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        public void CheckTodos(DataTable DT, String COLUMNA, Boolean CHECKED)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                if (COLUMNA == "sql")
                {
                    if (DT.Rows[i]["tablas"].ToString() == "porpieza")
                    {
                        DT.Rows[i]["dbf"] = CHECKED;
                    }
                    else if (DT.Rows[i]["tablas"].ToString() == "cliente")
                    {
                        DT.Rows[i]["dbf"] = CHECKED;
                    }
                    else if (DT.Rows[i]["tablas"].ToString() == "prefijos")
                    {
                        DT.Rows[i]["dbf"] = CHECKED;
                    }
                    else
                    {
                        DT.Rows[i][COLUMNA] = CHECKED;
                    }

                }else
                {
                    DT.Rows[i][COLUMNA] = CHECKED;
                }

            }
        }

        private void checkBTodosSucursales_Click(object sender, EventArgs e)
        {
            CheckTodos(_dtSucursal, "importar", checkBTodosSucursales.Checked);
        }

        private void checkbxDbfTodos_Click(object sender, EventArgs e)
        {
            
            if (checkbxDbfTodos.Checked)
            {
                checkbxSqlTodos.Checked = false;
                CheckTodos(_dtTablas, "sql", !checkbxDbfTodos.Checked);
            }
            CheckTodos(_dtTablas, "dbf", checkbxDbfTodos.Checked);

        }

        private void checkbxSqlTodos_Click(object sender, EventArgs e)
        {

            if (checkbxSqlTodos.Checked)
            {
                checkbxDbfTodos.Checked = false;
                CheckTodos(_dtTablas, "dbf", !checkbxSqlTodos.Checked);
            }
            CheckTodos(_dtTablas, "sql", checkbxSqlTodos.Checked);

        }
        
        private void dgvTablas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (dgvTablas.Columns[e.ColumnIndex].Name == "dbf")
            {
                checkbxDbfTodos.Checked = false;
                for (int i = 0; i < _dtTablas.Rows.Count; i++)
                {

                    if (_dtTablas.Rows[i]["tablas"].ToString() == dgvTablas.CurrentRow.Cells["tablas"].Value.ToString())
                    {

                        if (Convert.ToBoolean(_dtTablas.Rows[i]["sql"]) == true)
                        {
                            _dtTablas.Rows[i]["dbf"] = true;
                            _dtTablas.Rows[i]["sql"] = false;

                        }
                        //else
                        //{
                        //    _dtTablas.Rows[i]["sql"] = false;
                        //}

                        break;
                    }
                    

                }

            }
            else if (dgvTablas.Columns[e.ColumnIndex].Name == "sql")
            {
                checkbxSqlTodos.Checked = false;
                for (int i = 0; i < _dtTablas.Rows.Count; i++)
                {
                    //porpieza
                    //cliente
                    //prefijos
                    //|| dgvTablas.CurrentRow.Cells["tablas"].Value.ToString() == "porpieza" || dgvTablas.CurrentRow.Cells["tablas"].Value.ToString() == "prefijos"
                    if ((_dtTablas.Rows[i]["tablas"].ToString() == "porpieza" && dgvTablas.CurrentRow.Cells["tablas"].Value.ToString() == "porpieza") ||
                        (_dtTablas.Rows[i]["tablas"].ToString() == "cliente" && dgvTablas.CurrentRow.Cells["tablas"].Value.ToString()=="cliente") ||
                        (_dtTablas.Rows[i]["tablas"].ToString() == "prefijos" && dgvTablas.CurrentRow.Cells["tablas"].Value.ToString() == "prefijos"))
                    {
                        MessageBox.Show(_dtTablas.Rows[i]["tablas"].ToString()+" no disponible en esta opción");
                        _dtTablas.Rows[i]["sql"] = false;
                        break; 
                    }
                    else
                    {
                        if (_dtTablas.Rows[i]["tablas"].ToString() == dgvTablas.CurrentRow.Cells["tablas"].Value.ToString())
                        {

                            if (Convert.ToBoolean(_dtTablas.Rows[i]["dbf"]) == true)
                            {
                                _dtTablas.Rows[i]["sql"] = true;
                                _dtTablas.Rows[i]["dbf"] = false;

                            }
                            break;
                        }
                    }
                    
                }
            }
            
            }

        private void dgvSucursal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSucursal.Columns[e.ColumnIndex].Name == "importar")
            {
                checkBTodosSucursales.Checked = false;

            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            int impsucu = 0;
            for (int i = 0; i < _dtSucursal.Rows.Count; i++)
            {

                if (Convert.ToBoolean(_dtSucursal.Rows[i]["importar"].ToString()) == true)
                {
                    impsucu++;
                    break;
                }

            }

            int impTabla = 0;
            for (int i = 0; i < _dtTablas.Rows.Count; i++)
            {

                if (Convert.ToBoolean(_dtTablas.Rows[i]["dbf"].ToString()) == true || Convert.ToBoolean(_dtTablas.Rows[i]["sql"].ToString()) == true)
                {
                    impTabla++;
                    break;
                }

            }

            if (impsucu > 0)
            {
                if (impTabla > 0)
                {
                    if (copiarTablas == null)
                    {
                        btnImportar.Text = "Detener";
                        copiarTablas = new Thread(PasarTablas);
                        copiarTablas.IsBackground = true;
                        copiarTablas.Start();
                    }
                    else
                    {
                        copiarTablas.Suspend();
                        var detener = MessageBox.Show("¿Desea detener la importación?", "¡Aviso!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (detener == DialogResult.Yes)
                        {
                            btnImportar.Text = "Importar";
                            copiarTablas.Resume();
                            copiarTablas.Abort();
                            copiarTablas = null;
                            lblMensaje.Text = "...";
                            barraProgreso.Value = 0;
                            this.Invoke(new CamposEnableDelegate(_funcion.CamposEnabled), this, true, btnImportar, "Importar");
                        }
                        else
                        {
                            copiarTablas.Resume();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("¡Selecciona una tabla!", "Espera", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                    

            }
            else
            {
                MessageBox.Show("¡Selecciona una sucursal!", "Espera", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
                
        }

        delegate void PosicionSucuDelegate(int i);
        public void PosicionSucu(int i)
        {
            dgvSucursal.CurrentCell = dgvSucursal.Rows[i].Cells[2];
        }

        delegate void PosicionTablaDelegate(int j);
        public void PosicionTabla(int j)
        {
            dgvTablas.CurrentCell = dgvTablas.Rows[j].Cells[0];
        }
        delegate void CamposEnableDelegate(Form _Formulario, Boolean _Enabled, Button _btnHilo, String btnTexto);

        public void PasarTablas()
        {
            
            this.Invoke(new CamposEnableDelegate(_funcion.CamposEnabled),this,false,btnImportar, "Detener");

            for (int i = 0; i < _dtSucursal.Rows.Count; i++)
            {
                var impdtSucu = _dtSucursal.Rows[i]["importar"];
                var almdtSucu = _dtSucursal.Rows[i]["almacen"];
                var rutadtSucu = _dtSucursal.Rows[i]["dbf"];

                if (Convert.ToBoolean(impdtSucu) == true)
                {

                    foreach (DataGridViewRow rowSuc in dgvSucursal.Rows)
                    {
                        if (rowSuc.Cells["almacen"].Value == almdtSucu)
                        {
                            this.Invoke(new PosicionSucuDelegate(PosicionSucu), i);
                        }

                    }

                    //String carpetaLocal = Application.StartupPath  + @"\tmp_expo\" + nomExpo+@"\" + almdtSucu.ToString();
                    //String carpetaTmpExpo = rutadtSucu.ToString() + @"\tmp_expo";
                    //if (!Directory.Exists(carpetaLocal))
                    //{
                    //    Directory.CreateDirectory(carpetaLocal);
                    //}

                    //if (!Directory.Exists(carpetaTmpExpo))
                    //{
                    //    Directory.CreateDirectory(carpetaTmpExpo);
                    //}


                    for (int j = 0; j < _dtTablas.Rows.Count; j++)
                    {
                        var dbfdtTablas = _dtTablas.Rows[j]["dbf"];
                        var sqldtTablas = _dtTablas.Rows[j]["sql"];
                        var tbldtTablas = _dtTablas.Rows[j]["tablas"];

                        foreach (DataGridViewRow row in dgvTablas.Rows)
                        {
                            if (row.Cells[0].Value == tbldtTablas)
                            {

                                this.Invoke(new PosicionTablaDelegate(PosicionTabla), j);
                                //break;

                            }

                        }

                        if (Convert.ToBoolean(dbfdtTablas) == true)
                        {

                            String carpetaLocal = Application.StartupPath + @"\tmp_expo\" + nomExpo + @"\" + almdtSucu.ToString();
                            String carpetaTmpExpo = rutadtSucu.ToString() + @"\tmp_expo";
                            if (!Directory.Exists(carpetaLocal))
                            {
                                Directory.CreateDirectory(carpetaLocal);
                            }

                            if (!Directory.Exists(carpetaTmpExpo))
                            {
                                Directory.CreateDirectory(carpetaTmpExpo);
                            }

                            //for (int iPosi = 0; iPosi < _dtTablas.Rows.Count; iPosi++)
                            //{
                            //foreach (DataGridViewRow row in dgvTablas.Rows)
                            //{
                            //    if (row.Cells[0].Value == tbldtTablas)
                            //    {

                            //        this.Invoke(new PosicionTablaDelegate(PosicionTabla), j);
                            //        //break;

                            //    }

                            //}

                            _funcion.Cargando(this, barraProgreso, 0, 1, 3, lblMensaje, "Preparando dbf " + tbldtTablas.ToString());

                            //COPIAR LOS DBF EN EL MISMO SERVIDOR CON OTRO NOMBRE
                            var servFptOrigen = Path.Combine(rutadtSucu.ToString(), tbldtTablas.ToString() + ".fpt");
                            var servFptDestino = Path.Combine(carpetaTmpExpo, "expo_" + tbldtTablas.ToString() + ".fpt");
                            //COPIAR LOS ARCHIVOS A LAS CARPETAS QUE EL SISTEMA CREA
                            var localFptDestino = Path.Combine(carpetaLocal, tbldtTablas.ToString() + ".fpt");

                            var servDbfOrigen = Path.Combine(rutadtSucu.ToString(), tbldtTablas.ToString() + ".dbf");
                            var servDbfDestino = Path.Combine(carpetaTmpExpo, "expo_" + tbldtTablas.ToString() + ".dbf");
                            var localDbfDestino = Path.Combine(carpetaLocal, tbldtTablas.ToString() + ".dbf");

                            if (File.Exists(servFptOrigen))
                            {

                                File.Copy(servFptOrigen, servFptDestino, true);

                                _funcion.Cargando(this, barraProgreso, 0, 2, 3, lblMensaje, "Preparando para copiar fpt " + tbldtTablas.ToString());
                                //Thread.Sleep(1000);

                                CopyFile(servFptDestino, localFptDestino);

                            }

                            if (File.Exists(servDbfOrigen))
                            {
                                File.Copy(servDbfOrigen, servDbfDestino, true);

                                _funcion.Cargando(this, barraProgreso, 0, 2, 3, lblMensaje, "Preparando para copiar dbf " + tbldtTablas.ToString());
                                //Thread.Sleep(1000);

                                CopyFile(servDbfDestino, localDbfDestino);
                            }

                            int totalProcesoDBF = 4;
                            int actualProcesoDBF = 1;
                            _funcion.Cargando(this, barraProgreso, 0, actualProcesoDBF, totalProcesoDBF, lblMensaje, "Preparando para importación: " + tbldtTablas.ToString());
                            Thread.Sleep(5000);


                            string cadena = @"Driver={Microsoft Visual Foxpro Driver};UID=;SourceType=DBF;SourceDB=" + carpetaLocal + " ;Exclusive=No;SHARED=YES;collate=Machine;NULL=NO;DELETED=NO;BACKGROUNDFETCH=YES;";
                            OdbcConnection con = new OdbcConnection();  //se crea la variable de conexion para el dbf
                            con.ConnectionString = cadena;              //se crea la conexion
                            con.Open();
                            string consulta = "SELECT * FROM " + tbldtTablas.ToString();
                            OdbcDataAdapter adapter = new OdbcDataAdapter(consulta, con);
                            DataTable dtDBF = new DataTable();

                            try
                            {
                                adapter.Fill(dtDBF);
                                con.Close();

                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show(ex.Message);
                                con.Close();

                                dtDBF.Clear();


                                ProcessStartInfo info = null;
                                String _RutaTabla = @"tmp_expo\" + nomExpo + @"\" + almdtSucu.ToString() + @"\" + tbldtTablas.ToString();
                                //String _Alm = _dtTablas.Rows[j]["almacen"].ToString();
                                info = new ProcessStartInfo(@"copiartabla.exe", '"' + _RutaTabla + '"');
                                //info = new ProcessStartInfo(@"C:\CRM\CRM_VentasDos.exe", "" + cvsuc + " " + Fecha + "");
                                info.WindowStyle = ProcessWindowStyle.Hidden;
                                programa = Process.Start(info);
                                programa.WaitForExit(1000 * 60 * 900);
                                programa.StartInfo.UseShellExecute = false;

                                //programa.Close();

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

                            dtDBF.Columns.Add("id_sucursalalm", typeof(String));

                            for (int iAlm = 0; iAlm < dtDBF.Rows.Count; iAlm++)
                            {
                                _funcion.Cargando(this, barraProgreso, 0, iAlm, dtDBF.Rows.Count, lblMensaje, "Asignando el almacen: " + tbldtTablas.ToString());
                                dtDBF.Rows[iAlm]["ID_SUCURSALALM"] = almdtSucu.ToString();
                            }

                            using (SqlConnection _consql = new SqlConnection(_CadenaConexion))
                            {
                                //bool respuesta = false;
                                SqlTransaction _tran;
                                string _tabla = "dbf_" + tbldtTablas.ToString();
                                if (_consql.State == ConnectionState.Closed)
                                {
                                    _consql.Open();
                                }

                                //_con.Open();
                                actualProcesoDBF++;
                                _funcion.Cargando(this, barraProgreso, 0, actualProcesoDBF, totalProcesoDBF, lblMensaje, "Limpiando: " + tbldtTablas.ToString());
                                String sqlBorrar = "DELETE FROM dbf_" + tbldtTablas.ToString() + " WHERE ID_SUCURSALALM = '" + almdtSucu.ToString() + "'";
                                SqlCommand comando = new SqlCommand(sqlBorrar, _consql);
                                comando.CommandTimeout = 300;
                                comando.ExecuteNonQuery();

                                actualProcesoDBF++;
                                _funcion.Cargando(this, barraProgreso, 0, actualProcesoDBF, totalProcesoDBF, lblMensaje, "Verificando campos: " + tbldtTablas.ToString());
                                _tran = _consql.BeginTransaction();
                                using (SqlBulkCopy bulkCopy =
                                    new SqlBulkCopy(_consql, SqlBulkCopyOptions.KeepNulls & SqlBulkCopyOptions.KeepIdentity, _tran))
                                {

                                    bulkCopy.DestinationTableName = _tabla;
                                    bulkCopy.BulkCopyTimeout = 300;
                                    try
                                    {
                                        //dtDBF.Columns.Count
                                        for (int jj = 0; jj < dtDBF.Columns.Count; jj++)
                                        {
                                            _funcion.Cargando(this, barraProgreso, 0, jj, dtDBF.Columns.Count, lblMensaje, "Preparando campos: " + tbldtTablas.ToString());
                                            bulkCopy.ColumnMappings.Add(dtDBF.Columns[jj].ColumnName.ToString(), dtDBF.Columns[jj].ColumnName.ToString().ToUpper());

                                        }

                                        //bulkCopy.BatchSize = 5000;
                                        bulkCopy.WriteToServer(dtDBF);
                                        _tran.Commit();
                                        //respuesta = true;
                                        actualProcesoDBF++;
                                        _funcion.Cargando(this, barraProgreso, 0, actualProcesoDBF, totalProcesoDBF, lblMensaje, "Información importada: " + tbldtTablas.ToString());
                                        //_dtTablas.Rows[j]["dbf"] = false;
                                    }
                                    catch (Exception exx)
                                    {
                                        //correo.SendError(ex, System.Net.Mail.MailPriority.High, "Las ventas del día " + _fecha + " de la Sucursal " + _suc + "" + ex.StackTrace);
                                        MessageBox.Show(exx.Message);
                                        //respuesta = false;
                                        try
                                        {
                                            _tran.Rollback();
                                        }
                                        catch (Exception)
                                        {

                                            //throw;
                                        }

                                    }
                                    finally
                                    {

                                        _consql.Close();
                                        //copiarTablas = null;
                                        //respuesta = true;
                                    }
                                }

                            }

                        }
                        else if (Convert.ToBoolean(sqldtTablas) == true)
                        {
                            if (!String.IsNullOrEmpty(_dtTablas.Rows[j]["tablassql"].ToString()))
                            {
                                int totalProcesoDBF = 4;
                                int actualProcesoDBF = 1;
                                String CadenaConexionSql = "Data Source=" + _dtSucursal.Rows[i]["servidor"].ToString() + ";"
                                      + "Initial Catalog=" + _dtSucursal.Rows[i]["db"] + ";"
                                      + "Integrated Security=false;"
                                      + "UID=" + _dtSucursal.Rows[i]["usuario"].ToString() + ";"
                                      + "PWD=" + _dtSucursal.Rows[i]["contrasena"].ToString() + ";";
                                //MessageBox.Show("Lo siento, no disponible la importación del SQL " + tbldtTablas, ":.3",MessageBoxButtons.OK, MessageBoxIcon.Information);
                                using (SqlConnection _consqlOrigen = new SqlConnection(CadenaConexionSql))
                                {
                                    if (_consqlOrigen.State == ConnectionState.Closed)
                                    {
                                        _funcion.Cargando(this, barraProgreso, 0, actualProcesoDBF, totalProcesoDBF, lblMensaje, "Conectando a la base de tados");
                                        _consqlOrigen.Open();
                                    }

                                    SqlCommand sqlComTablas = new SqlCommand(_dtTablas.Rows[j]["tablassql"].ToString(), _consqlOrigen);
                                    if (!(_dtTablas.Rows[j]["tablas"].ToString() == "estatus"))
                                    {

                                        sqlComTablas.Parameters.AddWithValue("@ORGANIZATION_ID", _dtSucursal.Rows[i]["ORGANIZATION_ID"]);
                                    }

                                    SqlDataAdapter myAdaptador = new SqlDataAdapter(sqlComTablas);
                                    _dtTblTablaSql = new DataTable();
                                    myAdaptador.Fill(_dtTblTablaSql);

                                }


                                _dtTblTablaSql.Columns.Add("ID_SUCURSALALM", typeof(String));

                                for (int iAlm = 0; iAlm < _dtTblTablaSql.Rows.Count; iAlm++)
                                {
                                    _funcion.Cargando(this, barraProgreso, 0, iAlm, _dtTblTablaSql.Rows.Count, lblMensaje, "Asignando el almacen: " + tbldtTablas.ToString());
                                    _dtTblTablaSql.Rows[iAlm]["ID_SUCURSALALM"] = almdtSucu.ToString();
                                }

                                using (SqlConnection _consql = new SqlConnection(_CadenaConexion))
                                {

                                    SqlTransaction _tranSql;
                                    string _tabla = "dbf_" + tbldtTablas.ToString();
                                    if (_consql.State == ConnectionState.Closed)
                                    {
                                        _consql.Open();
                                    }

                                    //_dtTblArticuloSql = _funcion.EstructuraTabla("dbf_articulo");
                                    _dtDbfTabla = _funcion.EstructuraTabla("dbf_" + tbldtTablas.ToString());

                                    //_dtTblArticuloSql = _funcion.llenar_dt("");
                                    actualProcesoDBF++;
                                    _funcion.Cargando(this, barraProgreso, 0, actualProcesoDBF, totalProcesoDBF, lblMensaje, "Limpiando: " + tbldtTablas.ToString());
                                    String sqlBorrar = "DELETE FROM dbf_" + tbldtTablas.ToString() + " WHERE ID_SUCURSALALM = '" + almdtSucu.ToString() + "'";
                                    SqlCommand comando = new SqlCommand(sqlBorrar, _consql);
                                    comando.CommandTimeout = 300;
                                    comando.ExecuteNonQuery();

                                    _tranSql = _consql.BeginTransaction();
                                    using (SqlBulkCopy bulkCopy =
                                        new SqlBulkCopy(_consql, SqlBulkCopyOptions.KeepNulls & SqlBulkCopyOptions.KeepIdentity, _tranSql))
                                    {

                                        bulkCopy.DestinationTableName = _tabla;
                                        bulkCopy.BulkCopyTimeout = 300;
                                        try
                                        {

                                            string[] campos = _dtTablas.Rows[j]["campos"].ToString().Split(',');
                                            for (int ii = 0; ii < campos.Count(); ii++)
                                            {
                                                _funcion.Cargando(this, barraProgreso, 0, ii, campos.Count(), lblMensaje, "Preparando campos: " + tbldtTablas.ToString());
                                                //MessageBox.Show(campos[ii]);
                                                String[] campo = campos[ii].ToString().Split('-');
                                                //MessageBox.Show(campo[0].ToString()+" "+campo[1].ToString());
                                                bulkCopy.ColumnMappings.Add(campo[0].ToString(), campo[1].ToString());
                                            }



                                            //bulkCopy.BatchSize = 5000;
                                            actualProcesoDBF++;
                                            _funcion.Cargando(this, barraProgreso, 0, actualProcesoDBF, totalProcesoDBF, lblMensaje, "Copiando Información");
                                            bulkCopy.WriteToServer(_dtTblTablaSql);
                                            _tranSql.Commit();
                                            //respuesta = true;
                                            //actualProcesoDBF++;
                                            //_funcion.Cargando(this, barraProgreso, 0, actualProcesoDBF, totalProcesoDBF, lblMensaje, "Información importada: " + tbldtTablas.ToString());
                                            //_dtTablas.Rows[j]["dbf"] = false;
                                        }
                                        catch (Exception exx)
                                        {
                                            //correo.SendError(ex, System.Net.Mail.MailPriority.High, "Las ventas del día " + _fecha + " de la Sucursal " + _suc + "" + ex.StackTrace);
                                            MessageBox.Show(exx.Message);
                                            //respuesta = false;
                                            try
                                            {
                                                _tranSql.Rollback();
                                            }
                                            catch (Exception)
                                            {

                                                //throw;
                                            }

                                        }
                                        finally
                                        {

                                            _consql.Close();
                                            //copiarTablas = null;
                                            //respuesta = true;
                                        }
                                    }

                                }
                            }
                            else
                            {
                                MessageBox.Show(_dtTablas.Rows[j]["tablas"].ToString()+" no esta disponible en esta opción");
                            }
                        }

                        GC.Collect();

                    }
                }
            }
            _funcion.Cargando(this, barraProgreso, 0, 1, 1, lblMensaje, "Proceso terminado: ");
            MessageBox.Show("Proceso terminado", "¡Listo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            copiarTablas = null;
            //btnImportar.Text = "Importar";
            _funcion.Cargando(this, barraProgreso, 0, 0,1, lblMensaje, "...");
            this.Invoke(new CamposEnableDelegate(_funcion.CamposEnabled), this, true, btnImportar, "Importar");
            GC.Collect();
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
                    _funcion.Cargando(this, barraProgreso, 0, i, bta.Length, lblMensaje, "copiando... "+ i / 1024 +" kb de " + bta.Length /1024+" kb");
                    //barraProgreso.Value = i;
                    //Application.DoEvents();
                }
            }
            //_funcion.Cargando(this, barraProgreso, 10, bta.Length, bta.Length, lblMensaje, "listo Tabla: "+ nombreTabla);
            fs.Close();
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
        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
