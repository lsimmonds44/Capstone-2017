USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_package')
BEGIN
DROP PROCEDURE sp_update_package
Print '' print  ' *** dropping procedure sp_update_package'
End
GO

Print '' print  ' *** creating procedure sp_update_package'
GO
Create PROCEDURE sp_update_package
(
@old_PACKAGE_ID[INT],
@old_DELIVERY_ID[INT],
@new_DELIVERY_ID[INT],
@old_ORDER_ID[INT],
@new_ORDER_ID[INT]
)
AS
BEGIN
UPDATE package
SET DELIVERY_ID = @new_DELIVERY_ID, ORDER_ID = @new_ORDER_ID
WHERE (PACKAGE_ID = @old_PACKAGE_ID)
AND (DELIVERY_ID = @old_DELIVERY_ID)
AND (ORDER_ID = @old_ORDER_ID)
END
