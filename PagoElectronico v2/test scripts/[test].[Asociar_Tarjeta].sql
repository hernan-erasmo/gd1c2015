USE [GD1C2015]
GO
/****** Object:  StoredProcedure [test].[Asociar_Tarjeta]    Script Date: 05/26/2015 14:17:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
