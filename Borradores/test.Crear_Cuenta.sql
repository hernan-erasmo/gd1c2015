CREATE PROCEDURE [test].[Crear_Cuenta] (
	@clienteId int, 
	@cuentaNumero numeric(18, 0), 
	@fechaApertura datetime, 
	@paisId int, 
	@tipoctaId int, 
	@monedaId int) 
As
BEGIN


	INSERT INTO test.Cuenta (
			[Cuenta_Numero],
			[Cuenta_Fecha_Creacion],
			[Cuenta_Fecha_Cierre],
			[Cuenta_Estado_Id],
			[Cuenta_Tipocta_Id],
			[Cuenta_Pais_Id],
			[Cuenta_Moneda_Id],
			[Cuenta_Saldo],
			[Cuenta_Deudora],
			[Cuenta_Cliente_Id])
		VALUES(
			@cuentaNumero,
			@fechaApertura,
			@fechaApertura,--
			1,--estado id, pendiente de activacion
			@tipoctaId,
			@paisId,
			@monedaId,
			0,--saldo inicial
			0,--Cuenta deudora? falso
			@clienteId);

	-- FALTAN LAS DEMAS OPERACIONES EN EL RESTO DE LAS TABLAS
			
END
