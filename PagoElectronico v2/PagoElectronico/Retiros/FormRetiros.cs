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
            string cbxCuentaQuery = "SELECT Cuenta_Numero 'Valor', CAST(Cuenta_Numero AS VARCHAR(18)) + ' (' + Tipocta_Descripcion + ')' 'Etiqueta'"
                                    + "FROM test.Cuenta , test.Tipocta "
                                    + "WHERE Tipocta_Id = Cuenta_Tipocta_Id AND Cuenta_Cliente_Id = " + usuario.ClienteId;
            Herramientas.llenarComboBox(cbxCuenta, cbxCuentaQuery,true);
            Herramientas.llenarComboBox(cbxMoneda, "SELECT Moneda_Id 'Valor', Moneda_Descripcion 'Etiqueta' FROM test.Moneda",true);
            Herramientas.llenarComboBox(cbxBanco, "SELECT Banco_Codigo 'Valor', Banco_Nombre + ' - ' + Banco_Direccion 'ETIQUETA' FROM test.banco", true);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }

        private void btnRetirar_Click(object sender, EventArgs e)
        {

            if (usuario.Documento.Equals(txtDocumento.Text))
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

                    //Utils.Herramientas.EjecutarStoredProcedure("SARASA.retirar_efectivo", lista);
                    Utils.Herramientas.EjecutarStoredProcedure("test.retirar_efectivo", lista);

                    Utils.Herramientas.msebox_informacion("Retiro realizado");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                }
            }
            else 
            {
                Utils.Herramientas.msebox_informacion("El documento no es válido");
            }
        }
    }
}
