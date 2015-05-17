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

namespace PagoElectronico.ABM_Tarjeta
{
    public partial class FormBuscar : Form
    {
        Form formPadre;
        FormAsociar asociarTarjeta;
        FormBuscar modificarTarjeta;
        Tarjeta tarjeta = new Tarjeta();

        public FormBuscar()
        {
            InitializeComponent();
        }

        public FormBuscar(Form f)
        {
            InitializeComponent();
            formPadre = f;

        }

        //  Carga el formulario
        private void FormBuscar_Load(object sender, EventArgs e)
        {
            //  Si es administrador el txtCliente es igual a ""
            //  y el boton btnBuscarCli esta habilitado
            //  Si es cliente el txtCliente es el nombre del cliente
            btnBuscarClie.Enabled = true;
            txtCliente.Enabled = false;

            //  Si no hay un cliente, no se puede asociar una tarjeta
            if (txtCliente.Text == "")
            {
                btnAsociar.Enabled = false;
            }
            else
            {
                btnAsociar.Enabled = true;
            }

            //  Llena el combo de emisor
            cbxEmisor.Text = "";
            cbxEmisor.Items.Clear();//VACIA LOS ELEMENTOS DEL COMBO
            Herramientas.llenarComboBox(cbxEmisor, "SELECT * FROM test.EmisorTC");

            btnDesasociar.Enabled = false;
            btnModificar.Enabled = false;

        }

        
        
        
        
        //  Metodos publicos
        //  Ingresar texto en el txtCliente
        public void setClienteTexto(string sCliente)
        {
            this.txtCliente.Text = sCliente;

        }

        //  BUSCAR: Ejecuta el SP para buscar todas las tarjetas de credito
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DataTable resultados;
            try
            {
                List<SqlParameter> lista = Herramientas.GenerarListaDeParametros(
//                    "@cliente", txtCliente.Text,
//                    "@numeroTarjeta", txtNumero.Text,
                    "@emisorTarjeta", cbxEmisor.Text);

                resultados = Herramientas.EjecutarStoredProcedure("test.Buscar_Tarjetas", lista);
                dataGridView1.DataSource = resultados;

                if(dataGridView1.RowCount > 0){ // Hay resultados habilita Desasociar y Modificar
                    btnDesasociar.Enabled = true;
                    btnModificar.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }

        //  Desasociar
        private void btnDesasociar_Click(object sender, EventArgs e)
        {
            //  La tarjeta:

        }


        //  Buscar Cliente (solo para administrador)
        private void btnBuscarClie_Click(object sender, EventArgs e)
        {
            FormBuscarClie formBuscarCliente = new FormBuscarClie(this);
            formBuscarCliente.Show();
            this.Hide();
        }

        //  Limpiar filtros de busqueda (OK)
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            //  Si es administrador tambien limpia el campo de cliente
            if(btnBuscarClie.Enabled == true)
                txtCliente.Text = "";

            txtNumero.Text = "";
            cbxEmisor.Text = "";

            btnDesasociar.Enabled = false;
            btnModificar.Enabled = false;

            //  Limpiar la tabla de resultados
            dataGridView1.DataSource = null;
        }

        //  Volver (OK)
        private void btnVolver_Click(object sender, EventArgs e)
        {
            formPadre.Show();   //  Muestra el formulario padre
            this.Close();       //  Cierra el formulario
        }

        //  Modificar: Recupera los valores del dataGridView1
        private void btnModificar_Click(object sender, EventArgs e)
        {

            //  tarjeta = new Tarjeta();
            tarjeta.Numero = dataGridView1.SelectedCells[0].Value.ToString();
            tarjeta.FechaEmision = dataGridView1.SelectedCells[1].Value.ToString();
            tarjeta.FechaVencimiento = dataGridView1.SelectedCells[2].Value.ToString();
            tarjeta.CodigoSeguridad = dataGridView1.SelectedCells[3].Value.ToString();
            tarjeta.Emisor = dataGridView1.SelectedCells[4].Value.ToString();
           // txtCliente.Text = dataGridView1.SelectedRows[0].Index.ToString();

            //  Con la tarjeta seleccionada
            //  Abrir un formulario con los datos de la tarjeta
            FormModificar formModificar = new FormModificar(this, txtCliente.Text, tarjeta);
            formModificar.Show();
            this.Hide();
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            //  Si no hay un cliente, no se puede asociar una tarjeta
            if(txtCliente.Text == ""){
                btnAsociar.Enabled = false;
            }
            else{
                btnAsociar.Enabled = true;
            }
        }

        //  Asociar
        private void btnAsociar_Click(object sender, EventArgs e)
        {
            FormAsociar formAsociar = new FormAsociar(this,txtCliente.Text);
            formAsociar.Show();
            this.Hide();
        }


    }
}
