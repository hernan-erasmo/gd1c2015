using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico.Login
{
    public partial class Login : Form
    {
        MenuPrincipal menuPrincipal;
        public Utils.Usuario usuario;

        public Login(MenuPrincipal menuPrincipal)
        {
            InitializeComponent();
            this.menuPrincipal = menuPrincipal;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //  Crea objeto usuario, con la informacion de la sesion
            usuario = new Utils.Usuario();

            comboBox1.Enabled = false;
            label3.Enabled = false;
        }

        //  Login
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (usuario.Logueado)
            {
                //acá cargamos el usuario con las funciones adecuadas para el rol que eligió
                //Utils.Herramientas.cargarFunciones(usuario);

                //y pasamos al form principal
                menuPrincipal.asignarPadre(this);
                menuPrincipal.asignarUsuario(usuario);
                this.Hide();
                menuPrincipal.Show();
                
                return;
            }
            
            //  Valida que los datos de autenticacion no esten vacios
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Por favor, completar la informacion", "Iniciar sesion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //  Carga la informacion en usuario
            usuario.Username = this.textBox1.Text;
            usuario.Password = this.textBox2.Text;
            
            Utils.Herramientas.ejecutarAutenticacion(usuario);

            switch (usuario.CodLogin)
            {
                case 0: //  Usuario o password invalido
                {
                    MessageBox.Show("Usuario o password incorrecto", "Iniciar sesion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                default:
                    if (usuario.Habilitado == false)
                    {
                        MessageBox.Show("Usuario inhabilitado", "Iniciar sesion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        usuario.Logueado = false;
                    } else {
                        //habilitamos el combo de rol y lo cargamos con los roles asignados a este usuario.
                        //cambiamos el texto del boton de "Login" a "Usar el rol seleccionado"    
                        comboBox1.Enabled = true;
                        label3.Enabled = true;
                        button1.Text = "Usar el rol seleccionado";

                        String query_roles_para_usuario = "" +
                            "SELECT rol.Rol_Descripcion " +
                            "FROM SARASA.Usuario usu " +
                            "INNER JOIN SARASA.Rol_x_Usuario rxu ON rxu.Usuario_Id = usu.Usuario_Id " +
                            "INNER JOIN SARASA.Rol rol ON rol.Rol_Id = rxu.Rol_Id " +
                            "WHERE usu.Usuario_Id = " + usuario.UsuarioId.ToString();
                        Utils.Herramientas.llenarComboBox(comboBox1, query_roles_para_usuario, true);
                    }
                break;
            }

        }

        //  Abre el ABM de Usuario-Cliente
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
