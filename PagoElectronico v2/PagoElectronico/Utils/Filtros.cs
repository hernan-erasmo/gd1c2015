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

        //  Genera la query de consulta del formulario buscar tarjeta
        public static string filtroBuscarCuenta(string clienteId, string numero, string tipoCuentaId,
            string monedaId, string paisId, string fechaAperturaDesde, string fechaAperturaHasta)
        {
            string q = "SELECT [Cuenta_Numero] 'Cuenta', [Cuenta_Cliente_Id] 'idCliente', [Cuenta_Estado_Id] 'idEstado', [Cuenta_Pais_Id] 'idPais', [Cuenta_Moneda_Id] 'idMoneda', [Cuenta_Tipocta_Id] 'idTipo'"
            + ",[Estado_Descripcion] 'Estado',[Tipocta_Descripcion] 'Tipo',[Pais_Nombre] 'Pais',[Moneda_Descripcion] 'Moneda'"
	        + ",[Cuenta_Saldo] 'Saldo',[Cuenta_Deudora] 'Deudora',[Cuenta_Fecha_Creacion] 'F.Creacion',[Cuenta_Fecha_Cierre] 'F.Cierre' "
            + "FROM [test].[Cuenta], [test].[Estado], [test].[Pais], [test].[Moneda], [test].[Tipocta] "
            + "WHERE [Cuenta_Estado_Id] = [Estado_Id] AND [Cuenta_Pais_Id] = [Pais_Id] AND [Cuenta_Moneda_Id] = [Moneda_Id] AND [Cuenta_Tipocta_Id] = [Tipocta_Id]";

            if (clienteId != "")
                q += " AND [Cuenta_Cliente_Id] = " + clienteId;

            if (numero != "")
                q += " AND [Cuenta_Numero]=" + numero;

            if (tipoCuentaId != "0")
                q += " AND [Cuenta_Tipocta_Id] = " + tipoCuentaId;

            if (monedaId != "0")
                q += " AND [Cuenta_Moneda_Id] = " + monedaId;

            if (paisId != "0")
                q += " AND [Pais_Id] = " + paisId;

            if (fechaAperturaDesde != "" && fechaAperturaHasta != "")
                q += " AND ([Cuenta_Fecha_Creacion] between '" + fechaAperturaDesde + "' AND '" + fechaAperturaHasta + "')";

            
            return q;
        }
   
    
    }
}
