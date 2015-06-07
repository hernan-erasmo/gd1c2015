using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Consulta_Saldos
{
    public partial class FormConsulta : Form
    {
        Utils.Usuario usuario;
        Form formPadre;

        public FormConsulta(Form f, Utils.Usuario user)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormConsultaSaldo_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }
    }
}
