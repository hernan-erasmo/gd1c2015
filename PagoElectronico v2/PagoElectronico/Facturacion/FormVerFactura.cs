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
            DataTable dt = (DataTable)usuario.llenarDataGridItemFactura(id_fact);
            if (dt != null)
            {
                TablaDatos.DataSource = dt;
            }
            else
            {
                Utils.Herramientas.msebox_informacion("No hay datos");
            }
            TablaDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }
    }
}
