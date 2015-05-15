using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Tarjeta
{
    public partial class ABM_Tarjetas : Form
    {
        Form formPadre;
        AsociarNuevaTarjeta asociarTarjeta = new PagoElectronico.ABM_Tarjeta.AsociarNuevaTarjeta();
        ModificarTarjeta modificarTarjeta = new PagoElectronico.ABM_Tarjeta.ModificarTarjeta();

        public void asignarPadre(Form padre)
        {
            this.formPadre = padre;
        }

        public ABM_Tarjetas()
        {
            InitializeComponent();
        }

        private void ABM_Tarjetas_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.formPadre.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            asociarTarjeta.asignarPadre(this);
            this.Hide();
            asociarTarjeta.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            modificarTarjeta.asignarPadre(this);
            this.Hide();
            asociarTarjeta.Show();
        }
    }
}
