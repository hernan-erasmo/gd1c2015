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


-- Linea de ejecucion
EXEC test.Buscar_Tarjetas @emisorTarjeta = 'Visa';
