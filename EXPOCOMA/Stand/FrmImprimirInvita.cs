﻿using CrystalDecisions.CrystalReports.Engine;
using EXPOCOMA.reportes;
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
    public partial class FrmImprimirInvita : Form
    {
        public String _CadenaConexion;
        funciones _funcion = new funciones();

        DataTable _dtSucursales;
        DataTable _dtClientes;

        DSReporte DSReport;
        IEnumerable<DataRow> sql_dtProve;


        public FrmImprimirInvita()
        {
            InitializeComponent();
        }

        private void FrmImprimirInvita_Load(object sender, EventArgs e)
        {
            _funcion.icono(this);
            _funcion._SQLCadenaConexion = _CadenaConexion;
            _dtSucursales = _funcion.llenar_dt("tbl_sucursal", "ALMACEN, SUCURSAL" ,"", "ORDER BY anfitrion DESC");
            

            cBoxSucursal.DataSource = _dtSucursales;
            cBoxSucursal.ValueMember = "ALMACEN";//"valor";
            cBoxSucursal.DisplayMember = "SUCURSAL"; //"opcion";

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            String[] clvCliente;
            String clvNoCliente ="";
            String claves = txtClvClientes.Text.Trim();
            DSReport = new DSReporte();
            int fil_dtClien;


            _dtClientes = _funcion.llenar_dt("dbf_cliente", "ID_SUCURSALALM, C_CLIENTE", "WHERE ID_SUCURSALALM = "+cBoxSucursal.SelectedValue.ToString());

            _dtClientes.Columns.Add("C_CLIEXPO", typeof(String));
            for (int i = 0; i < _dtClientes.Rows.Count; i++)
            {
                //_funcion.Cargando(this, barraProgreso, 0, iAlm, dtDBF.Rows.Count, lblMensaje, "Asignando el almacen: " + tbldtTablas.ToString());
                _dtClientes.Rows[i]["C_CLIEXPO"] = _dtClientes.Rows[i]["ID_SUCURSALALM"].ToString().Substring(2) + _dtClientes.Rows[i]["C_CLIENTE"].ToString();
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

                 sql_dtProve =
                    from dtClien in _dtClientes.AsEnumerable()
                    where dtClien.Field<String>("C_CLIENTE") == clvCliente[i].ToString()
                    select dtClien;

                if (sql_dtProve.Count() > 0)
                {
                    foreach (DataRow filClien in sql_dtProve)
                    {

                        //fil_dtClien = _dtClientes.Rows.Count;
                        //fil_dtClien = filClien.Rows.Count;
                        //for (int ii = 0; ii <= fil_dtClien-2; ii++)
                        //{
                            DSReport.Tables["dtEtiquetas"].Rows.Add(
                                new Object[]
                                {
                                    filClien["C_CLIEXPO"].ToString(),
                                }
                                );
                        //}
                        //    for (int i = 0; i <= filas - 2; i++)
                        //    {
                        //        dsReport.Tables[0].Rows.Add
                        //            (
                        //            new object[]
                        //            {
                        //   dtDatos.Rows[i]["C_CLIENTE"].ToString(),
                        //   dtDatos.Rows[i]["NOM_CLIEN"].ToString(),
                        //   dtDatos.Rows[i]["NOM_TIENDA"].ToString(),
                        //   dtDatos.Rows[i]["POBLACION"].ToString(),
                        //   dtDatos.Rows[i]["TELEFONO"].ToString(),
                        //   dtDatos.Rows[i]["C_AGENTE"].ToString(),

                        //            }
                        //            );
                    }
                
                }else
                {
                    clvNoCliente += clvCliente[i].ToString() + "\r\n";
                }


                



            }

            //ReportDocument cRep = new ReportDocument();
            //cRep.Load(Application.StartupPath + "\\reportes\\CREtiquetas.rpt");
            //cRep.SetDataSource(DSReport.Tables["dtEtiquetas"]);
            //crvClientes.ReportSource = cRep;

            txtNoImpre.Text = clvNoCliente;

            FrmVistaPrevia frmVistaPre = new FrmVistaPrevia();
            frmVistaPre.MdiParent = this.MdiParent;
            frmVistaPre._crvReporte = "CREtiquetas";
            frmVistaPre._dtTabla = DSReport.Tables["dtEtiquetas"];
            frmVistaPre.Show();


            //FrmAgregarProveArti frmAgregarArti = new FrmAgregarProveArti();
            //frmAgregarArti.MdiParent = this.MdiParent;
            //frmAgregarArti._CadenaConexion = _CadenaConexion;
            //frmAgregarArti._dtProve = _dtProve;
            ////frmAgregarArti._dtProveedor = _dtProveedor;
            //frmAgregarArti._dtArticulos = _dtArticulo;
            //frmAgregarArti._CProve = dgvProveedor.CurrentRow.Cells["C_PROVE"].Value.ToString();
            //frmAgregarArti._NomProve = dgvProveedor.CurrentRow.Cells["DESCRI"].Value.ToString();
            //frmAgregarArti._NumAlmacen = cBoxSucursal.SelectedValue.ToString();
            ////_frmSucusal.Owner = this;
            //frmAgregarArti.Show();

        }
    }
}
