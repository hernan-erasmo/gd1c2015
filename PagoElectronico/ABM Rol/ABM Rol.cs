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
    public partial class ABM_Rol : Form
    {
        Form formPadre;
        Crear_Rol crearRol = new PagoElectronico.ABM_Rol.Crear_Rol();
        ModificarEliminarRol modificarEliminarRol = new PagoElectronico.ABM_Rol.ModificarEliminarRol();

        public void asignarPadre(Form padre)
        {
            this.formPadre = padre;
        }

        public ABM_Rol()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            crearRol.asignarPadre(this);
            this.Hide();
            crearRol.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            formPadre.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            modificarEliminarRol.asignarPadre(this);
            this.Hide();
            modificarEliminarRol.Show();
        }
    }
}
