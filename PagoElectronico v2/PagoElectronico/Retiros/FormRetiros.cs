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
        string clienteId;
        string clienteDocumento;

        public FormRetiros(Form f, Utils.Usuario user)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
            this.clienteId = "" + user.ClienteId;
            this.clienteDocumento = "" + user.Documento;
        }


        private void FormRetiros_Load(object sender, EventArgs e)
        {
            if (usuario.RolId == "1")
                lklCliente.Enabled = true;
            else
                lklCliente.Enabled = false;

            if (usuario.ClienteId != 0)
            {
                txtCliente.Text = usuario.Apellido + ", " + usuario.Nombre + " (" + usuario.ClienteId + ")";
                Herramientas.llenarComboBoxSP(cbxCuenta, "SARASA.cbx_cuenta", Herramientas.GenerarListaDeParametros("@Cliente_Id", usuario.ClienteId, "@Estado_Desc", "Habilitada"), true);
            }
            Herramientas.llenarComboBoxSP(cbxMoneda, "SARASA.cbx_moneda", null, true);
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

            bool clienteOK = false, documentoOK = false,
                cuentaOK = false, monedaOK = false,
                importeOK = false, bancoOK = false;


            if (txtCliente.Text != "")
            {
                clienteOK = true;
                lklCliente.LinkColor = Color.Blue;
            }
            else
            {
                clienteOK = false;
                lklCliente.LinkColor = Color.Red;
            }

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

            if (clienteDocumento.Equals(txtDocumento.Text))
            {
                lblDocumento.ForeColor = Color.Black;
                documentoOK = true;
            }
            else
            {
                lblDocumento.ForeColor = Color.Red;
                documentoOK = false;
            }


            if (cbxCuenta.DataSource != null)
            {
                cuentaOK = true;
                lblCuenta.ForeColor = Color.Black;

            }
            else
            {
                cuentaOK = false;
                lblCuenta.ForeColor = Color.Red;
            }

            if (cbxBanco.DataSource != null)
            {
                bancoOK = true;
                lblBanco.ForeColor = Color.Black;

            }
            else
            {
                bancoOK = false;
                lblBanco.ForeColor = Color.Red;
            }

            if (cbxMoneda.DataSource != null)
            {
                monedaOK = true;
                lblMoneda.ForeColor = Color.Black;
            }
            else
            {
                monedaOK = false;
                lblMoneda.ForeColor = Color.Red;
            }

            if (clienteOK && documentoOK && importeOK && bancoOK && monedaOK && cuentaOK)
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
        }


        private void lklCliente_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ABM_Cliente.FormBuscar frmBuscarCliente = new ABM_Cliente.FormBuscar(this, usuario,
                                    "BuscarCliente", "Retiros.FormRetiros");
            frmBuscarCliente.Show();            
            this.Hide();
        }

        public void setClienteEncontrado(string clienteId, string nombre, string apellido, string documento)
        {
            txtCliente.Text = apellido + ", " + nombre + " (" + clienteId + ")";
            this.clienteId = clienteId;
            this.clienteDocumento = documento;

            cbxCuenta.DataSource = null;

            Herramientas.llenarComboBoxSP(cbxCuenta, "SARASA.cbx_cuenta", Herramientas.GenerarListaDeParametros("@Cliente_Id", this.clienteId, "@Estado_Desc", "Habilitada"), true);

        }
    }
}
