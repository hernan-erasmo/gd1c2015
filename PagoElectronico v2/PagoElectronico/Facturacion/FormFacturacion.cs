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

namespace PagoElectronico.Facturacion
{
    public partial class FormFacturacion : Form
    {

        Form formPadre;
        Utils.Usuario usuario;
        

        public FormFacturacion(Form f, Utils.Usuario user)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
        }

        private void FormFacturacion_Load(object sender, EventArgs e)
        {

        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }

        private void buttonGenerar_Click(object sender, EventArgs e)
        {
            Facturacion.FormGenerarFactura frmGenerarFactura = new Facturacion.FormGenerarFactura(this, usuario);
            this.Hide();
            frmGenerarFactura.Show();
        }

        private void buttonFacturasPagadas_Click(object sender, EventArgs e)
        {
            Facturacion.FormFacturasPagas frmFacturasPagas = new Facturacion.FormFacturasPagas(this, usuario);
            this.Hide();
            frmFacturasPagas.Show();
        }

    }
}
