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
            Herramientas.llenarComboBoxSP(cbxRol,"SARASA.cbx_rol",
                        Herramientas.GenerarListaDeParametros("@usuario_id", 0),false);

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            formPadre.Show();
            this.Dispose();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            btnModificar.Enabled = false;
            btnCrear.Enabled = false;
            btnEliminar.Enabled = false;


            lblEstadoBusqueda.Text = "Ejecutando busqueda...";

            //  ARMA LA QUERY A EJECUTAR BASADO EN LOS FILTROS
            string queryConsulta = Filtros.filtroBuscarUsuario(txtUsuario.Text, "" + ((KeyValuePair<string, string>)cbxRol.SelectedItem).Key);

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
	}
}
