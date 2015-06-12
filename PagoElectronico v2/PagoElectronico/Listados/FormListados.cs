using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string idConsulta = ((KeyValuePair<string, string>)cbxConsulta.SelectedItem).Key;
            string idTrimestre = ((KeyValuePair<string, string>)cbxTrimestre.SelectedItem).Key;
            string año = txtAño.Text;

            switch (idConsulta)
            {
                case "1":
                    //EJECUTA EL PROCEDURE DE LA CONSULTA 1
                    break;
                case "2":
                    //EJECUTA EL PROCEDURE DE LA CONSULTA 2
                    break;
                case "3":
                    //EJECUTA EL PROCEDURE DE LA CONSULTA 3
                    break;
                case "4":
                    //EJECUTA EL PROCEDURE DE LA CONSULTA 4
                    break;
                case "5":
                    //EJECUTA EL PROCEDURE DE LA CONSULTA 5
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
            Utils.Herramientas.msebox_informacion("SP_CONSULTA_" + idConsulta + "(@año="+año+", @idTrimestre="+idTrimestre+")");
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
