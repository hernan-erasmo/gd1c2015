--*******************************************************************************
--*******************************************************************************

CREATE TABLE test.Cliente (
	Cliente_Id					integer,
	Cliente_Pais_Id				integer,		--Cli_Pais_Codigo en gd_esquema.Maestra
	Cliente_Nombre				nvarchar(255),
	Cliente_Apellido			nvarchar(255),	
	Cliente_Tipodoc_Id			integer,		--Cli_Tipo_Doc_Cod en gd_esquema.Maestra
	Cliente_Doc_Nro				numeric(18,0),	--Cli_Nro_Doc en gd_esquema.Maestra
	Cliente_Dom_Calle			nvarchar(255),
	Cliente_Dom_Numero			numeric(18,0),
	Cliente_Dom_Piso			numeric(18,0),
	Cliente_Dom_Depto			nvarchar(10),
	Cliente_Fecha_Nacimiento	datetime,		--Cli_Fecha_Nac en gd_esquema.Maestra
	Cliente_Mail				nvarchar(255),
	Cliente_Habilitado			bit DEFAULT 1	-- 1: Habilitado, 0: No habilitado
)


CREATE TABLE test.Usuario (
	Usuario_Id					integer,
	Usuario_Nombre				nvarchar(12),
	Usuario_Password			nvarchar(8),
	Usuario_Habilitado			bit DEFAULT 1	-- 1: Habilitado, 0: No habilitado
	Usuario_Cliente_Id			integer
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



INSERT INTO test.Cliente (
	Cliente_Id					integer,
	Cliente_Pais_Id				integer,		--Cli_Pais_Codigo en gd_esquema.Maestra
	Cliente_Nombre				nvarchar(255),
	Cliente_Apellido			nvarchar(255),	
	Cliente_Tipodoc_Id			integer,		--Cli_Tipo_Doc_Cod en gd_esquema.Maestra
	Cliente_Doc_Nro				numeric(18,0),	--Cli_Nro_Doc en gd_esquema.Maestra
	Cliente_Dom_Calle			nvarchar(255),
	Cliente_Dom_Numero			numeric(18,0),
	Cliente_Dom_Piso			numeric(18,0),
	Cliente_Dom_Depto			nvarchar(10),
	Cliente_Fecha_Nacimiento	datetime,		--Cli_Fecha_Nac en gd_esquema.Maestra
	Cliente_Mail				nvarchar(255),
	Cliente_Habilitado			bit DEFAULT 1	-- 1: Habilitado, 0: No habilitado
)

--	ALTA DE CLIENTE
INSERT INTO test.Cliente (Cliente_Id,Cliente_Nombre,Cliente_Apellido,Cliente_Doc_Nro)
VALUES (1,'Aien','Rodríguez',60308840);
INSERT INTO test.Cliente (Cliente_Id,Cliente_Nombre,Cliente_Apellido,Cliente_Doc_Nro)
VALUES (2,'Alejandro','Figueroa',30560800);
INSERT INTO test.Cliente (Cliente_Id,Cliente_Nombre,Cliente_Apellido,Cliente_Doc_Nro)
VALUES (3,'Alejo','Córdoba',74291539);


--	ALTA DE USUARIO
INSERT INTO test.Usuario (Usuario_Id, Usuario_Nombre, Usuario_Password) VALUES (1,'userAdmin','1111');
INSERT INTO test.Usuario (Usuario_Id, Usuario_Nombre, Usuario_Password) VALUES (2,'userCliente','2222');
INSERT INTO test.Usuario (Usuario_Id, Usuario_Nombre, Usuario_Password) VALUES (3,'userOperador','3333');

--	ALTA DE ROL
INSERT INTO test.Rol (Rol_Id, Rol_Descripcion) VALUES (1,'Administrador');
INSERT INTO test.Rol (Rol_Id, Rol_Descripcion) VALUES (2,'Cliente');
INSERT INTO test.Rol (Rol_Id, Rol_Descripcion) VALUES (3,'Operador');


--	ALTA DE FUNCION
INSERT INTO test.Funcion (Funcion_Id, Funcion_Descripcion) VALUES (1,'AsociarTarjeta');
INSERT INTO test.Funcion (Funcion_Id, Funcion_Descripcion) VALUES (2,'DesasociarTarjeta');
INSERT INTO test.Funcion (Funcion_Id, Funcion_Descripcion) VALUES (3,'ModificarTarjeta');
INSERT INTO test.Funcion (Funcion_Id, Funcion_Descripcion) VALUES (4,'AceptarTarjeta');
INSERT INTO test.Funcion (Funcion_Id, Funcion_Descripcion) VALUES (5,'BuscarTarjeta');

--	ASOCIAR A ROL UNA FUNCION
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

-- ASOCIAR A USUARIO UN ROL
INSERT INTO test.UsuarioRol (UsuarioRol_UsuarioId, UsuarioRol_RolId) VALUES (1,1);
INSERT INTO test.UsuarioRol (UsuarioRol_UsuarioId, UsuarioRol_RolId) VALUES (2,2);
NSERT INTO test.UsuarioRol (UsuarioRol_UsuarioId, UsuarioRol_RolId) VALUES (3,3);

-- ASOCIAR A USUARIO UN CLIENTE
UPDATE test.USUARIO SET Usuario_Cliente_Id = 1 WHERE Usuario_Id = 1
UPDATE test.USUARIO SET Usuario_Cliente_Id = 2 WHERE Usuario_Id = 2
UPDATE test.USUARIO SET Usuario_Cliente_Id = 3 WHERE Usuario_Id = 3


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

--	Usuario-Cliente
SELECT * 
FROM test.Usuario, test.Cliente
WHERE Usuario_cliente_Id = Cliente_Id

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
