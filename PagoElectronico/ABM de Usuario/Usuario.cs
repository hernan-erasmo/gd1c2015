using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace PagoElectronico.ABM_de_Usuario
{
    class Usuario : PagoElectronico.Utilidades.Conexion
    {
        private string username;
        private string password;

        public Usuario()
        {
            this.username = string.Empty;
            this.password = string.Empty;
            this.sentenciaSql = string.Empty;
        }

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

        public bool Buscar()
        {
            bool Resultado = false;
            this.sentenciaSql = string.Format(@"SELECT usuario_id FROM Usuario WHERE usuario_username='{0}' AND usuario_password='{1}'", this.username, this.password);
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
                this.mensaje = "Usuario/Contraseña Incorrectas";
            }
            this.conSql.Close();
            return Resultado;
        }

    }
}