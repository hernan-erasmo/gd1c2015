using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Utils;

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

        private void ModificarCliente_Load(object sender, EventArgs e)
        {
            Herramientas.llenarComboBoxSP(cbxTipoDoc, "SARASA.cbx_tipodoc", null, true);
            Herramientas.llenarComboBoxSP(cbxPais, "SARASA.cbx_pais", null, true);

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
            Herramientas.msebox_informacion("Se debe ejecutar un SP");
        }
    }
}
