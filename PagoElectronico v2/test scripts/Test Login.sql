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


CREATE TABLE test.Tc (
	Tc_Num_Tarjeta			varchar(64),	--64 bytes ya que el hash de sha256 tiene 64 caracteres
	Tc_Cliente_Id			integer,
	Tc_Fecha_Emision		datetime		NOT NULL,
	Tc_Fecha_Vencimiento	datetime		NOT NULL,
	Tc_Codigo_Seg			nvarchar(64)	NOT NULL,	--64 bytes ya que el hash de sha256 tiene 64 caracteres
	Tc_Emisor_Desc			nvarchar(255)	NOT NULL,
	Tc_Ultimos_Cuatro		nvarchar(4)		NOT NULL
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
	Rol_Habilitado			bit DEFAULT 1	-- 1: Habilitado, 0: No habilitado
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

---------------------------------------------------------------------------------
CREATE TABLE test.Moneda (
	Moneda_Id				integer			identity(1,1) PRIMARY KEY,
	Moneda_Descripcion		varchar(255)	NOT NULL
)

CREATE TABLE test.Estado (
	Estado_Id				integer			identity(1,1) PRIMARY KEY,
	Estado_Descripcion		nvarchar(255)	NOT NULL
)

CREATE TABLE test.Cuenta (
	Cuenta_Numero				numeric(18,0),
	Cuenta_Fecha_Creacion		datetime,
	Cuenta_Fecha_Cierre			datetime,
	Cuenta_Estado_Id			integer NULL,
	Cuenta_Tipocta_Id			integer NULL,
	Cuenta_Pais_Id				integer NULL,
	Cuenta_Moneda_Id			integer	NULL,
	Cuenta_Saldo				numeric(18,0),
	Cuenta_Deudora				bit DEFAULT 0,	-- 1: Tiene deuda (valor < 0.00), 0: No tiene deuda (valor >= 0.00)
	Cuenta_Cliente_Id			integer	NULL
)

CREATE TABLE test.Tipocta (
	Tipocta_Id					integer			identity(1,1) PRIMARY KEY,
	Tipocta_Descripcion			nvarchar(255)	NOT NULL,
	Tipocta_Vencimiento_Dias	integer			NOT NULL,
	Tipocta_Costo_Crea			numeric(18,2)	NOT NULL,
	Tipocta_Costo_Mod			numeric(18,2)	NOT NULL,
	Tipocta_Costo_Trans			numeric(18,2)	NOT NULL,

	CHECK (Tipocta_Vencimiento_Dias >= 1),
	CHECK (Tipocta_Costo_Crea >= 0),
	CHECK (Tipocta_Costo_Mod >= 0),
	CHECK (Tipocta_Costo_Trans >= 0)
)

CREATE TABLE test.Deposito (
	Deposito_Id					numeric(18,0),--	identity(1,1) PRIMARY KEY,
	Deposito_Fecha				datetime,--		NOT NULL,
	Deposito_Importe			numeric(18,2),--	NOT NULL,
	Deposito_Moneda_Id			integer,--			FOREIGN KEY REFERENCES SARASA.Moneda (Moneda_Id) NOT NULL,
	Deposito_Tc_Num_Tarjeta		varchar(64),--		FOREIGN KEY REFERENCES SARASA.Tc (Tc_Num_Tarjeta) NOT NULL,
	Deposito_Cuenta_Numero		numeric(18,0),--	FOREIGN KEY REFERENCES SARASA.Cuenta (Cuenta_Numero) NOT NULL,
	Deposito_Codigo_Ingreso		varchar(32),	--Es NULL hasta que se dispara el trigger after insert para generarlo.

--	CHECK (Deposito_Importe >= 0)
)

CREATE TABLE test.Pais (
	Pais_Id 		integer			identity(1,1) PRIMARY KEY,
	Pais_Nombre		nvarchar(255)
)
--Migra los paises desde tabla maestra
SET IDENTITY_INSERT test.Pais ON
INSERT INTO test.Pais (Pais_Id, Pais_Nombre)
SELECT DISTINCT maestra.Cli_Pais_Codigo, maestra.Cli_Pais_Desc			-- Países que figuran como atributos de clientes.
FROM gd_esquema.Maestra maestra
UNION	
SELECT DISTINCT maestra.Cuenta_Pais_Codigo, maestra.Cuenta_Pais_Desc	-- Países que figuran como atributos de cuentas.
FROM gd_esquema.Maestra maestra
SET IDENTITY_INSERT test.Pais OFF


--------------------------------------------------------------------------------------------------
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

-- ALTA de Moneda
INSERT INTO test.Moneda (Moneda_Descripcion)
VALUES ('Dólar Estadounidense')

-- ALTA de Estados
INSERT INTO test.Estado (Estado_Descripcion)
VALUES ('Pendiente de activación'), ('Cerrada'), ('Inhabilitada'), ('Habilitada')

-- ALTA DE Cuenta
INSERT INTO test.Cuenta([Cuenta_Numero],[Cuenta_Fecha_Creacion],[Cuenta_Fecha_Cierre],
		[Cuenta_Estado_Id],[Cuenta_Tipocta_Id],[Cuenta_Pais_Id],[Cuenta_Moneda_Id],
		[Cuenta_Saldo],[Cuenta_Deudora],[Cuenta_Cliente_Id])
	VALUES(111110000000010001,'28/05/2015','28/06/2015',4,2,null,1,0,0,20)
	
INSERT INTO test.Cuenta([Cuenta_Numero],[Cuenta_Fecha_Creacion],[Cuenta_Fecha_Cierre],
		[Cuenta_Estado_Id],[Cuenta_Tipocta_Id],[Cuenta_Pais_Id],[Cuenta_Moneda_Id],
		[Cuenta_Saldo],[Cuenta_Deudora],[Cuenta_Cliente_Id])
	VALUES(111110000000010002,'28/05/2015','28/07/2015',4,3,null,1,0,0,20)


--	ALTA DE CLIENTE
INSERT INTO test.Cliente (Cliente_Id,Cliente_Nombre,Cliente_Apellido,Cliente_Doc_Nro)
VALUES (1,'Aien','Rodríguez',60308840);
INSERT INTO test.Cliente (Cliente_Id,Cliente_Nombre,Cliente_Apellido,Cliente_Doc_Nro)
VALUES (2,'Alejandro','Figueroa',30560800);
INSERT INTO test.Cliente (Cliente_Id,Cliente_Nombre,Cliente_Apellido,Cliente_Doc_Nro)
VALUES (3,'Alejo','Córdoba',74291539);
INSERT INTO test.Cliente (Cliente_Id,Cliente_Nombre,Cliente_Apellido,Cliente_Doc_Nro)
VALUES (20,'Guillermo','Franco',59276294);


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
INSERT INTO test.UsuarioRol (UsuarioRol_UsuarioId, UsuarioRol_RolId) VALUES (3,3);

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
