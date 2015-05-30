﻿using System;
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
    public partial class FormBuscar : Form
    {
        Utils.Usuario usuario;
        Form formPadre;

        public FormBuscar(Form f, Utils.Usuario user)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
        }

        private void FormBuscar_Load(object sender, EventArgs e)
        {
            dtpFechaAperturaDesde.Enabled = false;
            dtpFechaAperturaHasta.Enabled = false;

            Herramientas.llenarComboBox(cbxPais, "SELECT Pais_Id 'Valor', Pais_Nombre 'Etiqueta' FROM test.pais ORDER BY Pais_Nombre");
            Herramientas.llenarComboBox(cbxMoneda, "SELECT Moneda_Id 'Valor', Moneda_Descripcion 'Etiqueta' FROM test.Moneda");
            Herramientas.llenarComboBox(cbxTipoCta, "SELECT Tipocta_Id 'Valor', Tipocta_Descripcion 'Etiqueta' FROM test.Tipocta");

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            formPadre.Show();   //  Muestra el formulario padre
            this.Close();       //  Cierra el formulario
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            FormCrear formCrear = new FormCrear(this, usuario);
            formCrear.Show();
            this.Hide();
        }

        private void chkFechaApertura_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFechaApertura.Checked == true)
            {
                dtpFechaAperturaDesde.Enabled = true;
                dtpFechaAperturaHasta.Enabled = true;
            }
            else
            {
                dtpFechaAperturaDesde.Enabled = false;
                dtpFechaAperturaHasta.Enabled = false;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            lblEstadoBusqueda.Text = "Ejecutando busqueda...";

            string fechaAperturaDesde = "", fechaAperturaHasta = "";
            string cliente = txtCliente.Text;
            string numero = txtNumero.Text;
            string tipoCuentaId = ((KeyValuePair<string, string>)cbxTipoCta.SelectedItem).Key;
            string monedaId = ((KeyValuePair<string, string>)cbxMoneda.SelectedItem).Key;
            string paisId = ((KeyValuePair<string, string>)cbxPais.SelectedItem).Key;


            if (chkFechaApertura.Checked)
            {
                fechaAperturaDesde = dtpFechaAperturaDesde.Value.ToShortDateString();
                fechaAperturaHasta = dtpFechaAperturaHasta.Value.ToShortDateString();
            }


            //  ARMA LA QUERY A EJECUTAR BASADO EN LOS FILTROS
            string queryConsulta = Utils.Filtros.filtroBuscarCuenta(
                cliente, numero, tipoCuentaId, monedaId, paisId,
                fechaAperturaDesde, fechaAperturaHasta);

            Herramientas.msebox_informacion(queryConsulta);



            DataTable resultados;
            try
            {
                resultados = Herramientas.ejecutarConsultaTabla(queryConsulta);
                dataGridView1.DataSource = resultados;

                lblEstadoBusqueda.Text = "Se encontraron " + dataGridView1.RowCount + " filas";

                if (dataGridView1.RowCount > 0)
                { // Hay resultados habilita los botones para dar de Baja y Modificar
                    btnBaja.Enabled = true;
                    btnModificar.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
                lblEstadoBusqueda.Text = "Error al realizar la busqueda";
            }
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            lblEstadoBusqueda.Text = "";    //  Indica estado de la busqueda

            //  Si es administrador tambien limpia el campo de cliente
            if (btnBuscarClie.Enabled == true)
                txtCliente.Text = "";

            txtNumero.Text = "";

            cbxMoneda.SelectedIndex = 0;
            cbxTipoCta.SelectedIndex = 0;
            cbxPais.SelectedIndex = 0;

            btnBaja.Enabled = false;
            btnModificar.Enabled = false;

            //  Limpia las fechas de los filtros
            chkFechaApertura.Checked = false;
            dtpFechaAperturaDesde.Enabled = false;
            dtpFechaAperturaHasta.Enabled = false;

            //  Limpiar la tabla de resultados
            dataGridView1.DataSource = null;
        }
    }
}
