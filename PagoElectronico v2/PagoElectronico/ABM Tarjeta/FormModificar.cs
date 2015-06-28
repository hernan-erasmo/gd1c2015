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

        public FormModificar(Form f, Tarjeta t)
        {
            InitializeComponent();
            formPadre = f;
            tarjeta = t;
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

            txtCliente.Text = tarjeta.Apellido + ", " + tarjeta.Nombre + " (" + tarjeta.ClienteId + ")";
            txtCodSeguridad.Text = tarjeta.CodigoSeguridad;
            //txtNumero.Text = tarjeta.Numero;
            txtNumero.Text = tarjeta.Descripcion;

            dtpFechaEmision.Value = DateTime.Parse(tarjeta.FechaEmision);
            dtpFechaVencimiento.Value = DateTime.Parse(tarjeta.FechaVencimiento);

            //  Llena el combo de emisor
            cbxEmisor.Items.Clear();//VACIA LOS ELEMENTOS DEL COMBO
            Herramientas.llenarComboBoxSP(cbxEmisor, "SARASA.cbx_emisor", null, true);
            cbxEmisor.Text = tarjeta.Emisor;
        }

        //  Volver
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            this.formPadre.Show();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            bool codSeguridadOK = false, fechasOk = false;

            string msj = "Seguro que quiere MODIFICAR la información de la TARJERTA " +
                tarjeta.Descripcion + "(" + tarjeta.Emisor + ")\n" +
                 "del Cliente: " + txtCliente.Text + "?";


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
    
            if (fechasOk && codSeguridadOK)
            {
                var result = MessageBox.Show(msj, "Desasociar tarjeta",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);//, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.OK)
                {
                    List<SqlParameter> lista = Utils.Herramientas.GenerarListaDeParametros(
                        "@cliente_id", this.tarjeta.ClienteId,
                        "@tc_num", this.tarjeta.Numero,
                        "@tc_emision", dtpFechaEmision.Value.ToShortDateString(),
                        "@tc_vencimiento", dtpFechaVencimiento.Value.ToShortDateString(),
                        "@tc_codseg", txtCodSeguridad.Text,
                        "@tc_emisor", cbxEmisor.Text);
                    Herramientas.EjecutarStoredProcedure("SARASA.Modificar_Tarjeta", lista);
                }
                this.Dispose();
                this.formPadre.Show();
            }
        }
    }
}
