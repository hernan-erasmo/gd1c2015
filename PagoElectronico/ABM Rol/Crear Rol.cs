using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Rol
{
    public partial class Crear_Rol : Form
    {
        Form formPadre;

        public void asignarPadre(Form padre)
        {
            this.formPadre = padre;
        }

        public Crear_Rol()
        {
            InitializeComponent();
        }

        private void Crear_Rol_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            formPadre.Show();
        }
    }
}
