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
            //Herramientas.msebox_informacion("Abre el buscador para elegir la cuenta de otro cliente");
            //cbxCuentaDestino.Text = "444550000000032105 (Oro)";

            ABM_Cuenta.FormBuscar frmBuscarCuenta = new ABM_Cuenta.FormBuscar(this, usuario,
                                                "BuscarCuenta", "Transferencias.FormTransferencias");
            frmBuscarCuenta.Show();
            this.Hide();
        }

        private void btnTransferir_Click(object sender, EventArgs e)
        {

            if (Herramientas.IsDecimal(txtImporte.Text))
            {
                lblImporte.ForeColor = Color.Green;

                try
                {
                    List<SqlParameter> lista = Herramientas.GenerarListaDeParametros(
                            "@cuenta_origen", ((KeyValuePair<string, string>)cbxCuenta.SelectedItem).Key,
                            "@cuenta_destino", numeroCuenta,
                            "@importe", txtImporte.Text);

                    if (Herramientas.EjecutarStoredProcedure("SARASA.realizar_transferencia", lista) != null) 
                    {
                        string msj = "TRANSFERENCIA:\n"
                            + "Id Cliente: " + usuario.ClienteId + "\n"
                            + "Cuenta Origen: " + ((KeyValuePair<string, string>)cbxCuenta.SelectedItem).Key + "\n"
                            + "Cuenta Destino: " + numeroCuenta + "\n"
                            + "Importe: " + txtImporte.Text + "\n";
                        Utils.Herramientas.msebox_informacion(msj);
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

        private void cbxCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Text = "valor (" + ((KeyValuePair<string, string>)cbxCuenta.SelectedItem).Key + ")";
        }
    }
}
