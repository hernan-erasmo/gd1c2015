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
        Tarjeta tarjeta = new Tarjeta();

        public FormModificar()
        {
            InitializeComponent();
        }

        public FormModificar(Form f, string cliente, Tarjeta t)
        {
            InitializeComponent();
            padre = f;
            txtCliente.Text = cliente;

            tarjeta = t;

            txtCodSeguridad.Text = tarjeta.CodigoSeguridad;
//            txtFechaEmision.Text = tarjeta.FechaEmision;
//            txtFechaVencimiento.Text = tarjeta.FechaVencimiento;
            txtNumero.Text = tarjeta.Numero;
            cbxEmisor.Text = tarjeta.Emisor;

            dtpFechaEmision.Value = DateTime.Parse(tarjeta.FechaEmision);
            dtpFechaVencimiento.Value = DateTime.Parse(tarjeta.FechaVencimiento);

            //tarjeta.Numero = t.Numero;
            //tarjeta.CodigoSeguridad = t.CodigoSeguridad;
            //tarjeta.Emisor = t.Emisor;
            //tarjeta.FechaEmision = t.FechaEmision;
            //tarjeta.FechaVencimiento = t.FechaVencimiento;



            
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
            cbxEmisor.Items.Clear();//VACIA LOS ELEMENTOS DEL COMBO
            Utils.Herramientas.llenarComboBox(cbxEmisor, "SELECT * FROM test.EmisorTC");

        }

        //  Buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
          
        }


        //  Volver
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            this.padre.Show();
        }

/*
        private void btnModificar_Click(object sender, EventArgs e)
        {
            //  RECUPERA LOS VALORES Y LOS PASA COMO PARAMETROS
            if (dtpFechaEmision.Value.ToShortDateString().Equals(dtpFechaVencimiento.Value.ToShortDateString()))
            {
                Utils.Herramientas.msebox_informacion("Existen valores inválidos: " + dtpFechaEmision.Value.ToShortTimeString() + "=" + dtpFechaVencimiento.Value.ToShortTimeString());
            }
            else
            {   //  Se pudo grabar la tarjeta

                //  EJECUTA EL STORE PROCEDURE QUE GRABA LOS DATOS EN LA TABLA
                string nombreSP = "Test.Modificar_Tarjeta";    //  Nombre del StoreProcedure

                try
                {
                    List<SqlParameter> lista = Utils.Herramientas.GenerarListaDeParametros(
                        "@tarjetaId", "PRUEBA",     //Numero de tarjeta
                        "@tarjetaNumero", txtNumero.Text,
                        "@tarjetaFechaEmision", dtpFechaEmision.Value.ToShortDateString(),
                        "@tarjetaFechaVencimiento", dtpFechaVencimiento.Value.ToShortDateString(),
                        "@tarjetaCodigoSeg", txtCodSeguridad.Text,
                        "@tarjetaEmisorDescripcion", cbxEmisor.Text);

                    Utils.Herramientas.EjecutarStoredProcedure(nombreSP, lista);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                }

                String msj = "EXEC " + nombreSP + " ";
                msj += "@tarjetaId = nnnn, ";
                msj += "@tarjetaNumero = '" + txtNumero.Text + "', ";
                msj += "@tarjetaFechaEmision = '" + dtpFechaEmision.Value.ToShortDateString() + "', ";
                msj += "@tarjetaFechaVencimiento = '" + dtpFechaVencimiento.Value.ToShortDateString() + "', ";
                msj += "@tarjetaCodigoSeg = '" + txtCodSeguridad.Text + "', ";
                msj += "@tarjetaEmisorDescripcion = '" + cbxEmisor.Text + "'";

                Utils.Herramientas.msebox_informacion(msj);

                txtCodSeguridad.Text = "";
                txtNumero.Text = "";
                cbxEmisor.Text = "";
                dtpFechaEmision.Value = DateTime.Now;
                dtpFechaVencimiento.Value = DateTime.Now;
            }




            //            dtpFechaEmision.Value = DateTime.Parse("12/05/1988");
            //            dtpFechaVencimiento.Value = DateTime.Parse("2015-12-05 00:00:00.000");
            //  Se recuperan todos los valores de formulario
            //  Se envian los parametros juntos co
            Utils.Herramientas.msebox_informacion("Se modificaron los datos de la tarjeta del cliente " + txtCliente.Text);
            this.Close();
            this.padre.Show();
        }

*/    }
}
