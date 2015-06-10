--**************************************************************************************

-- Valores de @codigo
--	0: Login OK. usuario habilitado con más de un rol
--	-1: Usuario no existe
--	-2: Usuario o Pass incorrectos
--	x: Login OK, usuario habilitado con un único rol (rolID = x)

CREATE PROCEDURE [SARASA].[Ejecutar_Autenticacion] (
	@username nvarchar(20),
	@password nvarchar(64),
	@codigo integer OUTPUT,
	@nombre nvarchar(255) OUTPUT,
	@apellido nvarchar(255) OUTPUT,
	@clienteId integer OUTPUT,
	@clienteDocumento numeric(18,0) OUTPUT, --Cliente_Doc_Nro
	@usuarioId integer OUTPUT,
	@rolNombre nvarchar(20) OUTPUT)
AS
BEGIN

--DECLARE	@username nvarchar(20) = 'admin',@password nvarchar(64)='e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7',
--	@codigo integer,@nombre nvarchar(255),@apellido nvarchar(255),@clienteId integer,@clienteDocumento numeric(18,0),
--	@usuarioId integer,@rolNombre nvarchar(20);

--	Busca el usuario para ver si existe y si esta habilitado o no
	SET @rolNombre = ''
	SET @nombre = ''
	SET @apellido = ''
	SET @clienteId = 0
	SET @clienteDocumento = 0

	--	Valida solo el usuario y pass
	SELECT @codigo = Usuario_Habilitado, @usuarioId =Usuario_Id
	FROM SARASA.Usuario
	WHERE Usuario_Username = @username AND Usuario_Password = @password
	


	IF @codigo is null
	BEGIN
		SET @codigo = -1	-- Usuario no existe
		SET @usuarioId=0;
	END
	ELSE
	BEGIN 
		IF @codigo  = 0
		BEGIN
			SET @codigo = -2	-- Usuario inhabilitado
			SET @clienteDocumento = 0
		END
		ELSE
		BEGIN
			-- Busca la información del cliente del usuario autenticado
			

			SELECT @clienteId = Cliente_Id, @nombre = Cliente_Nombre, @apellido = Cliente_Apellido, @clienteDocumento = Cliente_Doc_Nro
			FROM SARASA.Usuario, SARASA.Cliente
			WHERE Usuario_Cliente_Id = Cliente_Id

			
			-- Busca cuantos roles tiene asignado el usuario
			SET @codigo = 0	-- Usuario habilitado
			
			DECLARE @AUX integer = 0;

			SELECT DISTINCT @AUX = COUNT(*)
			FROM SARASA.Rol_x_Usuario ru, SARASA.Rol r
			WHERE ru.Rol_Id = r.Rol_Id AND r.Rol_Estado = 1 AND ru.Usuario_Id = @usuarioId

			IF(@AUX=1)-- Busca el id del unico rol habilitado del usuario
			BEGIN
				SELECT @codigo = ru.Rol_Id, @rolNombre= r.Rol_Descripcion
				FROM SARASA.Rol_x_Usuario ru, Rol r
				WHERE ru.Usuario_Id = @usuarioId AND r.Rol_Id = ru.Rol_Id

			END
		END
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
