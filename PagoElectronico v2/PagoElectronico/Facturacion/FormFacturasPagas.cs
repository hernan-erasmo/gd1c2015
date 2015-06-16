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
            TablaDatos.DataSource = Utils.Herramientas.ejecutarConsultaTabla("SELECT f.Factura_Numero AS Id_Factura, f.Factura_Fecha AS Fecha FROM GD1C2015.SARASA.Factura f, GD1C2015.SARASA.Itemfact i, GD1C2015.SARASA.Cliente c WHERE i.Itemfact_Factura_Numero= f.Factura_Numero AND f.Factura_Cliente_Id=c.Cliente_Id AND i.Itemfact_Pagado=1 AND c.Cliente_Id=" + this.usuario.ClienteId+ " GROUP BY f.Factura_Numero, f.Factura_Fecha ORDER BY f.Factura_Fecha DESC");
            TablaDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }

        private void buttonDetalles_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.TablaDatos.CurrentRow.Cells[0].Value);
            Facturacion.FormVerFactura frmDetalles = new Facturacion.FormVerFactura(this, usuario, id);
            this.Hide();
            frmDetalles.Show();
        }

        private void TablaDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
