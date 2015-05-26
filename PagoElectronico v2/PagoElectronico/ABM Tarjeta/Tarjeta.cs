using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagoElectronico.ABM_Tarjeta
{
    public class Tarjeta
    {
        private string numero;
        public string Numero
        {
            get
            {
                return numero;
            }
            set
            {
                numero = value;
            }
        }

        private string emisor;
        public string Emisor
        {
            get
            {
                return emisor;
            }
            set
            {
                emisor = value;
            }
        }

        private string codigoSeguridad;
        public string CodigoSeguridad
        {
            get
            {
                return codigoSeguridad;
            }
            set
            {
                codigoSeguridad = value;
            }
        }

        private string fechaEmision;
        public string FechaEmision
        {
            get
            {
                return fechaEmision;
            }
            set
            {
                fechaEmision = value;
            }
        }

        private string fechaVencimiento;
        public string FechaVencimiento
        {
            get
            {
                return fechaVencimiento;
            }
            set
            {
                fechaVencimiento = value;
            }
        }

    }
}
