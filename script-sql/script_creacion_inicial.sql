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
	Tc_Ultimos_Cuatro		nvarchar(4)		NOT NULL,
	Tc_Asociada			bit DEFAULT 1,	--1: Habilitada, 0: No habilitada
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

	CHECK (Tipocta_Vencimiento_Dias >= 0),
	CHECK (Tipocta_Costo_Crea >= 0),
	CHECK (Tipocta_Costo_Mod >= 0),
	CHECK (Tipocta_Costo_Trans >= 0)
)

CREATE TABLE SARASA.Emisor (
	Emisor_Id					integer			identity(1,1) PRIMARY KEY,
	Emisor_Descripcion 			nvarchar(255)	NOT NULL
)

CREATE TABLE SARASA.Cuenta (
	Cuenta_Numero						numeric(18,0)	identity(1,1) PRIMARY KEY,
	Cuenta_Fecha_Creacion				datetime,
	Cuenta_Fecha_Cierre					datetime,
	Cuenta_Estado_Id					integer			FOREIGN KEY REFERENCES SARASA.Estado (Estado_Id) NOT NULL,
	Cuenta_Tipocta_Id					integer			FOREIGN KEY REFERENCES SARASA.Tipocta (Tipocta_Id) NOT NULL,
	Cuenta_Pais_Id						integer			FOREIGN KEY REFERENCES SARASA.Pais (Pais_Id) NOT NULL,
	Cuenta_Moneda_Id					integer			FOREIGN KEY REFERENCES SARASA.Moneda (Moneda_Id) NOT NULL,
	Cuenta_Saldo						numeric(18,2),
	Cuenta_Cliente_Id					integer			FOREIGN KEY REFERENCES SARASA.Cliente (Cliente_Id) NOT NULL,
	Cuenta_Dias_De_Suscripcion			integer,
	Cuenta_Ultima_Modificacion_Tipo		datetime,
	Cuenta_Items_No_Facturados			integer,

	CHECK (Cuenta_Saldo >= 0.0)
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
	Itemfact_Factura_Numero			numeric(18,0),
	Itemfact_Pagado					bit DEFAULT 1,	--1: Pagado, 0: No pagado
)

CREATE TABLE SARASA.Usuario (
	Usuario_Id						integer			identity(1,1) PRIMARY KEY,
	Usuario_Username				nvarchar(20)	NOT NULL UNIQUE,
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
	Funcion_Descripcion				nvarchar(40)	NOT NULL
)

CREATE TABLE SARASA.Rol_x_Funcion (
	Rol_Id 							integer			FOREIGN KEY REFERENCES SARASA.Rol(Rol_Id) NOT NULL,
	Funcion_Id 						integer			FOREIGN KEY REFERENCES SARASA.Funcion(Funcion_Id) NOT NULL
)

CREATE TABLE SARASA.Inhabilitacion (
	Inhab_Id 				numeric(18,0)			identity(1,1) PRIMARY KEY,
	Inhab_Cliente_Id 		integer					FOREIGN KEY REFERENCES SARASA.Cliente(Cliente_Id) NOT NULL,
	Inhab_Fecha				datetime				NOT NULL
)
GO

/****************************************
	Creamos claves primarias y foráneas
*****************************************/

ALTER TABLE SARASA.Cliente ADD FOREIGN KEY (Cliente_Pais_Id) REFERENCES SARASA.Pais (Pais_Id)
ALTER TABLE SARASA.Cliente ADD FOREIGN KEY (Cliente_Tipodoc_Id) REFERENCES SARASA.Tipodoc (Tipodoc_Id)

ALTER TABLE SARASA.Rol_x_Usuario ADD PRIMARY KEY (Rol_Id, Usuario_Id)
ALTER TABLE SARASA.Rol_x_Funcion ADD PRIMARY KEY (Rol_Id, Funcion_Id)
GO

/*******************************
	Creamos funciones y SPs
********************************/

CREATE FUNCTION SARASA.generar_codigo_ingreso(
	@deposito_id numeric(18,0))
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

CREATE FUNCTION SARASA.generar_codigo_egreso(
	@retiro_id numeric(18,0))
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

CREATE FUNCTION SARASA.cuenta_al_dia(
	@cuenta_numero numeric(18,0))
RETURNS bit
AS
BEGIN
	DECLARE @cant_items_impagos integer
	SELECT @cant_items_impagos =	COUNT(item.Itemfact_Pagado)
									FROM SARASA.Itemfact item 
									WHERE item.Itemfact_Pagado = 0 AND item.Itemfact_Cuenta_Numero = @cuenta_numero
	IF @cant_items_impagos = 0
	BEGIN
		RETURN 1
	END
	RETURN 0
END
GO

-- Si la suscripción venció, retorna un número negativo (igual a la cantidad de días que pasaron desde que venció)
CREATE FUNCTION SARASA.calcular_cantidad_dias_restantes(
	@cuenta_numero numeric(18,0))
RETURNS integer
AS
BEGIN
	DECLARE @dias_restantes integer
	
	DECLARE @ultima_modificacion datetime
	SELECT @ultima_modificacion = cue.Cuenta_Ultima_Modificacion_Tipo FROM SARASA.Cuenta cue WHERE cue.Cuenta_Numero = @cuenta_numero

	DECLARE @dias_totales integer
	SELECT @dias_totales = cue.Cuenta_Dias_De_Suscripcion FROM SARASA.Cuenta cue WHERE cue.Cuenta_Numero = @cuenta_numero
	
	DECLARE @fecha_vencimiento datetime
	SET @fecha_vencimiento = DATEADD(day,@dias_totales,@ultima_modificacion)

	SET @dias_restantes = DATEDIFF(day,GETDATE(),@fecha_vencimiento)
	RETURN @dias_restantes
END
GO

CREATE PROCEDURE SARASA.crear_rol(
	@rol_desc				varchar(20),
	@rol_estado				bit,
	@funciones_asociadas	varchar(200)) --Contiene un listado de ids de funciones separadas por comas, por ej: 1,6,20,4,12
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN
	BEGIN TRY
		SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

		IF @starttrancount = 0
			BEGIN TRANSACTION
				DECLARE @rol_insertado int;
				DECLARE @funcion_id varchar(3) = NULL;

				-- Inserta el nuevo Rol en la tabla SARASA.Rol
				INSERT INTO SARASA.Rol (Rol_Descripcion, Rol_Estado)
				VALUES (@rol_desc, @rol_estado);

				-- Obtenemos el valor de id para el rol insertado recien
				SELECT @rol_insertado = IDENT_CURRENT('SARASA.Rol');

				-- Relacionamos ese rol con las funciones que vienen seleccionadas desde la app
				WHILE LEN(@funciones_asociadas) > 0
				BEGIN
					IF PATINDEX('%,%', @funciones_asociadas) > 0
					BEGIN
						SET @funcion_id = SUBSTRING(@funciones_asociadas, 0, PATINDEX('%,%', @funciones_asociadas));
						SET @funciones_asociadas = SUBSTRING(@funciones_asociadas, LEN(@funcion_id + ',') + 1, LEN(@funciones_asociadas));

						INSERT INTO SARASA.Rol_x_Funcion (Rol_Id, Funcion_Id)
						VALUES (@rol_insertado, @funcion_id);
					END
					ELSE
					BEGIN
						SET @funcion_id = @funciones_asociadas;
						SET @funciones_asociadas = NULL;
						INSERT INTO SARASA.Rol_x_Funcion (Rol_Id, Funcion_Id)
						VALUES (@rol_insertado, @funcion_id)
					END
				END

		IF @starttrancount = 0
			COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
			ROLLBACK TRANSACTION
		SELECT @error_message = ERROR_MESSAGE()
		RAISERROR('Error en la transacción al crear el Rol %s: %s',16,1, @rol_desc, @error_message)
	END CATCH
END
GO

CREATE PROCEDURE SARASA.modificar_rol (
	@rol_id					integer,
	@rol_desc				varchar(20),
	@rol_estado				bit,
	@funciones_asociadas	varchar(200)) --Contiene un listado de ids de funciones separadas por comas, por ej: 1,6,20,4,12
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION
			DECLARE @funcion_id varchar(3) = null;

			-- Actualizamos la tabla SARASA.Rol
			UPDATE	SARASA.Rol
			SET Rol_Descripcion = @rol_desc, Rol_Estado = @rol_estado
			WHERE Rol_Id = @rol_id;

			-- Eliminamos de la tabla SARASA.Rol_x_Funcion todas las relaciones con éste Rol
			DELETE FROM SARASA.Rol_x_Funcion WHERE SARASA.Rol_x_Funcion.Rol_Id = @rol_id;

			-- Relacionamos éste Rol con las funciones que vienen en @funciones_asociadas
			WHILE LEN(@funciones_asociadas) > 0
			BEGIN
				IF PATINDEX('%,%', @funciones_asociadas) > 0
				BEGIN
					SET @funcion_id = SUBSTRING(@funciones_asociadas, 0, PATINDEX('%,%', @funciones_asociadas))
					SET @funciones_asociadas = SUBSTRING(@funciones_asociadas, LEN(@funcion_id + ',') + 1, LEN(@funciones_asociadas))

					INSERT INTO SARASA.Rol_x_Funcion (Rol_Id, Funcion_Id)
					VALUES (@rol_id, @funcion_id);
				END
				ELSE
				BEGIN
					SET @funcion_id = @funciones_asociadas
					SET @funciones_asociadas = NULL

					INSERT INTO SARASA.Rol_x_Funcion (Rol_Id, Funcion_Id)
					VALUES (@rol_id, @funcion_id);
				END
			END

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	RAISERROR('Error en la transacción al modificar el Rol %s: %s',16,1, @rol_desc, @error_message)
END CATCH
GO

CREATE PROCEDURE [SARASA].[eliminar_rol] (
	@rol_id					integer)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION

			-- Actualizamos la tabla SARASA.Rol
			UPDATE	SARASA.Rol SET Rol_Estado = 0 WHERE Rol_Id = @rol_id;

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	RAISERROR('Error en la transacción al modificar el Rol con ID %d: %s',16,1, @rol_id, @error_message)
END CATCH
GO

CREATE PROCEDURE [SARASA].[buscar_rol] (
                    @rol varchar(20),
                    @funcion varchar(40))
AS
BEGIN

IF(@rol='' AND @funcion='')
	SELECT Rol_Id, Rol_Estado,Rol_Descripcion, Rol_Etiqueta =CASE WHEN(Rol_Estado = 1) 
								THEN (Rol_Descripcion + ' (habilitado)')
								ELSE (Rol_Descripcion + ' (deshabilitado)')END
	FROM SARASA.Rol;

ELSE IF(@funcion='')
	SELECT Rol_Id, Rol_Estado,Rol_Descripcion, Rol_Etiqueta =CASE WHEN(Rol_Estado = 1)
								THEN (Rol_Descripcion + ' (habilitado)')
								ELSE (Rol_Descripcion + ' (deshabilitado)')END
	FROM SARASA.Rol
	WHERE Rol_Descripcion = @rol;
	
ELSE IF(@rol='')
	SELECT r.Rol_Id, r.Rol_Estado, r.Rol_Descripcion, Rol_Etiqueta =CASE WHEN(r.Rol_Estado = 1) 
								THEN (r.Rol_Descripcion + ' (habilitado)')
								ELSE (r.Rol_Descripcion + ' (deshabilitado)')END
	FROM SARASA.Rol r, SARASA.Rol_x_Funcion rf, SARASA.Funcion f
	WHERE r.Rol_Id = rf.Rol_Id AND rf.Funcion_Id = f.Funcion_Id 
		AND f.Funcion_Descripcion = @funcion
ELSE
	SELECT 'No se pudo realizar la busqueda de roles'

END
GO

CREATE PROCEDURE [SARASA].[buscar_funciones] (
                    @rolId int,
                    @form varchar(10))
AS
BEGIN

IF(@form = 'Buscar')			-- Funciones del rol seleccionado (rolId)

	SELECT f.Funcion_Id, f.Funcion_Descripcion, 0 'Seleccionado'
	FROM SARASA.Rol r, SARASA.Rol_x_Funcion rf, SARASA.Funcion f
	WHERE r.Rol_Id = rf.Rol_Id AND rf.Funcion_Id = f.Funcion_Id
		AND r.Rol_Id = @rolId
		
ELSE IF(@form = 'Crear')		-- Todas las funciones del sistema

	SELECT Funcion_Id, Funcion_Descripcion, 0 'Seleccionado'
	FROM SARASA.Funcion
	
ELSE IF(@form = 'Modificar')	--	Todas las funciones, y cuales estan asociadas aun determinado rol (rolId)

	SELECT f.Funcion_Id, f.Funcion_Descripcion, 
		isnull((SELECT rf.Rol_Id
				FROM SARASA.Rol_x_Funcion rf
				WHERE rf.Funcion_Id = f.Funcion_Id
				AND rf.Rol_Id = @rolId),0) 'Seleccionado'
	FROM SARASA.Funcion f

ELSE IF(@form = 'Login')	--	Recupera la descripcion de las funciones de un determinado rol (rolId)

	SELECT f.Funcion_Descripcion
	FROM SARASA.Rol_x_Funcion rf, SARASA.Funcion f
	WHERE rf.Rol_Id = @rolId AND rf.Funcion_Id = f.Funcion_Id
	
ELSE

	SELECT 'Formulario desconocido'

END
GO

