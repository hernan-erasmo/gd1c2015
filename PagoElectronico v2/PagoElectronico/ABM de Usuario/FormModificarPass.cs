using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Utils;
using System.Data.SqlClient;

namespace PagoElectronico.ABM_de_Usuario
{
    public partial class FormModificarPass : Form
    {
        Usuario usuario;
        Form formPadre;

        public FormModificarPass(Form f, Usuario user)
        {
            InitializeComponent();
            this.formPadre = f;
            this.usuario = user;
        }

        //  Boton X
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Dispose();
            formPadre.Show();
        }

        private void FormModificarPass_Load(object sender, EventArgs e)
        {
            label4.Text = usuario.Username;
        }


        private void btnCambiar_Click(object sender, EventArgs e)
        {
            if (txtPasswordNuevo.Text != "")
            {
                lblPassword.ForeColor = Color.Black;

                List<SqlParameter> lista = Herramientas.GenerarListaDeParametros("@usuario_id",
                        usuario.UsuarioId,
                        "@usuario_pass",
                        Herramientas.sha256_hash(txtPasswordNuevo.Text.ToString()));


                if(Herramientas.EjecutarStoredProcedure("SARASA.modificar_pass_usuario",lista) != null)
                {
                    this.Dispose();
                    formPadre.Show();
                }
            }
            else
                lblPassword.ForeColor = Color.Red;
        }

        //  Muestra información de la sesion
        private void lklInfoLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Herramientas.msebox_informacion(usuario.getInfo());
        }
    }
}
