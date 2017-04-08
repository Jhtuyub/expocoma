using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EXPOCOMA.Stand
{
    public partial class FrmProveArti : Form
    {
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

        String Sesion = "ALEX  CARDENAS";
        String SesionLetra = "J";

        private Thread CargarInfo;
        private Thread MarcarTodProv;
        private Thread MarcarTodArti;
        private Thread GuardarProveedor;

        IEnumerable<DataRow> sql_dtProve;
        IEnumerable<DataRow> sql_dtArtiGuardados;
        IEnumerable<DataRow> sql_dtArtiGuar;

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
            _dtSucursales = _funcion.llenar_form("tbl_sucursal", "anfitrion DESC", "almacen, sucursal");
            _dtProveedor = _funcion.llenar_form("dbf_proveedo", "c_prove ASC", "id, ID_SUCURSALALM, C_PROVE, DESCRI, RESP_COMA,  C_PROVE2");
            _dtArticulo = _funcion.llenar_form("dbf_articulo", "c_prove ASC", "id, ID_SUCURSALALM, C_ARTI, FAMI_ARTI, DES_ARTI, CAP_ARTI, EMPAQUE2, STATUS, C_PROVE, C_PROVE2, CANTIDAD, CANCELA, FALTANTE, COSTO, UNIDAD, CAJA, EXHIBIDOR, MARG_PRE4");
            _dtProveGuardados = _funcion.llenar_dt("tbl_provexpo", "*", "WHERE COMPRADOR = '" + SesionLetra + "'");
            _dtArtiGuardados = _funcion.llenar_dt("tbl_artiexpo", "*");
            //id, ID_SUCURSALALM, C_PROVE, C_ARTI, DES_ARTI, EMPAQUE2, CAP_ARTI, STATUS
            this.Invoke((MethodInvoker)delegate
                {
                    cBoxSucursal.DataSource = _dtSucursales;
                    cBoxSucursal.ValueMember = "ALMACEN";//"valor";
                    cBoxSucursal.DisplayMember = "SUCURSAL"; //"opcion";

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
                //dgvArticulo.Columns["STATUS"].Visible = true;
                dgvArticulo.Columns["STATUS"].HeaderText = "ST";
                dgvArticulo.Columns["STATUS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvArticulo.Columns["STATUS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
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

            GC.Collect();

            _funcion.DesabilitarControles(this, true);

        }

        private void FrmProveArti_Load(object sender, EventArgs e)
        {
            //CargarInformacion();
            CargarInfo = new Thread(CargarInformacion);
            CargarInfo.IsBackground = true;
            CargarInfo.Start();

            _frmProveArti = this;

           

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
                }else
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
                //copiarTablas.Suspend();
                //var detener = MessageBox.Show("¿Desea detener la importación?", "¡Aviso!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                //if (detener == DialogResult.Yes)
                //{
                //    copiarTablas.Resume();
                //    btnImportar.Text = "Importar";
                //    copiarTablas.Abort();
                //    copiarTablas = null;
                //    lblMensaje.Text = "...";
                //    barraProgreso.Value = 0;
                //    this.Invoke(new CamposEnableDelegate(_funcion.CamposEnabled), this, true, btnImportar, "Importar");
                //    //this.Invoke(new CamposEnableDelegate(_funcion.CamposEnabled), this, true, btnImportar);

                //    FrmIndex.opcPartSuc.Enabled = true;
                //    FrmIndex.opcImporTabla.Enabled = true;

                e.Cancel = true;
                //    GC.Collect();
            }
            else
            {
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
        //else
        //{
        //    FrmIndex.opcPartSuc.Enabled = true;
        //    FrmIndex.opcImporTabla.Enabled = true;
        //    GC.Collect();
        //}
        //}
    }
}
