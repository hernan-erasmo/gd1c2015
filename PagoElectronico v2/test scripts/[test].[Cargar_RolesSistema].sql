USE [GD1C2015]
GO
/****** Object:  StoredProcedure [test].[Cargar_RolesSistema]    Script Date: 05/26/2015 14:19:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [test].[Cargar_RolesSistema] (
                    @rol varchar(100),
                    @funcion varchar(100))
AS
BEGIN

IF(@rol='' AND @funcion='')
	SELECT Rol_Id, Rol_Habilitado,Rol_Descripcion, Rol_Etiqueta =CASE WHEN(Rol_Habilitado = 1) 
								THEN (Rol_Descripcion + ' (habilitado)')
								ELSE (Rol_Descripcion + ' (deshabilitado)')END
	FROM test.Rol;

ELSE IF(@funcion='')
	SELECT Rol_Id, Rol_Habilitado,Rol_Descripcion, Rol_Etiqueta =CASE WHEN(Rol_Habilitado = 1) 
								THEN (Rol_Descripcion + ' (habilitado)')
								ELSE (Rol_Descripcion + ' (deshabilitado)')END
	FROM test.Rol
	WHERE Rol_Descripcion = @rol;
	
ELSE IF(@rol='')
	SELECT Rol_Id, Rol_Habilitado,Rol_Descripcion, Rol_Etiqueta =CASE WHEN(Rol_Habilitado = 1) 
								THEN (Rol_Descripcion + ' (habilitado)')
								ELSE (Rol_Descripcion + ' (deshabilitado)')END
	FROM test.Rol, test.RolFuncion, test.Funcion
	WHERE Rol_Id = RolFuncion_RolId AND RolFuncion_FuncionId = Funcion_Id 
		AND Funcion_Descripcion = @funcion
ELSE
	SELECT 'No se pudo realizar la busqueda de roles'

END