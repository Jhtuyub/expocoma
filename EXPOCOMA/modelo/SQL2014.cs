using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EXPOCOMA
{
    class SQL2014
    {
        private SqlConnection conex;
        public SQL2014()
        {
            try
            {
                conex = new SqlConnection();
                conex.ConnectionString = "Data Source=JTUYUB\\SQLEXPRESS;"
                                        + "Initial Catalog=expo_merida2016;"
                                        +"Integrated Security=false;"
                                        +"UID=administrator;"
                                        +"PWD=Peritas01;"; //
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        public void Open()
        {
            try { conex.Open(); }
            catch (SqlException ex) { MessageBox.Show("" + ex); }
        }

        public void Close()
        {
            conex.Close();
        }
    }
}
