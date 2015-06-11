using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace PagoElectronico
{
    public partial class MenuPrincipal : Form
    {
        Utils.Usuario usuario;
        Form formPadre;

        public MenuPrincipal()
        {
            InitializeComponent();
        }

        public void asignarPadre(Form padre)
        {
            this.formPadre = padre;
        }

        public void asignarUsuario(Utils.Usuario user)
        {
            this.usuario = user;
            this.lklLogin.Text = user.Username + " ("+ user.Apellido + ", " + user.Nombre + ")";
        }

/*        
        private void button5_Click(object sender, EventArgs e)
        {
            abmCliente.asignarPadre(this);
            this.Hide();
            abmCliente.Show();
        }
*/

        //  Realizar depositos
        private void button11_Click(object sender, EventArgs e)
        {
            Depositos.FormDepositos formDeposito = new Depositos.FormDepositos(this,usuario);
            formDeposito.Show();
            this.Hide();
        }

/*
        private void button7_Click(object sender, EventArgs e)
        {
            abmRol.asignarPadre(this);
            this.Hide();
            abmRol.Show();
        }
*/
        //  Abre el formulario de Login
        private void lklCerrarSesion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();        //  Oculta el formulario principal
            Login.Login formLogin = new Login.Login(this);  //  Crea un formulario de Login
            formLogin.Show();   //  Muestra el formulario de login

        }

        private void lblLoginDatos_Click(object sender, EventArgs e)
        {
            this.Text = "Modificar cliente";
        }

        //  ABM Tarjeta
        private void btnABMTarjeta_Click(object sender, EventArgs e)
        {
            ABM_Tarjeta.FormBuscar abmTarjeta = new ABM_Tarjeta.FormBuscar(this, usuario);
            this.Hide();    //  Oculta el menu
            abmTarjeta.Show();
        }

        //  Salir
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
        }

        //  Depositos
        private void btnDepositos_Click(object sender, EventArgs e)
        {
            Depositos.FormDepositos frmDeposito = new Depositos.FormDepositos(this, usuario);
            this.Hide();    //  Oculta el menu
            frmDeposito.Show();
        }

        private void btnABMRol_Click(object sender, EventArgs e)
        {
            ABM_Rol.FormBuscar frmBuscar = new ABM_Rol.FormBuscar(this,usuario);
            frmBuscar.Show();
            this.Hide();

        }

        private void lklLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Utils.Herramientas.msebox_informacion("" + usuario.Funciones.Count);

        }

        private void btnRetiros_Click(object sender, EventArgs e)
        {
            Retiros.FormRetiros frmRetiro = new Retiros.FormRetiros(this, usuario);
            this.Hide();    //  Oculta el menu
            frmRetiro.Show();
        }

        private void btnTransferencias_Click(object sender, EventArgs e)
        {
            Transferencias.FormTransferencias frmTransferencia = new Transferencias.FormTransferencias(this, usuario);
            this.Hide();
            frmTransferencia.Show();
        }

        private void btnABMCuenta_Click(object sender, EventArgs e)
        {
            ABM_Cuenta.FormBuscar abmCuenta = new ABM_Cuenta.FormBuscar(this, usuario);
            this.Hide();    //  Oculta el menu
            abmCuenta.Show();
        }

        private void btnFacturacion_Click(object sender, EventArgs e)
        {
            Facturacion.FormFacturacion frm = new Facturacion.FormFacturacion(this,usuario);
            this.Hide();
            frm.Show();
        }

        private void btnABMCliente_Click(object sender, EventArgs e)
        {
            //ABM_Cliente.FormCrear frm = new ABM_Cliente.FormCrear(this, usuario);
            ABM_Cliente.FormBuscar frm = new ABM_Cliente.FormBuscar(this, usuario, "ABMCliente", "MenuPrincipal");
            this.Hide();
            frm.Show();
            
        }

        private void btnListados_Click(object sender, EventArgs e)
        {
            Listados.FormListados frm = new Listados.FormListados(this,usuario);
            this.Hide();
            frm.Show();
        }

        private void btnConsultaSaldos_Click(object sender, EventArgs e)
        {
            Consulta_Saldos.FormConsulta frm = new Consulta_Saldos.FormConsulta(this, usuario);
            this.Hide();
            frm.Show();
        }
    }
}
