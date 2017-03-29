USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_product_category')
BEGIN
DROP PROCEDURE sp_retrieve_product_category
Print '' print  ' *** dropping procedure sp_retrieve_product_category'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_product_category'
GO
Create PROCEDURE sp_retrieve_product_category
(
@PRODUCT_ID[INT],
@CATEGORY_ID[NVARCHAR](200)
)
AS
BEGIN
SELECT PRODUCT_ID, CATEGORY_ID
FROM product_category
WHERE PRODUCT_ID = @PRODUCT_ID
AND CATEGORY_ID = @CATEGORY_ID
END
