using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EXPOCOMA.modelo
{   
    class dbconexion
    {

        public static SqlCeConnection CeConexion()
        {

            SqlCeConnection _con = new SqlCeConnection("Data Source =" + Properties.Settings.Default.dblocal + ".sdf; Persist Security Info = False");
            //SqlCeConnection _con = new SqlCeConnection("Data Source =dbexpo.sdf; Persist Security Info = False");
            _con.Open();
            return _con;
        }

        public String SqlConexion(String _DB)
        {
            return "Data Source=" + Properties.Settings.Default.sqlserServidor + ";"
                                  + "Initial Catalog=" + _DB + ";"
                                  + "Integrated Security=false;"
                                  + "UID=" + Properties.Settings.Default.sqlserUsuario + ";"
                                  + "PWD=" + Properties.Settings.Default.sqlserPass + ";";
        }

        public String SqlCone()
        {
            return "Data Source=" + Properties.Settings.Default.sqlserServidor + ";"
                                  + "Initial Catalog=master;"
                                  + "Integrated Security=false;"
                                  + "UID=" + Properties.Settings.Default.sqlserUsuario + ";"
                                  + "PWD=" + Properties.Settings.Default.sqlserPass + ";";
        }

        public static SqlConnection SqlConexion_e(String ___TABLA)
        {

            SqlConnection _con = new SqlConnection("Data Source=" + Properties.Settings.Default.sqlserServidor + ";"
                                  + "Initial Catalog=" + ___TABLA + ";"
                                  + "Integrated Security=false;"
                                  + "UID=" + Properties.Settings.Default.sqlserUsuario + ";"
                                  + "PWD=" + Properties.Settings.Default.sqlserPass + ";");
            try
            {
                _con.Open();
                // Insert code to process data.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to connect to data source");
            }
            finally
            {
                _con.Close();
            }

            return _con;

            //SqlConnection _con = new SqlConnection();
            //try
            //{
            //     _con = new SqlConnection("Data Source=" + Properties.Settings.Default.sqlserServidor + ";"
            //                           + "Initial Catalog=" + ___TABLA + ";"
            //                           + "Integrated Security=false;"
            //                           + "UID=" + Properties.Settings.Default.sqlserUsuario + ";"
            //                           + "PWD=" + Properties.Settings.Default.sqlserPass + ";");
            //    _con.Open();
            //}
            //catch (ArgumentException arEx)
            //{
            //    MessageBox.Show("Algun problema :S " + arEx.ToString());

            //    //throw;
            //}
            //return _con;
        }

        //    private SqlConnection conex;
        //
        //    public dblocal()
        //    {
        //        try
        //        {
        //            conex = new SqlConnection();
        //            conex.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|db_expo.mdf;Integrated Security=True;Connect Timeout=30"; //
        //        }
        //        //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bin\Debug\db_expo.mdf;Integrated Security=True;Connect Timeout=30
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //
        //        }
        //    }
        //
        //    public void Open()
        //    {
        //        try { conex.Open(); }
        //        catch (SqlException ex) { MessageBox.Show("" + ex); }
        //    }
        //
        //    public void Close()
        //    {
        //        conex.Close();
        //    }
        //
        //}
    }
}
