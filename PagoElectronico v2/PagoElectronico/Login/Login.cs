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

namespace PagoElectronico.Login
{
    public partial class Login : Form
    {
        int idProcesoLogin;
        MenuPrincipal menuPrincipal;
        public Usuario usuario;

        public Login(MenuPrincipal menuPrincipal)
        {
            InitializeComponent();
            this.menuPrincipal = menuPrincipal;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            idProcesoLogin = 0;
            gbPermisos.Enabled = false;
            gbPermisos.Visible = false;
        }

        //  Login
        private void button1_Click_1(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Black;
            label2.ForeColor = Color.Black;
            lblInfo.Text = "";

            if (idProcesoLogin == 0)
            {
                //  Validacion datos del formulario
                if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))// || string.IsNullOrEmpty(comboBox1.Text))
                {
                    label1.ForeColor = Color.Red;
                    label2.ForeColor = Color.Red;
                    lblInfo.Text = "Completar todos datos del formulario";
                }
                else
                {
                    //  Objeto usuario, con la informacion de la sesion
                    usuario = new Utils.Usuario();

                    //  Carga la informacion en usuario
                    usuario.Username = this.textBox1.Text;
                    usuario.Password = Herramientas.sha256_hash(textBox2.Text);

                    Herramientas.ejecutarAutenticacion(usuario);

                    switch (usuario.CodLogin)
                    {
                        case 0: //  Autenticacion correcta
                            {

                                idProcesoLogin = 1;
                                gbLogin.Enabled = false;    //Bloquea los datos del usuario

                                //  Busca los roles del usuario para que elija uno
                                Herramientas.llenarComboBoxSP(comboBox1, 
                                    "SARASA.cbx_rol", 
                                    Herramientas.GenerarListaDeParametros("@usuarioId", usuario.UsuarioId), 
                                    true);
                                gbPermisos.Visible = true;
                                gbPermisos.Enabled = true;
                                btnLogin.Text = "Continuar..";
                                break;
                            }
                        case -1: //  Usuario o password invalido
                            {
                                //Console.WriteLine("Case 1");
                                label1.ForeColor = Color.Red;
                                label2.ForeColor = Color.Red;
                                lblInfo.Text = "Usuario o password incorrecto";
                                break;
                            }
                        case -2: //  Usuario inhabilitado
                            {
                                lblInfo.Text = "Usuario inhabilitado";
                                break;
                            }
                        default: // Autenticacion correcta, con rol unico
                            {
                                Herramientas.cargarFunciones(usuario);
                                menuPrincipal.asignarPadre(this);
                                menuPrincipal.asignarUsuario(usuario);
                                this.Hide();
                                menuPrincipal.Show();

                                break;
                            }
                    }
                }
            }
            else //    Etapa final de login, si tiene más de un rol, muestra el combo
            { 
                // Carga las funciones del rol seleccionado
                usuario.Rol = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Value;
                usuario.RolId = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;

                Herramientas.cargarFunciones(usuario);
                menuPrincipal.asignarPadre(this);
                menuPrincipal.asignarUsuario(usuario);
                this.Hide();
                menuPrincipal.Show();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            gbPermisos.Enabled = false;
            gbPermisos.Visible = false;
            gbLogin.Enabled = true;
            textBox1.Text = "";
            textBox2.Text = "";
            idProcesoLogin = 0;
            btnLogin.Text = "Login";
        }
    }
}