CREATE PROCEDURE [SARASA].[crear_cliente] (
	@Cliente_Nombre nvarchar(255),
	@Cliente_Apellido nvarchar(255),
	@Cliente_Tipodoc_Id int,
	@Cliente_Doc_Nro numeric(18, 0),
	@Cliente_Dom_Calle nvarchar(255),
	@Cliente_Dom_Numero numeric(18, 0),
	@Cliente_Dom_Piso numeric(18, 0),
	@Cliente_Dom_Depto nvarchar(10),
	@Cliente_Fecha_Nacimiento datetime,
	@Cliente_Mail nvarchar(255),
	@Cliente_Pais_Id int,
	@Cliente_Habilitado bit,
	@Usuario_Id int,
	@Usuario_Username nvarchar(20),
	@Usuario_Password nvarchar(64),
--	@Usuario_Fecha_Creacion datetime,
--	@Usuario_Fecha_Modificacion datetime,
	@Usuario_Pregunta_Sec nvarchar(255),
	@Usuario_Respuesta_Sec nvarchar(64),
--	@Usuario_Habilitado bit,
	@Rol_Id int)
AS

	DECLARE @cliente_insertado int;
	DECLARE @usuario_insertado int;

	INSERT INTO SARASA.Cliente (
		Cliente_Pais_Id,
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
	VALUES(
		@Cliente_Pais_Id,
		@Cliente_Nombre,
		@Cliente_Apellido,
		@Cliente_Tipodoc_Id,
		@Cliente_Doc_Nro,
		@Cliente_Dom_Calle,
		@Cliente_Dom_Numero,
		@Cliente_Dom_Piso,
		@Cliente_Dom_Depto,
		@Cliente_Fecha_Nacimiento,
		@Cliente_Mail,
		@Cliente_Habilitado)

	-- Obtenemos el valor de id para el cliente insertado
	SELECT @cliente_insertado = IDENT_CURRENT('SARASA.Cliente');

	IF (@Usuario_Id = 0)
	BEGIN

		-- ALTA DE USUARIO NUEVO PARA EL CLIENTE
		INSERT INTO SARASA.Usuario (
			Usuario_Cliente_Id,
			Usuario_Username,
			Usuario_Password,
			Usuario_Fecha_Creacion,
			Usuario_Fecha_Modificacion,
			Usuario_Pregunta_Sec,
			Usuario_Respuesta_Sec,
			Usuario_Habilitado)
		VALUES(
			@cliente_insertado,
			@Usuario_Username,
			@Usuario_Password,
			SYSDATETIME(),--@Usuario_Fecha_Creacion,
			SYSDATETIME(),--@Usuario_Fecha_Modificacion,
			@Usuario_Pregunta_Sec,
			@Usuario_Respuesta_Sec,
			1)--@Usuario_Habilitado)

		-- Obtenemos el valor de id para el cliente insertado
		SELECT @usuario_insertado = IDENT_CURRENT('SARASA.Usuario');
			
		-- ASOCIA EL NUEVO USUARIO CON UN ROL
		INSERT INTO SARASA.Rol_x_Usuario (Rol_Id,Usuario_Id) VALUES(@Rol_Id, @usuario_insertado)

	END
	ELSE
	BEGIN

		-- ASOCIA EL CLIENTE A UN USUARIO QUE YA EXISTE
		UPDATE SARASA.Usuario SET Usuario_Cliente_Id = @cliente_insertado
		WHERE Usuario_Id = @Usuario_Id
	END
GO

CREATE PROCEDURE SARASA.modificar_cliente(
			@Cliente_Id int,
			@Cliente_Nombre nvarchar(255),
			@Cliente_Apellido nvarchar(255),
			@Cliente_Tipodoc_Id int,
			@Cliente_Doc_Nro numeric(18,0),
			@Cliente_Dom_Calle nvarchar(255),
			@Cliente_Dom_Numero numeric(18,0),
			@Cliente_Dom_Piso numeric(18,0),
			@Cliente_Dom_Depto nvarchar(10),
			@Cliente_Mail nvarchar(255),
			@Cliente_Pais_Id int,
			@Cliente_Fecha_Nacimiento datetime,
			@Cliente_Habilitado bit)
AS
	UPDATE SARASA.Cliente
	   SET	Cliente_Pais_Id = @Cliente_Pais_Id,
			Cliente_Nombre = @Cliente_Nombre,
			Cliente_Apellido = @Cliente_Apellido,
			Cliente_Tipodoc_Id = @Cliente_Tipodoc_Id,
			Cliente_Doc_Nro = @Cliente_Doc_Nro,
			Cliente_Dom_Calle = @Cliente_Dom_Calle,
			Cliente_Dom_Numero = @Cliente_Dom_Numero,
			Cliente_Dom_Piso = @Cliente_Dom_Piso,
			Cliente_Dom_Depto = @Cliente_Dom_Depto,
			Cliente_Fecha_Nacimiento = @Cliente_Fecha_Nacimiento,
			Cliente_Mail = @Cliente_Mail,
			Cliente_Habilitado = @Cliente_Habilitado
	 WHERE Cliente_Id = @Cliente_Id
GO

CREATE PROCEDURE [SARASA].[eliminar_cliente] (
	@cliente_id					integer)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION

			-- Actualizamos la tabla SARASA.Cliente
			UPDATE	SARASA.Cliente SET Cliente_Habilitado = 0 WHERE Cliente_Id = @cliente_id;

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	RAISERROR('Error en la transacción al modificar Cliente con ID %d: %s',16,1, @cliente_id, @error_message)
END CATCH
GO


CREATE PROCEDURE SARASA.realizar_deposito (
	@cliente_id				integer,
	@deposito_fecha			datetime,
	@deposito_importe		numeric(18,2),
	@deposito_moneda_id		integer,
	@deposito_tarjeta_num	varchar(64),
	@deposito_cuenta_num	numeric(18,0)
)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION
			DECLARE @estado_habilitada integer
			DECLARE @estado_cuenta integer
			DECLARE @cuenta_numero_string nvarchar(40)
			DECLARE @tarjeta_vencida nvarchar(2)
			DECLARE @fecha_hoy datetime
			DECLARE @fecha_venc_tarjeta datetime

			--Verificamos que la cuenta esté habilitada. Si no, salimos con RAISERROR
			SELECT @estado_habilitada = est.Estado_Id FROM SARASA.Estado est WHERE est.Estado_Descripcion = 'Habilitada';
			SELECT @estado_cuenta = cue.Cuenta_Estado_Id FROM SARASA.Cuenta cue WHERE cue.Cuenta_Numero = @deposito_cuenta_num;

			IF @estado_cuenta != @estado_habilitada
			BEGIN
				SET @cuenta_numero_string = CAST(@deposito_cuenta_num AS nvarchar(40))
				RAISERROR('La cuenta nro. %s no está habilitada. No se puede procesar el depósito.',16,1,@cuenta_numero_string)
			END

			--Verificamos que la tarjeta no esté vencida. Si está vencida, salimos con RAISERROR
			SET @fecha_hoy = GETDATE()
			SELECT @fecha_venc_tarjeta = tar.Tc_Fecha_Vencimiento FROM SARASA.Tc tar WHERE tar.Tc_Num_Tarjeta = @deposito_tarjeta_num

			SELECT @tarjeta_vencida = CASE WHEN CAST(@fecha_venc_tarjeta AS DATE) < CAST(@fecha_hoy AS DATE) THEN 'SI' ELSE 'NO' END
			
			IF @tarjeta_vencida = 'SI'
			BEGIN
				RAISERROR('La tarjeta de crédito con número %s está vencida.',16,1,@deposito_tarjeta_num)
			END

			INSERT INTO SARASA.Deposito (	Deposito_Fecha,
											Deposito_Importe,
											Deposito_Moneda_Id,
											Deposito_Tc_Num_Tarjeta,
											Deposito_Cuenta_Numero)
			VALUES (@deposito_fecha,
					@deposito_importe,
					@deposito_moneda_id,
					@deposito_tarjeta_num,
					@deposito_cuenta_num)

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	IF ERROR_STATE() = 2	-- Abortamos la transacción porque ha caducado la suscripción de la cuenta. Entonces la inhabilitamos.
	BEGIN
		DECLARE @estado_inhabilitada integer
		SELECT @estado_inhabilitada = est.Estado_Id FROM SARASA.Estado est WHERE est.Estado_Descripcion = 'Inhabilitada';

		UPDATE SARASA.Cuenta
		SET Cuenta_Estado_Id = @estado_inhabilitada
		WHERE Cuenta_Numero = @deposito_cuenta_num
	END
	RAISERROR('Error al realizar el depósito: %s',16,1, @error_message)
END CATCH
GO

CREATE PROCEDURE SARASA.emitir_cheque (
	@cliente_id		integer,
	@banco_codigo	numeric(18,0),
	@importe		numeric(18,2),
	@cheque_id		numeric(18,0)	OUTPUT
)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION

			INSERT INTO SARASA.Cheque (Cheque_Cliente_Nombre, Cheque_Banco_Id, Cheque_Importe, Cheque_Numero, Cheque_Fecha)
			SELECT	(cli.Cliente_Apellido + ', ' + cli.Cliente_Nombre),
					@banco_codigo,
					@importe,
					(SELECT MAX(che.Cheque_Numero) FROM SARASA.Cheque che) + 1,
					GETDATE()
			FROM SARASA.Cliente cli
			WHERE cli.Cliente_Id = @cliente_id

	IF @starttrancount = 0
		COMMIT TRANSACTION

	SELECT @cheque_id = SCOPE_IDENTITY();
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	RAISERROR('Error en la emisión del cheque: %s',16,1, @error_message)
END CATCH
GO

CREATE PROCEDURE SARASA.retirar_efectivo (
	@cliente_id				integer,
	@cliente_documento		numeric(18,0),
	@cuenta_nro				numeric(18,0),
	@moneda_id				integer,
	@importe				numeric(18,2),
	@banco_codigo			numeric(18,0)
)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION
			DECLARE @cheque_id numeric(18,0)
			DECLARE @estado_cuenta integer
			DECLARE @estado_habilitada integer
			DECLARE @saldo_cuenta numeric(18,2)
			DECLARE @cuenta_numero_string nvarchar(40)	--Sólo para manejo de errores
			DECLARE @importe_string nvarchar(20)		--Sólo para manejo de errores

			--Verificamos que la cuenta esté habilitada. Si no, salimos con RAISERROR
			SELECT @estado_habilitada = est.Estado_Id FROM SARASA.Estado est WHERE est.Estado_Descripcion = 'Habilitada';
			SELECT @estado_cuenta = cue.Cuenta_Estado_Id FROM SARASA.Cuenta cue WHERE cue.Cuenta_Numero = @cuenta_nro;

			IF @estado_cuenta != @estado_habilitada
			BEGIN
				SET @cuenta_numero_string = CAST(@cuenta_nro AS nvarchar(40))
				RAISERROR('La cuenta nro. %s no está habilitada. No se puede procesar el retiro.',16,1,@cuenta_numero_string)
			END

			--Verificamos que haya saldo suficiente para hacer el retiro. Si no, salimos con RAISERROR
			SELECT @saldo_cuenta = cue.Cuenta_Saldo FROM SARASA.Cuenta cue WHERE cue.Cuenta_Numero = @cuenta_nro
			
			IF @importe > @saldo_cuenta
			BEGIN
				SET @importe_string = CAST(@importe AS nvarchar(255))
				SET @cuenta_numero_string = CAST(@cuenta_nro AS nvarchar(40))
				RAISERROR('La cuenta nro. %s no posee saldo suficiente para hacer un retiro de $%s',16,1,@cuenta_numero_string,@importe_string)
			END

			--Emitimos el cheque y guardamos el id del mismo
			EXECUTE SARASA.emitir_cheque @cliente_id, @banco_codigo, @importe, @cheque_id OUTPUT;

			--Registramos el retiro
			INSERT INTO SARASA.Retiro (Retiro_Cuenta_Id, Retiro_Cheque_Id, Retiro_Importe, Retiro_Fecha)
			VALUES (@cuenta_nro, @cheque_id, @importe, GETDATE())

			--Luego SARASA.tr_retiro_aff_ins_generar_codigo inserta el código de egreso en SARASA.Retiro

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	IF ERROR_STATE() = 2	-- Abortamos la transacción porque ha caducado la suscripción de la cuenta. Entonces la inhabilitamos.
	BEGIN
		DECLARE @estado_inhabilitada integer
		SELECT @estado_inhabilitada = est.Estado_Id FROM SARASA.Estado est WHERE est.Estado_Descripcion = 'Inhabilitada';

		UPDATE SARASA.Cuenta
		SET Cuenta_Estado_Id = @estado_inhabilitada
		WHERE Cuenta_Numero = @cuenta_nro
	END
	RAISERROR('Error en la transacción de retiro de efectivo: %s',16,1, @error_message)
END CATCH
GO

CREATE PROCEDURE SARASA.crear_item_factura (
	@cuenta_num		numeric(18,0),
	@descripcion	nvarchar(255),
	@importe		numeric(18,2),
	@fecha			datetime,
	@factura_nro	numeric(18,0),
	@pagado			bit
)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION

			INSERT INTO SARASA.Itemfact (	Itemfact_Cuenta_Numero,
											Itemfact_Descripcion,
											Itemfact_Importe,
											Itemfact_Fecha,
											Itemfact_Factura_Numero,
											Itemfact_Pagado)
			VALUES (	@cuenta_num,
						@descripcion,
						@importe,
						@fecha,
						@factura_nro,
						@pagado)

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	RAISERROR('Error al generar ítem para factura $s: %s',16,1,@descripcion,@error_message)
END CATCH
GO

