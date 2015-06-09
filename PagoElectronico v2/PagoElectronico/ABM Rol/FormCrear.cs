using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Utils;

namespace PagoElectronico.ABM_Rol
{
    public partial class FormCrear : Form
    {
        Form formPadre;

        public FormCrear(Form padre)
        {
            InitializeComponent();
            formPadre = padre;
        }

        private void FormCrear_Load(object sender, EventArgs e)
        {
            lbxFunciones.DisplayMember = "Descripcion";
            lbxFunciones.ValueMember = "Id";
            Herramientas.llenarListBoxFuncionesSistema(lbxFunciones,0,"Crear");
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string nombreRol = txtRol.Text;
            int habilitado = 0;
            if (chkHabilitado.Checked)
                habilitado = 1;

            string listIds = "";
            foreach (Object item in lbxFunciones.SelectedItems)
                listIds += ((ItemFuncion)item).Id  + ",";


            Herramientas.crearRol(nombreRol, habilitado, listIds);

            Herramientas.msebox_informacion("Rol: " + nombreRol + " (Habilitado= "+habilitado+")");
        }

    }
}
