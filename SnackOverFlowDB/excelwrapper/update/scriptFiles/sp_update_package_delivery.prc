USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_package_delivery')
BEGIN
DROP PROCEDURE sp_update_package_delivery
Print '' print  ' *** dropping procedure sp_update_package_delivery'
End
GO

Print '' print  ' *** creating procedure sp_update_package_delivery'
GO
Create PROCEDURE sp_update_package_delivery
(
@PACKAGE_ID[INT],
@new_DELIVERY_ID[INT]
)
AS
BEGIN
UPDATE package
SET DELIVERY_ID = @new_DELIVERY_ID
WHERE (PACKAGE_ID = @PACKAGE_ID)
END
