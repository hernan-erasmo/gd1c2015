using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PagoElectronico.Utilidades
{
    class llenarCombos: PagoElectronico.Utilidades.Conexion
    {
        public llenarCombos()
        {
            conSql.Open();
        }

        public void llenarComboRol(ComboBox combo)
        {
           
            comandoSql = new SqlCommand("SELECT rol_nombre FROM Rol", conSql);
            Reg = comandoSql.ExecuteReader();
            while (Reg.Read())
            {
                combo.Items.Add(Reg["rol_nombre"].ToString());
            }
            Reg.Close();



        }


    }
}
