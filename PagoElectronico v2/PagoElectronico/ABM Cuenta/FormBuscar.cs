using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Cuenta
{
    public partial class FormBuscar : Form
    {
        Utils.Usuario usuario;
        Form formPadre;

        public FormBuscar(Form f, Utils.Usuario user)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
        }

        private void FormBuscar_Load(object sender, EventArgs e)
        {

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            formPadre.Show();   //  Muestra el formulario padre
            this.Close();       //  Cierra el formulario
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            FormCrear formCrear = new FormCrear(this, usuario);
            formCrear.Show();
            this.Hide();
        }
    }
}
