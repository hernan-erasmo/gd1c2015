using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Cliente
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Envia al formulario principal el usuario ingresado


            Form1 frmLogin = new Form1();
            //      AltaUsuario formAltaUsuario = new AltaUsuario();

            this.Close();
            //formAltaUsuario.Show();
            frmLogin.Show();
        }
    }
}
