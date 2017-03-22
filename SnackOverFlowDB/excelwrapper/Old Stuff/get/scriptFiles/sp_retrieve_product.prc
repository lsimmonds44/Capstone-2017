USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_product')
BEGIN
DROP PROCEDURE sp_retrieve_product
Print '' print  ' *** dropping procedure sp_retrieve_product'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_product'
GO
Create PROCEDURE sp_retrieve_product
(
@PRODUCT_ID[INT]
)
AS
BEGIN
SELECT PRODUCT_ID, NAME, DESCRIPTION, UNIT_PRICE, IMAGE_NAME, ACTIVE, UNIT_OF_MEASUREMENT, DELIVERY_CHARGE_PER_UNIT
FROM product
WHERE PRODUCT_ID = @PRODUCT_ID
END
