﻿using System;
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
        Form formPadre;
        Usuario usuario;
        int pasoCrear;
        string clienteDesc;
        int clienteId;

        public FormCrear(Form f, Usuario user, string clienteDesc, string clienteId)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
            this.clienteDesc = clienteDesc;
            this.clienteId = Int32.Parse(clienteId);
        }

        private void FormCrear_Load(object sender, EventArgs e)
        {
            pasoCrear = 1;
            txtCliente.Text = clienteDesc;
//            dtpFechaApertura.Value = DateTime.Now;
            dtpFechaApertura.Value = DateTime.Parse(usuario.Fecha);

            Herramientas.llenarComboBoxSP(cbxPais,"SARASA.cbx_pais",null,true);
            Herramientas.llenarComboBoxSP(cbxTipoCta, "SARASA.cbx_tipocta",null,true);
            Herramientas.llenarComboBoxSP(cbxMoneda, "SARASA.cbx_moneda",null,true);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            formPadre.Show();   //  Muestra el formulario padre
            this.Close();       //  Cierra el formulario
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            Cuenta cuenta;

            if (pasoCrear == 1) // Estado base del formulario antes de crear alguna cuenta
            {
                cuenta = new Cuenta();
                cuenta.IdCliente = clienteId;
                cuenta.FechaApertura = dtpFechaApertura.Value.ToShortDateString();
                cuenta.IdTipo = int.Parse(((KeyValuePair<string, string>)cbxTipoCta.SelectedItem).Key);
                cuenta.IdPais = int.Parse(((KeyValuePair<string, string>)cbxPais.SelectedItem).Key);
                cuenta.IdMoneda = int.Parse(((KeyValuePair<string, string>)cbxMoneda.SelectedItem).Key);

                try
                {
                    Herramientas.ejecutarCrearCuenta(cuenta);
                    txtNumero.Text = cuenta.Numero;
                    gbCuenta.Enabled = false;
                    btnCrear.Text = "Finalizar";
                    pasoCrear = 2;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                }

            }
            else //La cuenta ya se creo, se vuelve a poner disponible el formulario para crear otra cuenta
            {
                gbCuenta.Enabled = true;
                cbxTipoCta.SelectedIndex = 0;
                cbxPais.SelectedIndex = 0;
                cbxMoneda.SelectedIndex = 0;
                btnCrear.Text = "Crear";
                txtNumero.Text = "";
                pasoCrear = 1;
                dtpFechaApertura.Value = DateTime.Now;
            }

        }

        private void dtpFechaApertura_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
