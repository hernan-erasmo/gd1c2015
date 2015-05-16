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
        MenuPrincipal menuPrincipal = new PagoElectronico.MenuPrincipal();

        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           Utilidades.llenarCombos rol = new PagoElectronico.Utilidades.llenarCombos();
           rol.llenarComboRol(comboBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Login

            ABM_de_Usuario.Usuario usuario = new PagoElectronico.ABM_de_Usuario.Usuario();

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Por favor, completar la informacion", "Iniciar sesion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                usuario.Username = this.textBox1.Text;
                usuario.Password = this.textBox2.Text;
                usuario.Rol = this.comboBox1.Text;
                if (usuario.Buscar() == true)
                {
                    menuPrincipal.asignarPadre(this);
                    this.Hide();
                    menuPrincipal.Show();
                    MessageBox.Show(usuario.Mensaje, "Iniciar sesion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(usuario.Mensaje, "Iniciar sesion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        
        }

    }
}
