using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using PagoElectronico.Utils;

namespace PagoElectronico.Login
{
    public partial class Login : Form
    {
        int idProcesoLogin;
        public Usuario usuario;
        public long fecha;
        public DateTime fechaDate;

        public Login()
        {
            InitializeComponent();
        }

        //  Boton X
        protected override void OnClosing(CancelEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            idProcesoLogin = 0;
            gbPermisos.Enabled = false;
            gbPermisos.Visible = false;
            fecha = Utils.Herramientas.leerArchivoConfig();
            fechaDate = Utils.Herramientas.convertirFechaATipoDatetime(fecha);

            //Ejecutar el stored procedure para setear la fecha
            string nombreSP = "SARASA.set_datetime_app";    //  Nombre del StoreProcedure

            List<SqlParameter> lista = Utils.Herramientas.GenerarListaDeParametros(
                            "@datetime_app", fechaDate);
            Utils.Herramientas.EjecutarStoredProcedure(nombreSP, lista);
            if (textBox1.CanFocus)
            {
                this.textBox1.Focus();
            }
            else
            {
                this.textBox1.Select();
                this.ActiveControl = textBox1;
            }

        }

        //  Login
        private void button1_Click_1(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Black;
            label2.ForeColor = Color.Black;
            lblInfo.Text = "";

                if (idProcesoLogin == 0)
                {
                    //  Validacion datos del formulario
                    if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
                    {
                        label1.ForeColor = Color.Red;
                        label2.ForeColor = Color.Red;
                        lblInfo.Text = "Completar todos datos del formulario";
                    }
                    else
                    {
                        //  Objeto usuario, con la informacion de la sesion
                        usuario = new Utils.Usuario();

                        //  Carga la fecha del sistema
                        usuario.Fecha = fechaDate.ToShortDateString();

                        //  Carga la informacion en usuario
                        usuario.Username = Convert.ToString(this.textBox1.Text);
                        usuario.Password = Herramientas.sha256_hash(textBox2.Text);

                        Herramientas.ejecutarAutenticacion(usuario);

                        switch (usuario.CodLogin)
                        {
                            case 0: //  Autenticacion correcta con mas de un rol
                                {

                                    idProcesoLogin = 1;
                                    gbLogin.Enabled = false;    //Bloquea los datos del usuario

                                    //  Busca los roles del usuario para que elija uno
                                    gbPermisos.Visible = true;
                                    gbPermisos.Enabled = true;
                                    List<SqlParameter> lista = Utils.Herramientas.GenerarListaDeParametros(
                                    "@usuario_id", Convert.ToInt32(usuario.UsuarioId));
                                    Herramientas.llenarComboBoxSP(comboBox1, "SARASA.cbx_rol", lista, true);
                                    btnLogin.Text = "Continuar..";
                                    break;
                                }
                            case -1: //  Usuario invalido
                                {
                                    label1.ForeColor = Color.Red;
                                    label2.ForeColor = Color.Red;
                                    lblInfo.Text = "Usuario incorrecto";
                                    break;
                                }
                            case -2: //  Usuario inhabilitado
                                {
                                    lblInfo.Text = "Usuario inhabilitado";
                                    break;
                                }
                            case -3: //  Usuario existe, password incorrecta
                                {
                                    //Registra el intento fallido en el usuario
                                    lblInfo.Text = "Password incorrecta";
                                    string nombreSP = "SARASA.Registrar_Intento_Fallido";    //  Nombre del StoreProcedure
                                    List<SqlParameter> listaParametros = Utils.Herramientas.GenerarListaDeParametros(
                                    "@usuario_id", this.usuario.UsuarioId);
                                    Utils.Herramientas.EjecutarStoredProcedure(nombreSP, listaParametros);

                                    //Si tiene 3 intentos fallidos, se inhabilita al usuario
                                    string nombreSP2 = "SARASA.Comprueba_Intentos_E_Inhabilita_Usuario";    //  Nombre del StoreProcedure
                                    List<SqlParameter> listaParam = Utils.Herramientas.GenerarListaDeParametros(
                                    "@usuario_id", this.usuario.UsuarioId);
                                    Utils.Herramientas.EjecutarStoredProcedure(nombreSP2, listaParam);

                                    //Registra como intento fallido en la tabla de log
                                    string nombreSP3 = "SARASA.Registra_Log";    //  Nombre del StoreProcedure
                                    List<SqlParameter> listaParam3 = Utils.Herramientas.GenerarListaDeParametros(
                                    "@usuario_id", this.usuario.UsuarioId, "@resultado", '0');
                                    Utils.Herramientas.EjecutarStoredProcedure(nombreSP3, listaParam3);

                                    break;
                                }
                            default: // Autenticacion correcta, con rol unico
                                {
                                    Herramientas.cargarFunciones(usuario);
                                    MenuPrincipal menuPrincipal = new MenuPrincipal();
                                    menuPrincipal.asignarPadre(this);
                                    menuPrincipal.asignarUsuario(usuario);
                                    string nombreSP = "SARASA.Reiniciar_Intentos";    //  Nombre del StoreProcedure
                                    List<SqlParameter> listaParametros = Utils.Herramientas.GenerarListaDeParametros(
                                    "@usuario_id", this.usuario.UsuarioId);

                                    Utils.Herramientas.EjecutarStoredProcedure(nombreSP, listaParametros);
                                    this.Hide();
                                    menuPrincipal.Show();

                                    //Registra como intento fallido en la tabla de log
                                    string nombreSP3 = "SARASA.Registra_Log";    //  Nombre del StoreProcedure
                                    List<SqlParameter> listaParam3 = Utils.Herramientas.GenerarListaDeParametros(
                                    "@usuario_id", this.usuario.UsuarioId, "@resultado", '1');
                                    Utils.Herramientas.EjecutarStoredProcedure(nombreSP3, listaParam3);

                                    break;
                                }
                        }
                    }
                }
                else //    Etapa final de login, si tiene más de un rol, muestra el combo
                {
                    // Carga las funciones del rol seleccionado
                    usuario.Rol = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Value;
                    usuario.RolId = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;

                    string nombreSP = "SARASA.Reiniciar_Intentos";    //  Nombre del StoreProcedure
                    List<SqlParameter> listaParametros = Utils.Herramientas.GenerarListaDeParametros(
                    "@usuario_id", this.usuario.UsuarioId);
                    Utils.Herramientas.EjecutarStoredProcedure(nombreSP, listaParametros);

                    //Registra como intento fallido en la tabla de log
                    string nombreSP3 = "SARASA.Registra_Log";    //  Nombre del StoreProcedure
                    List<SqlParameter> listaParam3 = Utils.Herramientas.GenerarListaDeParametros(
                    "@usuario_id", this.usuario.UsuarioId, "@resultado", '1');
                    Utils.Herramientas.EjecutarStoredProcedure(nombreSP3, listaParam3);


                    Herramientas.cargarFunciones(usuario);
                    MenuPrincipal menuPrincipal = new MenuPrincipal();
                    menuPrincipal.asignarPadre(this);
                    menuPrincipal.asignarUsuario(usuario);
                    this.Hide();
                    menuPrincipal.Show();

                }
            }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            gbPermisos.Enabled = false;
            gbPermisos.Visible = false;
            gbLogin.Enabled = true;
            textBox1.Text = "";
            textBox2.Text = "";
            idProcesoLogin = 0;
            btnLogin.Text = "Login";
        }

        }
}
