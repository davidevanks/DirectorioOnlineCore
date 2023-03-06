
-- =============================================
-- Author:		kenneth Gaitán
-- Description:	sp que implementa logica de busqueda para negocios
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetAllBusinessBySearch]
@Search varchar(max), 
@IdDepartment INT NULL,
@IdCategoria INT NULL
AS
BEGIN
	
	IF @Search='' OR @Search=NULL
	BEGIN
			select B.Id AS BusinessId, B.NombreNegocio AS BusinessName,(SUBSTRING(B.DescripcionNegocio,1,471)+'...') AS BusinessDescription,DEP.Nombre AS DepartmentName, 
			CAT.Nombre AS CategoryName, B.LogoNegocio AS BusinessLogo, ISNULL(dbo.fnc_GetStarsBusiness(B.Id),0) AS BusinessStars from bs.Negocio B 
			INNER JOIN bs.CatCategoria CAT ON CAT.Id=B.IdCategoria
			INNER JOIN bs.CatDepartamento DEP ON B.IdDepartamento=DEP.Id
			WHERE
			B.Status=17 AND B.IdDepartamento=ISNULL(@IdDepartment,B.IdDepartamento) AND B.IdCategoria=ISNULL(@IdCategoria,B.IdCategoria)
		order by ISNULL(dbo.fnc_GetStarsBusiness(B.Id),0) DESC

	END
	ELSE
	BEGIN
		select B.Id AS BusinessId, B.NombreNegocio AS BusinessName,(SUBSTRING(B.DescripcionNegocio,1,471)+'...')  AS BusinessDescription,DEP.Nombre AS DepartmentName, 
		CAT.Nombre AS CategoryName, B.LogoNegocio AS BusinessLogo, ISNULL(dbo.fnc_GetStarsBusiness(B.Id),0) AS BusinessStars from bs.Negocio B 
		INNER JOIN bs.CatCategoria CAT ON CAT.Id=B.IdCategoria
		INNER JOIN bs.CatDepartamento DEP ON B.IdDepartamento=DEP.Id
		WHERE B.Status=17 AND B.IdDepartamento=ISNULL(@IdDepartment,B.IdDepartamento) AND B.IdCategoria=ISNULL(@IdCategoria,B.IdCategoria)
		--AND (CONTAINS(B.DescripcionNegocio, @Search) OR CONTAINS(B.Tags, @Search) OR CONTAINS(B.NombreNegocio, @Search))
		AND (B.DescripcionNegocio LIKE  '%'+@Search+'%'  OR B.Tags LIKE  '%'+@Search+'%' OR B.NombreNegocio LIKE  '%'+@Search+'%' )
		order by ISNULL(dbo.fnc_GetStarsBusiness(B.Id),0) DESC
	END
	
END