USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_category')
BEGIN
DROP PROCEDURE sp_delete_category
Print '' print  ' *** dropping procedure sp_delete_category'
End
GO

Print '' print  ' *** creating procedure sp_delete_category'
GO
Create PROCEDURE sp_delete_category
(
@CATEGORY_ID[NVARCHAR](200)
)
AS
BEGIN
DELETE FROM category
WHERE CATEGORY_ID = @CATEGORY_ID
END
