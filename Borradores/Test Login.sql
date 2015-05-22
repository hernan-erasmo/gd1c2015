
--*******************************************************************************
--	Usuario-Rol-Funcion
SELECT Usuario_Habilitado, Usuario_Nombre, Rol_Descripcion, Funcion_Descripcion
FROM test.Usuario, test.UsuarioRol, test.Rol, test.RolFuncion, test.Funcion
WHERE (Usuario_Id = UsuarioRol_UsuarioId AND UsuarioRol_RolId = Rol_Id AND
	Rol_Id = RolFuncion_RolId AND RolFuncion_FuncionId = Funcion_Id)
	
	
--	Rol-Funcion
SELECT Rol_Descripcion, Funcion_Descripcion
FROM test.Rol, test.RolFuncion, test.Funcion
WHERE Rol_Id = RolFuncion_RolId AND RolFuncion_FuncionId = Funcion_Id 


--	Recuperar todos los roles
SELECT Rol_Descripcion FROM test.Rol

SELECT * FROM test.Usuario

SELECT * FROM test.Funcion

SELECT * FROM test.Rol
SELECT * FROM test.UsuarioRol
SELECT * FROM test.RolFuncion


--Deshabilitar usuario
UPDATE test.Usuario SET Usuario_Habilitado = 0
WHERE Usuario_Nombre = 'userCliente'


--*******************************************************************************
CREATE PROCEDURE test.EjecutarAutenticacion (
	@username varchar(100),
	@password varchar(100),
	@habilitado varchar(100)) 
AS
BEGIN

--	declare
--	@username varchar(100),
--	@password varchar(100),
--	@habilitado varchar(100);

	-- Revisar si existe el usuario
	set @username= 'userCliente';
	set @password='2222';

	SET @habilitado = (SELECT Usuario_Habilitado FROM test.Usuario WHERE Usuario_Nombre = @username AND Usuario_Password = @password)

	SELECT @habilitado 'Habilitado'	--Cantidad de filas devueltas
	SELECT @@ROWCOUNT  'Cant Resultados'	--Cantidad de filas devueltas

END
--*******************************************************************************
--*******************************************************************************

CREATE TABLE test.Usuario (
	Usuario_Id					integer,
	Usuario_Nombre				nvarchar(12),
	Usuario_Password			nvarchar(8),
	Usuario_Habilitado			bit DEFAULT 1	-- 1: Habilitado, 0: No habilitado
)

CREATE TABLE test.Rol (
	Rol_Id					integer,
	Rol_Descripcion			nvarchar(20)
)

CREATE TABLE test.Funcion (
	Funcion_Id					integer,
	Funcion_Descripcion			nvarchar(20),
)

CREATE TABLE test.RolFuncion (
	RolFuncion_RolId			integer,
	RolFuncion_FuncionId		integer
)

CREATE TABLE test.UsuarioRol (
	UsuarioRol_UsuarioId		integer,
	UsuarioRol_RolId			integer
)


INSERT INTO test.Usuario (Usuario_Id, Usuario_Nombre, Usuario_Password) VALUES (1,'userAdmin','1111');
INSERT INTO test.Usuario (Usuario_Id, Usuario_Nombre, Usuario_Password) VALUES (2,'userCliente','2222');


INSERT INTO test.Rol (Rol_Id, Rol_Descripcion) VALUES (1,'Administrador');
INSERT INTO test.Rol (Rol_Id, Rol_Descripcion) VALUES (2,'Cliente');
INSERT INTO test.Rol (Rol_Id, Rol_Descripcion) VALUES (3,'Operador');


INSERT INTO test.Funcion (Funcion_Id, Funcion_Descripcion) VALUES (1,'AsociarTarjeta');
INSERT INTO test.Funcion (Funcion_Id, Funcion_Descripcion) VALUES (2,'DesasociarTarjeta');
INSERT INTO test.Funcion (Funcion_Id, Funcion_Descripcion) VALUES (3,'ModificarTarjeta');
INSERT INTO test.Funcion (Funcion_Id, Funcion_Descripcion) VALUES (4,'AceptarTarjeta');
INSERT INTO test.Funcion (Funcion_Id, Funcion_Descripcion) VALUES (5,'BuscarTarjeta');


INSERT INTO test.RolFuncion (RolFuncion_RolId, RolFuncion_FuncionId) VALUES (1,1);
INSERT INTO test.RolFuncion (RolFuncion_RolId, RolFuncion_FuncionId) VALUES (1,2);
INSERT INTO test.RolFuncion (RolFuncion_RolId, RolFuncion_FuncionId) VALUES (1,3);
INSERT INTO test.RolFuncion (RolFuncion_RolId, RolFuncion_FuncionId) VALUES (1,4);
INSERT INTO test.RolFuncion (RolFuncion_RolId, RolFuncion_FuncionId) VALUES (1,5);

INSERT INTO test.RolFuncion (RolFuncion_RolId, RolFuncion_FuncionId) VALUES (2,1);
INSERT INTO test.RolFuncion (RolFuncion_RolId, RolFuncion_FuncionId) VALUES (2,5);

INSERT INTO test.RolFuncion (RolFuncion_RolId, RolFuncion_FuncionId) VALUES (3,2);
INSERT INTO test.RolFuncion (RolFuncion_RolId, RolFuncion_FuncionId) VALUES (3,3);
INSERT INTO test.RolFuncion (RolFuncion_RolId, RolFuncion_FuncionId) VALUES (3,5);

INSERT INTO test.UsuarioRol (UsuarioRol_UsuarioId, UsuarioRol_RolId) VALUES (1,1);
INSERT INTO test.UsuarioRol (UsuarioRol_UsuarioId, UsuarioRol_RolId) VALUES (2,2);



--*******************************************************************************
--	Usuario-Rol-Funcion
SELECT Usuario_Habilitado, Usuario_Nombre, Rol_Descripcion, Funcion_Descripcion
FROM test.Usuario, test.UsuarioRol, test.Rol, test.RolFuncion, test.Funcion
WHERE (Usuario_Id = UsuarioRol_UsuarioId AND UsuarioRol_RolId = Rol_Id AND
	Rol_Id = RolFuncion_RolId AND RolFuncion_FuncionId = Funcion_Id)
	
	
--	Rol-Funcion
SELECT Rol_Descripcion, Funcion_Descripcion
FROM test.Rol, test.RolFuncion, test.Funcion
WHERE Rol_Id = RolFuncion_RolId AND RolFuncion_FuncionId = Funcion_Id 


--	Recuperar todos los roles
SELECT Rol_Descripcion FROM test.Rol

SELECT * FROM test.Usuario

SELECT * FROM test.Funcion

SELECT * FROM test.Rol
SELECT * FROM test.UsuarioRol
SELECT * FROM test.RolFuncion


--Deshabilitar usuario
UPDATE test.Usuario SET Usuario_Habilitado = 0
WHERE Usuario_Nombre = 'userCliente'
--*******************************************************************************
