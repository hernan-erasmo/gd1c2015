USE [GD1C2015]
GO
/****** Object:  StoredProcedure [test].[Modificar_Tarjeta]    Script Date: 05/26/2015 19:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [test].[Modificar_Tarjeta] (
	@tarjetaId varchar(64),
	@tarjetaNumero varchar(64),
	@tarjetaFechaEmision datetime,
	@tarjetaFechaVencimiento datetime,
	@tarjetaCodigoSeg varchar(3),
	@tarjetaEmisorDescripcion varchar(255),
	@tarjetaUltimosCuatro nvarchar(4)) As
	BEGIN

	UPDATE [test].[Tc] SET
			Tc_Num_Tarjeta = @tarjetaNumero,					--	varchar(64),
			Tc_Fecha_Emision = @tarjetaFechaEmision,			--	datetime	NOT NULL,
			Tc_Fecha_Vencimiento = @tarjetaFechaVencimiento,	--	datetime	NOT NULL,
			Tc_Codigo_Seg = @tarjetaCodigoSeg,					--	nvarchar(64)	NOT NULL,
			Tc_Emisor_Desc = @tarjetaEmisorDescripcion,			--	nvarchar(255)	NOT NULL,
			Tc_Ultimos_Cuatro = @tarjetaUltimosCuatro			--	nvarchar(4)	NOT NULL
	WHERE Tc_Num_Tarjeta = @tarjetaId;

END
