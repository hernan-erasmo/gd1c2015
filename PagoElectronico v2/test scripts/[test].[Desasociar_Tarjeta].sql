USE [GD1C2015]
GO
/****** Object:  StoredProcedure [test].[Desasociar_Tarjeta]    Script Date: 05/26/2015 19:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [test].[Desasociar_Tarjeta] (
	@tarjetaId varchar(64)) As
	BEGIN

	DELETE FROM [test].[Tc] WHERE Tc_Num_Tarjeta = @tarjetaId;

END
