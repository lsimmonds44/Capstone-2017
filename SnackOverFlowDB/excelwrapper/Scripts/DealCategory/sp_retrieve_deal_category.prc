USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_deal_category')
BEGIN
DROP PROCEDURE sp_retrieve_deal_category
Print '' print  ' *** dropping procedure sp_retrieve_deal_category'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_deal_category'
GO
Create PROCEDURE sp_retrieve_deal_category
(
@DEAL_ID[INT],
@CATEGORY_ID[NVARCHAR](200)
)
AS
BEGIN
SELECT DEAL_ID, CATEGORY_ID, ACTIVE
FROM deal_category
WHERE DEAL_ID = @DEAL_ID
AND CATEGORY_ID = @CATEGORY_ID
END
