USE [GD1C2015]
GO
/****** Object:  StoredProcedure [test].[Ejecutar_Autenticacion]    Script Date: 06/05/2015 00:28:04 ******/
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
	@clienteId integer OUTPUT,
	@clienteDocumento numeric(18,0) OUTPUT) --Cliente_Doc_Nro
AS
BEGIN
--	Busca el usuario para ver si existe y si esta habilitado o no

--DECLARE	@codigo int, @habilitado int, @nombre nvarchar(255), @apellido nvarchar(255), @clienteId integer; 

	SELECT @codigo = Usuario_Habilitado, @nombre = Cliente_Nombre, 
			@apellido = Cliente_Apellido, @clienteId = Cliente_Id,
			@clienteDocumento = Cliente_Doc_Nro
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

/*
DECLARE @usuario nvarchar(255)='userCliente', @fechaHora datetime = SYSDATETIME(), @intento integer;
	IF(@loginEstado = 0)
	BEGIN

		-- Recupera el numero de intento anterior
		select TOP 1 @intento = LogLogin_Intento from test.loglogin
		where LogLogin_usuario = @usuario
		order by LogLogin_FechaHora desc

		-- Aumenta el numero de intento
		SET @intento = @intento + 1

		-- Inserta en la tabla de LogLogin
		INSERT INTO test.loglogin (LogLogin_FechaHora,LogLogin_Usuario,LogLogin_Valido,LogLogin_Intento)
		VALUES(@fechaHora,@usuario,0,@intento)
	END
	ELSE
	BEGIN
		INSERT INTO test.loglogin (LogLogin_FechaHora,LogLogin_Usuario,LogLogin_Valido,LogLogin_Intento)
		VALUES(@fechaHora,@usuario,1,0)
	END
*/	
	
--	select @codigo 'Codigo', @nombre 'Nombre', @apellido 'Apellido', @clienteId 'ClienteId'
	
END