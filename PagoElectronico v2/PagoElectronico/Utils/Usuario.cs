using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
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
            get { return this.UsuarioId; }
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
    }
}