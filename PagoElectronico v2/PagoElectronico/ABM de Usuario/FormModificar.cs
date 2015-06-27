using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using PagoElectronico.Utils;

namespace PagoElectronico.ABM_de_Usuario
{
    public partial class FormModificar : Form
    {
        int id;
        Form formPadre;
        string nombre_usuario;

        public FormModificar(Form frm, int user, string nombre)
        {
            InitializeComponent();
            this.formPadre = frm;
            this.id = user;
            this.nombre_usuario = nombre;
        }

        private void FormModificar_Load(object sender, EventArgs e)
        {
            this.txtUsuario.Text = nombre_usuario;
            txtUsuario.Enabled = false;

            lbxRoles.ClearSelected();
            lbxRoles.Items.Clear();
            string rolDesc = "", funcionDesc = "";
            Utils.Herramientas.llenarListBoxRolesSistema(lbxRoles, rolDesc, funcionDesc);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            formPadre.Show();
            this.Dispose();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int hab;
            if(checkBox1.Checked)
            {hab = 1;}
            else
            {hab = 0;}
            bool passwordOK = false, preguntaOK = false, respuestaOK = false;

            if (txtPassword.Text != "")
            {
                passwordOK = true;
                lblPassword.ForeColor = Color.Black;
            }
            else
            {
                passwordOK = false;
                lblPassword.ForeColor = Color.Red;
            }


            if (txtPreguntaSec.Text != "")
            {
                preguntaOK = true;
                lblPregunta.ForeColor = Color.Black;
            }
            else
            {
                preguntaOK = false;
                lblPregunta.ForeColor = Color.Red;
            }


            if (txtRespuestaSec.Text != "")
            {
                respuestaOK = true;
                lblRespuesta.ForeColor = Color.Black;
            }
            else
            {
                respuestaOK = false;
                lblRespuesta.ForeColor = Color.Red;
            }

            
            if (passwordOK && preguntaOK && respuestaOK)
            {
                if(lbxRoles.SelectedItems.Count!=0)
                {

                List<SqlParameter> lista = Herramientas.GenerarListaDeParametros(
                    "@usuario_id", this.id,
                    "@pass", Herramientas.sha256_hash(txtPassword.Text.ToString()),
                    "@preg", txtPreguntaSec.Text,
                    "@resp", Herramientas.sha256_hash(txtRespuestaSec.Text.ToString()),
                    "@hab", hab);
                Herramientas.EjecutarStoredProcedure("SARASA.modificar_usuario", lista);

                foreach (Utils.ItemRol item in lbxRoles.SelectedItems)
                {
                    lista = Herramientas.GenerarListaDeParametros(
                    "@usuario_id", this.id,
                    "@rol", item.Id);
                    Herramientas.EjecutarStoredProcedure("SARASA.insertar_rol_usuario", lista);

                }

                Herramientas.msebox_informacion("Usuario modificado con éxito");
                this.Dispose();
                this.formPadre.Show();
                }
                else
                {
                    Herramientas.msebox_informacion("Debe seleccionar al menos un rol");
                }
            }
            else
            {
                Herramientas.msebox_informacion("Completar todos los campos");
            }
        }

        private void lbxRoles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
