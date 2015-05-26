USE [GD1C2015]
GO
/****** Object:  StoredProcedure [test].[Crear_Rol]    Script Date: 05/26/2015 14:20:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [test].[Crear_Rol] (
	@rolNombre varchar(20),
	@rolHabilitado bit,
	@listaId varchar(200)) 
AS
BEGIN

--	DECLARE @listaId varchar(200) = '1,6,20,'
--	DECLARE @rolNombre varchar(20) = 'Monitor'
--	DECLARE @rolHabilitado int = 1;
	
	DECLARE @rolId int;
	DECLARE @funcionId varchar(3) = null;
	
	--	Calcula el proximo indice de la tabla test.Rol
	DECLARE @auxCant int;
	SELECT @auxCant = COUNT(rol_id) + 1FROM test.Rol


	-- Inserta el Rol en la tabla test.Rol
	INSERT INTO test.Rol (Rol_Id,Rol_Descripcion,Rol_Habilitado)
	VALUES (@auxCant,@rolNombre,@rolHabilitado);

	-- Recupera el Id del Rol insertado
	SELECT @rolId = Rol_Id FROM test.Rol WHERE Rol_Descripcion = @rolNombre

	-- Relaciona el nuevo rol (rolId) con las funciones seleccionadas (listaId)
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
