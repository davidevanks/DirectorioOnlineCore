
-- =============================================
-- Author:		kenneth Gaitán
-- Description:	este es un test
-- =============================================
CREATE PROCEDURE usp_GetAllCategoriesTest

AS
BEGIN
	select * from [bs].CatCategoria where IdPadre=0
END