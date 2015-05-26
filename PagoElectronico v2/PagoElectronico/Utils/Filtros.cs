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

            q += "SELECT [Tarjeta_Numero] 'Numero',[Tarjeta_Fecha_Emision] 'Fecha Emision',[Tarjeta_Fecha_Vencimiento] 'Fecha Vencimiento',[Tarjeta_Codigo_Seg] 'Codigo',[Tarjeta_Emisor_Descripcion] 'Emisor' ";
            q += "FROM [GD1C2015].[gd_esquema].[Maestra] ";

            if (cliente != "")
            {
                if (primero)
                    q += "WHERE ";

                q += "[Cli_Nro_Doc] = '" + cliente + "'";
                primero = false;
            }

            if (numero != "")
            {
                if (primero)
                    q += "WHERE ";
                else
                    q += " and ";

                q += "[Tarjeta_Numero] = '" + numero + "'";
                primero = false;
            }

            if (emisor != "")
            {
                if (primero)
                    q += "WHERE ";
                else
                    q += " and ";

                q += "[Tarjeta_Emisor_Descripcion] = '" + emisor + "'";
                primero = false;
            }

            if (fechaEmisionDesde != "" && fechaEmisionHasta != "")
            {
                if (primero)
                    q += "WHERE ";
                else
                    q += " and ";

                q += "([Tarjeta_Fecha_Emision] between '" + fechaEmisionDesde + "' and '" + fechaEmisionHasta + "')";
                primero = false;
            }

            if (fechaVencimientoDesde != "" && fechaVencimientoHasta != "")
            {
                if (primero)
                    q += "WHERE ";
                else
                    q += " and ";

                q += "([Tarjeta_Fecha_Vencimiento] between '" + fechaVencimientoDesde + "' and '" + fechaVencimientoHasta + "')";
                primero = false;
            }

            return q;

        }
    }
}
