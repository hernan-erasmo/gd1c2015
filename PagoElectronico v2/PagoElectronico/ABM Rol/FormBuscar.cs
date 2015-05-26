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

            Utils.Herramientas.llenarListBoxRolesSistema(lbxRoles,rolDesc,funcionDesc);

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ABM_Rol.FormCrear frmCrear = new ABM_Rol.FormCrear(this);
            frmCrear.Show();
            this.Hide();
            
            //irol.Descripcion = txtRol.Text;
            //irol.Id = int.Parse(txtFuncion.Text);
            //irol.Habilitado = chkHabilitado.Checked;

            //for (int j = 1; j <= int.Parse(txtFuncion.Text); j++) 
            //{
            //    ItemRol irol = new ItemRol();
            //    irol.Descripcion = "Rol_" + j;
            //    irol.Id = j;
            //    irol.Habilitado = true;
            //        lbxRoles.Items.Add(irol);
            //    if(j%2==0)
            //        lbxRoles.SelectedItems.Add(irol);

            //}

            txtRol.Text = "";
            txtFuncion.Text = "";

//            lbxRoles.SelectedItems.Add
            //ListItem obj = new ListItem();
            //obj.Text  = Li.ToString();
            //obj.Value = Li.ToString();
            //PoListBox.Items.Add(obj);

            //comboSource.Add(int.Parse(txtFuncion.Text), txtRol.Text);
            //lbxRoles.DataSource = new BindingSource(comboSource, null);

        }


        private void lbxRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxRoles.SelectedItem != null)
            {
                lbxFunciones.Items.Clear();
                Utils.Herramientas.llenarListBoxFuncionesSistema(lbxFunciones, ((ItemRol)lbxRoles.SelectedItem).Id, "Buscar");
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

            //El indice del ultimo seleccionado
            Utils.Herramientas.msebox_informacion(lbxFunciones.SelectedIndex.ToString());
//            lbxFunciones.SelectedItems.Add
        }
    }
}
