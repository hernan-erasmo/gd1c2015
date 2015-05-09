using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Entidades;



namespace PagoElectronico
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string usuario;
            //string pass;
            //usuario = textBox1.Text;
            //pass = textBox2.Text;

            string consulta = "SELECT * FROM ROL";

            label3.Text = consulta;

            cb.Text = "";
            cb.Items.Clear();//VACIA LOS ELEMENTOS DEL COMBO

            Herramientas.llenarComboBox(cb, consulta);
            // ENVIAR QUERY

            //RECIBIR RESULTADO



        }

        private void label3_Click(object sender, EventArgs e)
        {
            label3.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strquery = "delete from ROL where Nombre = '" + cb.Text + "'";
            label3.Text = strquery;
            Herramientas.ejecutarConsultaSimple(strquery);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Abrir filtro de busqueda
            
            ABM_Cliente.Form1 frmCliente = new ABM_Cliente.Form1();
      //      AltaUsuario formAltaUsuario = new AltaUsuario();

            this.Hide();
            //formAltaUsuario.Show();
            frmCliente.Show();
        }
    }
}
