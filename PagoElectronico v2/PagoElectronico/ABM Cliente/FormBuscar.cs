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
            else if (tipoFormPadre.Equals("ABM_Cliente.FormCrear"))
            {
                ((ABM_Cliente.FormCrear)formPadre).setUsuarioEncontrado(dataGridView1.SelectedCells[1].Value.ToString(),
                                                                        dataGridView1.SelectedCells[2].Value.ToString());
            }
            formPadre.Show();
            this.Dispose();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                List<SqlParameter> lista = Herramientas.GenerarListaDeParametros(
                                 "@cliente_id", dataGridView1.SelectedCells[0].Value.ToString());

                Herramientas.EjecutarStoredProcedure("SARASA.eliminar_cliente", lista);
                Herramientas.msebox_informacion("EXEC SARASA.eliminar_cliente @cliente_id=" + dataGridView1.SelectedCells[0].Value.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
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
                Herramientas.msebox_informacion("ClienteId: " + cliente.ClienteId);
            }
            else
                Herramientas.msebox_informacion("SIN CLIENTE ASOCIADO, ClienteId: " + cliente.ClienteId);

            ABM_Cliente.FormModificar frmModificar = new ABM_Cliente.FormModificar(this,usuario,cliente);
            this.Hide();
            frmModificar.Show();


/*
0   + "Cliente_Id 'Cliente ID',"
3   + "Cliente_Nombre 'Nombre',"
4   + "Cliente_Apellido 'Apellido',"
5   + "Cliente_Mail 'Mail',"
7   + "Cliente_Doc_Nro 'Numero Doc',"
8   + "Cliente_Fecha_Nacimiento 'Fecha Nacimiento',"
11  + "Cliente_Habilitado 'Cliente Habilitado',"
16  + "Cliente_Pais_Id 'Pais ID',"
17  + "Cliente_Tipodoc_Id 'Tipo Doc ID',"
18  + "Cliente_Dom_Calle 'Calle',"
19  + "Cliente_Dom_Numero 'Numero',"
20  + "Cliente_Dom_Piso 'Piso',"
21  + "Cliente_Dom_Depto 'Dpto'"


1   + "Usuario_Id 'User ID',"
2   + "Usuario_Username 'User',"
6   + "Tipodoc_Descripcion 'Tipo Doc',"
9   + "Pais_Nombre 'Pais',"
10  + "Cliente_Dom_Calle + ' ' + CONVERT(nvarchar(20),Cliente_Dom_Numero) + ', Piso '+ CONVERT(nvarchar(20),Cliente_Dom_Piso) + ', Dpto ' + Cliente_Dom_Depto 'Direccion',"
12  + "Usuario_Habilitado 'User Habilitado',"
13  + "Usuario_Fecha_Creacion 'User F.Creacion',"
14  + "Usuario_Fecha_Modificacion 'User F.Modificacion',"
15  + "Usuario_Pregunta_Sec 'Pregunta Sec',"
*/
        }
    }
}
