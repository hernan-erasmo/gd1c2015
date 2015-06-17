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

            q += "SELECT "
                + "[Tc_Cliente_Id] 'Cliente Id',"
                + "[Cliente_Nombre] 'Nombre',"
                + "[Cliente_Apellido] 'Apellido',"
                + "[Tc_Num_Tarjeta] 'Numero',"
                + "[Tc_Fecha_Emision] 'Fecha Emision',"
                + "[Tc_Fecha_Vencimiento] 'Fecha Vencimiento',"
                + "[Tc_Codigo_Seg] 'Codigo',"
                + "[Tc_Emisor_Desc] 'Emisor' ";
//                + "[Tc_Ultimos_Cuatro] 'Ultimos Cuatro'";
            q += "FROM [SARASA].[Tc] LEFT JOIN [SARASA].[Cliente] ON ([Tc_Cliente_Id] = [Cliente_Id]) ";
            q += "WHERE [SARASA].[Tc].[Tc_Asociada]='1'";

            if (cliente != "0")
            {
                q += " and ";
                q += "[Tc_Cliente_Id] = " + cliente;
            }

            if (numero != "")
            {
                    q += " and ";

                q += "[Tc_Num_Tarjeta] = '" + numero + "'";
            }

            if (emisor != "<Ninguno>")
            {
                    q += " and ";

                q += "[Tc_Emisor_Desc] = '" + emisor + "'";
            }

            if (fechaEmisionDesde != "" && fechaEmisionHasta != "")
            {
                    q += " and ";

                q += "([Tc_Fecha_Emision] between '" + fechaEmisionDesde + "' and '" + fechaEmisionHasta + "')";
            }

            if (fechaVencimientoDesde != "" && fechaVencimientoHasta != "")
            {
                    q += " and ";

                q += "([Tc_Fecha_Vencimiento] between '" + fechaVencimientoDesde + "' and '" + fechaVencimientoHasta + "')";
            }
            return q;

        }

        //  Genera la query de consulta del formulario buscar tarjeta
        public static string filtroBuscarCuenta(string clienteId, string numero, string tipoCuentaId,
            string monedaId, string paisId, string fechaAperturaDesde, string fechaAperturaHasta)
        {
            string q = "SELECT Cuenta_Cliente_Id 'Cliente Id',Cuenta_Numero 'Numero Cuenta',"
            + "Cuenta_Tipocta_Id 'TipoCta Id',Tipocta_Descripcion 'Tipo Cta',Cuenta_Estado_Id 'Estado Id',Estado_Descripcion 'Estado',"
            + "Cuenta_Pais_Id 'Pais Id',Pais_Nombre 'Pais',"
            + "Cuenta_Saldo 'Saldo',Cuenta_Moneda_Id  'Moneda Id',Moneda_Descripcion 'Moneda',"
            + "Cuenta_Dias_De_Suscripcion 'Dias de suscripcion',Cuenta_Fecha_Creacion 'Fecha Creacion',Cuenta_Fecha_Cierre 'Fecha Cierre',"
            + "Cuenta_Ultima_Modificacion_Tipo 'Fecha Mod Tipo',Cuenta_Items_No_Facturados 'Items no facturados',"
            + "Cliente_Nombre 'Nombre',Cliente_Apellido 'Apellido'"
            + " FROM SARASA.Cuenta, SARASA.Estado, SARASA.Pais, SARASA.Moneda, SARASA.Tipocta, SARASA.Cliente"
            + " WHERE Cuenta_Estado_Id = Estado_Id AND Cuenta_Pais_Id = Pais_Id AND Cuenta_Tipocta_Id = Tipocta_Id AND Cuenta_Cliente_Id = Cliente_Id";

            if (clienteId != "0")
                q += " AND Cuenta_Cliente_Id = " + clienteId;

            if (numero != "")
                q += " AND Cuenta_Numero=" + numero;

            if (tipoCuentaId != "0")
                q += " AND Cuenta_Tipocta_Id = " + tipoCuentaId;

            if (monedaId != "0")
                q += " AND Cuenta_Moneda_Id = " + monedaId;

            if (paisId != "0")
                q += " AND Cuenta_Pais_Id = " + paisId;

            if (fechaAperturaDesde != "" && fechaAperturaHasta != "")
                q += " AND (Cuenta_Fecha_Creacion between '" + fechaAperturaDesde + "' AND '" + fechaAperturaHasta + "')";

            
            return q;
        }

        //  Genera la query de consulta del formulario buscar cliente (y usuario)
        public static string filtroBuscarCliente(string username, string nombre, string apellido,
            string mail, string docNumero, string tipodocId)
        {
            bool primero = true;
            string q = "SELECT "
                    + "Cliente_Id 'Cliente ID',"
                    + "Usuario_Id 'User ID',"
                    + "Usuario_Username 'User',"
                    + "Cliente_Nombre 'Nombre',"
                    + "Cliente_Apellido 'Apellido',"
                    + "Cliente_Mail 'Mail',"
                    + "Tipodoc_Descripcion 'Tipo Doc',"
                    + "Cliente_Doc_Nro 'Numero Doc',"
                    + "Cliente_Fecha_Nacimiento 'Fecha Nacimiento',"
                    + "Pais_Nombre 'Pais',"
                    + "Cliente_Dom_Calle + ' ' + CONVERT(nvarchar(20),Cliente_Dom_Numero) + ', Piso '+ CONVERT(nvarchar(20),Cliente_Dom_Piso) + ', Dpto ' + Cliente_Dom_Depto 'Direccion',"
                    + "Cliente_Habilitado 'Cliente Habilitado',"
                    + "Usuario_Habilitado 'User Habilitado',"
                    + "Usuario_Fecha_Creacion 'User F.Creacion',"
                    + "Usuario_Fecha_Modificacion 'User F.Modificacion',"
                    + "Usuario_Pregunta_Sec 'Pregunta Sec',"
                    + "Cliente_Pais_Id 'Pais ID',"
                    + "Cliente_Tipodoc_Id 'Tipo Doc ID',"
                    + "Cliente_Dom_Calle 'Calle',"
                    + "Cliente_Dom_Numero 'Numero',"
                    + "Cliente_Dom_Piso 'Piso',"
                    + "Cliente_Dom_Depto 'Dpto'"
                    + " FROM SARASA.Usuario FULL OUTER JOIN SARASA.Cliente ON (Usuario_Cliente_Id = Cliente_Id)"
                    + "	LEFT JOIN SARASA.Pais ON (Cliente_Pais_Id = Pais_Id)"
                    + "	LEFT JOIN SARASA.Tipodoc ON (Cliente_Tipodoc_Id = Tipodoc_Id) ";



            if (username != "")
            {
                if (primero)
                    q += "WHERE ";

                q += "Usuario_Username = '" + username + "'";
                primero = false;
            }

            if (nombre != "")
            {
                if (primero)
                    q += "WHERE ";
                else
                    q += " and ";

                q += "Cliente_Nombre = '" + nombre + "'";
                primero = false;
            }

            if (apellido != "")
            {
                if (primero)
                    q += "WHERE ";
                else
                    q += " and ";

                q += "Cliente_Apellido = '" + apellido + "'";
                primero = false;
            }

            if (mail != "")
            {
                if (primero)
                    q += "WHERE ";
                else
                    q += " and ";

                q += "Cliente_Mail = '" + mail + "'";
                primero = false;
            }

            if (docNumero != "")
            {
                if (primero)
                    q += "WHERE ";
                else
                    q += " and ";


                q += "Cliente_Doc_Nro = " + docNumero;
                primero = false;
            }

            if (tipodocId != "0")
            {
                if (primero)
                    q += "WHERE ";
                else
                    q += " and ";


                q += "Tipodoc_Id = " + tipodocId;
                primero = false;
            }

            return q;
        }
 
        //  Genera la query de consulta del formulario buscar cliente (y usuario)
        public static string filtroBuscarUsuario(string username)
        {
            string q = "SELECT "
                    + "Usuario_Cliente_Id 'Cliente ID',"
                    + "Usuario_Id 'User ID',"
                    + "Usuario_Username 'User',"
                    + "Usuario_Habilitado 'User Habilitado',"
                    + "Usuario_Fecha_Creacion 'User F.Creacion',"
                    + "Usuario_Fecha_Modificacion 'User F.Modificacion'"
                    + " FROM SARASA.Usuario"
                    + " WHERE Usuario_Cliente_Id is null ";

            if (username != "")
                q += "AND Usuario_Username = '" + username + "'";

            return q;
        }


        public static string filtroBuscarUsuario(string username, string rolId) 
        {
            string q = "SELECT u.Usuario_Id 'Usuario ID',"
                + " u.Usuario_Username 'Usuario', u.Usuario_Cliente_Id 'Cliente ID',"
                + " u.Usuario_Fecha_Creacion 'Fecha Creación', u.Usuario_Fecha_Modificacion 'Fecha Modificación',"
                + " u.Usuario_Pregunta_Sec 'Pregunta Secreta', u.Usuario_Habilitado 'Habilitado',"
                + " u.Usuario_IntentosFallidos 'Intentos Login',u.Usuario_PrimerUso 'Primer Uso'";

            if(username == "" && rolId == "0")
            {
                q += " FROM SARASA.Usuario u";
            }
            else if(username != "" &&  rolId == "0")
            {
                q += " FROM SARASA.Usuario u";
                q += " WHERE Usuario_Username LIKE '%" + username + "%'";

            }
            else if(username == "" && rolId != "0")
            {
                q += " FROM SARASA.Usuario u, SARASA.Rol_x_Usuario ru";
                q += " WHERE u.Usuario_Id = ru.Usuario_Id AND ru.Rol_Id = " + rolId;

            }
            else
            {
                q += " FROM SARASA.Usuario u, SARASA.Rol_x_Usuario ru";
                q += " WHERE u.Usuario_Id = ru.Usuario_Id";
                q += " AND ru.Rol_Id = " + rolId;
                q += " AND Usuario_Username LIKE '%" + username + "%'";            
            }



            return q;
        }
    }
}
