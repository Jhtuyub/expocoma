using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EXPOCOMA.Stand
{
    public partial class FrmAgregarProveArti : Form
    {
        public String _CadenaConexion;
        public String _NumAlmacen;
        public String _CProve;
        public String _NomProve;
        public DataTable _dtAddArticulo;
        public DataTable _dtArticulos;

        funciones _funcion = new funciones();

        

        String Sesion = "ALEX  CARDENAS";
        String SesionLetra = "H";

        public FrmAgregarProveArti()
        {
            InitializeComponent();
        }

        private void FrmAgregarProveArti_Load(object sender, EventArgs e)
        {
            //_funcion._SQLCadenaConexion = _CadenaConexion;
            //_dtProveedor = _funcion.llenar_dt("dbf_proveedo", "id, ID_SUCURSALALM, C_PROVE, DESCRI, RESP_COMA,  C_PROVE2", "WHERE ID_SUCURSALALM = " + _NumAlmacen + " AND RESP_COMA = '"+ SesionLetra+"'", "ORDER BY C_PROVE");

            dgvProveedor.DataSource = _dtAddArticulo;

            //_dtProveedor.Columns.Add("+", typeof(Boolean));
            //for (int i = 0; i < _dtProveedor.Rows.Count; i++)
            //{
            //    _dtProveedor.Rows[i]["+"] = false;
            //}

            dgvProveedor.Columns["id"].Visible = false;
            dgvProveedor.Columns["ID_SUCURSALALM"].Visible = false;
            //dgvProveedor.Columns["C_PROVE2"].Visible = false;
            dgvProveedor.Columns["PARTICIPA"].DisplayIndex = 0;
            dgvProveedor.Columns["PARTICIPA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvProveedor.Columns["PARTICIPA"].HeaderText = "+";
            dgvProveedor.Columns["C_PROVE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvProveedor.Columns["RESP_COMA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvProveedor.Columns["C_PROVE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvProveedor.DefaultCellStyle.SelectionBackColor = Properties.Settings.Default.filaSeleccion;
            dgvProveedor.AlternatingRowsDefaultCellStyle.BackColor = Properties.Settings.Default.filaAltern;

            txtClProve.Text = _CProve;
            txtNomProve.Text = _NomProve;

            DataRow[] _drArticuloProve = _dtArticulos.Select("ID_SUCURSALALM = "+ _NumAlmacen + " AND C_PROVE = '" + _CProve + "'");
            foreach (DataRow fila in _drArticuloProve)
            {
                //fila[8] = C_PROVE
                //fila[9] = C_PROVE2
                //MessageBox.Show(fila[8].ToString()+"  "+fila[9].ToString());
                for (int i = 0; i < _dtAddArticulo.Rows.Count; i++)
                {
                    if ((_dtAddArticulo.Rows[i]["ID_SUCURSALALM"].ToString() == _NumAlmacen) && (_dtAddArticulo.Rows[i]["C_PROVE"].ToString() == fila[9].ToString()))
                    {
                        _dtAddArticulo.Rows[i]["PARTICIPA"] = true;
                    }
                }
            }

        }

        private void FrmAgregarProveArti_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmProveArti._frmProveArti.Enabled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            String CProve;
            Boolean participa;
            //DataRow[] _drProveedor = _dtAddArticulo.Select("participa = true AND RESP_COMA = '" + SesionLetra + "'");
            DataRow[] _drProveedor = _dtAddArticulo.Select("ID_SUCURSALALM = '"+_NumAlmacen +"' AND RESP_COMA = '" + SesionLetra + "'");
            DataTable _dtTmpProve = _dtAddArticulo.Clone();
            foreach (DataRow fila in _drProveedor)
            {
                _dtTmpProve.ImportRow(fila);
            }

            for (int i = 0; i < _dtTmpProve.Rows.Count; i++)
            {
                CProve = _dtTmpProve.Rows[i]["C_PROVE"].ToString();
                participa =Convert.ToBoolean(_dtTmpProve.Rows[i]["PARTICIPA"]);
                //MessageBox.Show(""+participa);
                //if (participa)
                //{
                    for (int ii = 0; ii < _dtArticulos.Rows.Count; ii++)
                    {

                        if ((_dtArticulos.Rows[ii]["C_PROVE"].ToString() == CProve) && (_dtArticulos.Rows[ii]["ID_SUCURSALALM"].ToString() == _NumAlmacen))
                        {
                        ////if (_dtArticulos.Rows[i]["C_PROVE"].ToString()==_CProve )
                        ////{
                            if (participa)
                            {
                                if (!(_dtArticulos.Rows[ii]["STATUS"].ToString() == "*") && (!(_dtArticulos.Rows[ii]["STATUS"].ToString() == "INACTIVO")))
                                {
                                    //MessageBox.Show(CProve + "       " + _dtArticulos.Rows[i]["C_PROVE"].ToString());
                                    //MessageBox.Show(_dtArticulos.Rows[i]["c_arti"].ToString());
                                    _dtArticulos.Rows[ii]["C_PROVE"] = txtClProve.Text;
                                }
                            }
                            else
                            {
                                _dtArticulos.Rows[ii]["C_PROVE"] = _dtArticulos.Rows[ii]["C_PROVE2"].ToString();
                            }
                        ////if (!(_dtArticulos.Rows[i]["C_PROVE"].ToString() == _dtArticulos.Rows[i]["C_PROVE2"].ToString()))
                        ////{

                        ////}

                        ////}

                        

                    }
                    }
                //}
                //else
                //{


                //    for (int ii = 0; ii < _dtArticulos.Rows.Count; ii++)
                //    {

                //        if ((_dtArticulos.Rows[ii]["C_PROVE"].ToString() == CProve) && (_dtArticulos.Rows[ii]["ID_SUCURSALALM"].ToString() == _NumAlmacen))
                //        {
                //            _dtArticulos.Rows[ii]["C_PROVE"] = _dtArticulos.Rows[ii]["C_PROVE2"].ToString();
                //        }
                //    }


                    
                //}
                

                //_dtGuardarProveedor.ImportRow(fila);
            }

            Close();
            //for (int i = 0; i < _drProveedor.Count(); i++)
            //{
            //    CProve = _drProveedor[2].ToString();
            //    MessageBox.Show(CProve);
            //}
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

       
    }
}
