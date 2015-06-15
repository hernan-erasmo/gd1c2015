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

                    if (Herramientas.EjecutarStoredProcedure("SARASA.retirar_efectivo", lista) != null)
                    {
                        Utils.Herramientas.msebox_informacion("Retiro realizado");
                    }                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Verificar que el formulario este completo", "Retiro", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else 
            {
                Utils.Herramientas.msebox_informacion("El documento no es válido");
            }
        }
    }
}
