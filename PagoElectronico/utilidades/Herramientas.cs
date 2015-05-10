using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
//using PagoElectronico.Utilidades;
using System.Data;
using System.Configuration;
using System.Web;
using System.Xml.Linq;
using PagoElectronico.Utilidades;

namespace PagoElectronico.Entidades
{
    class Herramientas
    {
        public static void msebox_informacion(string texto)
        {
            MessageBox.Show(texto, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void msgbox_advertencia(string texto)
        {
            MessageBox.Show(texto, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public Herramientas()
        {
        }

        public static void llenarComboBox(ComboBox cb, string consulta)
        {
            SqlDataReader dReader;
            try
            {

                conexion cn = new conexion();
                cn.abrir_conexion();

                SqlCommand query = new SqlCommand(consulta, cn.abrir_conexion());
                dReader = query.ExecuteReader();

                while (dReader.Read())
                {
                    cb.Items.Add(dReader[1]);
                }
                dReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar el combo Box " + ex.ToString());
            }
        }


        public static SqlDataReader ejecutarConsultaSimple(string consulta)
        {
            try
            {
                SqlDataReader dReader;
                conexion cn = new conexion();

                SqlCommand query = new SqlCommand(consulta, cn.abrir_conexion());
                dReader = query.ExecuteReader();

                return dReader;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
                return null;
            }
        }
    }
}
