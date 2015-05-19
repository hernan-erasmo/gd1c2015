/*
	El procedimiento agrega registros a la tabla maestra simulando el alta de tarjetas.
	Desde la funcion Asociar del ABM Tarjeta, no importa el valor del nombre del cliente
	ya que el SP hardcodea el valor 'PRUEBA' en el campo Cli_Nombre
	para facilitar su borrado.	
*/
CREATE PROCEDURE [test].[Asociar_Tarjeta] (
	@clienteId varchar(16),
	@tarjetaNumero varchar(16),
	@tarjetaFechaEmision datetime,
	@tarjetaFechaVencimiento datetime,
	@tarjetaCodigoSeg varchar(3),
	@tarjetaEmisorDescripcion varchar(255)) As
	BEGIN

		-- PARA BORRAR LOS REGISTROS QUE SE GENEREN CON ESTE PROCEDURE HAY QUE EJECUTAR:
		-- DELETE FROM [GD1C2015].[gd_esquema].[Maestra] WHERE [Cli_Nombre] = 'PRUEBA'		

		INSERT INTO [GD1C2015].[gd_esquema].[Maestra] 
			([Cli_Nombre],[Tarjeta_Numero],[Tarjeta_Fecha_Emision],[Tarjeta_Fecha_Vencimiento],
			[Tarjeta_Codigo_Seg],[Tarjeta_Emisor_Descripcion])
		VALUES('PRUEBA',@tarjetaNumero,@tarjetaFechaEmision,@tarjetaFechaVencimiento,
		@tarjetaCodigoSeg,@tarjetaEmisorDescripcion)

	END
