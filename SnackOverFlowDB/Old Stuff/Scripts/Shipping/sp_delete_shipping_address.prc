USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_shipping_address')
BEGIN
DROP PROCEDURE sp_delete_shipping_address
Print '' print  ' *** dropping procedure sp_delete_shipping_address'
End
GO

Print '' print  ' *** creating procedure sp_delete_shipping_address'
GO
Create PROCEDURE sp_delete_shipping_address
(
@ADDRESS_ID[INT]
)
AS
BEGIN
DELETE FROM shipping_address
WHERE ADDRESS_ID = @ADDRESS_ID
END
