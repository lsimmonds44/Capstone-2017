USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_shipping_address_list')
BEGIN
Drop PROCEDURE sp_retrieve_shipping_address_list
Print '' print  ' *** dropping procedure sp_retrieve_shipping_address_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_shipping_address_list'
GO
Create PROCEDURE sp_retrieve_shipping_address_list
AS
BEGIN
SELECT ADDRESS_ID, USER_ID, ADDRESS1, ADDRESS2, CITY, STATE, ZIP, ADDRESS_NAME
FROM shipping_address
END
