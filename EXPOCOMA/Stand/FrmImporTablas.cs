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
        public String _nomUsuario;

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
            _funcion.icono(this);
            _funcion.PicCargando(picbCargando);
            //picbCargando.Image = Image.FromFile(Application.StartupPath + @"\recursos\loader.gif");
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
            dgvSucursal.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSucursal.Columns["importar"].DisplayIndex = 0;
            dgvSucursal.Columns["importar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvSucursal.Columns["id"].Visible = false;
            dgvSucursal.Columns["id_catsucursal"].Visible = false;
            dgvSucursal.Columns["anfitrion"].ReadOnly = true;
            dgvSucursal.Columns["anfitrion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvSucursal.Columns["almacen"].ReadOnly = true;
            dgvSucursal.Columns["almacen"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvSucursal.Columns["almacen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSucursal.Columns["almacen"].HeaderText = "alm";
            dgvSucursal.Columns["organization_id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvSucursal.Columns["organization_id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSucursal.Columns["organization_id"].HeaderText = "alm sql";
            dgvSucursal.Columns["sucursal"].ReadOnly = true;
            dgvSucursal.Columns["servidorsucu"].Visible = true;
            dgvSucursal.Columns["servidor"].ReadOnly = true;
            dgvSucursal.Columns["dbf"].ReadOnly = true;
            dgvSucursal.Columns["dbf"].Visible = false;
            dgvSucursal.Columns["servidor"].Visible = false;
            dgvSucursal.Columns["db"].ReadOnly = true;
            dgvSucursal.Columns["db"].Visible = false;
            dgvSucursal.Columns["usuario"].ReadOnly = true;
            dgvSucursal.Columns["usuario"].Visible = false;
            dgvSucursal.Columns["contrasena"].Visible = false;
            dgvSucursal.Columns["ruta_baja"].Visible = false;
            dgvSucursal.Columns["agen_baja"].Visible = false;


            dgvSucursal.DefaultCellStyle.SelectionBackColor = Properties.Settings.Default.filaSeleccion;
            dgvSucursal.AlternatingRowsDefaultCellStyle.BackColor = Properties.Settings.Default.filaAltern;

            String[,] _cbdatos = {
                {
                    "cliente",
                    "agentes", //agentes
                    "rutagen",//rutagen
                    "rutas",//rutas
                    "proveedo",
                    "articulo",
                    "inventa,inv001",//SOLO SE PROGRAMO LA PARTE DE DBF, YA QUE EN SQL NO ESTA AL DIA SOBRE SU INFORMACION
                    "fam_arti",
                    "porpieza",

                    "prefijos",

                    "statarti",
                    "estatus",

                },
                { "false","false","false","false","false", "false", "false","false", "false", "false", "false", "false"},
                {
                    "",//cliente
                    "", //agentes
                    "",//rutagen
                    "",//rutas
                    "SELECT * FROM VW_PO_VENDORS_INT WHERE organizacion = @ORGANIZATION_ID",//proveedo
                    "SELECT * FROM VW_MTL_SYSTEM_ITEMS_B_CMA_2 WHERE ORGANIZATION_ID = @ORGANIZATION_ID",//articulo
                    "",//INV001-INVENTA - SOLO SE PROGRAMO LA PARTE DE DBF, YA QUE EN SQL NO ESTA AL DIA SOBRE SU INFORMACION
                    "SELECT * FROM PO_FAMILIAS_COMA WHERE ORGANIZATION_ID = @ORGANIZATION_ID",//fam_arti
                    "",//porpieza
                    
                    "",//prefijos
                    
                    "SELECT statarti.ITEM_NUMBER, statarti.STATUS_NUMBER, estatus.CONCEPTO, articulos.DESCRIPTION, articulos.ATTRIBUTE3, articulos.ATTRIBUTE2, articulos.NO_PROV_AFECTA_PRECIO FROM po_supp_item_status_int statarti INNER JOIN PO_CATALOG_STATUS estatus ON statarti.STATUS_NUMBER = estatus.STATUS_NUMBER INNER JOIN VW_MTL_SYSTEM_ITEMS_B_CMA_2 articulos ON statarti.ITEM_NUMBER = articulos.SEGMENT1 WHERE (statarti.ORGANIZATION_ID = @ORGANIZATION_ID) AND (statarti.STATUS_NUMBER <= 5 OR statarti.STATUS_NUMBER IN (12, 19, 20, 34)) AND (articulos.ORGANIZATION_ID = @ORGANIZATION_ID) ORDER BY statarti.ITEM_NUMBER, CONVERT(SMALLDATETIME, statarti.FECHA_CREACION, 105), CAST(statarti.STATUS_NUMBER AS numeric)",//statarti
                    "SELECT * FROM po_catalog_status",//estatus
                    
                },
                { "false","false","false","false","false", "false", "false","false", "false", "false", "false", "false"},
                {
                    "",//Cliente
                    "", //agentes
                    "",//rutagen
                    "",//rutas
                    "ID_SUCURSALALM-ID_SUCURSALALM,SEGMENT1-C_PROVE,SEGMENT1-C_PROVE2,VENDOR_NAME-DESCRI,ATTRIBUTE9-RESP_COMA", //PROVEEDO
                    "ID_SUCURSALALM-ID_SUCURSALALM,SEGMENT1-C_ARTI,NO_PROV_AFECTA_PRECIO-C_PROVE,NO_PROV_AFECTA_PRECIO-C_PROVE2,SEGMENT2-FAMI_ARTI,DESCRIPTION-DES_ARTI,DESCRIPTION-DES_ART2,ATTRIBUTE2-CAP_ARTI,ATTRIBUTE3-EMPAQUE2,INVENTORY_ITEM_STATUS_CODE-STATUS,ATTRIBUTE13-CAJA,ATTRIBUTE14-UNIDAD,ATTRIBUTE15-EXHIBIDOR",
                    "",
                    "ID_SUCURSALALM-ID_SUCURSALALM,SEGMENT2-FAMI_ARTI,DESCRIPCION_SEG2-NOMBRE,IVA-IVA,IVA-IVA2", //
                    "",
                    
                    "",
                    
                    "ID_SUCURSALALM-ID_SUCURSALALM,ITEM_NUMBER-C_ARTI,STATUS_NUMBER-STATUS,CONCEPTO-CONCEPTO,DESCRIPTION-DES_ARTI,ATTRIBUTE3-EMPAQUE,ATTRIBUTE2-CAP_ARTI,NO_PROV_AFECTA_PRECIO-C_PROVE", //,status_number-STATUS
                    "ID_SUCURSALALM-ID_SUCURSALALM,STATUS_NUMBER-STATUS,CONCEPTO-CONCEPTO,FECHA_ALTA-FECHA_ALT",
                    
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
            dgvTablas.Columns["tablassql"].Visible = false;
            dgvTablas.Columns["campos"].Visible = false;
            
            dgvTablas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvTablas.DefaultCellStyle.SelectionBackColor = Properties.Settings.Default.filaSeleccion;
            dgvTablas.AlternatingRowsDefaultCellStyle.BackColor = Properties.Settings.Default.filaAltern;
            
        }

        private void FrmImporTablas_FormClosing(object sender, FormClosingEventArgs ee)
        {
            DataTable _prove = _funcion.llenar_dt("dbf_proveedo", "id");
            DataTable _arti = _funcion.llenar_dt("dbf_articulo", "id");

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

                    if ((_prove.Rows.Count > 0)&&(_arti.Rows.Count > 0))
                    {
                        FrmIndex.opcPartSuc.Enabled = true;
                        FrmIndex.opcImporTabla.Enabled = true;
                        FrmIndex.opcProveArti.Enabled = true;
                    }
                   

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
                if ((_prove.Rows.Count > 0) && (_arti.Rows.Count > 0))
                {
                    FrmIndex.opcPartSuc.Enabled = true;
                    FrmIndex.opcImporTabla.Enabled = true;
                    FrmIndex.opcProveArti.Enabled = true;
                }
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
                    else if (DT.Rows[i]["tablas"].ToString() == "agentes")
                    {
                        DT.Rows[i]["dbf"] = CHECKED;
                    }
                    else if (DT.Rows[i]["tablas"].ToString() == "rutagen")
                    {
                        DT.Rows[i]["dbf"] = CHECKED;
                    }
                    else if (DT.Rows[i]["tablas"].ToString() == "rutas")
                    {
                        DT.Rows[i]["dbf"] = CHECKED;
                    }
                    else if (DT.Rows[i]["tablas"].ToString() == "prefijos")
                    {
                        DT.Rows[i]["dbf"] = CHECKED;
                    }
                    else if (DT.Rows[i]["tablas"].ToString() == "inventa,inv001")
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
                        (_dtTablas.Rows[i]["tablas"].ToString() == "agentes" && dgvTablas.CurrentRow.Cells["tablas"].Value.ToString() == "agentes") ||
                        (_dtTablas.Rows[i]["tablas"].ToString() == "rutagen" && dgvTablas.CurrentRow.Cells["tablas"].Value.ToString() == "rutagen") ||
                        (_dtTablas.Rows[i]["tablas"].ToString() == "rutas" && dgvTablas.CurrentRow.Cells["tablas"].Value.ToString() == "rutas") ||
                        (_dtTablas.Rows[i]["tablas"].ToString() == "prefijos" && dgvTablas.CurrentRow.Cells["tablas"].Value.ToString() == "prefijos") ||
                        (_dtTablas.Rows[i]["tablas"].ToString() == "inventa,inv001" && dgvTablas.CurrentRow.Cells["tablas"].Value.ToString() == "inventa,inv001"))
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

                            String carpetaLocal = Application.StartupPath + @"\tmp_expo\" + nomExpo + @"\" + _nomUsuario + @"\" + almdtSucu.ToString();
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

                            String[] cutTablas = tbldtTablas.ToString().Split(',');
                            String nomTblTablas = "";
                            String nomDbfTablas = "";

                            if (!(almdtSucu.ToString() == "001")) //ESTO SE HIZO POR EL DETALLE DE MERIDA QUE TIENE INV001 Y LAS OTRAS SUCURSALES TIENEN INVENTA
                            {
                                nomTblTablas = cutTablas[0].ToString();
                                nomDbfTablas = cutTablas[0].ToString();
                            }
                            else
                            {
                                try
                                {
                                    nomTblTablas = cutTablas[0].ToString();
                                    nomDbfTablas = cutTablas[1].ToString();
                                }
                                catch (Exception)
                                {

                                    nomTblTablas = cutTablas[0].ToString();
                                    nomDbfTablas = cutTablas[0].ToString();
                                }

                            }
                            //for (int ii = 0; ii < cutTablas.Count(); ii++)
                            //{
                            //    nomTblTablas = cutTablas[0].ToString();
                            //    if (File.Exists(rutadtSucu.ToString()+"\\"+ cutTablas[ii].ToString()+".dbf"))
                            //    {
                            //        nomDbfTablas = cutTablas[ii].ToString();
                            //        break;
                            //    }
                            //}

                            _funcion.Cargando(this, barraProgreso, 0, 1, 3, lblMensaje, "Preparando dbf " + nomDbfTablas);

                            //COPIAR LOS DBF EN EL MISMO SERVIDOR CON OTRO NOMBRE
                            var servFptOrigen = Path.Combine(rutadtSucu.ToString(), nomDbfTablas + ".fpt");
                            var servFptDestino = Path.Combine(carpetaTmpExpo, "expo_" + nomDbfTablas + ".fpt");
                            //COPIAR LOS ARCHIVOS A LAS CARPETAS QUE EL SISTEMA CREA
                            var localFptDestino = Path.Combine(carpetaLocal, nomDbfTablas + ".fpt");

                            var servDbfOrigen = Path.Combine(rutadtSucu.ToString(), nomDbfTablas + ".dbf");
                            var servDbfDestino = Path.Combine(carpetaTmpExpo, "expo_" + nomDbfTablas + ".dbf");
                            var localDbfDestino = Path.Combine(carpetaLocal, nomDbfTablas + ".dbf");

                            var servCdxOrigen = Path.Combine(rutadtSucu.ToString(), nomDbfTablas + ".cdx");
                            var servCdxDestino = Path.Combine(carpetaTmpExpo, "expo_" + nomDbfTablas + ".cdx");
                            var localCdxDestino = Path.Combine(carpetaLocal, nomDbfTablas + ".cdx");

                            if (File.Exists(servFptOrigen))
                            {

                                File.Copy(servFptOrigen, servFptDestino, true);

                                _funcion.Cargando(this, barraProgreso, 0, 2, 3, lblMensaje, "Preparando para copiar fpt " + nomDbfTablas);
                                //Thread.Sleep(1000);

                                CopyFile(servFptDestino, localFptDestino);

                            }

                            if (File.Exists(servDbfOrigen))
                            {
                                File.Copy(servDbfOrigen, servDbfDestino, true);

                                _funcion.Cargando(this, barraProgreso, 0, 2, 3, lblMensaje, "Preparando para copiar dbf " + nomDbfTablas);
                                //Thread.Sleep(1000);

                                CopyFile(servDbfDestino, localDbfDestino);
                            }

                            if (File.Exists(servCdxOrigen))
                            {
                                File.Copy(servCdxOrigen, servCdxDestino, true);

                                _funcion.Cargando(this, barraProgreso, 0, 2, 3, lblMensaje, "Preparando para copiar cdx " + nomDbfTablas);
                                //Thread.Sleep(1000);

                                CopyFile(servCdxDestino, localCdxDestino);
                            }

                            int totalProcesoDBF = 4;
                            int actualProcesoDBF = 1;
                            _funcion.Cargando(this, barraProgreso, 0, actualProcesoDBF, totalProcesoDBF, lblMensaje, "Preparando para importación: " + nomTblTablas);
                            Thread.Sleep(5000);

                            ProcessStartInfo info = null;
                            String _RutaTabla = @"tmp_expo\" + nomExpo + @"\" + _nomUsuario + @"\" + almdtSucu.ToString() + @"\" + nomDbfTablas;
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


                            string cadena = @"Driver={Microsoft Visual Foxpro Driver};UID=;SourceType=DBF;SourceDB=" + carpetaLocal + " ;Exclusive=No;SHARED=YES;collate=Machine;NULL=NO;DELETED=NO;BACKGROUNDFETCH=YES;";
                            OdbcConnection con = new OdbcConnection();  //se crea la variable de conexion para el dbf
                            con.ConnectionString = cadena;              //se crea la conexion
                            con.Open();
                            string consulta = "SELECT * FROM " + nomDbfTablas;
                            OdbcDataAdapter adapter = new OdbcDataAdapter(consulta, con);
                            DataTable dtDBF = new DataTable();



                            try
                            {
                                adapter.Fill(dtDBF);
                                con.Close();

                            }
                            catch (Exception ex)
                            {
                                //throw;
                                //MessageBox.Show(ex.Message);
                                con.Close();

                                dtDBF.Clear();




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

                            String[] archivos = Directory.GetFiles(carpetaLocal, nomDbfTablas + ".*");

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
                                _funcion.Cargando(this, barraProgreso, 0, iAlm, dtDBF.Rows.Count, lblMensaje, "Asignando el almacen: " + nomDbfTablas);
                                dtDBF.Rows[iAlm]["ID_SUCURSALALM"] = almdtSucu.ToString();
                            }

                            using (SqlConnection _consql = new SqlConnection(_CadenaConexion))
                            {
                                //bool respuesta = false;
                                SqlTransaction _tran;
                                string _tabla = "dbf_" + nomTblTablas;
                                if (_consql.State == ConnectionState.Closed)
                                {
                                    _consql.Open();
                                }

                                //_con.Open();
                                actualProcesoDBF++;
                                _funcion.Cargando(this, barraProgreso, 0, actualProcesoDBF, totalProcesoDBF, lblMensaje, "Limpiando: " + nomTblTablas);
                                String sqlBorrar = "DELETE FROM dbf_" + nomTblTablas + " WHERE ID_SUCURSALALM = '" + almdtSucu.ToString() + "'";
                                SqlCommand comando = new SqlCommand(sqlBorrar, _consql);
                                comando.CommandTimeout = 300;
                                comando.ExecuteNonQuery();

                                actualProcesoDBF++;
                                _funcion.Cargando(this, barraProgreso, 0, actualProcesoDBF, totalProcesoDBF, lblMensaje, "Verificando campos: " + nomTblTablas);
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
                                            _funcion.Cargando(this, barraProgreso, 0, jj, dtDBF.Columns.Count, lblMensaje, "Preparando campos: " + nomTblTablas);
                                            
                                            bulkCopy.ColumnMappings.Add(dtDBF.Columns[jj].ColumnName.ToString(), dtDBF.Columns[jj].ColumnName.ToString().ToUpper());

                                        }

                                        //bulkCopy.BatchSize = 5000;
                                        bulkCopy.WriteToServer(dtDBF);
                                        _tran.Commit();
                                        //respuesta = true;
                                        actualProcesoDBF++;
                                        _funcion.Cargando(this, barraProgreso, 0, actualProcesoDBF, totalProcesoDBF, lblMensaje, "Información importada: " + nomTblTablas);
                                        //_dtTablas.Rows[j]["dbf"] = false;
                                    }
                                    catch (Exception exx)
                                    {
                                        //correo.SendError(ex, System.Net.Mail.MailPriority.High, "Las ventas del día " + _fecha + " de la Sucursal " + _suc + "" + ex.StackTrace);
                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            MessageBox.Show(exx.Message);
                                        });
                                        
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

                            //if (Directory.Exists(carpetaLocal))
                            //{
                            //    Directory.Delete(carpetaLocal);
                            //}

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
                                        _funcion.Cargando(this, barraProgreso, 0, actualProcesoDBF, totalProcesoDBF, lblMensaje, "Conectando a la base de datos");
                                        _consqlOrigen.Open();
                                    }

                                    SqlCommand sqlComTablas = new SqlCommand(_dtTablas.Rows[j]["tablassql"].ToString(), _consqlOrigen);
                                    sqlComTablas.CommandTimeout = 10000;
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
                                            this.Invoke((MethodInvoker)delegate
                                            {
                                                MessageBox.Show(exx.Message);
                                            });
                                            
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
                                this.Invoke((MethodInvoker)delegate
                                {
                                    MessageBox.Show(_dtTablas.Rows[j]["tablas"].ToString() + " no esta disponible en esta opción");
                                });
                                
                            }
                        }

                        GC.Collect();

                    }
                }
            }
            _funcion.Cargando(this, barraProgreso, 0, 1, 1, lblMensaje, "Proceso terminado: ");
            this.Invoke((MethodInvoker)delegate
            {
                MessageBox.Show("Proceso terminado", "¡Listo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
            
            this.Invoke(new PosicionTablaDelegate(PosicionTabla), 0);
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
                this.Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show(ee.Message);
                });
                
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

        private void dgvSucursal_MouseMove(object sender, MouseEventArgs e)
        {
            dgvSucursal.Focus();
        }

        private void dgvTablas_MouseMove(object sender, MouseEventArgs e)
        {
            dgvTablas.Focus();
        }
    }
}