CREATE PROCEDURE SARASA.crear_cuenta (
	@cliente_id			integer,
	@fecha_apertura		datetime,
	@tipo_cuenta_id		integer,
	@moneda_id			integer,
	@pais_id			integer,
	@Cuenta_Numero		numeric(18,0) OUTPUT
)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION
			DECLARE @estado_cuenta integer
			DECLARE @tipo_cuenta_gratuita integer

			SELECT @estado_cuenta = est.Estado_Id FROM SARASA.Estado est WHERE est.Estado_Descripcion = 'Pendiente de activación'
			SELECT @tipo_cuenta_gratuita = tipo.Tipocta_Id FROM SARASA.Tipocta tipo WHERE tipo.Tipocta_Descripcion = 'Gratuita'

			-- Si la cuenta es de tipo 'gratuita' se abre directamente como habilitada
			IF @tipo_cuenta_id = @tipo_cuenta_gratuita
			BEGIN
				SELECT @estado_cuenta = est.Estado_Id FROM SARASA.Estado est WHERE est.Estado_Descripcion = 'Habilitada'
			END

			-- Creamos la cuenta
			INSERT INTO SARASA.Cuenta (	Cuenta_Fecha_Creacion,
										Cuenta_Estado_Id, 
										Cuenta_Tipocta_Id,
										Cuenta_Pais_Id,
										Cuenta_Moneda_Id,
										Cuenta_Saldo,
										Cuenta_Cliente_Id,
										Cuenta_Dias_De_Suscripcion,
										Cuenta_Ultima_Modificacion_Tipo,
										Cuenta_Items_No_Facturados)
			VALUES (	@fecha_apertura,
						@estado_cuenta,
						@tipo_cuenta_id,
						@pais_id,
						@moneda_id,
						0.0,
						@cliente_id,
						(SELECT tipo.Tipocta_Vencimiento_Dias
							FROM SARASA.Tipocta tipo
							WHERE tipo.Tipocta_Id = @tipo_cuenta_id),
						GETDATE(),
						0)	-- Es el valor inicial, luego el trigger que genera el item de la factura lo incrementará
						
			-- Obtenemos el valor de id para el cliente insertado
			SELECT @Cuenta_Numero = IDENT_CURRENT('SARASA.Cuenta');

			-- Luego el trigger SARASA.tr_cuenta_aff_ins_crear_item_factura se encargará de generar el ítem de factura correspondiente al costo de apertura

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	RAISERROR('Error en la creación de la cuenta: %s',16,1, @error_message)
END CATCH
GO

CREATE PROCEDURE SARASA.modificar_cuenta (
	@cliente_id				integer,
	@cuenta_numero			numeric(18,0),
	@tipo_cuenta_deseado	integer,
	@estado_deseado 		integer
)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION

			DECLARE @cuenta_string nvarchar(40)
			DECLARE @estado_habilitada integer
			DECLARE @estado_cerrada integer
			DECLARE @estado_inhabilitada integer
			DECLARE @estado_actual integer
			DECLARE @fecha_hoy datetime
			DECLARE @importe numeric(18,2)
			DECLARE @tipo_cuenta_actual integer

			SET @fecha_hoy = GETDATE()
			SELECT @tipo_cuenta_actual = cue.Cuenta_Tipocta_Id FROM SARASA.Cuenta cue WHERE cue.Cuenta_Numero = @cuenta_numero
			SELECT @importe = tipo.Tipocta_Costo_Mod FROM SARASA.Tipocta tipo WHERE tipo.Tipocta_Id = @tipo_cuenta_deseado
			SELECT @estado_habilitada = est.Estado_Id FROM SARASA.Estado est WHERE est.Estado_Descripcion = 'Habilitada'
			SELECT @estado_cerrada = est.Estado_Id FROM SARASA.Estado est WHERE est.Estado_Descripcion = 'Cerrada'
			SELECT @estado_inhabilitada = est.Estado_Id FROM SARASA.Estado est WHERE est.Estado_Descripcion = 'Inhabilitada'
			SELECT @estado_actual = cue.Cuenta_Estado_Id FROM SARASA.Cuenta cue WHERE cue.Cuenta_Numero = @cuenta_numero
			SET @cuenta_string = CAST(@cuenta_numero AS nvarchar(40)) --solo en caso de error

			-- Si la cuenta está cerrada, salimos con RAISERROR
			IF @estado_actual = @estado_cerrada
			BEGIN
				RAISERROR('No se puede modificar la cuenta %s porque está cerrada.',16,1,@cuenta_string)
			END

			-- Si se quiere inhabilitar, se inhabilita.
			IF @estado_deseado = @estado_inhabilitada
			BEGIN
				UPDATE SARASA.Cuenta
				SET Cuenta_Estado_Id = @estado_inhabilitada
				WHERE Cuenta_Numero = @cuenta_numero
			END

			-- Si el tipo de cuenta es distinto al actual, generamos un item de factura por el cambio de tipo.
			IF @tipo_cuenta_actual != @tipo_cuenta_deseado
			BEGIN
				EXEC SARASA.crear_item_factura @cuenta_numero, 'Cambio de tipo de cuenta.',@importe,@fecha_hoy,NULL,0
				
				-- Luego modificamos el tipo y actualizamos los campos auxiliares
				DECLARE @cant_dias_restantes integer
				SET @cant_dias_restantes = SARASA.calcular_cantidad_dias_restantes(@cuenta_numero)

				DECLARE @cant_dias_nueva_suscripcion integer
				SELECT @cant_dias_nueva_suscripcion = tipo.Tipocta_Vencimiento_Dias FROM SARASA.Tipocta tipo WHERE tipo.Tipocta_Id = @tipo_cuenta_deseado

				UPDATE SARASA.Cuenta
				SET Cuenta_Tipocta_Id = @tipo_cuenta_deseado,
					Cuenta_Ultima_Modificacion_Tipo = GETDATE(),
					Cuenta_Dias_De_Suscripcion = @cant_dias_nueva_suscripcion + @cant_dias_restantes
				WHERE Cuenta_Numero = @cuenta_numero
			END

			-- Si se quiere cerrar o habilitar la cuenta, validamos que no haya ningún item factura impago para ese nro de cuenta
			IF @estado_deseado = @estado_cerrada OR @estado_deseado = @estado_habilitada
			BEGIN
				IF @estado_actual != @estado_deseado	--Solo tiene sentido cuando el estado es distinto al actual
				BEGIN
					IF SARASA.cuenta_al_dia(@cuenta_numero) = 0
					BEGIN
						RAISERROR('No se puede modificar el estado de la cuenta %s porque aún tiene costos sin facturar.',16,1,@cuenta_string)
					END

					-- Si no, modificamos el estado de la cuenta y listo
					UPDATE SARASA.Cuenta
					SET Cuenta_Estado_Id = @estado_deseado
					WHERE Cuenta_Numero = @cuenta_numero
				END
			END			

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	RAISERROR('Error en la modificación de la cuenta: %s',16,1, @error_message)
END CATCH
GO

CREATE PROCEDURE SARASA.acreditar_en_cuenta (
	@cuenta_numero	numeric(18,0),
	@importe		numeric(18,2)
)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION
			DECLARE @cuenta_numero_string nvarchar(40)

			DECLARE @saldo_actual numeric(18,2)
			SELECT @saldo_actual = cue.Cuenta_Saldo FROM SARASA.Cuenta cue WHERE cue.Cuenta_Numero = @cuenta_numero

			UPDATE SARASA.Cuenta
			SET Cuenta_Saldo = @saldo_actual + @importe
			WHERE Cuenta_Numero = @cuenta_numero

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	SET @cuenta_numero_string = CAST(@cuenta_numero AS nvarchar(40))
	RAISERROR('Error al realizar la acreditación de fondos en la cuenta destino %s: %s',16,1,@cuenta_numero_string,@error_message)
END CATCH
GO

CREATE PROCEDURE SARASA.debitar_de_cuenta (
	@cuenta_numero			numeric(18,0),
	@importe				numeric(18,2),
	@se_cobra_comision 		bit
)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION
			DECLARE @cuenta_numero_string nvarchar(40) --solo para errores

			DECLARE @saldo_actual numeric(18,2)
			SELECT @saldo_actual = cue.Cuenta_Saldo FROM SARASA.Cuenta cue WHERE cue.Cuenta_Numero = @cuenta_numero
			
			DECLARE @fecha_hoy datetime
			SET @fecha_hoy = GETDATE()
			
			DECLARE	@comision numeric(18,2)
			SELECT	@comision = tcta.Tipocta_Costo_Trans
						FROM SARASA.Cuenta cue
						INNER JOIN SARASA.Tipocta tcta ON cue.Cuenta_Tipocta_Id = tcta.Tipocta_Id
						WHERE cue.Cuenta_Numero = @cuenta_numero


		-- Debitamos el dinero.
		UPDATE SARASA.Cuenta
		SET Cuenta_Saldo = @saldo_actual - @importe
		WHERE Cuenta_Numero = @cuenta_numero

		-- Generamos el item factura correspondiente
		EXEC SARASA.crear_item_factura @cuenta_numero,'Comisión por transferencia',@comision,@fecha_hoy,NULL,0

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	SET @cuenta_numero_string = CAST(@cuenta_numero as nvarchar(40))
	RAISERROR('Error al realizar el débito de fondos en la cuenta origen %s: %s',16,1,@cuenta_numero_string,@error_message)
END CATCH
GO

CREATE PROCEDURE SARASA.realizar_transferencia (
	@cuenta_origen numeric(18,0),
	@cuenta_destino numeric(18,0),
	@importe numeric(18,2)
)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION
			DECLARE @cuenta_origen_string nvarchar(40) --solo para errores
			DECLARE @cuenta_destino_string nvarchar(40) --solo para errores
			
			DECLARE @cliente_cuenta_origen numeric(18,0)
			SELECT @cliente_cuenta_origen = cue.Cuenta_Cliente_Id FROM SARASA.Cuenta cue WHERE cue.Cuenta_Numero = @cuenta_origen
			
			DECLARE @cliente_cuenta_destino numeric(18,0)
			SELECT @cliente_cuenta_destino = cue.Cuenta_Cliente_Id FROM SARASA.Cuenta cue WHERE cue.Cuenta_Numero = @cuenta_destino

			DECLARE @maximo_a_debitar numeric(18,2)
			SELECT @maximo_a_debitar = cue.Cuenta_Saldo FROM SARASA.Cuenta cue WHERE cue.Cuenta_Numero = @cuenta_origen

			-- Hacemos las validaciones necesarias.
			IF @importe <= 0
			BEGIN
				RAISERROR('El importe a transferir debe ser mayor a cero.',16,1)
			END

			IF @importe > @maximo_a_debitar
			BEGIN
				RAISERROR('El monto a debitar supera el saldo de la cuenta de origen.',16,1)
			END

			DECLARE @se_cobra_comision bit = 1
			IF @cliente_cuenta_origen = @cliente_cuenta_destino
			BEGIN
				SET @se_cobra_comision = 0
			END

			-- Hacemos el débito de la cuenta origen y generamos el item factura por la comisión
			EXEC SARASA.debitar_de_cuenta @cuenta_origen, @importe, @se_cobra_comision

			-- Y luego el depósito en la cuenta destino
			EXEC SARASA.acreditar_en_cuenta @cuenta_destino, @importe
		
	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	SET @cuenta_origen_string = CAST(@cuenta_origen as nvarchar(40))
	SET @cuenta_destino_string = CAST(@cuenta_destino as nvarchar(40))
	RAISERROR('Error al realizar la transferencia entre las cuentas %s y %s: %s',16,1,@cuenta_destino_string,@cuenta_origen_string,@error_message)
END CATCH
GO

USE GD1C2015
GO

CREATE PROCEDURE SARASA.consultar_saldos (
	@cuenta_numero numeric(18,0)
)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION

			SELECT cue.Cuenta_Saldo AS Saldo
			FROM SARASA.Cuenta cue
			WHERE cue.Cuenta_Numero = @cuenta_numero
			
			SELECT TOP(5) dep.Deposito_Fecha AS Fecha, dep.Deposito_Codigo_Ingreso AS Codigo_Ingreso, dep.Deposito_Importe AS Importe
			FROM SARASA.Deposito dep
			WHERE dep.Deposito_Cuenta_Numero = @cuenta_numero
			ORDER BY dep.Deposito_Fecha DESC

			SELECT TOP(5) ret.Retiro_Fecha AS Fecha, ret.Retiro_Codigo_Egreso AS Codigo_Egreso, ret.Retiro_Importe AS Importe
			FROM SARASA.Retiro ret
			WHERE ret.Retiro_Cuenta_Id = @cuenta_numero
			ORDER BY ret.Retiro_Fecha DESC

			SELECT TOP(10) transf.Transferencia_Fecha AS Fecha, transf.Transferencia_Cuenta_Origen_Id AS Cuenta_Origen, transf.Transferencia_Cuenta_Destino_Id AS Cuenta_Destino, transf.Transferencia_Importe AS Importe, transf.Transferencia_Costo AS Comisión
			FROM SARASA.Transferencia transf
			WHERE transf.Transferencia_Cuenta_Destino_Id = @cuenta_numero
			OR transf.Transferencia_Cuenta_Origen_Id = @cuenta_numero
			ORDER BY transf.Transferencia_Fecha DESC

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	RAISERROR('Error en la consulta de saldo: %s',16,1, @error_message)
END CATCH
GO

