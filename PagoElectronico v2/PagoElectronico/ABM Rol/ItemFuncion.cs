using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagoElectronico.ABM_Rol
{
    public class ItemFuncion
    {
        private int id;
        private string descripcion;
        private bool seleccionado;

        public ItemFuncion(int id, string descripcion, int seleccionado)
        {
            this.id = id;
            this.descripcion = descripcion;
            if (seleccionado == 0)
                this.seleccionado = false;
            else
                this.seleccionado = true;
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Descripcion
        {
            get { return this.descripcion; }
            set { this.descripcion = value; }
        }

        public bool Seleccionado
        {
            get { return this.seleccionado; }
            set { this.seleccionado = value; }
        }
    }
}
