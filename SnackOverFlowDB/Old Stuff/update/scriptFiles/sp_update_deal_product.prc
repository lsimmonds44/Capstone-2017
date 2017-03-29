USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_deal_product')
BEGIN
DROP PROCEDURE sp_update_deal_product
Print '' print  ' *** dropping procedure sp_update_deal_product'
End
GO

Print '' print  ' *** creating procedure sp_update_deal_product'
GO
Create PROCEDURE sp_update_deal_product
(
@old_DEAL_ID[INT],
@old_PRODUCT_ID[INT],
@old_ACTIVE[BIT],
@new_ACTIVE[BIT]
)
AS
BEGIN
UPDATE deal_product
SET ACTIVE = @new_ACTIVE
WHERE (DEAL_ID = @old_DEAL_ID)
AND (PRODUCT_ID = @old_PRODUCT_ID)
AND (ACTIVE = @old_ACTIVE)
END
