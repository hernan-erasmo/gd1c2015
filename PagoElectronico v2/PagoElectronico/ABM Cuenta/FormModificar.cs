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

namespace PagoElectronico.ABM_Cuenta
{
    public partial class FormModificar : Form
    {
        Cuenta cuenta;
        Form formPadre;
        Usuario usuario;

        public FormModificar(Form f, Cuenta cuenta, Usuario user)
        {
            InitializeComponent();
            formPadre = f;
            this.cuenta = cuenta;
            this.usuario = user;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }

        private void FormModificar_Load(object sender, EventArgs e)
        {
            txtCliente.Text = cuenta.DesCliente + " (" + cuenta.IdCliente + ")";
            txtNumero.Text = cuenta.Numero;

            if (usuario.RolId != "1") 
            {
                cbxEstado.Visible = false;
                lblEstado.Visible = false;
            }


            Herramientas.llenarComboBoxSP(cbxTipoCta, "SARASA.cbx_tipocta", null, true);
            cbxTipoCta.SelectedValue = cuenta.IdTipo.ToString();

            Herramientas.llenarComboBoxSP(cbxEstado, "SARASA.cbx_estado", null, true);
            cbxEstado.SelectedValue = cuenta.IdEstado.ToString();            


        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            string msj = "Seguro que quiere MODIFICAR la información de la CUENTA " + txtNumero.Text + "\n" +
                "del Cliente: " + txtCliente.Text + "?";

            var result = MessageBox.Show(msj, "Modificar cuenta",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);//, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.OK)
            {
                List<SqlParameter> lista = Utils.Herramientas.GenerarListaDeParametros(
                    "@cliente_id", cuenta.IdCliente,
                    "@cuenta_numero", cuenta.Numero,
                    "@tipo_cuenta_deseado", ((KeyValuePair<string, string>)cbxTipoCta.SelectedItem).Key,
                    "@estado_deseado", ((KeyValuePair<string, string>)cbxEstado.SelectedItem).Key);

                Herramientas.EjecutarStoredProcedure("SARASA.modificar_cuenta", lista);

                this.Dispose();
                this.formPadre.Show();
            }
        }
    }
}
