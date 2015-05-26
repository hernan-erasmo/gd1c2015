USE [GD1C2015]
GO
/****** Object:  StoredProcedure [test].[Cargar_FuncionesSistema]    Script Date: 05/26/2015 14:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [test].[Cargar_FuncionesSistema] (
                    @rolId int,
                    @form varchar(100))
AS
BEGIN

--DECLARE @rolId int, @form varchar(100);
--SET @rolId = 2;
--SET @form  = 'Modificar';

IF(@form = 'Buscar')
	-- Funciones del rol seleccionado (rolId)
	SELECT Funcion_Id, Funcion_Descripcion, 0 'Seleccionado'
	FROM test.Rol, test.RolFuncion, test.Funcion
	WHERE Rol_Id = RolFuncion_RolId AND RolFuncion_FuncionId = Funcion_Id
		AND Rol_Id = @rolId
ELSE IF(@form = 'Crear')
	-- Todas las funciones del sistema
	SELECT Funcion_Id, Funcion_Descripcion, 0 'Seleccionado'
	FROM test.Funcion
ELSE IF(@form = 'Modificar')
	--	Todas las funciones, y cuales estan asociadas aun determinado rol (rolId)
	SELECT f.Funcion_Id, f.Funcion_Descripcion, 
		isnull((SELECT RolFuncion_RolId
				FROM test.RolFuncion 
				WHERE RolFuncion_FuncionId = f.Funcion_Id
				AND RolFuncion_RolId = @rolId),0) 'Seleccionado'
	FROM test.Funcion f
ELSE
	SELECT 'Formulario desconocido'


END