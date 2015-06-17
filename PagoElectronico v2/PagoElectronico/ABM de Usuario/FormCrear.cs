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
    public partial class FormCrear : Form
    {

        Usuario usuario;
        Form formPadre;

        public FormCrear(Form f, Usuario user)
        {
            InitializeComponent();
            formPadre = f;
            this.usuario = user;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Herramientas.llenarComboBoxSP(cbxRol,
                  "SARASA.cbx_rol",
                  Herramientas.GenerarListaDeParametros("@usuario_id", 0),
                  true);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Dispose();
            formPadre.Show();

        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            bool usernameOK = false, passwordOK = false, preguntaOK = false, respuestaOK = false;

            if (txtUsuario.Text != "")
            {
                usernameOK = true;
                lblUsuario.ForeColor = Color.Black;
            }
            else 
            {
                usernameOK = false;
                lblUsuario.ForeColor = Color.Red;
            }

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


            if (usernameOK && passwordOK && preguntaOK && respuestaOK)
            {
                try
                {
                    List<SqlParameter> lista = Herramientas.GenerarListaDeParametros(
                        "@Usuario_Username",txtUsuario.Text,
                        "@Usuario_Password", Herramientas.sha256_hash(txtPassword.Text.ToString()),
                        "@Usuario_Pregunta_Sec", txtPreguntaSec.Text,
                        "@Usuario_Respuesta_Sec", Herramientas.sha256_hash(txtRespuestaSec.Text.ToString()),
                        "@Rol_Id", ((KeyValuePair<string, string>)cbxRol.SelectedItem).Key);

                    if (Herramientas.EjecutarStoredProcedure("SARASA.crear_usuario", lista) == null)
                    { 
                    }
                    else
                    {
                        this.Dispose();
                        formPadre.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                }
            }
        }
    }
}
