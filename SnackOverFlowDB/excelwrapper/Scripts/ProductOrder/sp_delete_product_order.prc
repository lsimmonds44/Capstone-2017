USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_product_order')
BEGIN
DROP PROCEDURE sp_delete_product_order
Print '' print  ' *** dropping procedure sp_delete_product_order'
End
GO

Print '' print  ' *** creating procedure sp_delete_product_order'
GO
Create PROCEDURE sp_delete_product_order
(
@ORDER_ID[INT]
)
AS
BEGIN
DELETE FROM product_order
WHERE ORDER_ID = @ORDER_ID
END
