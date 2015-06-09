using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Facturacion
{
    public partial class FormFacturasPagas : Form
    {
        Form formPadre;
        Utils.Usuario usuario;

        public FormFacturasPagas(Form f, Utils.Usuario user)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
        }

        private void FormFacturasPagas_Load(object sender, EventArgs e)
        {

        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }

        private void buttonDetalles_Click(object sender, EventArgs e)
        {
            Facturacion.FormVerFactura frmDetalles = new Facturacion.FormVerFactura(this, usuario);
            this.Hide();
            frmDetalles.Show();
        }

    }
}
