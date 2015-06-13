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
    public partial class FormVerFactura : Form
    {
        Form formPadre;
        Utils.Usuario usuario;
        int id_fact;

        public FormVerFactura(Form f, Utils.Usuario user, int id)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
            id_fact = id;

        }

        private void FormVerFactura_Load(object sender, EventArgs e)
        {
            TablaDatos.DataSource = Utils.Herramientas.ejecutarConsultaTabla("SELECT i.Itemfact_Id, i.Itemfact_Cuenta_Numero AS Cuenta, i.Itemfact_Descripcion AS Descripcion, i.Itemfact_Importe AS Importe, i.Itemfact_Fecha AS Fecha FROM GD1C2015.SARASA.Itemfact i WHERE i.Itemfact_Factura_Numero=" + this.id_fact);
            TablaDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }
    }
}
