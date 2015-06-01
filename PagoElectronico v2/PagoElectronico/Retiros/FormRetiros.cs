using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Utils;

namespace PagoElectronico.Retiros
{
    public partial class FormRetiros : Form
    {
        Utils.Usuario usuario;
        Form formPadre;

        public FormRetiros(Form f, Utils.Usuario user)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
        }

        private void FormRetiros_Load(object sender, EventArgs e)
        {
            txtCliente.Text = usuario.Apellido + ", " + usuario.Nombre + " (" + usuario.ClienteId + ")";
            string cbxCuentaQuery = "SELECT Cuenta_Numero 'Valor', CAST(Cuenta_Numero AS VARCHAR(18)) + ' (' + Tipocta_Descripcion + ')' 'Etiqueta'"
                                    + "FROM test.Cuenta , test.Tipocta "
                                    + "WHERE Tipocta_Id = Cuenta_Tipocta_Id AND Cuenta_Cliente_Id = " + usuario.ClienteId;
            Herramientas.llenarComboBox(cbxCuenta, cbxCuentaQuery,true);
            Herramientas.llenarComboBox(cbxMoneda, "SELECT Moneda_Id 'Valor', Moneda_Descripcion 'Etiqueta' FROM test.Moneda",true);

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }
    }
}
