USE [GD1C2015]
GO
/****** Object:  StoredProcedure [test].[Ejecutar_Autenticacion]    Script Date: 05/26/2015 14:20:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [test].[Ejecutar_Autenticacion] (
	@username nvarchar(12),
	@password nvarchar(8),
	@codigo integer OUTPUT,
	@nombre nvarchar(255) OUTPUT,
	@apellido nvarchar(255) OUTPUT,
	@clienteId integer OUTPUT) 
AS
BEGIN
--	Busca el usuario para ver si existe y si esta habilitado o no

--DECLARE	@codigo int, @habilitado int, @nombre nvarchar(255), @apellido nvarchar(255), @clienteId integer; 

	SELECT @codigo = Usuario_Habilitado, @nombre = Cliente_Nombre, 
			@apellido = Cliente_Apellido, @clienteId = Cliente_Id
	FROM test.Usuario , test.Cliente
--	WHERE Usuario_Nombre = 'userOperador' AND Usuario_Password = '333'
	WHERE Usuario_Nombre = @username AND Usuario_Password = @password
		AND Usuario_Cliente_Id = Cliente_Id 
	
	
	IF @codigo is null
	BEGIN
		SET @codigo = 1	-- Usuario no existe
		SET @nombre = ''
		SET @apellido = ''
		SET @clienteId = 0
	END
	ELSE
	BEGIN 
		IF @codigo  = 0
			SET @codigo = 2	-- Usuario inhabilitado
		ELSE
			SET @codigo = 0	-- Usuario habilitado
	END
	
	
--	select @codigo 'Codigo', @nombre 'Nombre', @apellido 'Apellido', @clienteId 'ClienteId'
	
END