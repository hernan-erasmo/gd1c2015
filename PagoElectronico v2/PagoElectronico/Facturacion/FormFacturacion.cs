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

namespace PagoElectronico.Facturacion
{
    public partial class FormFacturacion : Form
    {

        Form formPadre;
        Utils.Usuario usuario;
        

        public FormFacturacion(Form f, Utils.Usuario user)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
        }

        private void FormFacturacion_Load(object sender, EventArgs e)
        {

        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }
        
        private void buttonGenerar_Click(object sender, EventArgs e)
        {
            //me fijo si hay elementos por facturar
            string comp;
            comp = Utils.Herramientas.comprobarItemsImpagos(this.usuario);
            if (Int32.Parse(comp) == 1)
            {
                Decimal result = Utils.Herramientas.ejecutarPuedeFacturar(this.usuario);
                if (result == 1) //valido que las cuentas puedan pagar los items antes de facturarlos
                {
                    //genero la nueva factura
                    string factura_id;

                    factura_id = Utils.Herramientas.generarFactura(this.usuario);

                    Facturacion.FormGenerarFactura frmGenerarFactura = new Facturacion.FormGenerarFactura(this, usuario, factura_id);

                    //actualizo los item de factura

                    string nombreSP = "SARASA.facturar_items";    //  Nombre del StoreProcedure
                    try
                    {
                        List<SqlParameter> lista = Utils.Herramientas.GenerarListaDeParametros(
                            "@factura_id", Int32.Parse(factura_id),
                            "@cliente_id", this.usuario.ClienteId);

                        Utils.Herramientas.EjecutarStoredProcedure(nombreSP, lista);
                        this.Hide();
                        this.formPadre.Show();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.ToString());
                    }

                    this.Hide();
                    frmGenerarFactura.Show();
                }
                else
                {
                    Utils.Herramientas.msebox_informacion("Una de las cuentas no posee saldo suficiente para facturar los items");
                }

            }
            else
            {
                Utils.Herramientas.msebox_informacion("No hay items para facturar");
            }
        }

        private void buttonFacturasPagadas_Click(object sender, EventArgs e)
        {
            Facturacion.FormFacturasPagas frmFacturasPagas = new Facturacion.FormFacturasPagas(this, usuario);
            this.Hide();
            frmFacturasPagas.Show();
        }

    }
}
