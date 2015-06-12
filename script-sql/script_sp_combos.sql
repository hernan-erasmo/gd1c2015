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
