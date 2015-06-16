using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Utils;
using System.Data.SqlClient;

namespace PagoElectronico.ABM_Cuenta
{
    public partial class FormBuscar : Form
    {
        Form formPadre;
        Usuario usuario;
        Cuenta cuenta = new Cuenta();
        string clienteId;
        string tipoFormBusqueda;
        string tipoFormPadre;

        public FormBuscar(Form f, Utils.Usuario user, string tipoBusqueda, string tipoPadre)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
            tipoFormPadre = tipoPadre;
            tipoFormBusqueda = tipoBusqueda;

//            ABM_Cuenta.FormBuscar frmBuscarCuenta = new ABM_Cuenta.FormBuscar(this, usuario,
//                                    "BuscarCuenta", "Consulta_Saldos.FormConsulta");

        }


        public FormBuscar(Form f, Utils.Usuario user)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
        }

        private void FormBuscar_Load(object sender, EventArgs e)
        {
            dtpFechaAperturaDesde.Enabled = false;
            dtpFechaAperturaHasta.Enabled = false;

            txtCliente.Text = "";
            clienteId = "0";

            if (tipoFormBusqueda.Equals("BuscarCuenta"))
            {
                flowLayoutPanel1.Visible = false;
                btnAceptar.Visible = true;

                if(tipoFormPadre.Equals("Consulta_Saldos.FormConsulta"))
                {
                    if (usuario.RolId == "1")
                    {
                        btnBuscarClie.Visible = true;
                    }
                    else
                    {
                        btnBuscarClie.Visible = false;
                        txtCliente.Text = usuario.Apellido + ", " + usuario.Nombre + " (" + usuario.ClienteId + ")";
                        clienteId = "" + usuario.ClienteId;
                    }                
                }
                else if (tipoFormPadre.Equals("Transferencias.FormTransferencias"))
                {
                    btnBuscarClie.Visible = true;
                }

            }
            else if (tipoFormBusqueda.Equals("ABMCuenta"))
            {
                flowLayoutPanel1.Visible = true;
                btnAceptar.Visible = false;

                btnBaja.Enabled = false;
                btnModificar.Enabled = false;
                btnRenovar.Enabled = false;

                if (usuario.RolId == "1")
                {
                    btnBuscarClie.Visible = true;
                    btnCrear.Enabled = false;
                }
                else
                {
                    btnBuscarClie.Visible = false;
                    btnCrear.Enabled = true;
                    txtCliente.Text = usuario.Apellido + ", " + usuario.Nombre + " (" + usuario.ClienteId + ")";
                    clienteId = "" + usuario.ClienteId;
                }

            }

            Herramientas.llenarComboBoxSP(cbxPais, "SARASA.cbx_pais", null, false);
            Herramientas.llenarComboBoxSP(cbxMoneda, "SARASA.cbx_moneda", null, false);
            Herramientas.llenarComboBoxSP(cbxTipoCta, "SARASA.cbx_tipocta",null,false);

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            formPadre.Show();   //  Muestra el formulario padre
            this.Close();       //  Cierra el formulario
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            FormCrear formCrear = new FormCrear(this, txtCliente.Text, clienteId);
            formCrear.Show();
            this.Hide();
        }

        private void chkFechaApertura_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFechaApertura.Checked == true)
            {
                dtpFechaAperturaDesde.Enabled = true;
                dtpFechaAperturaHasta.Enabled = true;
            }
            else
            {
                dtpFechaAperturaDesde.Enabled = false;
                dtpFechaAperturaHasta.Enabled = false;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            lblEstadoBusqueda.Text = "Ejecutando busqueda...";

            string fechaAperturaDesde = "", fechaAperturaHasta = "";
            string cliente = clienteId;//txtCliente.Text;
            string numero = txtNumero.Text;
            string tipoCuentaId = ((KeyValuePair<string, string>)cbxTipoCta.SelectedItem).Key;
            string monedaId = ((KeyValuePair<string, string>)cbxMoneda.SelectedItem).Key;
            string paisId = ((KeyValuePair<string, string>)cbxPais.SelectedItem).Key;


            if (chkFechaApertura.Checked)
            {
                fechaAperturaDesde = dtpFechaAperturaDesde.Value.ToShortDateString();
                fechaAperturaHasta = dtpFechaAperturaHasta.Value.ToShortDateString();
            }


            //  ARMA LA QUERY A EJECUTAR BASADO EN LOS FILTROS
            string queryConsulta = Utils.Filtros.filtroBuscarCuenta(
                cliente, numero, tipoCuentaId, monedaId, paisId,
                fechaAperturaDesde, fechaAperturaHasta);

            Herramientas.msebox_informacion(queryConsulta);



            DataTable resultados;
            try
            {
                resultados = Herramientas.ejecutarConsultaTabla(queryConsulta);
                dataGridView1.DataSource = resultados;

                dataGridView1.Columns["Cliente Id"].Visible = false;
                dataGridView1.Columns["TipoCta Id"].Visible = false;
                dataGridView1.Columns["Estado Id"].Visible = false;
                dataGridView1.Columns["Pais Id"].Visible = false;
                dataGridView1.Columns["Moneda Id"].Visible = false;


                lblEstadoBusqueda.Text = "Se encontraron " + dataGridView1.RowCount + " filas";

                if (dataGridView1.RowCount > 0)
                { // Hay resultados habilita los botones para dar de Baja y Modificar
                    btnBaja.Enabled = true;
                    btnModificar.Enabled = true;
                    btnRenovar.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
                lblEstadoBusqueda.Text = "Error al realizar la busqueda";
            }
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {

            string msj = "Seguro que quiere CERRAR la CUENTA \"" + dataGridView1.SelectedCells[1].Value.ToString() + "\"?\n";
            msj += "UNA CUENTA CERRADA NO PUEDE VOLVER A ACTIVARSE";

            var result = MessageBox.Show(msj, "Cerrar cuenta",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);//, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.OK) 
            {
                List<SqlParameter> lista = Utils.Herramientas.GenerarListaDeParametros(
                    "@cliente_id", dataGridView1.SelectedCells[0].Value.ToString(),
                    "@cuenta_numero", dataGridView1.SelectedCells[1].Value.ToString(),
                    "@tipo_cuenta_deseado", dataGridView1.SelectedCells[2].Value.ToString(),
                    "@estado_deseado", 2);  //  El estado con ID 2 es "Cerrada"

                Herramientas.EjecutarStoredProcedure("SARASA.modificar_cuenta", lista);
            }
            
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            lblEstadoBusqueda.Text = "";    //  Indica estado de la busqueda


            //  Si es administrador tambien limpia el campo de cliente
//            if (btnBuscarClie.Enabled == true) 
            if (btnBuscarClie.Visible == true) 
            {
                txtCliente.Text = "";
                //  Solo se blanquea si el boton de buscar cliente esta habilitado
                clienteId = "0";
            }
            

            txtNumero.Text = "";

            cbxMoneda.SelectedIndex = 0;
            cbxTipoCta.SelectedIndex = 0;
            cbxPais.SelectedIndex = 0;

            btnBaja.Enabled = false;
            btnModificar.Enabled = false;
            btnRenovar.Enabled = false;

            if (clienteId == "0") 
                btnCrear.Enabled = false;

            //  Limpia las fechas de los filtros
            chkFechaApertura.Checked = false;
            dtpFechaAperturaDesde.Enabled = false;
            dtpFechaAperturaHasta.Enabled = false;

            //  Limpiar la tabla de resultados
            dataGridView1.DataSource = null;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            cuenta = new Cuenta();
/*
            cuenta.DesCliente = txtCliente.Text;
            cuenta.Numero = dataGridView1.SelectedCells[0].Value.ToString();
            cuenta.IdCliente = int.Parse(dataGridView1.SelectedCells[1].Value.ToString());
            cuenta.IdEstado = int.Parse(dataGridView1.SelectedCells[2].Value.ToString());
            cuenta.IdPais = int.Parse(dataGridView1.SelectedCells[3].Value.ToString());
            cuenta.IdMoneda = int.Parse(dataGridView1.SelectedCells[4].Value.ToString());
            cuenta.IdTipo = int.Parse(dataGridView1.SelectedCells[5].Value.ToString());
            cuenta.Deudora = Boolean.Parse(dataGridView1.SelectedCells[11].Value.ToString());
            cuenta.FechaApertura = dataGridView1.SelectedCells[12].Value.ToString();
            cuenta.FechaCierre = dataGridView1.SelectedCells[13].Value.ToString();
*/
            cuenta.IdCliente = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            cuenta.Numero = dataGridView1.SelectedCells[1].Value.ToString();
            cuenta.IdTipo = int.Parse(dataGridView1.SelectedCells[2].Value.ToString());
            cuenta.IdEstado = int.Parse(dataGridView1.SelectedCells[4].Value.ToString());
            cuenta.DesCliente = dataGridView1.SelectedCells[17].Value.ToString() + ","
                + dataGridView1.SelectedCells[16].Value.ToString();


            ABM_Cuenta.FormModificar frmModificar = new ABM_Cuenta.FormModificar(this,cuenta);
            frmModificar.Show();
            this.Hide();
        }

        private void btnRenovar_Click(object sender, EventArgs e)
        {

            try
            {

                List<SqlParameter> lista = Herramientas.GenerarListaDeParametros(
                                 "@cuenta_numero", dataGridView1.SelectedCells[1].Value.ToString());

                Herramientas.EjecutarStoredProcedure("SARASA.renovar_suscripcion", lista);
                Herramientas.msebox_informacion("EXEC SARASA.renovar_suscripcion @cuenta_numero=" + dataGridView1.SelectedCells[0].Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }

        }

        private void btnBuscarClie_Click(object sender, EventArgs e)
        {
            ABM_Cliente.FormBuscar frmBuscarCliente = new ABM_Cliente.FormBuscar(this, usuario,
                                    "BuscarCliente", "ABM_Cuenta.FormBuscar");
            frmBuscarCliente.Show();
            this.Hide();

        }

        public void setClienteEncontrado(string clienteId, string nombre, string apellido)
        {
            this.txtCliente.Text = apellido + ", " + nombre + " (" + clienteId + ")";
            this.clienteId = clienteId;
            this.btnCrear.Enabled = true;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string num, tipo;
            if (tipoFormPadre.Equals("Consulta_Saldos.FormConsulta"))
            {
                num = dataGridView1.SelectedCells[1].Value.ToString();
                tipo = dataGridView1.SelectedCells[3].Value.ToString();
                ((Consulta_Saldos.FormConsulta)formPadre).setCuentaEncontrada(num, tipo);
            }
            else if (tipoFormPadre.Equals("Transferencias.FormTransferencias"))
            {
                num = dataGridView1.SelectedCells[1].Value.ToString();
                tipo = dataGridView1.SelectedCells[3].Value.ToString();
                ((Transferencias.FormTransferencias)formPadre).setCuentaEncontrada(num, tipo);

            
            }
            formPadre.Show();
            this.Dispose();
        }
    }
}
