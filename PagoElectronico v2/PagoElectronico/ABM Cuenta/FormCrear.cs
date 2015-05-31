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
    public partial class FormCrear : Form
    {
        Utils.Usuario usuario;
        Form formPadre;

        public FormCrear(Form f, Utils.Usuario user)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
        }

        private void FormCrear_Load(object sender, EventArgs e)
        {
            txtCliente.Text = usuario.Apellido + ", " + usuario.Nombre + " (" + usuario.ClienteId + ")";
            Herramientas.llenarComboBox(cbxPais, "SELECT Pais_Id 'Valor', Pais_Nombre 'Etiqueta' FROM test.pais ORDER BY Pais_Nombre");
            Herramientas.llenarComboBox(cbxTipoCta, "SELECT Tipocta_Id 'Valor', Tipocta_Descripcion 'Etiqueta' FROM test.Tipocta");
            Herramientas.llenarComboBox(cbxMoneda, "SELECT Moneda_Id 'Valor', Moneda_Descripcion 'Etiqueta' FROM test.Moneda");            
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            formPadre.Show();   //  Muestra el formulario padre
            this.Close();       //  Cierra el formulario
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {

            //  EJECUTA EL STORE PROCEDURE QUE GRABA LOS DATOS EN LA TABLA
            string nombreSP = "Test.Crear_Cuenta";    //  Nombre del StoreProcedure

            try
            {
                List<SqlParameter> lista = Herramientas.GenerarListaDeParametros(
                                            "@clienteId", usuario.ClienteId,
                                            "@cuentaNumero", txtNumero.Text,
                                            "@fechaApertura", dtpFechaApertura.Value.ToShortDateString(),
                                            "@paisId", ((KeyValuePair<string, string>)cbxPais.SelectedItem).Key,
                                            "@tipoctaId", ((KeyValuePair<string, string>)cbxTipoCta.SelectedItem).Key,
                                            "@monedaId", ((KeyValuePair<string, string>)cbxMoneda.SelectedItem).Key);


                Utils.Herramientas.EjecutarStoredProcedure(nombreSP, lista);

                Utils.Herramientas.msebox_informacion("Cuenta nueva creada");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }
    }
}
