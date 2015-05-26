USE [GD1C2015]
GO
/****** Object:  StoredProcedure [test].[Buscar_Tarjetas]    Script Date: 05/26/2015 14:18:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [test].[Buscar_Tarjetas] (
	@emisorTarjeta nvarchar(255)) As
	BEGIN
	--	SELECT * FROM test.EmisorTC
	SELECT TOP 5 
--		[Cli_Pais_Codigo],[Cli_Pais_Desc],[Cli_Nombre],[Cli_Apellido],[Cli_Tipo_Doc_Cod],[Cli_Nro_Doc]
--      ,[Cli_Tipo_Doc_Desc],[Cli_Dom_Calle],[Cli_Dom_Nro],[Cli_Dom_Piso],[Cli_Dom_Depto],[Cli_Fecha_Nac],[Cli_Mail]
--      ,[Cuenta_Numero],[Cuenta_Fecha_Creacion],[Cuenta_Estado],[Cuenta_Pais_Codigo],[Cuenta_Pais_Desc],[Cuenta_Fecha_Cierre]
--      ,[Deposito_Codigo],[Deposito_Fecha],[Deposito_Importe]
      [Tarjeta_Numero] 'Numero',[Tarjeta_Fecha_Emision] 'Fecha Emision',[Tarjeta_Fecha_Vencimiento] 'Fecha Vencimiento',[Tarjeta_Codigo_Seg] 'Codigo',[Tarjeta_Emisor_Descripcion] 'Emisor'
--      ,[Cuenta_Dest_Numero],[Cuenta_Dest_Fecha_Creacion],[Cuenta_Dest_Estado],[Cuenta_Dest_Pais_Codigo],[Cuenta_Dest_Pais_Desc],[Cuenta_Dest_Fecha_Cierre]
--      ,[Transf_Fecha],[Trans_Importe],[Trans_Costo_Trans]
--      ,[Retiro_Fecha],[Retiro_Codigo],[Retiro_Importe]
--      ,[Cheque_Numero],[Cheque_Fecha],[Cheque_Importe]
--      ,[Banco_Cogido],[Banco_Nombre],[Banco_Direccion]
--      ,[Item_Factura_Descr],[Item_Factura_Importe]
--      ,[Factura_Numero],[Factura_Fecha]
  FROM [GD1C2015].[gd_esquema].[Maestra]
  WHERE [Tarjeta_Numero] IS NOT NULL 
	OR [Tarjeta_Emisor_Descripcion] IS NOT NULL

	END
