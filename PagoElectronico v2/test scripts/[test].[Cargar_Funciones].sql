USE [GD1C2015]
GO
/****** Object:  StoredProcedure [test].[Cargar_Funciones]    Script Date: 05/26/2015 14:18:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [test].[Cargar_Funciones] (
	@username nvarchar(100),
	@rol nvarchar(100)) 
AS
BEGIN

	--	Usuario-Rol-Funcion
	SELECT Funcion_Descripcion
	FROM test.Usuario, test.UsuarioRol, test.Rol, test.RolFuncion, test.Funcion
	WHERE Usuario_Nombre = @username AND Rol_Descripcion = @rol AND 
		(Usuario_Id = UsuarioRol_UsuarioId AND UsuarioRol_RolId = Rol_Id AND
		Rol_Id = RolFuncion_RolId AND RolFuncion_FuncionId = Funcion_Id)
END