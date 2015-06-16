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

        public FormModificar(Form f, Cuenta cuenta)
        {
            InitializeComponent();
            formPadre = f;
            this.cuenta = cuenta;
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
            Herramientas.llenarComboBoxSP(cbxTipoCta, "SARASA.cbx_tipocta", null, true);
            cbxTipoCta.SelectedValue = cuenta.IdTipo.ToString();
            Herramientas.llenarComboBoxSP(cbxEstado, "SARASA.cbx_estado", null, true);
            cbxEstado.SelectedValue = cuenta.IdEstado.ToString();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
                List<SqlParameter> lista = Utils.Herramientas.GenerarListaDeParametros(
                    "@cliente_id",cuenta.IdCliente,
                    "@cuenta_numero",cuenta.Numero,
                    "@tipo_cuenta_deseado", ((KeyValuePair<string, string>)cbxTipoCta.SelectedItem).Key,
                    "@estado_deseado", ((KeyValuePair<string, string>)cbxEstado.SelectedItem).Key);

                Herramientas.EjecutarStoredProcedure("SARASA.modificar_cuenta", lista);

                Utils.Herramientas.msebox_informacion("Cuenta modificada con éxito");
        }
    }
}