CREATE PROCEDURE SARASA.renovar_suscripcion (
	@cuenta_numero numeric(18,0)
)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION
			DECLARE @tipo_cuenta_actual integer
			SELECT @tipo_cuenta_actual = cue.Cuenta_Tipocta_Id FROM SARASA.Cuenta cue WHERE cue.Cuenta_Numero = @cuenta_numero

			DECLARE @importe numeric(18,2)
			SELECT @importe = tipo.Tipocta_Costo_Mod FROM SARASA.Tipocta tipo WHERE tipo.Tipocta_Id = @tipo_cuenta_actual

			DECLARE @fecha_hoy datetime
			SET @fecha_hoy = GETDATE()

			-- Generamos el item factura correspondiente a la misma suscripción que tiene la cuenta.
			EXEC SARASA.crear_item_factura @cuenta_numero, 'Renovación de cuenta', @importe, @fecha_hoy, NULL, 0

			-- Tenemos que incrementar y modificar desde acá los valores para las columnas Cuenta_Ultima_Modificacion_Tipo y Cuenta_Dias_De_Suscripcion
			DECLARE @cant_dias_restantes integer
			SET @cant_dias_restantes = SARASA.calcular_cantidad_dias_restantes(@cuenta_numero)

			DECLARE @cant_dias_nueva_suscripcion integer
			SELECT @cant_dias_nueva_suscripcion = tipo.Tipocta_Vencimiento_Dias FROM SARASA.Tipocta tipo WHERE tipo.Tipocta_Id = @tipo_cuenta_actual
			
			-- Si la cantidad de días es negativa, la cuenta está inhabilitada por vencimiento, resetemos la fecha modificación tipo y la cantidad de días.
			IF @cant_dias_restantes < 0
			BEGIN
				UPDATE SARASA.Cuenta
				SET Cuenta_Ultima_Modificacion_Tipo = GETDATE(),
					Cuenta_Dias_De_Suscripcion = @cant_dias_nueva_suscripcion	--notar que no le sumamos los días restantes
				WHERE Cuenta_Numero = @cuenta_numero
			END
			ELSE
			BEGIN
				UPDATE SARASA.Cuenta
				SET Cuenta_Ultima_Modificacion_Tipo = GETDATE(),
					Cuenta_Dias_De_Suscripcion = @cant_dias_nueva_suscripcion + @cant_dias_restantes
				WHERE Cuenta_Numero = @cuenta_numero
			END

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	DECLARE @cuenta_numero_string nvarchar(40)
	SET @cuenta_numero_string = CAST(@cuenta_numero AS nvarchar(40))
	RAISERROR('Error al renovar la suscripción de la cuenta %s: %s',16,1,@cuenta_numero_string,@error_message)
END CATCH
GO

-- Consulta estadística nro. 1
CREATE PROCEDURE SARASA.inhabilitaciones_por_cliente (
	@fecha_desde datetime,
	@fecha_hasta datetime
)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION

			SELECT TOP 5 Tabla_Total.inhab_cliente_Id AS Cliente_Id, (cli.Cliente_Nombre + ' ' + cli.Cliente_Apellido) AS Nombre_y_Apellido, Tabla_Total.Cantidad_Inhabilitaciones AS Cant_inhabilitaciones
			FROM (	
					SELECT inhab.inhab_cliente_Id, COUNT(inhab.inhab_Id) AS Cantidad_Inhabilitaciones
					FROM SARASA.Inhabilitacion inhab
					WHERE inhab.inhab_fecha BETWEEN @fecha_desde AND @fecha_hasta
					GROUP BY inhab.inhab_cliente_Id) AS Tabla_Total
			INNER JOIN SARASA.Cliente cli ON cli.Cliente_Id = Tabla_Total.inhab_cliente_Id
			ORDER BY Cantidad_Inhabilitaciones DESC

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	RAISERROR('Error en la consulta estadística nro 1 (Clientes que alguna de sus cuentas fueron inhabilitadas por no pagar los costos de transacción): %s',16,1, @error_message)
END CATCH
GO

--Consulta estadística nro. 2
CREATE PROCEDURE SARASA.clientes_mas_comisiones_facturadas (
	@fecha_desde datetime,
	@fecha_hasta datetime
)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION

			SELECT TOP 5 Tabla_Total.Cuenta_Cliente_Id AS Cliente_Id, (cli.Cliente_Nombre + ' ' + cli.Cliente_Apellido) AS Nombre_y_Apellido, Tabla_Total.Total_Comisiones_Por_Cliente AS Comisiones_Por_Cliente
			FROM (	
					SELECT cue.Cuenta_Cliente_Id, SUM(Comisiones_Por_Cuenta.Cantidad_Comsiones_Facturadas) AS Total_Comisiones_Por_Cliente
					FROM (	
							SELECT item.Itemfact_Cuenta_Numero, COUNT(CASE WHEN Itemfact_Descripcion = 'Comisión por transferencia.' THEN 1 ELSE 0 END) AS Cantidad_Comsiones_Facturadas
							FROM SARASA.Itemfact item
							WHERE item.Itemfact_Fecha BETWEEN @fecha_desde AND @fecha_hasta
							GROUP BY item.Itemfact_Cuenta_Numero) AS Comisiones_Por_Cuenta
					INNER JOIN SARASA.Cuenta cue ON cue.Cuenta_Numero = Comisiones_Por_Cuenta.Itemfact_Cuenta_Numero
					GROUP BY cue.Cuenta_Cliente_Id) AS Tabla_Total
			INNER JOIN SARASA.Cliente cli ON cli.Cliente_Id = Tabla_Total.Cuenta_Cliente_Id
			ORDER BY Comisiones_Por_Cliente DESC

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	RAISERROR('Error en la consulta estadística nro 2 (Clientes con mayor cantidad de comisiones facturadas): %s',16,1, @error_message)
END CATCH
GO

-- Consulta estadística nro. 3
CREATE PROCEDURE SARASA.clientes_transferencias_entre_si (
	@fecha_desde datetime,
	@fecha_hasta datetime
)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION

		SELECT TOP 5 Tabla_Total.Cliente_Origen AS Cliente_Id, (cli.Cliente_Nombre + ' ' + cli.Cliente_Apellido) AS Nombre_y_Apellido, Tabla_Total.Cantidad_Transferencias AS Cantidad_Transferencias_Entre_Cuentas_Propias
		FROM
			(SELECT cue_origen.Cuenta_Cliente_Id AS Cliente_Origen, COUNT(transf.Transferencia_Id) AS Cantidad_Transferencias
			FROM SARASA.Transferencia transf
			INNER JOIN SARASA.Cuenta cue_origen ON cue_origen.Cuenta_Numero = transf.Transferencia_Cuenta_Origen_Id
			INNER JOIN SARASA.Cuenta cue_destino ON cue_destino.Cuenta_Numero = transf.Transferencia_Cuenta_Destino_Id
			WHERE cue_origen.Cuenta_Cliente_Id = cue_destino.Cuenta_Cliente_Id
			AND transf.Transferencia_Fecha BETWEEN @fecha_desde AND @fecha_hasta
			GROUP BY cue_origen.Cuenta_Cliente_Id) AS Tabla_Total
		INNER JOIN SARASA.Cliente cli ON cli.Cliente_Id = Tabla_Total.Cliente_Origen
		ORDER BY Tabla_Total.Cantidad_Transferencias DESC

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	RAISERROR('Error en la consulta estadística nro 3 (Clientes con mayor cantidad de transferencias entre sí): %s',16,1, @error_message)
END CATCH
GO

-- Consulta estadística nro. 4
CREATE PROCEDURE SARASA.movimientos_por_paises (
	@fecha_desde datetime,
	@fecha_hasta datetime
)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION

		SELECT TOP 5 pais.Pais_Nombre, Tabla_Total.Total_Movimientos
		FROM (	SELECT Total_Depositos.Cuenta_Pais_Id, SUM(COALESCE(Total_Depositos.Cantidad_Depositos,0) + COALESCE(Total_Retiros.Cantidad_Retiros,0)) AS Total_Movimientos
				FROM (	SELECT cue.Cuenta_Pais_Id, COUNT(Deposito_Id) AS Cantidad_Depositos
						FROM SARASA.Deposito dep
						INNER JOIN SARASA.Cuenta cue ON cue.Cuenta_Numero = dep.Deposito_Cuenta_Numero
						WHERE dep.Deposito_Fecha BETWEEN @fecha_desde AND @fecha_hasta
						GROUP BY cue.Cuenta_Pais_Id) AS Total_Depositos
				FULL OUTER JOIN 
					(	SELECT cue.Cuenta_Pais_Id, COUNT(Retiro_Id) AS Cantidad_Retiros
						FROM SARASA.Retiro ret
						INNER JOIN SARASA.Cuenta cue ON cue.Cuenta_Numero = ret.Retiro_Cuenta_Id
						WHERE ret.Retiro_Fecha BETWEEN @fecha_desde AND @fecha_hasta
						GROUP BY cue.Cuenta_Pais_Id) AS Total_Retiros
				ON Total_Depositos.Cuenta_Pais_Id = Total_Retiros.Cuenta_Pais_Id
				GROUP BY Total_Depositos.Cuenta_Pais_Id) AS Tabla_Total
		INNER JOIN SARASA.Pais pais ON pais.Pais_Id = Tabla_Total.Cuenta_Pais_Id
		GROUP BY pais.Pais_Nombre, Tabla_Total.Total_Movimientos
		ORDER BY Tabla_Total.Total_Movimientos DESC

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	RAISERROR('Error en la consulta estadística nro 4 (Países con mayor cantidad de movimientos): %s',16,1, @error_message)
END CATCH
GO

-- Consulta estadística nro. 5
CREATE PROCEDURE SARASA.total_facturado_por_tipo_cuenta (
	@fecha_desde datetime,
	@fecha_hasta datetime
)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION

		SELECT TOP 5 tipo.Tipocta_Descripcion, SUM(COALESCE(Resultado_Parcial.Total_Por_Cuenta,0)) AS Total_Por_Tipo
		FROM
			(	SELECT cue.Cuenta_Numero, cue.Cuenta_Tipocta_Id, SUM(Itemfact_Importe) AS Total_Por_Cuenta
				FROM SARASA.Itemfact item
				INNER JOIN SARASA.Cuenta cue ON item.Itemfact_Cuenta_Numero = cue.Cuenta_Numero
				WHERE item.Itemfact_Fecha BETWEEN @fecha_desde AND @fecha_hasta
				GROUP BY cue.Cuenta_Numero, cue.Cuenta_Tipocta_Id	) AS Resultado_Parcial
		FULL OUTER JOIN SARASA.Tipocta tipo ON tipo.Tipocta_Id = Resultado_Parcial.Cuenta_Tipocta_Id
		GROUP BY tipo.Tipocta_Descripcion
		ORDER BY Total_Por_Tipo DESC

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	RAISERROR('Error en la consulta estadística nro 5 (Total facturado para los distintos tipos de cuenta): %s',16,1, @error_message)
END CATCH
GO

-- Procedimiento para comprobar si un cliente tiene items sin facturar
CREATE PROCEDURE SARASA.comprobar_items_impagos (
	@cliente_id	integer, @comprobante integer OUTPUT
)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)


BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION
			
			IF EXISTS ( SELECT DISTINCT i.Itemfact_Id
						FROM GD1C2015.SARASA.Itemfact i, GD1C2015.SARASA.Cuenta cu, GD1C2015.SARASA.Cliente c
						WHERE i.Itemfact_Cuenta_Numero=cu.Cuenta_Numero AND
						cu.Cuenta_Cliente_Id=@cliente_id AND
						i.Itemfact_Pagado='0')
				SET @comprobante='1'
			ELSE
				SET @comprobante='0'
		

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	RAISERROR('Error en la transacción: %s',16,1, @error_message)
END CATCH
GO

-- Procedimiento que genera una nueva factura segun cliente
CREATE PROCEDURE SARASA.facturar_por_cliente (
	@cliente_id	integer, @factura_id numeric(18,0) OUTPUT
)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)
DECLARE @fecha_actual datetime
SET @fecha_actual = GETDATE()

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
			BEGIN TRANSACTION

		
				INSERT INTO SARASA.Factura (Factura_Fecha, Factura_Cliente_Id)
				VALUES (@fecha_actual, @cliente_id)
			
				SET @factura_id = ( SELECT TOP 1 f.Factura_Numero
								FROM GD1C2015.SARASA.Factura f
								WHERE f.Factura_Cliente_Id=@cliente_id
								ORDER BY f.Factura_Fecha ASC )
		

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	RAISERROR('Error en la transacción: %s',16,1, @error_message)
END CATCH
GO


-- Procedimiento para facturar los items de una factura
CREATE PROCEDURE SARASA.facturar_items (
	@factura_id numeric(18,0), @cliente_id integer
)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION

		UPDATE GD1C2015.SARASA.Itemfact
		SET Itemfact_Factura_Numero=@factura_id,  Itemfact_Pagado='1'
		WHERE Itemfact_Pagado='0' AND
				Itemfact_Id IN (SELECT DISTINCT i.Itemfact_Id
								FROM GD1C2015.SARASA.Itemfact i, GD1C2015.SARASA.Cuenta cu, GD1C2015.SARASA.Cliente c
								WHERE i.Itemfact_Cuenta_Numero=cu.Cuenta_Numero AND
										cu.Cuenta_Cliente_Id=@cliente_id)

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	RAISERROR('Error en la transacción: %s',16,1, @error_message)
END CATCH
GO

