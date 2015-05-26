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
    public partial class FormBuscarClie : Form
    {
        ABM_Tarjeta.FormBuscar formPadre;

        public FormBuscarClie()
        {
            InitializeComponent();
        }

        public FormBuscarClie(ABM_Tarjeta.FormBuscar f)
        {
            InitializeComponent();
            formPadre = f;
        }


        private void FormBuscarClie_Load(object sender, EventArgs e)
        {

        }

        //  VOLVER
        private void button1_Click(object sender, EventArgs e)
        {
            formPadre.Show();
            this.Close();

        }


        //  ACEPTAR
        private void button2_Click(object sender, EventArgs e)
        {
            formPadre.setClienteTexto(textBox1.Text);
            formPadre.Show();
            this.Close();

        }
    }
}
