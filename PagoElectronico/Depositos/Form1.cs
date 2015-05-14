using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Depositos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //  Vuelve al menu principal
        private void button1_Click(object sender, EventArgs e)
        {
            PagoElectronico.MenuPrincipal frmMenu = new PagoElectronico.MenuPrincipal();
            this.Hide();
            frmMenu.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
