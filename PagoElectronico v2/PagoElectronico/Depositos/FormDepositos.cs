using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Utils;

/*


Depósitos

Funcionalidad que permite a un cliente crear depósitos de dinero en alguna de las cuentas 
que tenga registrada.
Para registrar un depósito será necesario que se determinen ciertos datos a persistir:

-   Cuenta del cliente (Seleccion por buscador)
-   Importe (mayor o igual a 1)
-   Tipo moneda(selección simple, por el momento dólares)
-   Selección de la tarjeta de crédito (Seleccion por buscador)
    de las cuales tenga asociada por medio de la  cual  quiere  realizar  el  depósito.
    Se  debe  validar  que  la  dicha  TC  no  se encuentre vencida.
-   Fecha del depósito

En esta primera versión solo está previsto que los 
ingresos se realicen por medio de TC. 
Los depósitos a realizarse solo podrán ser efectuados por el mismo titular de la TC, la cuenta 
receptora de este depósito debe existir y estar habilitada.
Estos depósitos no tienen comisionesni tampoco generar un cargo que tenga que ser 
facturado dentro del caso de uso de facturación, así mismo es necesario que se genere un 
comprobante de depósito en donde  se registrará la fecha de dicho ingreso y el 
número de operación. El número de ingreso no será consecutivo a la numeración 
utilizada para los retiros de dinero y los números de transacciones realizadas. 
A modo de simplificación, los ingresos siempre serán efectivos y no habrá rechazo 
alguno por falta de disponibles en las TC de los clientes, impactando de manera 
instantánea en la cuenta electrónica, por esta razón la fecha del depósito deberá 
ser la fecha actual del sistema, obtenida del archivo de configuración, no se permiten 
depósitos con fecha pasadas o futuras a la que se encuentra en dicho archivo


*/
namespace PagoElectronico.Depositos
{
    public partial class FormDepositos : Form
    {
        Utils.Usuario usuario;
        Form formPadre;


        public FormDepositos(Form f, Utils.Usuario user)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
        }

        //  Vuelve al menu principal
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }

        private void FormDepositos_Load(object sender, EventArgs e)
        {

            txtCliente.Text = usuario.Apellido + ", " + usuario.Nombre + " (" + usuario.ClienteId + ")";
            string cbxCuentaQuery = "SELECT Cuenta_Numero 'Valor', CAST(Cuenta_Numero AS VARCHAR(18)) + ' (' + Tipocta_Descripcion + ')' 'Etiqueta'"
                                    + "FROM test.Cuenta , test.Tipocta "
                                    + "WHERE Tipocta_Id = Cuenta_Tipocta_Id AND Cuenta_Cliente_Id = " + usuario.ClienteId;

            string cbxTarjetaQuery = "SELECT Tc_Num_Tarjeta 'Valor', 'XXXX XXXX XXXX ' + Tc_Ultimos_Cuatro + ' (' + Tc_Emisor_Desc+ ')' 'Etiqueta' "
                                    + "FROM test.Tc WHERE Tc_Cliente_Id = " + usuario.ClienteId;

            Herramientas.llenarComboBox(cbxCuenta, cbxCuentaQuery,true);
            Herramientas.llenarComboBox(cbxTarjeta, cbxTarjetaQuery,true);
            Herramientas.llenarComboBox(cbxMoneda, "SELECT Moneda_Id 'Valor', Moneda_Descripcion 'Etiqueta' FROM test.Moneda",true);

        }

        private void btnDepositar_Click(object sender, EventArgs e)
        {

            Utils.Herramientas.GenerarListaDeParametros(
                "@cliente_id",usuario.ClienteId,
                "@deposito_fecha",dtpFecha.Value.ToShortDateString(),
                "@deposito_importe",txtImporte.Text,
                "@deposito_moneda_id",((KeyValuePair<string, string>)cbxMoneda.SelectedItem).Key,
                "@deposito_tarjeta_num", ((KeyValuePair<string, string>)cbxTarjeta.SelectedItem).Key,
                "@deposito_cuenta_num", ((KeyValuePair<string, string>)cbxCuenta.SelectedItem).Key
                );

            string msj = "DEPOSITO:\n"
                    + "Id Cliente: " + usuario.ClienteId + "\n"
                    + "Cuenta: " + ((KeyValuePair<string, string>)cbxCuenta.SelectedItem).Key + "\n"
                    + "Tarjeta: " + ((KeyValuePair<string, string>)cbxTarjeta.SelectedItem).Key + "\n"
                    + "Importe: " + txtImporte.Text + "\n";
            Utils.Herramientas.msebox_informacion(msj);


        }
    }
}
