USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_product_list')
BEGIN
Drop PROCEDURE sp_retrieve_product_list
Print '' print  ' *** dropping procedure sp_retrieve_product_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_product_list'
GO
Create PROCEDURE sp_retrieve_product_list
AS
BEGIN
SELECT PRODUCT_ID, NAME, DESCRIPTION, UNIT_PRICE, IMAGE_NAME, ACTIVE, UNIT_OF_MEASUREMENT, DELIVERY_CHARGE_PER_UNIT, IMAGE_BINARY
FROM product
END
