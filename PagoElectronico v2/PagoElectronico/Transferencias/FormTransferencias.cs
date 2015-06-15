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

        public FormTransferencias(Form f, Utils.Usuario user)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
        }

        //  Boton X
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Dispose();
            formPadre.Show();
        }

        private void FormTransferencias_Load(object sender, EventArgs e)
        {

            txtCliente.Text = usuario.Apellido + ", " + usuario.Nombre + " ("+usuario.ClienteId+")";

            Herramientas.llenarComboBoxSP(cbxCuenta, 
                    "SARASA.cbx_cuenta", 
                    Herramientas.GenerarListaDeParametros("@Cliente_Id", usuario.ClienteId, "@Estado_Desc", "Habilitada"),
                    true);

            Herramientas.llenarComboBoxSP(cbxCuentaDestino,
                    "SARASA.cbx_cuenta",
                    Herramientas.GenerarListaDeParametros("@Cliente_Id", usuario.ClienteId, "@Estado_Desc", "Habilitada"),
                    true);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
            formPadre.Show();
        }


        //  Abre el buscador, para seleccionar una cuenta de otro cliente
        private void lklCuentaDestino_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            ABM_Cuenta.FormBuscar frmBuscarCuenta = new ABM_Cuenta.FormBuscar(this, usuario,
                                                "BuscarCuenta", "Transferencias.FormTransferencias");
            frmBuscarCuenta.Show();
            this.Hide();
        }

        private void btnTransferir_Click(object sender, EventArgs e)
        {

            if (Herramientas.IsDecimal(txtImporte.Text))
            {
                lblImporte.ForeColor = Color.Black;

                try
                {
                    List<SqlParameter> lista = Herramientas.GenerarListaDeParametros(
                            "@cuenta_origen", ((KeyValuePair<string, string>)cbxCuenta.SelectedItem).Key,
                            "@cuenta_destino", numeroCuenta,
                            "@importe", txtImporte.Text);

                    if (Herramientas.EjecutarStoredProcedure("SARASA.realizar_transferencia", lista) != null) 
                    {
                        string msj = "CLIENTE: " + usuario.Apellido + ", "+ usuario.Nombre + " (" +usuario.ClienteId + ")\n"
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
                catch (Exception ex)
                {
                    MessageBox.Show("Verificar que el formulario este completo", "Transferencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            else
                lblImporte.ForeColor = Color.Red;

        }

        public void setCuentaEncontrada(string numero, string tipo)
        {
            cbxCuentaDestino.Text = numero + " (" + tipo + ")";
            this.numeroCuenta = numero;
        }

        private void cbxCuentaDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            numeroCuenta = ((KeyValuePair<string, string>)cbxCuentaDestino.SelectedItem).Key;
        }

    }
}
