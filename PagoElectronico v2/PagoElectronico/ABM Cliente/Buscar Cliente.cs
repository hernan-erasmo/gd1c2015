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
    public partial class ModificacionBaja : Form
    {
        Form formPadre;
        ModificarCliente modificarCliente = new PagoElectronico.ABM_Cliente.ModificarCliente();

        public void asignarPadre(Form padre)
        {
            this.formPadre = padre;
        }

        public ModificacionBaja()
        {
            InitializeComponent();
        }

        private void ModificacionBaja_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.formPadre.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            modificarCliente.asignarPadre(this);
            this.Hide();
            modificarCliente.Show();
        }
    }
}
