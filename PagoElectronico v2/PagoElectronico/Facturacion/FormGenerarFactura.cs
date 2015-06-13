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
    public partial class FormGenerarFactura : Form
    {
        Form formPadre;
        Utils.Usuario usuario;
        string factura;

        public FormGenerarFactura(Form f, Utils.Usuario user, string factura_id)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
            factura = factura_id;
        }

        private void FormGenerarFactura_Load(object sender, EventArgs e)
        {
            this.label2.Text = factura;
            TablaDatos.DataSource = Utils.Herramientas.ejecutarConsultaTabla("SELECT i.Itemfact_Id, i.Itemfact_Cuenta_Numero AS Cuenta, i.Itemfact_Descripcion AS Descripcion, i.Itemfact_Importe AS Importe, i.Itemfact_Fecha AS Fecha FROM GD1C2015.SARASA.Itemfact i WHERE i.Itemfact_Factura_Numero=" + this.factura);
            TablaDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            double total = 0;
            foreach (DataGridViewRow row in TablaDatos.Rows)
            {
                total += Convert.ToDouble(row.Cells[3].Value);
            }
            this.label5.Text = Convert.ToString(total);

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }
    }
}
