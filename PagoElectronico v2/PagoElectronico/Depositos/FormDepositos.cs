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
            PagoElectronico.MenuPrincipal frmMenu = new PagoElectronico.MenuPrincipal();
            this.Hide();
            frmMenu.Show();
        }
    }
}
