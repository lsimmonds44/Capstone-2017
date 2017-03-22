USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_product')
BEGIN
DROP PROCEDURE sp_delete_product
Print '' print  ' *** dropping procedure sp_delete_product'
End
GO

Print '' print  ' *** creating procedure sp_delete_product'
GO
Create PROCEDURE sp_delete_product
(
@PRODUCT_ID[INT]
)
AS
BEGIN
DELETE FROM product
WHERE PRODUCT_ID = @PRODUCT_ID
END
