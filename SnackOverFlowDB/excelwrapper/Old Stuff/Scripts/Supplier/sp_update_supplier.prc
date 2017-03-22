USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_supplier')
BEGIN
DROP PROCEDURE sp_update_supplier
Print '' print  ' *** dropping procedure sp_update_supplier'
End
GO

Print '' print  ' *** creating procedure sp_update_supplier'
GO
Create PROCEDURE sp_update_supplier
(
@old_SUPPLIER_ID[INT],
@old_USER_ID[INT],
@new_USER_ID[INT],
@old_IS_APPROVED[BIT],
@new_IS_APPROVED[BIT],
@old_APPROVED_BY[INT],
@new_APPROVED_BY[INT],
@old_FARM_TAX_ID[NVARCHAR](64),
@new_FARM_TAX_ID[NVARCHAR](64)
)
AS
BEGIN
UPDATE supplier
SET USER_ID = @new_USER_ID, IS_APPROVED = @new_IS_APPROVED, APPROVED_BY = @new_APPROVED_BY, FARM_TAX_ID = @new_FARM_TAX_ID
WHERE (SUPPLIER_ID = @old_SUPPLIER_ID)
AND (USER_ID = @old_USER_ID)
AND (IS_APPROVED = @old_IS_APPROVED)
AND (APPROVED_BY = @old_APPROVED_BY)
AND (FARM_TAX_ID = @old_FARM_TAX_ID)
END
