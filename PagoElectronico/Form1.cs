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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnDeposito_Click(object sender, EventArgs e)
        {

        }

        //  Ir a ABM Cliente
        private void button5_Click(object sender, EventArgs e)
        {
            ABM_Cliente.Form1 frmCliente = new ABM_Cliente.Form1();
            this.Hide();
            frmCliente.Show();
        }

        //  Ir a ABM Cuenta
        private void button6_Click(object sender, EventArgs e)
        {
            ABM_Cuenta.Form1 frmCuenta = new ABM_Cuenta.Form1();
            this.Hide();
            frmCuenta.Show();
        }

        //  Ir a ABM Usuario
        private void button8_Click(object sender, EventArgs e)
        {
            ABM_de_Usuario.Form1 frmUsuario = new ABM_de_Usuario.Form1();
            this.Hide();
            frmUsuario.Show();
        }

        //  Ir a ABM Rol
        private void button7_Click(object sender, EventArgs e)
        {
            ABM_Rol.Form1 frmRol = new ABM_Rol.Form1();
            this.Hide();
            frmRol.Show();
        }

        //  Ir a Consulta Saldos
        private void button12_Click(object sender, EventArgs e)
        {
            Consulta_Saldos.Form1 frmConsultaSaldos = new Consulta_Saldos.Form1();
            this.Hide();
            frmConsultaSaldos.Show();
        }

        //  Ir a Depositos
        private void button11_Click(object sender, EventArgs e)
        {
            Depositos.Form1 frmDepositos = new Depositos.Form1();
            this.Hide();
            frmDepositos.Show();
        }

        //  Ir a Facturacion
        private void button10_Click(object sender, EventArgs e)
        {
            Facturacion.Form1 frmFacturacion = new Facturacion.Form1();
            this.Hide();
            frmFacturacion.Show();
        }

        //  Ir a Listados
        private void button9_Click(object sender, EventArgs e)
        {
            Listados.Form1 frmListados = new Listados.Form1();
            this.Hide();
            frmListados.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        //  Ir a Login
        private void button19_Click(object sender, EventArgs e)
        {
            Login.Form1 frmLogin = new Login.Form1();
            this.Hide();
            frmLogin.Show();
        }

        //  Ir a Retiros
        private void button18_Click(object sender, EventArgs e)
        {
            Retiros.Form1 frmRetiros = new Retiros.Form1();
            this.Hide();
            frmRetiros.Show();

        }

        //  Ir a Transferencias
        private void button17_Click(object sender, EventArgs e)
        {
            Transferencias.Form1 frmTransferencias = new Transferencias.Form1();
            this.Hide();
            frmTransferencias.Show();

        }

        //  Ir a Listados
        private void button16_Click(object sender, EventArgs e)
        {
            Listados.Form1 frmListados = new Listados.Form1();
            this.Hide();
            frmListados.Show();
        }
    }
}
