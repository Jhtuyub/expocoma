using EXPOCOMA.Funciones;
using EXPOCOMA.inicio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EXPOCOMA.Stand
{
    public partial class FrmSucursales : Form
    {
        funcionesdblocal _funciondblocal = new funcionesdblocal();
        funciones _funciones = new funciones();
        public String _CadenaConexion;

        internal static DataGridView _DgvSucursal;
        internal static Form _frmSucursal;
        internal static DataTable _dtcat;
        


        DataRow _drcat;
         DataTable _dtbl;
        DataRow _drtbl;

        public FrmSucursales()
        {
            InitializeComponent();
        }

        //public static void validarSucursal()
        //{
            
        //}
        private void FrmSucursales_Load(object sender, EventArgs e)
        {
            _funciones.icono(this);
            //validarSucursal();

            _funciones._SQLCadenaConexion = _CadenaConexion;//CONEXION PARA LA BASE DE DATOS SQLSERVER2015 EXPRESS

            //_funciones._TIPObasedatos = "dbcompaq";
            //_funciones.cargar_datos(dgvCatEmpresa, "cat_sucursal");
            _dtcat = _funciondblocal.llenar_form("cat_sucursal");
            dgvCatEmpresa.DataSource = _dtcat;
            //dgvCatEmpresa.ClearSelection();
            dgvCatEmpresa.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCatEmpresa.Columns["id"].Visible = false;
            dgvCatEmpresa.Columns["dbf"].Visible = false;
            dgvCatEmpresa.Columns["usuario"].Visible = false;
            dgvCatEmpresa.Columns["contrasena"].Visible = false;
            dgvCatEmpresa.Columns["servidor"].Visible = false;
            dgvCatEmpresa.Columns["db"].Visible = false;
            dgvCatEmpresa.Columns["almacen"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvCatEmpresa.Columns["almacen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCatEmpresa.Columns["almacen"].HeaderText = "alm";
            dgvCatEmpresa.Columns["organization_id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvCatEmpresa.Columns["organization_id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCatEmpresa.Columns["organization_id"].HeaderText = "alm sql";
            dgvCatEmpresa.Columns["servidorsucu"].Visible = false;
            dgvCatEmpresa.Columns["ruta_baja"].Visible = false;
            dgvCatEmpresa.Columns["agen_baja"].Visible = false;

            dgvCatEmpresa.DefaultCellStyle.SelectionBackColor = Properties.Settings.Default.filaSeleccion;
            dgvCatEmpresa.AlternatingRowsDefaultCellStyle.BackColor = Properties.Settings.Default.filaAltern;

            //_funciones._TIPObasedatos = "sqlserver";
            //_funciones.cargar_datos(dgvTblEmpresa, "tbl_sucursal");
            _dtbl = _funciones.llenar_form("tbl_sucursal");
            dgvTblEmpresa.DataSource = _dtbl;
            //dgvTblEmpresa.ClearSelection();
            //dgvTblEmpresa.CurrentCell = dgvTblEmpresa.Rows[0].Cells[0];
            //dgvTblEmpresa.Columns["anfitrion"].Visible = false;
            dgvTblEmpresa.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTblEmpresa.Columns["anfitrion"].ReadOnly = false;
            dgvTblEmpresa.Columns["anfitrion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvTblEmpresa.Columns["id"].Visible = false;
            dgvTblEmpresa.Columns["id_catsucursal"].Visible = false;
            dgvTblEmpresa.Columns["almacen"].ReadOnly = true;
            dgvTblEmpresa.Columns["almacen"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvTblEmpresa.Columns["almacen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTblEmpresa.Columns["almacen"].HeaderText = "alm";
            dgvTblEmpresa.Columns["organization_id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvTblEmpresa.Columns["organization_id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTblEmpresa.Columns["organization_id"].HeaderText = "alm sql";
            dgvTblEmpresa.Columns["servidorsucu"].Visible = false;
            dgvTblEmpresa.Columns["sucursal"].ReadOnly = true;
            dgvTblEmpresa.Columns["servidor"].ReadOnly = true;
            dgvTblEmpresa.Columns["dbf"].Visible = false;
            dgvTblEmpresa.Columns["usuario"].Visible = false;
            dgvTblEmpresa.Columns["contrasena"].Visible = false;
            dgvTblEmpresa.Columns["db"].Visible = false;
            dgvTblEmpresa.Columns["ruta_baja"].Visible = false;
            dgvTblEmpresa.Columns["agen_baja"].Visible = false;
            dgvTblEmpresa.Columns["servidor"].Visible = false;

            dgvTblEmpresa.DefaultCellStyle.SelectionBackColor = Properties.Settings.Default.filaSeleccion;
            dgvTblEmpresa.AlternatingRowsDefaultCellStyle.BackColor = Properties.Settings.Default.filaAltern;
            //dgvTblEmpresa.Sort(dgvTblEmpresa.Columns["anfitrion"], ListSortDirection.Descending);

            _DgvSucursal = dgvCatEmpresa;
            _frmSucursal = this;

            for (int i = 0; i < _dtbl.Rows.Count; i++)
            {
                for (int j = 0; j < _dtcat.Rows.Count; j++)
                {
                    if (_dtcat.Rows[j]["almacen"].ToString() == _dtbl.Rows[i]["almacen"].ToString())
                    {
                        _dtcat.Rows.RemoveAt(j);
                    }
                }
            }

            

        }

        

        public static void enabledTrue()
        {
            FrmSucursales _sucu = new FrmSucursales();
            _sucu.Enabled = true;
            
        }


        private void FrmSucursales_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataTable _dtTblSucu = _funciones.llenar_dt("tbl_sucursal", "id");
            if (_dtTblSucu.Rows.Count > 0)
            {
                FrmIndex.opcPartSuc.Enabled = true;
                FrmIndex.opcImporTabla.Enabled = true;
            }else
            {
                FrmIndex.opcPartSuc.Enabled = true;
            }
            
            GC.Collect();
        }

        private void altaSucursalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmSucursales _sucu = new FrmSucursales();
            //_sucu.Opacity = 0.50;
            //this.Hide();
            //this.TransparencyKey = this.BackColor;

            frmCURDAdminSucursal _CURDSucursal = new frmCURDAdminSucursal();
            _CURDSucursal.Owner = this;
            _CURDSucursal.___accion = "nuevo";
            _CURDSucursal.___idDato = 0;
            _CURDSucursal.___dtTbl = _dtbl;
            _CURDSucursal._Form = "FrmSucursales";
            //_CURDSucursal.TopMost = true;
            _CURDSucursal.MdiParent = this.MdiParent;
            //_CURDSucursal.Activate();
            _CURDSucursal.Show();
            this.Enabled = false;
            
        }

        private void FrmSucursales_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnPasar_Click(object sender, EventArgs e)
        {

            

            Boolean anfitrion =false;
            if (dgvCatEmpresa.Rows.Count > 0)
            {
                //dgvCatEmpresa.Rows[0].Selected = true;

                //if (_dtbl.Rows.Count>0)
                //{
                    
                //    for (int i = 0; i < _dtbl.Rows.Count; i++)
                //    {
                //        DataRow[] buscaRenglon = _dtbl.Select("anfitrion = '"+ anfitrion + "'", "anfitrion");
                        
                //    }
                //}
                

                var _idDato = dgvCatEmpresa.CurrentRow.Cells["id"].Value.ToString();
                //orden = dgvTblEmpresa.Rows.Count + 1;
                String almacen = dgvCatEmpresa.CurrentRow.Cells["almacen"].Value.ToString();
                String organizator = dgvCatEmpresa.CurrentRow.Cells["organization_id"].Value.ToString();
                String sucursal = dgvCatEmpresa.CurrentRow.Cells["sucursal"].Value.ToString();
                String servidor = dgvCatEmpresa.CurrentRow.Cells["servidor"].Value.ToString();
                String servidorsucu = dgvCatEmpresa.CurrentRow.Cells["servidorsucu"].Value.ToString();
                String dbf = dgvCatEmpresa.CurrentRow.Cells["dbf"].Value.ToString();
                String usuario = dgvCatEmpresa.CurrentRow.Cells["usuario"].Value.ToString();
                String contrasena = dgvCatEmpresa.CurrentRow.Cells["contrasena"].Value.ToString();
                String db = dgvCatEmpresa.CurrentRow.Cells["db"].Value.ToString();
                String rutaBaja = dgvCatEmpresa.CurrentRow.Cells["ruta_baja"].Value.ToString();
                String agenBaja = dgvCatEmpresa.CurrentRow.Cells["agen_baja"].Value.ToString();

                _drtbl = _dtbl.NewRow();
                _drtbl["id_catsucursal"] = _idDato;
                _drtbl["anfitrion"] = anfitrion;
                _drtbl["almacen"] = almacen;
                _drtbl["organization_id"] = organizator;
                _drtbl["sucursal"] = sucursal;
                _drtbl["servidor"] = servidor;
                _drtbl["servidorsucu"] = servidorsucu;
                _drtbl["dbf"] = dbf;
                _drtbl["usuario"] = usuario;
                _drtbl["contrasena"] = contrasena;
                _drtbl["db"] = db;
                _drtbl["ruta_baja"] = rutaBaja;
                _drtbl["agen_baja"] = agenBaja;
                _dtbl.Rows.Add(_drtbl);

               

                _dtcat.Rows.RemoveAt(dgvCatEmpresa.CurrentRow.Index);

                dgvTblEmpresa.DataSource = _dtbl;
                dgvCatEmpresa.DataSource = _dtcat;

                //MessageBox.Show("" + _dtbl.Rows.Count);
                //String[,] _DatoSql =
                //    {
                //        {"orden","almacen","sucursal","servidor","serv_fact","copy_files","path_corp"},
                //        {orden, almacen, sucursal, servidor, serv_fact, copy_files, path_corp},
                //        {"int","varchar","varchar","varchar","varchar","varchar","varchar"}
                //    };
                //_funciones._TIPObasedatos = "sqlserver";
                //_funciones.guardar_datos(_DatoSql, "tbl_sucursal", "nuevo", 0, dgvTblEmpresa);
            }
        }

        private void btnRegre_Click(object sender, EventArgs e)
        {
            if (dgvTblEmpresa.Rows.Count > 0)
            {
                //DataRow[] rows = _dtbl.Select();
                //DataRow row = rows[1];
                //var ind= _dtbl.Rows[1];
                //int index = _dtbl.Rows.IndexOf(ind);
                //_dtbl.Rows[0].curre
                //dgvTblEmpresa.Rows[1].Selected = true;

                var _idDato = dgvTblEmpresa.CurrentRow.Cells["id_catsucursal"].Value.ToString();
                //String orden = "1";
                String almacen = dgvTblEmpresa.CurrentRow.Cells["almacen"].Value.ToString();
                String organizator = dgvTblEmpresa.CurrentRow.Cells["organization_id"].Value.ToString();
                String sucursal = dgvTblEmpresa.CurrentRow.Cells["sucursal"].Value.ToString();
                String servidor = dgvTblEmpresa.CurrentRow.Cells["servidor"].Value.ToString();
                String servidorsucu = dgvTblEmpresa.CurrentRow.Cells["servidorsucu"].Value.ToString();
                String dbf = dgvTblEmpresa.CurrentRow.Cells["dbf"].Value.ToString();
                String usuario = dgvTblEmpresa.CurrentRow.Cells["usuario"].Value.ToString();
                String contrasena = dgvTblEmpresa.CurrentRow.Cells["contrasena"].Value.ToString();
                String db = dgvTblEmpresa.CurrentRow.Cells["db"].Value.ToString();
                String rutaBaja = dgvTblEmpresa.CurrentRow.Cells["ruta_baja"].Value.ToString();
                String agenBaja = dgvTblEmpresa.CurrentRow.Cells["agen_baja"].Value.ToString();

                _drcat = _dtcat.NewRow();
                _drcat["id"] = _idDato;
                //_drcat["orden"] = orden;
                _drcat["almacen"] = almacen;
                _drcat["organization_id"] = organizator;
                _drcat["sucursal"] = sucursal;
                _drcat["servidor"] = servidor;
                _drcat["servidorsucu"] = servidorsucu;
                _drcat["dbf"] = dbf;
                _drcat["usuario"] = usuario;
                _drcat["contrasena"] = contrasena;
                _drcat["db"] = db;
                _drcat["ruta_baja"] = rutaBaja;
                _drcat["agen_baja"] = agenBaja;
                _dtcat.Rows.Add(_drcat);

                _dtbl.Rows.RemoveAt(dgvTblEmpresa.CurrentRow.Index);

                dgvTblEmpresa.DataSource = _dtbl;
                dgvCatEmpresa.DataSource = _dtcat;
                //var _idDato = dgvTblEmpresa.CurrentRow.Cells["id"].Value.ToString();
                //_funciones.eliminar_datos("tbl_sucursal", _idDato, dgvTblEmpresa);
            }
            
        }

        private void dgvCatEmpresa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnPasar.PerformClick();
        }

        private void dgvTblEmpresa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnRegre.PerformClick();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int anfi = 0;
            for (int i = 0; i < _dtbl.Rows.Count; i++)
            {
                
                    if (Convert.ToBoolean(_dtbl.Rows[i]["anfitrion"].ToString()) == true)
                    {
                        anfi++;
                        break;
                    }
                
            }

            if (anfi > 0)
            {
                //_funciones._TIPObasedatos = "sqlserver";
                _funciones.LimpiarTabla("tbl_sucursal");
                int insertado = 0;
                for (int i = 0; i < _dtbl.Rows.Count; i++)
                {
                    //_dtbl.Rows[i]["id_catsucursal"].ToString();

                    String _idDato = _dtbl.Rows[i]["id_catsucursal"].ToString();
                    String anfitrion = _dtbl.Rows[i]["anfitrion"].ToString();
                    String almacen = _dtbl.Rows[i]["almacen"].ToString();
                    String organization = _dtbl.Rows[i]["organization_id"].ToString();
                    String sucursal = _dtbl.Rows[i]["sucursal"].ToString();
                    String servidor = _dtbl.Rows[i]["servidor"].ToString();
                    String servidorsucu = _dtbl.Rows[i]["servidorsucu"].ToString();
                    String dbf = _dtbl.Rows[i]["dbf"].ToString();
                    String usuario = _dtbl.Rows[i]["usuario"].ToString();
                    String contrasena = _dtbl.Rows[i]["contrasena"].ToString();
                    String db = _dtbl.Rows[i]["db"].ToString();
                    String rutaBaja = _dtbl.Rows[i]["ruta_baja"].ToString();
                    String agenBaja = _dtbl.Rows[i]["agen_baja"].ToString();

                    String[,] _datosSQL =
                        {
                    {"id_catsucursal", "anfitrion","almacen", "organization_id","sucursal",
                            "servidor","servidorsucu","dbf","usuario","contrasena", "db",
                        "ruta_baja", "agen_baja"},
                    {_idDato,anfitrion,almacen, organization,sucursal,
                            servidor,servidorsucu,dbf,usuario,contrasena,
                            db, rutaBaja, agenBaja},
                    {"int","bit","varchar","varchar","varchar",
                            "varchar","varchar","varchar","varchar","varchar",
                            "varchar","varchar","varchar"}
                    };

                    _funciones.guardar_datos(_datosSQL, "tbl_sucursal", "nuevo", 0, dgvTblEmpresa);
                    insertado++;
                }

                if (insertado == _dtbl.Rows.Count)
                {
                    dgvTblEmpresa.Sort(dgvTblEmpresa.Columns["anfitrion"], ListSortDirection.Descending);
                    MessageBox.Show("Se ha guardado las sucursales participantes", "¡Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //FrmIndex.opcPartSuc.Enabled = true;
                    //FrmIndex.opcImporTabla.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Ocurrio algo " + insertado, "¡Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {
                MessageBox.Show("Debes seleccionar al anfitrion", "¡Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            
            //    foreach (DataRow filaTbl in _dtbl.Rows)
            //{

            //    var _idDato = dgvTblEmpresa.CurrentRow.Cells["id_catsucursal"].Value.ToString();
            //    String orden = dgvTblEmpresa.CurrentRow.Cells["orden"].Value.ToString();
            //    String almacen = dgvTblEmpresa.CurrentRow.Cells["almacen"].Value.ToString();
            //    String sucursal = dgvTblEmpresa.CurrentRow.Cells["sucursal"].Value.ToString();
            //    String servidor = dgvTblEmpresa.CurrentRow.Cells["servidor"].Value.ToString();
            //    String serv_fact = dgvTblEmpresa.CurrentRow.Cells["serv_fact"].Value.ToString();
            //    String copy_files = dgvTblEmpresa.CurrentRow.Cells["copy_files"].Value.ToString();
            //    String path_corp = dgvTblEmpresa.CurrentRow.Cells["path_corp"].Value.ToString();

            //    String[,] _datosSQL= 
            //        {
            //        {"id_catsucursal", "orden","almacen","sucursal", "servidor","serv_fact","copy_files","path_corp"},
            //        {_idDato,orden,almacen,sucursal,servidor,serv_fact,copy_files,path_corp},
            //        {"int","int","varchar","varchar","varchar","varchar","varchar","varchar"}
            //        };
                
            //    _funciones.guardar_datos(_datosSQL, "tbl_sucursal", "nuevo", 0, dgvTblEmpresa);
            //}
        }

        private void dgvTblEmpresa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show("" + e.RowIndex);
            if (dgvTblEmpresa.Rows.Count > 0)
            {
                //int count = 0;
                if (dgvTblEmpresa.Columns[e.ColumnIndex].Name == "anfitrion")
                {

                    //DataGridViewRow row = dgvTblEmpresa.Rows[e.RowIndex];

                    for (int i = 0; i < _dtbl.Rows.Count; i++)
                    {
                        if (!(i== e.RowIndex))
                        {
                            _dtbl.Rows[i]["anfitrion"] = false;
                        }
                        

                        //count++;
                    }

                    //DataGridViewCheckBoxCell cellSelecion = row.Cells["anfitrion"] as DataGridViewCheckBoxCell;
                    //cellSelecion.Value = true;
                    //btnRegre.Enabled = true;
                    //_dtbl.Rows[e.RowIndex]["anfitrion"] = true;
                }
                //_dtbl.Rows[e.RowIndex]["anfitrion"] = true;
                //if (count == _dtbl.Rows.Count)
                //{
                //    dgvTblEmpresa.CurrentRow.Cells["anfitrion"].Value = true;
                //}

            }
        }

        private void dgvCatEmpresa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTblEmpresa.Rows.Count > 0)
            {
                btnPasar.Enabled = true;
            }
        }

        private void dgvCatEmpresa_MouseMove(object sender, MouseEventArgs e)
        {
            dgvCatEmpresa.Focus();
        }

        private void dgvTblEmpresa_MouseMove(object sender, MouseEventArgs e)
        {
            dgvTblEmpresa.Focus();
        }
    }
}
