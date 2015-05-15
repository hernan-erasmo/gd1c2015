using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

            //if (loginCorrecto) then {
            
            menuPrincipal.asignarPadre(this);
            this.Hide();
            menuPrincipal.Show();
            //}
            //else
            //{(devolver un mensaje)}
        }

    }
}
