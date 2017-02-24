USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_category')
BEGIN
DROP PROCEDURE sp_retrieve_category
Print '' print  ' *** dropping procedure sp_retrieve_category'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_category'
GO
Create PROCEDURE sp_retrieve_category
(
@CATEGORY_ID[NVARCHAR](200)
)
AS
BEGIN
SELECT CATEGORY_ID
FROM category
WHERE CATEGORY_ID = @CATEGORY_ID
END
