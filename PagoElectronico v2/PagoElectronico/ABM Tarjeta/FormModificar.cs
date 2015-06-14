using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using PagoElectronico.Utils;

namespace PagoElectronico.ABM_Tarjeta
{
    public partial class FormModificar : Form
    {
        Form formPadre;
        Tarjeta tarjeta;

        public FormModificar()
        {
            InitializeComponent();
        }

        public FormModificar(Form f, string cliente, Tarjeta t)
        {
            InitializeComponent();
            formPadre = f;
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

        //  Boton X
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Dispose();
            formPadre.Show();
        }

        private void FormModificar_Load(object sender, EventArgs e)
        {
            txtCliente.Enabled = false;

            //  Llena el combo de emisor
            cbxEmisor.Items.Clear();//VACIA LOS ELEMENTOS DEL COMBO
            Herramientas.llenarComboBoxSP(cbxEmisor, "SARASA.cbx_emisor", null, true);
            cbxEmisor.Text = tarjeta.Emisor;
        }

        //  Buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
          
        }


        //  Volver
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            this.formPadre.Show();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dtpFechaEmision.Value.ToShortDateString().Equals(dtpFechaVencimiento.Value.ToShortDateString()))
            {// EMISION Y VENCIMIENTO IGUALES, ERROR AL GUARDAR
                Utils.Herramientas.msebox_informacion("Existen valores inválidos: " + dtpFechaEmision.Value.ToShortTimeString() + "=" + dtpFechaVencimiento.Value.ToShortTimeString());
            }
            else
            {   //  Se pudo grabar la tarjeta

                //  EJECUTA EL STORE PROCEDURE QUE GRABA LOS DATOS EN LA TABLA
                string nombreSP = "Test.Modificar_Tarjeta";    //  Nombre del StoreProcedure

                try
                {
                    List<SqlParameter> lista = Utils.Herramientas.GenerarListaDeParametros(
                        "@tarjetaId", tarjeta.Numero,
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

                Utils.Herramientas.msebox_informacion("Datos Guardados");

                txtCodSeguridad.Text = "";
                txtNumero.Text = "";
                cbxEmisor.Text = "";
                dtpFechaEmision.Value = DateTime.Now;
                dtpFechaVencimiento.Value = DateTime.Now;
            }
        }
    }
}
