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


        //  Boton X: Fin de toda la aplicacion
        protected override void OnClosing(CancelEventArgs e)
        {
//            Application.Exit();
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

        //  Realizar depositos
        private void button11_Click(object sender, EventArgs e)
        {
            Depositos.FormDepositos formDeposito = new Depositos.FormDepositos(this,usuario);
            formDeposito.Show();
            this.Hide();
        }

        //  Abre el formulario de Login
        private void lklCerrarSesion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login.Login formLogin = new Login.Login();  //  Crea un formulario de Login
            formLogin.Show();   //  Muestra el formulario de login
            this.Close();        //  Oculta el formulario principal
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
            if (usuario.Funciones.Contains("Depositos"))
                btnDepositos.Visible = true;
            else
                btnDepositos.Visible = false;

            if (usuario.Funciones.Contains("Retiros"))
                btnRetiros.Visible = true;
            else
                btnRetiros.Visible = false;

            if (usuario.Funciones.Contains("Transferencias"))
                btnTransferencias.Visible = true;
            else
                btnTransferencias.Visible = false;

            if (usuario.Funciones.Contains("Facturacion"))
                btnFacturacion.Visible = true;
            else
                btnFacturacion.Visible = false;

            if (usuario.Funciones.Contains("ConsultaSaldo"))
                btnConsultaSaldos.Visible = true;
            else
                btnConsultaSaldos.Visible = false;

            if (usuario.Funciones.Contains("ListadoEstadístico"))
                btnListados.Visible = true;
            else
                btnListados.Visible = false;

            if (usuario.Funciones.Contains("BuscarRol"))
                btnABMRol.Visible = true;
            else
                btnABMRol.Visible = false;

            if (usuario.Funciones.Contains("BuscarTarjeta"))
                btnABMTarjeta.Visible = true;
            else
                btnABMTarjeta.Visible = false;

            if (usuario.Funciones.Contains("BuscarCuenta"))
                btnABMCuenta.Visible = true;
            else
                btnABMCuenta.Visible = false;

            if (usuario.Funciones.Contains("BuscarCliente"))
                btnABMCliente.Visible = true;
            else
                btnABMCliente.Visible = false;

            if (usuario.Funciones.Contains("BuscarUsuario"))
                btnABMUsuario.Visible = true;
            else
                btnABMUsuario.Visible = false;

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
            ABM_de_Usuario.FormModificarPass frm = new ABM_de_Usuario.FormModificarPass(this, usuario);
            this.Hide();
            frm.Show();
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
            ABM_Cuenta.FormBuscar abmCuenta = new ABM_Cuenta.FormBuscar(this, usuario, "ABMCuenta", "MenuPrincipal");
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

        private void btnABMUsuario_Click(object sender, EventArgs e)
        {

            ABM_de_Usuario.FormBuscar frm = new ABM_de_Usuario.FormBuscar(this, usuario,
                                                    "ABMUsuario", "MenuPrincipal");
//            ABM_de_Usuario.FormBuscar frm = new ABM_de_Usuario.FormBuscar(this, usuario);
            this.Hide();
            frm.Show();
        }
    }
}
