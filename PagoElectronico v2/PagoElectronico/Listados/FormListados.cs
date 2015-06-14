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

namespace PagoElectronico.Listados
{
    public partial class FormListados : Form
    {
        Utils.Usuario usuario;
        Form formPadre;

        public FormListados(Form f, Utils.Usuario user)
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

        private void FormListado_Load(object sender, EventArgs e)
        {

            Dictionary<string, string> cbsTrimestre = new Dictionary<string, string>();
            cbsTrimestre.Add("1", "1er");
            cbsTrimestre.Add("2", "2do");
            cbsTrimestre.Add("3", "3ro");
            cbsTrimestre.Add("4", "4to");
            cbxTrimestre.DataSource = new BindingSource(cbsTrimestre, null);
            cbxTrimestre.DisplayMember = "Value";
            cbxTrimestre.ValueMember = "Key";

            Dictionary<string, string> cbsConsulta = new Dictionary<string, string>();
            cbsConsulta.Add("1", "Consulta 1");
            cbsConsulta.Add("2", "Consulta 2");
            cbsConsulta.Add("3", "Consulta 3");
            cbsConsulta.Add("4", "Consulta 4");
            cbsConsulta.Add("5", "Consulta 5");
            cbxConsulta.DataSource = new BindingSource(cbsConsulta, null);
            cbxConsulta.DisplayMember = "Value";
            cbxConsulta.ValueMember = "Key";

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvListado.DataSource = null;
            if (Herramientas.IsNumeric(txtAño.Text))
            {
                lblAño.ForeColor = Color.Black;

                string idConsulta = ((KeyValuePair<string, string>)cbxConsulta.SelectedItem).Key;
                string idTrimestre = ((KeyValuePair<string, string>)cbxTrimestre.SelectedItem).Key;
                string año = txtAño.Text;
                string fechaDesde = "", fechaHasta = "";

                switch (idTrimestre)
                {
                    case "1":   // Enero, Febrero, Marzo
                        fechaDesde = "01/01/" + año;
                        fechaHasta = "31/03/" + año;
                        break;
                    case "2":   //  Abril, Mayo, Junio
                        fechaDesde = "01/04/" + año;
                        fechaHasta = "30/06/" + año;
                        break;
                    case "3":   //  Julio, Agosto, Septiembre
                        fechaDesde = "01/07/" + año;
                        fechaHasta = "30/09/" + año;
                        break;
                    case "4":   //  Octubre, Noviembre, Diciembre
                        fechaDesde = "01/10/" + año;
                        fechaHasta = "31/12/" + año;
                        break;
                }


                List<SqlParameter> parametros = Herramientas.GenerarListaDeParametros("@fecha_desde","","@fecha_hasta","");

                switch (idConsulta)
                {
                    case "1":
                        //EJECUTA EL PROCEDURE DE LA CONSULTA 1
                        dgvListado.DataSource = Herramientas.EjecutarStoredProcedure("SARASA.inhabilitaciones_por_cliente", parametros);
                        break;
                    case "2":
                        //EJECUTA EL PROCEDURE DE LA CONSULTA 2
                        dgvListado.DataSource = Herramientas.EjecutarStoredProcedure("SARASA.clientes_mas_comisiones_facturadas", parametros);
                        break;
                    case "3":
                        //EJECUTA EL PROCEDURE DE LA CONSULTA 3
                        dgvListado.DataSource = Herramientas.EjecutarStoredProcedure("SARASA.clientes_transferencias_entre_si", parametros);
                        break;
                    case "4":
                        //EJECUTA EL PROCEDURE DE LA CONSULTA 4
                        dgvListado.DataSource = Herramientas.EjecutarStoredProcedure("SARASA.movimientos_por_paises", parametros);
                        break;
                    case "5":
                        //EJECUTA EL PROCEDURE DE LA CONSULTA 5
                        dgvListado.DataSource = Herramientas.EjecutarStoredProcedure("SARASA.total_facturado_por_tipo_cuenta", parametros);
                        break;
                }

            }
            else 
            {
                lblAño.ForeColor = Color.Red;
            }

        }

        private void cbxConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {            
            string id = ((KeyValuePair<string, string>)cbxConsulta.SelectedItem).Key;
            switch(id)
            {
                case "1":
                    lblInfo.Text = "Consulta 1: Clientes que alguna de sus cuentas fueron inhabilitadas por no pagar los costos de transacción";
                    break;
                case "2":
                    lblInfo.Text = "Consulta 2: Cliente con mayor cantidad de comisiones facturadas en todas sus cuentas";
                    break;
                case "3":
                    lblInfo.Text = "Consulta 3: Clientes con mayor cantidad de transacciones realizadas entre cuentas propias";
                    break;
                case "4":
                    lblInfo.Text = "Consulta 4: Paises con mayor cantidad de movimientos tanto ingresos como egresos";
                    break;
                case "5":
                    lblInfo.Text = "Consulta 5: Total facturado para los distintos tipos de cuentas";
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }
    }
}
