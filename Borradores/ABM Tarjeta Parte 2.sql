-- Store Procedure...
-- Se actualiza el Store Procedure de la Parte 1, ahora hace una consulta a la base maestra
-- para cargar la lista de tarjetas. (Los filtros todavia no funcionan)

ALTER PROCEDURE [test].[Buscar_Tarjetas] (
	@emisorTarjeta nvarchar(255)) As
	BEGIN
	--	SELECT * FROM test.EmisorTC
	SELECT TOP 5 
--		[Cli_Pais_Codigo],[Cli_Pais_Desc],[Cli_Nombre],[Cli_Apellido],[Cli_Tipo_Doc_Cod],[Cli_Nro_Doc]
--      ,[Cli_Tipo_Doc_Desc],[Cli_Dom_Calle],[Cli_Dom_Nro],[Cli_Dom_Piso],[Cli_Dom_Depto],[Cli_Fecha_Nac],[Cli_Mail]
--      ,[Cuenta_Numero],[Cuenta_Fecha_Creacion],[Cuenta_Estado],[Cuenta_Pais_Codigo],[Cuenta_Pais_Desc],[Cuenta_Fecha_Cierre]
--      ,[Deposito_Codigo],[Deposito_Fecha],[Deposito_Importe]
      [Tarjeta_Numero],[Tarjeta_Fecha_Emision],[Tarjeta_Fecha_Vencimiento],[Tarjeta_Codigo_Seg],[Tarjeta_Emisor_Descripcion]
--      ,[Cuenta_Dest_Numero],[Cuenta_Dest_Fecha_Creacion],[Cuenta_Dest_Estado],[Cuenta_Dest_Pais_Codigo],[Cuenta_Dest_Pais_Desc],[Cuenta_Dest_Fecha_Cierre]
--      ,[Transf_Fecha],[Trans_Importe],[Trans_Costo_Trans]
--      ,[Retiro_Fecha],[Retiro_Codigo],[Retiro_Importe]
--      ,[Cheque_Numero],[Cheque_Fecha],[Cheque_Importe]
--      ,[Banco_Cogido],[Banco_Nombre],[Banco_Direccion]
--      ,[Item_Factura_Descr],[Item_Factura_Importe]
--      ,[Factura_Numero],[Factura_Fecha]
  FROM [GD1C2015].[gd_esquema].[Maestra]
  WHERE [Tarjeta_Numero] IS NOT NULL 
	OR [Tarjeta_Emisor_Descripcion] IS NOT NULL

	END




--*******************************************************************************************************
-- Buscar (al lado del cliente):
-- El boton solo esta habilitado para los administradores. En este caso abre un form temporal, despues se debe
-- modificar por el formulario de busqueda de cliente.


-- Limpiar: 
-- Borra todos los valores de los filtros (falta que borre la tabla), tambien borra el cliente si el usuario
-- es administrador.


-- Buscar:
-- Ejecuta un store procedure, que busca en la tabla maestra unos registros de prueba
-- Despues solo se modifica el SP para que reciba los valores de todos los filtros y haga la busqueda
-- en la tabla migrada.


-- Modificar:
-- Toma los valores de la tarjeta seleccionada despues de hacer una busqueda y los carga en el formulario para
-- luego hacer el update. 
-- Falta agregar validacion de bloquear el boton hasta que existan datos en la tabla
-- Falta el SP que actualiza el registro de tarjeta.


-- Asociar: 
-- El boton solo se habilita si el textbox cliente no esta vacio.

