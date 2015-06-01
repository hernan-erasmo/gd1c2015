USE GD1C2015

GO

/**************************************************************************************************************
	Borramos todas las dependencias del schema SARASA, antes de borrarlo. Para esto vamos concatenando
	queries dentro de la variable @drop_schema_dependencies y luego la ejecutamos de una al final de todo.
***************************************************************************************************************/

DECLARE @schema_name				varchar(max)	= 'SARASA';
DECLARE @schema_id					int				= schema_id(@schema_name),
		@drop_schema_dependencies	varchar(max)	= ''

-- Borramos los FKs
SELECT @drop_schema_dependencies = @drop_schema_dependencies +
		'alter table [' + @schema_name + '].[' + OBJECT_NAME(parent_object_id) + '] ' +
		'drop CONSTRAINT [' + OBJECT_NAME(object_id) + '];' + CHAR(30)
FROM sys.foreign_keys WHERE schema_id = @schema_id

-- Borramos las vistas
SELECT @drop_schema_dependencies = @drop_schema_dependencies +
		'drop view [' + @schema_name + '].[' + OBJECT_NAME(object_id) + '];' + CHAR(30)
FROM sys.views WHERE schema_id = @schema_id

-- Borramos las funciones
SELECT @drop_schema_dependencies = @drop_schema_dependencies +
		'drop function [' + @schema_name + '].[' + OBJECT_NAME(object_id) + '];' + CHAR(30)
FROM sys.objects WHERE type in ('FN', 'IF', 'TF', 'FS', 'FT') and schema_id = @schema_id
	
-- Borramos las tablas
SELECT @drop_schema_dependencies = @drop_schema_dependencies +
		'drop table [' + @schema_name + '].[' + OBJECT_NAME(object_id) + '];' + CHAR(30)
FROM sys.tables WHERE schema_id = @schema_id

-- Borramos los stored procedures
SELECT @drop_schema_dependencies = @drop_schema_dependencies +
		'drop procedure [' + @schema_name + '].[' + OBJECT_NAME(object_id) + '];' + CHAR(30)
FROM sys.procedures WHERE schema_id = @schema_id

-- Borramos cualquier tipo definido por nosotros
SELECT @drop_schema_dependencies = @drop_schema_dependencies +
		'drop type [' + @schema_name + '].[' + name + '];' + CHAR(30)
FROM sys.types WHERE schema_id = @schema_id	

-- Ahora si, ejecutamos el query que borra todas las dependencias del schema
EXEC (@drop_schema_dependencies)
GO

/************************************************
	Borramos el schema SARASA, si es que existe
*************************************************/

IF EXISTS (SELECT * FROM sys.schemas WHERE	name = 'SARASA')
DROP SCHEMA [SARASA]
GO

/*****************************
	Creamos el schema SARASA
******************************/

CREATE SCHEMA SARASA AUTHORIZATION gd
GO

/***********************
	Creamos las tablas
************************/

