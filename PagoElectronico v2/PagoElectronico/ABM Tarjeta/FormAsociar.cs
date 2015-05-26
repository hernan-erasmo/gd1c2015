using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

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
            dtpFechaEmision.Value = DateTime.Now;
            dtpFechaVencimiento.Value = DateTime.Now;

            //  Llena el combo de emisor
            cbxEmisor.Items.Clear();//VACIA LOS ELEMENTOS DEL COMBO
            Utils.Herramientas.llenarComboBox(cbxEmisor, "SELECT * FROM test.EmisorTC");
        }

        //  Asociar: Ejecutar SP asociarTarjeta(idcliente, demas_parametros)
        private void btnAsociar_Click(object sender, EventArgs e)
        {

            if (dtpFechaEmision.Value.ToShortDateString().Equals(dtpFechaVencimiento.Value.ToShortDateString()))
            {// EMISION Y VENCIMIENTO IGUALES, ERROR AL GUARDAR
                Utils.Herramientas.msebox_informacion("Existen valores inválidos: " + dtpFechaEmision.Value.ToShortTimeString() + "=" + dtpFechaVencimiento.Value.ToShortTimeString());
            }
            else
            {   //  Se pudo grabar la tarjeta

                //  EJECUTA EL STORE PROCEDURE QUE GRABA LOS DATOS EN LA TABLA
                string nombreSP = "Test.Asociar_Tarjeta";    //  Nombre del StoreProcedure

                try
                {
                    List<SqlParameter> lista = Utils.Herramientas.GenerarListaDeParametros(
                        "@clienteId", txtCliente,
                        "@tarjetaNumero", txtNumero.Text,
                        "@tarjetaFechaEmision", dtpFechaEmision.Value.ToShortDateString(),
                        "@tarjetaFechaVencimiento", dtpFechaVencimiento.Value.ToShortDateString(),
                        "@tarjetaCodigoSeg", txtCodSeguridad.Text,
                        "@tarjetaEmisorDescripcion", cbxEmisor.Text,
                        "@tarjetaUltimosCuatro", Utils.Herramientas.stringRight(txtNumero.Text, 4));

                       Utils.Herramientas.EjecutarStoredProcedure(nombreSP, lista);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                }
                
                String msj = "Nueva Tarjetan\n";
                msj += "@clienteId = 'PRUEBA'\n";
                msj += "@tarjetaNumero = '" + txtNumero.Text + "'\n";
                msj += "@tarjetaFechaEmision = '" + dtpFechaEmision.Value.ToShortDateString() + "'\n";
                msj += "@tarjetaFechaVencimiento = '" + dtpFechaVencimiento.Value.ToShortDateString() + "'\n";
                msj += "@tarjetaCodigoSeg = '" + txtCodSeguridad.Text + "'\n";
                msj += "@tarjetaEmisorDescripcion = '" + cbxEmisor.Text + "'\n";

                Utils.Herramientas.msebox_informacion(msj);

                txtCodSeguridad.Text = "";
                txtNumero.Text = "";
                cbxEmisor.Text = "";
                dtpFechaEmision.Value = DateTime.Now;
                dtpFechaVencimiento.Value = DateTime.Now;
            }




//            dtpFechaEmision.Value = DateTime.Parse("12/05/1988");
//            dtpFechaVencimiento.Value = DateTime.Parse("2015-12-05 00:00:00.000");

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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cbxEmisor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtpFechaVencimiento_ValueChanged(object sender, EventArgs e)
        {

        }

    }
}
