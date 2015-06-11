-- Esquema test
CREATE SCHEMA test AUTHORIZATION gd

-- Crea la tabla de EmisorTC
CREATE TABLE test.EmisorTC (
	EmisorTC_Id	integer	identity(1,1) PRIMARY KEY,
	EmisorTC_Descripcion	nvarchar(255)	NOT NULL
)

-- Inserta algunos valores
INSERT INTO test.EmisorTC (EmisorTC_Descripcion) VALUES ('American Express')
INSERT INTO test.EmisorTC (EmisorTC_Descripcion) VALUES ('Mastercard')
INSERT INTO test.EmisorTC (EmisorTC_Descripcion) VALUES ('Visa')

SELECT * FROM test.EmisorTC


--	SP de prueba para el filtro de Tarjetas de credito
CREATE PROCEDURE [test].[Buscar_Tarjetas] (
	@emisorTarjeta nvarchar(255)) As
	BEGIN
		SELECT * FROM test.EmisorTC
	END

--	Tabla de prueba de Tarjetas de creditos
CREATE TABLE [test].[Tc] (
	Tc_Num_Tarjeta			integer,
	Tc_Cliente_Id			integer,
	Tc_Fecha_Emision		nvarchar(),
	Tc_Fecha_Vencimiento	datetime,
	Tc_Codigo_Seg			nvarchar(4),
	Tc_Emisor_Desc			nvarchar(255)
)

INSERT INTO [test].[Tc] (
	Tc_Num_Tarjeta, 
	Tc_Cliente_Id,
	Tc_Fecha_Emision,
	Tc_Fecha_Vencimiento,
	Tc_Codigo_Seg,
	Tc_Emisor_Desc)
	VALUES (1,101,SYSDATETIME(),SYSDATETIME(),1234,'Visa');


-- Linea de ejecucion
EXEC test.Buscar_Tarjetas @emisorTarjeta = 'Visa';
