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
