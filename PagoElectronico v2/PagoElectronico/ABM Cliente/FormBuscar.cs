using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Utils;
using System.Collections;
using System.Data.SqlClient;

namespace PagoElectronico.ABM_Cliente
{
    public partial class FormBuscar : Form
    {
        Usuario usuario;
        ArrayList funciones;
        Form formPadre;
        string tipoFormBusqueda;
        string tipoFormPadre;

        public FormBuscar()
        {
            InitializeComponent();
        }

        public FormBuscar(Form f, Utils.Usuario user, string tipoFormBusqueda, string tipoFormPadre)
        {
            InitializeComponent();
            this.formPadre = f;
            this.usuario = user;
            this.tipoFormBusqueda = tipoFormBusqueda;
            this.tipoFormPadre = tipoFormPadre;
        }

        //  Boton X
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Dispose();
            formPadre.Show();
        }

        private void FormBuscar_Load(object sender, EventArgs e)
        {
            btnModificar.Enabled = false;
            btnModUsuario.Enabled = false;
            btnEliminar.Enabled = false;
            btnAceptar.Enabled = false;
            btnAsociarTC.Enabled = false;


            if (tipoFormBusqueda.Equals("ABMCliente"))
            {
                btnAceptar.Visible = false;
            }
            else if (tipoFormBusqueda.Equals("BuscarUsuario")) 
            {
                flowLayoutPanel1.Visible = false;
                txtNombre.Enabled = false;
                txtApellido.Enabled = false;
                txtNumDoc.Enabled = false;
                cbxTipoDoc.Enabled = false;
                txtMail.Enabled = false;
            }
            else if (tipoFormBusqueda.Equals("BuscarCliente"))
            {
                flowLayoutPanel1.Visible = false;
            }


            //  Muestra las funciones segun la lista de funciones
            if (usuario.Funciones.Contains("CrearCliente"))
                btnCrear.Visible=true;
            else
                btnCrear.Visible = false;

            if (usuario.Funciones.Contains("EliminarCliente"))
                btnEliminar.Visible = true;
            else
                btnEliminar.Visible = false;

            if (usuario.Funciones.Contains("ModificarUsuarioCliente"))
                btnModUsuario.Visible = true;
            else
                btnModUsuario.Visible = false;

            if (usuario.Funciones.Contains("ModificarCliente"))
                btnModificar.Visible = true;
            else
                btnModificar.Visible = false;

            if (usuario.Funciones.Contains("AsociarTarjeta"))
                btnAsociarTC.Visible = true;
            else
                btnAsociarTC.Visible = false;



            Herramientas.llenarComboBoxSP(cbxTipoDoc, "SARASA.cbx_tipodoc", null, false);
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            ABM_Cliente.FormCrear frm = new ABM_Cliente.FormCrear(this, usuario);
            this.Hide();
            frm.Show();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            lblEstadoBusqueda.Text = "";    //  Indica estado de la busqueda

            txtNombre.Text = "";
            txtApellido.Text = "";
            txtNumDoc.Text = "";
            txtMail.Text = "";
            txtUsuario.Text = "";

            cbxTipoDoc.SelectedIndex = 0;

            btnModificar.Enabled = false;
            btnModUsuario.Enabled = false;
            btnEliminar.Enabled = false;
            btnAceptar.Enabled = false;
            btnAsociarTC.Enabled = false;



            //  Limpiar la tabla de resultados
            dataGridView1.DataSource = null;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            btnModificar.Enabled = false;
            btnModUsuario.Enabled = false;
            btnEliminar.Enabled = false;
            btnAceptar.Enabled = false;
            btnAsociarTC.Enabled = false;

            lblEstadoBusqueda.Text = "Ejecutando busqueda...";

            //  ARMA LA QUERY A EJECUTAR BASADO EN LOS FILTROS
            string queryConsulta;
            if (tipoFormBusqueda.Equals("BuscarUsuario")) //  Busca usuarios sin clientes asociados
            {
                queryConsulta = Filtros.filtroBuscarUsuario(txtUsuario.Text);
            }
            else 
            {
                queryConsulta = Filtros.filtroBuscarCliente(txtUsuario.Text, txtNombre.Text, txtApellido.Text, txtMail.Text, txtNumDoc.Text, "" + ((KeyValuePair<string, string>)cbxTipoDoc.SelectedItem).Key);
            }
            

            Herramientas.msebox_informacion(queryConsulta);

            DataTable resultados;
            try
            {
                resultados = Herramientas.ejecutarConsultaTabla(queryConsulta);
                dataGridView1.DataSource = resultados;

                if (tipoFormBusqueda.Equals("BuscarUsuario")) //  Busca usuarios sin clientes asociados
                { }
                else
                {
                    dataGridView1.Columns["Cliente ID"].Visible = false;
                    dataGridView1.Columns["User ID"].Visible = false;
                    dataGridView1.Columns["Pais ID"].Visible = false;
                    dataGridView1.Columns["Tipo Doc ID"].Visible = false;
                    dataGridView1.Columns["Calle"].Visible = false;
                    dataGridView1.Columns["Numero"].Visible = false;
                    dataGridView1.Columns["Piso"].Visible = false;
                    dataGridView1.Columns["Dpto"].Visible = false;
                    dataGridView1.Columns["Pregunta Sec"].Visible = false;
                } 

                lblEstadoBusqueda.Text = "Se encontraron " + dataGridView1.RowCount + " filas";

                if (dataGridView1.RowCount > 0)
                {
                    btnAceptar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnModificar.Enabled = true;
                    btnModUsuario.Enabled = true;
                    btnAsociarTC.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
                lblEstadoBusqueda.Text = "Error al realizar la busqueda";
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            if (tipoFormPadre.Equals("ABM_Tarjeta.FormBuscar"))
            {
//                        public void setClienteEncontrado(string clienteId, string nombre, string apellido) 
                ((ABM_Tarjeta.FormBuscar)formPadre).setClienteEncontrado(dataGridView1.SelectedCells[0].Value.ToString(),
                                                                        dataGridView1.SelectedCells[3].Value.ToString(),
                                                                        dataGridView1.SelectedCells[4].Value.ToString());
            }
            else if (tipoFormPadre.Equals("ABM_Cuenta.FormBuscar"))
            {
                ((ABM_Cuenta.FormBuscar)formPadre).setClienteEncontrado(dataGridView1.SelectedCells[0].Value.ToString(),
                                                                        dataGridView1.SelectedCells[3].Value.ToString(),
                                                                        dataGridView1.SelectedCells[4].Value.ToString());

            }
            else if (tipoFormPadre.Equals("ABM_Cliente.FormCrear"))
            {
                ((ABM_Cliente.FormCrear)formPadre).setUsuarioEncontrado(dataGridView1.SelectedCells[1].Value.ToString(),
                                                                        dataGridView1.SelectedCells[2].Value.ToString());
            }
            else if (tipoFormPadre.Equals("Depositos.FormDepositos"))
            {
                ((Depositos.FormDepositos)formPadre).setClienteEncontrado(dataGridView1.SelectedCells[0].Value.ToString(),
                                                                        dataGridView1.SelectedCells[3].Value.ToString(),
                                                                        dataGridView1.SelectedCells[4].Value.ToString());
            }
            else if (tipoFormPadre.Equals("Retiros.FormRetiros"))
            {
                ((Retiros.FormRetiros)formPadre).setClienteEncontrado(dataGridView1.SelectedCells[0].Value.ToString(),
                                                                        dataGridView1.SelectedCells[3].Value.ToString(),
                                                                        dataGridView1.SelectedCells[4].Value.ToString(),
                                                                        dataGridView1.SelectedCells[7].Value.ToString());
            }
            else if (tipoFormPadre.Equals("Transferencias.FormTransferencias"))
            {
                ((Transferencias.FormTransferencias)formPadre).setClienteEncontrado(dataGridView1.SelectedCells[0].Value.ToString(),
                                                                        dataGridView1.SelectedCells[3].Value.ToString(),
                                                                        dataGridView1.SelectedCells[4].Value.ToString());
            }
            else if (tipoFormPadre.Equals("Facturacion.FormFacturacion"))
            {
                ((Facturacion.FormFacturacion)formPadre).setClienteEncontrado(dataGridView1.SelectedCells[0].Value.ToString(),
                                                                        dataGridView1.SelectedCells[3].Value.ToString(),
                                                                        dataGridView1.SelectedCells[4].Value.ToString());
            }


            formPadre.Show();
            this.Dispose();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string msj = "Seguro que quiere ELIMINAR el CLIENTE \"" +
                dataGridView1.SelectedCells[4].Value.ToString() + ", " +
                dataGridView1.SelectedCells[3].Value.ToString() + " (" +
                dataGridView1.SelectedCells[0].Value.ToString() + ")\"?\n";
            msj += "SE REALIZA UNA BAJA LÓGICA";

            var result = MessageBox.Show(msj, "Eliminar cliente",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);//, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.OK)
            {
                List<SqlParameter> lista = Herramientas.GenerarListaDeParametros(
                                 "@cliente_id", dataGridView1.SelectedCells[0].Value.ToString());
                Herramientas.EjecutarStoredProcedure("SARASA.eliminar_cliente", lista);
            }
        }

        //  Modificar Cliente
        private void btnModificar_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            cliente.ClienteId = dataGridView1.SelectedCells[0].Value.ToString();

            if (cliente.ClienteId != "") 
            {
                cliente.Nombre = dataGridView1.SelectedCells[3].Value.ToString();
                cliente.Apellido = dataGridView1.SelectedCells[4].Value.ToString();
                cliente.Mail = dataGridView1.SelectedCells[5].Value.ToString();

                cliente.TipoDocId = dataGridView1.SelectedCells[17].Value.ToString();
                cliente.NumeroDoc = dataGridView1.SelectedCells[7].Value.ToString();

                cliente.PaisId = dataGridView1.SelectedCells[16].Value.ToString();
                cliente.DomCalle = dataGridView1.SelectedCells[18].Value.ToString();
                cliente.DomNumero = dataGridView1.SelectedCells[19].Value.ToString();
                cliente.DomPiso = dataGridView1.SelectedCells[20].Value.ToString();
                cliente.DomDpto = dataGridView1.SelectedCells[21].Value.ToString();

                cliente.FechaNacimiento = dataGridView1.SelectedCells[8].Value.ToString();
                cliente.Habilitado = bool.Parse(dataGridView1.SelectedCells[11].Value.ToString());
            }
            else
                Herramientas.msebox_informacion("SIN CLIENTE ASOCIADO, ClienteId: " + cliente.ClienteId);

            ABM_Cliente.FormModificar frmModificar = new ABM_Cliente.FormModificar(this,usuario,cliente);
            this.Hide();
            frmModificar.Show();
        }

        private void btnAsociarTC_Click(object sender, EventArgs e)
        {
            string clienteId = dataGridView1.SelectedCells[0].Value.ToString();
            string clienteDesc = dataGridView1.SelectedCells[4].Value.ToString() + ", " +
                dataGridView1.SelectedCells[3].Value.ToString() + " ( " + clienteId + ")";

            ABM_Tarjeta.FormAsociar frmAsociar = new ABM_Tarjeta.FormAsociar(this, clienteId, clienteDesc);
            this.Hide();
            frmAsociar.Show();
        }
    }
}
