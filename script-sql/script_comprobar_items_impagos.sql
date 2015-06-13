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
