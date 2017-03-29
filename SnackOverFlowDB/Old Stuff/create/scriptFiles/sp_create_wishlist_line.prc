USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_wishlist_line')
BEGIN
DROP PROCEDURE sp_create_wishlist_line
Print '' print  ' *** dropping procedure sp_create_wishlist_line'
End
GO

Print '' print  ' *** creating procedure sp_create_wishlist_line'
GO
Create PROCEDURE sp_create_wishlist_line
(
@CUSTOMER_ID[INT],
@PRODUCT_ID[INT]
)
AS
BEGIN
INSERT INTO WISHLIST_LINE (CUSTOMER_ID, PRODUCT_ID)
VALUES
(@CUSTOMER_ID, @PRODUCT_ID)
END
