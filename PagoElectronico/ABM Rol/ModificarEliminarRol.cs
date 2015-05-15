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
    public partial class ModificarEliminarRol : Form
    {
        Form formPadre;
        Modificar_Rol modificarRol = new PagoElectronico.ABM_Rol.Modificar_Rol();

        public void asignarPadre(Form padre)
        {
            this.formPadre = padre;
        }

        public ModificarEliminarRol()
        {
            InitializeComponent();
        }

        private void ModificarEliminarRol_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            formPadre.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            modificarRol.asignarPadre(this);
            this.Hide();
            modificarRol.Show();
        }
    }
}
