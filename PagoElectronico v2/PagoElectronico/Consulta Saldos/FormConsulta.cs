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

namespace PagoElectronico.Consulta_Saldos
{
    public partial class FormConsulta : Form
    {
        Utils.Usuario usuario;
        Form formPadre;
        string numeroCuenta = "0";

        public FormConsulta(Form f, Utils.Usuario user)
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

        private void FormConsultaSaldo_Load(object sender, EventArgs e)
        {
            dgvSaldo.ColumnHeadersVisible = false;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtable = new DataTable();
                conexion cn = new conexion();
                SqlCommand query = new SqlCommand("SARASA.consultar_saldos", cn.abrir_conexion());
                query.CommandType = CommandType.StoredProcedure;

                List<SqlParameter> parametros = Herramientas.GenerarListaDeParametros("@cuenta_numero", numeroCuenta);
                query.Parameters.AddRange(parametros.ToArray());

                SqlDataReader reader = query.ExecuteReader();

                if (reader.HasRows)
                {
                    DataTable dt1 = new DataTable();
                    dt1.Load(reader);
                    dgvSaldo.DataSource = dt1;

                    DataTable dt2 = new DataTable();
                    dt2.Load(reader);
                    dgvDepositos.DataSource = dt2;


                    DataTable dt3 = new DataTable();
                    dt3.Load(reader);
                    dgvRetiros.DataSource = dt3;


                    DataTable dt4 = new DataTable();
                    dt4.Load(reader);
                    dgvTransferencias.DataSource = dt4;
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("" + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lklCuenta_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
//            Herramientas.msebox_informacion("Buscar una cuenta");
//            "Consulta_Saldos.FormConsulta","BuscarCuenta"
            ABM_Cuenta.FormBuscar frmBuscarCuenta = new ABM_Cuenta.FormBuscar(this, usuario,
                                                "BuscarCuenta", "Consulta_Saldos.FormConsulta");
            frmBuscarCuenta.Show();
            this.Hide();
        }

        public void setCuentaEncontrada(string numero, string tipo)
        {
            txtCuenta.Text = numero + " (" + tipo + ")";
            this.numeroCuenta = numero;
            dgvSaldo.DataSource = null;
            dgvDepositos.DataSource = null;
            dgvRetiros.DataSource = null;
            dgvTransferencias.DataSource = null;

        }
    }
}