-- Procedimiento para asociar una tarjeta al cliente logueado
CREATE PROCEDURE SARASA.Asociar_Tarjeta (
	@cliente_id		integer,
	@tc_num		varchar(64),
	@tc_ultimoscuatro	nvarchar(4),
	@tc_emision		nvarchar(255),
	@tc_vencimiento		nvarchar(255),
	@tc_codseg	nvarchar(64),
	@tc_emisor	nvarchar(255)
)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION

				INSERT INTO SARASA.Tc (Tc_Cliente_Id,Tc_Codigo_Seg,Tc_Emisor_Desc,Tc_Fecha_Emision,Tc_Fecha_Vencimiento,Tc_Num_Tarjeta,Tc_Ultimos_Cuatro,Tc_Asociada)
				VALUES (@cliente_id, @tc_codseg, @tc_emisor, @tc_emision, @tc_vencimiento, @tc_num, @tc_ultimoscuatro,'1')

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	RAISERROR('Error en la transacción: %s',16,1, @error_message)
END CATCH
GO

-- Procedimiento para desasociar una tarjeta de un cliente
CREATE PROCEDURE SARASA.Desasociar_Tarjeta (
	@cliente_id		integer,
	@tc_num		varchar(64)
)
AS

SET XACT_ABORT ON	-- Si alguna instruccion genera un error en runtime, revierte la transacción.
SET NOCOUNT ON		-- No actualiza el número de filas afectadas. Mejora performance y reduce carga de red.

DECLARE @starttrancount int
DECLARE @error_message nvarchar(4000)

BEGIN TRY
	SELECT @starttrancount = @@TRANCOUNT	--@@TRANCOUNT lleva la cuenta de las transacciones abiertas.

	IF @starttrancount = 0
		BEGIN TRANSACTION

		UPDATE GD1C2015.SARASA.Tc
		SET Tc_Asociada='0'
		WHERE Tc_Cliente_Id= @cliente_id AND
				Tc_Num_Tarjeta = @tc_num

	IF @starttrancount = 0
		COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF XACT_STATE() <> 0 AND @starttrancount = 0	--XACT_STATE() es cero sólo cuando no hay ninguna transacción activa para este usuario.
		ROLLBACK TRANSACTION
	SELECT @error_message = ERROR_MESSAGE()
	RAISERROR('Error en la transacción: %s',16,1, @error_message)
END CATCH
GO



/***********************
	Creamos procedures para la carga de los combos en la app
************************/

CREATE PROCEDURE [SARASA].[cbx_rol]
AS
	SELECT Rol_Id 'Valor', Rol_Descripcion 'Etiqueta' FROM SARASA.Rol
GO


CREATE PROCEDURE [SARASA].[cbx_tipodoc]
AS
	SELECT Tipodoc_Id 'Valor', Tipodoc_Descripcion 'Etiqueta' FROM SARASA.Tipodoc
GO


CREATE PROCEDURE [SARASA].[cbx_pais]
AS	
	SELECT Pais_Id 'Valor', Pais_Nombre 'Etiqueta'
	FROM SARASA.Pais
	ORDER BY Pais_Nombre
GO


CREATE PROCEDURE [SARASA].[cbx_moneda]
AS	
	SELECT Moneda_Id 'Valor', Moneda_Descripcion 'Etiqueta' 
	FROM SARASA.Moneda
GO


CREATE PROCEDURE [SARASA].[cbx_tipocta]
AS	
	SELECT Tipocta_Id 'Valor', Tipocta_Descripcion 'Etiqueta' 
	FROM SARASA.Tipocta
GO

CREATE PROCEDURE [SARASA].[cbx_estado]
AS	
	SELECT Estado_Id 'Valor', Estado_Descripcion 'Etiqueta' 
	FROM SARASA.Estado
GO


CREATE PROCEDURE [SARASA].[cbx_emisor]
AS
	SELECT Emisor_Id 'Valor', Emisor_Descripcion 'Etiqueta' FROM SARASA.Emisor
GO


CREATE PROCEDURE [SARASA].[cbx_tc](@Cliente_Id integer)
AS

IF(@Cliente_Id = 0)
BEGIN
	SELECT Tc_Num_Tarjeta 'Valor', 'XXXX XXXX XXXX ' + Tc_Ultimos_Cuatro + ' (' + Tc_Emisor_Desc+ ')' 'Etiqueta'
	FROM SARASA.Tc
END
ELSE
BEGIN
	SELECT Tc_Num_Tarjeta 'Valor', 'XXXX XXXX XXXX ' + Tc_Ultimos_Cuatro + ' (' + Tc_Emisor_Desc+ ')' 'Etiqueta'
	FROM SARASA.Tc
	WHERE Tc_Cliente_Id = @Cliente_Id
END
GO


CREATE PROCEDURE [SARASA].[cbx_cuenta](
		@Cliente_Id integer,
		@Estado_Desc nvarchar(255))
AS

IF(@Cliente_Id =0 AND @Estado_Desc='')
BEGIN

	SELECT Cuenta_Numero 'Valor', CAST(Cuenta_Numero AS VARCHAR(18)) + ' (' + Tipocta_Descripcion + ')' 'Etiqueta'
	FROM SARASA.Cuenta, SARASA.Tipocta
	WHERE Cuenta_Tipocta_Id = Tipocta_Id 

END
ELSE IF(@Cliente_Id != 0 AND @Estado_Desc != '')
BEGIN

	SELECT Cuenta_Numero 'Valor', CAST(Cuenta_Numero AS VARCHAR(18)) + ' (' + Tipocta_Descripcion + ')' 'Etiqueta'
	FROM SARASA.Cuenta, SARASA.Tipocta, SARASA.Estado
	WHERE Cuenta_Tipocta_Id = Tipocta_Id 
		AND Cuenta_Estado_Id = Estado_Id
		AND Estado_Descripcion = @Estado_Desc
		AND Cuenta_Cliente_Id = @Cliente_Id

END
ELSE IF(@Cliente_Id != 0)
BEGIN

	SELECT Cuenta_Numero 'Valor', CAST(Cuenta_Numero AS VARCHAR(18)) + ' (' + Tipocta_Descripcion + ')' 'Etiqueta'
	FROM SARASA.Cuenta, SARASA.Tipocta
	WHERE Cuenta_Tipocta_Id = Tipocta_Id 
		AND Cuenta_Cliente_Id = @Cliente_Id

END
ELSE IF(@Estado_Desc != '')
BEGIN
	SELECT Cuenta_Numero 'Valor', CAST(Cuenta_Numero AS VARCHAR(18)) + ' (' + Tipocta_Descripcion + ')' 'Etiqueta'
	FROM SARASA.Cuenta, SARASA.Tipocta, SARASA.Estado
	WHERE Cuenta_Tipocta_Id = Tipocta_Id 
		AND Cuenta_Estado_Id = Estado_Id
		AND Estado_Descripcion = @Estado_Desc
END
GO


CREATE PROCEDURE [SARASA].[cbx_banco]
AS
	SELECT Banco_Codigo 'Valor', Banco_Nombre + ' (' + CAST(Banco_Codigo AS VARCHAR(18)) + ' - '+ Banco_Direccion + ')' 'Etiqueta'
	FROM SARASA.Banco
GO

/***********************
	Creamos triggers
************************/

-- Actualiza el registro de un depósito con el código de ingreso que le corresponde.
CREATE TRIGGER SARASA.tr_deposito_aff_ins_generar_codigo
ON SARASA.Deposito
AFTER INSERT
AS
BEGIN
	UPDATE SARASA.Deposito
	SET Deposito_Codigo_Ingreso = SARASA.generar_codigo_ingreso(i.Deposito_Id)
	FROM INSERTED i
	WHERE SARASA.Deposito.Deposito_Id = i.Deposito_Id
END
GO

-- Actualiza el saldo de una cuenta luego de realizado un depósito
CREATE TRIGGER SARASA.tr_deposito_aff_ins_modificar_saldo_cuenta
ON SARASA.Deposito
AFTER INSERT
AS
BEGIN
	UPDATE SARASA.Cuenta
	SET Cuenta_Saldo = Cuenta_Saldo + i.Deposito_Importe
	FROM INSERTED i
	WHERE SARASA.Cuenta.Cuenta_Numero = i.Deposito_Cuenta_Numero
END
GO

-- Detecta si una cuenta realizó un depósito con su suscripción vencida.
CREATE TRIGGER SARASA.tr_deposito_aff_ins_detectar_cuenta_vencida
ON SARASA.Deposito
AFTER INSERT
AS
BEGIN
	DECLARE @cuenta_numero numeric(18,0)
	SELECT @cuenta_numero = i.Deposito_Cuenta_Numero FROM INSERTED i

	DECLARE @dias_restantes integer
	SET @dias_restantes = SARASA.calcular_cantidad_dias_restantes(@cuenta_numero)

	DECLARE @tipo_gratuita integer
	SELECT @tipo_gratuita = tipo.Tipocta_Id FROM SARASA.Tipocta tipo WHERE tipo.Tipocta_Descripcion = 'Gratuita'

	DECLARE @cuenta_tipo integer
	SELECT @cuenta_tipo = cue.Cuenta_Tipocta_Id FROM SARASA.Cuenta cue WHERE cue.Cuenta_Numero = @cuenta_numero

	IF @dias_restantes < 0 AND @cuenta_tipo != @tipo_gratuita
	BEGIN
		RAISERROR('La suscripción de la cuenta ha caducado.',16,2) --Notar que el estado es distinto, ya que en el procedure vamos a inhabilitar la cuenta si falla por éste estado.
	END
END
GO

-- Actualiza el registro de un retiro con el código de egreso que le corresponde.
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

-- Modifica el saldo de una cuenta luego de realizado un retiro
CREATE TRIGGER SARASA.tr_retiro_aff_ins_modificar_saldo_cuenta
ON SARASA.Retiro
AFTER INSERT
AS
BEGIN
	UPDATE SARASA.Cuenta
	SET Cuenta_Saldo = Cuenta_Saldo - i.Retiro_Importe
	FROM INSERTED i
	WHERE SARASA.Cuenta.Cuenta_Numero = i.Retiro_Cuenta_Id
END
GO

-- Detecta si una cuenta realizó un retiro con su suscripción vencida.
CREATE TRIGGER SARASA.tr_retiro_aff_ins_detectar_cuenta_vencida
ON SARASA.Retiro
AFTER INSERT
AS
BEGIN
	DECLARE @cuenta_numero numeric(18,0)
	SELECT @cuenta_numero = i.Retiro_Cuenta_Id FROM INSERTED i

	DECLARE @dias_restantes integer
	SET @dias_restantes = SARASA.calcular_cantidad_dias_restantes(@cuenta_numero)

	DECLARE @tipo_gratuita integer
	SELECT @tipo_gratuita = tipo.Tipocta_Id FROM SARASA.Tipocta tipo WHERE tipo.Tipocta_Descripcion = 'Gratuita'

	DECLARE @cuenta_tipo integer
	SELECT @cuenta_tipo = cue.Cuenta_Tipocta_Id FROM SARASA.Cuenta cue WHERE cue.Cuenta_Numero = @cuenta_numero

	IF @dias_restantes < 0 AND @cuenta_tipo != @tipo_gratuita
	BEGIN
		RAISERROR('La suscripción de la cuenta ha caducado.',16,2) --Notar que el estado es distinto, ya que en el procedure vamos a inhabilitar la cuenta si falla por éste estado.
	END
END
GO

-- Genera un item factura después de la creación de una cuenta
CREATE TRIGGER SARASA.tr_cuenta_aff_ins_crear_item_factura
ON SARASA.Cuenta
AFTER INSERT
AS
BEGIN
	DECLARE @precio_por_tipo_cuenta		numeric(18,2)
	DECLARE @tipo_de_cuenta				integer
	DECLARE @cuenta_num 				numeric(18,0)
	DECLARE @fecha 						datetime

	SELECT @tipo_de_cuenta = i.Cuenta_Tipocta_Id FROM INSERTED i
	SELECT @precio_por_tipo_cuenta = tipo.Tipocta_Costo_Crea FROM SARASA.Tipocta tipo WHERE tipo.Tipocta_Id = @tipo_de_cuenta
	SELECT @cuenta_num = i.Cuenta_Numero FROM INSERTED i
	SET @fecha = GETDATE()

	EXEC SARASA.crear_item_factura @cuenta_num, 'Creación de cuenta', @precio_por_tipo_cuenta, @fecha, NULL, 0
END
GO

-- Incrementa la cantidad de items facturados para una cuenta, y la inhabilita si ésta llega a 5 ítems sin facturar
CREATE TRIGGER SARASA.tr_itemfact_aff_ins
ON SARASA.Itemfact
AFTER INSERT
AS
BEGIN
	DECLARE @cuenta_numero numeric(18,0)
	SELECT @cuenta_numero = i.Itemfact_Cuenta_Numero FROM INSERTED i

	DECLARE @cant_items_impagos integer
	SELECT @cant_items_impagos = cue.Cuenta_Items_No_Facturados FROM SARASA.Cuenta cue WHERE cue.Cuenta_Numero = @cuenta_numero

	-- Actualizamos la tabla Cuenta
	UPDATE SARASA.Cuenta
	SET Cuenta_Items_No_Facturados = @cant_items_impagos + 1
	WHERE Cuenta_Numero = @cuenta_numero

	-- Como la transacción todavía no se ejecutó, para validar que el número es >= 5 hay que comparar con 4
	IF @cant_items_impagos >= 4
	BEGIN
		DECLARE @estado_inhabilitada integer
		SELECT @estado_inhabilitada = est.Estado_Id FROM SARASA.Estado est WHERE est.Estado_Descripcion = 'Inhabilitada'

		DECLARE @tipo_actual integer
		SELECT @tipo_actual = cue.Cuenta_Tipocta_Id FROM INSERTED item
													INNER JOIN SARASA.Cuenta cue ON cue.Cuenta_Numero = item.Itemfact_Cuenta_Numero
													WHERE cue.Cuenta_Numero = @cuenta_numero

		DECLARE @cliente_id integer
		SELECT @cliente_id = cli.Cliente_Id FROM INSERTED item
											INNER JOIN SARASA.Cuenta cue ON cue.Cuenta_Numero = item.Itemfact_Cuenta_Numero
											INNER JOIN SARASA.Cliente cli ON cue.Cuenta_Cliente_Id = cli.Cliente_Id
											WHERE cue.Cuenta_Numero = @cuenta_numero

		EXEC SARASA.modificar_cuenta @cliente_id, @cuenta_numero, @tipo_actual, @estado_inhabilitada

		-- Agregamos la inhabilitación a la tabla auxiliar que lleva la cuenta de estos eventos para el listado estadístico nro. 1
		INSERT INTO SARASA.Inhabilitacion(Inhab_Cliente_Id, Inhab_Fecha)
		VALUES	(@cliente_id, GETDATE())
	END
