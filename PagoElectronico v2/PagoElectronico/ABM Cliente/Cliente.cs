using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagoElectronico.ABM_Cliente
{
    public class Cliente
    {
        private string clienteId;
        public string ClienteId
        {
            get { return this.clienteId; }
            set { this.clienteId = value; }
        }

        private string nombre;
        public string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }

        private string apellido;
        public string Apellido
        {
            get { return this.apellido; }
            set { this.apellido = value; }
        }

        private string mail;
        public string Mail
        {
            get { return this.mail; }
            set { this.mail = value; }
        }

        private string numeroDoc;
        public string NumeroDoc
        {
            get { return this.numeroDoc; }
            set { this.numeroDoc = value; }
        }

        private string tipoDocId;
        public string TipoDocId
        {
            get { return this.tipoDocId; }
            set { this.tipoDocId = value; }
        }

        private string paisId;
        public string PaisId
        {
            get { return this.paisId; }
            set { this.paisId = value; }
        }

        private string domCalle;
        public string DomCalle
        {
            get { return this.domCalle; }
            set { this.domCalle = value; }
        }

        private string domNumero;
        public string DomNumero
        {
            get { return this.domNumero; }
            set { this.domNumero = value; }
        }

        private string domPiso;
        public string DomPiso
        {
            get { return this.domPiso; }
            set { this.domPiso = value; }
        }

        private string domDpto;
        public string DomDpto
        {
            get { return this.domDpto; }
            set { this.domDpto = value; }
        }

        private string fechaNacimiento;
        public string FechaNacimiento
        {
            get { return this.fechaNacimiento; }
            set { this.fechaNacimiento = value; }
        }

        private bool habilitado;
        public bool Habilitado
        {
            get { return this.habilitado; }
            set { this.habilitado = value; }
        }

    }
}
