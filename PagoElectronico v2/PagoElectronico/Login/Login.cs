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
        MenuPrincipal menuPrincipal;
        public Utils.Usuario usuario;

        public Login(MenuPrincipal menuPrincipal)
        {
            InitializeComponent();
            this.menuPrincipal = menuPrincipal;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Herramientas.llenarComboBoxSP(comboBox1, "SARASA.cbx_rol", true);
        }

        //  Login
        private void button1_Click_1(object sender, EventArgs e)
        {
            //  Crea objeto usuario, con la informacion de la sesion
            usuario = new Utils.Usuario();

            //  Valida que los datos de autenticacion no esten vacios
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Por favor, completar la informacion", "Iniciar sesion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //  Carga la informacion en usuario
                usuario.Username = this.textBox1.Text;
                usuario.Password = this.textBox2.Text;
                usuario.Rol = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Value;
                usuario.RolId = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;

                //dataGridView1.DataSource = Utils.Herramientas.ejecutarAutenticacion(usuario);
                Utils.Herramientas.ejecutarAutenticacion(usuario);

                switch (usuario.CodLogin)
                {
                    case 0: //  Autenticacion correcta
                    {
                        Console.WriteLine("Sesion iniciada (user: " + usuario.Username + "");

                        Utils.Herramientas.cargarFunciones(usuario);

                        menuPrincipal.asignarPadre(this);
                        menuPrincipal.asignarUsuario(usuario);
                        this.Hide();
                        menuPrincipal.Show();
                        break;
                    }
                    case 1: //  Usuario o password invalido
                    {
                        //Console.WriteLine("Case 1");
                        MessageBox.Show("Usuario o password incorrecto", "Iniciar sesion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    case 2: //  Usuario inhabilitado
                    {
                        MessageBox.Show("Usuario inhabilitado", "Iniciar sesion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                    default:
                        break;
                }

            }

        }

        //  Abre el ABM de Usuario-Cliente
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
