using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Configuration;
using System.Web;
using System.Xml.Linq;

namespace PagoElectronico.Utils
{
    class conexion
    {
//        public static SqlConnection cn = null;
//        public static SqlCommand cmd;
//        public static SqlDataReader reader;
//        public static SqlDataAdapter adapter;


        public SqlConnection abrir_conexion()
        {
            try
            {
                string string_connection = ConfigurationManager.ConnectionStrings["PagoElectronico.Properties.Settings.ConnectionString"].ConnectionString;
                SqlConnection cn = new SqlConnection(string_connection);
                cn.Open();

                return cn;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se conecto: " + ex.ToString());
                return null;
            }
        }
    }
}