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
        string clienteId = "0";
        string clienteDesc = "";

        public FormAsociar()
        {
            InitializeComponent();
        }

//        public FormAsociar(Form f, string cliente, Usuario user)
        public FormAsociar(Form f, string clienteId, string clienteDesc)
        {
            InitializeComponent();
            formPadre = f;
            this.clienteDesc = clienteDesc;
            this.clienteId = clienteId;

            //usuario = user;
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
            txtCliente.Text = clienteDesc;

            dtpFechaEmision.Value = DateTime.Now;
            dtpFechaVencimiento.Value = DateTime.Now;

            //  Llena el combo de emisor
            cbxEmisor.Items.Clear();//VACIA LOS ELEMENTOS DEL COMBO
            Herramientas.llenarComboBoxSP(cbxEmisor,"SARASA.cbx_emisor",null,true);
        }

        //  Asociar: Ejecutar SP asociarTarjeta(idcliente, demas_parametros)
        private void btnAsociar_Click(object sender, EventArgs e)
        {
            bool numeroOk = false, codSeguridadOK = false, fechasOk = false;

            if (Herramientas.IsNumericLong(txtNumero.Text) && (txtNumero.Text.ToString().Length == 16))
            {
                numeroOk = true;
                lblNumero.ForeColor = Color.Black;
            }
            else
            {
                numeroOk = false;
                lblNumero.ForeColor = Color.Red;
            }
    
            if (Herramientas.IsNumeric(txtCodSeguridad.Text))
            {
                codSeguridadOK = true;
                lblCodSeguridad.ForeColor = Color.Black;
            }
            else
            {
                codSeguridadOK = false;
                lblCodSeguridad.ForeColor = Color.Red;
            }


            if (dtpFechaEmision.Value.ToShortDateString().Equals(dtpFechaVencimiento.Value.ToShortDateString()))
            {// EMISION Y VENCIMIENTO IGUALES, ERROR AL GUARDAR
                //   Utils.Herramientas.msebox_informacion("Existen valores inválidos: " + dtpFechaEmision.Value.ToShortTimeString() + "=" + dtpFechaVencimiento.Value.ToShortTimeString());
                fechasOk = false;
                lblFechaEmision.ForeColor = Color.Red;
                lblFechaVencimiento.ForeColor = Color.Red;
            }
            else 
            {
                fechasOk = true;
                lblFechaEmision.ForeColor = Color.Black;
                lblFechaVencimiento.ForeColor = Color.Black;
            }
    
            if (fechasOk && numeroOk && codSeguridadOK)
            {
                try
                {
                    List<SqlParameter> lista = Utils.Herramientas.GenerarListaDeParametros(
                        "@cliente_id", clienteId,
                        "@tc_num", Herramientas.sha256_hash(txtNumero.Text),//Convert.ToString(txtNumero.Text),
                        "@tc_ultimoscuatro", Convert.ToString(Utils.Herramientas.stringRight(txtNumero.Text, 4)),
                        "@tc_emision", dtpFechaEmision.Value.ToShortDateString(),
                        "@tc_vencimiento", dtpFechaVencimiento.Value.ToShortDateString(),
                        "@tc_codseg", Convert.ToString(txtCodSeguridad.Text),
                        "@tc_emisor", Convert.ToString(cbxEmisor.Text));

                        Herramientas.EjecutarStoredProcedure("SARASA.Asociar_Tarjeta", lista);
                        Herramientas.msebox_informacion("Tarjeta asociada con éxito");
                        this.Close();
                        this.formPadre.Show();
                    
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
