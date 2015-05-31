USE [GD1C2015]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [test].[Realizar_Deposito] (
		@clienteId integer,
		@depositoFecha datetime,
		@depositoImporte numeric(18, 2),
		@depositoMoneda integer,
		@depositoTarjeta varchar(64),
		@depositoCuenta numeric(18, 0)) 
As
BEGIN


	--	Actualiza el saldo en la tabla Cuenta
	UPDATE [test].[Cuenta] SET [Cuenta_Saldo] = ([Cuenta_Saldo] + @depositoImporte)
	WHERE [Cuenta_Numero] = @depositoCuenta

END