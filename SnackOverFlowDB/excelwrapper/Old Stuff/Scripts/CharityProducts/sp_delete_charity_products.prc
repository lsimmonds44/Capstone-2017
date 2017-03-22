USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_charity_products')
BEGIN
DROP PROCEDURE sp_delete_charity_products
Print '' print  ' *** dropping procedure sp_delete_charity_products'
End
GO

Print '' print  ' *** creating procedure sp_delete_charity_products'
GO
Create PROCEDURE sp_delete_charity_products
(
@CHARITY_ID[INT],
@PRODUCT_LOT_ID[INT]
)
AS
BEGIN
DELETE FROM charity_products
WHERE CHARITY_ID = @CHARITY_ID
AND PRODUCT_LOT_ID = @PRODUCT_LOT_ID
END
