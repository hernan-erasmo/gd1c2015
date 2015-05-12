using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using PagoElectronico.Utilidades;

namespace PagoElectronico.ABM_Tarjeta
{
    public partial class FormBuscar : Form
    {
        public FormBuscar()
        {
            InitializeComponent();
        }

        //  BUSCAR
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                List<SqlParameter> lista = Herramientas.GenerarListaDeParametros(
                    "@cliente", txtCliente.Text,
                    "@emisorTarjeta", cbxEmisor.Text,
                    "@numeroTarjeta", txtNumero.Text);

                string nombreSP = "Test.Buscar_Tarjeta";    //  Nombre del StoreProcedure
                DataTable resultados = new DataTable();

                resultados = Herramientas.EjecutarStoredProcedure(nombreSP, lista);
                dataGridView1.DataSource = resultados;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
/*
                Create procedure Test.Buscar_Tarjeta (
                    @cliente nvarchar(255),
                    @emisor nvarchar(255),
                    @numero numeric(18,0)) As
                    Begin 
                        Select * from dbo.Hotel
                        where Hotel_Nombre like @nombre Or
                        Hotel_Estrellas = @cant_estrellas Or
                        Hotel_Ciudad like @ciudad Or
                        Hotel_Pais like @pais
                    End
                Go
*/
            }
        }
    }
}
