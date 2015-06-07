USE [GD1C2015]
GO
/****** Object:  StoredProcedure [SARASA].[cbx_rol]    Script Date: 06/07/2015 15:14:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [SARASA].[cbx_rol]
AS
	SELECT Rol_Id 'Valor', Rol_Descripcion 'Etiqueta' FROM test.Rol
GO
/****** Object:  StoredProcedure [SARASA].[cbx_tipodoc]    Script Date: 06/07/2015 15:14:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [SARASA].[cbx_tipodoc]
AS
	select Tipodoc_Id 'Valor', Tipodoc_Descripcion 'Etiqueta' from SARASA.Tipodoc
GO
