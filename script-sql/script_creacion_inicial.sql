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
	Tc_Num_Tarjeta			varchar(64)		PRIMARY KEY,	--64 bytes ya que el hash de sha256 tiene 256 bits de longitud
	Tc_Cliente_Id			integer			FOREIGN KEY REFERENCES SARASA.Cliente (Cliente_Id) NOT NULL,
	Tc_Fecha_Emision		datetime,
	Tc_Fecha_Vencimiento	datetime,
	Tc_Codigo_Seg			nvarchar(4),
	Tc_Emisor_Desc			nvarchar(255)
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

GO

/****************************************
	Creamos claves primarias y foráneas
*****************************************/

ALTER TABLE SARASA.Cliente ADD FOREIGN KEY (Cliente_Pais_Id) REFERENCES SARASA.Pais (Pais_Id)
ALTER TABLE SARASA.Cliente ADD FOREIGN KEY (Cliente_Tipodoc_Id) REFERENCES SARASA.Tipodoc (Tipodoc_Id)

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
						Tc_Emisor_Desc)
SELECT DISTINCT tm.Tarjeta_Numero,
				(	SELECT cli.Cliente_Id
					FROM SARASA.Cliente cli
					WHERE tm.Tarjeta_Numero IS NOT NULL AND tm.Cli_Nro_Doc = cli.Cliente_Doc_Nro),
				tm.Tarjeta_Fecha_Emision,
				tm.Tarjeta_Fecha_Vencimiento,
				tm.Tarjeta_Codigo_Seg,
				tm.Tarjeta_Emisor_Descripcion
				
FROM gd_esquema.Maestra tm
WHERE tm.Tarjeta_Numero IS NOT NULL
GO
