USE [GD1C2015]
GO
/****** Object:  StoredProcedure [test].[Asociar_Tarjeta]    Script Date: 05/26/2015 19:25:37 ******/
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
	@tarjetaEmisorDescripcion varchar(255),
	@tarjetaUltimosCuatro nvarchar(4)) As
	BEGIN

	INSERT INTO [test].[Tc] (
			Tc_Num_Tarjeta,--		varchar(64),
			Tc_Cliente_Id,--		integer,
			Tc_Fecha_Emision,--		datetime	NOT NULL,
			Tc_Fecha_Vencimiento,--	datetime	NOT NULL,
			Tc_Codigo_Seg,--		nvarchar(64)	NOT NULL,
			Tc_Emisor_Desc,--		nvarchar(255)	NOT NULL,
			Tc_Ultimos_Cuatro)--	nvarchar(4)	NOT NULL
		VALUES (
			@tarjetaNumero,
			@clienteId,
			@tarjetaFechaEmision,
			@tarjetaFechaVencimiento,
			@tarjetaCodigoSeg,
			@tarjetaEmisorDescripcion,
			@tarjetaUltimosCuatro);
			
			
			
END
