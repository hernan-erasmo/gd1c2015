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

        private void txtNumero_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

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
    }
}
