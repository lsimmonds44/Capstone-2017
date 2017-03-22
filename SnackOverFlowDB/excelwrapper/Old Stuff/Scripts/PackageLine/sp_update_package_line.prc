USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_package_line')
BEGIN
DROP PROCEDURE sp_update_package_line
Print '' print  ' *** dropping procedure sp_update_package_line'
End
GO

Print '' print  ' *** creating procedure sp_update_package_line'
GO
Create PROCEDURE sp_update_package_line
(
@old_PACKAGE_LINE_ID[INT],
@old_PACKAGE_ID[INT],
@new_PACKAGE_ID[INT],
@old_PRODUCT_LOT_ID[INT],
@new_PRODUCT_LOT_ID[INT],
@old_QUANTITY[INT],
@new_QUANTITY[INT],
@old_PRICE_PAID[DECIMAL](5,2),
@new_PRICE_PAID[DECIMAL](5,2)
)
AS
BEGIN
UPDATE package_line
SET PACKAGE_ID = @new_PACKAGE_ID, PRODUCT_LOT_ID = @new_PRODUCT_LOT_ID, QUANTITY = @new_QUANTITY, PRICE_PAID = @new_PRICE_PAID
WHERE (PACKAGE_LINE_ID = @old_PACKAGE_LINE_ID)
AND (PACKAGE_ID = @old_PACKAGE_ID)
AND (PRODUCT_LOT_ID = @old_PRODUCT_LOT_ID)
AND (QUANTITY = @old_QUANTITY)
AND (PRICE_PAID = @old_PRICE_PAID)
END
