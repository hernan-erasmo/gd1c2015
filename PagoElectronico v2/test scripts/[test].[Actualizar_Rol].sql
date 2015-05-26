USE [GD1C2015]
GO
/****** Object:  StoredProcedure [test].[Actualizar_Rol]    Script Date: 05/26/2015 14:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [test].[Actualizar_Rol] (
	@rolId integer,
	@rolNombre varchar(20),
	@rolHabilitado bit,
	@listaId varchar(200)) 
AS
BEGIN

--	DECLARE @listaId varchar(200) = '1,6,20,'
--	DECLARE @rolNombre varchar(20) = 'Monitor'
--	DECLARE @rolHabilitado int = 1;

	DECLARE @funcionId varchar(3) = null;

	--	Actualiza la tabla test.Rol	
	UPDATE test.Rol 
	SET Rol_Descripcion = @rolNombre, Rol_Habilitado = @rolHabilitado
	WHERE Rol_Id = @rolId;

	--	Elimina de la tabla test.RolFuncion, todas las relaciones con el rol @rolId
	DELETE FROM test.RolFuncion WHERE RolFuncion_RolId = @rolId;

	-- Relaciona el rol (rolId) con las nuevas funciones seleccionadas (listaId)
	WHILE LEN(@listaId) > 0
	BEGIN
		IF PATINDEX('%,%',@listaId) > 0
		BEGIN
			SET @funcionId = SUBSTRING(@listaId, 0, PATINDEX('%,%',@listaId))
--			SELECT @funcionId 'Res'
			SET @listaId = SUBSTRING(@listaId, LEN(@funcionId + ',') + 1, LEN(@listaId))

			INSERT INTO test.RolFuncion (RolFuncion_RolId,RolFuncion_FuncionId)
			VALUES (@rolId,@funcionId);
		END
		ELSE
		BEGIN
			SET @funcionId = @listaId
			SET @listaId = NULL
--			SELECT @funcionId 'Ulti'
			INSERT INTO test.RolFuncion (RolFuncion_RolId,RolFuncion_FuncionId)
			VALUES (@rolId,@funcionId);
		END
	END

END
