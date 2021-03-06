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
        string userId;

        public void asignarPadre(Form padre)
        {
            this.formPadre = padre;
        }

        public FormCrear(Form f, Utils.Usuario user)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
            labelResultado.Visible = false;
        }

        public FormCrear()
        {
            InitializeComponent();
        }

        //  Boton X
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Dispose();
            formPadre.Show();
        }

        private void AltaCliente_Load(object sender, EventArgs e)
        {
            userId = "";

            rbBuscarUser.Checked = true;
            gbAltaUser.Enabled = false;

            Herramientas.llenarComboBoxSP(cbxPais,
                "SARASA.cbx_pais",null,
                true);
 
            Herramientas.llenarComboBoxSP(cbxTipoDoc,
                "SARASA.cbx_tipodoc", null,
                true);

            Herramientas.llenarComboBoxSP(cbxRol,
                "SARASA.cbx_rol",
                Herramientas.GenerarListaDeParametros("@usuario_id", 0),
                true);

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
            string resultado;
            string tipodoc = ((KeyValuePair<string, string>)cbxTipoDoc.SelectedItem).Key;
            resultado = Herramientas.comprobarDocMail(tipodoc, txtNumDoc.Text, txtMail.Text);
            label4.ForeColor = Color.Black;
            label5.ForeColor = Color.Black;
            label6.ForeColor = Color.Black;

            if(resultado == "1")
            {
                label4.ForeColor = Color.Red;
                label5.ForeColor = Color.Red;
                labelResultado.Text = "Tipo y Nro de Documento ya existentes";
                labelResultado.ForeColor = Color.Red;
                labelResultado.Visible = true;
            }
             if(resultado == "2")
            {
                label6.ForeColor = Color.Red;
                labelResultado.Text = "Mail ya existente";
                labelResultado.ForeColor = Color.Red;
                labelResultado.Visible = true;
            }
             if(resultado == "3")
            {
                label4.ForeColor = Color.Red;
                label5.ForeColor = Color.Red;
                label6.ForeColor = Color.Red;
                labelResultado.Text = "Tipo Doc, Nro de Documento y Mail ya existentes";
                labelResultado.ForeColor = Color.Red;
                labelResultado.Visible = true;
            }
            if(resultado == "0")
            {
            
            //  EJECUTA EL STORE PROCEDURE QUE GRABA LOS DATOS EN LA TABLA
            try
            {
                List<SqlParameter> lista;

                if (txtPiso.Text == "" && txtDepto.Text == "")
                {
                    lista = Herramientas.GenerarListaDeParametros(
                                "@Cliente_Nombre", txtNombre.Text,
                                "@Cliente_Apellido", txtApellido.Text,
                                "@Cliente_Tipodoc_Id", ((KeyValuePair<string, string>)cbxTipoDoc.SelectedItem).Key,
                                "@Cliente_Doc_Nro", txtNumDoc.Text,
                                "@Cliente_Dom_Calle", txtCalle.Text,
                                "@Cliente_Dom_Numero", txtCalleNum.Text,
                                "@Cliente_Dom_Piso", "0",
                                "@Cliente_Dom_Depto", "0",
                                "@Cliente_Mail", txtMail.Text,
                                "@Cliente_Pais_Id", ((KeyValuePair<string, string>)cbxPais.SelectedItem).Key,
                                "@Cliente_Fecha_Nacimiento", dtpFechaNac.Value.ToShortDateString(),
                                "@Cliente_Habilitado", chkEstado.Checked,
                                "@Usuario_Id", userId,
                                "@Usuario_Username", txtUsuario.Text,
                                "@Usuario_Password", Herramientas.sha256_hash(txtPassword.Text),
                                "@Usuario_Pregunta_Sec", txtPreguntaSec.Text,
                                "@Usuario_Respuesta_Sec", Herramientas.sha256_hash(txtRespuestaSec.Text),
                                "@Rol_Id", ((KeyValuePair<string, string>)cbxRol.SelectedItem).Key);
                }
                else
                {
                    lista = Herramientas.GenerarListaDeParametros(
                                     "@Cliente_Nombre", txtNombre.Text,
                                     "@Cliente_Apellido", txtApellido.Text,
                                     "@Cliente_Tipodoc_Id", ((KeyValuePair<string, string>)cbxTipoDoc.SelectedItem).Key,
                                     "@Cliente_Doc_Nro", txtNumDoc.Text,
                                     "@Cliente_Dom_Calle", txtCalle.Text,
                                     "@Cliente_Dom_Numero", txtCalleNum.Text,
                                     "@Cliente_Dom_Piso", txtPiso.Text,
                                     "@Cliente_Dom_Depto", txtDepto.Text,
                                     "@Cliente_Mail", txtMail.Text,
                                     "@Cliente_Pais_Id", ((KeyValuePair<string, string>)cbxPais.SelectedItem).Key,
                                     "@Cliente_Fecha_Nacimiento", dtpFechaNac.Value.ToShortDateString(),
                                     "@Cliente_Habilitado", chkEstado.Checked,
                                     "@Usuario_Id", userId,
                                     "@Usuario_Username", txtUsuario.Text,
                                     "@Usuario_Password", Herramientas.sha256_hash(txtPassword.Text),
                                     "@Usuario_Pregunta_Sec", txtPreguntaSec.Text,
                                     "@Usuario_Respuesta_Sec", Herramientas.sha256_hash(txtRespuestaSec.Text),
                                     "@Rol_Id", ((KeyValuePair<string, string>)cbxRol.SelectedItem).Key);
                }
                
                Herramientas.EjecutarStoredProcedure("SARASA.crear_cliente", lista);
                Herramientas.msebox_informacion("Cliente nueva creada (ID_USER: "+userId+")");
                this.Close();
                this.formPadre.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
            }

        }

        private void txtVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }

        private void rbBuscarUser_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rbBuscarUser.Checked) 
            {
                gbBuscarUser.Enabled = true;
                gbAltaUser.Enabled = false;
            }
        }

        private void rbAltaUser_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rbAltaUser.Checked)
            {
                gbBuscarUser.Enabled = false;
                gbAltaUser.Enabled = true;
                userId = "0";
                txtUsuarioBusq.Text = "";
            }
        }

        private void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
//            ABM_Cliente.FormBuscar frmBuscar = new ABM_Cliente.FormBuscar(this, usuario, 
//                                                "BuscarUsuario", "ABM_Cliente.FormCrear");
            ABM_de_Usuario.FormBuscar frmBuscar = new ABM_de_Usuario.FormBuscar(this, usuario,
                                                    "BuscarUsuario", "ABM_Cliente.FormCrear");
            
            frmBuscar.Show();
            this.Hide();
        
        }

        public void setUsuarioEncontrado(string userId, string username) 
        {
            txtUsuarioBusq.Text = username + " (" + userId + ")";
            this.userId = userId;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            //Informacion del Cliente
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtNumDoc.Text = "";
            txtMail.Text = "";
            txtCalle.Text = "";
            txtCalleNum.Text = "";
            txtPiso.Text = "";
            txtDepto.Text = "";
            cbxTipoDoc.SelectedIndex = 0;
            cbxPais.SelectedIndex = 0;
            chkEstado.Checked = false;

            //Buscar Usuario
            rbBuscarUser.Checked = true;
            txtUsuarioBusq.Text = "";
            userId = "0";

            //Informacion Alta Usuario
            txtUsuario.Text = "";
            txtPassword.Text = "";
            txtPreguntaSec.Text = "";
            txtRespuestaSec.Text = "";
            cbxRol.SelectedIndex = 0;

        }
    }
}
