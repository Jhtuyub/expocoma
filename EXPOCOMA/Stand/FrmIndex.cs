using EXPOCOMA.inicio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EXPOCOMA.Stand
{
    public partial class FrmIndex : Form
    {
        funciones _funcion = new funciones();
        public String nomExpo;
        public String _CadenaConexion;
        private Thread CrearTablasThread;

        internal static ToolStripMenuItem opcPartSuc;
        internal static ToolStripMenuItem opcImporTabla;
        internal static ToolStripMenuItem opcProveArti;
        public FrmIndex()
        {
            InitializeComponent();
            opcPartSuc = empresasParticipantesToolStripMenuItem;
            opcImporTabla = importacionDeTablasToolStripMenuItem;
            opcProveArti = proveedoresYArticulosToolStripMenuItem;
        }

        private void FrmIndex_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form parent = this.Owner;
            parent.Show();
            GC.Collect();
        }

        public void FondoPantalla()
        {
            //Ruta donde se encuentra nuestra imagen
            string ruta = Application.StartupPath + @"\recursos\EXPO_ COMA.JPG";

            //Comprobamos que la ruta exista
            if (File.Exists(ruta))
            {
                //Creamos un Bitmap con la imagen
                Bitmap bmp = new Bitmap(ruta);

                //Se la colocamos de fondo al formulario
                this.BackgroundImage = bmp;
                this.BackgroundImageLayout = ImageLayout.Center;
            }
        }

        private void FrmIndex_Load(object sender, EventArgs e)
        {
            //this.BackgroundImage = "";
            _funcion.icono(this);
            FondoPantalla();
            //this.bac = Color.White;


            _funcion._SQLCadenaConexion = _CadenaConexion;
            this.Text = "EXPOCOMA "+nomExpo+ " - FrmIndex";
            //_funcion._SQLCadenaConexion = _CadenaConexion;
            toolStripSLMensaje.Text = "";
            CrearTablasThread = new Thread(Tablas);
            CrearTablasThread.IsBackground = true;
            CrearTablasThread.Start();

            
        }

        public Boolean habilitarSubMenu(String tabla, String campo = "id")
        {
            Boolean submenu = false;
            //_funcion._TIPObasedatos = "sqlserver";
            
            try
            {
                //DataTable dttblSucursal = _funcion.llenar_form(tabla);
                DataTable dttblSucursal = _funcion.llenar_dt(tabla,"TOP 1 "+ campo);
                if (dttblSucursal.Rows.Count > 0)
                {
                    submenu = true;
                }
                else
                {
                    submenu = false;
                }
            }
            catch (Exception)
            {

                submenu = false;
            }
            return submenu;
        }


        public void habiImportTablas(String tabla, ToolStripItem submenu)
        {
            //_funcion._TIPObasedatos = "sqlserver";
            //_funcion._SQLCadenaConexion = _CadenaConexion;
            try
            {
                DataTable dttblSucursal = _funcion.llenar_form(tabla);
                if (dttblSucursal.Rows.Count > 0)
                {
                    submenu.Enabled = true;
                }
                else
                {
                    submenu.Enabled = false;
                }
            }
            catch (Exception)
            {

                submenu.Enabled = false;
            }


        }

        public void Tablas()
        {

            


            Int32 resultado;

            String _tblSucursal = "IF OBJECT_ID('tbl_sucursal') IS NULL "
                  + "CREATE TABLE tbl_sucursal("
                  + "[id][int] IDENTITY(1, 1) NOT NULL,"
                  + "[id_catsucursal][int] NOT NULL,"
                  + "[anfitrion] bit  not null default 0,"
                  + "[almacen] [varchar](5) NOT NULL,"
                  + "[organization_id] [varchar](5) NOT NULL,"
                  + "[sucursal] [varchar](100) NOT NULL,"
                  + "[dbf] [varchar](100) NOT NULL,"
                  + "[servidor] [varchar](100) NOT NULL,"
                  + "[usuario] [varchar](100) NOT NULL,"
                  + "[contrasena] [varchar](100) NOT NULL,"
                  + "[db] [varchar](100) NOT NULL"
                  + ") ON[PRIMARY]";

            String _dbfArticulo = "IF OBJECT_ID('dbf_articulo') IS NULL "
                + "CREATE TABLE [dbf_articulo] ("
                + "[id][int] IDENTITY(1, 1) NOT NULL,"
                + "[ID_SUCURSALALM][varchar](5) NOT NULL,"
                + "[C_ARTI][varchar](4) NULL,"
                + "[FAMI_ARTI] [varchar](10) NULL,"
                + "[DES_ARTI] [varchar](250) NULL,"
                + "[DES_ART2] [varchar](250) NULL,"
                + "[CAP_ARTI] [varchar](7) NULL,"
                + "[EMPAQUE1] [numeric](4,0) NULL,"
                + "[EMPAQUE2] [numeric](4,0) NULL,"
                + "[UBICA_CAJA] [varchar](9) NULL,"

                + "[UBICA_PZAS] [varchar](9) NULL,"
                + "[UBICJ_PISO] [varchar](9) NULL,"
                + "[UBIPZ_PISO] [varchar](9) NULL,"
                + "[IEPS] [numeric](6,2) NULL,"
                + "[DES_CONTA] [numeric](6,2) NULL,"
                + "[DES_CONTF] [numeric](6,2) NULL,"
                + "[MARG_PISO] [numeric](6,2) NULL,"
                + "[MARG_CESP] [numeric](6,2) NULL,"
                + "[P_MAX] [numeric](12,2) NULL,"
                + "[PESO_ARTI] [numeric](7,2) NULL,"

                + "[FAMI_COMI] [varchar](2) NULL,"
                + "[PRECIO_VEN] [numeric](12,2) NULL,"
                + "[PRECIO_VEF] [numeric](12,2) NULL,"
                + "[COMPRAS] [numeric](14,2) NULL,"
                + "[VENTAS] [numeric](14,2) NULL,"
                + "[STATUS] [varchar](20) NULL,"
                + "[OF_PIEZAS] [numeric](4) NULL,"
                + "[OF_DESCTO] [numeric](6,2) NULL,"
                + "[C_PROVE] [varchar](5) NULL,"
                + "[C_PROVE2] [varchar](8) NULL,"
                + "[COSTO] [numeric](9,2) NULL,"

                + "[CAJA] [varchar](2) NULL,"
                + "[UNIDAD] [varchar](2) NULL,"
                + "[EXHIBIDOR] [varchar](2) NULL,"
                + "[MARG_PRE3] [numeric](6,2) NULL,"
                + "[MARG_PRE4] [numeric](6,2) NULL,"
                + "[CANTIDAD] [numeric](4) NULL,"
                + "[CANCELA] [varchar](1) NULL,"
                + "[PLAN] [numeric](3) NULL,"
                + "[ALTURA] [numeric](3) NULL,"
                + "[FECHA_ALTA] [datetime] NULL,"

                + "[CODBAR] [varchar](15) NULL,"
                + "[ANCHO] [numeric](7,2) NULL,"
                + "[ALTO] [numeric](7,2) NULL,"
                + "[LARGO] [numeric](7,2) NULL,"
                + "[CONTAMINA] [varchar](1) NULL,"
                + "[FALTANTE] [varchar](1) NULL,"
                + "[FE_FALTA] [datetime] NULL,"
                + "[HO_FALTA] [varchar](5) NULL,"
                + "[CAUSA_FAL] [numeric](2) NULL,"
                + "[ACTIVA_ENT] [varchar](1) NULL,"

                + "[MINART] [varchar](1) NULL,"
                + "[VTAMAX] [numeric](3) NULL,"
                + "[PESO_EQUI] [numeric](9,2) NULL,"
                + "[CB_CAJA] [varchar](15) NULL,"
                + "[FE_BAJA] [datetime] NULL,"
                + "[INI_PEDIR] [numeric](10) NULL,"
                + "[ROTACION] [varchar](2) NULL,"
                + "[CB_EMPLAYE] [varchar](15) NULL,"
                + "[COSTO_TA] [numeric](9,2) NULL,"
                + "[POLTRANSFE] [bit] NOT NULL DEFAULT 0,"

                + "[DIAS_INV] [numeric](3) NULL,"
                + "[COMESTIBLE] [bit] NOT NULL DEFAULT 0,"
                + "[CADUCIDAD] [bit] NOT NULL DEFAULT 0,"
                + "[TIPOART] [numeric](1) NULL,"
                + "[REV_DATOS] [bit] NOT NULL DEFAULT 0,"
                + "[EXIS_MAX] [numeric](8) NULL,"
                + "[EXIS_MIN] [numeric](8) NULL,"
                + "[PED_ALTER] [bit] NOT NULL DEFAULT 0,"
                + "[ACT_PISO] [bit] NOT NULL DEFAULT 0,"
                + "[FALTACOM] [varchar](1) NULL,"

                + "[FE_FALTCOM] [datetime] NULL,"
                + "[HO_FALTCOM] [varchar](5) NULL,"
                + "[ID_ARTI] [numeric](8) NULL,"
                + "[CUADXTAR] [bit] NOT NULL DEFAULT 0,"
                + "[TIPO_ART] [varchar](1) NULL,"
                + "[FPGC] [numeric](10,2) NULL,"
                + "[COD_SKU] [varchar](10) NULL,"
                + "[STATUS_OLD] [varchar](1) NULL,"
                + "[CANT_OLD] [numeric](6, 0) NULL,"
                + "[CAN_OLD] [numeric](4, 0) NULL,"

                + "[STAT_OLD] [varchar](1) NULL,"
                + "[NOBOLSA] [int] NULL,"
                + "[EMPLAYEFAB] [bit] NOT NULL DEFAULT 0,"
                + "[TIPOBOL] [int] NULL,"
                + "[CTO_WILLY] [numeric](9,2) NULL,"
                + "[F_CAMBIO] [varchar](100) NULL"
                + ")";

            String _dbfFamArti = "IF OBJECT_ID('dbf_fam_arti') IS NULL "
                + "CREATE TABLE [dbf_fam_arti] ("
                + "[id][int] IDENTITY(1, 1) NOT NULL,"
                + "[ID_SUCURSALALM][varchar](5) NOT NULL,"
                + "[FAMI_ARTI] [varchar](3),"
                + "[NOMBRE] [varchar](150),"
                + "[IVA] [varchar](15),"
                + "[IVA2] [varchar](15),"
                + "[TIPO_FAMI] [varchar](1),"
                + "[MARCA] [varchar](1)"
                + ")";

            String _dbfPorpieza = "IF OBJECT_ID('dbf_porpieza') IS NULL "
                + "CREATE TABLE [dbf_porpieza] ("
                + "[id][int] IDENTITY(1, 1) NOT NULL,"
                + "[ID_SUCURSALALM][varchar](5) NOT NULL,"
                + "[C_ARTI] [varchar](4) NULL,"
                + "[PORCENTAJE] [Numeric](5,2) NULL"
                + ")";
            String _dbfCliente = "IF OBJECT_ID('dbf_cliente') IS NULL "
            + "CREATE TABLE[dbf_cliente] ("
            + "[id][int] IDENTITY(1, 1) NOT NULL,"
            + "[ID_SUCURSALALM][varchar](5) NOT NULL,"
            + "[C_CLIENTE] [varchar](5) NULL,"
            + "[C_AGENTE] [varchar](3) NULL,"
            + "[C_RUTA][varchar](2) NULL,"
            + "[NOM_CLIEN][varchar](40) NULL,"
            + "[DIRECCI][varchar](40) NULL,"
            + "[POBLACION][varchar](20) NULL,"
            + "[NOM_TIENDA][varchar](100) NULL,"
            + "[TELEFONO][varchar](10) NULL," //9

            + "[CELULAR][varchar](10) NULL,"
            + "[F_A_CLIENT][datetime] NULL,"
            + "[DIA_VISITA][numeric](1) NULL,"
            + "[SAL_ACTU][numeric](12,2) NULL,"
            + "[SAL_ANTE][numeric](12,2) NULL,"
            + "[CARGO][numeric](12,2) NULL,"
            + "[ABONO][numeric](12,2) NULL,"
            + "[RFC][varchar](16) NULL,"
            + "[STAT_PAGA2][varchar](1) NULL,"
            + "[REMEDIO][numeric](13,2) NULL," //19

            + "[NUM_ORD][numeric](5) NULL,"
            + "[ESPECIAL][varchar](1) NULL,"
            + "[FLETE][varchar](1) NULL,"
            + "[STATUS][varchar](1) NULL,"
            + "[TIPO_CLI][numeric](2) NULL,"
            + "[PREOFE][varchar](1) NULL,"
            + "[SALDO][numeric](12,2) NULL,"
            + "[PROMCOM][numeric](12,2) NULL,"
            + "[DOC_IEPS][varchar](1) NULL,"
            + "[TIPO_PRE][numeric](1) NULL,"

            + "[ESRES][varchar](1) NULL,"
            + "[REFDIR][varchar](100) NULL,"
            + "[FEC_DOCTOS][datetime] NULL,"
            + "[ID_DOCTOS][varchar](14) NULL,"
            + "[FEC_CREDI][datetime] NULL,"
            + "[AUT_CREDI][varchar](15) NULL,"
            + "[LIM_CREDI][numeric](12,2) NULL,"
            + "[HOR_CREDI][varchar](5) NULL,"
            + "[PLAZO][numeric](2) NULL,"
            + "[FEC_CAN][datetime] NULL,"

            + "[HOR_CAN][varchar](5) NULL,"
            + "[AUT_CAN][varchar](15) NULL,"
            + "[MOTIVO][text] NULL,"
            + "[COD_RUDI][numeric](6) NULL,"
            + "[COD_POBLA][numeric](3) NULL,"
            + "[EXCLUYE][varchar](1) NULL,"
            + "[ID_FECHA][datetime] NULL,"
            + "[ID_USUARIO][varchar](15) NULL,"
            + "[ID_HORA][varchar](5) NULL,"
            + "[CVE_ANT][varchar](5) NULL,"

            + "[CVE_POS][varchar](5) NULL,"
            + "[MARGEN][numeric](7,2) NULL,"
            + "[PRESUPUEST][numeric](6,4) NULL,"
            + "[VENCE_TIPO][datetime] NULL,"
            + "[ORDEN_V][numeric](4) NULL,"
            + "[NUEVO][varchar](1) NULL,"
            + "[SUPERFICIE][numeric](8,2) NULL,"
            + "[GRPCLIENTE][numeric](2) NULL,"
            + "[CURP][varchar](23) NULL,"
            + "[R_DIASENT][numeric](3) NULL,"

            + "[GRUPO][varchar](5) NULL,"
            + "[DIR_FACT][varchar](1) NULL,"
            + "[DIR_PRIM][varchar](1) NULL,"
            + "[CONSEC][numeric](4) NULL,"
            + "[ORACLE][datetime] NULL,"
            + "[FE_ALT_ESP][datetime] NULL,"
            + "[FE_BAJ_ESP][datetime] NULL,"
            + "[VAL_DES][varchar](1) NULL,"
            + "[PVAL_DES][numeric](6,2) NULL,"
            + "[TAR_CRE][varchar](1) NULL,"

            + "[PTAR_CRE][numeric](6,2) NULL,"
            + "[KIT][varchar](1) NULL,"
            + "[DIREC2][varchar](20) NULL,"
            + "[DIREC3][varchar](40) NULL,"
            + "[RELACION][bit] NOT NULL DEFAULT 0,"
            + "[ACT_CREDI][varchar](50) NULL,"
            + "[FE_ACTCRED][datetime] NULL,"
            + "[HR_ACTCRED][varchar](5) NULL,"
            + "[CORREO][varchar](50) NULL,"
            + "[LATITUD][numeric](12,8) NULL,"

            + "[LONGITUD][numeric](12,8) NULL,"
            + "[CALLE1][varchar](60) NULL,"
            + "[CALLE2][varchar](60) NULL,"
            + "[NUMEXT][varchar](10) NULL,"
            + "[NUMINT][varchar](10) NULL,"
            + "[COLONIA][varchar](100) NULL,"
            + "[MUNICIPIO][varchar](30) NULL,"
            + "[POBLACIONE][varchar](30) NULL,"
            + "[ESTADO][varchar](20) NULL,"
            + "[CODPOST][numeric](5) NULL,"//89

            + "[CRECILIM][numeric](12,2) NULL,"
            + "[FE_CRECI][datetime] NULL,"
            + "[HO_CRECI][varchar](5) NULL,"
            + "[AUTO_CRECI][varchar](15) NULL,"
            + "[ACT_CRECI][varchar](15) NULL,"
            + "[FEAUTCRECI][datetime] NULL,"
            + "[HRAUTCRECI][varchar](5) NULL,"
            + "[CRUZAMIEN][varchar](120) NULL,"
            + "[CODPAIS][numeric](2) NULL,"
            + "[LADA][numeric](3) NULL,"
            + "[NOMBRES][varchar](160) NULL,"
            + "[APELLIDOS][varchar](160) NULL"
            + ")";

            String _dbfPrefijos = "IF OBJECT_ID('dbf_prefijos') IS NULL "
                + "CREATE TABLE [dbf_prefijos] ("
                + "[id][int] IDENTITY(1, 1) NOT NULL,"
                + "[ID_SUCURSALALM][varchar](5) NOT NULL,"
                + "[C_ARTI][varchar](4),"
                + "[PRECIO1][numeric](8,2),"
                + "[PRECIO2][numeric](8,2),"
                + "[PRECIO3][numeric](8,2),"
                + "[PRECIO4][numeric](8,2)"
                + ")";

            String _dbfProveedo = "IF OBJECT_ID('dbf_proveedo') IS NULL "
                + "CREATE TABLE [dbf_proveedo] ("
                + "[id][int] IDENTITY(1, 1) NOT NULL,"
                + "[ID_SUCURSALALM][varchar](5) NOT NULL,"
                + "[C_PROVE][varchar](6) NULL,"
                + "[C_PROVE2][varchar](8) NULL,"
                + "[DESCRI][varchar](225) NULL,"
                + "[CALLE][varchar](30) NULL,"
                + "[NUMERO][varchar](6) NULL,"
                + "[COLONIA][varchar](50) NULL,"
                + "[CIUDAD][varchar](20) NULL,"
                + "[TEL_AGEN][varchar](12) NULL,"
                +"[NOM_AGEN][varchar](30) NULL,"

                +"[NOM_GEREN][varchar](30) NULL,"
                +"[TEL_GEREN][varchar](12) NULL,"
                +"[PAGO_ANT][varchar](1) NULL,"
                +"[PUESTO_BOD][varchar](1) NULL,"
                +"[BON_M_MALA][varchar](1) NULL,"
                +"[BON_M_FAL][varchar](1) NULL,"
                +"[CAMB_FISI][varchar](1) NULL,"
                +"[FLETE][varchar](2) NULL,"
                +"[COMPRADO][numeric](19,2) NULL,"
                +"[CAJAS][numeric](13,2) NULL,"

                +"[DIA_PEDIDO][varchar](1) NULL,"
                +"[TIEMPO_SUR][numeric](3) NULL,"
                +"[RESP_COMA][varchar](1) NULL,"
                +"[RESURTIDO][varchar](1) NULL,"
                +"[COMENTARIO][varchar](60) NULL,"
                +"[FECHA_ALTA][datetime] NULL,"
                +"[RFC_PROVE][varchar](16) NULL,"
                +"[CO_POSTAL][varchar](6) NULL,"
                +"[AP_POSTAL][varchar](6) NULL,"
                +"[PROVE_LF][varchar](1) NULL,"

                +"[NOM_DIREC][varchar](20) NULL,"
                +"[TEL_DIREC][varchar](12) NULL,"
                +"[GEREN_VTAS][varchar](20) NULL,"
                +"[TEL_VTAS][varchar](12) NULL,"
                +"[FAX][varchar](10) NULL,"
                +"[BONIFICA][varchar](2) NULL,"
                +"[DEVOLUCION][varchar](1) NULL,"
                +"[DES_VOL][numeric](5,2) NULL,"
                +"[DES_NODEV][numeric](5,2) NULL,"
                +"[DES_CONFI][numeric](5,2) NULL,"

                +"[DES_PCRECI][numeric](5,2) NULL,"
                +"[DES_COMER][numeric](9,5) NULL,"
                +"[STATUS][varchar](1) NULL,"
                +"[CAMBIO][datetime] NULL,"
                +"[OBSERV1][varchar](60) NULL,"
                +"[OBSERV2][varchar](60) NULL,"
                +"[RESP_SUR][varchar](20) NULL,"
                +"[TEL_RSUR][varchar](12) NULL,"
                +"[TEL_RSUR2][varchar](12) NULL,"
                +"[DCONFI_A][numeric](6,2) NULL,"

                +"[DCONFI_NA][numeric](6,2) NULL,"
                +"[RESP_SEG][varchar](1) NULL,"
                +"[COD_CONTAB][varchar](4) NULL,"
                +"[COD_ANTICI][varchar](4) NULL,"
                +"[TAB_DESCAR][numeric](4) NULL,"
                +"[P_DESCAR][varchar](1) NULL,"
                +"[MARCA][varchar](1) NULL,"
                + "[M_CONSIGNA][bit] NOT NULL DEFAULT 0,"
                + "[IMP_10][bit] NOT NULL DEFAULT 0,"
                +"[ID_PROV][numeric](8) NULL,"

                +"[FORMADEV][varchar](2) NULL"
                +")";

            String _dbfStatarti = "IF OBJECT_ID('dbf_statarti') IS NULL "
                + "CREATE TABLE [dbf_statarti] ("
                + "[id][int] IDENTITY(1, 1) NOT NULL,"
                + "[ID_SUCURSALALM][varchar](5) NOT NULL,"
                + "[C_ARTI][varchar](6) NULL,"
                +"[STATUS][numeric](2) NULL,"
                +"[CONCEPTO][varchar](225) NULL,"
                +"[DES_ARTI][varchar](225) NULL,"
                +"[EMPAQUE][numeric](4) NULL,"
                +"[CAP_ARTI][varchar](7) NULL,"
                +"[C_PROVE][varchar](10) NULL,"
                +"[CANT_ST5][numeric](6) NULL,"

                +"[UNID_ST5][varchar](50) NULL,"
                +"[FEC_ST5][datetime] NULL,"
                +"[MARCA][varchar](1) NULL,"
                +"[UPD_ORACLE][datetime] NULL,"
                +"[C_PROVE2][varchar](8) NULL"
                +")";

            String _dbfEstatus = "IF OBJECT_ID('dbf_estatus') IS NULL "
                +"CREATE TABLE[dbf_estatus] ("
                + "[id][int] IDENTITY(1, 1) NOT NULL,"
                + "[ID_SUCURSALALM][varchar](5) NOT NULL,"
                + "[STATUS][numeric](2) NULL,"
                + "[CONCEPTO][varchar](40) NULL,"
                + "[FECHA_ALT][datetime] NULL,"
                + "[MARCA][varchar](1) NULL"
                + ")";

            String _catprovexpo = "IF OBJECT_ID('cat_provexpo') IS NULL "
                + "CREATE TABLE[cat_provexpo] ("
                + "[id][int] IDENTITY(1, 1) NOT NULL,"
                + "[ID_SUCURSALALM][varchar](5) NOT NULL,"
                + "[orden][numeric](3) NOT NULL,"
                + "[c_prove2][varchar](8) NULL,"
                + "[c_prove][varchar](4) NULL,"
                + "[nom_prove][varchar](200) NULL,"
                + "[comprador][varchar](5) NULL"
                + ")";

            String _tblprovexpo = "IF OBJECT_ID('tbl_provexpo') IS NULL "
                 + "CREATE TABLE[tbl_provexpo] ("
                + "[id][int] IDENTITY(1, 1) NOT NULL,"
                + "[ID_SUCURSALALM][varchar](5) NOT NULL,"
                + "[C_PROVE] [varchar](6) NOT NULL,"
                + "[C_PROVE2][varchar](8) NULL,"
                + "[DESCRI] [varchar](225) NOT NULL,"
                + "[PORADIC] [numeric](10, 4) NULL,"
                + "[MARGEN] [numeric](10,4) NULL,"
                + "[COMPRADOR] [varchar](5) NOT NULL"
                + ")";

            String _tblartiexpo = "IF OBJECT_ID('tbl_artiexpo') IS NULL "
                 + "CREATE TABLE[tbl_artiexpo] ("
                + "[id][int] IDENTITY(1, 1) NOT NULL,"
                + "[ID_SUCURSALALM][varchar](5) NOT NULL,"
                +"[C_ARTI][varchar](4) NULL,"
                +"[FAMI_ARTI][varchar](10) NULL,"
                +"[DES_ARTI][varchar](250) NULL,"
                +"[CAP_ARTI][varchar](7) NULL,"
                +"[EMPAQUE2][numeric](4,0) NULL,"
                +"[PRECIO_VEN][numeric](12,2) NULL,"
                +"[PRECIO_ESP][numeric](12,2) NULL,"
                +"[STATUS][varchar](20) NULL,"
                +"[C_PROVE][varchar](5) NULL,"
                + "[C_PROVE2][varchar](8) NULL,"
                + "[CANTIDAD][numeric](4) NULL,"
                +"[CANCELA][varchar](1) NULL,"
                +"[FALTANTE][varchar](1) NULL,"
                +"[UNI_VEN][varchar](1) NULL,"
                +"[COSTO][numeric](9,2) NULL,"
                +"[MARGEN][numeric](6,2) NULL,"
                +"[IEPS][numeric](6,2) NULL,"
                +"[UNIDAD][varchar](2) NULL,"
                +"[CAJA][varchar](2) NULL,"
                +"[EXHIBIDOR][varchar](2) NULL,"
                +"[PORCENTAJE][numeric](5,2) NULL,"
                +"[POR_CAJA][numeric](10,4) NULL,"
                +"[OFERTA][bit]NOT NULL DEFAULT 0,"
                +"[IDARTI][numeric](4) NULL"
                + ")";




            String[,] tablas = {
                { "tbl_sucursal", "dbf_articulo", "dbf_fam_arti", "dbf_porpieza", "dbf_cliente", "dbf_prefijos", "dbf_proveedo", "dbf_statarti", "dbf_estatus", "tbl_provexpo","tbl_artiexpo" },
                {_tblSucursal, _dbfArticulo, _dbfFamArti, _dbfPorpieza, _dbfCliente,_dbfPrefijos, _dbfProveedo, _dbfStatarti, _dbfEstatus,_tblprovexpo, _tblartiexpo }
            };
            //MessageBox.Show(_CadenaConexion);

            using (SqlConnection _Con = new SqlConnection(_CadenaConexion))
            {
                Decimal _total =13;
                Int32 posicion = 1;
                SqlCommand Crear;
                _funcion.Cargando(this, toolStripPBar, 10, posicion, _total, toolStripSLMensaje, "...");
                
                _Con.Open();

                var posicionBar = 0;
                var totalTablas = tablas.GetLength(1) + 1;
                for (int i = 0; i < totalTablas - 1; i++)
                {
                    posicionBar +=  i;
                    _funcion.Cargando(this, toolStripPBar, 10, posicion, totalTablas, toolStripSLMensaje, "Validando " + tablas[0,i]);

                    Crear = new SqlCommand(tablas[1, i], _Con);
                    resultado = Crear.ExecuteNonQuery();
                    posicion++;
                    _funcion.Cargando(this, toolStripPBar, 10, posicion, totalTablas, toolStripSLMensaje, "Listo "+ tablas[0, i]);


                }

              
            }



            //this.Invoke((MethodInvoker)delegate
            //{
            //habiImportTablas("tbl_sucursal", importacionDeTablasToolStripMenuItem);
            //_funcion.Cargando(this, toolStripPBar, 10, 1, 1, toolStripSLMensaje, "Listo");
            importacionDeTablasToolStripMenuItem.Enabled = habilitarSubMenu("tbl_sucursal");
            //proveedoresYArticulosToolStripMenuItem.Enabled = habilitarSubMenu("dbf_articulo");
            if (habilitarSubMenu("dbf_proveedo") && habilitarSubMenu("dbf_articulo"))
            {
                proveedoresYArticulosToolStripMenuItem.Enabled = true;

            }
            else
            {
                proveedoresYArticulosToolStripMenuItem.Enabled = false;
            }
            //});

            _funcion.Cargando(this, toolStripPBar, 10, 0, 1, toolStripSLMensaje, "...");

        }

        private void empresasParticipantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSucursales _frmSucusal = new FrmSucursales();
            _frmSucusal.MdiParent = this;
            _frmSucusal._CadenaConexion = _CadenaConexion;
            //_frmSucusal.Owner = this;
            _frmSucusal.Show();
            empresasParticipantesToolStripMenuItem.Enabled = false;
            importacionDeTablasToolStripMenuItem.Enabled = false;
        }

        private void importacionDeTablasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmImporTablas _frmImporTablas = new FrmImporTablas();
            _frmImporTablas.MdiParent = this;
            _frmImporTablas._CadenaConexion = _CadenaConexion;
            _frmImporTablas.nomExpo = nomExpo;
            //_frmSucusal.Owner = this;
            _frmImporTablas.Show();
            
            empresasParticipantesToolStripMenuItem.Enabled = false;
            importacionDeTablasToolStripMenuItem.Enabled = false;
            opcProveArti.Enabled = false;
        }

        private void proveedoresYArticulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProveArti _frmProveArti = new FrmProveArti();
            _frmProveArti.MdiParent = this;
            _frmProveArti._CadenaConexion = _CadenaConexion;
            //_frmSucusal.Owner = this;
            _frmProveArti.Show();
            empresasParticipantesToolStripMenuItem.Enabled = false;
            importacionDeTablasToolStripMenuItem.Enabled = false;
            proveedoresYArticulosToolStripMenuItem.Enabled = false;
        }

        private void FrmIndex_SizeChanged(object sender, EventArgs e)
        {
            //FondoPantalla();
        }
    }
}
