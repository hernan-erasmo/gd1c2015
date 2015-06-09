using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using PagoElectronico.Utilidades;
using System.Collections;

namespace PagoElectronico.Utils
{
    public class Usuario
    {
        private string username;
        private string password;
        private string nombre;
        private string apellido;
        private string documento;
        private string rol;
        private string rolId;
        private ArrayList funciones;
        private int codLogin;
        private int clienteId;
        private int usuarioId;


        //Constructor
        public Usuario()
        {
            this.username = string.Empty;
            this.password = string.Empty;
            this.rol = string.Empty;
            this.nombre = string.Empty;
            this.apellido = string.Empty;
            this.funciones = new ArrayList();
        }

        //Propiedades
        public string Username
        {
            get { return this.username; }
            set { this.username = value; }
        }
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }
        public string Documento
        {
            get { return this.documento; }
            set { this.documento = value; }
        }

        public string Rol
        {
            get { return this.rol; }
            set { this.rol = value; }
        }

        public string RolId
        {
            get { return this.rolId; }
            set { this.rolId = value; }
        }

        public ArrayList Funciones
        {
            get { return this.funciones; }
            set { this.funciones = value; }
        }

        public int CodLogin
        {
            get { return this.codLogin; }
            set { this.codLogin = value; }        
        }

        public int ClienteId
        {
            get { return this.clienteId; }
            set { this.clienteId = value; }
        }

        public int UsuarioId
        {
            get { return this.usuarioId; }
            set { this.usuarioId = value; }
        }

        public string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }

        public string Apellido
        {
            get { return this.apellido; }
            set { this.apellido = value; }
        }

        //llenaElDataGridDelFormulariDeFacturasPagas
        public DataTable llenarDataGridFacturasPagas()
        {
            conexion cn = new conexion();

            String sql = "SELECT f.Factura_Numero AS Id_Factura, f.Factura_Fecha AS Fecha FROM GD1C2015.SARASA.Factura f, GD1C2015.SARASA.Itemfact i, GD1C2015.SARASA.Cliente c WHERE i.Itemfact_Factura_Numero= f.Factura_Numero AND f.Factura_Cliente_Id=c.Cliente_Id AND i.Itemfact_Pagado=1 AND c.Cliente_Id=" +clienteId+ "GROUP BY f.Factura_Numero, f.Factura_Fecha";

            DataSet ds = new DataSet();
            SqlDataAdapter sqd = new SqlDataAdapter(sql, cn.abrir_conexion());
            sqd.Fill(ds, "Fila");
            return ds.Tables["Fila"];


        }
    }
}