
-- =============================================
-- Author:		KENNETH GAITÁN
-- Create date: 17/05/2022
-- Description:	Calcula estrellas de un negocio
-- =============================================
CREATE FUNCTION [dbo].[fnc_GetStarsBusiness]
(
	-- Add the parameters for the function here
	@BusinessId INT
)
RETURNS INT
AS
BEGIN
	

	DECLARE @TotalStars INT=0;
	DECLARE @TotalsRegisters INT =0;
	DECLARE @Result INT =0;

	SET @TotalStars=(SELECT SUM(Stars) FROM BS.Reviews WHERE Active=1 AND IdBusiness=@BusinessId);
	SET @TotalsRegisters=(SELECT COUNT(Stars) FROM BS.Reviews WHERE Active=1 AND IdBusiness=@BusinessId);

	SET @Result=@TotalStars/@TotalsRegisters;
	-- Return the result of the function
	RETURN @Result

END