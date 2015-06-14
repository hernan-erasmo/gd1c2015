using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

namespace PagoElectronico.Utils
{
    class Herramientas
    {

        public Herramientas()
        {
        }

        //  Muestra un msg box de informacion
        public static void msebox_informacion(string texto)
        {
            MessageBox.Show(texto, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //  Muentra un msg box de advertencia
        public static void msgbox_advertencia(string texto)
        {
            MessageBox.Show(texto, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        //  Recupera los ultimos caracteres de un String
        public static string stringRight(string param, int length)
        {
            int value = param.Length - length;
            string result = param.Substring(value, length);
            return result;
        }

        //  Valida si los valores son numericos
        public static bool IsNumeric(string num)
        {
            try
            {
                int x = Convert.ToInt32(num);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //  Encriptador SHA256
        public static String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (System.Security.Cryptography.SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        //  Ejecuta la auteticacion del usuario y carga la lista de funciones
        public static void ejecutarAutenticacion(Usuario user)
        {
            string nombreSP = "SARASA.Ejecutar_Autenticacion";

            List<SqlParameter> listaParametros = Herramientas.GenerarListaDeParametros(
                "@username", user.Username, "@password", user.Password);

            conexion cn = new conexion();

            SqlCommand query = new SqlCommand(nombreSP, cn.abrir_conexion());
            query.CommandType = CommandType.StoredProcedure;


            //	Agregar los parametros del tipo INPUT
            query.Parameters.AddRange(listaParametros.ToArray());

            //	Definir el parametro del tipo OUTPUT
            SqlParameter ParamCodigo = new SqlParameter("@codigo", 0);
            ParamCodigo.Direction = ParameterDirection.Output;
            query.Parameters.Add(ParamCodigo);

            SqlParameter ParamNombre = new SqlParameter("@nombre", "");
            ParamNombre.Direction = ParameterDirection.Output;
            ParamNombre.Size = 255;
            query.Parameters.Add(ParamNombre);

            SqlParameter ParamApellido = new SqlParameter("@apellido", "");
            ParamApellido.Direction = ParameterDirection.Output;
            ParamApellido.Size = 255;
            query.Parameters.Add(ParamApellido);

            SqlParameter ParamClienteId = new SqlParameter("@clienteId", 0);
            ParamClienteId.Direction = ParameterDirection.Output;
            query.Parameters.Add(ParamClienteId);

            SqlParameter ParamClienteDocumento = new SqlParameter("@clienteDocumento", 0);
            ParamClienteDocumento.Direction = ParameterDirection.Output;
            query.Parameters.Add(ParamClienteDocumento);

            SqlParameter ParamUsuarioId = new SqlParameter("@usuarioId", 0);
            ParamUsuarioId.Direction = ParameterDirection.Output;
            query.Parameters.Add(ParamUsuarioId);

            SqlParameter ParamRolNombre = new SqlParameter("@rolNombre", "");
            ParamRolNombre.Direction = ParameterDirection.Output;
            ParamRolNombre.Size = 20;
            query.Parameters.Add(ParamRolNombre);

            query.ExecuteNonQuery();

            user.CodLogin = int.Parse(query.Parameters["@codigo"].SqlValue.ToString());
            user.ClienteId = int.Parse(query.Parameters["@clienteId"].SqlValue.ToString());
            user.Nombre = query.Parameters["@nombre"].SqlValue.ToString();
            user.Apellido = query.Parameters["@apellido"].SqlValue.ToString();
            user.Documento = query.Parameters["@clienteDocumento"].SqlValue.ToString();
            user.UsuarioId = int.Parse(query.Parameters["@usuarioId"].SqlValue.ToString());
            user.Rol = query.Parameters["@rolNombre"].SqlValue.ToString();
            user.RolId = query.Parameters["@codigo"].SqlValue.ToString();

        }

        //  Ejecuta la auteticacion del usuario y carga la lista de funciones
        public static void ejecutarCrearCuenta(Cuenta cuenta)
        {
            List < SqlParameter > listaParametros = Herramientas.GenerarListaDeParametros(
            "@cliente_id", cuenta.IdCliente,
            "@fecha_apertura", cuenta.FechaApertura,
            "@tipo_cuenta_id", cuenta.IdTipo,
            "@moneda_id", cuenta.IdMoneda,
            "@pais_id", cuenta.IdPais);

            conexion cn = new conexion();

            SqlCommand query = new SqlCommand("SARASA.crear_cuenta2", cn.abrir_conexion());
            query.CommandType = CommandType.StoredProcedure;

            //	Agregar los parametros del tipo INPUT
            query.Parameters.AddRange(listaParametros.ToArray());

            //	Definir el parametro del tipo OUTPUT
            SqlParameter ParamNumeroCta = new SqlParameter("@numeroCta",SqlDbType.Decimal);
            ParamNumeroCta.SqlDbType = SqlDbType.Decimal;
            ParamNumeroCta.Direction = ParameterDirection.Output;
            query.Parameters.Add(ParamNumeroCta);

            query.ExecuteNonQuery();

            cuenta.Numero = query.Parameters["@numeroCta"].SqlValue.ToString();
        }


        public static void cargarFunciones(Usuario user)
        {
            SqlDataReader dReader;
            try
            {
                string nombreSP = "SARASA.buscar_funciones";

                MessageBox.Show("Usuario: "+user.Username+" Rol: " +user.RolId);

                List<SqlParameter> listaParametros = Herramientas.GenerarListaDeParametros("@rolId", user.RolId, "@form", "Login");

                conexion cn = new conexion();


                SqlCommand query = new SqlCommand(nombreSP, cn.abrir_conexion());
                query.CommandType = CommandType.StoredProcedure;

                //	Agregar los parametros del tipo INPUT
                query.Parameters.AddRange(listaParametros.ToArray());


                dReader = query.ExecuteReader();

                while (dReader.Read())
                {
                    user.Funciones.Add(dReader[0].ToString());
                }
                dReader.Close();

                MessageBox.Show("Funciones disponibles: " + user.Funciones.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar las funciones del rol. " + ex.ToString());

            }
        }

        //  Carga la lista de funciones para asociar a un rol nuevo
        //public static void llenarListBoxFuncionesSistema(ListBox lb, string rol, string funcion)
        public static void llenarListBoxFuncionesSistema(ListBox lb, int rolId, string formulario)
        {
            SqlDataReader dReader;
            try
            {
                List<SqlParameter> listaParametros = Herramientas.GenerarListaDeParametros("@rolId", rolId, "@form", formulario);

                conexion cn = new conexion();

                SqlCommand query = new SqlCommand("SARASA.buscar_funciones", cn.abrir_conexion());
                
                query.Parameters.AddRange(listaParametros.ToArray());
                
                query.CommandType = CommandType.StoredProcedure;
                
                dReader = query.ExecuteReader();

                lb.DisplayMember = "Descripcion";
                lb.ValueMember = "Id";

                while (dReader.Read())
                {
                    ItemFuncion item = new ItemFuncion(
                                int.Parse(dReader[0].ToString()),
                                dReader[1].ToString(),
                                int.Parse(dReader[2].ToString()));

                    lb.Items.Add(item);

                    if (formulario == "Modificar" && item.Seleccionado) 
                        lb.SelectedItems.Add(item);
                }

                dReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar el combo Box " + ex.ToString());

            }
        }

        public static void llenarListBoxRolesSistema(ListBox lb, string rolDesc, string funcionDesc)
        {
            SqlDataReader dReader;
            try
            {
                lb.DisplayMember = "Etiqueta";
                lb.ValueMember = "Id";

                conexion cn = new conexion();
                SqlCommand query = new SqlCommand("[SARASA].[buscar_rol]", cn.abrir_conexion());
                List<SqlParameter> listaParametros = Herramientas.GenerarListaDeParametros("@rol", rolDesc, "@funcion", funcionDesc);
                query.Parameters.AddRange(listaParametros.ToArray());
                query.CommandType = CommandType.StoredProcedure;
                dReader = query.ExecuteReader();


                while (dReader.Read())
                {
                    lb.Items.Add(
                        new ItemRol(
                            int.Parse(dReader[0].ToString()), 
                            bool.Parse(dReader[1].ToString()),
                            dReader[2].ToString(), 
                            dReader[3].ToString()));
                }

                dReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar el combo Box " + ex.ToString());

            }
        }

        public static void crearRol(string nombreRol, int habilitado, string listIds) 
        {
            conexion cn = new conexion();

            SqlCommand query = new SqlCommand("SARASA.Crear_Rol", cn.abrir_conexion());

            query.CommandType = CommandType.StoredProcedure;
            
            //	Agregar los parametros del tipo INPUT
            List<SqlParameter> listaParametros = Herramientas.GenerarListaDeParametros(
                            "@rol_desc", nombreRol, "@rol_estado", habilitado, "@funciones_asociadas", listIds);
            
            query.Parameters.AddRange(listaParametros.ToArray());

            query.ExecuteNonQuery();

        }

        public static void actualizarRol(int idRol, string nombreRol, int habilitado, string listIds)
        {
            List<SqlParameter> listaParametros = Herramientas.GenerarListaDeParametros(
                "@rol_id", idRol, "@rol_desc", nombreRol, "@rol_estado", habilitado, "@funciones_asociadas", listIds);

            conexion cn = new conexion();
            
            SqlCommand query = new SqlCommand("[SARASA].[modificar_rol]", cn.abrir_conexion());

            query.CommandType = CommandType.StoredProcedure;

            //	Agregar los parametros del tipo INPUT
            query.Parameters.AddRange(listaParametros.ToArray());

            query.ExecuteNonQuery();

        }

        public static void eliminarRol(int idRol)
        {
            List<SqlParameter> listaParametros = Herramientas.GenerarListaDeParametros("@rol_id", idRol);

            conexion cn = new conexion();

            SqlCommand query = new SqlCommand("[SARASA].[eliminar_rol]", cn.abrir_conexion());

            query.CommandType = CommandType.StoredProcedure;

            //	Agregar los parametros del tipo INPUT
            query.Parameters.AddRange(listaParametros.ToArray());

            query.ExecuteNonQuery();

        }

        //  Carga el combo box con el resultado de la consulta
        public static void llenarComboBox(ComboBox cb, string consulta, bool obligatorio)
        {
            SqlDataReader dReader;
            try
            {
                conexion cn = new conexion();
                //cn.abrir_conexion();

                SqlCommand query = new SqlCommand(consulta, cn.abrir_conexion());
                dReader = query.ExecuteReader();

                //	Add keys and values in a Dictionary Object
                Dictionary<string,string> comboSource = new Dictionary<string, string>();
                
                //*************************************************
                if (!obligatorio)
                    comboSource.Add("0", "<Ninguno>");

                if (dReader.HasRows) 
                {
//                    comboSource.Add("0", "<Ninguno>");
                    while (dReader.Read())
                    {
                        comboSource.Add(dReader[0].ToString(), dReader[1].ToString());
                    }

                    //	Bind the source Dictionary object to Combobox
                    cb.DataSource = new BindingSource(comboSource, null);
                    cb.DisplayMember = "Value";
                    cb.ValueMember = "Key";
                }

                dReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar el combo Box " + ex.ToString());

            }
        }

        //  Carga el combo box con el resultado de la consulta ejecutando un SP
        public static void llenarComboBoxSP(ComboBox cb, string nombreSP, List<SqlParameter> parametros,bool obligatorio)
        {
            SqlDataReader dReader;
            try
            {
                conexion cn = new conexion();
                SqlCommand query = new SqlCommand(nombreSP, cn.abrir_conexion());
                query.CommandType = CommandType.StoredProcedure;

                if(parametros != null)
                    query.Parameters.AddRange(parametros.ToArray());

                dReader = query.ExecuteReader();

                //	Add keys and values in a Dictionary Object
                Dictionary<string, string> comboSource = new Dictionary<string, string>();

                //*************************************************
                if (!obligatorio)
                    comboSource.Add("0", "<Ninguno>");

                if (dReader.HasRows)
                {
                    //                    comboSource.Add("0", "<Ninguno>");
                    while (dReader.Read())
                    {
                        comboSource.Add(dReader[0].ToString(), dReader[1].ToString());
                    }

                    //	Bind the source Dictionary object to Combobox
                    cb.DataSource = new BindingSource(comboSource, null);
                    cb.DisplayMember = "Value";
                    cb.ValueMember = "Key";
                }

                dReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar el combo Box " + ex.ToString());

            }
        }

        //  Carga el combo box con el resultado de la consulta
        public static void llenarComboBoxConSeleccion(ComboBox cb, string consulta, string valor, bool obligatorio)
        {
            SqlDataReader dReader;
            int indice =0;
            try
            {
                conexion cn = new conexion();
                //cn.abrir_conexion();

                SqlCommand query = new SqlCommand(consulta, cn.abrir_conexion());
                dReader = query.ExecuteReader();

                //	Add keys and values in a Dictionary Object
                Dictionary<string, string> comboSource = new Dictionary<string, string>();

                //*************************************************

                if(!obligatorio)
                    comboSource.Add("0", "<Ninguno>");

                if (dReader.HasRows)
                {


                    while (dReader.Read())
                    {
                        comboSource.Add(dReader[0].ToString(), dReader[1].ToString());
                        if (valor.Equals(dReader[0].ToString()))
                            indice = comboSource.Count -1;
                    }

                    //	Bind the source Dictionary object to Combobox
                    cb.DataSource = new BindingSource(comboSource, null);
                    cb.DisplayMember = "Value";
                    cb.ValueMember = "Key";
                    cb.SelectedIndex = indice;
                }

                dReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar el combo Box " + ex.ToString());
            }
        }

        //  Ejecuta un Store Procedure sin parametros, devuelve DataTable
        public static DataTable EjecutarStoredProcedureSinParametros(string nombreSP)
        {
            try
            {
                DataTable dtable = new DataTable();
                conexion cn = new conexion();
                SqlCommand query = new SqlCommand(nombreSP, cn.abrir_conexion());
                query.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(query);
                da.Fill(dtable);

                return dtable;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString(), "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        public static SqlDataReader ejecutarConsultaSimple(string consulta)
        {
            try
            {
                conexion cn = new conexion();
                SqlCommand query = new SqlCommand(consulta, cn.abrir_conexion());
                SqlDataReader dReader = query.ExecuteReader();

                return dReader;
//              return query.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
                return null;
            }
        }

        //  Ejecuta query para cargar tabla
        public static DataTable ejecutarConsultaTabla(string consulta)
        {

            try
            {
                DataTable dtable = new DataTable();
                conexion cn = new conexion();
                SqlCommand query = new SqlCommand(consulta, cn.abrir_conexion());
//                query.e
//                query.CommandType = CommandType.StoredProcedure;

                //  Se pasan los parametros como array
//                query.Parameters.AddRange(parametros.ToArray());
                SqlDataAdapter da = new SqlDataAdapter(query);
                da.Fill(dtable);

                return dtable;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString(), "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        //  Ejecuta un Store Procedure (nombreSP) con parametros (parametros)
        public static DataTable EjecutarStoredProcedure(string nombreSP, List<SqlParameter> parametros)
        {
            try
            {
                DataTable dtable = new DataTable();

                conexion cn = new conexion();
                SqlCommand query = new SqlCommand(nombreSP, cn.abrir_conexion());
                query.CommandType = CommandType.StoredProcedure;
                //  Se pasan los parametros como array
                query.Parameters.AddRange(parametros.ToArray());

                SqlDataAdapter da = new SqlDataAdapter(query);
                da.Fill(dtable);

                return dtable;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(""+ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        //  Ejecuta un Store Procedure (nombreSP) con parametros (parametros)
        public static string EjecutarStoredProcedureOutParameter(string nombreSP, List<SqlParameter> parametros)
        {
            try
            {
                DataTable dtable = new DataTable();

                conexion cn = new conexion();
                SqlCommand query = new SqlCommand(nombreSP, cn.abrir_conexion());
                query.CommandType = CommandType.StoredProcedure;
                //  Se pasan los parametros como array
                query.Parameters.AddRange(parametros.ToArray());

                //  Agrego el parametro de output
                //SqlParameter outParam = query.Parameters.Add("@habilitado", SqlDbType.VarChar);
                SqlParameter outParam = query.Parameters.Add("@factura_id", SqlDbType.Int);
                outParam.Direction = ParameterDirection.Output;

                //  Recupera el valor de salida
                return outParam.Value.ToString();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString(), "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        public static int ejec_SP_NOQUERY(string noquery, List<SqlParameter> lista)
        {

            conexion conection = new conexion();
            SqlCommand query = new SqlCommand(noquery, conection.abrir_conexion());
            query.CommandType = CommandType.StoredProcedure;
            //paso lo parametros como array
            query.Parameters.AddRange(lista.ToArray());
            try
            {
                int filas_afectadas = query.ExecuteNonQuery();
                return filas_afectadas;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

        }

        //  Genera una lista con los parametros para ejecutar un query
        public static List<SqlParameter> GenerarListaDeParametros(params object[] values)
        {
            if (values.Length % 2 != 0)
            {
                throw new ArgumentException("La cantidad de argumentos debe ser par (clave-valor)");
            }

            List<SqlParameter> paramList = new List<SqlParameter>();

            for (int i = 0; i < values.Length; i++)
            {
                if (i % 2 == 0)
                {
                    String paramName = values[i].ToString();
                    object paramValue = values[i + 1];
                    paramList.Add(new SqlParameter(paramName, paramValue));
                }
            }
            return paramList;
        }

        public static string generarFactura(Usuario user)
        {
            string nombreSP = "SARASA.facturar_por_cliente";

            List<SqlParameter> listaParametros = Herramientas.GenerarListaDeParametros(
                "@cliente_id", user.ClienteId);

            conexion cn = new conexion();

            SqlCommand query = new SqlCommand(nombreSP, cn.abrir_conexion());
            query.CommandType = CommandType.StoredProcedure;


            //	Agregar los parametros del tipo INPUT
            query.Parameters.AddRange(listaParametros.ToArray());

            //	Definir el parametro del tipo OUTPUT
            SqlParameter factura = new SqlParameter("@factura_id", 0);
            factura.Direction = ParameterDirection.Output;
            query.Parameters.Add(factura);

            query.ExecuteNonQuery();

            return (query.Parameters["@factura_id"].SqlValue.ToString());
        }

        public static string comprobarItemsImpagos(Usuario user)
        {
            string nombreSP = "SARASA.comprobar_items_impagos";

            List<SqlParameter> listaParametros = Herramientas.GenerarListaDeParametros(
                "@cliente_id", user.ClienteId);

            conexion cn = new conexion();

            SqlCommand query = new SqlCommand(nombreSP, cn.abrir_conexion());
            query.CommandType = CommandType.StoredProcedure;


            //	Agregar los parametros del tipo INPUT
            query.Parameters.AddRange(listaParametros.ToArray());

            //	Definir el parametro del tipo OUTPUT
            SqlParameter factura = new SqlParameter("@comprobante", 0);
            factura.Direction = ParameterDirection.Output;
            query.Parameters.Add(factura);

            query.ExecuteNonQuery();

            return (query.Parameters["@comprobante"].SqlValue.ToString());
            
        }

        public static void facturarItems(Usuario user, int factura_id)
        {
            string nombreSP = "SARASA.facturar_items";

            List<SqlParameter> listaParametros = Herramientas.GenerarListaDeParametros(
                "@factura_id", factura_id, "@cliente_id", user.ClienteId);

            conexion cn = new conexion();

            SqlCommand query = new SqlCommand(nombreSP, cn.abrir_conexion());
            query.CommandType = CommandType.StoredProcedure;


            //	Agregar los parametros del tipo INPUT
            query.Parameters.AddRange(listaParametros.ToArray());

            query.ExecuteNonQuery();
        }


        //Llave de la clase
    }
}
