USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_package_line')
BEGIN
DROP PROCEDURE sp_create_package_line
Print '' print  ' *** dropping procedure sp_create_package_line'
End
GO

Print '' print  ' *** creating procedure sp_create_package_line'
GO
Create PROCEDURE sp_create_package_line
(
@PACKAGE_ID[INT],
@PRODUCT_LOT_ID[INT],
@QUANTITY[INT],
@PRICE_PAID[DECIMAL](5,2)
)
AS
BEGIN
INSERT INTO PACKAGE_LINE (PACKAGE_ID, PRODUCT_LOT_ID, QUANTITY, PRICE_PAID)
VALUES
(@PACKAGE_ID, @PRODUCT_LOT_ID, @QUANTITY, @PRICE_PAID)
END
