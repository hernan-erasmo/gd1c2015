using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using PagoElectronico.Utilidades;

namespace PagoElectronico.ABM_de_Usuario
{
    class Usuario : PagoElectronico.Utilidades.Conexion
    {
        private string username;
        private string password;
        private string rol;

        //Constructor
        public Usuario()
        {
            this.username = string.Empty;
            this.password = string.Empty;
            this.sentenciaSql = string.Empty;
            this.rol = string.Empty;
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

        public string Rol
        {
            get { return this.rol; }
            set { this.rol = value; }
        }

        //Metodos
        public bool Buscar()
        {
            bool Resultado = false;
            this.sentenciaSql = string.Format(@"SELECT usuario_id, rol_id FROM Usuario, Rol, RolXUsuario WHERE usuario_username='{0}' AND usuario_password='{1}' AND rol_nombre='{2}' AND usuario_id=ru_usuario_id AND ru_rol_id=rol_id", this.username, this.password, this.rol);
            this.comandoSql = new SqlCommand(this.sentenciaSql, this.conSql);
            this.conSql.Open();
            Reg = null;
            Reg = this.comandoSql.ExecuteReader();
            if (Reg.Read())
            {
                Resultado = true;
                this.mensaje = "Bienvenido";
            }
            else
            {
                this.mensaje = "Error en los datos. Por favor verificar";
            }
            this.conSql.Close();
            return Resultado;
        }

    }
}