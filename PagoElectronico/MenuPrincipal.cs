using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace PagoElectronico
{
    public partial class MenuPrincipal : Form
    {
        Form formPadre;
        ABM_Cliente.ABM_Cliente abmCliente = new PagoElectronico.ABM_Cliente.ABM_Cliente();
        ABM_Rol.ABM_Rol abmRol = new PagoElectronico.ABM_Rol.ABM_Rol();
        ABM_Tarjeta.ABM_Tarjetas abmTarjetas = new PagoElectronico.ABM_Tarjeta.ABM_Tarjetas();
        


        public MenuPrincipal()
        {
            InitializeComponent();
        }

        public void asignarPadre(Form padre)
        {
            this.formPadre = padre;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            abmCliente.asignarPadre(this);
            this.Hide();
            abmCliente.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            abmTarjetas.asignarPadre(this);
            this.Hide();
            abmTarjetas.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            abmRol.asignarPadre(this);
            this.Hide();
            abmRol.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            this.formPadre.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
