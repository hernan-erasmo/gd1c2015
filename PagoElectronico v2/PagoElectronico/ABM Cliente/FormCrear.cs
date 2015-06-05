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

namespace PagoElectronico.ABM_Cliente
{
    public partial class FormCrear : Form
    {
        Form formPadre;
        Usuario usuario;

        public void asignarPadre(Form padre)
        {
            this.formPadre = padre;
        }

        public FormCrear(Form f, Utils.Usuario user)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
        }

        public FormCrear()
        {
            InitializeComponent();
        }

        private void AltaCliente_Load(object sender, EventArgs e)
        {
            Herramientas.llenarComboBox(cbxPais, "SELECT Pais_Id 'Valor', Pais_Nombre 'Etiqueta' FROM test.pais ORDER BY Pais_Nombre", true);

            Utils.Herramientas.llenarComboBox(cbxTipoDoc, "select Tipodoc_Id 'Valor', Tipodoc_Descripcion 'Etiqueta' from test.Tipodoc", true);
            Utils.Herramientas.llenarComboBox(cbxRol, "SELECT Rol_Id 'Valor', Rol_Descripcion 'Etiqueta' FROM test.Rol", true);
           


            //Herramientas.GenerarListaDeParametros(
            //    //"@cliId", txtCliente.Text,           //  @cliId integer,
            //    "@cliNombre", txtNombre.Text,        //  @cliNombre nvarchar(255),
            //    "@cliApellido", txtApellido.Text,    //  @cliApellido nvarchar(255),
            //    "@cliTipoDocId", cbxTipoDoc.SelectedItem.ToString(), //  @cliTipoDocId integer,
            //    "@cliNumDoc", txtNumDoc.Text,       //  @cliNumDoc numeric(18,0)
            //    "@cliMail", txtMail.Text,           //  @cliMail nvarchar(255)
            //    "@cliPaisId", cbxPais.SelectedItem.ToString(),      //  @cliPaisId integer,
            //    "@cliDomCalle", txtCalle.Text,      //  @cliCalle nvarchar(255),
            //    "@cliDomNumero", txtCalleNum.Text,  //  @cliDomNumero numeric(18,0),
            //    "@cliDomPiso", txtPiso.Text,        //  @cliDomPiso numeric(18,0),
            //    "@cliDomDpto", txtDepto.Text,       //  @cliDomDpto nvarchar(10),
            //    "@cliFechaNac", dtpFechaNac,        //  @cliFechaNac datetime,
            //    "@cliHabilitado", chkEstado.Checked);    // @cliHabilitado bit);

        }

        private void txtCrear_Click(object sender, EventArgs e)
        {
            //  EJECUTA EL STORE PROCEDURE QUE GRABA LOS DATOS EN LA TABLA
            string nombreSP = "Test.Crear_Cliente";    //  Nombre del StoreProcedure

            try
            {
                List<SqlParameter> lista = Herramientas.GenerarListaDeParametros(
                                            "@cliId", usuario.ClienteId,
                                            "@cliNombre", txtNombre.Text,
                                            "@cliApellido", txtApellido.Text,
                                            "@cliTipoDocId", ((KeyValuePair<string, string>)cbxTipoDoc.SelectedItem).Key,
                                            "@cliNumDoc", txtNumDoc.Text,
                                            "@cliMail", txtMail.Text,
                                            "@cliPaisId", ((KeyValuePair<string, string>)cbxPais.SelectedItem).Key,
                                            "@cliDomCalle", txtCalle.Text,
                                            "@cliDomNumero", txtCalleNum.Text,
                                            "@cliDomPiso", txtPiso.Text,
                                            "@cliDomDpto", txtDepto.Text,
                                            "@cliFechaNac", dtpFechaNac.Value.ToShortDateString(),
                                            "@cliHabilitado", chkEstado.Checked);

                Herramientas.EjecutarStoredProcedure(nombreSP, lista);
                Herramientas.msebox_informacion("Cliente nueva creada");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }

        private void chkEstado_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }
    }
}