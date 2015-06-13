using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagoElectronico.Utils
{
    public class ItemRol
    {
        private int id;
        private string descripcion;
        private bool habilitado;
        private string etiqueta;

        public ItemRol(int id, bool habilitado, string descripcion, string etiqueta)
        {
            this.id = id;
            this.descripcion = descripcion;
            this.habilitado = habilitado;
            this.etiqueta = etiqueta;
        }

        public ItemRol()
        {
            
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

        public bool Habilitado
        {
            get { return this.habilitado; }
            set { this.habilitado = value; }
        }

        public string Etiqueta
        {
            get { return this.etiqueta; }
            set { this.etiqueta = value; }
        }
    }
}
