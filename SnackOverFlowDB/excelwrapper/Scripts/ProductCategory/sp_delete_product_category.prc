USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_product_category')
BEGIN
DROP PROCEDURE sp_delete_product_category
Print '' print  ' *** dropping procedure sp_delete_product_category'
End
GO

Print '' print  ' *** creating procedure sp_delete_product_category'
GO
Create PROCEDURE sp_delete_product_category
(
@PRODUCT_ID[INT],
@CATEGORY_ID[NVARCHAR](200)
)
AS
BEGIN
DELETE FROM product_category
WHERE PRODUCT_ID = @PRODUCT_ID
AND CATEGORY_ID = @CATEGORY_ID
END
