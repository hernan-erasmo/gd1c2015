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
    public partial class FormModificar : Form
    {
        Form padre;

        public FormModificar()
        {
            InitializeComponent();
        }

        public FormModificar(Form f, string cliente)
        {
            InitializeComponent();
            padre = f;
            txtCliente.Text = cliente;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormModificar_Load(object sender, EventArgs e)
        {
            txtCliente.Enabled = false;

            //  Llena el combo de emisor
            cbxEmisor.Text = "";
            cbxEmisor.Items.Clear();//VACIA LOS ELEMENTOS DEL COMBO
            Utils.Herramientas.llenarComboBox(cbxEmisor, "SELECT * FROM test.EmisorTC");

        }

        //  Buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
          
        }

        //  Asociar: Ejecutar SP asociarTarjeta(idcliente, demas_parametros)
        private void btnAsociar_Click(object sender, EventArgs e)
        {
            Utils.Herramientas.msebox_informacion("Se modificaron los datos de la tarjeta del cliente " + txtCliente.Text);
            this.Close();
            this.padre.Show();
        }

        //  Volver
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            this.padre.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
