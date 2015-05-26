using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Rol
{
    public partial class FormModificar : Form
    {
        Form formPadre;
        ItemRol itemRol;

        public FormModificar(Form padre, ItemRol itemRol)
        {
            InitializeComponent();
            this.formPadre = padre;
            this.itemRol = itemRol;
        }

        private void FormModificar_Load(object sender, EventArgs e)
        {
            txtRol.Text = itemRol.Descripcion;
            chkHabilitado.Checked =itemRol.Habilitado;

            Utils.Herramientas.llenarListBoxFuncionesSistema(lbxFunciones, itemRol.Id, "Modificar");
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombreRol = txtRol.Text;
            int habilitado = 0;
            if (chkHabilitado.Checked)
                habilitado = 1;

            string listIds = "";
            foreach (Object item in lbxFunciones.SelectedItems)
                listIds += ((ItemFuncion)item).Id + ",";


            Utils.Herramientas.actualizarRol(itemRol.Id,nombreRol, habilitado, listIds);

            Utils.Herramientas.msebox_informacion("Rol: " + nombreRol + " (Habilitado= " + habilitado + ")");
        }
    }
}
