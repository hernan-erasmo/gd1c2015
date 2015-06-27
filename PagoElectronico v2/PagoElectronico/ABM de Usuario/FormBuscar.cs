/*
 * Creado por SharpDevelop.
 * Usuario: L0613363
 * Fecha: 16/06/2015
 * Hora: 03:08 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using PagoElectronico.Utils;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PagoElectronico.ABM_de_Usuario
{
	/// <summary>
	/// Description of FormBuscar.
	/// </summary>
	public partial class FormBuscar : Form
	{
        Usuario usuario;
        ArrayList funciones;
        Form formPadre;
        string tipoFormBusqueda;
        string tipoFormPadre;

        public FormBuscar(Form f, Usuario user, string tipoFormBusqueda, string tipoFormPadre)
        {
            InitializeComponent();
            this.formPadre = f;
            this.usuario = user;
            this.tipoFormBusqueda = tipoFormBusqueda;
            this.tipoFormPadre = tipoFormPadre;
        }

        public FormBuscar(Form f, Usuario user)//, string tipoFormBusqueda, string tipoFormPadre)
        {
            InitializeComponent();
            this.formPadre = f;
            this.usuario = user;
            //this.tipoFormBusqueda = tipoFormBusqueda;
            //this.tipoFormPadre = tipoFormPadre;
        }


        private void FormBuscar_Load(object sender, EventArgs e)
        {
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;

            if (tipoFormBusqueda.Equals("ABMUsuario"))
            {
                btnAceptar.Visible = false;
            }
            else if (tipoFormBusqueda.Equals("BuscarUsuario"))
            {
                flowLayoutPanel1.Visible = false;
            }

            Herramientas.llenarComboBoxSP(cbxRol, "SARASA.cbx_rol",
                        Herramientas.GenerarListaDeParametros("@usuario_id", 0), false);

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            formPadre.Show();
            this.Dispose();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;


            lblEstadoBusqueda.Text = "Ejecutando busqueda...";

            //  ARMA LA QUERY A EJECUTAR BASADO EN LOS FILTROS
            string queryConsulta;
            if (tipoFormBusqueda.Equals("BuscarUsuario")) //  Busca usuarios sin clientes asociados
            {
                queryConsulta = Filtros.filtroBuscarUsuario(txtUsuario.Text, "" + ((KeyValuePair<string, string>)cbxRol.SelectedItem).Key,true);
            }
            else
            {
                queryConsulta = Filtros.filtroBuscarUsuario(txtUsuario.Text, "" + ((KeyValuePair<string, string>)cbxRol.SelectedItem).Key,false);
            }


            Herramientas.msebox_informacion(queryConsulta);

            DataTable resultados;
            try
            {
                resultados = Herramientas.ejecutarConsultaTabla(queryConsulta);
                dataGridView1.DataSource = resultados;

                //dataGridView1.Columns["Cliente ID"].Visible = false;
                //dataGridView1.Columns["User ID"].Visible = false;
                //dataGridView1.Columns["Pais ID"].Visible = false;
                //dataGridView1.Columns["Tipo Doc ID"].Visible = false;
                //dataGridView1.Columns["Calle"].Visible = false;
                //dataGridView1.Columns["Numero"].Visible = false;
                //dataGridView1.Columns["Piso"].Visible = false;
                //dataGridView1.Columns["Dpto"].Visible = false;
                //dataGridView1.Columns["Pregunta Sec"].Visible = false;


                lblEstadoBusqueda.Text = "Se encontraron " + dataGridView1.RowCount + " filas";

                if (dataGridView1.RowCount > 0)
                {
                    btnEliminar.Enabled = true;
                    btnModificar.Enabled = true;
                    btnCrear.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
                lblEstadoBusqueda.Text = "Error al realizar la busqueda";
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            lblEstadoBusqueda.Text = "";    //  Indica estado de la busqueda
            txtUsuario.Text = "";
            cbxRol.SelectedIndex = 0;

            btnCrear.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;

            //  Limpiar la tabla de resultados
            dataGridView1.DataSource = null;
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            ABM_de_Usuario.FormCrear frm = new ABM_de_Usuario.FormCrear(this, usuario);
            frm.Show();
            this.Hide();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow != null) //Valida que se haya seleccionado una fila del DataGridView
            {
                try
                {
                    List<SqlParameter> lista = Utils.Herramientas.GenerarListaDeParametros(
                       "@Usuario_Id", dataGridView1.CurrentRow.Cells[0].Value.ToString());

                    Herramientas.EjecutarStoredProcedure("SARASA.eliminar_usuario", lista);
 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                }
            }
            else
            {
                Utils.Herramientas.msebox_informacion("Por favor, seleccionar una fila de usuario");
            }




        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            if (tipoFormPadre.Equals("ABM_Cliente.FormCrear"))
            {
                ((ABM_Cliente.FormCrear)formPadre).setUsuarioEncontrado(dataGridView1.SelectedCells[0].Value.ToString(),
                                                                        dataGridView1.SelectedCells[1].Value.ToString());
            }

            formPadre.Show();
            this.Dispose();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells[0].Value);
            string nombre = Convert.ToString(this.dataGridView1.CurrentRow.Cells[1].Value);
            ABM_de_Usuario.FormModificar frm = new ABM_de_Usuario.FormModificar(this,id,nombre);
            this.Hide();
            frm.Show();
        }
	}
}
