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

namespace PagoElectronico.Transferencias
{
    public partial class FormTransferencias : Form
    {
        Form formPadre;
        Usuario usuario;
        string numeroCuenta = "0";
        string clienteId;

        public FormTransferencias(Form f, Utils.Usuario user)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
            this.clienteId = "" + user.ClienteId;
        }

        //  Boton X
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Dispose();
            formPadre.Show();
        }


        private void FormTransferencias_Load(object sender, EventArgs e)
        {
            if (usuario.RolId == "1")
                lklCliente.Enabled = true;
            else
                lklCliente.Enabled = false;

            if (usuario.ClienteId != 0)
            {
                txtCliente.Text = usuario.Apellido + ", " + usuario.Nombre + " (" + usuario.ClienteId + ")";
                Herramientas.llenarComboBoxSP(cbxCuenta,
                        "SARASA.cbx_cuenta",
                        Herramientas.GenerarListaDeParametros("@Cliente_Id", usuario.ClienteId, "@Estado_Desc", "Habilitada"),
                        true);
                Herramientas.llenarComboBoxSP(cbxCuentaDestino,
                        "SARASA.cbx_cuenta",
                        Herramientas.GenerarListaDeParametros("@Cliente_Id", usuario.ClienteId, "@Estado_Desc", "Habilitada"),
                        true);
            }

        }


        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
            formPadre.Show();
        }


        private void btnTransferir_Click(object sender, EventArgs e)
        {
            bool importeOK = false, clienteOK = false, ctaOrigenOK = false, ctaDestinoOK = false;

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

            if (cbxCuenta.DataSource != null)
            {
                ctaOrigenOK = true;
                lblCuenta.ForeColor = Color.Black;
            }
            else
            {
                ctaOrigenOK = false;
                lblCuenta.ForeColor = Color.Red;
            }

            if (cbxCuentaDestino.DataSource != null)
            {
                ctaDestinoOK = true;
                lklCuentaDestino.LinkColor = Color.Blue;
            }
            else
            {
                ctaDestinoOK = false;
                lklCuentaDestino.LinkColor = Color.Red;
            }


            if (clienteOK && ctaOrigenOK && ctaDestinoOK && importeOK)
            {
                List<SqlParameter> lista = Herramientas.GenerarListaDeParametros(
                        "@cuenta_origen", ((KeyValuePair<string, string>)cbxCuenta.SelectedItem).Key,
                        "@cuenta_destino", numeroCuenta,
                        "@importe", txtImporte.Text);

                if (Herramientas.EjecutarStoredProcedure("SARASA.realizar_transferencia", lista) != null)
                {
                    string msj = "CLIENTE: " + usuario.Apellido + ", " + usuario.Nombre + " (" + usuario.ClienteId + ")\n"
                        + "CUENTA ORIGEN: " + ((KeyValuePair<string, string>)cbxCuenta.SelectedItem).Key + "\n"
                        + "CUENTA DESTINO: " + numeroCuenta + "\n"
                        + "IMPORTE: $" + txtImporte.Text + "\n";

                    MessageBox.Show(msj, "TRANSFERENCIA - COMPROBANTE",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cbxCuenta.SelectedIndex = 0;
                    cbxCuentaDestino.SelectedIndex = 0;
                    txtImporte.Text = "";
                }            
            }
        }


        //  Abre el buscador, para seleccionar un cliente
        private void lklCliente_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ABM_Cliente.FormBuscar frmBuscarCliente = new ABM_Cliente.FormBuscar(this, usuario,
                                    "BuscarCliente", "Transferencias.FormTransferencias");
            frmBuscarCliente.Show();
            this.Hide();
        }

        //  Abre el buscador, para seleccionar una cuenta de otro cliente
        private void lklCuentaDestino_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ABM_Cuenta.FormBuscar frmBuscarCuenta = new ABM_Cuenta.FormBuscar(this, usuario,
                                                "BuscarCuenta", "Transferencias.FormTransferencias");
            frmBuscarCuenta.Show();
            this.Hide();
        }


        public void setCuentaEncontrada(string numero, string tipo)
        {
            cbxCuentaDestino.Text = numero + " (" + tipo + ")";
            this.numeroCuenta = numero;
        }


        public void setClienteEncontrado(string clienteId, string nombre, string apellido)
        {
            txtCliente.Text = apellido + ", " + nombre + " (" + clienteId + ")";
            this.clienteId = clienteId;

            cbxCuenta.DataSource = null;
            cbxCuentaDestino.DataSource = null;

            Herramientas.llenarComboBoxSP(cbxCuenta,
                    "SARASA.cbx_cuenta",
                    Herramientas.GenerarListaDeParametros("@Cliente_Id", this.clienteId, "@Estado_Desc", "Habilitada"),
                    true);

            Herramientas.llenarComboBoxSP(cbxCuentaDestino,
                    "SARASA.cbx_cuenta",
                    Herramientas.GenerarListaDeParametros("@Cliente_Id", this.clienteId, "@Estado_Desc", "Habilitada"),
                    true);
        }


        private void cbxCuentaDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCuentaDestino.DataSource != null)
                numeroCuenta = ((KeyValuePair<string, string>)cbxCuentaDestino.SelectedItem).Key;
            else
                numeroCuenta = "0";
        }
    }
}
