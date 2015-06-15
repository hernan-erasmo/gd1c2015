using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Utils;
using System.Data.SqlClient;

namespace PagoElectronico.Retiros
{
    public partial class FormRetiros : Form
    {
        Utils.Usuario usuario;
        Form formPadre;

        public FormRetiros(Form f, Utils.Usuario user)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
        }

        private void FormRetiros_Load(object sender, EventArgs e)
        {
            txtCliente.Text = usuario.Apellido + ", " + usuario.Nombre + " (" + usuario.ClienteId + ")";
            Herramientas.llenarComboBoxSP(cbxCuenta, "SARASA.cbx_cuenta", Herramientas.GenerarListaDeParametros("@Cliente_Id", usuario.ClienteId, "@Estado_Desc", "Habilitada"), true);
            Herramientas.llenarComboBoxSP(cbxMoneda, "SARASA.cbx_moneda",null,true);
            Herramientas.llenarComboBoxSP(cbxBanco, "SARASA.cbx_banco", null, true);
        }

        //  Boton X
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Dispose();
            formPadre.Show();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }

        private void btnRetirar_Click(object sender, EventArgs e)
        {
            bool importeOK = false, documentoOK = false;

            if (Herramientas.IsDecimal(txtImporte.Text))
            {
                lblImporte.ForeColor = Color.Black;
                importeOK = true;
            }
            else
            {
                lblImporte.ForeColor = Color.Red;
                importeOK = false;
            }

            if (usuario.Documento.Equals(txtDocumento.Text))
            {
                lblDocumento.ForeColor = Color.Black;
                documentoOK = true;
            }
            else
            {
                lblDocumento.ForeColor = Color.Red;
                documentoOK = false;
            }

            if (documentoOK && importeOK)
            {
                try
                {
                    List<SqlParameter> lista = Herramientas.GenerarListaDeParametros(
                                                "@cliente_id", usuario.ClienteId,
                                                "@cliente_documento", txtDocumento.Text,
                                                "@cuenta_nro", ((KeyValuePair<string, string>)cbxCuenta.SelectedItem).Key,
                                                "@moneda_id", ((KeyValuePair<string, string>)cbxMoneda.SelectedItem).Key,
                                                "@importe", txtImporte.Text,
                                                "@banco_codigo", ((KeyValuePair<string, string>)cbxBanco.SelectedItem).Key);

                    if (Herramientas.EjecutarStoredProcedure("SARASA.retirar_efectivo", lista) != null)
                    {

                        string boleta = "CLIENTE: " + usuario.Apellido +", "+ usuario.Nombre + " (" + usuario.ClienteId + ")\n"
                                    + "CUENTA: " + ((KeyValuePair<string, string>)cbxCuenta.SelectedItem).Key + "\n"
                                    + "IMPORTE: $" + txtImporte.Text + " (" + ((KeyValuePair<string, string>)cbxMoneda.SelectedItem).Value+ ")\n"
                                    + "BANCO: " + ((KeyValuePair<string, string>)cbxBanco.SelectedItem).Value + "\n";

                        MessageBox.Show(boleta, "RETIRO - COMPROBANTE",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtDocumento.Text = "";
                        txtImporte.Text = "";
                        cbxBanco.SelectedIndex = 0;
                        cbxCuenta.SelectedIndex = 0;
                        cbxMoneda.SelectedIndex = 0;

                    }                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Verificar que el formulario este completo", "Retiro", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
