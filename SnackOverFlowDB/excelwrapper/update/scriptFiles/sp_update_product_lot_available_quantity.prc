USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_product_lot_available_quantity')
BEGIN
DROP PROCEDURE sp_update_product_lot_available_quantity
Print '' print  ' *** dropping procedure sp_update_product_lot_available_quantity'
End
GO

Print '' print  ' *** creating procedure sp_update_product_lot_available_quantity'
GO
Create PROCEDURE sp_update_product_lot_available_quantity
(
@PRODUCT_LOT_ID[INT],
@old_AVAILABLE_QUANTITY[INT],
@new_AVAILABLE_QUANTITY[INT]
)
AS
BEGIN
UPDATE product_lot
SET AVAILABLE_QUANTITY = @new_AVAILABLE_QUANTITY
WHERE (PRODUCT_LOT_ID = @PRODUCT_LOT_ID)
AND AVAILABLE_QUANTITY = @old_AVAILABLE_QUANTITY
RETURN @@ROWCOUNT
END