END
GO

/************************************************************************
	Insertamos los datos que no estan disponibles en la tabla maestra.
*************************************************************************/

-- Roles
SET IDENTITY_INSERT SARASA.Rol ON
INSERT INTO SARASA.Rol (Rol_Id, Rol_Descripcion)
VALUES 	(1, 'Administrador'),
		(2, 'Cliente')
SET IDENTITY_INSERT SARASA.Rol OFF

-- Funciones
SET IDENTITY_INSERT SARASA.Funcion ON
INSERT INTO SARASA.Funcion (Funcion_Id, Funcion_Descripcion)
VALUES	--(1, 'ABM de Rol'),(2, 'ABM de Usuario'),(3, 'ABM de Cliente'),(4, 'ABM de Cuenta'),
		(1, 'Depositos'),
		(2, 'Retiros'),
		(3, 'Transferencias'),
		(4, 'Facturacion'),
		(5, 'ConsultaSaldo'),
		(6, 'ListadoEstadístico'),
		(7, 'AsociarTarjeta'),(8, 'DesasociarTarjeta'),(9, 'ModificarTarjeta'),(10, 'AceptarTarjeta'),
		(11, 'BuscarTarjeta'),(12, 'BuscarRol'),(13, 'AgregarRol'),(14, 'ModificarRol'),(15, 'EliminarRol')
SET IDENTITY_INSERT SARASA.Funcion OFF

-- Mapeos iniciales entre roles y funciones
INSERT INTO SARASA.Rol_x_Funcion (Rol_Id, Funcion_Id)
VALUES	(1,1),(1,2),(1,3),(1,4),(1,5),(1,6),	--Funciones iniciales para Administrador (No son ABMs)
		(1,7),(1,8),(1,9),(1,10),				--Funciones iniciales para Administrador (ABM Tarjeta)
		(1,11),(1,12),(1,13),(1,14),(1,15),		--Funciones iniciales para Administrador (ABM Rol)
		(2,1),(2,2),(2,3),(2,4),(2,5),(2,6)		--Funciones iniciales para Cliente

-- Monedas
INSERT INTO SARASA.Moneda (Moneda_Descripcion)
VALUES ('Dólar Estadounidense')

-- Estados
INSERT INTO SARASA.Estado (Estado_Descripcion)
VALUES ('Pendiente de activación'), ('Cerrada'), ('Inhabilitada'), ('Habilitada')

-- Tipos de cuenta
INSERT INTO SARASA.Tipocta (Tipocta_Descripcion,
							Tipocta_Vencimiento_Dias,
							Tipocta_Costo_Crea,
							Tipocta_Costo_Mod,
							Tipocta_Costo_Trans)
VALUES 	('Gratuita', 0, 0, 0, 0),
		('Bronce', 30, 5, 1, 3),
		('Plata', 60, 10, 1, 2),
		('Oro', 90, 15, 1, 1)

		
-- Usuarios administradores
INSERT INTO SARASA.Usuario (Usuario_Username,
							Usuario_Password,
							Usuario_Fecha_Creacion,
							Usuario_Fecha_Modificacion,
							Usuario_Pregunta_Sec,
							Usuario_Respuesta_Sec,
							Usuario_Habilitado,
							Usuario_Cliente_Id)
