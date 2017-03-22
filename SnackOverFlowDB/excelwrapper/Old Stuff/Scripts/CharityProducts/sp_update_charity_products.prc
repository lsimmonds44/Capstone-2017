USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_charity_products')
BEGIN
DROP PROCEDURE sp_update_charity_products
Print '' print  ' *** dropping procedure sp_update_charity_products'
End
GO

Print '' print  ' *** creating procedure sp_update_charity_products'
GO
Create PROCEDURE sp_update_charity_products
(
@old_CHARITY_ID[INT],
@old_PRODUCT_LOT_ID[INT],
@old_QUANTITY[INT],
@new_QUANTITY[INT]
)
AS
BEGIN
UPDATE charity_products
SET QUANTITY = @new_QUANTITY
WHERE (CHARITY_ID = @old_CHARITY_ID)
AND (PRODUCT_LOT_ID = @old_PRODUCT_LOT_ID)
AND (QUANTITY = @old_QUANTITY)
END
