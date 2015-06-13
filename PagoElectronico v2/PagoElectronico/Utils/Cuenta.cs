using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagoElectronico.Utils
{
    public class Cuenta
    {
        private string desCliente;
        private string numero;
        private string fechaApertura;
        private string fechaCierre;
        private int idCliente;
        private int idPais;
        private int idTipo;
        private int idMoneda;
        private int idEstado;
        private bool deudora;

        //Constructor
        public Cuenta()
        {
            this.desCliente = string.Empty;
            this.numero = string.Empty;
            this.fechaApertura = string.Empty;
            this.fechaCierre = string.Empty;
        }

        //Propiedades
        public string DesCliente
        {
            get { return this.desCliente; }
            set { this.desCliente = value; }
        }

        public string Numero
        {
            get { return this.numero; }
            set { this.numero = value; }
        }

        public string FechaApertura
        {
            get { return this.fechaApertura; }
            set { this.fechaApertura = value; }
        }

        public string FechaCierre
        {
            get { return this.fechaCierre; }
            set { this.fechaCierre = value; }
        }

        public int IdCliente
        {
            get { return this.idCliente; }
            set { this.idCliente = value; }
        }

        public int IdPais
        {
            get { return this.idPais; }
            set { this.idPais = value; }
        }

        public int IdTipo
        {
            get { return this.idTipo; }
            set { this.idTipo = value; }
        }

        public int IdMoneda
        {
            get { return this.idMoneda; }
            set { this.idMoneda = value; }
        }

        public int IdEstado
        {
            get { return this.idEstado; }
            set { this.idEstado = value; }
        }

        public bool Deudora
        {
            get { return this.deudora; }
            set { this.deudora = value; }
        }
    }
}
