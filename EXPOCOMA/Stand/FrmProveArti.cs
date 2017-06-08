using System;
using System.Collections;
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
    public partial class FrmProveArti : Form
    {
        private Process programa;
        public String _CadenaConexion;
        internal static Form _frmProveArti;

        funciones _funcion = new funciones();

        //FrmProveArti frmProveArti = new FrmProveArti();

        DataTable _dtSucursales;

        DataTable _dtProveedor;
        DataTable _dtGuardarProveedor;
        DataTable _dtTblProveedor;
        DataView _dvProveedor;


        DataTable _dtGuardarArticulo;
        DataTable _dtTblArticulo;
        DataTable _dtArticulo;
        DataView _dvArticulo;
        DataView _dvArticuloTMP;
        DataRow[] foundRows;

        DataTable _dtProveGuardados;
        DataTable _dtArtiGuardados;
        DataTable _dtTblTablaSql;
        DataTable _dtTmpArticulo;


        String Sesion = "---";
        String SesionLetra = "N";
        String CproveFiltrar;
        String CartiFiltar;

        private Thread CargarInfo;
        private Thread MarcarTodProv;
        private Thread MarcarTodArti;
        private Thread GuardarProveedor;
        private Thread trdActualizarArticulo;

        IEnumerable<DataRow> sql_dtProve;
        IEnumerable<DataRow> sql_dtArtiGuardados;
        IEnumerable<DataRow> sql_dtArtiGuar;
        IEnumerable<DataRow> sql_dtTblTablaSql; //para filtrar los articulos que le perteneces a los proveedores actualizados.

        String[,] _tablas =
            {
                {
                    //_tablasNombre - TABLAS 
                    "proveedo",
                    "articulo",
                    "inventa,inv001",
                    "",
                },
                {
                    //_esTablaBdf - SI ES BDF
                    "false",
                    "false",
                    "true",
                    "false",
                },
                {

                //_tablasBDFCampos - CAMPOS DE LA TABLA DBF
                "", //
                    "",
                    "ID_SUCURSALALM,C_ARTI,DIS_DISPO",
                    "",
                },
                {
                    //_esTablaSql - SI ES SQL
                    "true",
                    "true",
                    "false",
                    "false",
                },
                {
                //_tablasSql - TABLA SQL
                    "SELECT * FROM VW_PO_VENDORS_INT WHERE organizacion = @ORGANIZATION_ID AND ATTRIBUTE9 = @RESP_COMA",
                    "select arti.*, starti.status_number, st.concepto as stconcepto from VW_PO_SUPPLIERS_ITEMS_CMA_2 arti, po_supp_item_status_int starti, PO_CATALOG_STATUS st where (arti.segment1 = starti.item_number and arti.organization_id = starti.organization_id) AND (starti.status_number = st.status_number) AND ( arti.organization_id = @ORGANIZATION_ID) AND starti.status_number in (2,4,7,8,12,19,20,22,33,35)",//"SELECT * FROM VW_MTL_SYSTEM_ITEMS_B_CMA_2 WHERE ORGANIZATION_ID = @ORGANIZATION_ID", //AND NO_PROV_AFECTA_PRECIO IN (@CPROVE)
                    "",
                    "",
                },
                {
                //_tablasSQLCampos - CAMPOS DE LA TABLA SQL
                    "ID_SUCURSALALM,SEGMENT1,VENDOR_NAME,ATTRIBUTE9,inactive_date,BAJA_GRAL,ESTATUS",
                    "ID_SUCURSALALM,SEGMENT1,NO_PROV_AFECTA_PRECIO,NO_PROV_AFECTA_PRECIO,SEGMENT2,DESCRIPTION,DESCRIPTION,ATTRIBUTE2,ATTRIBUTE3,STATUS_NUMBER,ATTRIBUTE13,ATTRIBUTE14,ATTRIBUTE15,ATTRIBUTE7", //INVENTORY_ITEM_STATUS_CODE = STATUS
                    "",
                    "",
                },
                {
                //_tablasNomDestino - TABLAS DESTINO
                    "dbf_proveedo",
                    "dbf_articulo",
                    "dbf_inventa",
                    "",
                },
                {
                //_tablasNomDestinoCampos - CAMPOS DE TABLAS DESTINO
                    "ID_SUCURSALALM,C_PROVE,DESCRI,RESP_COMA,inactive_date,BAJA_GRAL,STATUS",//
                    "ID_SUCURSALALM,C_ARTI,C_PROVE,C_PROVE2,FAMI_ARTI,DES_ARTI,DES_ART2,CAP_ARTI,EMPAQUE2,STATUS,CAJA,UNIDAD,EXHIBIDOR,CANTIDAD",
                    "ID_SUCURSALALM,C_ARTI,DIS_DISPO",
                    "",
                },
                {
                //_tablasNomDestiCamposComparar - CAMPOS DE TABLAS DESTINO PARA COMPARA SI EXISTEN O NO
                    "ID_SUCURSALALM-ID_SUCURSALALM-C_PROVE-SEGMENT1",
                    "ID_SUCURSALALM-ID_SUCURSALALM-C_ARTI-SEGMENT1",
                    "ID_SUCURSALALM-ID_SUCURSALALM-C_ARTI-C_ARTI",
                    "",
                },

    };

        public String _nomUsuario;
        public String nomExpo;

        String tmpCarpetaLocal;
        String carpetaServ;
        String tmpCarpetaServ;
        String idActSucu;
        String servidorSucu;
        String actuRutaBaja;
        String actuAgenBaja;
        String servConex;
        String servBd;
        String servUsua;
        String servPass;
        String organiIdsucu;

        String _tablasNombre;
        String _esTablaBdf;
        String _tablasBDFCampos;
        String _esTablaSql;
        String _tablasSql;
        String _tablasSQLCampos;
        String _tablasNomDestino;
        String _tablasNomDestinoCampos;
        String _tablasNomDestiCamposComparar;

        IEnumerable<DataRow> sql_dtsucu;

        
        public FrmProveArti()
        {
            InitializeComponent();
        }


        public void CargarInformacion()
        {
            _funcion.DesabilitarControles(this, false);
            //if (InvokeRequired)
            //{
            //    Invoke(new Action(delegate ()
            //    {

            //SUCURSAL
            _funcion._SQLCadenaConexion = _CadenaConexion;
            _dtSucursales = _funcion.llenar_form("tbl_sucursal", "anfitrion DESC", "almacen, sucursal,dbf,SERVIDORSUCU,servidor,db,usuario,contrasena,ORGANIZATION_ID");
//            SELECT id, ID_SUCURSALALM, C_PROVE, DESCRI, RESP_COMA, C_PROVE2
//FROM dbf_proveedo
//where(STATUS = '*' or BAJA_GRAL <> '' or inactive_date <> '')
            _dtProveedor = _funcion.llenar_dt("dbf_proveedo", "id, ID_SUCURSALALM, C_PROVE, DESCRI, RESP_COMA,  C_PROVE2,STATUS", "WHERE (STATUS <> '*')"); //WHERE (STATUS <> '*') or ((inactive_date is null or BAJA_GRAL ='') AND (inactive_date is null and BAJA_GRAL =''))
            //_dtClientes = _funcion.llenar_dt("dbf_cliente clie, dbf_agentes agen", "clie.ID_SUCURSALALM, clie.C_CLIENTE, clie.NOM_CLIEN, clie.C_AGENTE AS CLIAGEN, agen.C_AGENTE AS AGENAGEN, agen.NOM_AGENTE, clie.C_RUTA, clie.NOM_TIENDA, clie.POBLACION, clie.TELEFONO", "WHERE (clie.C_AGENTE = agen.C_AGENTE)AND(clie.ID_SUCURSALALM = '" + idSucur + "' AND agen.ID_SUCURSALALM = '" + idSucur + "')");// "WHERE ID_SUCURSALALM = "+cBoxSucursal.SelectedValue.ToString());
            //_dtArticulo = _funcion.llenar_form("dbf_articulo", "c_prove ASC", "id, ID_SUCURSALALM, C_ARTI, FAMI_ARTI, DES_ARTI, CAP_ARTI, EMPAQUE2, STATUS, C_PROVE, C_PROVE2, CANTIDAD, CANCELA, FALTANTE, COSTO, UNIDAD, CAJA, EXHIBIDOR, MARG_PRE4");
            _dtArticulo = _funcion.llenar_dt("dbf_articulo arti,dbf_inventa inve", "arti.id, arti.ID_SUCURSALALM, arti.C_ARTI, arti.FAMI_ARTI, arti.DES_ARTI, arti.CAP_ARTI, arti.EMPAQUE2, arti.STATUS, arti.C_PROVE, arti.C_PROVE2, arti.CANTIDAD, arti.CANCELA, arti.FALTANTE, arti.COSTO, arti.CAJA, arti.UNIDAD, arti.EXHIBIDOR, arti.MARG_PRE4, inve.DIS_DISPO", "WHERE arti.C_ARTI = inve.C_ARTI AND arti.ID_SUCURSALALM = inve.ID_SUCURSALALM AND arti.STATUS <> '*' AND arti.STATUS <> 'INACTIVO'");
            _dtProveGuardados = _funcion.llenar_dt("tbl_provexpo", "*", "WHERE COMPRADOR = '" + SesionLetra + "'");
            _dtArtiGuardados = _funcion.llenar_dt("tbl_artiexpo", "*");
            //id, ID_SUCURSALALM, C_PROVE, C_ARTI, DES_ARTI, EMPAQUE2, CAP_ARTI, STATUS
            this.Invoke((MethodInvoker)delegate
                {
                    if (cBoxSucursal.DataSource == null)
                    {
                        cBoxSucursal.DataSource = _dtSucursales;
                        cBoxSucursal.ValueMember = "ALMACEN";//"valor";
                        cBoxSucursal.DisplayMember = "SUCURSAL"; //"opcion";
                    }
                    
                    
                    _dtProveedor.Columns.Add("PARTICIPA", typeof(Boolean));
                    for (int i = 0; i < _dtProveedor.Rows.Count; i++)
                    {
                        _dtProveedor.Rows[i]["PARTICIPA"] = false;
                    }


                    _dtArticulo.Columns.Add("PARTICIPA", typeof(Boolean));
                    for (int i = 0; i < _dtArticulo.Rows.Count; i++)
                    {
                        _dtArticulo.Rows[i]["PARTICIPA"] = false;
                    }
                    
            });


            foreach (DataRow row_dtProveGuardados in _dtProveGuardados.Rows)
            {


                sql_dtProve =
              from dtProveGuar in _dtProveedor.AsEnumerable()
              where dtProveGuar.Field<String>("C_PROVE") == row_dtProveGuardados["C_PROVE"].ToString() &&
              dtProveGuar.Field<String>("ID_SUCURSALALM") == row_dtProveGuardados["ID_SUCURSALALM"].ToString() &&
              dtProveGuar.Field<String>("RESP_COMA") == SesionLetra
              select dtProveGuar;

                //MessageBox.Show("primer ciclo: " + row_dtProveGuardados["C_PROVE"].ToString());

                foreach (DataRow rowstProveGuar in sql_dtProve)
                {

                    rowstProveGuar.SetField("PARTICIPA", true);
                    rowstProveGuar.AcceptChanges();


                    //_dtArtiGuardados = _funcion.llenar_dt("tbl_artiexpo", "*", "WHERE C_PROVE = '" + rowstProveGuar.Field<String>("C_PROVE") + "'" +
                    //    " AND ID_SUCURSALALM = '" + rowstProveGuar.Field<String>("ID_SUCURSALALM") + "'");
                    sql_dtArtiGuardados =
              from dtArtiGuar in _dtArtiGuardados.AsEnumerable()
              where dtArtiGuar.Field<String>("C_PROVE") == rowstProveGuar["C_PROVE"].ToString() &&
              dtArtiGuar.Field<String>("ID_SUCURSALALM") == rowstProveGuar["ID_SUCURSALALM"].ToString()
                  //dtArtiGuar.Field<String>("RESP_COMA") == SesionLetra
                  select dtArtiGuar;

                    //MessageBox.Show("segundo ciclo "+ rowstProveGuar["C_PROVE"].ToString());

                    foreach (DataRow row_dtArtiGuardados in sql_dtArtiGuardados)
                    {
                        sql_dtArtiGuar =
                       from dtArtiGuarr in _dtArticulo.AsEnumerable()
                       where dtArtiGuarr.Field<String>("C_ARTI") == row_dtArtiGuardados["C_ARTI"].ToString() &&
                       dtArtiGuarr.Field<String>("C_PROVE2") == row_dtArtiGuardados["C_PROVE2"].ToString() &&
                       dtArtiGuarr.Field<String>("ID_SUCURSALALM") == row_dtArtiGuardados["ID_SUCURSALALM"].ToString()
                       select dtArtiGuarr;

                        //MessageBox.Show("tercero ciclo "+ row_dtArtiGuardados["C_ARTI"].ToString());

                        foreach (DataRow rowstArtiGuar in sql_dtArtiGuar)
                        {
                            if (rowstArtiGuar["C_PROVE"].ToString() == row_dtArtiGuardados["C_PROVE"].ToString())
                            {
                                rowstArtiGuar.SetField("PARTICIPA", true);
                                rowstArtiGuar.AcceptChanges();
                            }
                            else
                            {
                                rowstArtiGuar.SetField("C_PROVE", row_dtArtiGuardados["C_PROVE"].ToString());
                                rowstArtiGuar.SetField("PARTICIPA", true);
                                rowstArtiGuar.AcceptChanges();
                                //MessageBox.Show(rowstArtiGuar.Field<String>("C_PROVE") +"=="+ _dtArtiGuardados.Rows[ii]["C_PROVE"].ToString());
                            }

                            //MessageBox.Show("cuarto ciclo " + rowstArtiGuar["C_ARTI"]);
                        }
                    }

                    //for (int ii = 0; ii < _dtArtiGuardados.Rows.Count; ii++)
                    //{

                    //}

                    //GC.Collect();

                }



            }
            //cBoxSucursal.SelectedItem = _cbDatos[1, 0];

            //*************************************PROVEEDOR
            String[,] _cbdatos = {
                { "C_PROVE", "DESCRI" },
                { "Clv. Proveedor", "Nombre" }
            };
            this.Invoke((MethodInvoker)delegate { 
                _funcion.llenarCombobox(cboxBusProve, _cbdatos);
            });
            

            String[,] _cbOrdDatos = {
                { SesionLetra, "" },
                { "Mis proveedores", "Todos" }

            };
            this.Invoke((MethodInvoker)delegate {
                _funcion.llenarCombobox(cboxOrdProve, _cbOrdDatos);
            });

            //FiltrarProveedor(_dtProveedor, "ID_SUCURSALALM = " + cBoxSucursal.SelectedValue.ToString());
            //this.Invoke(new Action(delegate () {
            //    FiltrarProveedor();
            //}));

            this.Invoke((MethodInvoker)delegate {


                dgvProveedor.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dgvProveedor.DataSource = _dtProveedorTMP;
                dgvProveedor.Columns["id"].Visible = false;
                dgvProveedor.Columns["ID_SUCURSALALM"].Visible = false;
                dgvProveedor.Columns["C_PROVE2"].Visible = false;
                dgvProveedor.Columns["PARTICIPA"].DisplayIndex = 0;
                dgvProveedor.Columns["PARTICIPA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvProveedor.Columns["C_PROVE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvProveedor.Columns["C_PROVE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvProveedor.Columns["C_PROVE"].HeaderText = "Clv. Proveedor";
                dgvProveedor.Columns["DESCRI"].HeaderText = "Nombre";
                dgvProveedor.Columns["RESP_COMA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                

                dgvProveedor.DefaultCellStyle.SelectionBackColor = Properties.Settings.Default.filaSeleccion;
                dgvProveedor.AlternatingRowsDefaultCellStyle.BackColor = Properties.Settings.Default.filaAltern;
            });
            

            for (int i = 0; i < dgvProveedor.Rows.Count; i++)
            {
                //dgvProveedor.Rows[i].["PARTICIPA"]. = false;
                if (!(dgvProveedor.Rows[i].Cells["RESP_COMA"].Value.ToString() == SesionLetra))
                {
                    dgvProveedor.Rows[i].Cells["PARTICIPA"].ReadOnly = true;
                }
            }

            //*************************************ARTICULO
            String[,] _cbBusArtidatos = {
                { "C_ARTI", "DES_ARTI", "EMPAQUE2", "CAP_ARTI" },
                { "Clv. Articulo", "Nombre", "Empaque(2)", "Capacidad" }
            };
            this.Invoke((MethodInvoker)delegate {
                _funcion.llenarCombobox(cBoxBusArticulo, _cbBusArtidatos);
            });


            //String[,] _cbOrdArtiDatos = {
            //    { SesionLetra, "" },
            //    { "Mis proveedores", "Todos" }

            //};
            //_funcion.llenarCombobox(cboxOrdProve, _cbOrdArtiDatos);



            //FiltrarArticulo(_dtArticulo, "ID_SUCURSALALM = " + cBoxSucursal.SelectedValue.ToString());

            //_dtArticuloTMP = _dtArticulo.Clone();
            //FiltrarArticulo();

            this.Invoke((MethodInvoker)delegate {
                dgvArticulo.DataSource = _dtArticulo.Clone();

                dgvArticulo.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvArticulo.Columns["id"].Visible = false;
                dgvArticulo.Columns["ID_SUCURSALALM"].Visible = false;
                dgvArticulo.Columns["FAMI_ARTI"].Visible = false;
                dgvArticulo.Columns["C_ARTI"].HeaderText = "Clv. Articulo";
                dgvArticulo.Columns["DES_ARTI"].HeaderText = "Nombre";
                dgvArticulo.Columns["C_PROVE"].Visible = false;
                dgvArticulo.Columns["C_PROVE2"].Visible = false;
                dgvArticulo.Columns["CANTIDAD"].Visible = false;
                dgvArticulo.Columns["CANCELA"].Visible = false;
                dgvArticulo.Columns["FALTANTE"].Visible = false;
                dgvArticulo.Columns["COSTO"].Visible = false;
                dgvArticulo.Columns["PARTICIPA"].DisplayIndex = 0;
                dgvArticulo.Columns["PARTICIPA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                //dgvArticulo.Columns["C_ARTI"].Visible = true;
                dgvArticulo.Columns["C_ARTI"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvArticulo.Columns["C_ARTI"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dgvArticulo.Columns["EMPAQUE2"].Visible = true;
                dgvArticulo.Columns["EMPAQUE2"].HeaderText = "EM";
                dgvArticulo.Columns["EMPAQUE2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvArticulo.Columns["EMPAQUE2"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                //dgvArticulo.Columns["CAP_ARTI"].Visible = true;
                dgvArticulo.Columns["CAP_ARTI"].HeaderText = "CAP";
                dgvArticulo.Columns["CAP_ARTI"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvArticulo.Columns["CAP_ARTI"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvArticulo.Columns["STATUS"].Visible = true;
                dgvArticulo.Columns["STATUS"].HeaderText = "ST";
                dgvArticulo.Columns["STATUS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvArticulo.Columns["STATUS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvArticulo.Columns["UNIDAD"].HeaderText = "U";
                dgvArticulo.Columns["UNIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvArticulo.Columns["UNIDAD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvArticulo.Columns["CAJA"].HeaderText = "C";
                dgvArticulo.Columns["CAJA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvArticulo.Columns["CAJA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvArticulo.Columns["EXHIBIDOR"].HeaderText = "E";
                dgvArticulo.Columns["EXHIBIDOR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvArticulo.Columns["EXHIBIDOR"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvArticulo.Columns["MARG_PRE4"].Visible = false;
                dgvArticulo.Columns["DIS_DISPO"].HeaderText = "EXIS";
                dgvArticulo.Columns["DIS_DISPO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvArticulo.Columns["DIS_DISPO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvArticulo.DefaultCellStyle.SelectionBackColor = Properties.Settings.Default.filaSeleccion;
                dgvArticulo.AlternatingRowsDefaultCellStyle.BackColor = Properties.Settings.Default.filaAltern;
                
            

            //for (int i = 0; i < _dtProveGuardados.Rows.Count; i++)
            //{
               

                

            //}

                picbCargando.Visible = false;
                cBoxSucursal.Focus();
                

            });



            //using (SqlConnection _consql = new SqlConnection(_CadenaConexion))
            //{
            //    if (_consql.State == ConnectionState.Closed)
            //    {
            //        _consql.Open();
            //    }

            //    String sqlBorrar = "SELECT * FROM tbl_provexpo WHERE COMPRADOR = '" + SesionLetra + "'";
            //    SqlCommand comando = new SqlCommand(sqlBorrar, _consql);
            //    comando.CommandTimeout = 300;
            //    comando.ExecuteNonQuery();

            //    if (_consql.State == ConnectionState.Open)
            //    {
            //        _consql.Close();
            //    }
            //}

            //if (!(String.IsNullOrEmpty(SucursalActual)))
            //{
            //    this.Invoke((MethodInvoker)delegate
            //    {
            //        cBoxSucursal.SelectedValue = SucursalActual;
            //    });
                
            //}

            GC.Collect();

            _funcion.DesabilitarControles(this, true);

        }

        private void FrmProveArti_Load(object sender, EventArgs e)
        {
            _funcion.icono(this);
            _funcion.PicCargando(picbCargando);
            
            //CargarInformacion();
            CargarInfo = new Thread(CargarInformacion);
            CargarInfo.IsBackground = true;
            CargarInfo.Start();

            _frmProveArti = this;

            //_dtCProve = new DataTable();
            //_dtCProve.Columns.Add("C_PROVE");

        }

        public void FiltrarProveedor()
        {
            String consulta = "(ID_SUCURSALALM = " + cBoxSucursal.SelectedValue.ToString()+")";
            if (!(txtBusProve.Text == null))
            {

                if (!(cboxOrdProve.SelectedValue.ToString() == ""))
                {
                    consulta += " AND (" + cboxBusProve.SelectedValue.ToString() + " LIKE '%" + txtBusProve.Text + "%' AND resp_coma = '" + cboxOrdProve.SelectedValue.ToString() + "')";
                }
                else
                {
                    consulta += " AND (" + cboxBusProve.SelectedValue.ToString() + " LIKE '%" + txtBusProve.Text + "%')";
                }

            }

            _dvProveedor = _dtProveedor.DefaultView;
            _dvProveedor.RowFilter = consulta;
            _dvProveedor.Sort = cboxBusProve.SelectedValue.ToString() + " ASC";
            dgvProveedor.DataSource = _dvProveedor;

            for (int i = 0; i < dgvProveedor.Rows.Count; i++)
            {
                

                //dgvProveedor.Rows[i].["PARTICIPA"]. = false;
                if (!(dgvProveedor.Rows[i].Cells["resp_coma"].Value.ToString() == SesionLetra))
                {
                    dgvProveedor.Rows[i].Cells["PARTICIPA"].ReadOnly = true;
                }
          

            }
                       
        }

        public void FiltrarArticulo()
        {
            
            String consulta = "(ID_SUCURSALALM = " + cBoxSucursal.SelectedValue.ToString() + ")";
            
                consulta += " AND (C_PROVE = " + dgvProveedor.CurrentRow.Cells["C_PROVE"].Value.ToString() + " AND " + cBoxBusArticulo.SelectedValue.ToString() + " LIKE '%" + txtBusArticulo.Text + "%')";
               

                _dvArticulo = _dtArticulo.DefaultView;
                _dvArticulo.RowFilter = consulta;
            _dvArticulo.Sort = cBoxBusArticulo.SelectedValue.ToString() + " ASC";
            _dvArticulo.Sort = "STATUS ASC";
            dgvArticulo.DataSource = _dvArticulo;

            

            for (int i = 0; i < dgvArticulo.Rows.Count; i++)
            {
                //dgvProveedor.Rows[i].["PARTICIPA"]. = false;
                if ((dgvArticulo.Rows[i].Cells["STATUS"].Value.ToString() == "*")|| (dgvArticulo.Rows[i].Cells["STATUS"].Value.ToString() == "INACTIVO"))
                {
                    dgvArticulo.Rows[i].DefaultCellStyle.BackColor = Properties.Settings.Default.filaroja;
                    dgvArticulo.Rows[i].ReadOnly = true;
                }
            }

        }
        
        private void FrmProveArti_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    this.Close();
            //}
        }

        private void cBoxSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                FiltrarProveedor();
                chBoxProvTodos.Checked = false;
                chBoxArtiTodos.Checked = false;
                //SucursalActual = cBoxSucursal.SelectedValue.ToString();
                //FiltrarArticulo();
            }
            catch (Exception)
            {

                //throw;
            }
            
        }

        private void dgvProveedor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //int colIndex = e.ColumnIndex;
            //MarcarTodArti = new Thread(TodosProveArti);
            //MarcarTodArti.IsBackground = true;
            //MarcarTodArti.Start(colIndex);
            if (dgvProveedor.Columns[e.ColumnIndex].Name == "PARTICIPA")
            {
                // Query the database for the row to be updated.

                
                //DataTable orders = _dtProveedor;

                // Query the SalesOrderHeader table for orders placed 
                // after August 8, 2001.
                IEnumerable<DataRow> sql_dtProve =
                    from dtProve in _dtProveedor.AsEnumerable()
                    where dtProve.Field<String>("C_PROVE") == dgvProveedor.CurrentRow.Cells["C_PROVE"].Value.ToString() && dtProve.Field<String>("ID_SUCURSALALM") == cBoxSucursal.SelectedValue.ToString() && dtProve.Field<String>("RESP_COMA") == SesionLetra
                    select dtProve;

                foreach (DataRow rowstProve in sql_dtProve)
                {
                    //cust.SetField("C_PROVE","00000");
                    //_dtProveedor.Rows[i]["PARTICIPA"] = !Convert.ToBoolean(dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value);
                    rowstProve.SetField("PARTICIPA", !Convert.ToBoolean(dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value));
                    rowstProve.AcceptChanges();
                    //MessageBox.Show("" + rowstProve.Field<String>("C_PROVE"));
                    //MessageBox.Show("Modificado");
                    //query.Shi
                    //cust.SetField<string>("Mariner") = "";
                    //cust.ShipVia = 2;
                    // Insert any additional changes to column values.

                    IEnumerable<DataRow> sql_dtArti =
                        from dtArti in _dtArticulo.AsEnumerable()
                        where dtArti.Field<String>("C_PROVE") == rowstProve.Field<String>("C_PROVE") && dtArti.Field<String>("ID_SUCURSALALM") == rowstProve.Field<String>("ID_SUCURSALALM")
                        select dtArti;

                    
                    foreach (DataRow rowdtArti in sql_dtArti)
                    {
                        //cust.SetField("C_PROVE","00000");
                        //_dtProveedor.Rows[i]["PARTICIPA"] = !Convert.ToBoolean(dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value);
                        if (!(rowdtArti.Field<String>("STATUS") == "*" || rowdtArti.Field<String>("STATUS") == "INACTIVO"))
                        {
                            rowdtArti.SetField("PARTICIPA", Convert.ToBoolean(dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value));
                            rowdtArti.AcceptChanges();
                        }
                        
                        //MessageBox.Show("Modificado");
                        //query.Shi
                        //cust.SetField<string>("Mariner") = "";
                        //cust.ShipVia = 2;
                        // Insert any additional changes to column values.
                    }

                     //_dvArticulo = ();

                    btnAgregarArti.Enabled = Convert.ToBoolean(dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value);


                }


                //DataTable orders = _dtProveedor;

                // Query the SalesOrderHeader table for orders placed 
                // after August 8, 2001.
                

                // Submit the changes to the database.
                //try
                //{
                //    orders.AcceptChanges();
                //    MessageBox.Show("Modificado en la datatable");
                //    //db.SubmitChanges();
                //}
                //catch (Exception ee)
                //{
                //    //Console.WriteLine(ee);
                //    MessageBox.Show(ee.Message);
                //    // Provide for exceptions.
                //}




                //EnumerableRowCollection query =
                //    from ord in _dtProveedor.AsEnumerable()
                //    where ord.Field<>("C_PROVE") == "00030"
                //    select ord;

                // Execute the query, and change the column values
                // you want to change.

                //foreach (DataRow cust in query)
                //{
                //    MessageBox.Show(""+ cust.Field<>("C_PROVE"));
                //    //Console.WriteLine(/*cust.Field<>("C_PROVE")*/);
                //}

                //foreach (_dtProveedor ord in query)
                //{
                //    ord.ShipName = "Mariner";
                //    ord.ShipVia = 2;
                //    // Insert any additional changes to column values.
                //}

                //// Submit the changes to the database.
                //try
                //{
                //    db.SubmitChanges();
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine(e);
                //    // Provide for exceptions.
                //}
                //MessageBox.Show("");

            }

        }

        //public void TodosProveArti(Object _colIndex)
        //{
        //    int colIndex = Convert.ToInt32(_colIndex);
        //    //btnAgregarArti.Enabled = Convert.ToBoolean(dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value);
        //    if (dgvProveedor.Columns[colIndex].Name == "PARTICIPA")
        //    {
        //        //dgvProveedor.Enabled = false;
        //        String consArti;
        //        for (int i = 0; i < _dtProveedor.Rows.Count; i++)
        //        {
        //            if (dgvProveedor.CurrentRow.Cells["RESP_COMA"].Value.ToString() == SesionLetra)
        //            {
        //                this.Invoke((MethodInvoker)delegate
        //                {
        //                    if ((_dtProveedor.Rows[i]["ID_SUCURSALALM"].ToString() == cBoxSucursal.SelectedValue.ToString()) && (_dtProveedor.Rows[i]["C_PROVE"].ToString() == dgvProveedor.CurrentRow.Cells["C_PROVE"].Value.ToString()))
        //                    {

        //                        consArti = "(ID_SUCURSALALM = " + cBoxSucursal.SelectedValue.ToString() + ") AND (c_prove = " + _dtProveedor.Rows[i]["C_PROVE"].ToString() + " AND STATUS <> '*' OR STATUS <> 'INACTIVO')";
        //                        //consArti = "(ID_SUCURSALALM = " + cBoxSucursal.SelectedValue.ToString() + ") AND (c_prove = " + _dtProveedor.Rows[i]["C_PROVE"].ToString() + " AND (STATUS <> '*' OR STATUS <> 'INACTIVO'))";
        //                        foundRows = _dtArticulo.Select(consArti);
        //                        if (foundRows.LongCount() > 0)
        //                        {
        //                            //MessageBox.Show("hola");
        //                            _dtProveedor.Rows[i]["PARTICIPA"] = !Convert.ToBoolean(dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value);
        //                            //dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value= !Convert.ToBoolean(dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value);

        //                            for (int ii = 0; ii < _dtArticulo.Rows.Count; ii++)
        //                            {
        //                                if ((_dtArticulo.Rows[ii]["ID_SUCURSALALM"].ToString() == cBoxSucursal.SelectedValue.ToString()) && (_dtArticulo.Rows[ii]["C_PROVE"].ToString() == dgvProveedor.CurrentRow.Cells["C_PROVE"].Value.ToString()) && (_dtArticulo.Rows[ii]["STATUS"].ToString() != "*") && (_dtArticulo.Rows[ii]["STATUS"].ToString() != "INACTIVO"))
        //                                {
        //                                    _dtArticulo.Rows[ii]["PARTICIPA"] = Convert.ToBoolean(dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value);
        //                                    chBoxProvTodos.Checked = false;
        //                                    chBoxArtiTodos.Checked = false;
        //                                    //chBoxArtiTodos.Checked = Convert.ToBoolean(dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value);
        //                                }
        //                            }

        //                        }
        //                        //else
        //                        //{
        //                        //    _dtProveedor.Rows[i]["PARTICIPA"] = false;
        //                        //    dgvProveedor.CurrentRow.Cells["PARTICIPA"].ReadOnly = true;
        //                        //}


        //                    }
        //                });
                        
        //            }
        //        }

        //        //if (Convert.ToBoolean(dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value) == true)
        //        //{
        //        this.Invoke((MethodInvoker)delegate
        //        {
        //            

        //        });
        //        //}
        //        //else
        //        //{
        //        //    btnAgregarArti.Enabled = false;
        //        //}

        //        //dgvProveedor.Enabled = true;

        //    }
        //}

        private void txtBusProve_KeyUp(object sender, KeyEventArgs e)
        {
            FiltrarProveedor();
          
        }

        private void cboxBusProve_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBusProve.Focus();
        }

        private void cboxOrdProve_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //FiltrarProveedor(_dtProveedor, "ID_SUCURSALALM = " + cBoxSucursal.SelectedValue.ToString());

                chBoxProvTodos.Enabled = true;
                chBoxArtiTodos.Enabled = true;
                if (string.IsNullOrWhiteSpace(cboxOrdProve.SelectedValue.ToString()))
                {
                    chBoxProvTodos.Enabled = false;
                    chBoxArtiTodos.Enabled = false;
                }

                FiltrarProveedor();
            }
            catch (Exception)
            {

                //throw;
            }
            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //int checkProve = 0;
            //for (int i = 0; i < _dtProveedor.Rows.Count; i++)
            //{

            //    if (Convert.ToBoolean(_dtProveedor.Rows[i]["PARTICIPA"].ToString()) == true)
            //    {
            //        checkProve++;
            //        break;
            //    }

            //}
            //if (!(checkProve > 0))
            //{
            //    MessageBox.Show("¡No tienes seleccionado ningún proveedor!", "Espera", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            GuardarProveedor = new Thread(GuardarProve);
            GuardarProveedor.IsBackground = true;
            GuardarProveedor.Start();
            //}
            //_dtProveedor=_dtArticulo.Clone().Copy();
        }

        public void GuardarProve()
        {
         
                _funcion.DesabilitarControles(this, false);

            //Thread.Sleep(500);


            String sqlPartiProve = "(PARTICIPA = true AND RESP_COMA = '" + SesionLetra + "')";
            DataRow[] drPartiProve = _dtProveedor.Select(sqlPartiProve);
            DataTable dtPartiProve = _dtProveedor.Clone();
            DataRow[] _drproveArti;

            foreach (DataRow fila in drPartiProve)
            {
                //dtPartiProve.ImportRow(fila);
                //[0=id][1=idalmacen][2=c_clave]
                _drproveArti = _dtArticulo.Select("(PARTICIPA = true AND ID_SUCURSALALM = " + fila[1].ToString() + " AND C_PROVE = '" + fila[2].ToString() + "') AND (STATUS <> '*' OR STATUS <> 'INACTIVO')");
                if (_drproveArti.Count() > 0)
                {
                    dtPartiProve.ImportRow(fila);
                }
                else
                {
                    if (cBoxMostarProv.Checked)
                    {
                        MessageBox.Show("El proveedor " + fila[2].ToString() + " (" + fila[1].ToString() + ") no tienen articulos disponibles");
                    }

                }
            }

            if (!(drPartiProve.Count() > 0))
            {
                MessageBox.Show("¡No tienes seleccionado ningún proveedor!", "Espera", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                Int32 staGuardado = 0;
                using (SqlConnection _consql = new SqlConnection(_CadenaConexion))
                {
                    SqlTransaction _tran;
                    string _tabla = "tbl_provexpo";

                    if (_consql.State == ConnectionState.Closed)
                    {
                        _consql.Open();
                    }

                    _dtTblProveedor = _funcion.EstructuraTabla("tbl_provexpo");

                    String sqlBorrar = "DELETE FROM tbl_provexpo WHERE COMPRADOR = '" + SesionLetra + "'";
                    SqlCommand comando = new SqlCommand(sqlBorrar, _consql);
                    comando.CommandTimeout = 300;
                    comando.ExecuteNonQuery();

                    _tran = _consql.BeginTransaction();


                    using (SqlBulkCopy bulkCopy =
                        new SqlBulkCopy(_consql, SqlBulkCopyOptions.KeepNulls & SqlBulkCopyOptions.KeepIdentity, _tran))
                    {
                        bulkCopy.DestinationTableName = _tabla;
                        bulkCopy.BulkCopyTimeout = 300;


                        try
                        {
                            
                        
                            bulkCopy.ColumnMappings.Add("ID_SUCURSALALM", "ID_SUCURSALALM");
                            bulkCopy.ColumnMappings.Add("C_PROVE", "C_PROVE");
                            bulkCopy.ColumnMappings.Add("C_PROVE2", "C_PROVE2");
                            bulkCopy.ColumnMappings.Add("DESCRI", "DESCRI");
                            bulkCopy.ColumnMappings.Add("RESP_COMA", "COMPRADOR");

                            //bulkCopy.BatchSize = 5000;
                            bulkCopy.WriteToServer(dtPartiProve);
                            _tran.Commit();
                         

                            staGuardado++;
                        }
                        catch (Exception ex)
                        {
                            //correo.SendError(ex, System.Net.Mail.MailPriority.High, "Las ventas del día " + _fecha + " de la Sucursal " + _suc + "" + ex.StackTrace);
                            MessageBox.Show(ex.Message);
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
                            //MessageBox.Show("Guardado");
                        }


                    }

                }

                using (SqlConnection _consqlArti = new SqlConnection(_CadenaConexion))
                {

                    if (_consqlArti.State == ConnectionState.Closed)
                    {
                        _consqlArti.Open();
                    }

                    SqlTransaction _tranArticulos;
                    string _tablaArticulo = "tbl_artiexpo";

                    DataRow[] _drArticulo = _dtArticulo.Select("participa = true");
                    _dtGuardarArticulo = _dtArticulo.Clone();
                    foreach (DataRow fila in _drArticulo)
                    {
                        _dtGuardarArticulo.ImportRow(fila);
                    }

                    _dtTblArticulo = _funcion.EstructuraTabla("tbl_artiexpo");
                    //tbl_provexpo
                    DataTable _dtCProvedor = _funcion.llenar_dt("DBF_PROVEEDO", "C_PROVE, C_PROVE2", "WHERE RESP_COMA = '" + SesionLetra + "'", "ORDER BY C_PROVE ASC");

                    for (int i = 0; i < _dtCProvedor.Rows.Count; i++)
                    {

                        String sqlBorrarArticulo = "DELETE FROM tbl_artiexpo WHERE C_PROVE = '" + _dtCProvedor.Rows[i]["C_PROVE"].ToString() + "'";
                        SqlCommand comandoArticulo = new SqlCommand(sqlBorrarArticulo, _consqlArti);
                        comandoArticulo.CommandTimeout = 300;
                        comandoArticulo.ExecuteNonQuery();
                    }


                    _tranArticulos = _consqlArti.BeginTransaction();

                    using (SqlBulkCopy bulkCopy =
                        new SqlBulkCopy(_consqlArti, SqlBulkCopyOptions.KeepNulls & SqlBulkCopyOptions.KeepIdentity, _tranArticulos))
                    {
                        bulkCopy.DestinationTableName = _tablaArticulo;
                        bulkCopy.BulkCopyTimeout = 300;


                        try
                        {
                            ////dtDBF.Columns.Count
                            //for (int i = 0; i < _dtTblArticulo.Columns.Count; i++)
                            //{
                            //    //_funcion.Cargando(this, barraProgreso, 0, jj, dtDBF.Columns.Count, lblMensaje, "Preparando campos: " + tbldtTablas.ToString());
                            //    bulkCopy.ColumnMappings.Add(_dtTblArticulo.Columns[i].ColumnName.ToString(), _dtTblArticulo.Columns[i].ColumnName.ToString().ToUpper());

                            //}

                            bulkCopy.ColumnMappings.Add("ID_SUCURSALALM", "ID_SUCURSALALM");
                            bulkCopy.ColumnMappings.Add("C_ARTI", "C_ARTI");
                            bulkCopy.ColumnMappings.Add("FAMI_ARTI", "FAMI_ARTI");
                            bulkCopy.ColumnMappings.Add("DES_ARTI", "DES_ARTI");
                            bulkCopy.ColumnMappings.Add("CAP_ARTI", "CAP_ARTI");
                            bulkCopy.ColumnMappings.Add("EMPAQUE2", "EMPAQUE2");
                            //bulkCopy.ColumnMappings.Add("PRECIO_VEN", "PRECIO_VEN");
                            //bulkCopy.ColumnMappings.Add("PRECIO_ESP", "PRECIO_ESP");
                            bulkCopy.ColumnMappings.Add("STATUS", "STATUS");
                            bulkCopy.ColumnMappings.Add("C_PROVE", "C_PROVE");
                            bulkCopy.ColumnMappings.Add("C_PROVE2", "C_PROVE2");
                            bulkCopy.ColumnMappings.Add("CANTIDAD", "CANTIDAD");
                            bulkCopy.ColumnMappings.Add("CANCELA", "CANCELA");
                            bulkCopy.ColumnMappings.Add("FALTANTE", "FALTANTE");
                            //bulkCopy.ColumnMappings.Add("UNI_VEN", "UNI_VEN");
                            bulkCopy.ColumnMappings.Add("COSTO", "COSTO");
                            //    //bulkCopy.ColumnMappings.Add("MARGEN", "MARGEN");
                            //    //bulkCopy.ColumnMappings.Add("IEPS", "IEPS");
                            bulkCopy.ColumnMappings.Add("UNIDAD", "UNIDAD");
                            bulkCopy.ColumnMappings.Add("CAJA", "CAJA");
                            bulkCopy.ColumnMappings.Add("EXHIBIDOR", "EXHIBIDOR");
                            //bulkCopy.ColumnMappings.Add("MARG_PRE4", "MARG_PRE4");
                            bulkCopy.ColumnMappings.Add("MARG_PRE4", "MARGEN");
                            //bulkCopy.ColumnMappings.Add("PORCENTAJE", "PORCENTAJE");
                            //bulkCopy.ColumnMappings.Add("POR_CAJA", "POR_CAJA");
                            //bulkCopy.ColumnMappings.Add("OFERTA", "OFERTA");





                            //bulkCopy.ColumnMappings.Add("RESP_COMA", "COMPRADOR");

                            //bulkCopy.BatchSize = 5000;
                            bulkCopy.WriteToServer(_dtGuardarArticulo);
                            _tranArticulos.Commit();
                            //respuesta = true;
                            //actualProcesoDBF++;
                            //_funcion.Cargando(this, barraProgreso, 0, actualProcesoDBF, totalProcesoDBF, lblMensaje, "Información importada: " + tbldtTablas.ToString());
                            //_dtTablas.Rows[j]["dbf"] = false;

                            staGuardado++;
                        }
                        catch (Exception ex)
                        {
                            //correo.SendError(ex, System.Net.Mail.MailPriority.High, "Las ventas del día " + _fecha + " de la Sucursal " + _suc + "" + ex.StackTrace);
                            MessageBox.Show(ex.Message);
                            //respuesta = false;
                            try
                            {
                                _tranArticulos.Rollback();
                            }
                            catch (Exception)
                            {

                                //throw;
                            }

                        }
                        finally
                        {

                            _consqlArti.Close();
                            //copiarTablas = null;
                            //respuesta = true;
                            //MessageBox.Show("Guardado");
                        }


                    }

                }


                if (staGuardado == 2)
                {
                    MessageBox.Show("Se ha guardado", "¡Listo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //FrmIndex.opcProveArti.Enabled = true;
                    
                }
                else
                {
                    MessageBox.Show("Los proveedores o los articulos no se guardaron",":S Algo ocurrio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            GuardarProveedor = null;

            _funcion.DesabilitarControles(this, true);
        }


        private void txtBusArticulo_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                FiltrarArticulo();
            }
            catch (Exception)
            {

                //throw;
            }
            
        }

        private void cBoxBusArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBusArticulo.Focus();
        }

        private void cBoxOrdArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FiltrarArticulo();
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void dgvProveedor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                FiltrarArticulo();
            }
            catch (Exception)
            {

                //throw;
            }
            
            //btnAgregarArti.Enabled = true;
        }

        private void dgvProveedor_SelectionChanged(object sender, EventArgs e)
        {

            try
            {
                btnAgregarArti.Enabled = Convert.ToBoolean(dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value);

                FiltrarArticulo();
                //chBoxArtiTodos.Checked = Convert.ToBoolean(dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value);
            }
            catch (Exception)
            {

                //throw;
            }
        }
        //delegate void DelegateCheckTodos();
        private void chBoxProvTodos_Click(object sender, EventArgs e)
        {
            dgvArticulo.DataSource = _dtArticulo.Clone();
            if (!(string.IsNullOrWhiteSpace(cboxOrdProve.SelectedValue.ToString())))
            {

                MarcarTodProv = new Thread(TodosProvedor);
                MarcarTodProv.IsBackground = true;
                MarcarTodProv.Start();

            }
            else
            {
                chBoxProvTodos.Enabled = false;
            }


        }
       
        public void TodosProvedor()
        {
            _funcion.DesabilitarControles(this, false);
            //this.Invoke(new CamposEnableDelegate(_funcion.CamposEnabled), this, false);
            //Thread.Sleep(2000);
            String idSucur = "";
            this.Invoke((MethodInvoker)delegate
            {
                 idSucur = cBoxSucursal.SelectedValue.ToString();
            });
            IEnumerable<DataRow> sql_dtProve =
                    from dtProve in _dtProveedor.AsEnumerable()
                    where dtProve.Field<String>("ID_SUCURSALALM") == idSucur && dtProve.Field<String>("RESP_COMA") == SesionLetra
                    select dtProve;

                foreach (DataRow rowstProve in sql_dtProve)
                {

                //cust.SetField("C_PROVE","00000");
                //_dtProveedor.Rows[i]["PARTICIPA"] = !Convert.ToBoolean(dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value);
                //rowstProve.SetField("PARTICIPA", Convert.ToBoolean(rowstProve.Field<Boolean>("PARTICIPA")));
                rowstProve.SetField("PARTICIPA", chBoxProvTodos.Checked);
                rowstProve.AcceptChanges();
                    //MessageBox.Show("" + rowstProve.Field<String>("C_PROVE"));
                    //MessageBox.Show("Modificado");
                    //query.Shi
                    //cust.SetField<string>("Mariner") = "";
                    //cust.ShipVia = 2;
                    // Insert any additional changes to column values.

                    IEnumerable<DataRow> sql_dtArti =
                        from dtArti in _dtArticulo.AsEnumerable()
                        where dtArti.Field<String>("C_PROVE") == rowstProve.Field<String>("C_PROVE") && dtArti.Field<String>("ID_SUCURSALALM") == rowstProve.Field<String>("ID_SUCURSALALM")
                        select dtArti;


                    foreach (DataRow rowdtArti in sql_dtArti)
                    {
                        //cust.SetField("C_PROVE","00000");
                        //_dtProveedor.Rows[i]["PARTICIPA"] = !Convert.ToBoolean(dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value);
                        if (!(rowdtArti.Field<String>("STATUS") == "*" || rowdtArti.Field<String>("STATUS") == "INACTIVO"))
                        {
                            rowdtArti.SetField("PARTICIPA", rowstProve.Field<Boolean>("PARTICIPA"));
                            rowdtArti.AcceptChanges();
                        }

                        //MessageBox.Show("Modificado");
                        //query.Shi
                        //cust.SetField<string>("Mariner") = "";
                        //cust.ShipVia = 2;
                        // Insert any additional changes to column values.
                    }
                }

            
            

            


            //String consulta ="";
            //String _forProve = "";
            //String _cBoxSucursal = "";
            //for (int i = 0; i < dgvProveedor.Rows.Count; i++)
            //{
            //IEnumerable<DataRow> sql_dtProve =
            //    from order in orders.AsEnumerable()
            //    where order.Field<String>("C_PROVE") == dgvProveedor.CurrentRow.Cells["C_PROVE"].Value.ToString() && order.Field<String>("ID_SUCURSALALM") == cBoxSucursal.SelectedValue.ToString() && order.Field<String>("RESP_COMA") == SesionLetra
            //    select order;
            //    this.Invoke((MethodInvoker)delegate
            //    {
            //        consulta = "(ID_SUCURSALALM = " + cBoxSucursal.SelectedValue.ToString() + ") AND (c_prove = " + dgvProveedor.Rows[i].Cells["C_PROVE"].Value.ToString() + " AND STATUS <> '*' OR STATUS <> 'INACTIVO')";
            //        _forProve = dgvProveedor.Rows[i].Cells["C_PROVE"].Value.ToString();
            //        _cBoxSucursal = cBoxSucursal.SelectedValue.ToString();
            //    });

            //    _dvArticuloTMP = _dtArticulo.DefaultView;
            //    _dvArticuloTMP.RowFilter = consulta;
            //    //_dvArticuloTMP.Sort = cBoxBusArticulo.SelectedValue.ToString() + " ASC";


            //    if (_dvArticuloTMP.Count > 0)
            //    {
            //        this.Invoke((MethodInvoker)delegate
            //        {
            //            dgvProveedor.Rows[i].Cells["PARTICIPA"].Value = chBoxProvTodos.Checked;
            //        });
            //        //

            //        for (int ii = 0; ii < _dtArticulo.Rows.Count; ii++)
            //        {

            //            if ((_dtArticulo.Rows[ii]["ID_SUCURSALALM"].ToString() == _cBoxSucursal) && (_dtArticulo.Rows[ii]["C_PROVE"].ToString() == _forProve) && (_dtArticulo.Rows[ii]["STATUS"].ToString() != "*") && (_dtArticulo.Rows[ii]["STATUS"].ToString() != "INACTIVO"))
            //            {
            //                _dtArticulo.Rows[ii]["PARTICIPA"] = chBoxProvTodos.Checked;
            //            }
            //        }
            //    }


            //}
            //this.Invoke((MethodInvoker)delegate
            //{
            //    dgvArticulo.DataSource = _dtArticulo.Clone();
            //});


            _funcion.DesabilitarControles(this, true);

        }

        public void CheckTodos()
        {
            String consulta;
            for (int i = 0; i < dgvProveedor.Rows.Count; i++)
            {
                 consulta = "(ID_SUCURSALALM = " + cBoxSucursal.SelectedValue.ToString() + ") AND (c_prove = " + dgvProveedor.Rows[i].Cells["C_PROVE"].Value.ToString() + " AND STATUS <> '*')";


                _dvArticuloTMP = _dtArticulo.DefaultView;
                _dvArticuloTMP.RowFilter = consulta;
                //_dvArticuloTMP.Sort = cBoxBusArticulo.SelectedValue.ToString() + " ASC";


                if (_dvArticuloTMP.Count > 0)
                {
                    dgvProveedor.Rows[i].Cells["PARTICIPA"].Value = chBoxProvTodos.Checked;

                    for (int ii = 0; ii < _dtArticulo.Rows.Count; ii++)
                    {
                        if ((_dtArticulo.Rows[ii]["ID_SUCURSALALM"].ToString() == cBoxSucursal.SelectedValue.ToString()) && (_dtArticulo.Rows[ii]["C_PROVE"].ToString() == dgvProveedor.Rows[i].Cells["C_PROVE"].Value.ToString()) && (_dtArticulo.Rows[ii]["STATUS"].ToString() != "*"))
                        {
                            _dtArticulo.Rows[ii]["PARTICIPA"] = chBoxProvTodos.Checked;
                        }
                    }
                }

               
            }
            dgvArticulo.DataSource = _dtArticulo.Clone();
          
        }

        private void chBoxArtiTodos_Click(object sender, EventArgs e)
        {
            int marcados = 0;
            for (int ii = 0; ii < _dtArticulo.Rows.Count; ii++)
            {
                if ((_dtArticulo.Rows[ii]["ID_SUCURSALALM"].ToString() == cBoxSucursal.SelectedValue.ToString()) && (_dtArticulo.Rows[ii]["C_PROVE"].ToString() == dgvProveedor.CurrentRow.Cells["C_PROVE"].Value.ToString()) && (_dtArticulo.Rows[ii]["STATUS"].ToString() != "*") && (_dtArticulo.Rows[ii]["STATUS"].ToString() != "INACTIVO"))
                {
                    _dtArticulo.Rows[ii]["PARTICIPA"] = Convert.ToBoolean(chBoxArtiTodos.Checked);
                    marcados++;
                    //chBoxProvTodos.Checked = false;
                    //chBoxArtiTodos.Checked = Convert.ToBoolean(dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value);
                }
            }
            if (marcados>0)
            {
                dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value = Convert.ToBoolean(chBoxArtiTodos.Checked);
            }
        }

        private void dgvArticulo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvArticulo.Columns[e.ColumnIndex].Name == "PARTICIPA")
            {
                chBoxArtiTodos.Checked = false;
                //dgvProveedor.Rows[e.ColumnIndex].Cells["PARTICIPA"].Value = true;

                dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value = true;

                var sql_dtArti =
                    from dtArti in _dtArticulo.AsEnumerable()
                    where dtArti.Field<String>("C_ARTI") == dgvArticulo.CurrentRow.Cells["C_PROVE"].Value.ToString() && dtArti.Field<String>("ID_SUCURSALALM") == cBoxSucursal.SelectedValue.ToString() && dtArti.Field<String>("RESP_COMA") == SesionLetra
                    select dtArti;

                foreach (DataRow fila in sql_dtArti)
                {
                    fila.SetField("PARTICIPA",!Convert.ToBoolean(dgvArticulo.CurrentRow.Cells["PARTICIPA"].Value));
                    fila.AcceptChanges();

                    var sql_dtProve =
                        from dtProve in _dtProveedor.AsEnumerable()
                        where dtProve.Field<String>("C_PROVE") == fila.Field<String>("C_PROVE")
                        select dtProve;

                    foreach (DataRow row in sql_dtProve)
                    {
                        row.SetField("PARTICIPA",true);
                        row.AcceptChanges();
                    }

                }

                //if (Convert.ToBoolean(dgvArticulo.CurrentRow.Cells["PARTICIPA"].Value) == false)
                //{
                //    //MessageBox.Show("hola");
                //    String consMarcados = "(PARTICIPA = TRUE) AND (ID_SUCURSALALM = " + cBoxSucursal.SelectedValue.ToString() + ") AND (c_prove = " + dgvProveedor.CurrentRow.Cells["C_PROVE"].Value.ToString() + ")";
                //    foundRows = _dtArticulo.Select(consMarcados);
                //    if (foundRows.LongCount() > 0)
                //    {
                //        //MessageBox.Show("marcados: "+ foundRows.LongCount());
                //        //dgvArticulo.CurrentRow.Cells["PARTICIPA"].Value;
                //        dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value = true;
                //    }
                //    else
                //    {
                //        dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value = false;
                //    }
                //}


            }
                
        }

        private void btnAgregarArti_Click(object sender, EventArgs e)
        {

            //dgvProveedor.CurrentRow.Cells["PARTICIPA"].Value = true;
            
            DataTable _dtProve;
            DataRow[] _drProve = _dtProveedor.Select("ID_SUCURSALALM ="+ cBoxSucursal.SelectedValue.ToString() + " AND participa = false AND RESP_COMA = '" + SesionLetra + "'");

            _dtProve = _dtProveedor.Clone();
            foreach (DataRow fila in _drProve)
            {
                _dtProve.ImportRow(fila);
            }

            FrmAgregarProveArti frmAgregarArti = new FrmAgregarProveArti();
            frmAgregarArti.MdiParent = this.MdiParent;
            frmAgregarArti._CadenaConexion = _CadenaConexion;
            frmAgregarArti._dtProve = _dtProve;
            //frmAgregarArti._dtProveedor = _dtProveedor;
            frmAgregarArti._dtArticulos = _dtArticulo;
            frmAgregarArti._CProve = dgvProveedor.CurrentRow.Cells["C_PROVE"].Value.ToString();
            frmAgregarArti._NomProve = dgvProveedor.CurrentRow.Cells["DESCRI"].Value.ToString();
            frmAgregarArti._NumAlmacen = cBoxSucursal.SelectedValue.ToString();
            //_frmSucusal.Owner = this;
            frmAgregarArti.Show();
            this.Enabled = false;

            

        }

        private void FrmProveArti_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(GuardarProveedor == null))
            {
                MessageBox.Show("No puede cerrar esta ventana","Guardando Información", MessageBoxButtons.OK,MessageBoxIcon.Information);
        
                e.Cancel = true;
                //    GC.Collect();
            }
            else
            {

                //DataTable _dtTblprove = _funcion.llenar_dt("tbl_sucursal", "id");
                //DataTable _dtTblArti = _funcion.llenar_dt("tbl_sucursal", "id");
                //if (_dtTblSucu.Rows.Count > 0)
                //{
                FrmIndex.opcProveArti.Enabled = true;
                FrmIndex.opcPartSuc.Enabled = true;
                FrmIndex.opcImporTabla.Enabled = true;
                //    FrmIndex.opcImporTabla.Enabled = true;
                //}
                //else
                //{
                //    FrmIndex.opcPartSuc.Enabled = true;
                //}

                GC.Collect();
            }

        }

        private void dgvProveedor_MouseMove(object sender, MouseEventArgs e)
        {
            dgvProveedor.Focus();
        }

        private void dgvArticulo_MouseMove(object sender, MouseEventArgs e)
        {
            dgvArticulo.Focus();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            
            if (trdActualizarArticulo == null)
            {
                trdActualizarArticulo = new Thread(actualizarArti);
                trdActualizarArticulo.IsBackground = true;
                trdActualizarArticulo.Start();
            }
            else
            {
                trdActualizarArticulo.Suspend();
                var detener = MessageBox.Show("¿Desea detener la actualización?", "¡Aviso!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (detener == DialogResult.Yes)
                {
                    //btnImportar.Text = "Importar";
                    trdActualizarArticulo.Resume();
                    trdActualizarArticulo.Abort();
                    trdActualizarArticulo = null;
                    //lblMensaje.Text = "...";
                    //barraProgreso.Value = 0;
                    //this.Invoke(new CamposEnableDelegate(_funcion.CamposEnabled), this, true, btnImportar, "Importar");
                    _funcion.DesabilitarControles(this, true, btnActualizar);
                    _funcion.Cargando(this, stripPBEstatus, 0, 0, 1, stripSLEstatus, "...");
                }
                else
                {
                    trdActualizarArticulo.Resume();
                }
            }
        }

        public void actualizarArti()
        {
            _funcion.DesabilitarControles(this, false, btnActualizar);
            //Thread.Sleep(5000);

            CproveFiltrar = "";
            CartiFiltar = "";
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
                //actuRutaBaja = filSucu["RUTA_BAJA"].ToString();
                //actuAgenBaja = filSucu["AGEN_BAJA"].ToString();
                servConex = filSucu["servidor"].ToString();
                servBd = filSucu["db"].ToString();
                servUsua = filSucu["usuario"].ToString();
                servPass = filSucu["contrasena"].ToString();
                organiIdsucu = filSucu["ORGANIZATION_ID"].ToString();
                break;
            }
            
           

            for (int i = 0; i < _tablas.GetLength(1); i++)
            {

                _tablasNombre = _tablas[0, i];
                _esTablaBdf = _tablas[1, i];
                _tablasBDFCampos = _tablas[2, i];
                _esTablaSql = _tablas[3, i];
                _tablasSql = _tablas[4, i];
                _tablasSQLCampos = _tablas[5, i];
                _tablasNomDestino = _tablas[6, i];
                _tablasNomDestinoCampos = _tablas[7, i];
                _tablasNomDestiCamposComparar = _tablas[8, i];


                if (Convert.ToBoolean(_esTablaBdf))//CUANDO EL TRASPASO ES EN BDF
                {
                    if (_funcion.PingServ(servidorSucu))
                    {

                        if (!Directory.Exists(tmpCarpetaLocal))
                        {
                            Directory.CreateDirectory(tmpCarpetaLocal);
                        }

                        if (!Directory.Exists(tmpCarpetaServ))
                        {
                            Directory.CreateDirectory(tmpCarpetaServ); //COLOCAR UN TRY PARA CUANDO NO PUEDA ENTRAR AL SERVIDOR PARA CREAR LAS CARPETAS DE COPIADO
                        }

                        String[] cutTablas = _tablasNombre.ToString().Split(',');
                        String nomTblTablas = "";
                        String nomDbfTablas = "";

                        if (!(idActSucu == "001")) //ESTO SE HIZO POR EL DETALLE DE MERIDA QUE TIENE INV001 Y LAS OTRAS SUCURSALES TIENEN INVENTA
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




                        int actualProcesoDBF = 1;
                        int totalProcesoDBF = 3;
                        _funcion.Cargando(this, stripPBEstatus, 0, actualProcesoDBF, totalProcesoDBF, stripSLEstatus, "Preparando dbf " + nomDbfTablas);

                        //COPIAR LOS DBF EN EL MISMO SERVIDOR CON OTRO NOMBRE
                        var servFptOrigen = Path.Combine(carpetaServ, nomDbfTablas + ".fpt");
                        var servFptDestino = Path.Combine(tmpCarpetaServ, "expo_" + nomDbfTablas + ".fpt");
                        //COPIAR LOS ARCHIVOS A LAS CARPETAS QUE EL SISTEMA CREA
                        var localFptDestino = Path.Combine(tmpCarpetaLocal, nomDbfTablas + ".fpt");

                        //AQUI TE QUEDASTE
                        var servDbfOrigen = Path.Combine(carpetaServ.ToString(), nomDbfTablas + ".dbf");
                        var servDbfDestino = Path.Combine(tmpCarpetaServ, "expo_" + nomDbfTablas + ".dbf");
                        var localDbfDestino = Path.Combine(tmpCarpetaLocal, nomDbfTablas + ".dbf");

                        var servCdxOrigen = Path.Combine(carpetaServ.ToString(), nomDbfTablas + ".cdx");
                        var servCdxDestino = Path.Combine(tmpCarpetaServ, "expo_" + nomDbfTablas + ".cdx");
                        var localCdxDestino = Path.Combine(tmpCarpetaLocal, nomDbfTablas + ".cdx");

                        if (File.Exists(servFptOrigen))
                        {

                            File.Copy(servFptOrigen, servFptDestino, true);

                            _funcion.Cargando(this, stripPBEstatus, 0, 2, 3, stripSLEstatus, "Preparando copiado fpt " + nomDbfTablas);
                            //Thread.Sleep(1000);

                            CopyFile(servFptDestino, localFptDestino);
                            File.Delete(servFptDestino);

                        }

                        if (File.Exists(servDbfOrigen))
                        {
                            File.Copy(servDbfOrigen, servDbfDestino, true);

                            _funcion.Cargando(this, stripPBEstatus, 0, 2, 3, stripSLEstatus, "Preparando copiado dbf " + nomDbfTablas);
                            //Thread.Sleep(1000);

                            CopyFile(servDbfDestino, localDbfDestino);
                            File.Delete(servDbfDestino);
                        }

                        if (File.Exists(servCdxOrigen))
                        {
                            File.Copy(servCdxOrigen, servCdxDestino, true);

                            _funcion.Cargando(this, stripPBEstatus, 0, 2, 3, stripSLEstatus, "Preparando para copiar cdx " + nomDbfTablas);
                            //Thread.Sleep(1000);

                            CopyFile(servCdxDestino, localCdxDestino);
                        }

                        _funcion.Cargando(this, stripPBEstatus, 0, actualProcesoDBF, totalProcesoDBF, stripSLEstatus, "Preparando para importación: " + nomTblTablas);
                        //Thread.Sleep(5000);


                        ProcessStartInfo info = null;
                        //String _RutaTabla = @"tmp_expo\" + nomExpo + @"\" + almdtSucu.ToString() + @"\" + _tablasNombre;
                        //String _RutaTabla = tmpCarpetaLocal + @"\" + _tablasNombre;
                        String _RutaTabla = @"tmp_expo\" + nomExpo + @"\" + _nomUsuario + @"\" + idActSucu + @"\" + nomDbfTablas;
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
                        

                        string cadena = @"Driver={Microsoft Visual Foxpro Driver};UID=;SourceType=DBF;SourceDB=" + tmpCarpetaLocal + " ;Exclusive=No;SHARED=YES;collate=Machine;NULL=NO;DELETED=NO;BACKGROUNDFETCH=YES;";
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


                        String[] archivos = Directory.GetFiles(tmpCarpetaLocal, _tablasNombre + ".*");

                        foreach (String file in archivos)
                        {
                            if (File.Exists(file))
                            {
                                //MessageBox.Show(file);
                                File.Delete(file);
                            }
                        }


                        if (_tablasNombre == "inventa,inv001")
                        {

                            string[] valores = CartiFiltar.Split(',');
                            DataTable _dtArti = new DataTable();

                            _dtArti = dtDBF.Copy();
                            sql_dtTblTablaSql =
                                from tblCArti in _dtArti.AsEnumerable()
                                where valores.Contains(tblCArti.Field<String>("C_ARTI"))
                                select tblCArti;

                            dtDBF.Clear();

                            foreach (DataRow filaArti in sql_dtTblTablaSql)
                            {
                                dtDBF.ImportRow(filaArti);
                            }

                            //this.Invoke((MethodInvoker)delegate
                            //{
                            //    dgvArticulo.DataSource = dtDBF;
                            //});

                            //Thread.Sleep(10000000);


                        }


                        dtDBF.Columns.Add("id_sucursalalm", typeof(String));

                        for (int iAlm = 0; iAlm < dtDBF.Rows.Count; iAlm++)
                        {
                            _funcion.Cargando(this, stripPBEstatus, 0, iAlm, dtDBF.Rows.Count, stripSLEstatus, "Asignando el almacen: " + _tablasNombre);
                            dtDBF.Rows[iAlm]["ID_SUCURSALALM"] = idActSucu;
                        }

                        //if (chRutabaja.Checked)
                        //{
                        //    DataRow[] _dataRow;
                        //    DataTable _tmpDtDBF;
                        //    int posicion = 0;
                        //    _dataRow = dtDBF.Select("C_RUTA <> " + actuRutaBaja);

                        //    _tmpDtDBF = dtDBF.Clone();
                        //    foreach (DataRow fila in _dataRow)
                        //    {
                        //        _tmpDtDBF.ImportRow(fila);
                        //    }
                        //    dtDBF.Clear();
                        //    _dataRow = null;
                        //    _dataRow = _tmpDtDBF.Select();
                        //    foreach (DataRow fila in _dataRow)
                        //    {
                        //        _funcion.Cargando(this, stripPBEstatus, 0, posicion++, _dataRow.Count(), stripSLEstatus, "Filtrando clientes: " + _tablasNombre);
                        //        dtDBF.ImportRow(fila);
                        //    }
                        //}


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
                            //try
                            //{
                            //var hola = "WHERE " + campo[0].ToString() + " = " + dtDBF.Rows[ii][campo[1].ToString()] + " AND " + campo[2].ToString() + " = '" + dtDBF.Rows[ii][campo[3].ToString()] + "'";
                            _dtTmpArticulo = _funcion.llenar_dt(_tablasNomDestino, _tablasNomDestinoCampos, "WHERE " + campo[0].ToString() + " = " + dtDBF.Rows[ii][campo[1].ToString()] + " AND " + campo[2].ToString() + " = '" + dtDBF.Rows[ii][campo[3].ToString()] + "'");
                            //}
                            //catch (Exception)
                            //{

                            //    _dtTmpCliente = _funcion.llenar_dt(_tablasNomDestino, _tablasNomDestinoCampos, "WHERE " + campo[0].ToString() + " = " + dtDBF.Rows[ii][campo[0].ToString()] + " AND " + campo[1].ToString() + " = " + dtDBF.Rows[ii][campo[1].ToString()]);
                            //}


                            totalCiente = _dtTmpArticulo.Rows.Count;

                            for (int iii = 0; iii < camposguardar.Count(); iii++)
                            {
                                dr = _datos.NewRow();
                                dr["campo"] = camposguardar[iii];
                                dr["valor"] = dtDBF.Rows[ii][camposguardar[iii]];
                                dr["tipo"] = "varchar";
                                _datos.Rows.Add(dr);
                            }

                            //if (dtDBF.Rows[ii][campo[1].ToString()].ToString() == "GE40")
                            //{
                            //    MessageBox.Show("totalCiente");
                            //}
                            if (!(totalCiente > 0))
                            {

                                _funcion.Cargando(this, stripPBEstatus, 0, ii, totaldtDBF, stripSLEstatus, _tablasNomDestino + " nuevo registro: " + ii + "/" + totaldtDBF);
                                _sql = _funcion._sql(_datos, _tablasNomDestino, "nuevo", "");
                            }
                            else
                            {

                                _funcion.Cargando(this, stripPBEstatus, 0, ii, totaldtDBF, stripSLEstatus, _tablasNomDestino + " modificando registro: " + ii + " / " + totaldtDBF);
                                var hola = "WHERE " + campo[0].ToString() + " = " + dtDBF.Rows[ii][campo[1].ToString()] + " AND " + campo[2].ToString() + " = " + dtDBF.Rows[ii][campo[3].ToString()];
                                _sql = _funcion._sql(_datos, _tablasNomDestino, "modificar", "WHERE " + campo[0].ToString() + " = " + dtDBF.Rows[ii][campo[1].ToString()] + " AND " + campo[2].ToString() + " = '" + dtDBF.Rows[ii][campo[3].ToString()]+"'");
                            }
                            try
                            {
                                
                                guardarClientes(_sql, _CadenaConexion);
                            }
                            catch (Exception e)
                            {

                                MessageBox.Show(e.Message, "¡Espera!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }

                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            MessageBox.Show("No se pudo conectar al servidor", "¡Espera!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        });
                    }


                }
                if (Convert.ToBoolean(_esTablaSql)) //TRASPASO DE INFORMACIÓN SQL
                {

                    int totalProcesoDBF = 4;
                                int actualProcesoDBF = 1;
                                String CadenaConexionSql = "Data Source=" + servConex + ";"
                                      + "Initial Catalog=" + servBd + ";"
                                      + "Integrated Security=false;"
                                      + "UID=" + servUsua + ";"
                                      + "PWD=" + servPass + ";";

                    using (SqlConnection _consqlOrigen = new SqlConnection(CadenaConexionSql))
                    {
                        if (_consqlOrigen.State == ConnectionState.Closed)
                        {
                            _funcion.Cargando(this, stripPBEstatus, 0, actualProcesoDBF, totalProcesoDBF, stripSLEstatus, "Conectando a la base de datos");
                            _consqlOrigen.Open();
                        }

                        SqlCommand sqlComTablas = new SqlCommand(_tablasSql, _consqlOrigen);
                        sqlComTablas.CommandTimeout = 10000;
                        if (!(_tablasNombre == "dbf_estatus"))
                        {

                            sqlComTablas.Parameters.AddWithValue("@ORGANIZATION_ID", organiIdsucu);
                            if (_tablasNombre == "proveedo")
                            {
                                sqlComTablas.Parameters.AddWithValue("@RESP_COMA", SesionLetra);
                            }
                            //if (_tablasNombre == "articulo")
                            //{
                            //    sqlComTablas.Parameters.Add("@CPROVE", SqlDbType.VarChar).Value = CproveFiltrar;
                            //}

                        }

                        SqlDataAdapter myAdaptador = new SqlDataAdapter(sqlComTablas);
                        _dtTblTablaSql = new DataTable();
                        myAdaptador.Fill(_dtTblTablaSql);


                        if (_tablasNombre == "articulo")
                        {


                            var estatus = "";
                            string[] valores = CproveFiltrar.Split(',');
                            //var status = new List<string> { "2", "4", "7", "8", "12", "19", "20", "22", "33", "35" };
                            DataTable _dtTmpArti = new DataTable();
                            DataTable _dtArti = new DataTable();
                            DataRow _drArti;

                            _dtArti.Columns.Add("c_arti");
                            _dtArti.Columns.Add("estatus");

                            _dtArti.Clear();
                            _dtTmpArti = _dtTblTablaSql.Copy();

                            sql_dtTblTablaSql =
                                from tblCProve in _dtTmpArti.AsEnumerable()
                                where valores.Contains(tblCProve["NO_PROV_AFECTA_PRECIO"])
                                orderby tblCProve["SEGMENT1"], tblCProve["FECHA_CREACION"]
                                select tblCProve;


                            //var sql_CArti =
                            //    from tblCProve in sql_dtTblTablaSql
                            //    where valores.Contains(tblCProve["NO_PROV_AFECTA_PRECIO"])
                            //    group tblCProve by tblCProve["SEGMENT1"] into grupo
                            //    select grupo;

                            //foreach (var fila in sql_CArti)
                            //{

                            //    foreach (DataRow row in fila)
                            //    {
                            //        //estatus += name["SEGMENT1"];

                            //        estatus = "";
                            //        //estatus += row["SEGMENT1"] + ":";
                            var arti = "";
                            foreach (var filadtArti in sql_dtTblTablaSql)
                            {


                                if (arti == filadtArti["SEGMENT1"].ToString())
                                {
                                    estatus += filadtArti.Field<String>("status_number") + ",";

                                }
                                else
                                {
                                    if (!(String.IsNullOrWhiteSpace(arti)))
                                    {
                                        _drArti = _dtArti.NewRow();
                                        _drArti["c_arti"] = arti;
                                        _drArti["estatus"] = estatus.TrimEnd(',');
                                        _dtArti.Rows.Add(_drArti);
                                    }
                                    estatus = filadtArti["status_number"].ToString() + ",";
                                    arti = filadtArti["SEGMENT1"].ToString();

                                }
                                //MessageBox.Show(filadtArti["SEGMENT1"].ToString() + " = " + estatus);
                                //}
                                //MessageBox.Show(filadtArti["SEGMENT1"].ToString() + " = " + estatus);
                            }
                            foreach (var filaEsta in _dtArti.AsEnumerable())
                            {
                                foreach (var filadtArti in sql_dtTblTablaSql)
                                {
                                
                                    if (filaEsta["c_arti"].ToString() == filadtArti["SEGMENT1"].ToString())
                                    {
                                        filadtArti.SetField("status_number", filaEsta["estatus"]);
                                        filadtArti.AcceptChanges();
                                    }
                                }
                            }

                            //var sql_CArti =
                            //    from tblCProve in sql_dtTblTablaSql
                            //    where valores.Contains(tblCProve["NO_PROV_AFECTA_PRECIO"])
                            //    group tblCProve by tblCProve["SEGMENT1"] into grupo
                            //    select grupo;

                            //foreach (var fila in sql_CArti)
                            //{

                            //    estatus = "";
                            //    foreach (DataRow row in fila)
                            //    {
                            //        //estatus += name["SEGMENT1"];


                            //        //estatus += row["SEGMENT1"] + ":";
                            //        foreach (var filadtArti in sql_dtTblTablaSql)
                            //        {

                            //            //if (row["SEGMENT1"] == filadtArti["SEGMENT1"])
                            //            //{
                            //            //    MessageBox.Show(row["SEGMENT1"] + "--" + filadtArti["SEGMENT1"]);
                            //            //}

                            //            if (filadtArti["SEGMENT1"].ToString() == "GE40")
                            //            {
                            //                MessageBox.Show(filadtArti["SEGMENT1"].ToString());
                            //            }
                            //        }


                            //        break;
                            //    }

                            //    MessageBox.Show(estatus);
                            //}



                            _dtTblTablaSql.Clear();

                            _dtTblTablaSql = _dtTmpArti.Copy();

                            //foreach (DataRow filaArti in sql_dtTblTablaSql)
                            //{

                            //    foreach (DataRow filaBuscar in sql_dtTblTablaSql)
                            //    {
                            //        if (filaArti.Field<String>("SEGMENT1") == filaBuscar.Field<String>("SEGMENT1"))
                            //        {
                            //            estatus += "("+ filaBuscar.Field<String>("SEGMENT1") +")"+ filaBuscar.Field<String>("status_number") + ",";
                            //        }
                            //    }
                            //    //estatus += filaArti.Field<String>("status_number") + ",";
                            //    //_dtTblTablaSql.ImportRow(filaArti);
                            //    //if (filaArti.Field<String>("NO_PROV_AFECTA_PRECIO") == "02474")
                            //    //{
                            //    //    MessageBox.Show(filaArti.Field<String>("NO_PROV_AFECTA_PRECIO"));
                            //    //}
                            //}
                            //MessageBox.Show(estatus);
                            ////this.Invoke((MethodInvoker)delegate {
                            ////    dgvArticulo.DataSource = _dtTblTablaSql;
                            ////});


                        }

                        _dtTblTablaSql.Columns.Add("ID_SUCURSALALM", typeof(String));
                        if (_tablasNombre == "proveedo")
                        {
                            _dtTblTablaSql.Columns.Add("ESTATUS", typeof(String));
                            //_dtCProve.Clear();
                            
                        }

                        for (int iAlm = 0; iAlm < _dtTblTablaSql.Rows.Count; iAlm++)
                        {
                            _funcion.Cargando(this, stripPBEstatus, 0, iAlm, _dtTblTablaSql.Rows.Count, stripSLEstatus, "Asignando el almacen: " + _tablasNombre);
                            _dtTblTablaSql.Rows[iAlm]["ID_SUCURSALALM"] = idActSucu;


                            

                            if (_tablasNombre == "proveedo")
                            {

                                //IIF(!ISNULL(inactive_date) OR !EMPTY(BAJA_GRAL), '*', '')
                                if (_dtTblTablaSql.Rows[iAlm]["inactive_date"].ToString().Length > 0)
                                {
                                    _dtTblTablaSql.Rows[iAlm]["ESTATUS"] = "*";
                                }
                                else if (_dtTblTablaSql.Rows[iAlm]["BAJA_GRAL"].ToString().Length > 0)
                                {
                                    _dtTblTablaSql.Rows[iAlm]["ESTATUS"] = "*";
                                }
                                else
                                {
                                    _dtTblTablaSql.Rows[iAlm]["ESTATUS"] = " ";
                                }

                                if (!(iAlm == _dtTblTablaSql.Rows.Count-1))
                                {
                                    CproveFiltrar += _dtTblTablaSql.Rows[iAlm]["SEGMENT1"].ToString() + ",";
                                }
                                else
                                {
                                    CproveFiltrar += _dtTblTablaSql.Rows[iAlm]["SEGMENT1"].ToString();
                                }

                                //_drCProve = _dtCProve.NewRow();
                                //_drCProve["C_PROVE"] = _dtTblTablaSql.Rows[iAlm]["SEGMENT1"];
                                //_dtCProve.Rows.Add(_drCProve);

                            }

                            if (_tablasNombre == "articulo")
                            {
                                _dtTblTablaSql.Rows[iAlm]["ATTRIBUTE2"] = _dtTblTablaSql.Rows[iAlm]["ATTRIBUTE2"] + " " + _dtTblTablaSql.Rows[iAlm]["ATTRIBUTE4"];
                                //sqlComTablas.Parameters.AddWithValue("@ORGANIZATION_ID", _dtSucursal.Rows[i]["ORGANIZATION_ID"]);
                                if (!(iAlm == _dtTblTablaSql.Rows.Count - 1))
                                {
                                    CartiFiltar += _dtTblTablaSql.Rows[iAlm]["SEGMENT1"].ToString() + ",";
                                }
                                else
                                {
                                    CartiFiltar += _dtTblTablaSql.Rows[iAlm]["SEGMENT1"].ToString();
                                }
                            }

                        }

                        int totaldtSQL = _dtTblTablaSql.Rows.Count;
                        int totalArticulo;
                        String[] campo = _tablasNomDestiCamposComparar.ToString().Split('-');
                        String[] camposOrigen = _tablasSQLCampos.ToString().Split(',');
                        String[] camposDestino = _tablasNomDestinoCampos.ToString().Split(',');
                        String _sql = "";
                        DataTable _datos = new DataTable();
                        DataRow dr;
                        _datos.Columns.Add("campo", typeof(String));
                        _datos.Columns.Add("valor", typeof(String));
                        _datos.Columns.Add("tipo", typeof(String));
                        for (int ii = 0; ii < totaldtSQL; ii++)
                        {
                            _datos.Clear();

                           

                                //String hola = "WHERE " + campo[0].ToString() + " = " + _dtTblTablaSql.Rows[ii][campo[1].ToString()] + " AND " + campo[2].ToString() + " = " + _dtTblTablaSql.Rows[ii][campo[3].ToString()];
                                _dtTmpArticulo = _funcion.llenar_dt(_tablasNomDestino, _tablasNomDestinoCampos, "WHERE " + campo[0].ToString() + " = " + _dtTblTablaSql.Rows[ii][campo[1].ToString()].ToString().Trim() + " AND " + campo[2].ToString() + " = '" + _dtTblTablaSql.Rows[ii][campo[3].ToString()].ToString().Trim()+"'");

                            //MessageBox.Show("WHERE " + campo[0].ToString() + " = " + _dtTblTablaSql.Rows[ii][campo[1].ToString()] + " AND " + campo[2].ToString() + " = '" + _dtTblTablaSql.Rows[ii][campo[3].ToString()] + "'");

                            totalArticulo = _dtTmpArticulo.Rows.Count;

                            for (int iii = 0; iii < camposOrigen.Count(); iii++)
                            {
                                dr = _datos.NewRow();
                                dr["campo"] = camposDestino[iii];
                                dr["valor"] = _dtTblTablaSql.Rows[ii][camposOrigen[iii]];
                                dr["tipo"] = "varchar";
                                _datos.Rows.Add(dr);
                            }


                            if (!(totalArticulo > 0))
                            {

                                _funcion.Cargando(this, stripPBEstatus, 0, ii, totaldtSQL, stripSLEstatus, _tablasNomDestino+" nuevo registro: " + ii + "/" + totaldtSQL);
                                _sql = _funcion._sql(_datos, _tablasNomDestino, "nuevo", "");
                            }
                            else
                            {

                                _funcion.Cargando(this, stripPBEstatus, 0, ii, totaldtSQL, stripSLEstatus, _tablasNomDestino+" modificando registro: " + ii + " / " + totaldtSQL);
                                _sql = _funcion._sql(_datos, _tablasNomDestino, "modificar", "WHERE " + campo[0].ToString() + " = " + _dtTblTablaSql.Rows[ii][campo[1].ToString()] + " AND " + campo[2].ToString() + " = '" + _dtTblTablaSql.Rows[ii][campo[3].ToString()]+"'");
                            }
                            //try
                            //{

                                //ThreadGuardarClie = new Thread(delegate ()
                                //{
                                guardarArticulo(_sql, _CadenaConexion);
                                //});
                                //ThreadGuardarClie.Start();

                                ////_con.Open();
                                //SqlCommand comando = new SqlCommand(_sql, _con);
                                ////resultado = comando.ExecuteNonQuery();
                                //comando.CommandTimeout = 10000;
                                //comando.ExecuteNonQuery();
                                ////            __dblocal.Close();
                            //}
                            //catch (Exception e)
                            //{
                            //    this.Invoke((MethodInvoker)delegate
                            //    {
                            //        MessageBox.Show(e.Message, "¡Espera!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    });

                            //}


                        }

                        if (_consqlOrigen.State == ConnectionState.Open)
                        {
                            _funcion.Cargando(this, stripPBEstatus, 0, actualProcesoDBF, totalProcesoDBF, stripSLEstatus, "Desconectando a la base de datos");
                            _consqlOrigen.Close();
                        }

                    }
                    //CargarInformacion();
                    //_funcion.DesabilitarControles(this, false, btnActualizar);
                }
                else
                {

                }



            }

            _funcion.Cargando(this, stripPBEstatus, 0, 1, 1, stripSLEstatus, "Terminando proceso...");

            Thread.Sleep(500);
            _funcion.Cargando(this, stripPBEstatus, 0, 0, 1, stripSLEstatus, "...");
            _funcion.DesabilitarControles(this, true, btnActualizar);
            trdActualizarArticulo = null;

            CargarInfo = new Thread(CargarInformacion);
            CargarInfo.IsBackground = true;
            CargarInfo.Start();
        }

        public void guardarArticulo(String sql, String conexion)
        {
            using (SqlConnection _con = new SqlConnection(conexion))
            {
                if (_con.State == ConnectionState.Closed)
                {
                    _con.Open();
                }
                //_con.Open();
                SqlCommand comando = new SqlCommand(sql, _con);
                //resultado = comando.ExecuteNonQuery();
                comando.CommandTimeout = 10000;
                comando.ExecuteNonQuery();
                //            __dblocal.Close();
                if (_con.State == ConnectionState.Open)
                {
                    _con.Close();
                }
            }
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

        public void guardarClientes(String sql, String conexion)
        {
            using (SqlConnection _con = new SqlConnection(conexion))
            {
                if (_con.State == ConnectionState.Closed)
                {
                    _con.Open();
                }
                //_con.Open();
                SqlCommand comando = new SqlCommand(sql, _con);
                //resultado = comando.ExecuteNonQuery();
                comando.CommandTimeout = 10000;
                comando.ExecuteNonQuery();
                //            __dblocal.Close();
                if (_con.State == ConnectionState.Open)
                {
                    _con.Close();
                }
            }
        }

    }
}
