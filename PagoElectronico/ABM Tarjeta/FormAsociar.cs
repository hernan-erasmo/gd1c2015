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
    public partial class FormAsociar : Form
    {
        Form padre;

        public FormAsociar()
        {
            InitializeComponent();
        }

        public FormAsociar(Form f, string cliente)
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

        //  Asociar
        private void FormAsociar_Load(object sender, EventArgs e)
        {
            txtCliente.Enabled = false;
        }

        //  Buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
          
        }

        //  Asociar: Ejecutar SP asociarTarjeta(idcliente, demas_parametros)
        private void btnAsociar_Click(object sender, EventArgs e)
        {
            Utils.Herramientas.msebox_informacion("Se asocio la nueva tarjeta al cliente " + txtCliente.Text);
            txtCodSeguridad.Text = "";
            txtNumero.Text = "";
            txtFechaEmision.Text = "";
            txtFechaVencimiento.Text = "";
            cbxEmisor.Text = "";
        }


        private void txtCliente_TextChanged(object sender, EventArgs e)
        {

        }

        //  Volver
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            this.padre.Show();
        }

    }
}
