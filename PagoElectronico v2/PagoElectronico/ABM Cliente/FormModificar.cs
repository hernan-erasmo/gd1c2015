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

namespace PagoElectronico.ABM_Cliente
{
    public partial class FormModificar : Form
    {
        Form formPadre;
        Usuario usuario;
        Cliente cliente;

        public void asignarPadre(Form padre)
        {
            this.formPadre = padre;
        }

        public FormModificar()
        {
            InitializeComponent();
        }

        public FormModificar(Form padre,Usuario user, Cliente cliente)
        {
            InitializeComponent();
            this.formPadre = padre;
            this.usuario = user;
            this.cliente = cliente;
        }

        //  Boton X
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Dispose();
            formPadre.Show();
        }

        private void ModificarCliente_Load(object sender, EventArgs e)
        {
            Herramientas.llenarComboBoxSP(cbxTipoDoc, "SARASA.cbx_tipodoc", null, true);
            cbxTipoDoc.SelectedValue = cliente.TipoDocId;
            Herramientas.llenarComboBoxSP(cbxPais, "SARASA.cbx_pais", null, true);
            cbxPais.SelectedValue = cliente.PaisId;

            txtNombre.Text = cliente.Nombre;
            txtApellido.Text = cliente.Apellido;
            txtNumDoc.Text = cliente.NumeroDoc;
            txtMail.Text = cliente.Mail;
            txtCalle.Text = cliente.DomCalle;
            txtCalleNum.Text = cliente.DomNumero;
            txtPiso.Text = cliente.DomPiso;
            txtDepto.Text = cliente.DomDpto;

            chkEstado.Checked = cliente.Habilitado;
            dtpFechaNac.Value = DateTime.Parse(cliente.FechaNacimiento);
        }

        private void txtVolver_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.formPadre.Show();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool docNumeroOK = false, domPisoOK = false, domNumeroOK = false;

            string msj = "Seguro que quiere MODIFICAR la información del CLIENTE \"" +
                cliente.Apellido + ", " +
                cliente.Nombre + " (" +
                cliente.ClienteId + ")\"?";


            if (Herramientas.IsNumeric(txtNumDoc.Text))
            {
                docNumeroOK = true;
                lblNroDocumento.ForeColor = Color.Black;   
            }
            else 
            {
                docNumeroOK = false;
                lblNroDocumento.ForeColor = Color.Red;
            }

            if (Herramientas.IsNumeric(txtCalleNum.Text))
            {
                domNumeroOK = true;
                lblDomNum.ForeColor = Color.Black;
            }
            else
            {
                domNumeroOK = false;
                lblDomNum.ForeColor = Color.Red;
            }

            if (Herramientas.IsNumeric(txtPiso.Text))
            {
                domPisoOK = true;
                lblDomPiso.ForeColor = Color.Black;
            }
            else
            {
                domPisoOK = false;
                lblDomPiso.ForeColor = Color.Red;
            }

            if (docNumeroOK && domNumeroOK && domPisoOK)
            {
                var result = MessageBox.Show(msj, "Modificar cliente",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);//, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.OK)
                {
                    List<SqlParameter> lista = Herramientas.GenerarListaDeParametros(
                        "@Cliente_Id", cliente.ClienteId,
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
                        "@Cliente_Habilitado", chkEstado.Checked);

                    Herramientas.EjecutarStoredProcedure("SARASA.modificar_cliente", lista);

                    this.Dispose();
                    this.formPadre.Show();
                }            
            }
        }
    }
}
