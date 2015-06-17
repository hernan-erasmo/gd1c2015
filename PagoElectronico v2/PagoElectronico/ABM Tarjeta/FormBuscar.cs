﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using PagoElectronico.Utils;
using System.Collections;

namespace PagoElectronico.ABM_Tarjeta
{
    public partial class FormBuscar : Form
    {
        Usuario usuario;
        ArrayList funciones;
        string clienteId = "0";
        Form formPadre;

        public FormBuscar()
        {
            InitializeComponent();
        }

        public FormBuscar(Form f, Utils.Usuario user)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
        }

        //  Boton X
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Dispose();
            formPadre.Show();
        }

        //  Carga el formulario
        private void FormBuscar_Load(object sender, EventArgs e)
        {

            //  Inicializa estado de fecha del Filtro
            dtpFechaEmisionDesde.Enabled = false;
            dtpFechaEmisionHasta.Enabled = false;
            dtpFechaVencimientoDesde.Enabled = false;
            dtpFechaVencimientoHasta.Enabled = false;


            //  Llena el combo de emisor
            cbxEmisor.Items.Clear();//VACIA LOS ELEMENTOS DEL COMBO
            Herramientas.llenarComboBoxSP(cbxEmisor, "SARASA.cbx_emisor", null, false);

            txtCliente.Text = "";
            clienteId = "0";

            btnAsociar.Enabled = false;
            btnDesasociar.Enabled = false;
            btnModificar.Enabled = false;
            txtCliente.Enabled = false;

            //  Muestra las funciones segun la lista de funciones
            if (usuario.Funciones.Contains("AsociarTarjeta"))
                btnAsociar.Visible=true;
            else
                btnAsociar.Visible = false;

            if (usuario.Funciones.Contains("DesasociarTarjeta"))
                btnDesasociar.Visible = true;
            else
                btnDesasociar.Visible = false;


            if (usuario.Funciones.Contains("ModificarTarjeta"))
                btnModificar.Visible = true;
            else
                btnModificar.Visible = false;


            if (usuario.Funciones.Contains("AceptarTarjeta"))
                btnAceptar.Visible = true;
            else
                btnAceptar.Visible = false;


            //if (usuario.Funciones.Contains("BuscarTarjeta"))
            //{
            //    btnBuscar.Visible = true;
            //    btnLimpiar.Visible = true;
            //}
            //else
            //{
            //    btnBuscar.Visible = false;
            //    btnLimpiar.Visible = false;
            //}

            
            //  Si el usuario tiene rol de administrador
            if (usuario.Rol.Equals("Administrador"))
            { 
                btnBuscarClie.Visible = true;
                txtCliente.Text = "";
            }
            else
            {
                btnBuscarClie.Visible = false;
                txtCliente.Text = usuario.Username;

            }

            if (usuario.RolId == "1")
            {
                btnBuscarClie.Visible = true;
                btnAsociar.Enabled = false;
            }
            else
            {
                btnBuscarClie.Visible = false;
                btnAsociar.Enabled = true;
                txtCliente.Text = usuario.Apellido + ", " + usuario.Nombre + " (" + usuario.ClienteId + ")";
                clienteId = "" + usuario.ClienteId;
            }
            //  Si es administrador el txtCliente es igual a ""
            //  y el boton btnBuscarCli esta habilitado
            //  Si es cliente el txtCliente es el nombre del cliente
            

            //  Si no hay un cliente, no se puede asociar una tarjeta
            //if (txtCliente.Text == "")
            //    btnAsociar.Enabled = false;
            //else
            //    btnAsociar.Enabled = true;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

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
            lblEstadoBusqueda.Text = "Ejecutando busqueda...";

            string fechaEmisionDesde, fechaEmisionHasta;
            string fechaVencimientoDesde, fechaVencimientoHasta;

            string cliente = this.clienteId;
            string numero = txtNumero.Text;
            string emisor = cbxEmisor.Text;

            if (chkFechaEmision.Checked)
            {
                fechaEmisionDesde = dtpFechaEmisionDesde.Value.ToShortDateString();
                fechaEmisionHasta = dtpFechaEmisionHasta.Value.ToShortDateString();
            }
            else 
            {
                fechaEmisionDesde = fechaEmisionHasta = "";
            }

            if(chkFechaVencimiento.Checked)
            {
                fechaVencimientoDesde = dtpFechaVencimientoDesde.Value.ToShortDateString();
                fechaVencimientoHasta = dtpFechaVencimientoHasta.Value.ToShortDateString();
            }
            else
            {
                fechaVencimientoDesde = fechaVencimientoHasta = "";
            }



            //  ARMA LA QUERY A EJECUTAR BASADO EN LOS FILTROS
            string queryConsulta = Utils.Filtros.filtroBuscarTarjeta(cliente,numero,emisor,
            fechaEmisionDesde,fechaEmisionHasta,fechaVencimientoDesde,fechaVencimientoHasta);

            Herramientas.msebox_informacion(queryConsulta);



            DataTable resultados;
            try
            {
                resultados = Herramientas.ejecutarConsultaTabla(queryConsulta);
                dataGridView1.DataSource = resultados;

                dataGridView1.Columns["Cliente Id"].Visible = false;
                dataGridView1.Columns["Nombre"].Visible = false;
                dataGridView1.Columns["Apellido"].Visible = false;

                lblEstadoBusqueda.Text = "Se encontraron " + dataGridView1.RowCount + " filas";

                if(dataGridView1.RowCount > 0){ // Hay resultados habilita Desasociar y Modificar
                    btnDesasociar.Enabled = true;
                    btnModificar.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
                lblEstadoBusqueda.Text = "Error al realizar la busqueda";
            }
        }


        //  Desasociar (LO ELIMINA DE LA BASE)
        private void btnDesasociar_Click(object sender, EventArgs e)
        {

            //  EJECUTA EL STORE PROCEDURE QUE GRABA LOS DATOS EN LA TABLA
            string nombreSP = "SARASA.Desasociar_Tarjeta";
            

            //Utils.Herramientas.msebox_informacion(idTarjeta);
            if (this.dataGridView1.CurrentRow != null) //Valida que se haya seleccionado una fila del DataGridView
            {
                string idTarjeta = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                try
                {
                    List<SqlParameter> lista = Utils.Herramientas.GenerarListaDeParametros(
                       "@cliente_id", Convert.ToInt32(this.usuario.ClienteId), "@tc_num", idTarjeta);

                    Utils.Herramientas.EjecutarStoredProcedure(nombreSP, lista);
                    Utils.Herramientas.msebox_informacion("Tarjeta (ID:" + idTarjeta + ") desasociada");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                }
            }
            else
            {
                Utils.Herramientas.msebox_informacion("Por favor, selecciona una fila de tarjeta");
            }
        }

        //  Buscar Cliente (solo para administrador)
        private void btnBuscarClie_Click(object sender, EventArgs e)
        {
            ABM_Cliente.FormBuscar frmBuscarCliente = new ABM_Cliente.FormBuscar(this, usuario,
                                                "BuscarCliente", "ABM_Tarjeta.FormBuscar");
            frmBuscarCliente.Show();
            this.Hide();
        }

        //  Limpiar filtros de busqueda (OK)
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            lblEstadoBusqueda.Text = "";    //  Indica estado de la busqueda

            //  Si es administrador tambien limpia el campo de cliente
            if (btnBuscarClie.Enabled == true) 
            {
                txtCliente.Text = "";
                this.clienteId = "0";            
            }

            txtNumero.Text = "";
            cbxEmisor.Text = "";

            btnDesasociar.Enabled = false;
            btnModificar.Enabled = false;

            //  Limpia las fechas de los filtros
            chkFechaEmision.Checked = false;
            dtpFechaEmisionDesde.Enabled = false;
            dtpFechaEmisionHasta.Enabled = false;

            chkFechaVencimiento.Checked = false;
            dtpFechaVencimientoDesde.Enabled = false;
            dtpFechaVencimientoHasta.Enabled = false;


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

            Tarjeta tarjeta = new Tarjeta();
            
            tarjeta.ClienteId = dataGridView1.SelectedCells[0].Value.ToString();//Cliente ID
            tarjeta.Nombre = dataGridView1.SelectedCells[1].Value.ToString();//NOmbre
            tarjeta.Apellido = dataGridView1.SelectedCells[2].Value.ToString();//Apellido

            tarjeta.Numero = dataGridView1.SelectedCells[3].Value.ToString();
            tarjeta.FechaEmision = dataGridView1.SelectedCells[4].Value.ToString();
            tarjeta.FechaVencimiento = dataGridView1.SelectedCells[5].Value.ToString();
            tarjeta.CodigoSeguridad = dataGridView1.SelectedCells[6].Value.ToString();
            tarjeta.Emisor = dataGridView1.SelectedCells[7].Value.ToString();
           // txtCliente.Text = dataGridView1.SelectedRows[0].Index.ToString();

            //  Con la tarjeta seleccionada
            //  Abrir un formulario con los datos de la tarjeta
            FormModificar formModificar = new FormModificar(this, tarjeta);
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
            FormAsociar formAsociar = new FormAsociar(this,txtCliente.Text,this.usuario);
            formAsociar.Show();
            this.Hide();
        }

        //  Aceptar:
        //  Se usa para buscar tarjetas desde otros formularios
        private void btnAceptar_Click(object sender, EventArgs e)
        {

        }


        private void chkFechaEmision_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFechaEmision.Checked == true)
            {
                dtpFechaEmisionDesde.Enabled = true;
                dtpFechaEmisionHasta.Enabled = true;

            }
            else 
            {
                dtpFechaEmisionDesde.Enabled = false;
                dtpFechaEmisionHasta.Enabled = false;
            }
        }

        private void chkFechaVencimiento_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFechaVencimiento.Checked == true)
            {
                dtpFechaVencimientoDesde.Enabled = true;
                dtpFechaVencimientoHasta.Enabled = true;

            }
            else
            {
                dtpFechaVencimientoDesde.Enabled = false;
                dtpFechaVencimientoHasta.Enabled = false;
            }
        }

        //private void cbxEmisor_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string key = ((KeyValuePair<string, string>)cbxEmisor.SelectedItem).Key;
        //    string value = ((KeyValuePair<string, string>)cbxEmisor.SelectedItem).Value;

        //    this.Text = "Key: " + key + ", Value: " + value;
        //}

        public void setClienteEncontrado(string clienteId, string nombre, string apellido) 
        {
            this.txtCliente.Text = apellido +", "+ nombre + " (" + clienteId + ")";
            this.clienteId = clienteId;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
