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
