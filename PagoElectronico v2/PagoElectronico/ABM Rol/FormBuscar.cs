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
    public partial class FormBuscar : Form
    {
        Utils.Usuario usuario;
        Form formPadre;
        ModificarEliminarRol modificarEliminarRol = new PagoElectronico.ABM_Rol.ModificarEliminarRol();

        Dictionary<int, string> comboSource = new Dictionary<int, string>();

        public void asignarPadre(Form padre)
        {
            this.formPadre = padre;
        }

        public FormBuscar(Form padre, Utils.Usuario user)
        {
            InitializeComponent();
            formPadre = padre;
            usuario = user;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (usuario.Funciones.Contains("AgregarRol"))
                btnAgregar.Visible = true;
            else
                btnAgregar.Visible = false;

            if (usuario.Funciones.Contains("ModificarRol"))
                btnModificar.Visible = true;
            else
                btnModificar.Visible = false;


            if (usuario.Funciones.Contains("EliminarRol"))
                btnEliminar.Visible = true;
            else
                btnEliminar.Visible = false;

        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            formPadre.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            modificarEliminarRol.asignarPadre(this);
            this.Hide();
            modificarEliminarRol.Show();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            formPadre.Show();
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            lbxRoles.ClearSelected();
            lbxRoles.Items.Clear();
            lbxFunciones.ClearSelected();
            lbxFunciones.Items.Clear();

            string rolDesc = "", funcionDesc="";

            if (txtRol.Text != "")
                rolDesc = txtRol.Text;
            if(txtFuncion.Text != "")
                funcionDesc = txtFuncion.Text;

            Herramientas.llenarListBoxRolesSistema(lbxRoles,rolDesc,funcionDesc);

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ABM_Rol.FormCrear frmCrear = new ABM_Rol.FormCrear(this);
            frmCrear.Show();
            this.Hide();
            
            txtRol.Text = "";
            txtFuncion.Text = "";
        }


        private void lbxRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxRoles.SelectedItem != null)
            {
                lbxFunciones.Items.Clear();
                Herramientas.llenarListBoxFuncionesSistema(lbxFunciones, ((ItemRol)lbxRoles.SelectedItem).Id, "Buscar");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ABM_Rol.FormModificar frmModificar = new ABM_Rol.FormModificar(this,(ItemRol)lbxRoles.SelectedItem);
            frmModificar.Show();
            this.Hide();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Herramientas.eliminarRol(((ItemRol)lbxRoles.SelectedItem).Id);
            Herramientas.msebox_informacion(lbxFunciones.SelectedIndex.ToString());
        }
    }
}
