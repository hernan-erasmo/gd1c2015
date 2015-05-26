using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagoElectronico.Utils
{
    class Filtros
    {
        //  Genera la query de consulta del formulario buscar tarjeta
        public static string filtroBuscarTarjeta(string cliente, string numero, string emisor,
            string fechaEmisionDesde, string fechaEmisionHasta, 
            string fechaVencimientoDesde, string fechaVencimientoHasta)
        {

            string q = "";
            bool primero = true;

            q += "SELECT [Tc_Num_Tarjeta] 'Numero',"
                + "[Tc_Fecha_Emision] 'Fecha Emision',"
                + "[Tc_Fecha_Vencimiento] 'Fecha Vencimiento',"
                + "[Tc_Codigo_Seg] 'Codigo',"
                + "[Tc_Emisor_Desc] 'Emisor' ";
//                + "[Tc_Ultimos_Cuatro] 'Ultimos Cuatro'";
            q += "FROM [test].[Tc] ";

            if (cliente != "")
            {
                if (primero)
                    q += "WHERE ";

                q += "[Tc_Cliente_Id] = " + cliente;
                primero = false;
            }

            if (numero != "")
            {
                if (primero)
                    q += "WHERE ";
                else
                    q += " and ";

                q += "[Tc_Num_Tarjeta] = '" + numero + "'";
                primero = false;
            }

            if (emisor != "")
            {
                if (primero)
                    q += "WHERE ";
                else
                    q += " and ";

                q += "[Tc_Emisor_Desc] = '" + emisor + "'";
                primero = false;
            }

            if (fechaEmisionDesde != "" && fechaEmisionHasta != "")
            {
                if (primero)
                    q += "WHERE ";
                else
                    q += " and ";

                q += "([Tc_Fecha_Emision] between '" + fechaEmisionDesde + "' and '" + fechaEmisionHasta + "')";
                primero = false;
            }

            if (fechaVencimientoDesde != "" && fechaVencimientoHasta != "")
            {
                if (primero)
                    q += "WHERE ";
                else
                    q += " and ";

                q += "([Tc_Fecha_Vencimiento] between '" + fechaVencimientoDesde + "' and '" + fechaVencimientoHasta + "')";
                primero = false;
            }

            return q;

        }
    }
}
