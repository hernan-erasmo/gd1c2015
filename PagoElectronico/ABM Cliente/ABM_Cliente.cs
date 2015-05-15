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
    public partial class ABM_Cliente : Form
    {
        Form formPadre;
        AltaCliente altaCliente = new PagoElectronico.ABM_Cliente.AltaCliente();
        ModificacionBaja modificacionBajaCliente = new PagoElectronico.ABM_Cliente.ModificacionBaja();


        public void asignarPadre(Form padre)
        {
            this.formPadre = padre;
        }

        public ABM_Cliente()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            altaCliente.asignarPadre(this);
            this.Hide();
            altaCliente.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            modificacionBajaCliente.asignarPadre(this);
            this.Hide();
            modificacionBajaCliente.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.formPadre.Show();
        }

    }
}