VALUES	
		(	'admin1', 
			'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7',	--hash sha256 para w23e
			GETDATE(),
			GETDATE(),
			'¿Cual es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92', --hash sha256 para SARASA
			1,	--Habilitado
			NULL),
		(	'admin2', 
			'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7',	--hash sha256 para w23e
			GETDATE(),
			GETDATE(),
			'¿Cual es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92', --hash sha256 para SARASA
			1,	--Habilitado
			NULL),
		(	'admin3', 
			'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7',	--hash sha256 para w23e
			GETDATE(),
			GETDATE(),
			'¿Cual es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92', --hash sha256 para SARASA
			1,	--Habilitado
			NULL),
		(	'admin4', 
			'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7',	--hash sha256 para w23e
			GETDATE(),
			GETDATE(),
			'¿Cual es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92', --hash sha256 para SARASA
			1,	--Habilitado
			NULL),

		-- Usuarios comunes
		--1
		(	'gzfpxiza',
			'f14b2034f55a349c2f09b60b8aea31c860c086e48f4f13e0fbe2b54907812539',	--p97oqp
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--2
		(	'bslcfkku',
			'7574286971185752b3a1eb51278847a43605b4cdfbbc7c31d624933ab6615a64',	--2yfwpx
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--3
		(	'alnalkkd',
			'f731bb838c43b989a22a4a630f0e9d477d88263a6c4c751a31b3671a198c9682',	--9atkhg
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--4
		(	'owytunhy',
			'886b044eba552a5b4218bd05c8100f01d79ddd653ecc486c10f95cc1f3dfbcd9',	--cf1mq3
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--5
		(	'kjtyxnne',
			'410351144b095beb4c91cae8bb8b607dfa8d4854934143712c786cdcb32733a0',	--lmst5o
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--6
		(	'asmnepgu',
			'ecb04cbf14e9b3253e77348b53ce575012df5ea8b698a02137ebef6ba2326588',	--wr4yjv
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--7
		(	'kbiacfks',
			'11a985e8921aedf0ba48c3f90f295512eebcaea1344ea1747f149dc32f7d6228',	--4me164
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--8
		(	'nbdjfwnv',
			'535ccc2a0896b64557083d05db70cb10fea62aaebf95ecb3595071ab7a277289',	--yjip03
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--9
		(	'ucznqdzx',
			'a4f24ef457aab51bf5219cf253fcdadc260cbca10a3731157e81932fb79c76cf',	--s9pd8w
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--10
		(	'kvppboau',
			'498886af7bfbed61fd4cf7a60214bf0675842cb0005e38ca7e0c2a7cde82bf9c',	--l6z5pr
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--11
		(	'msmkyris',
			'bba080359e76647d89c293b2b72477ddaeca48a3b81dd1a1ef541d3ce9c5de8d',	--wn9i5q
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--12
		(	'pchsgdkb',
			'770af3e289e4222d5627a407b81419958d183e47863e8e8ccbe97572ff09bc57',	--fwgtfl
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--13
		(	'ksxwkchk',
			'23ab5d43a55d9f0da4e63cdeaf9b446f46a1591d88af34b1f150d1ca4a1a8e92',	--y158x5
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--14
		(	'asrzryed',
			'04fc3e2da1ed55ea9c4e17b777c33368bbf384f1da0b5fc646b6cfc8f5dcce15',	--vmwe1e
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--15
		(	'xsplpvki',
			'84dd88a13fe9e882a3b7f37b8ce95806fdb5598a8493e0b17dbfd60064d0356d',	--k1mm8w
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--16
		(	'josjbmvc',
			'b8a6fb38761aaeffe0f467dd14876b5440cb0b60609cff1c3358e5c0c01cd247',	--721oag
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--17
		(	'ifyjesis',
			'2c45c6cae638725843f79bc12b01af60906ee054a93fb0ca962852dc9c621100',	--ro4dly
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--18
		(	'jseabkev',
			'46d186cb29e663e1885188d622db73133e392a0bf291e37e76ed82d6d06344bf',	--052pwk
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--19
		(	'deambsyk',
			'28c75e21625a61bd880d1be190538bb030c6b275919e88bbfc4fdb4a161531ed',	--cvs7u5
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--20
		(	'dcfvzwom',
			'3ddbbcf9c929fc08818550418bf2473a059e237c756b3e035d78b7f3f7c8ccb0',	--bp6c3d
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--21
		(	'dvjqituw',
			'7dee79615ccce8508ec46d5eb7c0da052f52f666819e5c3f641860cd8c467dab',	--3p445e
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--22
		(	'pjvbucry',
			'6d5b33a4f653bb5a5ce10f573e642d0698023591de8bd7ee577c7f1c1f96c60b',	--y8suc3
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--23
		(	'bcjfvvoy',
			'03e0e86170863dba5eb9c5cb7f6cda21a39c0a8c0f94c5033a7998975a288dd2',	--im3ur7
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--24
		(	'xulpbsad',
			'd27b15acbbf57841a9126a14d02fcf5b359a4ae8a9cde4fe0e24b2154896e5b7',	--1xg2do
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--25
		(	'klzctzxd',
			'e2efc20ad5bbc2680ff9c894af48a6f1b882bf3c9c3564d3f18dba54bb30e292',	--3knvnj
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--26
		(	'wieidrnk',
			'e20b0daf06d1cbb884141d7990b51f54cfd353e998f1b3ee42f8fb3c18de78f3',	--p763mz
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--27
		(	'tlilpuib',
			'114a8ff9361fb5c34a4f6ea8c55c189f642482f3d678a1c57e7218cc037fb806',	--vxrffp
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--28
		(	'vcsipyzt',
			'f0db6c8155748e000d675140acdbe2759e7374d3d7cf6fb3efb300bd2bd8af5d',	--amuikj
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--29
		(	'soiewinc',
			'2f0c521c3d31679892a7814d8b74a5f8161d86504988418312d2ead487026f03',	--160dhc
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--30
		(	'fmnhffig',
			'11b41970a4ff3af09957b23c468c8fe9f2e38bc3a747a639cb907390594159ed',	--sk3isb
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--31
		(	'ecpwdhis',
			'f736e7c96b0e4a44f27b215a0beac613e8069d99346e9db6f2098cee85eb319e',	--xrr9cd
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--32
		(	'gxnouuoe',
			'a129ec91155dd1388428c26d32eb0484fef63743b19307c90a98439152fc839b',	--vofsen
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--33
		(	'raolevdp',
			'701c801861008315da81238c620f4302979a86e998b433f79d213fd6efdcd0bf',	--bdfj66
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--34
		(	'usulgaei',
			'b840923baf3ed6adb6a513108246e95f529345b25d54ab66052268e857fa4a3f',	--5p6wva
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--35
		(	'fzrquwyw',
			'edc4e266731268de5aa486fceb28408b3b4f89ddcb0c82ab064eefc2ba3a2112',	--dj27nv
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--36
		(	'rixezkyj',
			'a153bcd68a9e9459073725c7655fd31c7a5dc54e858ba5ed315a0b2c4fe9f378',	--noo940
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--37
		(	'nskorysc',
			'7992b990fa7cfd19a95e0e59642a9d99d07a884f58c720b887a499398ef71bca',	--e7ibju
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--38
		(	'ufghmzzq',
			'd8a9aaf547be1b2e0087ab960ec6d7503b116af5be2f99d40fe90bea2acf4d93',	--2htmyc
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--39
		(	'taccegfi',
			'afd2701b7a8901e32db3ad68b8ae4839969f85cb50634447ec2fb154e7aaf38c',	--ywhigi
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--40
		(	'xosnpexv',
			'3c5f6c765d2d99382088a9ec6aab03eef9770c55de3233f902f353c33ec3d927',	--f9r68q
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--41
		(	'cftjdsxz',
			'b08dd40e44a8e6755d2635251a068460426d0f0e1610669e9ac4b037bb43258b',	--ykr6a2
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--42
		(	'fanznvwa',
			'841597965ae18137a39654c6969cc2b46df910b597fa59f60c9b7f957963651e',	--7lyjsu
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--43
		(	'ydpglhuz',
			'68ec25fbd1272d828fd9022b77d66220436e6b32daab68c6e025bcf432a5d326',	--rxnoup
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--44
		(	'cbhqgasg',
			'38826164748ebf98cb782fcf133f9dd103862a29ab9177db66dac25b43153ad7',	--kwl3na
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--45
		(	'igotihfv',
			'bf269bfe219a34c5ac81feef5ea757b921439d5db35941e3be1864d3122a02e4',	--o7ls18
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--46
		(	'piachxsg',
			'3983df0a7a5c533299da24cee672557b3d7b11fd14060786d808894805cb6f67',	--xwfhvy
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--47
		(	'weravudp',
			'df2a019e3754faa725cb5ff1979c0b30ce1a6498fd4cc427b72a7afe745d09de',	--jommfu
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--48
		(	'pbtfhtqy',
			'331e05a53b3a651435e69c4712a5fea9c0c94fe41b47f36050fbb4bc8e587057',	--2hhdzj
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--49
		(	'iejkkprb',
			'6d95c23d9b5afebe28c83001665f40761e00f9cfd4b5d8437f67a98449ff433e',	--dbdxaj
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--50
		(	'dgrtimcj',
			'2afea4556b3465108df7bffaee914589cfc4f9ba069ffc95846530123532e041',	--bdztjz
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--51
		(	'hlkheqew',
			'fb8b37ece59c987eafb5a34303c2f3d52f0ead4c4ae822a555a7ff799f123bfd',	--jicijk
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--52
		(	'elesavdq',
			'46ea6a56fe6f90d22d71160e6a5689438ebf01050e2c69a2a6002d18fab3c5a4',	--kxlywy
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--53
		(	'rcguargy',
			'6b589cde820b31cebd89a801b64e02af93947fb9e51a3708a6e7762b3e19583d',	--xzdz9j
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--54
		(	'xfulzkeo',
			'4f07f5642298ad35edecf1fd363699d81ea77628f1eb4e410acf7c565d8dde23',	--zu2ytd
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--55
		(	'bybfgztn',
			'e52c7913702c799d9bcf34740fd699e157e100da64f041c4868cb999228b0096',	--kbadgj
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--56
		(	'azzghhmt',
			'5b489442531ff765e301c53a8c93496001b17c85095700a815d74bd76ea6f246',	--kvbiak
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--57
		(	'llxzjfpk',
			'a676dba3f8e35bfecf99b103f4c42749e77c9c51b0caf755544cf597f97afddb',	--ehxjq9
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--58
		(	'lbdkowfy',
			'fdd14f1ec1090d68989c78e28896db6b5b9d29a56a150c20d0efe6fb9e414edf',	--2n0qv5
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--59
		(	'cfsbnxgh',
			'66f5dc770b10136410d2a3655e85e85bba61b449d6e5fbe154656a991f911c7f',	--zqobjd
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--60
		(	'btitiwmg',
			'425631060047b8929ef23b1c786071b086e049c58805954bd0bf555aeee773aa',	--p9exm6
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--61
		(	'albajwnz',
			'1ee2ce47d7adf9d9a9d09b2a5344ad81eb5a2dfb64ab4876141d34002dca879c',	--l75wxe
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--62
		(	'llfkkeqx',
			'69f9b728fb646a9585d8a5fb1b0512cda50601a0fce1c5cc2ed1349bda2c856a',	--f4idgu
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--63
		(	'dxjjgwdj',
			'321b3917b0126aad72444082b97c8359ff1a69c4d7fd164838f3240e1a6a864f',	--eskjx1
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--64
		(	'nzfpgrjd',
			'0e1af5cef272be0ed8ea2ca733f5adfa28a50df8b93d49e57f38365e78a20647',	--d1yp1x
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--65
		(	'njzqhwwv',
			'ed028ed9fc4033e1caae17379fa494b2c089caada61f393ee06c130cc2f91888',	--ajw6oo
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--66
		(	'tcheefmv',
			'b411df3b99860f16b614f3b6d50b7576586c6f81f6c6903c172572d2595d3458',	--gl9zlp
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--67
		(	'hctwuroz',
			'acf8286c3c9977846284ad3aa39ec2bbaaf908518a4837b9a79b878db7802a55',	--q06kdu
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--68
		(	'ovrzlvyp',
			'58fff03c30a6c832c2d2f21d01a6c3a5b77aa56e5b3d73dbdcc68d9e63f5428b',	--17fbvf
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--69
		(	'spaqcjzz',
			'70ef097806ac6fba2dbd80fe19d118efde4ea13cd7ccb0e07e24320edde74e12',	--7rpwqm
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--70
		(	'emmgdgid',
			'e00f28d15629aeca8d96b3b846c6ed116aa26b0829dceef4566527284cd34cdc',	--pi711g
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--71
		(	'brhylcnf',
			'804ec9493f07ae8d16b9e0f5855191a817a8559c3434dc4e91e8c1d7b4099b8d',	--xmrep6
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--72
		(	'rnlwkqkp',
			'57c9f0484aff9589413ff9e74cbdb4a34dc0c6ab3738039ee6aadcb603d525e2',	--tm67op
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--73
		(	'mkdzsqma',
			'34e09d569457896f85b919cbf0f2c8d6da44243cea274eb6d66da1c124108a2e',	--88ca19
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--74
		(	'bqzxkdgy',
			'bb3b09da0709a54cc9dc0eb6a45be79fbe62cfff52af1a7d8e4f593c51d2489b',	--l4f77l
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--75
		(	'unpzneqj',
			'aec21b6b09bafacb626bd2138f5b8fa4ef45a6d82fae99efd2c7e294d20fdc44',	--45rnj7
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--76
		(	'hpftldaf',
			'b4a4fc7a87bbc75fdcfdaf9934fd0505ce5f7919aced0350f88b7bba3953aafe',	--5evjlc
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--77
		(	'ydfriirs',
			'128ae3aab59267f6ae968633d32025f1702f2e7bddc84513a3eedc4776df58ab',	--o8udj8
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--78
		(	'mzqazvjt',
			'64a5db941f98e5545541e6c367205fef716e4005b255b7ba21c181fb82dac4e4',	--s94jyq
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--79
		(	'yzenxkwy',
			'a4532aa3eebee9c1557f27576135ed7b9754bc71cc316bcb9e6d2d8556a3b0fc',	--b03eqz
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--80
		(	'zetdeycn',
			'5088de5766c6717b859e5608e7711dc12322d43f3e294c4cfda7058f7809d748',	--00d1gg
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--81
		(	'oommcuyr',
			'364e85fef27f7ad9725b78027c1b2909595cf686ef14196d79e80e0dabe993b7',	--byqqg1
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--82
		(	'zkqzjrgw',
			'8192a98ed44f985463539d1533b369265fe54b17533905d5a74ab0d11f5b98b5',	--sxbz75
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--83
		(	'ycftvrwd',
			'5e418baeaf247abb9772b1d48649190f98924c09d549ede5c7d8bc3742138364',	--6k1kg3
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--84
		(	'izipovhr',
			'5a9e41ef0d253555c72027c2c864b89367b0db8df13a3a278859a7217f036fba',	--mwwp20
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--85
		(	'dywzogng',
			'd991c2489a18584d51466ac30ff5dc65419daeaf6e2ff6edae0cc6f33ff1feff',	--th2oml
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--86
		(	'kplavqir',
			'8f505911b49277cf43619cf044c56e53eb744bf0de1c6677b697359de54e112a',	--zfnm12
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--87
		(	'vgaiyfve',
			'0deaae1fe72b81588c58884743826b32337a9c3e19bec1618ef9cf57ae6244ef',	--q6a82w
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--88
		(	'zxdrgdiq',
			'03c52eb86a8ca53ec14ae0fdc0e50b5d828adfd643d4b5c8a0898f27523f9c82',	--7h6hfh
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--89
		(	'fbvvoicn',
			'5f7e22fdc0b8aad937e2ee060ad8ce072ca9604fbedacbb9104d4aff6c7c0e04',	--ziv4lx
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--90
		(	'jruwnnmk',
			'0dd78c7d40d52b33b6b89ee31c8a84e534b803094216f31ee25dfa30298401f1',	--3g6oxz
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--91
		(	'rndczmrl',
			'ab9789639bde2ab775773b3db0e5ba2af47630e47ed43e4d7a26e8d609ced123',	--8xy16g
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--92
		(	'svcxoaqm',
			'07ceab1674a450411a9d5ab09ae95d568553f8dc60db9771a69febb3a4719157',	--85299m
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--93
		(	'vvycjqbm',
			'293c521f094d23bf99e26827da885a9fa2cad03fffca35843835bf92e6ab0abb',	--rrbx9b
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--94
		(	'kmmpzzta',
			'e5d0c5e6b973a18c0c8091ee38c5e5142b9dc0aefadabcecd2d2eede027ee3b3',	--0yc79m
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--95
		(	'xtfrpkyu',
			'd23edce8a617fd9c1b4b2b4f4460d2c6ce804de12f1db57de449b8e6ed5c3182',	--scogq9
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--96
		(	'zofbxuwe',
			'8b65762b0e73f984b64db2dd0e1537bf6772ba59de058afb64ca5e39db25e028',	--to0kcg
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--97
		(	'dktwmudn',
			'd4d714c2dcfd4cccb9f1cf82308cb9b0db492d66d67971ae0e547899c95bbcb5',	--dwr1dt
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--98
		(	'cbaatydl',
			'21add5dc4f4209ab60c635e853cc47daec2156f71295c29955289a43bb2e13b4',	--041spz
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--99
		(	'xhcdlsax',
			'2a638e72a7782b16bcc295e7e037480687ccdb4d642c50c3beefedd678aa0b25',	--9suzvn
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--100
		(	'tiikxotx',
			'6c5615d447439aed6d848da7ad3f4c513cf167f87249c0fb138c45b7a1d063c9',	--b577ek
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--101
		(	'lerdzkia',
			'41a7c28f04b9064707091d5253bf4d50b229b50ab8d1d3474e3613daab2655ae',	--701yef
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--102
		(	'jfpalrkx',
			'4fb96fbff5dfb16685c78047ad96437fbf635d7ae743e824c82434962f015e3a',	--9o7equ
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--103
		(	'lnwyhwju',
			'ea9a4def2b0a3dfa5d3b418140eb7fee4afb5ea66a72adf1a984f8e81d307651',	--sgs3nj
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--104
		(	'jggmaxil',
			'7549360c40d153a52a81a033d59c268b489b776f5f9964257e8004c103f96466',	--kacch9
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--105
		(	'dbiscklp',
			'658308aab4b9db4d387befc1635880f0fb70868067344a3977a6133d0d5adada',	--8wel4g
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--106
		(	'gdxbrncr',
			'90581a12641a5dd1c5b94b721afc1c5ed8e4948af81c74d206afa3681207d402',	--491t3h
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--107
		(	'yqybflkv',
			'c7f05043396a09000e8078d2581c0734b50eaaaaa6fe6076a999c4530d990ff3',	--722i3t
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--108
		(	'zqbkmgip',
			'a5a5450bdd865aa38f5388a9e874b08497140176f01a3d620e06652da2dc6e82',	--4jshuz
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--109
		(	'hghkwyds',
			'382ad712a69b42b652ec5c0d46b73f17f3c1e2fb855db05549934d8e7c4baf23',	--9iu6cd
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--110
		(	'nfhywavg',
			'0dab0412469e7a03a77fdd9a492d9cbcc716f7f992e7becbf2309d753b635767',	--zp6j91
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--111
		(	'cxdzzudg',
			'5c3c9265b4f3ec64ab18ff514f3c6acb4c9d4d6473b4e065c4edd2aeac43c50b',	--wb4fi5
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--112
		(	'gwhdnhgw',
			'57337f1a8bdcb4145fdbb8918e01a7c2c3c6da65174ec6e97cb7efdde061c243',	--pzj5fc
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--113
		(	'xvzqaykh',
			'eb79e170113b2feb4fe5909111d42a29f0b091721357244e2feaedbe5c2010e2',	--8d4bdx
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--114
		(	'rccmzzhh',
			'40b86cd0512e3c2788d45fd0777b26ce8d9fb90966eb125473f3695f8c23d668',	--ktj4gn
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--115
		(	'faocmlxp',
			'b6cc25f73bafd5c30ae94d68c4c976bdd0e874af2f335fd8ad4bb1902a61760f',	--tzdkg0
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--116
		(	'lzjzmkct',
			'ed6c48985f7abbfbca3f38c18388c58f4c5d6290a8c6de0857cf1e7564ff1f13',	--ry12y4
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--117
		(	'llvcflcb',
			'6a99b8a2fddb6be393ee719fe33266576ac42bf2388489be439cf10cbf266525',	--lvu9fj
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--118
		(	'lmommecv',
			'1de256676741d0117981f9e8238eecdfbc9faad1684589b0fce3d297aeb56f43',	--ez97tk
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--119
		(	'tchteadj',
			'ba3bde889d00d174e45690ef7423b0d5f614456ecb3942ad5fa6b477de69341b',	--rsunjk
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--120
		(	'ptrieimq',
			'9c04717d0a2d1cf0da11474b73cf621c153b15e18208ef2e8fcd27d669502879',	--8at47p
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--121
		(	'ufjxfslw',
			'e23647f3d8aae51b36ac05c1bca680b6816b637a037c3a0198b03b3d88083797',	--m2gbf9
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--122
		(	'sawrecvv',
			'1862b78effab3627c2f0b6bc05ae13ad3e274657c7fe271304da910171cc90bd',	--bph1ou
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--123
		(	'qgxypncz',
			'90abe1e26f5e217dd65e25aa1837c7c73a337adadcf3d40267a79eefa311d8ed',	--f6s4iu
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--124
		(	'eyjatpfe',
			'98644331e5485c6a2fcfaf2295d55eee92eaa086571afb79bb93550623f2c4b4',	--0edkcy
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--125
		(	'amiiintp',
			'70d1be75e725bd1d2b038df81f5d28e791a49390d58d23ddeeb0e10ae60cc2a4',	--5o5rzl
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--126
		(	'fdgnpsna',
			'01a91c7ef1018f5e99284fafc0afac5f8ffcc30de379ea26c254b356b19eb11e',	--oqxvsj
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--127
		(	'bwruhkrh',
			'aacd73dcdb4708bb0bd12c3320035419f3725305fa9cd42871c7bef3c123cfca',	--02h4qc
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--128
		(	'thwsomke',
			'a8f16802fe9e5733c5b2ebe41dd4ea69a6e10c58dd4ce7b77128da279650fa60',	--xvpcld
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--129
		(	'saibjlua',
			'55dd254c606a7aea4458de53ae99100ce0e274655e89082121780c7e3f0a6939',	--e4fxyo
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--130
		(	'tudgcfeu',
			'188715c04bb1664d22fdcf70c95090d3e0e819e8980820f815fac4acbb8b697f',	--mna0iz
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--131
		(	'omfeubaj',
			'12041e8982208b357ff0b560b296c19c0601310f5dc717e583466d6bfe2e4300',	--5nmpfw
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--132
		(	'acejevsz',
			'405fa581d5acdbd413c2e4f6b9fb21434236d00bc01ef18f7d16592a311229aa',	--00bkc3
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--133
		(	'obvuipud',
			'180d801acd6a0c7b35a311c7ca3091190adfa77514f8cca2f8ba21f4035d25e7',	--ojgotj
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--134
		(	'fskktsuw',
			'08aaf41850802b931ddff3f3235c29661cd378b950c9e95fee4f793fed528eb6',	--mcumab
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--135
		(	'hfpklcdm',
			'772056a46fb9ce423ce47bb5cf7bfde0aa2e0dd3c10dbb48c95926964e1e4089',	--wjx9jw
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--136
		(	'fxmjtjdn',
			'58552e99d7e53d37499ec3dbf1b685546c305a45e244037ff646ba9b7ac09672',	--z2fk47
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--137
		(	'mhspihxw',
			'af91758156c524c1d9f04dedcffef7188e58b2445a9a390e15c75293d35323e9',	--xvyjy4
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--138
		(	'zezsenap',
			'b55c3a5cff5c6de952fb4d39f8fb88b2bbc581cf45c3583709d30d073713b2a9',	--42sy9d
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--139
		(	'fcjzfutp',
			'd9c7821600fdfad702fef976eedf0576807e55bc88d7d83467eacad9e14338ff',	--8stdpc
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--140
		(	'vtlqjzfb',
			'b93ae936468b73388e94c84fae1105f777dbfc7a4d45b8d0a008c35d8ac3329d',	--9yho8m
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--141
		(	'krwlawsu',
			'7b133a9e0f9c6e65348f7c6b8489a62e05c9af61bd8fddd527fa934dbac033e4',	--yssisw
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--142
		(	'gxuwrrtl',
			'8aa4aaaa9cd0100d0e10e1946f2e66a600274417a25e4e4bdbf0c572efa32503',	--uerstv
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--143
		(	'xoromssh',
			'c7bd97cc0cdd0ec0acccff694a1ee088fe8682357ba24082d9d70c4c05562de9',	--6fmdf5
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--144
		(	'ntdytkzr',
			'1e59faedebf8bcdba47619fcbfed00eef91e6f62e80bec92b6292a31f95be2ab',	--6gtlpp
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--145
		(	'bvwonyro',
			'24d2d6bdeb97556c3e6e4a6dde97a789c74587502d19002ca4c045bdaebbc976',	--ac5r17
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--146
		(	'vlonanif',
			'e741aecfbf90b69e99d540f5b41dc164e97f73a5ccb119265776c066f97ff225',	--0wrhbg
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--147
		(	'avlnukrc',
			'4eedc5e68c1c5735f47ecfe79fdbb59db9c2a61fbd70796423232faed4da750f',	--xnww9z
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--148
		(	'pkeyfzxy',
			'e6e14b9740b023258535336c00cdee151845cf63a29b6afde0e56f9e68e15cc6',	--unpkmz
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--149
		(	'bhujdmdx',
			'2aaf2d95b52ab7d6676acccfab2b725c006ed0fd700a59d37b2770a80950194c',	--f6as2q
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--150
		(	'cnbtgtcn',
			'2e3bb2482bcb79cf30d8bac73651a3b33ba42e39b2abdcc203d39a20f4d51f38',	--appac0
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--151
		(	'tsbafgve',
			'68d25a21167543a83f25105db5a119d104029827576193956651dfa2b34b58e3',	--n292yt
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--152
		(	'aqlkrwtu',
			'22f0054a907818ab924fe1b9fbbc97dba33b15d4ca152187f3021db1d023f63a',	--nbvo1l
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--153
		(	'wfxackwo',
			'c8af1c13a17d5e77b616b7c5b58177d3bca1aac252caeeba46e77ca5d4ace246',	--ecopsz
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--154
		(	'vpwervcv',
			'ae9c30a35b6a358c8194be325fe4617fcf16d6e89c19816c1eb9992050ba764b',	--wmtnvy
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--155
		(	'korwyjhy',
			'4aafc1d7b9c78709efe7296a9a99904c057ca898febf8b03e6ef9adca742ffa3',	--r3h17p
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--156
		(	'pcekclwo',
			'f49642ea07f28156735ec26e982d13e6c6fae65ed6c8e2fb9bae223386a2a19e',	--rmhac4
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--157
		(	'pcxbchuu',
			'aa2dec8061106f56b74744d932cd4adecea7da62ecc3c658c5ef091c5676c00a',	--dxqboi
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--158
		(	'jcjrlzjq',
			'a71acdc9d3f64987e52a13aa81dbf2aa9e1cc5f55204ddba1ec9151c8ec6f69e',	--3tf2yu
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--159
		(	'snrlorxz',
			'efb0a0d890206c3f13bb8dc65a0aff52a97bd429916ef969742f332c16d87fde',	--3yi84e
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--160
		(	'dzapusmj',
			'2f5b5b717e38f4c2a8cd03019167a25fae752de186aeeffc76d1962b8cc8f621',	--gvoqi2
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--161
		(	'doanxtqe',
			'5f117551a190e6facf185e2eb5e5733d1d8661002b148a609b3261a6480c338b',	--kkx19u
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--162
		(	'mqrsigvp',
			'fcf7b24ecc6703a5aa19ed67fcd2ec317311934ae4dc19a23c9027851dfaa1d7',	--017jaq
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--163
		(	'sknkosmg',
			'41ee8b9bccae9160539e0eed4678e87e32cd9c253c692396cf54e69e7c2aa816',	--i65uam
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--164
		(	'gfhnvykh',
			'97f728a10abf05863e5ac3ca616f147295fdc01daf767aa663203f97d9fee039',	--dmrlpw
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL),
		--165
		(	'hgmciukc',
			'7cb905d197be9ad3a5f4236bb98a58fdacb110e6403ed576f17b1d9d339f21b3',	--ydyj9o
			GETDATE(),
			GETDATE(),
			'¿Cuál es el nombre de este grupo?',
			'8364183f24415b77bf25304c9f3c7f1f0f998f80d3b903db49a9605d44c1ce92',	--SARASA
			1,	--Habilitado
			NULL)

-- Mapeo entre roles y usuarios
INSERT INTO SARASA.Rol_x_Usuario (Rol_Id, Usuario_Id)
VALUES	(1,1), (1,2), (1,3), (1,4),	--administradores
		(2,5), (2,6), (2,7), (2,8), (2,9), (2,10), (2,11), (2,12), (2,13), (2,14), 
		(2,15), (2,16), (2,17), (2,18), (2,19), (2,20), (2,21), (2,22), (2,23), (2,24), 
		(2,25), (2,26), (2,27), (2,28), (2,29), (2,30), (2,31), (2,32), (2,33), (2,34), 
		(2,35), (2,36), (2,37), (2,38), (2,39), (2,40), (2,41), (2,42), (2,43), (2,44), 
		(2,45), (2,46), (2,47), (2,48), (2,49), (2,50), (2,51), (2,52), (2,53), (2,54), 
		(2,55), (2,56), (2,57), (2,58), (2,59), (2,60), (2,61), (2,62), (2,63), (2,64), 
		(2,65), (2,66), (2,67), (2,68), (2,69), (2,70), (2,71), (2,72), (2,73), (2,74), 
		(2,75), (2,76), (2,77), (2,78), (2,79), (2,80), (2,81), (2,82), (2,83), (2,84), 
		(2,85), (2,86), (2,87), (2,88), (2,89), (2,90), (2,91), (2,92), (2,93), (2,94), 
		(2,95), (2,96), (2,97), (2,98), (2,99), (2,100), (2,101), (2,102), (2,103), (2,104), 
		(2,105), (2,106), (2,107), (2,108), (2,109), (2,110), (2,111), (2,112), (2,113), (2,114), 
		(2,115), (2,116), (2,117), (2,118), (2,119), (2,120), (2,121), (2,122), (2,123), (2,124), 
		(2,125), (2,126), (2,127), (2,128), (2,129), (2,130), (2,131), (2,132), (2,133), (2,134), 
		(2,135), (2,136), (2,137), (2,138), (2,139), (2,140), (2,141), (2,142), (2,143), (2,144), 
		(2,145), (2,146), (2,147), (2,148), (2,149), (2,150), (2,151), (2,152), (2,153), (2,154), 
		(2,155), (2,156), (2,157), (2,158), (2,159), (2,160), (2,161), (2,162), (2,163), (2,164), 
		(2,165), (2,166), (2,167), (2,168), (2,169)
GO

/**********************************************************************
	Migramos los datos ubicados en la tabla maestra a nuestro esquema

	(Para esto primero deshabilitamos temporalmente algunos triggers
	que podrían llegar a interferir con el proceso de migración y la
	integridad de los datos)
***********************************************************************/

-- Deshabilitamos triggers
DISABLE TRIGGER SARASA.tr_deposito_aff_ins_modificar_saldo_cuenta ON SARASA.Deposito;
DISABLE TRIGGER SARASA.tr_retiro_aff_ins_modificar_saldo_cuenta ON SARASA.Retiro;
DISABLE TRIGGER SARASA.tr_cuenta_aff_ins_crear_item_factura ON SARASA.Cuenta;
DISABLE TRIGGER SARASA.tr_itemfact_aff_ins ON SARASA.Itemfact;
DISABLE TRIGGER SARASA.tr_deposito_aff_ins_detectar_cuenta_vencida ON SARASA.Deposito;
DISABLE TRIGGER SARASA.tr_retiro_aff_ins_detectar_cuenta_vencida ON SARASA.Retiro;
GO

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

-- Ahora que cargamos los clientes, les asignamos un usuario.
UPDATE SARASA.Usuario
SET Usuario_Cliente_Id = cli.Cliente_Id
FROM SARASA.Cliente cli
WHERE Usuario_Id > 4 AND
cli.Cliente_Id = Usuario_Id - 4		-- Hay 4 administradores a los que no queremos asignarle ningún cliente (aquellos con Usuario_Id=1,2,3,4)
GO

-- Desde tabla gd_esquema.Maestra a SARASA.Emisor
INSERT INTO SARASA.Emisor (Emisor_Descripcion)
	SELECT DISTINCT Maestra.Tarjeta_Emisor_Descripcion
	FROM gd_esquema.Maestra maestra WHERE Tarjeta_Emisor_Descripcion is not null ORDER BY Tarjeta_Emisor_Descripcion
GO

-- Desde tabla gd_esquema.Maestra a SARASA.Tc
INSERT INTO SARASA.Tc (	Tc_Num_Tarjeta, 
						Tc_Cliente_Id,
						Tc_Fecha_Emision,
						Tc_Fecha_Vencimiento,
						Tc_Codigo_Seg,
						Tc_Emisor_Desc,
						Tc_Ultimos_Cuatro,
						Tc_Asociada)
SELECT DISTINCT tm.Tarjeta_Numero,
				(	SELECT cli.Cliente_Id
					FROM SARASA.Cliente cli
					WHERE tm.Tarjeta_Numero IS NOT NULL AND tm.Cli_Nro_Doc = cli.Cliente_Doc_Nro),
				tm.Tarjeta_Fecha_Emision,
				tm.Tarjeta_Fecha_Vencimiento,
				tm.Tarjeta_Codigo_Seg,
				tm.Tarjeta_Emisor_Descripcion,
				SUBSTRING(tm.Tarjeta_Numero,13,16),
				'1'				
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
							Cuenta_Cliente_Id,
							Cuenta_Dias_De_Suscripcion,
							Cuenta_Ultima_Modificacion_Tipo,
							Cuenta_Items_No_Facturados)
SELECT DISTINCT tm.Cuenta_Numero,
				tm.Cuenta_Fecha_Creacion,
				tm.Cuenta_Fecha_Cierre,
				e.Estado_Id,
				t.Tipocta_Id,
				tm.Cuenta_Pais_Codigo,
				m.Moneda_Id,
				0.00,
				(SELECT cli.Cliente_Id
					FROM SARASA.Cliente cli
					WHERE tm.Cuenta_Numero IS NOT NULL AND tm.Cli_Nro_Doc = cli.Cliente_Doc_Nro),
				(SELECT tipo.Tipocta_Vencimiento_Dias
					FROM SARASA.Tipocta tipo
					WHERE tipo.Tipocta_Descripcion = 'Oro'),
				tm.Cuenta_Fecha_Creacion,	--En el momento de carga inicial, la fecha de creación y la de última modificación de tipo coinciden.
				0	--No hay items sin facturar en ninguna cuenta al momento de la migración
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

--Volvemos a habilitar los triggers deshabilitados previo a la migración
ENABLE TRIGGER SARASA.tr_deposito_aff_ins_modificar_saldo_cuenta ON SARASA.Deposito;
ENABLE TRIGGER SARASA.tr_retiro_aff_ins_modificar_saldo_cuenta ON SARASA.Retiro;
ENABLE TRIGGER SARASA.tr_cuenta_aff_ins_crear_item_factura ON SARASA.Cuenta;
ENABLE TRIGGER SARASA.tr_itemfact_aff_ins ON SARASA.Itemfact;
ENABLE TRIGGER SARASA.tr_deposito_aff_ins_detectar_cuenta_vencida ON SARASA.Deposito;
ENABLE TRIGGER SARASA.tr_retiro_aff_ins_detectar_cuenta_vencida ON SARASA.Retiro;
GO
