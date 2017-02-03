USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_product_lot')
BEGIN
DROP PROCEDURE sp_delete_product_lot
Print '' print  ' *** dropping procedure sp_delete_product_lot'
End
GO

Print '' print  ' *** creating procedure sp_delete_product_lot'
GO
Create PROCEDURE sp_delete_product_lot
(
@PRODUCT_LOT_ID[INT]
)
AS
BEGIN
DELETE FROM product_lot
WHERE PRODUCT_LOT_ID = @PRODUCT_LOT_ID
END
