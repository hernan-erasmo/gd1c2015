using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Utils;

namespace PagoElectronico.Transferencias
{
    public partial class FormTransferencias : Form
    {
        Form formPadre;
        Usuario usuario;

        public FormTransferencias(Form f, Utils.Usuario user)
        {
            InitializeComponent();
            formPadre = f;
            usuario = user;
        }
        public FormTransferencias()
        {
            InitializeComponent();
        }

        private void FormTransferencias_Load(object sender, EventArgs e)
        {
            txtCliente.Text = usuario.Apellido + ", " + usuario.Nombre + " (" + usuario.ClienteId + ")";
            string cbxCuentaQuery = "SELECT Cuenta_Numero 'Valor', CAST(Cuenta_Numero AS VARCHAR(18)) + ' (' + Tipocta_Descripcion + ')' 'Etiqueta'"
                                    + "FROM test.Cuenta , test.Tipocta "
                                    + "WHERE Tipocta_Id = Cuenta_Tipocta_Id AND Cuenta_Cliente_Id = " + usuario.ClienteId;

            Herramientas.llenarComboBox(cbxCuenta, cbxCuentaQuery, true);
            Herramientas.llenarComboBox(cbxCuentaDestino, cbxCuentaQuery, true);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            formPadre.Show();
        }


        //  Abre el buscador, para seleccionar una cuenta de otro cliente
        private void lklCuentaDestino_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Herramientas.msebox_informacion("Abre el buscador para elegir la cuenta de otro cliente");
            cbxCuentaDestino.Text = "444550000000032105 (Oro)";
        }
    }
}
