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

/*
Depósitos

Funcionalidad que permite a un cliente crear depósitos de dinero en alguna de las cuentas que tenga registrada.
Para registrar un depósito será necesario que se determinen ciertos datos a persistir:

-   Cuenta del cliente (Seleccion por buscador)
-   Importe (mayor o igual a 1)
-   Tipo moneda(selección simple, por el momento dólares)
-   Selección de la tarjeta de crédito (Seleccion por buscador)
    de las cuales tenga asociada por medio de la  cual  quiere  realizar  el  depósito.
    Se  debe  validar  que  la  dicha  TC  no  se encuentre vencida.
-   Fecha del depósito

En esta primera versión solo está previsto que los ingresos se realicen por medio de TC. 
Los depósitos a realizarse solo podrán ser efectuados por el mismo titular de la TC, la cuenta 
receptora de este depósito debe existir y estar habilitada.

Estos depósitos no tienen comisionesni tampoco generar un cargo que tenga que ser 
facturado dentro del caso de uso de facturación, así mismo es necesario que se genere un 
comprobante de depósito en donde  se registrará la fecha de dicho ingreso y el 
número de operación. El número de ingreso no será consecutivo a la numeración 
utilizada para los retiros de dinero y los números de transacciones realizadas. 


*/

namespace PagoElectronico.Depositos
{
    public partial class FormDepositos : Form
    {
        Utils.Usuario usuario;
        Form formPadre;
        string clienteId;

        public FormDepositos(Form f, Utils.Usuario user)
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

        //  Vuelve al menu principal
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }


        private void FormDepositos_Load(object sender, EventArgs e)
        {
            if (usuario.RolId == "1")
                lklCliente.Enabled = true;
            else
                lklCliente.Enabled = false;

            if (usuario.ClienteId != 0) 
            {
                txtCliente.Text = usuario.Apellido + ", " + usuario.Nombre + " (" + usuario.ClienteId + ")";
                Herramientas.llenarComboBoxSP(cbxCuenta, "SARASA.cbx_cuenta", Herramientas.GenerarListaDeParametros("@Cliente_Id", usuario.ClienteId, "@Estado_Desc", "Habilitada"), true);
                Herramientas.llenarComboBoxSP(cbxTarjeta, "SARASA.cbx_tc", Herramientas.GenerarListaDeParametros("@Cliente_Id", usuario.ClienteId), true);
            }

            Herramientas.llenarComboBoxSP(cbxMoneda, "SARASA.cbx_moneda",null,true);
        }


        private void btnDepositar_Click(object sender, EventArgs e)
        {
            bool importeOK = false, cuentaOK = false, tarjetaOK = false, 
                monedaOK = false, clienteOK = false;

            if (Herramientas.IsDecimal(txtImporte.Text))
            {
                importeOK = true;
                lblImporte.ForeColor = Color.Black;

            }
            else {
                importeOK = false;
                lblImporte.ForeColor = Color.Red;
            }

            if (cbxCuenta.DataSource != null)
            {
                cuentaOK = true;
                lblCuenta.ForeColor = Color.Black;

            }
            else {
                cuentaOK = false;
                lblCuenta.ForeColor = Color.Red;
            }

            if (cbxTarjeta.DataSource != null)
            {
                tarjetaOK = true;
                lblTarjeta.ForeColor = Color.Black;

            }
            else
            {
                tarjetaOK = false;
                lblTarjeta.ForeColor = Color.Red;
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

            if (clienteOK && importeOK && cuentaOK && tarjetaOK && monedaOK)
            {
                List<SqlParameter> lista = Herramientas.GenerarListaDeParametros(
                        "@cliente_id", this.clienteId,
                        "@deposito_fecha", dtpFecha.Value.ToShortDateString(),
                        "@deposito_importe", txtImporte.Text,
                        "@deposito_moneda_id", ((KeyValuePair<string, string>)cbxMoneda.SelectedItem).Key,
                        "@deposito_tarjeta_num", ((KeyValuePair<string, string>)cbxTarjeta.SelectedItem).Key,
                        "@deposito_cuenta_num", ((KeyValuePair<string, string>)cbxCuenta.SelectedItem).Key);

                if (Herramientas.EjecutarStoredProcedure("SARASA.realizar_deposito", lista) != null)
                {
                    string msj = "CLIENTE: " + usuario.Apellido + ", " + usuario.Nombre + " (" + this.clienteId + ")\n"

                        + "CUENTA: " + ((KeyValuePair<string, string>)cbxCuenta.SelectedItem).Key + "\n"
                        + "TARJETA: " + ((KeyValuePair<string, string>)cbxTarjeta.SelectedItem).Value + "\n"
                        + "IMPORTE: $" + txtImporte.Text + "\n";

                    MessageBox.Show(msj, "DEPOSITO - COMPROBANTE",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cbxCuenta.SelectedIndex = 0;
                    cbxMoneda.SelectedIndex = 0;
                    cbxTarjeta.SelectedIndex = 0;
                    txtImporte.Text = "";
                }
                else 
                {
                    MessageBox.Show("Verificar que el formulario este completo", "Depositos", MessageBoxButtons.OK, MessageBoxIcon.Warning);                    
                }
            }
        }


        private void lklCliente_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ABM_Cliente.FormBuscar frmBuscarCliente = new ABM_Cliente.FormBuscar(this, usuario,
                                    "BuscarCliente", "Depositos.FormDepositos");
            frmBuscarCliente.Show();            
            this.Hide();

        }


        public void setClienteEncontrado(string clienteId, string nombre, string apellido)
        {
            txtCliente.Text = apellido + ", " + nombre + " (" + clienteId + ")";
            this.clienteId = clienteId;

            cbxCuenta.DataSource = null;
            cbxTarjeta.DataSource = null;
            Herramientas.llenarComboBoxSP(cbxCuenta, "SARASA.cbx_cuenta", Herramientas.GenerarListaDeParametros("@Cliente_Id", this.clienteId, "@Estado_Desc", "Habilitada"), true);
            Herramientas.llenarComboBoxSP(cbxTarjeta, "SARASA.cbx_tc", Herramientas.GenerarListaDeParametros("@Cliente_Id", this.clienteId), true);

        }

    }
}
