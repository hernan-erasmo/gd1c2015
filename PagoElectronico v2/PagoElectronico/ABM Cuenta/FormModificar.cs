using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Utils;

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

            dtpFechaApertura.Value = DateTime.Parse(cuenta.FechaApertura);
            dtpFechaCierre.Value = DateTime.Parse(cuenta.FechaCierre);
            Herramientas.llenarComboBoxConSeleccion(cbxPais, "SELECT Pais_Id 'Valor', Pais_Nombre 'Etiqueta' FROM test.pais ORDER BY Pais_Nombre",cuenta.IdPais.ToString(),true);
            Herramientas.llenarComboBoxConSeleccion(cbxTipoCta, "SELECT Tipocta_Id 'Valor', Tipocta_Descripcion 'Etiqueta' FROM test.Tipocta",cuenta.IdTipo.ToString(),true);
            Herramientas.llenarComboBoxConSeleccion(cbxMoneda, "SELECT Moneda_Id 'Valor', Moneda_Descripcion 'Etiqueta' FROM test.Moneda",cuenta.IdMoneda.ToString(),true);
            Herramientas.llenarComboBoxConSeleccion(cbxEstado, "SELECT Estado_Id 'Valor', Estado_Descripcion 'Etiqueta' FROM test.Estado",cuenta.IdEstado.ToString(),true);


            chkCuentaDeudora.Checked = cuenta.Deudora;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

        }
    }
}
