USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_shipping_address')
BEGIN
DROP PROCEDURE sp_retrieve_shipping_address
Print '' print  ' *** dropping procedure sp_retrieve_shipping_address'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_shipping_address'
GO
Create PROCEDURE sp_retrieve_shipping_address
(
@ADDRESS_ID[INT]
)
AS
BEGIN
SELECT ADDRESS_ID, USER_ID, ADDRESS1, ADDRESS2, CITY, STATE, ZIP, ADDRESS_NAME
FROM shipping_address
WHERE ADDRESS_ID = @ADDRESS_ID
END
