USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_charity_products')
BEGIN
DROP PROCEDURE sp_create_charity_products
Print '' print  ' *** dropping procedure sp_create_charity_products'
End
GO

Print '' print  ' *** creating procedure sp_create_charity_products'
GO
Create PROCEDURE sp_create_charity_products
(
@CHARITY_ID[INT],
@PRODUCT_LOT_ID[INT],
@QUANTITY[INT]
)
AS
BEGIN
INSERT INTO CHARITY_PRODUCTS (CHARITY_ID, PRODUCT_LOT_ID, QUANTITY)
VALUES
(@CHARITY_ID, @PRODUCT_LOT_ID, @QUANTITY)
END
