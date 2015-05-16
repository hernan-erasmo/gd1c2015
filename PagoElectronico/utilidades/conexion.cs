using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PagoElectronico.Utilidades
{
    public abstract class Conexion
    {
        public string cadenaConexion;
        protected string sentenciaSql;
        protected int resultado;
        protected SqlConnection conSql;
        protected SqlCommand comandoSql;
        protected string mensaje;
        protected SqlDataReader Reg;


        public Conexion()
        {
            this.cadenaConexion = (@"Data Source=JUAN-PC\SQLSERVER2008; Initial Catalog=GD1C2015; Integrated Security=True");
            this.conSql = new SqlConnection(this.cadenaConexion);
            this.Reg = null;
            this.sentenciaSql = string.Empty;
        }

        public string Mensaje
        {
            get
            {
                return this.mensaje;
            }
        }

    }
}