CREATE TABLE SARASA.Cliente (
	Cliente_Id					integer			identity(1,1) PRIMARY KEY,
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

CREATE TABLE SARASA.Pais (
	Pais_Id 		integer			identity(1,1) PRIMARY KEY,
	Pais_Nombre		nvarchar(255)
)

CREATE TABLE SARASA.Tipodoc (
	Tipodoc_Id 				integer			identity(1,1) PRIMARY KEY,
	Tipodoc_Descripcion		nvarchar(255)
)

CREATE TABLE SARASA.Tc (
	Tc_Num_Tarjeta			varchar(64)		PRIMARY KEY,	--64 bytes ya que el hash de sha256 tiene 64 caracteres
	Tc_Cliente_Id			integer			FOREIGN KEY REFERENCES SARASA.Cliente (Cliente_Id) NOT NULL,
	Tc_Fecha_Emision		datetime		NOT NULL,
	Tc_Fecha_Vencimiento	datetime		NOT NULL,
	Tc_Codigo_Seg			nvarchar(64)	NOT NULL,	--64 bytes ya que el hash de sha256 tiene 64 caracteres
	Tc_Emisor_Desc			nvarchar(255)	NOT NULL,
	Tc_Ultimos_Cuatro		nvarchar(4)		NOT NULL
)

CREATE TABLE SARASA.Moneda (
	Moneda_Id				integer			identity(1,1) PRIMARY KEY,
	Moneda_Descripcion		varchar(255)	NOT NULL
)

CREATE TABLE SARASA.Estado (
	Estado_Id				integer			identity(1,1) PRIMARY KEY,
	Estado_Descripcion		nvarchar(255)	NOT NULL
)

CREATE TABLE SARASA.Tipocta (
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

CREATE TABLE SARASA.Cuenta (
	Cuenta_Numero				numeric(18,0)	identity(1,1) PRIMARY KEY,
	Cuenta_Fecha_Creacion		datetime,
	Cuenta_Fecha_Cierre			datetime,
	Cuenta_Estado_Id			integer			FOREIGN KEY REFERENCES SARASA.Estado (Estado_Id) NOT NULL,
	Cuenta_Tipocta_Id			integer			FOREIGN KEY REFERENCES SARASA.Tipocta (Tipocta_Id) NOT NULL,
	Cuenta_Pais_Id				integer			FOREIGN KEY REFERENCES SARASA.Pais (Pais_Id) NOT NULL,
	Cuenta_Moneda_Id			integer			FOREIGN KEY REFERENCES SARASA.Moneda (Moneda_Id) NOT NULL,
	Cuenta_Saldo				numeric(18,0),
	Cuenta_Deudora				bit DEFAULT 0,	-- 1: Tiene deuda (valor < 0.00), 0: No tiene deuda (valor >= 0.00)
	Cuenta_Cliente_Id			integer			FOREIGN KEY REFERENCES SARASA.Cliente (Cliente_Id) NOT NULL
)

CREATE TABLE SARASA.Deposito (
	Deposito_Id					numeric(18,0)	identity(1,1) PRIMARY KEY,
	Deposito_Fecha				datetime		NOT NULL,
	Deposito_Importe			numeric(18,2)	NOT NULL,
	Deposito_Moneda_Id			integer			FOREIGN KEY REFERENCES SARASA.Moneda (Moneda_Id) NOT NULL,
	Deposito_Tc_Num_Tarjeta		varchar(64)		FOREIGN KEY REFERENCES SARASA.Tc (Tc_Num_Tarjeta) NOT NULL,
	Deposito_Cuenta_Numero		numeric(18,0)	FOREIGN KEY REFERENCES SARASA.Cuenta (Cuenta_Numero) NOT NULL,
	Deposito_Codigo_Ingreso		varchar(32),	--Es NULL hasta que se dispara el trigger after insert para generarlo.

	CHECK (Deposito_Importe >= 0)
)

CREATE TABLE SARASA.Banco (
	Banco_Codigo				numeric(18,0)	identity(1,1) PRIMARY KEY,
	Banco_Nombre				nvarchar(255)	NOT NULL,
	Banco_Direccion				nvarchar(255)
)

CREATE TABLE SARASA.Cheque (
	Cheque_Id					numeric(18,0)	identity(1,1) PRIMARY KEY,
	Cheque_Importe				numeric(18,2)	NOT NULL,
	Cheque_Fecha				datetime,
	Cheque_Banco_Id				numeric(18,0)	FOREIGN KEY REFERENCES SARASA.Banco (Banco_Codigo) NOT NULL,
	Cheque_Cliente_Nombre		nvarchar(510)	NOT NULL,
	Cheque_Numero				numeric(18,0)	NOT NULL
)

CREATE TABLE SARASA.Retiro (
	Retiro_Id					numeric(18,0)	identity(1,1) PRIMARY KEY,
	Retiro_Cuenta_Id			numeric(18,0)	FOREIGN KEY REFERENCES SARASA.Cuenta (Cuenta_Numero) NOT NULL,
	Retiro_Cheque_Id			numeric(18,0)	FOREIGN KEY REFERENCES SARASA.Cheque (Cheque_Id) NOT NULL,
	Retiro_Importe				numeric(18,2)	NOT NULL,
	Retiro_Fecha				datetime,
	Retiro_Codigo_Egreso		varchar(32)		--Es NULL hasta que se dispara el trigger after insert para generarlo.
)

CREATE TABLE SARASA.Transferencia (
	Transferencia_Id				numeric(18,0)	identity(1,1) PRIMARY KEY,
	Transferencia_Cuenta_Origen_Id	numeric(18,0)	FOREIGN KEY REFERENCES SARASA.Cuenta (Cuenta_Numero) NOT NULL,
	Transferencia_Cuenta_Destino_Id	numeric(18,0)	FOREIGN KEY REFERENCES SARASA.Cuenta (Cuenta_Numero) NOT NULL,
	Transferencia_Importe			numeric(18,2)	NOT NULL,
	Transferencia_Fecha				datetime		NOT NULL,
	Transferencia_Costo				numeric(18,2)	NOT NULL
)

CREATE TABLE SARASA.Factura (
	Factura_Numero					numeric(18,0)	identity(1,1) PRIMARY KEY,
	Factura_Fecha					datetime		NOT NULL,
	Factura_Cliente_Id				integer			FOREIGN KEY REFERENCES SARASA.Cliente(Cliente_Id) NOT NULL
)

CREATE TABLE SARASA.Itemfact (
	Itemfact_Id						numeric(18,0)	identity(1,1) PRIMARY KEY,
	Itemfact_Cuenta_Numero			numeric(18,0)	FOREIGN KEY REFERENCES SARASA.Cuenta(Cuenta_Numero) NOT NULL,
	Itemfact_Descripcion			nvarchar(255),
	Itemfact_Importe				numeric(18,2)	NOT NULL,
	Itemfact_Fecha					datetime		NOT NULL,
	Itemfact_Factura_Numero			numeric(18,0)	NOT NULL,
	Itemfact_Pagado					bit DEFAULT 1,	--1: Pagado, 0: No pagado
)

CREATE TABLE SARASA.Usuario (
	Usuario_Id						integer			identity(1,1) PRIMARY KEY,
	Usuario_Username				nvarchar(12)	NOT NULL,
	Usuario_Password				nvarchar(64)	NOT NULL,
	Usuario_Fecha_Creacion			datetime		NOT NULL,
	Usuario_Fecha_Modificacion		datetime,
	Usuario_Pregunta_Sec			nvarchar(255)	NOT NULL,
	Usuario_Respuesta_Sec			nvarchar(64)	NOT NULL,
	Usuario_Habilitado				bit DEFAULT 1, 	--1: Habilitado, 0: No habilitado
	Usuario_Cliente_Id				integer			FOREIGN KEY REFERENCES SARASA.Cliente(Cliente_Id)
)

CREATE TABLE SARASA.Rol (
	Rol_Id							integer			identity(1,1) PRIMARY KEY,
	Rol_Descripcion					nvarchar(20)	NOT NULL,
	Rol_Estado						bit DEFAULT 1 	--1: Activo, 0: No activo
)

CREATE TABLE SARASA.Rol_x_Usuario (
	Rol_Id 							integer			FOREIGN KEY REFERENCES SARASA.Rol(Rol_Id) NOT NULL,
	Usuario_Id 						integer			FOREIGN KEY REFERENCES SARASA.Usuario(Usuario_Id) NOT NULL
)

CREATE TABLE SARASA.Funcion (
	Funcion_Id						integer			identity(1,1) PRIMARY KEY,
	Funcion_Descripcion				nvarchar(20)	NOT NULL
)
GO

/****************************************
	Creamos claves primarias y foráneas
*****************************************/

ALTER TABLE SARASA.Cliente ADD FOREIGN KEY (Cliente_Pais_Id) REFERENCES SARASA.Pais (Pais_Id)
ALTER TABLE SARASA.Cliente ADD FOREIGN KEY (Cliente_Tipodoc_Id) REFERENCES SARASA.Tipodoc (Tipodoc_Id)

ALTER TABLE SARASA.Rol_x_Usuario ADD PRIMARY KEY (Rol_Id, Usuario_Id)
GO

/*******************************
	Creamos funciones y SPs
********************************/

CREATE FUNCTION SARASA.generar_codigo_ingreso(@deposito_id numeric(18,0))
RETURNS varchar(32)
AS
BEGIN
	DECLARE @resultado_hash_binario varbinary(max)
	DECLARE	@hash_string varchar(32)
	SET @resultado_hash_binario = HASHBYTES('MD2',CAST(@deposito_id as varchar(18)))
	SET @hash_string = CONVERT(varchar(32),@resultado_hash_binario,2)
	RETURN @hash_string
END
GO

CREATE FUNCTION SARASA.generar_codigo_egreso(@retiro_id numeric(18,0))
RETURNS varchar(32)
AS
BEGIN
	DECLARE @resultado_hash_binario varbinary(max)
	DECLARE	@hash_string varchar(32)
	SET @resultado_hash_binario = HASHBYTES('MD4',CAST(@retiro_id as varchar(18)))
	SET @hash_string = CONVERT(varchar(32),@resultado_hash_binario,2)
	RETURN @hash_string
END
GO

/***********************
	Creamos triggers
************************/

CREATE TRIGGER SARASA.tr_deposito_aff_ins_generar_codigo
ON SARASA.Deposito
AFTER INSERT
AS
BEGIN
	UPDATE SARASA.Deposito
	SET Deposito_Codigo_Ingreso = SARASA.generar_codigo_ingreso(Deposito_Id)
	WHERE Deposito_Codigo_Ingreso IS NULL
END
GO

CREATE TRIGGER SARASA.tr_retiro_aff_ins_generar_codigo
ON SARASA.Retiro
AFTER INSERT
AS
BEGIN
	UPDATE SARASA.Retiro
	SET Retiro_Codigo_Egreso = SARASA.generar_codigo_egreso(Retiro_Id)
	WHERE Retiro_Codigo_Egreso IS NULL
END
GO

/************************************************************************
	Insertamos los datos que no estan disponibles en la tabla maestra.
*************************************************************************/

INSERT INTO SARASA.Moneda (Moneda_Descripcion)
VALUES ('Dólar Estadounidense')

INSERT INTO SARASA.Estado (Estado_Descripcion)
VALUES ('Pendiente de activación'), ('Cerrada'), ('Inhabilitada'), ('Habilitada')

INSERT INTO SARASA.Tipocta (Tipocta_Descripcion, Tipocta_Vencimiento_Dias, Tipocta_Costo_Crea, Tipocta_Costo_Mod, Tipocta_Costo_Trans)
VALUES 	('Gratuita', 2147483647, 0, 0, 0),
		('Bronce', 30, 5, 1, 3),
		('Plata', 60, 10, 1, 2),
		('Oro', 90, 15, 1, 1)
GO

/**********************************************************************
	Migramos los datos ubicados en la tabla maestra a nuestro esquema
***********************************************************************/

-- Desde tabla gd_esquema.Maestra a SARASA.Pais
SET IDENTITY_INSERT SARASA.Pais ON

INSERT INTO SARASA.Pais (Pais_Id, Pais_Nombre)
SELECT DISTINCT maestra.Cli_Pais_Codigo, maestra.Cli_Pais_Desc			-- Países que figuran como atributos de clientes.
FROM gd_esquema.Maestra maestra
UNION	
SELECT DISTINCT maestra.Cuenta_Pais_Codigo, maestra.Cuenta_Pais_Desc	-- Países que figuran como atributos de cuentas.
FROM gd_esquema.Maestra maestra

SET IDENTITY_INSERT SARASA.Pais OFF
GO

-- Desde tabla gd_esquema.Maestra a SARASA.Tipodoc
SET IDENTITY_INSERT SARASA.Tipodoc ON

INSERT INTO SARASA.Tipodoc (Tipodoc_Id, Tipodoc_Descripcion)
SELECT DISTINCT maestra.Cli_Tipo_Doc_Cod, maestra.Cli_Tipo_Doc_Desc
FROM gd_esquema.Maestra maestra

SET IDENTITY_INSERT SARASA.Tipodoc OFF
GO

-- Desde tabla gd_esquema.Maestra a SARASA.Cliente
INSERT INTO SARASA.Cliente (Cliente_Pais_Id, 
							Cliente_Nombre,
							Cliente_Apellido,
							Cliente_Tipodoc_Id,
							Cliente_Doc_Nro,
							Cliente_Dom_Calle,
							Cliente_Dom_Numero,
							Cliente_Dom_Piso,
							Cliente_Dom_Depto,
							Cliente_Fecha_Nacimiento,
							Cliente_Mail,
							Cliente_Habilitado)
SELECT DISTINCT maestra.Cli_Pais_Codigo,
				maestra.Cli_Nombre,
				maestra.Cli_Apellido,
				maestra.Cli_Tipo_Doc_Cod,
				maestra.Cli_Nro_Doc,
				maestra.Cli_Dom_Calle,
				maestra.Cli_Dom_Nro,
				maestra.Cli_Dom_Piso,
				maestra.Cli_Dom_Depto,
				maestra.Cli_Fecha_Nac,
				maestra.Cli_Mail,
				1		-- 1: Habilitado, 0: No habilitado
FROM gd_esquema.Maestra maestra
GO

-- Desde tabla gd_esquema.Maestra a SARASA.Tc
INSERT INTO SARASA.Tc (	Tc_Num_Tarjeta, 
						Tc_Cliente_Id,
						Tc_Fecha_Emision,
						Tc_Fecha_Vencimiento,
						Tc_Codigo_Seg,
						Tc_Emisor_Desc,
						Tc_Ultimos_Cuatro)
SELECT DISTINCT tm.Tarjeta_Numero,
				(	SELECT cli.Cliente_Id
					FROM SARASA.Cliente cli
					WHERE tm.Tarjeta_Numero IS NOT NULL AND tm.Cli_Nro_Doc = cli.Cliente_Doc_Nro),
				tm.Tarjeta_Fecha_Emision,
				tm.Tarjeta_Fecha_Vencimiento,
				tm.Tarjeta_Codigo_Seg,
				tm.Tarjeta_Emisor_Descripcion,
				SUBSTRING(tm.Tarjeta_Numero,13,16)				
FROM gd_esquema.Maestra tm
WHERE tm.Tarjeta_Numero IS NOT NULL
GO

-- Desde tabla gd_esquema.Maestra a SARASA.Cuenta
SET IDENTITY_INSERT SARASA.Cuenta ON
INSERT INTO SARASA.Cuenta (	Cuenta_Numero,
							Cuenta_Fecha_Creacion,
							Cuenta_Fecha_Cierre,
							Cuenta_Estado_Id,
							Cuenta_Tipocta_Id,
							Cuenta_Pais_Id,
							Cuenta_Moneda_Id,
							Cuenta_Saldo,
							Cuenta_Deudora,
							Cuenta_Cliente_Id)
SELECT DISTINCT tm.Cuenta_Numero,
				tm.Cuenta_Fecha_Creacion,
				tm.Cuenta_Fecha_Cierre,
				e.Estado_Id,
				t.Tipocta_Id,
				tm.Cuenta_Pais_Codigo,
				m.Moneda_Id,
				0.00,
				0,
				(SELECT cli.Cliente_Id
					FROM SARASA.Cliente cli
					WHERE tm.Cuenta_Numero IS NOT NULL AND tm.Cli_Nro_Doc = cli.Cliente_Doc_Nro)

FROM gd_esquema.Maestra tm, SARASA.Estado e, SARASA.Tipocta t, SARASA.Moneda m
WHERE e.Estado_Descripcion = 'Habilitada' AND t.Tipocta_Descripcion = 'Oro' AND m.Moneda_Descripcion = 'Dólar Estadounidense'
SET IDENTITY_INSERT SARASA.Cuenta OFF
GO

-- Desde tabla gd_esquema.Maestra a SARASA.Deposito
SET IDENTITY_INSERT SARASA.Deposito ON
INSERT INTO SARASA.Deposito (	Deposito_Id,
								Deposito_Fecha,
								Deposito_Importe,
								Deposito_Moneda_Id,
								Deposito_Tc_Num_Tarjeta,
								Deposito_Cuenta_Numero)
SELECT DISTINCT tm.Deposito_Codigo,
				tm.Deposito_Fecha,
				tm.Deposito_Importe,
				m.Moneda_Id,
				tm.Tarjeta_Numero,
				tm.Cuenta_Numero
FROM gd_esquema.Maestra tm, SARASA.Moneda m
WHERE tm.Deposito_Codigo IS NOT NULL
AND m.Moneda_Descripcion = 'Dólar Estadounidense'
SET IDENTITY_INSERT SARASA.Deposito OFF
GO

-- Desde tabla gd_esquema.Maestra a SARASA.Banco
SET IDENTITY_INSERT SARASA.Banco ON
INSERT INTO SARASA.Banco (	Banco_Codigo,
							Banco_Nombre,
							Banco_Direccion)
SELECT DISTINCT tm.Banco_Cogido,
				tm.Banco_Nombre,
				tm.Banco_Direccion
FROM gd_esquema.Maestra tm
WHERE tm.Banco_Cogido IS NOT NULL
SET IDENTITY_INSERT SARASA.Banco OFF
GO

-- Desde tabla gd_esquema.Maestra a SARASA.Cheque
INSERT INTO SARASA.Cheque (	Cheque_Numero,
							Cheque_Fecha,
							Cheque_Importe,
							Cheque_Cliente_Nombre,
							Cheque_Banco_Id)
SELECT DISTINCT tm.Cheque_Numero,
				tm.Cheque_Fecha,
				tm.Cheque_Importe,
				[tm].Cli_Apellido + ', ' + [tm].[Cli_Nombre],
				tm.Banco_Cogido
FROM gd_esquema.Maestra tm
WHERE tm.Cheque_Numero IS NOT NULL
GO

-- Desde tabla gd_esquema.Maestra a SARASA.Retiro
SET IDENTITY_INSERT SARASA.Retiro ON
INSERT INTO SARASA.Retiro (	Retiro_Id,
							Retiro_Cuenta_Id,
							Retiro_Cheque_Id,
							Retiro_Importe,
							Retiro_Fecha)
SELECT DISTINCT tm.Retiro_Codigo,
				tm.Cuenta_Numero,
				(	SELECT che.Cheque_Id
					FROM SARASA.Cheque che
					WHERE tm.Retiro_Codigo IS NOT NULL AND tm.Cheque_Numero = che.Cheque_Numero),
				tm.Retiro_Importe,
				tm.Retiro_Fecha
FROM gd_esquema.Maestra tm
WHERE tm.Retiro_Codigo IS NOT NULL
SET IDENTITY_INSERT SARASA.Retiro OFF
GO

-- Desde tabla gd_esquema.Maestra a SARASA.Transferencia
INSERT INTO SARASA.Transferencia (	Transferencia_Cuenta_Origen_Id,
									Transferencia_Cuenta_Destino_Id,
									Transferencia_Importe,
									Transferencia_Fecha,
									Transferencia_Costo)
SELECT 	tm.Cuenta_Numero,
		tm.Cuenta_Dest_Numero,
		tm.Trans_Importe,
		tm.Transf_Fecha,
		tm.Trans_Costo_Trans
FROM gd_esquema.Maestra tm
WHERE tm.Transf_Fecha IS NOT NULL
GO

--Desde tabla gd_esquema.Maestra a SARASA.Factura
SET IDENTITY_INSERT SARASA.Factura ON
INSERT INTO SARASA.Factura (	Factura_Numero,
								Factura_Fecha,
								Factura_Cliente_Id)
SELECT 	tm.Factura_Numero,
		tm.Factura_Fecha,
		(	SELECT cli.Cliente_Id
			FROM SARASA.Cliente cli
			WHERE tm.Factura_Numero IS NOT NULL AND tm.Cli_Nro_Doc = cli.Cliente_Doc_Nro)
FROM gd_esquema.Maestra tm
WHERE tm.Factura_Numero IS NOT NULL
SET IDENTITY_INSERT SARASA.Factura OFF
GO

--Desde tabla gd_esquema.Maestra a SARASA.Itemfact
INSERT INTO SARASA.Itemfact (	Itemfact_Cuenta_Numero,
								Itemfact_Descripcion,
								Itemfact_Importe,
								Itemfact_Fecha,
								Itemfact_Factura_Numero)
SELECT 	tm.Cuenta_Numero,
		tm.Item_Factura_Descr,
		tm.Item_Factura_Importe,
		tm.Factura_Fecha,
		tm.Factura_Numero
FROM gd_esquema.Maestra tm
WHERE tm.Item_Factura_Importe IS NOT NULL
GO
