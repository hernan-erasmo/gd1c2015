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
    public partial class FormAsociar : Form
    {
        Form formPadre;
        Usuario usuario;

        public FormAsociar()
        {
            InitializeComponent();
        }

        public FormAsociar(Form f, string cliente, Usuario user)
        {
            InitializeComponent();
            formPadre = f;
            txtCliente.Text = cliente;
            usuario = user;
        }

        //  Boton X
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Dispose();
            formPadre.Show();
        }

        //  Asociar
        private void FormAsociar_Load(object sender, EventArgs e)
        {
            txtCliente.Enabled = false;
            dtpFechaEmision.Value = DateTime.Now;
            dtpFechaVencimiento.Value = DateTime.Now;

            //  Llena el combo de emisor
            cbxEmisor.Items.Clear();//VACIA LOS ELEMENTOS DEL COMBO
            Herramientas.llenarComboBoxSP(cbxEmisor,"SARASA.cbx_emisor",null,true);
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
                string nombreSP = "SARASA.Asociar_Tarjeta";    //  Nombre del StoreProcedure

                try
                {
                    List<SqlParameter> lista = Utils.Herramientas.GenerarListaDeParametros(
                        "@cliente_id", this.usuario.ClienteId,
                        "@tc_num", Convert.ToString(txtNumero.Text),
                        "@tc_ultimoscuatro", Convert.ToString(Utils.Herramientas.stringRight(txtNumero.Text, 4)),
                        "@tc_emision", dtpFechaEmision.Value.ToShortDateString(),
                        "@tc_vencimiento", dtpFechaVencimiento.Value.ToShortDateString(),
                        "@tc_codseg", Convert.ToString(txtCodSeguridad.Text),
                        "@tc_emisor", Convert.ToString(cbxEmisor.Text));

                        Utils.Herramientas.EjecutarStoredProcedure(nombreSP, lista);
                        Utils.Herramientas.msebox_informacion("Tarjeta asociada con éxito");
                        this.Hide();
                        (this.formPadre).Show();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                }
                
                /*String msj = "Nueva Tarjetan\n";
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
                dtpFechaVencimiento.Value = DateTime.Now;*/
            }




//            dtpFechaEmision.Value = DateTime.Parse("12/05/1988");
//            dtpFechaVencimiento.Value = DateTime.Parse("2015-12-05 00:00:00.000");

        }



        //  Volver
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            this.formPadre.Show();
        }
    }
}
