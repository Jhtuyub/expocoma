//using EXPOCOMA.modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EXPOCOMA
{
    class funciones : modelo.dbconexion
    {

        //dblocal __dblocal = new dblocal();
        public String _TIPObasedatos = Properties.Settings.Default.sqlorigen;
        public String _SQLCadenaConexion;

        public void llenarCombobox(ComboBox _cbComBox, String[,] _cbDatos)
        {
            DataTable dt = new DataTable("cbcampo");
            dt.Columns.Add("valor");
            dt.Columns.Add("opcion");
            DataRow dr;
            //
            var totalArray = _cbDatos.GetLength(1);
            for (int i = 0; i < totalArray; i++)
            {
                dr = dt.NewRow();
                dr["valor"] = _cbDatos[0, i];
                dr["opcion"] = _cbDatos[1, i];
                dt.Rows.Add(dr);
            }

            
            _cbComBox.DataSource = dt;
            _cbComBox.ValueMember = "valor";
            _cbComBox.DisplayMember = "opcion";
            _cbComBox.SelectedItem = _cbDatos[1, 0];
        }
        /// <summary>
        /// LLENA UN DATATABLE Y LO REGRESA PARA SER LLENADOS LOS CONTROLES.
        /// EJEMPLO: _VARIABLE = LLENAR_FORM();
        /// </summary>
        /// <param name="___TABLA">NOMBRE DE LA TABLA</param>
        /// <param name="___IDDATO">ID DEL DATO A BUSCAR EN CASO DE MODIFICAR</param>
        /// <param name="___CAMPO">(OPCIONAL) NOMBRE DEL CAMPO DONDE SE VA ENCONTRAR EL DATO</param>
        /// <param name="___DATO">(OPCIONAL) DATO QUE VA A BUSCAR EN EL CAMPO QUE ASIGNASTE EN LA ANTERIOR</param>
        /// <returns>DEVUELVE UN DATATABLE LLENO DE INFORMACION SOLICITADA</returns>
        public DataTable llenar_form(String ___TABLA, String ___ORDEN = "id DESC", String ___CAMPOS = "*", int ___IDDATO = 0, String ___CAMPO = null, String ___DATO=null)
        {
            
             String _sql;
            if ((String.IsNullOrEmpty(___CAMPO) && (String.IsNullOrEmpty(___DATO))))
            {
                if (___IDDATO > 0)
                {
                    _sql = "SELECT "+ ___CAMPOS + " FROM " + ___TABLA + " WHERE id = " + ___IDDATO + " ORDER BY "+___ORDEN;
                }
                else
                {
                    _sql = "SELECT " + ___CAMPOS + " FROM " + ___TABLA + " ORDER BY " + ___ORDEN;
                }
            }
            else
            {
                _sql = "SELECT " + ___CAMPOS + " FROM " + ___TABLA + " WHERE "+ ___CAMPO+" LIKE '%" + ___DATO + "%' ORDER BY " + ___ORDEN;
            }

            if (_TIPObasedatos == "sqlserver")
            {
                using (SqlConnection _con = new SqlConnection(_SQLCadenaConexion))
                {
                    
                    SqlDataAdapter myAdaptador = new SqlDataAdapter(_sql, _con);
                    DataTable dt = new DataTable();
                    myAdaptador.Fill(dt);
                    //txtNombre.Text = dt.Rows[0]["nombre"].ToString();
                    //txtDB.Text = dt.Rows[0]["db"].ToString();
                    //_validarDB = dt.Rows[0]["db"].ToString();
                    return dt;
                }
            }
            //else if(_TIPObasedatos == "dbcompaq")
            //{
            //    using (SqlCeConnection _con = CeConexion())
            //    {

            //        SqlCeDataAdapter myAdaptador = new SqlCeDataAdapter(_sql, _con);
            //        DataTable dt = new DataTable();
            //        myAdaptador.Fill(dt);
            //        //txtNombre.Text = dt.Rows[0]["nombre"].ToString();
            //        //txtDB.Text = dt.Rows[0]["db"].ToString();
            //        //_validarDB = dt.Rows[0]["db"].ToString();
            //        return dt;
            //    }
            //}
            else
            {
                DataTable dt = new DataTable();
                return dt;
            }
            
            
            
            
        }
        /// <summary>
        /// OBTIENE LOS DATOS DE UNA TABLA EN SQL
        /// </summary>
        /// <param name="___TABLA">NOMBRE DE LA TABLA</param>
        /// <param name="___CAMPOS">NOMBRE(S) DEL(LOS) CAMPO(S)</param>
        /// <param name="___CONDICION">WHERE CAMPO = 'DATA'</param>
        /// <param name="___ORDEN">ORDER BY id DESC</param>
        /// <returns></returns>
        public DataTable llenar_dt(String ___TABLA, String ___CAMPOS = "*", String ___CONDICION="", String ___ORDEN = "")
        {

            String _sql = "SELECT "+ ___CAMPOS+" FROM "+___TABLA +" "+ ___CONDICION+" "+ ___ORDEN;
          

            if (_TIPObasedatos == "sqlserver")
            {
                using (SqlConnection _con = new SqlConnection(_SQLCadenaConexion))
                {

                    SqlDataAdapter myAdaptador = new SqlDataAdapter(_sql, _con);
                    DataTable dt = new DataTable();
                    myAdaptador.Fill(dt);
                    //txtNombre.Text = dt.Rows[0]["nombre"].ToString();
                    //txtDB.Text = dt.Rows[0]["db"].ToString();
                    //_validarDB = dt.Rows[0]["db"].ToString();
                    return dt;
                }
            }
            else
            {
                DataTable dt = new DataTable();
                return dt;
            }




        }

        public DataTable EstructuraTabla(String _Tabla)
        {
            DataTable _dt=new DataTable(); ;
            String _sql = "SELECT TOP 0 * FROM " + _Tabla;
            if (_TIPObasedatos == "sqlserver")
            {
                using (SqlConnection _con = new SqlConnection(_SQLCadenaConexion))
                {

                    SqlDataAdapter myAdaptador = new SqlDataAdapter(_sql, _con);
                    //_dt = /*new DataTable();*/
                    myAdaptador.Fill(_dt);
                    //txtNombre.Text = dt.Rows[0]["nombre"].ToString();
                    //txtDB.Text = dt.Rows[0]["db"].ToString();
                    //_validarDB = dt.Rows[0]["db"].ToString();
                    return _dt;
                }
            }

            //            SELECT campo1, campo2 FROM products
            //LIMIT 10
            return _dt;
        }

        /// <summary>
        /// CARGA DE DATOS A UN DATAGRIDVIEW
        /// </summary>
        /// <param name="___DGVDATOS">NAME DEL DATAGRIDVIEW DONDE SE VAN A MOSTRAR LOS DATOS</param>
        /// <param name="___TABLA">NOMBRE DE LA TABLA DONDE SE VAN A TOMAR LOS DATOS</param>
        /// <param name="___CAMPO">(OPCIONAL) NOMBRE DEL CAMPO DONDE SE REALIZARA EL FILTRO</param>
        /// <param name="___VALOR">(OPCIONAL) VALOR QUE VA A BUSCAR EN EL CAMPO QUE ASIGNASTE ANTRIORMENTE</param>
        /// <param name="___CAMPOORDEN">(OPCIONAL) CAMPO Y ORDEN QUE TENDRA LOS DATOS [id DESC]</param>
        public void cargar_datos(DataGridView ___DGVDATOS,String ___TABLA,String ___CAMPO = null, String ___VALOR=null, String ___CAMPOORDEN = "id DESC")
        {
            String _sql;

            if ((String.IsNullOrEmpty(___CAMPO) && (String.IsNullOrEmpty(___CAMPO))))
            {
                _sql = "SELECT * FROM " + ___TABLA + " ORDER BY " + ___CAMPOORDEN;
            }
            else
            {
                _sql = "SELECT * FROM " + ___TABLA + " WHERE "+ ___CAMPO + " LIKE '%"+ ___VALOR + "%' "+ " ORDER BY " + ___CAMPOORDEN;
            }

            if (_TIPObasedatos == "sqlserver")
            {
                using (SqlConnection _con = new SqlConnection(_SQLCadenaConexion))
                {

                    SqlDataAdapter myAdaptador = new SqlDataAdapter(_sql, _con);
                    DataTable dt = new DataTable();
                    myAdaptador.Fill(dt);

                    ___DGVDATOS.DataSource = dt;
                    //___DGVDATOS.Columns["id"].Visible = false;


                    //return dt;

                }
            }
            //else if (_TIPObasedatos == "dbcompaq")
            //{
            //    using (SqlCeConnection _con = CeConexion())
            //    {

            //        SqlCeDataAdapter myAdaptador = new SqlCeDataAdapter(_sql, _con);
            //        DataTable dt = new DataTable();
            //        myAdaptador.Fill(dt);

            //        ___DGVDATOS.DataSource = dt;
            //        //___DGVDATOS.Columns["id"].Visible = false;


            //        //return dt;

            //    }
            //}
            else
            {
                DataTable dt = new DataTable();
                ___DGVDATOS.DataSource= dt;
            }
                
        }
        /// <summary>
        /// CARGA DE DATOS A UN DATAGRIDVIEW CON SQL
        /// </summary>
        /// <param name="___DGVDATOS">NAME DEL DATAGRIDVIEW DONDE SE VAN A MOSTRAR LOS DATOS</param>
        /// <param name="___SQL">SQL PARA CARGAR DATOS: select * from tabla where campo = 1 order by campo desc</param>
        /// <param name="___TIPODB">CONEXION A BASE DE DATOS OPCIONES:[dbcompaq][sqlserver] </param>
        public void cargar_datos(DataGridView ___DGVDATOS, String ___SQL, String ___TIPODB)
        {

            if (___TIPODB == "sqlserver")
            {
                using (SqlConnection _con = new SqlConnection(_SQLCadenaConexion))
                {

                    SqlDataAdapter myAdaptador = new SqlDataAdapter(___SQL, _con);
                    DataTable dt = new DataTable();
                    myAdaptador.Fill(dt);

                    ___DGVDATOS.DataSource = dt;
                }
            }
            else if (___TIPODB == "dbcompaq")
            {
                using (SqlCeConnection _con = CeConexion())
                {

                    SqlCeDataAdapter myAdaptador = new SqlCeDataAdapter(___SQL, _con);
                    DataTable dt = new DataTable();
                    myAdaptador.Fill(dt);

                    ___DGVDATOS.DataSource = dt;
                    //___DGVDATOS.Columns["id"].Visible = false;
                    //return dt;

                }
            }
            else
            {
                DataTable dt = new DataTable();
                ___DGVDATOS.DataSource = dt;
            }
        }


        /// <summary>
        /// ASIGNA EL NOMBRE TITULO EN EL FORM DEPENDIENDO SI ES PARA AGREGAR O MODIFICAR
        /// </summary>
        /// <param name="___FORMULARIO">NAME DEL FORMULARIO ACTUAL, CASI SIEMPRE ES THIS</param>
        /// <param name="___ACCION">VALOR QUE VALIDA SI ES NUEVO O MODIFICAR</param>
        /// <param name="___NUEVO">STRING QUE MOSTRAR CUANDO EL FORMULARIO ES NUEVO</param>
        /// <param name="___MODIFICAR">STRING QUE MOSTRAR CUANDO EL FORMULARIO ES MODIFICAR</param>
        public void nombre_form(Form ___FORMULARIO, string ___ACCION, string ___NUEVO, string ___MODIFICAR)
        {
            if (___ACCION == "nuevo")
            {
                ___FORMULARIO.Text = ___NUEVO;
            }
            else if (___ACCION == "modificar")
            {
                ___FORMULARIO.Text = ___MODIFICAR;
            }
            else
            {

            }
        }

        /// <summary>
        /// LIMPIA CONTROLES EN EL FORM ACTUAL Y EN ALGUNOS CASOS LOS LABEL DE MENSAJE
        /// </summary>
        /// <param name="___FORMULARIO">NAME DEL FORM ACTUAL</param>
        public void reiniciar_campos(Form ___FORMULARIO)
        {
            var _focus = 0;
            foreach (Control c in ___FORMULARIO.Controls)
            {
                if (c is TextBox)
                {
                    if (_focus == 0)
                    {
                        c.Focus();
                        _focus++;
                    }
                    c.BackColor = System.Drawing.Color.White;
                    c.Text = "";
                }
                else if (c.Tag =="mensaje")
                {
                    c.Text = "";
                }
            }
        }

        /// <summary>
        /// PREGUNTA SI DESEA CERRAR EL FORM SI HAY CONTROLES NO VACIOS.
        /// </summary>
        /// <param name="___FORMULARIO">NAME DEL FORM ACTUAL</param>
        /// <returns>DEVUELVE UN TRUE: PARA CANCELAR EL CERRADO Y UN FALSE: PARA CERRAR</returns>
        public bool antes_cerrar(Form ___FORMULARIO)
        {
            var _validar = true;
            var _ClarCerrar = true; //CON ESTO CANCELA EL CIERRE (NO CIERRA EL FORMULARIO) 
            foreach (Control c in ___FORMULARIO.Controls)
            {
                if (c is TextBox)
                {
                    if (_validar == true)
                    {
                        if (!(string.IsNullOrWhiteSpace(c.Text)))
                        {
                            var _result = MessageBox.Show("¿Desea salir sin guardar?", "¡Aviso!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                            if (_result == DialogResult.Yes)
                            {
                                _validar = false;
                                _ClarCerrar = false;
                            }
                            else
                            {
                                _validar = false;
                                _ClarCerrar = true;
                            }
                        }
                        else
                        {
                            _ClarCerrar = false;
                        }
                    }
                    
                }
            }

            if (_ClarCerrar == false)
            {
                reiniciar_campos(___FORMULARIO);
            }

            return _ClarCerrar;
        }

        /// <summary>
        /// LIMPIA LOS CONTROLES
        /// </summary>
        /// <param name="___CONTROL">CONTROL QUE TOMARA EL FOCUS AL LIMPIAR</param>
        /// <param name="___FORMULARIO">NAME DEL FORM ACTUAL</param>
        public void limpiar_campos(Control ___CONTROL, Form ___FORMULARIO)
        {

            var _validar = true;
            var _limpiar = false; //CON ESTO CANCELA EL CIERRE (NO CIERRA EL FORMULARIO) 
            foreach (Control c in ___FORMULARIO.Controls)
            {
                if (c is TextBox)
                {
                    if (_validar == true)
                    {
                        if (!(string.IsNullOrWhiteSpace(c.Text)))
                        {
                            var _result = MessageBox.Show("¿Desea Limpiar?", "¡Aviso!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                            if (_result == DialogResult.Yes)
                            {
                                _validar = false;
                                 _limpiar = true;
                               // c.Text = "";
                            }
                            else
                            {
                                _validar = false;
                                _limpiar = false;
                            }
                        }
                        else
                        {
                             _limpiar = true;
                        }
                    }

                }
            }

            if (_limpiar)
            {
                foreach (Control c in ___FORMULARIO.Controls)
                    {
                        if (c is TextBox)
                        {
                            c.Text = "";
                        }
                    }
            }

            ___CONTROL.Focus();

            
        }
        
        /// <summary>
        /// COLOCA EN BLANCO EL FONDO DEL CONTROL
        /// </summary>
        /// <param name="___CONTROL">NAME DEL CONTROL</param>
        public void key_campo(Control ___CONTROL)
        {
            ___CONTROL.BackColor = System.Drawing.Color.White;
        }


        /// <summary>
        /// VALIDA SI EXISTE EL DATO EN LA BASE DE DATOS, POR EJEMPLO: AL TECLEAR EN TEXTBOX
        /// </summary>
        /// <param name="___CAMPO">CONTROL DONDE SE VA A VALIDAR</param>
        /// <param name="___VALIMENSAJE">LABEL DONDE DEBE MOSTRAR EL MENSAJE SI EXISTE</param>
        /// <param name="___ACCION">VALOR QUE VALIDA SI ES NUEVO O MODIFICAR</param>
        /// <param name="___MODIDATO">VALOR QUE IGNORARA PARA VALIDAR SI YA EXISTE EN LA DB CUANDO ES MODIFICAR</param>
        /// <param name="___SQL">DEBE CONTENER EL NOMBRE DE LA TABLA Y CAMPO A VALIDAR String[] _sql = { "Tabla", "Campo", "Dato", "tipo de dato" };</param>
        public void key_campo(Control ___CAMPO, Control ___VALIMENSAJE, String ___ACCION, String ___MODIDATO, String[] ___SQL)
        {
            if (!string.IsNullOrWhiteSpace(___CAMPO.Text))
            {
                ___CAMPO.BackColor = System.Drawing.Color.White;
                ___VALIMENSAJE.Text = "";

                /*
                 * ___SQL[0] = nombre de la tabla
                 * ___SQL[1] = nombre del campo
                 * ___SQL[2] = valor a buscar
                 * ___SQL[3] = tipo de dato
                 */

                //cmd.CommandText = "SELECT COUNT(*) FROM Alumnos";
                //resultado = cmd.ExecuteScalar();2
                //MessageBox.Show(___SQL[1] + " - "+ ___SQL[0]);

                var _sql = "";
                var Dato = "";

                if (___ACCION == "nuevo")
                {
                    if (___SQL[3] == "int")
                    {
                        Dato = ___SQL[2];
                    }
                    else
                    {
                        Dato = "'" + ___SQL[2] + "'";
                    }
                    //_sql = "SELECT " + ___SQL[1] + " FROM " + ___SQL[0] + " WHERE " + ___SQL[1] + " = '" + ___SQL[2] + "'";
                    _sql = "SELECT " + ___SQL[1] + " FROM " + ___SQL[0] + " WHERE " + ___SQL[1] + " = " + Dato;
                }
                else if (___ACCION == "modificar")
                {
                    if (___SQL[3] == "int")
                    {
                        Dato = ___SQL[2];
                    }
                    else
                    {
                        Dato = "'" + ___SQL[2] + "'";
                    }
                    //_sql = "SELECT " + ___SQL[1] + " FROM " + ___SQL[0] + " WHERE " + ___SQL[1] + " = '" + ___SQL[2] + "' AND NOT " + ___SQL[1] + " = '" + ___MODIDATO + "'";
                    _sql = "SELECT " + ___SQL[1] + " FROM " + ___SQL[0] + " WHERE " + ___SQL[1] + " = " + Dato + " AND NOT " + ___SQL[1] + " = '" + ___MODIDATO + "'";
                }

                var resultado = 0;
                if (_TIPObasedatos == "sqlserver")
                {
                    using (SqlConnection _con = new SqlConnection(_SQLCadenaConexion))
                    {
                        //SqlCeDataAdapter myAdaptador = new SqlCeDataAdapter(_sql, _con);
                        //DataSet DS1 = new DataSet();
                        //myAdaptador.Fill(DS1, _sql);
                        //resultado= DS1.Tables[0].Rows.Count;

                        SqlDataAdapter myAdaptador = new SqlDataAdapter(_sql, _con);
                        DataTable dt = new DataTable();
                        myAdaptador.Fill(dt);
                        resultado = dt.Rows.Count;
                    }
                }
                //else if (_TIPObasedatos == "dbcompaq")
                //{
                //    using (SqlCeConnection _con = CeConexion())
                //    {
                //        //SqlCeDataAdapter myAdaptador = new SqlCeDataAdapter(_sql, _con);
                //        //DataSet DS1 = new DataSet();
                //        //myAdaptador.Fill(DS1, _sql);
                //        //resultado= DS1.Tables[0].Rows.Count;

                //        SqlCeDataAdapter myAdaptador = new SqlCeDataAdapter(_sql, _con);
                //        DataTable dt = new DataTable();
                //        myAdaptador.Fill(dt);
                //        resultado = dt.Rows.Count;
                //    }
                //    //MessageBox.Show("Number of row(s) - " + resultado);

                //    if (resultado > 0)
                //    {
                //        ___VALIMENSAJE.Text = "Ya registrado";
                //        ___VALIMENSAJE.ForeColor = System.Drawing.Color.Red;
                //    }
                //}
                else
                {
                    ___VALIMENSAJE.Text = "No definido el origen de datos";
                    ___VALIMENSAJE.ForeColor = System.Drawing.Color.Blue;
                }

                if (resultado > 0)
                {
                    ___VALIMENSAJE.Text = "Ya registrado";
                    ___VALIMENSAJE.ForeColor = System.Drawing.Color.Red;
                }
            }
            
                    
                
            
        }


        /// <summary>
        /// VALIDA LOS CONTROLES ANTES DE IR A LA FUNCION PARA GUARDAR
        /// </summary>
        /// <param name="___FORMULARIO">FOR ACTUAL</param>
        /// <param name="___DATOS">LE SIRVE A LA FUNCION QUE GUARDAR LOS DATOS string[,] _datos = {{"nombre", "db"}, {txtNombre.Text, txtDB.Text },{"varchar", "varchar"} }; {{NOMBRE DE LOS CAMPOS}{CONTROLES}{TIPO DE DATOS}}</param>
        /// <param name="___TABLA">NOMBRE DE LA TABLA A GUARDAR</param>
        /// <param name="___TIMMENSAJE">CONTROL QUE TIME PARA TOMAR EL EFECTO DE PARPADEO DE LOS MENSAJES EN LOS LABEL</param>
        /// <param name="___ACCION">VALOR QUE VALIDA SI ES NUEVO O MODIFICAR</param>
        /// <param name="_IDDATO">EN EL CASO DE MODIFICAR COLOCAR LA ID DEL DATO</param>
        public void validar_campo(Form ___FORMULARIO, String[,] ___DATOS, String ___TABLA, String ___ACCION, int _IDDATO)
        {
            var _countMensaje = 0;
            foreach (Control c in ___FORMULARIO.Controls)
            {
                if ((c.Tag == "mensaje") && !(string.IsNullOrWhiteSpace(c.Text)))
                {
                    _countMensaje++;
                }
            }

            if (!(_countMensaje > 0))
            {
                // bool incorrecto = false;
                var _cont = 0;
                var _totalControles = 0;
                var _controlFocus = 0;
                foreach (Control c in ___FORMULARIO.Controls)
                {
                    if (c is TextBox)
                    {
                        _totalControles++;
                    }
                }

                foreach (Control c in ___FORMULARIO.Controls)
                {
                    if (c is TextBox)
                    {
                        if (string.IsNullOrWhiteSpace(c.Text))
                        {
                            if (_controlFocus == 0)
                            {
                                //c.Focus();
                            }
                            c.BackColor = System.Drawing.Color.Red;
                            _controlFocus++;
                        }
                        else
                        {
                            c.BackColor = System.Drawing.Color.White;
                            _cont++;
                        }
                    }
                }

                if (_cont == _totalControles)
                {
                    guardar_datos(___FORMULARIO, ___DATOS, ___TABLA, ___ACCION, _IDDATO);
                }
            }
            else
            {

                //ParameterizedThreadStart parameterizedThreadStart = new ParameterizedThreadStart(LabelMensajes); // va el nombre del metodo que recibe un object
                //Thread hilo = new Thread(parameterizedThreadStart);
                //hilo.Start(___FORMULARIO);

                Thread lblMensajeThread = new Thread(delegate() {
                    foreach (Control c in ___FORMULARIO.Controls)
                    {
                        if ((c.Tag == "mensaje") && !(string.IsNullOrWhiteSpace(c.Text)))
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                if ((i % 2)==0)
                                {
                                    c.ForeColor = System.Drawing.Color.Red;
                                }
                                else
                                {
                                    c.ForeColor = System.Drawing.Color.DarkRed;
                                }
                                Thread.Sleep(100);
                            }
                        }
                    }
                    
                });
                lblMensajeThread.Start();

                //___TIMMENSAJE.Start();
                //Thread lblMensajeThread = new Thread(nLabelMensajes);
                //lblMensajeThread.Start(___FORMULARIO);
                //MessageBox.Show("Uno o varios campos " + Environment.NewLine + "no estan llenados adecuadamente", "¡Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

      

        /// <summary>
        /// GUARDA O ACTUALIZA LA INFORMACION DE LA BASE DE DATOS
        /// </summary>
        /// <param name="___FORMULARIO">FORM ACTUAL</param>
        /// <param name="___DATOS">DATOS A GUARDAR string[,] _datos = {{"nombre", "db"}, {txtNombre.Text, txtDB.Text },{"varchar", "varchar"} }; {{NOMBRE DE LOS CAMPOS}{CONTROLES}{TIPO DE DATOS}}</param>
        /// <param name="___TABLA">NOMBRE DE LA TABLA A GUARDAR</param>
        /// <param name="___ACCION">VALOR QUE VALIDA SI ES NUEVO O MODIFICAR</param>
        /// <param name="_IDDATO">EN EL CASO DE MODIFICAR COLOCAR LA ID DEL DATO</param>
        public void guardar_datos(Form ___FORMULARIO, String[,] ___DATOS, String ___TABLA,String ___ACCION, int _IDDATO)
        {
            var _totalArray = ___DATOS.GetLength(0);
            var _totalDatos = ___DATOS.GetLength(1);
            string _sql;

            if (___ACCION == "nuevo")
            {
                _sql = "INSERT INTO " + ___TABLA + "(";
                var _countDatos = 1;
                for (int i = 0; i < _totalArray; i++)
                {
                    for (int j = 0; j < _totalDatos; j++)
                    {
                        if (i == 0)
                        {
                            if (_countDatos == _totalDatos)
                            {
                                _sql = _sql + ___DATOS[i, j] + ") VALUES (";
                                _countDatos = 1;
                            }
                            else
                            {
                                _sql = _sql + ___DATOS[i, j] + ", ";
                                _countDatos++;
                            }
                        }
                        else if (i == 1)
                        {
                            if (_countDatos == _totalDatos)
                            {
                                if (___DATOS[2, j] == "varchar")
                                {
                                    _sql = _sql + "'" + ___DATOS[i, j] + "')";
                                }
                                else if (___DATOS[2, j] == "int")
                                {
                                    _sql = _sql + ___DATOS[i, j] + ")";
                                }

                                _countDatos = 1;
                            }
                            else
                            {
                                if (___DATOS[2, j] == "varchar")
                                {
                                    _sql = _sql + "'" + ___DATOS[i, j] + "', ";
                                }
                                else if (___DATOS[2, j] == "int")
                                {
                                    _sql = _sql + ___DATOS[i, j] + ", ";

                                }

                                _countDatos++;
                            }
                        }
                    }
                }
            }
            else if (___ACCION == "modificar")
            {




                 _sql = "UPDATE " + ___TABLA + " SET ";
                var countDatos = 1;
                for (int i = 0; i < _totalArray - 1; i++)
                {
                    for (int j = 0; j < _totalDatos; j++)
                    {
                        if (i == 0)
                        {
                            if (countDatos == _totalDatos)
                            {
                                if (___DATOS[2, j] == "varchar")
                                {
                                    _sql = _sql + ___DATOS[i, j] + "='" + ___DATOS[1, j] + "'";
                                }
                                else if (___DATOS[2, j] == "int")
                                {
                                    _sql = _sql + ___DATOS[i, j] + "=" + ___DATOS[1, j] + "";
                                }
                            }
                            else
                            {
                                if (___DATOS[2, j] == "varchar")
                                {
                                    _sql = _sql + ___DATOS[i, j] + "='" + ___DATOS[1, j] + "', ";
                                }
                                else if (___DATOS[2, j] == "int")
                                {
                                    _sql = _sql + ___DATOS[i, j] + "=" + ___DATOS[1, j] + ", ";
                                }
                            }

                            countDatos++;
                        }
                        

                    }
                    //countDatos = 0;
                }
                _sql = _sql + " WHERE id = "+ _IDDATO;




            }
            else
            {
                _sql = "";
            }

            


            //MessageBox.Show(sql);

            int resultado=0;
            if (_TIPObasedatos == "sqlserver")
            {
                using (SqlConnection _con = new SqlConnection(_SQLCadenaConexion))
                {
                    SqlCommand comando = new SqlCommand (_sql, _con);
                    resultado = comando.ExecuteNonQuery();
                    //            __dblocal.Close();
                }
            }
           //else if (_TIPObasedatos == "dbcompaq")
           // {
           //     using (SqlCeConnection _con = CeConexion())
           //     {
           //         SqlCeCommand comando = new SqlCeCommand(_sql, _con);
           //         resultado = comando.ExecuteNonQuery();
           //         //            __dblocal.Close();
           //     }
           // }
            else
            {
                resultado = 0;
            }
                

            if (resultado == 1)
            {
                MessageBox.Show("Se ha almacenado la información", "¡EXITO!",MessageBoxButtons.OK, MessageBoxIcon.Information);
                reiniciar_campos(___FORMULARIO);
                ___FORMULARIO.Close();
            }
            else
            {
                MessageBox.Show("No logro guardarse");
            }
            //MessageBox.Show("Listo para guardae");
        }

        /// <summary>
        /// GUARDA O ACTUALIZA LA INFORMACION DE LA BASE DE DATOS, Y CARGA LA INFORMACION EN UN DATAGRIDVIEW
        /// </summary>
        /// <param name="___DATOS">DATOS A GUARDAR string[,] _datos = {{"nombre", "db"}, {txtNombre.Text, txtDB.Text },{"varchar", "varchar"} }; {{NOMBRE DE LOS CAMPOS}{CONTROLES}{TIPO DE DATOS}}</param>
        /// <param name="___TABLA">NOMBRE DE LA TABLA A GUARDAR</param>
        /// <param name="___ACCION">VALOR QUE VALIDA SI ES NUEVO O MODIFICAR</param>
        /// <param name="_IDDATO">EN EL CASO DE MODIFICAR COLOCAR LA ID DEL DATO</param>
        public void guardar_datos(String[,] ___DATOS, String ___TABLA, String ___ACCION, int _IDDATO, DataGridView ___DGVDATOS)
        {
            var _totalArray = ___DATOS.GetLength(0);
            var _totalDatos = ___DATOS.GetLength(1);
            string _sql;


            if (___ACCION == "nuevo")
            {
                _sql = "INSERT INTO " + ___TABLA + "(";
                var _countDatos = 1;
                for (int i = 0; i < _totalArray; i++)
                {
                    for (int j = 0; j < _totalDatos; j++)
                    {
                        if (i == 0)
                        {
                            if (_countDatos == _totalDatos)
                            {
                                _sql = _sql + ___DATOS[i, j] + ") VALUES (";
                                _countDatos = 1;
                            }
                            else
                            {
                                _sql = _sql + ___DATOS[i, j] + ", ";
                                _countDatos++;
                            }
                        }
                        else if (i == 1)
                        {
                            if (_countDatos == _totalDatos)
                            {
                                if (___DATOS[2, j] == "varchar")
                                {
                                    _sql = _sql + "'" + ___DATOS[i, j] + "')";
                                }
                                else if (___DATOS[2, j] == "int")
                                {
                                    _sql = _sql + ___DATOS[i, j] + ")";
                                }
                                else if (___DATOS[2, j] == "bit")
                                {
                                    var valor = 0;
                                    if (Convert.ToBoolean(___DATOS[i, j]) == true)
                                    {
                                        valor = 1;
                                    }
                                    else
                                    {
                                        valor = 0;
                                    }
                                    _sql = _sql + valor + ")";
                                    //_sql = _sql + Convert.ToBoolean(___DATOS[i, j]) + ")";
                                }

                                    _countDatos = 1;
                            }
                            else
                            {
                                if (___DATOS[2, j] == "varchar")
                                {
                                    _sql = _sql + "'" + ___DATOS[i, j] + "', ";
                                }
                                else if (___DATOS[2, j] == "int")
                                {
                                    _sql = _sql + ___DATOS[i, j] + ", ";

                                }
                                else if (___DATOS[2, j] == "bit")
                                {
                                    var valor = 0;
                                    if (Convert.ToBoolean(___DATOS[i, j]) == true)
                                    {
                                        valor = 1;
                                    }
                                    else
                                    {
                                        valor = 0;
                                    }
                                    _sql = _sql + valor+", ";

                                }

                                _countDatos++;
                            }
                        }
                    }
                }
            }
            else if (___ACCION == "modificar")
            {




                _sql = "UPDATE " + ___TABLA + " SET ";
                var countDatos = 1;
                for (int i = 0; i < _totalArray - 1; i++)
                {
                    for (int j = 0; j < _totalDatos; j++)
                    {
                        if (i == 0)
                        {
                            if (countDatos == _totalDatos)
                            {
                                if (___DATOS[2, j] == "varchar")
                                {
                                    _sql = _sql + ___DATOS[i, j] + "='" + ___DATOS[1, j] + "'";
                                }
                                else if (___DATOS[2, j] == "int")
                                {
                                    _sql = _sql + ___DATOS[i, j] + "=" + ___DATOS[1, j] + "";
                                }
                                else if (___DATOS[2, j] == "bit")
                                {
                                    var valor = 0;
                                    if (Convert.ToBoolean(___DATOS[i, j]) == true)
                                    {
                                        valor = 1;
                                    }
                                    else
                                    {
                                        valor = 0;
                                    }
                                    _sql = _sql + ___DATOS[i, j] + "=" + valor + "";
                                }

                            }
                            else
                            {
                                if (___DATOS[2, j] == "varchar")
                                {
                                    _sql = _sql + ___DATOS[i, j] + "='" + ___DATOS[1, j] + "', ";
                                }
                                else if (___DATOS[2, j] == "int")
                                {
                                    _sql = _sql + ___DATOS[i, j] + "=" + ___DATOS[1, j] + ", ";
                                }
                                else if (___DATOS[2, j] == "bit")
                                {
                                    var valor = 0;
                                    if (Convert.ToBoolean(___DATOS[i, j]) == true)
                                    {
                                        valor = 1;
                                    }
                                    else
                                    {
                                        valor = 0;
                                    }
                                    _sql = _sql + ___DATOS[i, j] + "=" + valor + ", ";
                                    //_sql = _sql + ___DATOS[i, j] + "=" + ___DATOS[1, j] + ", ";
                                }
}

                            countDatos++;
                        }


                    }
                    //countDatos = 0;
                }
                _sql = _sql + " WHERE id = " + _IDDATO;




            }
            else
            {
                _sql = "";
            }




            //MessageBox.Show(sql);

            int resultado = 0;
            if (_TIPObasedatos == "sqlserver")
            {
                using (SqlConnection _con = new SqlConnection(_SQLCadenaConexion))
                { 
                _con.Open();
                 SqlCommand comando = new SqlCommand(_sql, _con);
                    resultado = comando.ExecuteNonQuery();
                    //            __dblocal.Close();
                }
            }
            //else if (_TIPObasedatos == "dbcompaq")
            //{
            //    using (SqlCeConnection _con = CeConexion())
            //    {
            //        SqlCeCommand comando = new SqlCeCommand(_sql, _con);
            //        resultado = comando.ExecuteNonQuery();
            //        //            __dblocal.Close();
            //    }
            //}
            else
            {
                resultado = 0;
            }


            if (resultado == 1)
            {

                cargar_datos(___DGVDATOS, ___TABLA);
                //MessageBox.Show("Se ha almacenado la información", "¡EXITO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //reiniciar_campos(___FORMULARIO);
                //___FORMULARIO.Close();
            }
            else
            {
                MessageBox.Show("No logro guardarse");
            }
            //MessageBox.Show("Listo para guardae");
        }

        /// <summary>
        /// GUARDA O ACTULIZAR INFORMACION DE LA BASE DE DATOS, PENSADO PARA GUARDAR FILAS DE UN DATATABLE, FUNCIONANDO CON UN CICLO
        /// </summary>
        /// <param name="___DATOS">DATOS A GUARDAR string[,] _datos = {{"nombre", "db"}, {txtNombre.Text, txtDB.Text },{"varchar", "varchar"} }; {{NOMBRE DE LOS CAMPOS}{CONTROLES}{TIPO DE DATOS}}</param>
        /// <param name="___TABLA">NOMBRE DE LA TABLA A GUARDAR</param>
        /// <param name="___ACCION">VALOR QUE VALIDA SI ES NUEVO O MODIFICAR</param>
        /// <param name="___CONDICION">EN EL CASO DE MODIFICAR COLOCAR LA CONDICION A MODIFICAR id = 0</param>
        public void Guardar_Datatable(String[,] ___DATOS,  String ___TABLA, String ___CONDICION)
        {
            var _totalArray = ___DATOS.GetLength(0);
            var _totalDatos = ___DATOS.GetLength(1);
            String _sqlExiste = "SELECT * FROM "+___TABLA +" WHERE "+___CONDICION;
            String _sql;
            String ___ACCION = "";

            var resulExiste = 0;
            if (_TIPObasedatos == "sqlserver")
            {
                using (SqlConnection _con = new SqlConnection(_SQLCadenaConexion))
                {
                    //SqlCeDataAdapter myAdaptador = new SqlCeDataAdapter(_sql, _con);
                    //DataSet DS1 = new DataSet();
                    //myAdaptador.Fill(DS1, _sql);
                    //resultado= DS1.Tables[0].Rows.Count;

                    SqlDataAdapter myAdaptador = new SqlDataAdapter(_sqlExiste, _con);
                    DataTable dt = new DataTable();
                    myAdaptador.Fill(dt);
                    resulExiste = dt.Rows.Count;
                }
            }

            if (!(resulExiste > 0))
            {
                ___ACCION = "nuevo";
            }
            else
            {
                ___ACCION = "modificar";
            }


            if (___ACCION == "nuevo")
            {
                _sql = "INSERT INTO " + ___TABLA + "(";
                var _countDatos = 1;
                for (int i = 0; i < _totalArray; i++)
                {
                    for (int j = 0; j < _totalDatos; j++)
                    {
                        if (i == 0)
                        {
                            if (_countDatos == _totalDatos)
                            {
                                _sql = _sql + ___DATOS[i, j] + ") VALUES (";
                                _countDatos = 1;
                            }
                            else
                            {
                                _sql = _sql + ___DATOS[i, j] + ", ";
                                _countDatos++;
                            }
                        }
                        else if (i == 1)
                        {
                            if (_countDatos == _totalDatos)
                            {
                                if (___DATOS[2, j] == "varchar")
                                {
                                    _sql = _sql + "'" + ___DATOS[i, j] + "')";
                                }
                                else if (___DATOS[2, j] == "int")
                                {
                                    _sql = _sql + ___DATOS[i, j] + ")";
                                }

                                _countDatos = 1;
                            }
                            else
                            {
                                if (___DATOS[2, j] == "varchar")
                                {
                                    _sql = _sql + "'" + ___DATOS[i, j] + "', ";
                                }
                                else if (___DATOS[2, j] == "int")
                                {
                                    _sql = _sql + ___DATOS[i, j] + ", ";

                                }

                                _countDatos++;
                            }
                        }
                    }
                }
            }
            else if (___ACCION == "modificar")
            {




                _sql = "UPDATE " + ___TABLA + " SET ";
                var countDatos = 1;
                for (int i = 0; i < _totalArray - 1; i++)
                {
                    for (int j = 0; j < _totalDatos; j++)
                    {
                        if (i == 0)
                        {
                            if (countDatos == _totalDatos)
                            {
                                if (___DATOS[2, j] == "varchar")
                                {
                                    _sql = _sql + ___DATOS[i, j] + "='" + ___DATOS[1, j] + "'";
                                }
                                else if (___DATOS[2, j] == "int")
                                {
                                    _sql = _sql + ___DATOS[i, j] + "=" + ___DATOS[1, j] + "";
                                }
                            }
                            else
                            {
                                if (___DATOS[2, j] == "varchar")
                                {
                                    _sql = _sql + ___DATOS[i, j] + "='" + ___DATOS[1, j] + "', ";
                                }
                                else if (___DATOS[2, j] == "int")
                                {
                                    _sql = _sql + ___DATOS[i, j] + "=" + ___DATOS[1, j] + ", ";
                                }
                            }

                            countDatos++;
                        }


                    }
                    //countDatos = 0;
                }
                _sql = _sql + " WHERE " + ___CONDICION;




            }
            else
            {
                _sql = "";
            }




            //MessageBox.Show(sql);

            int resultado = 0;
            if (_TIPObasedatos == "sqlserver")
            {
                using (SqlConnection _con = new SqlConnection(_SQLCadenaConexion))
                {
                    SqlCommand comando = new SqlCommand(_sql, _con);
                    resultado = comando.ExecuteNonQuery();
                    //            __dblocal.Close();
                }
            }
            //else if (_TIPObasedatos == "dbcompaq")
            // {
            //     using (SqlCeConnection _con = CeConexion())
            //     {
            //         SqlCeCommand comando = new SqlCeCommand(_sql, _con);
            //         resultado = comando.ExecuteNonQuery();
            //         //            __dblocal.Close();
            //     }
            // }
            else
            {
                resultado = 0;
            }


            if (resultado == 1)
            {
                //MessageBox.Show("Se ha almacenado la información", "¡EXITO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //reiniciar_campos(___FORMULARIO);
                //___FORMULARIO.Close();
            }
            else
            {
                MessageBox.Show("No logro guardarse");
            }
        }

        /// <summary>
        /// ELIMINA EL REGISTRO
        /// </summary>
        /// <param name="___TABLA">NONBRE DE LA TABLA</param>
        /// <param name="___IDDATO">ID DEL DATO A ELIMINAR</param>
        /// <param name="___DATAGRID">DATAGRIDVIEW PARA ACTUALIZAR SU CONTENIDO CON LA FUNCION DE CARGAR DATOS</param>
        public void eliminar_datos(String ___TABLA,String ___IDDATO, DataGridView ___DATAGRID)
        {
            var eliminar = MessageBox.Show("¿Desea eliminar?", "¡Aviso!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (eliminar == DialogResult.Yes)
            {
                var _sql = "DELETE FROM "+ ___TABLA + " WHERE id = " + ___IDDATO;
                int resultado = 0;
                if (_TIPObasedatos == "sqlserver")
                {
                    using (SqlConnection _con = new SqlConnection(_SQLCadenaConexion))
                    {
                        _con.Open();
                        SqlCommand comando = new SqlCommand(_sql, _con);
                        resultado = comando.ExecuteNonQuery();
                        //            __dblocal.Close();
                    }
                }
                //else if (_TIPObasedatos == "dbcompaq")
                //{
                //    using (SqlCeConnection _con = CeConexion())
                //    {
                //        SqlCeCommand comando = new SqlCeCommand(_sql, _con);
                //        resultado = comando.ExecuteNonQuery();
                //        //            __dblocal.Close();
                //    }
                //}
                else
                {
                    resultado = 0;
                }
                    

                if (resultado == 1)
                {
                   // MessageBox.Show("Se ha eliminado", "¡EXITO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargar_datos(___DATAGRID, ___TABLA);
                }
                else
                {
                    MessageBox.Show("No logro Eliminarse");
                }
            }
        }

        public void LimpiarTabla(String _Tabla)
        {
            int resultado = 0;
            String _sql = "DELETE FROM "+_Tabla;
            if (_TIPObasedatos == "sqlserver")
            {
                using (SqlConnection _con = new SqlConnection(_SQLCadenaConexion))
                {
                    _con.Open();
                    SqlCommand comando = new SqlCommand(_sql, _con);
                    resultado = comando.ExecuteNonQuery();
                    //            __dblocal.Close();
                }
            }
            //else if (_TIPObasedatos == "dbcompaq")
            //{
            //    using (SqlCeConnection _con = CeConexion())
            //    {
            //        SqlCeCommand comando = new SqlCeCommand(_sql, _con);
            //        resultado = comando.ExecuteNonQuery();
            //        //            __dblocal.Close();
            //    }
            //}
            else
            {
                resultado = 0;
            }

            //cargar_datos(_DgvDatos, _Tabla);
        }

        delegate void ActuaProgressDelegate(ProgressBar _Barra, Decimal percent, Label _lblMensaje, String _Mensaje);
        delegate void ActuaToolProgressDelegate(ToolStripProgressBar _Barra, Decimal percent, ToolStripLabel _lblMensaje, String _Mensaje);
        public void ActuaProgress(ProgressBar _Barra, decimal _value, Label _lblMensaje, String _Mensaje)
        {
            _lblMensaje.Text = _Mensaje;
            if (this == null) return;
            _Barra.Value = (int)_value;
        }
        public void ActuaProgress(ToolStripProgressBar _Barra, decimal _value, ToolStripLabel _lblMensaje, String _Mensaje)
        {
            _lblMensaje.Text = _Mensaje;
            if (this == null) return;
            _Barra.Value = (int)_value;
        }
        /// <summary>
        /// LLENA EL PROGRESS BAR CUANDO ENTREA EN PROCESO
        /// </summary>
        /// <param name="_Formulario">FORMULARIO ACTUAL</param>
        /// <param name="_Barra">NOMBRE DEL PROGRESSBAR</param>
        /// <param name="_posiActual">VALUE PARA PROGRESSBAR</param>
        /// <param name="_total">TAMAÑO DEL PROGRESSBAR</param>
        /// <param name="_lblMensaje">LABEL DONDE SALDRA EL MENSAJE</param>
        /// <param name="_Mensaje">MENSAJE QUE EL LABEL MOSTRARA</param>
        public void Cargando(Form _Formulario,ProgressBar _Barra, Int32 _sleep, Int32 _posiActual, Decimal _total, Label _lblMensaje, String _Mensaje)
        {
            //_lblMensaje.Text = _Mensaje;
            if (!(_sleep==0))
            {
                Thread.Sleep(_sleep);
            }
            
            var percent = (_posiActual / _total) * 100;
            _Formulario.Invoke(new ActuaProgressDelegate(ActuaProgress), _Barra, percent, _lblMensaje, _Mensaje);
        }

        public void Cargando(Form _Formulario, ToolStripProgressBar _Barra, Int32 _sleep, Int32 _posiActual, Decimal _total,ToolStripLabel _lblMensaje, String _Mensaje)
        {
            if (!(_sleep == 0))
            {
                Thread.Sleep(_sleep);
            }
            var percent = (_posiActual / _total) * 100;
            _Formulario.Invoke(new ActuaToolProgressDelegate(ActuaProgress), _Barra, percent, _lblMensaje, _Mensaje);
        }


        delegate void CamposEnableDelegate(Form _Formulario, Boolean _Enabled, Button _btnHilo);
        delegate void TodosEnableDelegate(Form _Formulario, Boolean _Enabled);

        public void DesabilitarControles(Form _Formulario, Boolean _Enabled, Button _btnHilo)
        {
            _Formulario.Invoke(new CamposEnableDelegate(CamposEnabled), _Formulario, _Enabled, _btnHilo);
        }
        
        public void CamposEnabled(Form _Formulario, Boolean _Enabled, Button _btnHilo)
        {
            //var _focus = 0;
            foreach (Control c in _Formulario.Controls)
            {
                if (c is TextBox)
                {
                    c.Enabled = _Enabled;
                }
                else if (c is Button)
                {
                    c.Enabled = _Enabled;
                }
                else if (c is DataGridView)
                {
                    c.Enabled = _Enabled;
                }
                else if (c is CheckBox)
                {
                    c.Enabled = _Enabled;
                }
                else if (c is TabControl)
                {
                    c.Enabled = _Enabled;
                }
                else if (c is ComboBox)
                {
                    c.Enabled = _Enabled;
                }
                else if (c is PictureBox)
                {
                    c.Visible = !_Enabled;
                }


                _btnHilo.Enabled = true;
            }
        }
        
        public void CamposEnabled(Form _Formulario, Boolean _Enabled, Button _btnHilo, String btnTexto)
        {
            //var _focus = 0;
            foreach (Control c in _Formulario.Controls)
            {
                if (c is TextBox)
                {
                    c.Enabled = _Enabled;
                }
                else if (c is Button)
                {
                    c.Enabled = _Enabled;
                }
                else if (c is DataGridView)
                {
                    c.Enabled = _Enabled;
                }
                else if (c is CheckBox)
                {
                    c.Enabled = _Enabled;
                }
                else if (c is TabControl)
                {
                    c.Enabled = _Enabled;
                }
                else if (c is ComboBox)
                {
                    c.Enabled = _Enabled;
                }
                else if (c is PictureBox)
                {
                    c.Visible = !_Enabled;
                }

                _btnHilo.Enabled = true;
                _btnHilo.Text = btnTexto;
            }
        }

        public void DesabilitarControles(Form _Formulario, Boolean _Enabled)
        {
            _Formulario.Invoke(new TodosEnableDelegate(CamposEnabled), _Formulario, _Enabled);
        }
        public void CamposEnabled(Form _Formulario, Boolean _Enabled)
        {
            //var _focus = 0;
            foreach (Control c in _Formulario.Controls)
            {
                if (c is TextBox)
                {
                    c.Enabled = _Enabled;
                }
                else if (c is Button)
                {
                    c.Enabled = _Enabled;
                }
                else if (c is DataGridView)
                {
                    c.Enabled = _Enabled;
                }
                else if (c is CheckBox)
                {
                    c.Enabled = _Enabled;
                }
                else if (c is TabControl)
                {
                    c.Enabled = _Enabled;
                }
                else if (c is ComboBox)
                {
                    c.Enabled = _Enabled;
                }
                else if (c is PictureBox)
                {
                    c.Visible = !_Enabled;
                }

            }
        }



        public void icono(Form _FORM)
        {
            //La ruta de la imagen
            string ruta = Application.StartupPath + @"\recursos\comita.ico";

            //Comprobamos si existe
            if (File.Exists(ruta))
            {
                //Limpiamos la imagen actual
                _FORM.BackgroundImage = null;

                //La cargamos de nuevo
                //Bitmap bmp = new Bitmap(ruta);
                Icon ico = new Icon(ruta);

                //Y se la asignamos de nuevo al form
                _FORM.Icon = ico;
            }
        }




    }
}
