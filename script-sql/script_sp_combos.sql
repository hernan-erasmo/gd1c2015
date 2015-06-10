CREATE PROCEDURE [SARASA].[cbx_rol]
AS
	SELECT Rol_Id 'Valor', Rol_Descripcion 'Etiqueta' FROM test.Rol
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

