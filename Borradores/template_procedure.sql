/******************************************************************
	Template para stored procedures en T-SQL
	Más info en:
		http://stackoverflow.com/q/2073737/1603080 (pregunta)
		http://stackoverflow.com/a/2074139/1603080 (respuesta)
******************************************************************/

CREATE PROCEDURE [Nombre] (
	-- agregar acá los parámetros que recibe
	@parametro_ejemplo1		integer,
	@parametro_ejemplo2		varchar(29)
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

		--Hacer en este espacio lo que sea que haya que hacer y que pueda tirar
		--un error que haga necesario volver atrás cambios hechos en DB.

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
